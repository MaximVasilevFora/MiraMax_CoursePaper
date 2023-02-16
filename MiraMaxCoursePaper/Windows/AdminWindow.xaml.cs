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
	public partial class AdminWindow : Window
	{
		private Windows.AuthorizationWindow _authorizationWindow;
		public Windows.EditUserWindow editUserWindow;

		public AdminWindow()
		{
			InitializeComponent();

			InitPage();
		}

		public void InitPage()
		{
			Users.ItemsSource = DataWorking.SelectUsers();
		}

		private void More_Click(object sender, RoutedEventArgs e)
		{
			if (editUserWindow == null)
			{
				editUserWindow = new EditUserWindow(sender);
				editUserWindow.adminWindow = this;
				editUserWindow.ShowDialog();
			}
		}

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			if (_authorizationWindow == null)
			{ 
				_authorizationWindow = new Windows.AuthorizationWindow();
				_authorizationWindow.Show();
				this.Close();
			}
		}
	}
}
