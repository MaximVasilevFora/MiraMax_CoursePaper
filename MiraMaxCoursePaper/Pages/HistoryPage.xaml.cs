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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkingLibrary;

namespace MiraMaxCoursePaper.Pages
{
	public partial class HistoryPage : Page
	{
		private WorkingLibrary.Models.HistoryOfGood historyOfGood;

		public HistoryPage()
		{
			InitializeComponent();

			InitPage();
		}

		private void InitPage()
		{
			Audit.ItemsSource = DataWorking.SelectHistoryOfGood(CurrentUser.Id).OrderBy(x => x.SaleDate).ToList();
		}

		/// <summary>
		/// Удаляет историю продажи товара.
		/// </summary>
		private void EditColumn_Click(object sender, RoutedEventArgs e)
		{
			var result = System.Windows.MessageBox.Show("Вы уверены, что хотите удалить запись продажи?",
				"Confirmation",
				MessageBoxButton.YesNo,
				MessageBoxImage.Question);

			if (result == MessageBoxResult.No)
			{
				return;
			}

			historyOfGood = (sender as Button).DataContext as WorkingLibrary.Models.HistoryOfGood;

			var delete = DataWorking.DeleteGoodHistory(historyOfGood.Id);
			var update = DataWorking.UpdateGoodAfterDeletion(historyOfGood.IdGood, historyOfGood.SaleQuantity);

			if (DataWorking.SelectGoodQuantity(historyOfGood.IdGood) > 0 && DataWorking.UpdateGoodStatus(historyOfGood.IdGood, 1))
			{ 
				
			}

			if (delete && update)
			{
				InitPage();
				MessageBox.Show("Запись продажи была успешно удалена");

				DataWorking.InsertActionToAudit(DateTime.Now, DataWorking.EventType[6], CurrentUser.Id);
			}
		}
	}
}
