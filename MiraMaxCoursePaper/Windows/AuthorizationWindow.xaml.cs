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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using WorkingLibrary;

namespace MiraMaxCoursePaper.Windows
{
	/// <summary>
	/// Interaction logic for AuthorizationWindow.xaml
	/// </summary>
	public partial class AuthorizationWindow : Window
	{
		private const string _LoginText = "Логин";
		private const string _PasswordText = "Пароль";

		private Windows.RegistrationWindow registrationWindow;
		private Windows.ContentWindow contentWindow;
		private Windows.AdminWindow adminWindow;

		public AuthorizationWindow()
		{
			InitializeComponent();
		}

		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			var width = this.ActualWidth;
			var height = this.ActualHeight;

			if (width <= 900)
			{
				Advertisement.Visibility = Visibility.Collapsed;
				Grid.SetColumnSpan(ContentBox, 2);
			}
			else
			{
				Advertisement.Visibility = Visibility.Visible;
				Grid.SetColumnSpan(ContentBox, 1);
				Grid.SetColumn(ContentBox, 0);
			}
		}

		private void Login_LostFocus(object sender, RoutedEventArgs e)
		{
			Login.Text = Login.Text == String.Empty ? _LoginText : Login.Text;
		}

		private void Password_LostFocus(object sender, RoutedEventArgs e)
		{
			Password.Text = Password.Text == String.Empty ? _PasswordText : Password.Text;
		}

		private void Login_GotFocus(object sender, RoutedEventArgs e)
		{
			Login.Text = Login.Text == _LoginText ? "" : Login.Text;
		}

		private void Password_GotFocus(object sender, RoutedEventArgs e)
		{
			Password.Text = Password.Text == _PasswordText ? "" : Password.Text;
		}

		private void Registration_Click(object sender, RoutedEventArgs e)
		{
			if (registrationWindow == null)
			{
				registrationWindow = new RegistrationWindow();
				registrationWindow.Show();
				this.Close();
			}
		}

		private void GetPassword_Click(object sender, RoutedEventArgs e)
		{

		}

		public void FillUser(DataRow row)
		{
			CurrentUser.Id = Convert.ToInt32(row[0]);

			if (row[1] != System.DBNull.Value)
			{
				CurrentUser.SurName = Convert.ToString(row[1]);
			}
			
			CurrentUser.Name = Convert.ToString(row[2]);

			if (row[3] != System.DBNull.Value)
			{
				CurrentUser.Patronymic = Convert.ToString(row[3]);
			}
			
			CurrentUser.Login = Convert.ToString(row[4]);
			CurrentUser.Password = Convert.ToString(row[5]);

			if (row[6] != System.DBNull.Value)
			{
				CurrentUser.Phone = Convert.ToString(row[6]);
			}
		
			CurrentUser.Mail = Convert.ToString(row[7]);

			if (row[8] == System.DBNull.Value)
			{
				var bitmap = new Bitmap(MiraMaxCoursePaper.Properties.Resources.EmptyImageUser);
				CurrentUser.Image = DataWorking.ImageToByte(bitmap);
			}
			else
			{
				CurrentUser.Image = (byte[])row[8];
			}
			
			CurrentUser.Role = Convert.ToInt32(row[9]);
			CurrentUser.Ban = Convert.ToBoolean(row[10]);

			if (row[11] != System.DBNull.Value)
			{
				CurrentUser.Description = Convert.ToString(row[11]);
			}
			
		}

		private void Enter_Click(object sender, RoutedEventArgs e)
		{
			if (String.IsNullOrEmpty(Login.Text) || Login.Text == _LoginText)
			{
				MessageBox.Show("Введите значение в поле Логин");
				return;
			}

			if (String.IsNullOrEmpty(Password.Text) || Password.Text == _PasswordText)
			{
				MessageBox.Show("Введите значение в поле Пароль");
				return;
			}

			var db = new MiraMaxDataBase();

			var dataTable = new DataTable();

			var adapter = new MySqlDataAdapter();

			var sqlCommands = new MySqlCommand("SELECT * FROM `user`" +
				"WHERE `login` = @userLogin AND `password` = @userPassword",
				db.GetConnection());

			sqlCommands.Parameters.Add("@userLogin", MySqlDbType.VarChar).Value = Login.Text;
			sqlCommands.Parameters.Add("@userPassword", MySqlDbType.VarChar).Value = Password.Text;

			adapter.SelectCommand = sqlCommands;
			adapter.Fill(dataTable);

			if (dataTable.Rows.Count > 0)
			{
				if (Convert.ToBoolean(dataTable.Select()[0][10]) == true)
				{
					MessageBox.Show("Аккаунт заблокирован администратором");
					return;
				}

				MessageBox.Show("Авторизация прошла успешно");

				FillUser(dataTable.Select()[0]);

				DataWorking.InsertActionToAudit(DateTime.Now, DataWorking.EventType[0], CurrentUser.Id);

				if (contentWindow == null && CurrentUser.Role == 0)
				{
					adminWindow = new AdminWindow();
					adminWindow.Show();
					this.Close();
				}

				if (contentWindow == null && CurrentUser.Role == 1)
				{
					contentWindow = new ContentWindow();
					contentWindow.Show();
					this.Close();
				}
			}
			else
			{
				MessageBox.Show("Неправильный логин или пароль");
			}
		}
	}
}
