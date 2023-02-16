using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MySql.Data.MySqlClient;
using WorkingLibrary.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WorkingLibrary
{
	public class DataWorking
	{
		public static bool InsertActionToAudit(DateTime time, string someEvent, int idUser)
		{
			var dataBase = new MiraMaxDataBase();
			var mySqlCommand = new MySqlCommand("INSERT INTO `action_history` " +
				"(`time`, `event`, `id_user`) " +
				"VALUES (@time, @event, @id_user)",
				dataBase.GetConnection());

			mySqlCommand.Parameters.Add("@time", MySqlDbType.VarChar).Value = time.ToString("yyyy-MM-dd hh:mm:ss");
			mySqlCommand.Parameters.Add("@event", MySqlDbType.VarChar).Value = someEvent;
			mySqlCommand.Parameters.Add("@id_user", MySqlDbType.VarChar).Value = idUser;

			dataBase.OpenConnection();

			bool responce = false;

			if (mySqlCommand.ExecuteNonQuery() == 1)
			{
				responce = true;
			}

			dataBase.CloseConnection();

			return responce;
		}

		public static List<string> EventType = new List<string>()
		{
			"Авторизация в системе",
			"Регистрация пользователя",
			"Добавление новой товарной карточки",
			"Редактирование товарной карточки",
			"Удаление товарной карточки",
			"Добавление информации о продаже",
			"Удаление информации о продаже",
			"Редактирование профиля",
			"Изменение пароля",
			"Отправка информации о пароле на почту"
		};

		public static List<WorkingLibrary.Models.Good> GetGoodsList(DataRow[] dataTable)
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

		public static List<WorkingLibrary.Models.Good> SelectGoods(int userId)
		{
			var db = new MiraMaxDataBase();

			var dataTable = new DataTable();

			var adapter = new MySqlDataAdapter();

			var sqlCommands = new MySqlCommand("SELECT * FROM `good`" +
				"WHERE `id_user` = @userId",
				db.GetConnection());

			sqlCommands.Parameters.Add("@userId", MySqlDbType.VarChar).Value = userId;
			adapter.SelectCommand = sqlCommands;

			adapter.Fill(dataTable);

			return GetGoodsList(dataTable.Select());
		}

		public static bool UpdateGoodForSell(int idGood, int quantity)
		{
			var dataBase = new MiraMaxDataBase();
			var mySqlCommand = new MySqlCommand("UPDATE `good` " +
				"SET `quantity` = @quantity " +
				"WHERE `id` = @id",
				dataBase.GetConnection());

			mySqlCommand.Parameters.Add("@quantity", MySqlDbType.Int32).Value = quantity;
			mySqlCommand.Parameters.Add("@id", MySqlDbType.Int32).Value = idGood;

			dataBase.OpenConnection();

			bool responce = false;

			if (mySqlCommand.ExecuteNonQuery() == 1)
			{
				responce = true;
			}

			dataBase.CloseConnection();

			return responce;
		}

		public static bool UpdateGoodAfterDeletion(int idGood, int quantity)
		{
			var dataBase = new MiraMaxDataBase();
			var mySqlCommand = new MySqlCommand("UPDATE `good` " +
				"SET `quantity` = `quantity` + @quantity " +
				"WHERE `id` = @id",
				dataBase.GetConnection());

			mySqlCommand.Parameters.Add("@quantity", MySqlDbType.Int32).Value = quantity;
			mySqlCommand.Parameters.Add("@id", MySqlDbType.Int32).Value = idGood;

			dataBase.OpenConnection();

			bool responce = false;

			if (mySqlCommand.ExecuteNonQuery() == 1)
			{
				responce = true;
			}

			dataBase.CloseConnection();

			return responce;
		}

		public static bool UpdateGoodStatus(int idGood, int idStatus)
		{
			var dataBase = new MiraMaxDataBase();
			var mySqlCommand = new MySqlCommand("UPDATE `good` " +
				"SET `id_status` = @idStatus " +
				"WHERE `id` = @id",
				dataBase.GetConnection());

			mySqlCommand.Parameters.Add("@idStatus", MySqlDbType.Int32).Value = idStatus;
			mySqlCommand.Parameters.Add("@id", MySqlDbType.Int32).Value = idGood;

			dataBase.OpenConnection();

			bool responce = false;

			if (mySqlCommand.ExecuteNonQuery() == 1)
			{
				responce = true;
			}

			dataBase.CloseConnection();

			return responce;
		}

		public static bool InsertGoodHistory(int idGood, int quantity, int price, string description)
		{
			var dataBase = new MiraMaxDataBase();
			var mySqlCommand = new MySqlCommand("INSERT INTO `history_of_good` " +
				"(`sale_price`, `sale_quantity`, `sale_date`, `description`, `id_good`) " +
				"VALUES (@sale_price, @sale_quantity, @sale_date, @description, @id_good)",
				dataBase.GetConnection());

			mySqlCommand.Parameters.Add("@sale_price", MySqlDbType.Int32).Value = price;
			mySqlCommand.Parameters.Add("@sale_quantity", MySqlDbType.Int32).Value = quantity;

			mySqlCommand.Parameters.Add("@sale_date", MySqlDbType.Date).Value = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

			mySqlCommand.Parameters.Add("@description", MySqlDbType.VarChar).Value = description;
			mySqlCommand.Parameters.Add("@id_good", MySqlDbType.Int32).Value = idGood;

			dataBase.OpenConnection();

			bool responce = false;

			if (mySqlCommand.ExecuteNonQuery() == 1)
			{
				responce = true;
			}

			dataBase.CloseConnection();

			return responce;
		}

		public static bool DeleteGood(int id)
		{
			var dataBase = new MiraMaxDataBase();
			var mySqlCommand = new MySqlCommand("DELETE FROM `good` WHERE `id` = @id", dataBase.GetConnection());

			mySqlCommand.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

			dataBase.OpenConnection();

			bool responce = false;

			if (mySqlCommand.ExecuteNonQuery() == 1)
			{
				responce = true;
			}

			dataBase.CloseConnection();

			return responce;
		}

		public static bool DeleteGoodHistory(int id)
		{
			var dataBase = new MiraMaxDataBase();
			var mySqlCommand = new MySqlCommand("DELETE FROM `history_of_good` WHERE `id` = @id", dataBase.GetConnection());

			mySqlCommand.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

			dataBase.OpenConnection();

			bool responce = false;

			if (mySqlCommand.ExecuteNonQuery() == 1)
			{
				responce = true;
			}

			dataBase.CloseConnection();

			return responce;
		}

		public static List<User> SelectUsers()
		{
			var db = new MiraMaxDataBase();

			var dataTable = new DataTable();

			var adapter = new MySqlDataAdapter();

			var sqlCommands = new MySqlCommand("SELECT * FROM `user`", db.GetConnection());

			adapter.SelectCommand = sqlCommands;
			adapter.Fill(dataTable);

			var list = FillUsersList(dataTable.Select());

			return list;
		}

		private static List<User> FillUsersList(DataRow[] dataTable)
		{
			var users = new List<User>();

			foreach (var item in dataTable)
			{
				var user = new User();

				user.Id = Convert.ToInt32(item[0]);

				if (item[1] != System.DBNull.Value)
				{
					user.SurName = Convert.ToString(item[1]);
				}

				user.Name = Convert.ToString(item[2]);

				if (item[3] != System.DBNull.Value)
				{
					user.Patronymic = Convert.ToString(item[3]);
				}

				user.Login = Convert.ToString(item[4]);
				user.Password = Convert.ToString(item[5]);

				if (item[6] != System.DBNull.Value)
				{
					user.Phone = Convert.ToString(item[6]);
				}

				user.Mail = Convert.ToString(item[7]);

				if (item[8] != System.DBNull.Value)
				{
					user.Image = (byte[])item[8];
				}

				user.Role = Convert.ToInt32(item[9]);
				user.Ban = Convert.ToBoolean(item[10]);

				if (item[11] != System.DBNull.Value)
				{
					user.Description = Convert.ToString(item[11]);
				}

				users.Add(user);
			}

			return users;
		}

		public static List<GoodGroup> SelectAllGoodGroup()
		{
			var db = new MiraMaxDataBase();

			var dataTable = new DataTable();

			var adapter = new MySqlDataAdapter();

			var sqlCommands = new MySqlCommand("SELECT * FROM `good_group`", db.GetConnection());

			adapter.SelectCommand = sqlCommands;
			adapter.Fill(dataTable);

			var list = FillGoodGroupList(dataTable.Select());

			return list;
		}

		private static List<GoodGroup> FillGoodGroupList(DataRow[] dataTable)
		{
			var goodGroups = new List<GoodGroup>();

			foreach (var item in dataTable)
			{
				var goodGroup = new GoodGroup();

				goodGroup.Id = Convert.ToInt32(item[0]);
				goodGroup.Name = Convert.ToString(item[1]);

				if (item[2] != System.DBNull.Value)
				{
					goodGroup.Description = Convert.ToString(item[2]);
				}

				goodGroups.Add(goodGroup);
			}

			return goodGroups;
		}

		public static List<Country> SelectAllCountry()
		{
			var db = new MiraMaxDataBase();

			var dataTable = new DataTable();

			var adapter = new MySqlDataAdapter();

			var sqlCommands = new MySqlCommand("SELECT * FROM `country`", db.GetConnection());

			adapter.SelectCommand = sqlCommands;
			adapter.Fill(dataTable);

			var list = FillCountryList(dataTable.Select());

			return list;
		}

		private static List<Country> FillCountryList(DataRow[] dataTable)
		{
			var countryList = new List<Country>();

			foreach (var item in dataTable)
			{
				var country = new Country();

				country.Id = Convert.ToInt32(item[0]);
				country.Name = Convert.ToString(item[1]);
				country.ShortName = Convert.ToString(item[2]);

				if (item[3] != System.DBNull.Value)
				{
					country.Image = Convert.ToString(item[3]);
				}

				countryList.Add(country);
			}

			return countryList;
		}

		public static List<GoodStatus> SelectAllGoodStatus()
		{
			var db = new MiraMaxDataBase();

			var dataTable = new DataTable();

			var adapter = new MySqlDataAdapter();

			var sqlCommands = new MySqlCommand("SELECT * FROM `status`", db.GetConnection());

			adapter.SelectCommand = sqlCommands;
			adapter.Fill(dataTable);

			var list = FillGoodStatusList(dataTable.Select());

			return list;
		}

		public static List<GoodStatus> SelectAllGoodStatusWhereLastEmpty()
		{
			var db = new MiraMaxDataBase();

			var dataTable = new DataTable();

			var adapter = new MySqlDataAdapter();

			var sqlCommands = new MySqlCommand("SELECT * FROM `status` WHERE NOT id = @id", db.GetConnection());
			sqlCommands.Parameters.Add("@id", MySqlDbType.Int32).Value = 5;

			adapter.SelectCommand = sqlCommands;
			adapter.Fill(dataTable);

			var list = FillGoodStatusList(dataTable.Select());

			return list;
		}

		public static GoodStatus SelectGoodStatus(int id)
		{
			var db = new MiraMaxDataBase();

			var dataTable = new DataTable();

			var adapter = new MySqlDataAdapter();

			var sqlCommands = new MySqlCommand("SELECT * FROM `status` WHERE id = @id", db.GetConnection());
			sqlCommands.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

			adapter.SelectCommand = sqlCommands;
			adapter.Fill(dataTable);

			var item = dataTable.Select()[0];

			var goodStatus = new GoodStatus();

			goodStatus.Id = Convert.ToInt32(item[0]);
			goodStatus.Name = Convert.ToString(item[1]);

			if (item[2] != System.DBNull.Value)
			{
				goodStatus.Description = Convert.ToString(item[2]);
			}

			return goodStatus;
		}

		public static List<HistoryOfGood> SelectHistoryOfGood(int id)
		{
			var db = new MiraMaxDataBase();

			var dataTable = new DataTable();

			var adapter = new MySqlDataAdapter();

			var sqlCommands = new MySqlCommand("SELECT hog.id, hog.sale_price, hog.sale_quantity, hog.sale_date, hog.description, hog.id_good " +
				"FROM `history_of_good` hog INNER JOIN `good` g on hog.id_good = g.id " +
				"WHERE g.id_user = @id_user", db.GetConnection());

			sqlCommands.Parameters.Add("@id_user", MySqlDbType.Int32).Value = id;

			adapter.SelectCommand = sqlCommands;
			adapter.Fill(dataTable);

			var historyOfGood = FillHistoryOfGood(dataTable.Select());

			return historyOfGood;
		}

		private static List<HistoryOfGood> FillHistoryOfGood(DataRow[] dataTable)
		{
			var historyOfGoodList = new List<HistoryOfGood>();

			foreach (var item in dataTable)
			{
				var goodHistory = new HistoryOfGood();

				goodHistory.Id = Convert.ToInt32(item[0]);
				goodHistory.SalePrice = Convert.ToInt32(item[1]);
				goodHistory.SaleQuantity = Convert.ToInt32(item[2]);
				goodHistory.SaleDate = Convert.ToDateTime(item[3]);

				if (item[4] != System.DBNull.Value)
				{
					goodHistory.Description = Convert.ToString(item[4]);
				}

				goodHistory.IdGood = Convert.ToInt32(item[5]);

				historyOfGoodList.Add(goodHistory);
			}

			return historyOfGoodList;
		}

		public static List<ActionHistory> SelectActionById(int id)
		{
			var db = new MiraMaxDataBase();

			var dataTable = new DataTable();

			var adapter = new MySqlDataAdapter();

			var sqlCommands = new MySqlCommand("SELECT * FROM `action_history` WHERE id_user = @id_user", db.GetConnection());
			sqlCommands.Parameters.Add("@id_user", MySqlDbType.Int32).Value = id;

			adapter.SelectCommand = sqlCommands;
			adapter.Fill(dataTable);

			var actionsHistory = FillActions(dataTable.Select());

			return actionsHistory;
		}

		private static List<ActionHistory> FillActions(DataRow[] dataTable)
		{
			var actionsList = new List<ActionHistory>();

			foreach (var item in dataTable)
			{
				var action = new ActionHistory();

				action.Id = Convert.ToInt32(item[0]);
				action.Time = Convert.ToDateTime(item[1]);
				action.EventName = Convert.ToString(item[2]);
				action.IdUser = Convert.ToInt32(item[3]);

				actionsList.Add(action);
			}

			return actionsList;
		}

		private static List<GoodStatus> FillGoodStatusList(DataRow[] dataTable)
		{
			var goodStatusList = new List<GoodStatus>();

			foreach (var item in dataTable)
			{
				var goodStatus = new GoodStatus();

				goodStatus.Id = Convert.ToInt32(item[0]);
				goodStatus.Name = Convert.ToString(item[1]);

				if (item[2] != System.DBNull.Value)
				{
					goodStatus.Description = Convert.ToString(item[2]);
				}
				
				goodStatusList.Add(goodStatus);
			}

			return goodStatusList;
		}

		public static bool CheckUser(string userLogin)
		{
			var db = new MiraMaxDataBase();

			var dataTable = new DataTable();

			var adapter = new MySqlDataAdapter();

			var sqlCommands = new MySqlCommand("SELECT * FROM `user` WHERE `login` = @userLogin", db.GetConnection());
			sqlCommands.Parameters.Add("@userLogin", MySqlDbType.VarChar).Value = userLogin;

			adapter.SelectCommand = sqlCommands;
			adapter.Fill(dataTable);

			if (dataTable.Rows.Count > 0)
			{
				return true;
			}

			return false;
		}

		public static bool CheckGoodArticle(string article, int userId)
		{
			var db = new MiraMaxDataBase();

			var dataTable = new DataTable();

			var adapter = new MySqlDataAdapter();

			var sqlCommands = new MySqlCommand("SELECT * FROM `good` WHERE `article_number` = @article AND `id_user` = @userId", db.GetConnection());
			sqlCommands.Parameters.Add("@article", MySqlDbType.Int32).Value = Convert.ToInt32(article);
			sqlCommands.Parameters.Add("@userId", MySqlDbType.Int32).Value = userId;

			adapter.SelectCommand = sqlCommands;
			adapter.Fill(dataTable);

			if (dataTable.Rows.Count > 0)
			{
				return true;
			}

			return false;
		}

		public static int SelectGoodArticleById(int goodId)
		{
			var db = new MiraMaxDataBase();

			var dataTable = new DataTable();

			var adapter = new MySqlDataAdapter();

			var sqlCommands = new MySqlCommand("SELECT good.article_number FROM `good` WHERE `id` = @goodId", db.GetConnection());

			sqlCommands.Parameters.Add("@goodId", MySqlDbType.Int32).Value = goodId;

			adapter.SelectCommand = sqlCommands;
			adapter.Fill(dataTable);

			if (dataTable.Rows.Count > 0)
			{
				return Convert.ToInt32(dataTable.Select()[0][0]);
			}

			return 0;
		}

		public static int SelectGoodQuantity(int goodId)
		{
			var db = new MiraMaxDataBase();

			var dataTable = new DataTable();

			var adapter = new MySqlDataAdapter();

			var sqlCommands = new MySqlCommand("SELECT good.quantity FROM `good` WHERE `id` = @goodId", db.GetConnection());

			sqlCommands.Parameters.Add("@goodId", MySqlDbType.Int32).Value = goodId;

			adapter.SelectCommand = sqlCommands;
			adapter.Fill(dataTable);

			if (dataTable.Rows.Count > 0)
			{
				return Convert.ToInt32(dataTable.Select()[0][0]);
			}

			return 0;
		}

		/// <summary>
		/// Конвертирует массив байтов в bitmap правильно.
		/// </summary>
		public static BitmapImage GetBitmapImage(byte[] array)
		{
			BitmapImage bi;

			using (var ms = new MemoryStream(array))
			{
				bi = new BitmapImage();
				bi.BeginInit();
				bi.CreateOptions = BitmapCreateOptions.None;
				bi.CacheOption = BitmapCacheOption.OnLoad;
				bi.StreamSource = ms;

				bi.EndInit();
			}

			return bi;
		}

		public static ImageBrush CreateImageFromByte(byte[] array)
		{
			ImageBrush brush;
			BitmapImage bi;

			using (var ms = new MemoryStream(array))
			{
				brush = new ImageBrush();

				bi = new BitmapImage();
				bi.BeginInit();
				bi.CreateOptions = BitmapCreateOptions.None;
				bi.CacheOption = BitmapCacheOption.OnLoad;
				bi.StreamSource = ms;

				bi.EndInit();
			}

			brush.ImageSource = bi;

			return brush;
		}

		public static byte[] ImageToByte(System.Drawing.Image img)
		{
			ImageConverter converter = new ImageConverter();

			return (byte[])converter.ConvertTo(img, typeof(byte[]));
		}
	}
}
