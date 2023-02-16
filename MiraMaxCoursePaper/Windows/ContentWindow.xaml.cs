using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
	public partial class ContentWindow : Window
	{
		private AuthorizationWindow _authorizationWindow;

		public ContentWindow()
		{
			InitializeComponent();

			ChangePage("Pages/WelcomePage.xaml");
			InitWindow();
		}

		public void InitWindow()
		{
			UserName.Text = CurrentUser.Name;
			UserMail.Text = CurrentUser.Mail;

			if (CurrentUser.Image != null)
			{
				var image = DataWorking.GetBitmapImage(CurrentUser.Image);
				var brush = new ImageBrush();
				brush.ImageSource = image;
				Profil.Background = brush;
			}
			else
			{
				var bitmap = new Bitmap(MiraMaxCoursePaper.Properties.Resources.EmptyImageUser);
				var array = DataWorking.ImageToByte(bitmap);
				Profil.Background = DataWorking.CreateImageFromByte(array);
			}
		}

		public void ChangePage(string url)
		{
			ContentFrame.NavigationService.Navigate(new Uri(url, UriKind.Relative));
			Pages.WelcomePage.contentWindow = this;
			Pages.ProfilPage.contentWindow = this;
		}

		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			
		}

		private void MakeEnableButton()
		{ 
			var buttons = HeaderButtons.Children.OfType<Button>().ToList();

			foreach (var item in buttons)
			{ 
				item.IsEnabled = true;
			}

			Profil.IsEnabled = true;
		}

		private void HideButton(Button button)
		{
			button.IsEnabled = false;
		}

		private void Goods_Click(object sender, RoutedEventArgs e)
		{
			MakeEnableButton();
			ChangePage("Pages/GoodPage.xaml");
			HideButton((Button)sender);
		}

		private void Money_Click(object sender, RoutedEventArgs e)
		{
			MakeEnableButton();
			ChangePage("Pages/MoneyPage.xaml");
			HideButton((Button)sender);
		}

		private void History_Click(object sender, RoutedEventArgs e)
		{
			MakeEnableButton();
			ChangePage("Pages/HistoryPage.xaml");
			HideButton((Button)sender);
		}

		private void Indicators_Click(object sender, RoutedEventArgs e)
		{
			MakeEnableButton();
			ChangePage("Pages/IndicatorsPage.xaml");
			HideButton((Button)sender);
		}

		private void Audit_Click(object sender, RoutedEventArgs e)
		{
			MakeEnableButton();
			ChangePage("Pages/AuditPage.xaml");
			HideButton((Button)sender);
		}

		private void Profil_Click(object sender, RoutedEventArgs e)
		{
			MakeEnableButton();
			ChangePage("Pages/ProfilPage.xaml");
			HideButton((Button)sender);
		}

		private void DeleteCurrentUser()
		{
			CurrentUser.Id = -1;
			CurrentUser.SurName = null;
			CurrentUser.Name = null;
			CurrentUser.Patronymic = null;
			CurrentUser.Login = null;
			CurrentUser.Password = null;
			CurrentUser.Phone = null;
			CurrentUser.Mail = null;
			CurrentUser.Image = null;
			CurrentUser.Role = -1;
			CurrentUser.Ban = false;
			CurrentUser.Description = null;
		}

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			if (_authorizationWindow == null)
			{
				_authorizationWindow = new AuthorizationWindow();

				DeleteCurrentUser();

				_authorizationWindow.Show();
				this.Close();
			}
		}
	}
}
