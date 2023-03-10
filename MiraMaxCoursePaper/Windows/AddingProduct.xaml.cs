using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Forms;
using WorkingLibrary;
using System.Drawing;
using MySql.Data.MySqlClient;

namespace MiraMaxCoursePaper.Windows
{
	/// <summary>
	/// Interaction logic for AddingProduct.xaml
	/// </summary>
	public partial class AddingProduct : Window
	{
		private const string _GoodName = "Имя";
		private const string _Article = "Артикул";
		private const string _Price = "Цена покупки";
		private const string _Quantity = "Количество";
		private const string _Cost = "Стоимость";
		private const string _Description = "Описание";

		private byte[] _goodImageOfByte = null;

		private System.Windows.Forms.OpenFileDialog _openFileDialog = new System.Windows.Forms.OpenFileDialog();

		public Pages.GoodPage goodPage;

		public AddingProduct()
		{
			InitializeComponent();

			_openFileDialog.Filter = "Png files(*.png)|*.png|Jpg files(*.jpg)|*.jpg|Jpeg files(*.jpeg)|*.jpeg";

			Country.Items.Clear();
			Country.ItemsSource = DataWorking.SelectAllCountry();
			GoodGroup.Items.Clear();
			GoodGroup.ItemsSource = DataWorking.SelectAllGoodGroup();
			GoodStatus.Items.Clear();
			GoodStatus.ItemsSource = DataWorking.SelectAllGoodStatusWhereLastEmpty();
		}

		private void GoodName_GotFocus(object sender, RoutedEventArgs e)
		{
			GoodName.Text = GoodName.Text == _GoodName ? "" : GoodName.Text;
		}

		private void GoodName_LostFocus(object sender, RoutedEventArgs e)
		{
			GoodName.Text = GoodName.Text == String.Empty ? _GoodName : GoodName.Text;
		}

		private void Article_GotFocus(object sender, RoutedEventArgs e)
		{
			Article.Text = Article.Text == _Article ? "" : Article.Text;
		}

		private void Article_LostFocus(object sender, RoutedEventArgs e)
		{
			Article.Text = Article.Text == String.Empty ? _Article : Article.Text;
		}

		private void Price_GotFocus(object sender, RoutedEventArgs e)
		{
			Price.Text = Price.Text == _Price ? "" : Price.Text;
		}

		private void Price_LostFocus(object sender, RoutedEventArgs e)
		{
			Price.Text = Price.Text == String.Empty ? _Price : Price.Text;
		}

		private void Quantity_GotFocus(object sender, RoutedEventArgs e)
		{
			Quantity.Text = Quantity.Text == _Quantity ? "" : Quantity.Text;
		}

		private void Quantity_LostFocus(object sender, RoutedEventArgs e)
		{
			Quantity.Text = Quantity.Text == String.Empty ? _Quantity : Quantity.Text;
		}

		private void Cost_GotFocus(object sender, RoutedEventArgs e)
		{
			Cost.Text = Cost.Text == _Cost ? "" : Cost.Text;
		}

		private void Cost_LostFocus(object sender, RoutedEventArgs e)
		{
			Cost.Text = Cost.Text == String.Empty ? _Cost : Cost.Text;
		}

		private void Description_GotFocus(object sender, RoutedEventArgs e)
		{
			Description.Text = Description.Text == _Description ? "" : Description.Text;
		}

		private void Description_LostFocus(object sender, RoutedEventArgs e)
		{
			Description.Text = Description.Text == String.Empty ? _Description : Description.Text;
		}

		private void Image_Click(object sender, RoutedEventArgs e)
		{
			if (_openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				var image = new Bitmap(_openFileDialog.FileName);

				Way.Text = "Путь:" + _openFileDialog.FileName;

				_goodImageOfByte = DataWorking.ImageToByte(image);
			}
		}

		private void Article_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = SimbolIsDigit(e);
		}

		private bool SimbolIsDigit(TextCompositionEventArgs e)
		{
			if (!Char.IsDigit(e.Text, 0))
			{
				return true;
			}

			return false;
		}

