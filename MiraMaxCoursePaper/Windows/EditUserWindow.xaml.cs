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
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MiraMaxCoursePaper.Windows
{
	/// <summary>
	/// Interaction logic for EditUserWindow.xaml
	/// </summary>
	public partial class EditUserWindow : Window
	{
		private const string _LoginText = "Логин";
		private const string _Mail = "Почта";
		private const string _Name = "Имя";
		private const string _Surname = "Фамилия";
		private const string _Patronymic = "Отчество";
		private const string _Phone = "Телефон";
		private const string _Description = "Описание";
		private const string _Password = "Пароль";
		private const string _Role = "Роль";
		private const string _Ban = "Бан";

		private System.Windows.Forms.OpenFileDialog _openFileDialog = new System.Windows.Forms.OpenFileDialog();
		private byte[] _profilImage;
		private WorkingLibrary.Models.User _user;

		public Windows.AdminWindow adminWindow;

		public EditUserWindow(object sender)
		{
			InitializeComponent();

			_openFileDialog.Filter = "Png files(*.png)|*.png|Jpg files(*.jpg)|*.jpg|Jpeg files(*.jpeg)|*.jpeg";

			_user = (sender as System.Windows.Controls.Button).DataContext as WorkingLibrary.Models.User;

			InitPageTextBox(_user);
		}

		private void InitPageTextBox(WorkingLibrary.Models.User user)
		{
			Login.Text = user.Login;
			Mail.Text = user.Mail;
			Name.Text = user.Name;
			Password.Text = user.Password.ToString();
			Role.Text = user.Role.ToString();
			Ban.IsChecked = user.Ban;

			if (user.SurName == null)
			{
				Surname.Text = _Surname;
			}
			else
			{
				Surname.Text = user.SurName;
			}

			if (user.Patronymic == null)
			{
				Patronymic.Text = _Patronymic;
			}
			else
			{
				Patronymic.Text = user.Patronymic;
			}

			if (user.Phone == null)
			{
				Phone.Text = _Phone;
			}
			else
			{
				Phone.Text = user.Phone;
			}

			if (String.IsNullOrEmpty(user.Description))
			{
				Description.Text = _Description;
			}
			else
			{
				Description.Text = user.Description;
			}

			if (user.Image == null)
			{
				var bitmap = new Bitmap(MiraMaxCoursePaper.Properties.Resources.EmptyImageUser);
				ProfilImage.Source = ConvertBitmapToBitmapSource(bitmap);

				_profilImage = DataWorking.ImageToByte(bitmap);
			}
			else
			{
				ProfilImage.Source = DataWorking.GetBitmapImage(user.Image);

				_profilImage = user.Image;
			}
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			var result = System.Windows.MessageBox.Show("Вы уверены, что хотите отменить изменения?",
				"Confirmation",
				MessageBoxButton.YesNo,
				MessageBoxImage.Question);

			if (result == MessageBoxResult.No)
			{
				return;
			}

			InitPageTextBox(_user);
		}

		private void Save_Click(object sender, RoutedEventArgs e)
		{
			if (!CheckChangesOfTextBox())
			{
				System.Windows.MessageBox.Show("Данные в полях не были изменены. Впишите в доступные поля новые значения");
				return;
			}

			if (String.IsNullOrEmpty(Login.Text) || Login.Text == _LoginText)
			{
				System.Windows.MessageBox.Show("Введите значение в поле Логин");
				return;
			}

			if (String.IsNullOrEmpty(Password.Text) || Password.Text == _Password)
			{
				System.Windows.MessageBox.Show("Введите значение в поле Пароль");
				return;
			}

			if (Password.Text.Count() < 5)
			{
				System.Windows.MessageBox.Show("Пароль должен содержать более 4 символов");
				return;
			}

			if (String.IsNullOrEmpty(Role.Text) || Role.Text == _Role)
			{
				System.Windows.MessageBox.Show("Введите значение в поле Роль пользователя в системе");
				return;
			}

			if (String.IsNullOrEmpty(Mail.Text) || Mail.Text == _Mail)
			{
				System.Windows.MessageBox.Show("Ввведите значение в поле Почта");
				return;
			}

			if (!MailWorking.CheckMail(Mail.Text))
			{
				System.Windows.MessageBox.Show("Введите корректный почтовый адрес");
				return;
			}

			if (String.IsNullOrEmpty(Name.Text) || Name.Text == _Name)
			{
				System.Windows.MessageBox.Show("Ввведите значение в поле Имя");
				return;
			}

			if (DataWorking.CheckUser(Login.Text) && Login.Text != _user.Login)
			{
				System.Windows.MessageBox.Show("Пользователь с таким логином уже существует");
				return;
			}

			var dataBase = new MiraMaxDataBase();
			var mySqlCommand = new MySqlCommand("UPDATE `user` " +
				"SET `surname` = @surname, " +
				"`name` = @name, " +
				"`patronymic` = @patronymic, " +
				"`login` = @login, " +
				"`password` = @password, " +	
				"`phone` = @phone, " +
				"`mail` = @mail, " +
				"`image` = @image, " +
				"`role` = @role, " +
				"`ban` = @ban, " +
				"`description` = @description " +
				"WHERE `id` = @id",
				dataBase.GetConnection());

			mySqlCommand.Parameters.Add("@id", MySqlDbType.Int32).Value = _user.Id;
			mySqlCommand.Parameters.Add("@surname", MySqlDbType.VarChar).Value = (Surname.Text == _Surname ? null : Surname.Text);
			mySqlCommand.Parameters.Add("@name", MySqlDbType.VarChar).Value = Name.Text;
			mySqlCommand.Parameters.Add("@patronymic", MySqlDbType.VarChar).Value = (Patronymic.Text == _Patronymic ? null : Patronymic.Text);
			mySqlCommand.Parameters.Add("@login", MySqlDbType.VarChar).Value = Login.Text;
			mySqlCommand.Parameters.Add("@password", MySqlDbType.VarChar).Value = Password.Text;
			mySqlCommand.Parameters.Add("@phone", MySqlDbType.VarChar).Value = (Phone.Text == _Phone ? null : Phone.Text);
			mySqlCommand.Parameters.Add("@mail", MySqlDbType.VarChar).Value = Mail.Text;

			mySqlCommand.Parameters.Add("@image", MySqlDbType.MediumBlob).Value = _profilImage;

			mySqlCommand.Parameters.Add("@role", MySqlDbType.Int32).Value = Convert.ToInt32(Role.Text);
			mySqlCommand.Parameters.Add("@ban", MySqlDbType.Int32).Value = Ban.IsChecked;

			mySqlCommand.Parameters.Add("@description", MySqlDbType.VarChar).Value = Description.Text == _Description ? null : Description.Text;

			dataBase.OpenConnection();

			if (mySqlCommand.ExecuteNonQuery() == 1)
			{
				System.Windows.MessageBox.Show("Данные были изменены");
			}
			else
			{
				System.Windows.MessageBox.Show("Ошибка изменения данных");
				return;
			}

			dataBase.CloseConnection();

			this.Close();
		}

		private bool CheckChangesOfTextBox()
		{
			if (Login.Text != _user.Login)
			{
				return true;
			}

			if (Password.Text != _user.Password)
			{
				return true;
			}

			if (Convert.ToInt32(Role.Text) != _user.Role)
			{
				return true;
			}

			if (Ban.IsChecked != _user.Ban)
			{
				return true;
			}

			if (Mail.Text != _user.Mail)
			{
				return true;
			}

			if (Name.Text != _user.Name)
			{
				return true;
			}

			if (Surname.Text != _user.SurName)
			{
				return true;
			}

			if (Patronymic.Text != _user.Patronymic)
			{
				return true;
			}

			if (Phone.Text != _user.Phone)
			{
				return true;
			}

			if (Description.Text != _user.Description)
			{
				return true;
			}

			if (_user.Image != _profilImage)
			{
				return true;
			}

			return false;
		}

		private BitmapSource ConvertBitmapToBitmapSource(System.Drawing.Bitmap bitmap)
		{
			var bitmapData = bitmap.LockBits(
				new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
				System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

			var bitmapSource = BitmapSource.Create(
				bitmapData.Width, bitmapData.Height,
				bitmap.HorizontalResolution, bitmap.VerticalResolution,
				PixelFormats.Bgr24, null,
				bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

			bitmap.UnlockBits(bitmapData);

			return bitmapSource;
		}
		private byte[] BitmapImageInArray(BitmapImage imageSource)
		{
			byte[] data;
			JpegBitmapEncoder encoder = new JpegBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(imageSource));

			using (MemoryStream ms = new MemoryStream())
			{
				encoder.Save(ms);
				data = ms.ToArray();
			}

			return data;
		}

		private void ChangeProfilImage()
		{
			if (_openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				var bitmap = new BitmapImage(new Uri(_openFileDialog.FileName));
				ProfilImage.Source = bitmap;

				_profilImage = BitmapImageInArray(bitmap);
			}
		}

		private void Login_GotFocus(object sender, RoutedEventArgs e)
		{
			Login.Text = Login.Text == _LoginText ? "" : Login.Text;
		}

		private void Login_LostFocus(object sender, RoutedEventArgs e)
		{
			Login.Text = Login.Text == String.Empty ? _LoginText : Login.Text;
		}

		private void Mail_GotFocus(object sender, RoutedEventArgs e)
		{
			Mail.Text = Mail.Text == _Mail ? "" : Mail.Text;
		}

		private void Mail_LostFocus(object sender, RoutedEventArgs e)
		{
			Mail.Text = Mail.Text == String.Empty ? _Mail : Mail.Text;
		}

		private void Name_GotFocus(object sender, RoutedEventArgs e)
		{
			Name.Text = Name.Text == _Name ? "" : Name.Text;
		}

		private void Name_LostFocus(object sender, RoutedEventArgs e)
		{
			Name.Text = Name.Text == String.Empty ? _Name : Name.Text;
		}

		private void Surname_GotFocus(object sender, RoutedEventArgs e)
		{
			Surname.Text = Surname.Text == _Surname ? "" : Surname.Text;
		}

		private void Surname_LostFocus(object sender, RoutedEventArgs e)
		{
			Surname.Text = Surname.Text == String.Empty ? _Surname : Surname.Text;
		}

		private void Patronymic_GotFocus(object sender, RoutedEventArgs e)
		{
			Patronymic.Text = Patronymic.Text == _Patronymic ? "" : Patronymic.Text;
		}

		private void Patronymic_LostFocus(object sender, RoutedEventArgs e)
		{
			Patronymic.Text = Patronymic.Text == String.Empty ? _Patronymic : Patronymic.Text;
		}

		private void Phone_GotFocus(object sender, RoutedEventArgs e)
		{
			Phone.Text = Phone.Text == _Phone ? "" : Phone.Text;
		}

		private void Phone_LostFocus(object sender, RoutedEventArgs e)
		{
			Phone.Text = Phone.Text == String.Empty ? _Phone : Phone.Text;
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			adminWindow.editUserWindow = null;

			adminWindow.InitPage();
		}

		private void Password_GotFocus(object sender, RoutedEventArgs e)
		{
			Password.Text = Password.Text == _Password ? "" : Password.Text;
		}

		private void Password_LostFocus(object sender, RoutedEventArgs e)
		{
			Password.Text = Password.Text == String.Empty ? _Password : Password.Text;
		}

		private void Role_GotFocus(object sender, RoutedEventArgs e)
		{
			Role.Text = Role.Text == _Role ? "" : Role.Text;
		}

		private void Role_LostFocus(object sender, RoutedEventArgs e)
		{
			Role.Text = Role.Text == String.Empty ? _Role : Role.Text;
		}

		private void ChangeImage_Click(object sender, RoutedEventArgs e)
		{
			ChangeProfilImage();
		}

		private void Description_GotFocus(object sender, RoutedEventArgs e)
		{
			Description.Text = Description.Text == _Description ? "" : Description.Text;
		}

		private void Description_LostFocus(object sender, RoutedEventArgs e)
		{
			Description.Text = Description.Text == String.Empty ? _Description : Description.Text;
		}

		private void Phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (!Char.IsDigit(e.Text, 0))
			{
				e.Handled = true;
			}
		}

		private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			var width = this.ActualWidth;
			var height = this.ActualHeight;

			if (width <= 900)
			{
				ProfilImage.Visibility = Visibility.Collapsed;
				Grid.SetColumnSpan(ContentBox, 2);
			}
			else
			{
				ProfilImage.Visibility = Visibility.Visible;
				Grid.SetColumnSpan(ContentBox, 1);
				Grid.SetColumn(ContentBox, 0);
			}
		}

		private void ProfilImage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			ChangeProfilImage();
		}

		private void Role_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (!Char.IsDigit(e.Text, 0))
			{
				e.Handled = true;
			}
		}
	}
}
