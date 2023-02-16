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
using Word = Microsoft.Office.Interop.Word;

namespace MiraMaxCoursePaper.Pages
{
	public partial class MoneyPage : Page
	{
		private const string _Template = "0.00";

		private int _revenue = 0;
		private int _costPrice = 0;
		private int _grossProfit = 0;
		private int _amountCirculation = 0;//сумма в обороте

		private List<WorkingLibrary.Models.HistoryOfGood> _historyOfGoods;
		private List<WorkingLibrary.Models.Good> _goods;

		public MoneyPage()
		{
			InitializeComponent();

			InitPage();
		}

		public void InitPage()
		{
			_goods = DataWorking.SelectGoods(CurrentUser.Id);
			_historyOfGoods = DataWorking.SelectHistoryOfGood(CurrentUser.Id).ToList();

			foreach (var item in _historyOfGoods)
			{
				_revenue += item.SalePrice;
			}

			Revenue.Text = Convert.ToString(_revenue) + ".00";

			foreach (var item in _goods)
			{
				var sum = _historyOfGoods.Where(x => x.IdGood == item.Id).ToList();

				_costPrice += (item.Price * item.Quantity);

				foreach (var goodHistory in sum)
				{
					_costPrice += (item.Price * goodHistory.SaleQuantity);
				}
			}

			CostPrice.Text = _costPrice.ToString();

			_grossProfit = _revenue - _costPrice;
			GrossProfit.Text = Convert.ToString(_grossProfit) + ".00";

			foreach (var item in _goods)
			{
				_amountCirculation += item.Price * item.Quantity;
			}

			AmountCirculation.Text = Convert.ToString(_amountCirculation) + ".00";
		}

		private void Export_Click(object sender, RoutedEventArgs e)
		{
			var app = new Word.Application();
			Word.Document document = app.Documents.Add();

			Word.Paragraph tableParagraph = document.Paragraphs.Add();
			Word.Range tableRange = tableParagraph.Range;

			Word.Table clubTable = document.Tables.Add(tableRange, 5, 2);

			clubTable.Borders.InsideLineStyle =
				clubTable.Borders.InsideLineStyle =
				Word.WdLineStyle.wdLineStyleSingle;

			clubTable.Borders.OutsideLineStyle =
				clubTable.Borders.OutsideLineStyle =
				Word.WdLineStyle.wdLineStyleSingle;

			clubTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

			Word.Range cellRange = clubTable.Cell(1, 1).Range;
			cellRange.Text = "Статья";
			cellRange.Font.Bold = 2;

			cellRange = clubTable.Cell(1, 2).Range;
			cellRange.Text = "Прибыль/Убыток";
			cellRange.Bold = 2;

			clubTable.Rows[1].Range.Bold = 1;
			clubTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

			cellRange = clubTable.Cell(2, 1).Range;
			cellRange.Text = "Выручка";
			cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

			cellRange = clubTable.Cell(3, 1).Range;
			cellRange.Text = "Себе стоимость";
			cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

			cellRange = clubTable.Cell(4, 1).Range;
			cellRange.Text = "Валовая прибыль";
			cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

			cellRange = clubTable.Cell(5, 1).Range;
			cellRange.Text = "Сумма в обороте";
			cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

			cellRange = clubTable.Cell(2, 2).Range;
			cellRange.Text = _revenue.ToString();
			cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

			cellRange = clubTable.Cell(3, 2).Range;
			cellRange.Text = _costPrice.ToString();
			cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

			cellRange = clubTable.Cell(4, 2).Range;
			cellRange.Text = _grossProfit.ToString();
			cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

			cellRange = clubTable.Cell(5, 2).Range;
			cellRange.Text = _amountCirculation.ToString();
			cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

			app.Visible = true;
			document.SaveAs2(@"D:\outputFileWord.docx");
			document.SaveAs2(@"D:\outputFileWord.pdf", Word.WdExportFormat.wdExportFormatPDF);
		}
	}
}
