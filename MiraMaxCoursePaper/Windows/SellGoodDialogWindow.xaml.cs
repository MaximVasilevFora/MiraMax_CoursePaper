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
using WorkingLibrary;

namespace MiraMaxCoursePaper.Windows
{
	/// <summary>
	/// Interaction logic for SellGoodDialogWindow.xaml
	/// </summary>
	public partial class SellGoodDialogWindow : Window
	{
		public Pages.GoodPage goodPage;

		private WorkingLibrary.Models.Good _good;

		private const string _GoodNumber = "Число не более ";

		public SellGoodDialogWindow(object sender)
		{
			InitializeComponent();

			_good = (sender as Button).DataContext as WorkingLibrary.Models.Good;

			InitPage();
		}

		private void InitPage()
		{ 
			QuantityTextBox.Text = _GoodNumber + _good.Quantity.ToString();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			goodPage.sellGoodDialog = null;
			goodPage.RefreshGoodList();
		}

		private void Quantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (!Char.IsDigit(e.Text, 0))
			{
				e.Handled = true;
			}
		}

		private void Price_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (!Char.IsDigit(e.Text, 0))
			{
				e.Handled = true;
			}
		}

		private void Enter_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(Quantity.Text))
			{
				MessageBox.Show("Введите значение в поле Количество товара");
				return;
			}

			if (Convert.ToInt32(Quantity.Text) < 1)
			{
				MessageBox.Show("Введите значение в поле Количество больше чем Ноль");
				return;
			}

			if (string.IsNullOrEmpty(Price.Text))
			{
				MessageBox.Show("Введите значение в поле Общая стоимость товаров");
				return;
			}

			if (Convert.ToInt32(Quantity.Text) > _good.Quantity)
			{
				MessageBox.Show("Количество проданного товара превышает количество товара в наличии");
				return;
			}

			int lastQuantity = _good.Quantity - Convert.ToInt32(Quantity.Text);

			var update = DataWorking.UpdateGoodForSell(_good.Id, lastQuantity);
			var insert = DataWorking.InsertGoodHistory(_good.Id, Convert.ToInt32(Quantity.Text), Convert.ToInt32(Price.Text), Description.Text);

			if (DataWorking.SelectGoodQuantity(_good.Id) < 1)
			{
				var updateStatus = DataWorking.UpdateGoodStatus(_good.Id, 2);

				if (!updateStatus)
				{
					return;
				}
			}

			if (update && insert)
			{
				MessageBox.Show("Данные сохранены");

				DataWorking.InsertActionToAudit(DateTime.Now, DataWorking.EventType[5], CurrentUser.Id);
			}

			this.Close();
		}

	}
}
