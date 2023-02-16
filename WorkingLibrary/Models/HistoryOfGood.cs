using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingLibrary.Models
{
	public class HistoryOfGood
	{
		public int Id { get; set; }
		public int SalePrice { get; set; }
		public int SaleQuantity { get; set; }
		public DateTime SaleDate { get; set; }
		public string Description { get; set; }
		public int IdGood { get; set; }

		public string GetDate
		{
			get
			{
				return SaleDate.ToString("dd.MM.yyyy");
			}
		}

		public int GetArticleById
		{
			get
			{
				return DataWorking.SelectGoodArticleById(IdGood);
			}
		}

		public string GetSaleQuantity
		{
			get
			{
				return SaleQuantity + "Шт.";
			}
		}

		public string GetSalePrice
		{
			get
			{
				return SalePrice + "Руб.";
			}
		}
	}
}
