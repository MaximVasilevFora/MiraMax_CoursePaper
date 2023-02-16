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
	/// Interaction logic for PasswordDialogWindow.xaml
	/// </summary>
	public partial class PasswordDialogWindow : Window
	{
		public Windows.RegistrationWindow registrationWindow;
		
		public PasswordDialogWindow()
		{
			InitializeComponent();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			registrationWindow.passwordDialogWindow = null;
		}

		private void Enter_Click(object sender, RoutedEventArgs e)
		{
			if (String.IsNullOrEmpty(LetterNumberTextBox.Text))
			{
				MessageBox.Show("Введите значение в поле");
				return;
			}

			if (Convert.ToInt32(LetterNumberTextBox.Text) < 5)
			{
				MessageBox.Show("Введите число более 5");
				return;
			}

			if (Convert.ToInt32(LetterNumberTextBox.Text) > 100)
			{
				MessageBox.Show("Введите число менее 101");
				return;
			}

			var password = PasswordWorking.CreatePassword(Convert.ToInt32(LetterNumberTextBox.Text));

			registrationWindow.Password.Text = password;
			registrationWindow.SecondPassword.Text = password;
		}

		private void LetterNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (!Char.IsDigit(e.Text, 0))
			{
				e.Handled = true;
			}
		}
	}
}
