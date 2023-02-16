using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using WorkingLibrary;

namespace MiraMaxCoursePaper.Pages
{
	public partial class GoodPage : Page
	{
		private const string _GoodName = "Наименование товара";

		private string _FindedGoodName;

		public Windows.AddingProduct product;
		public Windows.EditGoodWindow editGoodWindow;
		public Windows.SellGoodDialogWindow sellGoodDialog;

		private List<WorkingLibrary.Models.Good> _listOfGood;

		public GoodPage()
		{
			InitializeComponent();

			GoodStatus.ItemsSource = DataWorking.SelectAllGoodStatus().ToList();

			_listOfGood = GetGoodsList(SelectGoods());

			GoodsList.ItemsSource = GetGoodsList(SelectGoods());

			GoodStatus.SelectedIndex = 4;
		}

		public void InitGoodList()
		{
			GoodsList.ItemsSource = GetGoodsList(SelectGoods());
		}

		private List<WorkingLibrary.Models.Good> GetGoodsList(DataRow[] dataTable)
		{
			var goodList = new List<WorkingLibrary.Models.Good>();

			foreach (var data in dataTable)
			{
				var good = new WorkingLibrary.Models.Good();

				good.Id = Convert.ToInt32(data[0]);
				good.Name = Convert.ToString(data[1]);
				good.ArticleNumber = Convert.ToInt32(data[2]);
				good.Price = Convert.ToInt32(data[3]);
				good.Quantity = Convert.ToInt32(data[4]);
				good.Sold = Convert.ToInt32(data[5]);

				if (data[6] != System.DBNull.Value)
				{
					good.Cost = Convert.ToInt32(data[6]);
				}

				if (data[7] == System.DBNull.Value)
				{
					var bitmap = new Bitmap(MiraMaxCoursePaper.Properties.Resources.EmptyImageGood);
					good.Image = DataWorking.ImageToByte(bitmap);
				}
				else
				{
					good.Image = (byte[])data[7];
				}
				
				good.PurchaseDate = Convert.ToDateTime(data[8]);

				if (data[9] != System.DBNull.Value)
				{
					good.DeletionDate = Convert.ToDateTime(data[9]);
				}

				good.ExpirationDate = Convert.ToDateTime(data[10]);

				if (data[11] != System.DBNull.Value)
				{
					good.Description = Convert.ToString(data[11]);
				}
				
				good.IdCountry = Convert.ToInt32(data[12]);
				good.IdGroup = Convert.ToInt32(data[13]);
				good.IdStatus = Convert.ToInt32(data[14]);
				good.IdUser = Convert.ToInt32(data[15]);

				goodList.Add(good);
			}

			return goodList;
		}

		private DataRow[] SelectGoods()
		{
			var db = new MiraMaxDataBase();

			var dataTable = new DataTable();

			var adapter = new MySqlDataAdapter();

			var sqlCommands = new MySqlCommand("SELECT * FROM `good`" +
				"WHERE `id_user` = @userId",
				db.GetConnection());

			sqlCommands.Parameters.Add("@userId", MySqlDbType.VarChar).Value = CurrentUser.Id;
			adapter.SelectCommand = sqlCommands;
			
			adapter.Fill(dataTable);

			return dataTable.Select();
		}

		private void GoodNameBox_GotFocus(object sender, RoutedEventArgs e)
		{
			GoodNameBox.Text = GoodNameBox.Text == _GoodName ? "" : GoodNameBox.Text;
		}

		private void GoodNameBox_LostFocus(object sender, RoutedEventArgs e)
		{
			GoodNameBox.Text = GoodNameBox.Text == String.Empty ? _GoodName : GoodNameBox.Text;
		}

		private void AddGood_Click(object sender, RoutedEventArgs e)
		{
			if (product == null)
			{
				product = new Windows.AddingProduct();
				product.goodPage = this;
				product.Show();
			}
		}

		private void GoodStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			RefreshGoodList();
		}

		private void GoodNameBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (GoodNameBox.Text == _GoodName)
			{
				return;
			}

			_FindedGoodName = GoodNameBox.Text;

			RefreshGoodList();
		}

		public void RefreshGoodList()
		{
			_listOfGood = GetGoodsList(SelectGoods());

			if (GoodStatus.SelectedItem != null)
			{
				var status = (WorkingLibrary.Models.GoodStatus)GoodStatus.SelectedValue;

				if (status.Id != 5)
				{
					_listOfGood = _listOfGood.Where(g => g.IdStatus == status.Id).ToList();
				}
			}

			if (GoodNameBox.Text != "" && GoodNameBox.Text != _GoodName)
			{
				_listOfGood = _listOfGood.Where(g => g.Name.ToLower().Contains(_FindedGoodName.ToLower())).ToList();
			}

			GoodsList.ItemsSource = _listOfGood;
		}

		/// <summary>
		/// Функция продажи опр-го количества товаров.
		/// </summary>
		private void EditGood_Click(object sender, RoutedEventArgs e)
		{
			if (sellGoodDialog == null)
			{
				sellGoodDialog = new Windows.SellGoodDialogWindow(sender);
				sellGoodDialog.goodPage = this;
				sellGoodDialog.Show();

				editGoodWindow.Close();
				editGoodWindow = null;
			}
		}

		private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if ((sender is Button))
			{
				return;
			}

			if (editGoodWindow == null)
			{
				editGoodWindow = new Windows.EditGoodWindow(sender);
				editGoodWindow.goodPage = this;
				editGoodWindow.Show();
			}
		}
	}
}