		private void Price_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = SimbolIsDigit(e);
		}

		private void Quantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = SimbolIsDigit(e);
		}

		private void Cost_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = SimbolIsDigit(e);
		}

		private void Save_Click(object sender, RoutedEventArgs e)
		{
			if (String.IsNullOrEmpty(GoodName.Text) || GoodName.Text == _GoodName)
			{
				System.Windows.MessageBox.Show("Введите значение в поле Имя товара");
				return;
			}

			if (String.IsNullOrEmpty(Article.Text) || Article.Text == _Article)
			{
				System.Windows.MessageBox.Show("Введите значение в поле Артикул");
				return;
			}

			if (String.IsNullOrEmpty(Price.Text) || Price.Text == _Price)
			{
				System.Windows.MessageBox.Show("Введите значение в поле Цена покупки");
				return;
			}

			if (String.IsNullOrEmpty(Quantity.Text) || Quantity.Text == _Quantity)
			{
				System.Windows.MessageBox.Show("Введите значение в поле Количество");
				return;
			}

			if (PurchaseDate.SelectedDate == null)
			{
				System.Windows.MessageBox.Show("Выберите значение в поле Дата покупки");
				return;
			}

			if (PurchaseDate.SelectedDate > DateTime.Now)
			{
				System.Windows.MessageBox.Show("Текущая дата меньше даты закупки");
				return;
			}

			if (ExpirationDate.SelectedDate == null)
			{
				System.Windows.MessageBox.Show("Выберите значение в поле Срок годности");
				return;
			}

			if (ExpirationDate.SelectedDate <= DateTime.Now)
			{
				System.Windows.MessageBox.Show("Дата окончания срока годности меньше текущей даты");
				return;
			}

			if (Country.SelectedItem == null)
			{
				System.Windows.MessageBox.Show("Выберите значение в поле Страна");
				return;
			}

			if (GoodGroup.SelectedItem == null)
			{
				System.Windows.MessageBox.Show("Выберите значение в поле Группа товара");
				return;
			}

			if (GoodStatus.SelectedItem == null)
			{
				System.Windows.MessageBox.Show("Выберите значение в поле Статус товара");
				return;
			}

			if (DataWorking.CheckGoodArticle(Article.Text, CurrentUser.Id))
			{
				var result = System.Windows.MessageBox.Show("Товар с таким артикулом уже существует, все равно добавить?",
				"Confirmation",
				MessageBoxButton.YesNo,
				MessageBoxImage.Question);

				if (result == MessageBoxResult.No)
				{
					return;
				}
			}

			var dataBase = new MiraMaxDataBase();
			var mySqlCommand = new MySqlCommand("INSERT INTO `good`" +
				"(`name`, `article_number`, `price`, `quantity`, `cost`, `image`, `purchase_date`, `expiration_date`, `description`, `id_country`, `id_group`, `id_status`, `id_user`) " +
				"VALUES (@name,@article_number,@price,@quantity,@cost,@image,@purchase_date,@expiration_date,@description,@id_country,@id_group,@id_status,@id_user)",
				dataBase.GetConnection());

			mySqlCommand.Parameters.Add("@name", MySqlDbType.VarChar).Value = GoodName.Text;
			mySqlCommand.Parameters.Add("@article_number", MySqlDbType.Int32).Value = Convert.ToInt32(Article.Text);
			mySqlCommand.Parameters.Add("@price", MySqlDbType.Int32).Value = Convert.ToInt32(Price.Text);
			mySqlCommand.Parameters.Add("@quantity", MySqlDbType.Int32).Value = Convert.ToInt32(Quantity.Text);
			mySqlCommand.Parameters.Add("@cost", MySqlDbType.Int32).Value = Cost.Text == _Cost ? 0 : Convert.ToInt32(Cost.Text);
			mySqlCommand.Parameters.Add("@image", MySqlDbType.MediumBlob).Value = _goodImageOfByte;
			mySqlCommand.Parameters.Add("@purchase_date", MySqlDbType.VarChar).Value = PurchaseDate.SelectedDate.Value.ToString("yyyy-MM-dd");
			mySqlCommand.Parameters.Add("@expiration_date", MySqlDbType.VarChar).Value = ExpirationDate.SelectedDate.Value.ToString("yyyy-MM-dd");
			mySqlCommand.Parameters.Add("@description", MySqlDbType.VarChar).Value = Description.Text == _Description ? null : Description.Text;

			WorkingLibrary.Models.Country country = (WorkingLibrary.Models.Country)Country.SelectedValue;
			WorkingLibrary.Models.GoodGroup goodGroup = (WorkingLibrary.Models.GoodGroup)GoodGroup.SelectedValue;
			WorkingLibrary.Models.GoodStatus status = (WorkingLibrary.Models.GoodStatus)GoodStatus.SelectedValue;

			if (Convert.ToInt32(Quantity.Text) < 1 && status.Id == 1)
			{
				status.Id = 2;
			}

			if (Convert.ToInt32(Quantity.Text) > 0 && status.Id == 2)
			{
				status.Id = 1;
			}

			mySqlCommand.Parameters.Add("@id_country", MySqlDbType.Int32).Value = country.Id;
			mySqlCommand.Parameters.Add("@id_group", MySqlDbType.Int32).Value = goodGroup.Id;
			mySqlCommand.Parameters.Add("@id_status", MySqlDbType.Int32).Value = status.Id;
			mySqlCommand.Parameters.Add("@id_user", MySqlDbType.Int32).Value = WorkingLibrary.CurrentUser.Id;

			dataBase.OpenConnection();

			if (mySqlCommand.ExecuteNonQuery() == 1)
			{
				System.Windows.MessageBox.Show("Товар успешно добавлен");

				DataWorking.InsertActionToAudit(DateTime.Now, DataWorking.EventType[2], CurrentUser.Id);
			}
			else
			{
				System.Windows.MessageBox.Show("Товар не был добавлен");
			}

			dataBase.CloseConnection();

			goodPage.InitGoodList();
			this.Close();
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			GoodName.Text = _GoodName;
			Article.Text = _Article;
			Price.Text = _Price;
			Quantity.Text = _Quantity;
			Cost.Text = _Cost;
			Description.Text = _Description;

			PurchaseDate.Text = null;
			ExpirationDate.Text = null;

			Country.SelectedItem = null;
			GoodGroup.SelectedItem = null;
			GoodStatus.SelectedItem = null;

			_goodImageOfByte = null;
			Way.Text = "Путь:";
		}

		private void CancelImage_Click(object sender, RoutedEventArgs e)
		{
			_goodImageOfByte = null;

			Way.Text = "Путь:";
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			goodPage.product = null;

			goodPage.RefreshGoodList();
		}
	}
}
