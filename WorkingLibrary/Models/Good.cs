using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WorkingLibrary.Models
{
	public class Good
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int ArticleNumber { get; set; }
		public int Price { get; set; }
		public int Quantity { get; set; }
		public int Sold { get; set; }
		public int Cost { get; set; }
		public byte[] Image { get; set; }
		public DateTime PurchaseDate { get; set; }
		public DateTime DeletionDate { get; set; }
		public DateTime ExpirationDate { get; set; }
		public string Description { get; set; }
		public int IdCountry { get; set; }
		public int IdGroup { get; set; }
		public int IdStatus { get; set; }
		public int IdUser { get; set; }

		public BitmapImage ImageFromByteArray
		{
			get
			{
				return DataWorking.GetBitmapImage(Image);
			}
		}

		public string ReadyArticle
		{
			get
			{
				return "Арт: " + ArticleNumber.ToString();
			}
		}

		public string ReadyPrice
		{
			get
			{
				return Price.ToString() + "Р";
			}
		}

		public string ReadyQuantity
		{
			get
			{
				return Quantity.ToString() + "Шт";
			}
		}

		public string ReadyExpirationDate
		{
			get
			{
				return "До: " + ExpirationDate.ToString("dd.MM.yyyy");
			}
		}

		public string ReadyStatus
		{
			get
			{
				return WorkingLibrary.DataWorking.SelectGoodStatus(IdStatus).Name;
			}
		}

		public SolidColorBrush ReadyStatusColor
		{
			get
			{
				var color = Colors.White;

				switch (IdStatus)
				{
					case 1:
						color = Colors.LightGreen;
						break;

					case 2:
						color = Colors.LightGray;
						break;

					case 3:
						color = Colors.DarkGray;
						break;

					case 4:
						color = Colors.Red;
						break;
				}

				return new SolidColorBrush(color);
			}
		}
	}
}