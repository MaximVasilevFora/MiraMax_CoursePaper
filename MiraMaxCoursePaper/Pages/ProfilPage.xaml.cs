using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkingLibrary;

namespace MiraMaxCoursePaper.Pages
{
	public partial class ProfilPage : Page
	{
		private const string _LoginText = "Логин";
		private const string _Mail = "Почта";
		private const string _Name = "Имя";
		private const string _Surname = "Фамилия";
		private const string _Patronymic = "Отчество";
		private const string _Phone = "Телефон";
		private const string _Description = "Описание";

		private System.Windows.Forms.OpenFileDialog _openFileDialog = new System.Windows.Forms.OpenFileDialog();
		private byte[] _profilImage;
		public static Windows.ContentWindow contentWindow;

		public ProfilPage()
		{
			InitializeComponent();
			_openFileDialog.Filter = "Png files(*.png)|*.png|Jpg files(*.jpg)|*.jpg|Jpeg files(*.jpeg)|*.jpeg";

			InitPageTextBox();
		}

		private void InitPageTextBox()
		{
			Login.Text = CurrentUser.Login;
			Mail.Text = CurrentUser.Mail;
			Name.Text = CurrentUser.Name;

			if (CurrentUser.SurName == null)
			{
				Surname.Text = _Surname;
			}
			else
			{
				Surname.Text = CurrentUser.SurName;
			}

			if (CurrentUser.Patronymic == null)
			{
				Patronymic.Text = _Patronymic;
			}
			else
			{
				Patronymic.Text = CurrentUser.Patronymic;
			}

			if (CurrentUser.Phone == null)
			{
				Phone.Text = _Phone;
			}
			else
			{
				Phone.Text = CurrentUser.Phone;
			}

			if (String.IsNullOrEmpty(CurrentUser.Description))
			{
				Description.Text = _Description;
			}
			else
			{
				Description.Text = CurrentUser.Description;
			}

			ProfilImage.Source = DataWorking.GetBitmapImage(CurrentUser.Image);
			_profilImage = CurrentUser.Image;
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

			if (DataWorking.CheckUser(Login.Text) && Login.Text != CurrentUser.Login)
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
				"`phone` = @phone, " +
				"`mail` = @mail, " +
				"`image` = @image, " +
				"`description` = @description " +
				"WHERE `id` = @id",
				dataBase.GetConnection());

			mySqlCommand.Parameters.Add("@id", MySqlDbType.Int32).Value = CurrentUser.Id;
			mySqlCommand.Parameters.Add("@surname", MySqlDbType.VarChar).Value = (Surname.Text == _Surname ? null : Surname.Text);
			mySqlCommand.Parameters.Add("@name", MySqlDbType.VarChar).Value = Name.Text;
			mySqlCommand.Parameters.Add("@patronymic", MySqlDbType.VarChar).Value = (Patronymic.Text == _Patronymic ? null : Patronymic.Text);
			mySqlCommand.Parameters.Add("@login", MySqlDbType.VarChar).Value = Login.Text;
			mySqlCommand.Parameters.Add("@phone", MySqlDbType.VarChar).Value = (Phone.Text == _Phone ? null : Phone.Text);
			mySqlCommand.Parameters.Add("@mail", MySqlDbType.VarChar).Value = Mail.Text;

			mySqlCommand.Parameters.Add("@image", MySqlDbType.MediumBlob).Value = _profilImage;

			mySqlCommand.Parameters.Add("@description", MySqlDbType.VarChar).Value = Description.Text == _Description ? null : Description.Text;

			dataBase.OpenConnection();

			if (mySqlCommand.ExecuteNonQuery() == 1)
			{
				System.Windows.MessageBox.Show("Данные были изменены");

				DataWorking.InsertActionToAudit(DateTime.Now, DataWorking.EventType[7], CurrentUser.Id);
			}
			else
			{
				System.Windows.MessageBox.Show("Ошибка изменения данных");
				return;
			}

			dataBase.CloseConnection();

			InitCurrentUser();
		}

		private void FillImageContent()
		{
			contentWindow.InitWindow();
		}

		private void InitCurrentUser()
		{
			CurrentUser.Image = _profilImage;
			CurrentUser.Login = Login.Text;
			CurrentUser.Mail = Mail.Text;
			CurrentUser.Name = Name.Text;

			if (String.IsNullOrEmpty(Surname.Text) || Surname.Text == _Surname)
			{
				CurrentUser.SurName = null;
			}
			else
			{
				CurrentUser.SurName = Surname.Text;
			}

			if (String.IsNullOrEmpty(Patronymic.Text) || Patronymic.Text == _Patronymic)
			{
				CurrentUser.Patronymic = null;
			}
			else
			{
				CurrentUser.Patronymic = Patronymic.Text;
			}

			if (String.IsNullOrEmpty(Phone.Text) || Phone.Text == _Phone)
			{
				CurrentUser.Phone = null;
			}
			else
			{
				CurrentUser.Phone = Phone.Text;
			}

			if (String.IsNullOrEmpty(Description.Text) || Description.Text == _Description)
			{
				CurrentUser.Description = null;
			}
			else
			{
				CurrentUser.Description = Description.Text;
			}

			FillImageContent();
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

		private bool CheckChangesOfTextBox()
		{
			if (Login.Text != CurrentUser.Login)
			{
				return true;
			}

			if (Mail.Text != CurrentUser.Mail)
			{
				return true;
			}

			if (Name.Text != CurrentUser.Name)
			{
				return true;
			}

			if (Surname.Text != CurrentUser.SurName)
			{
				return true;
			}

			if (Patronymic.Text != CurrentUser.Patronymic)
			{
				return true;
			}

			if (Phone.Text != CurrentUser.Phone)
			{
				return true;
			}

			if (Description.Text != CurrentUser.Description)
			{
				return true;
			}

			if (CurrentUser.Image != _profilImage)
			{
				return true;
			}

			return false;
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

			InitPageTextBox();
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

		private void ChangePassword_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ChangeProfilImage()
		{
			if (_openFileDialog.ShowDialog() == DialogResult.OK)
			{
				var bitmap = new BitmapImage(new Uri(_openFileDialog.FileName));
				ProfilImage.Source = bitmap;

				_profilImage = BitmapImageInArray(bitmap);
			}
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
	}
}
