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

namespace MiraMaxCoursePaper.Pages
{
	public partial class AuditPage : Page
	{
		public AuditPage()
		{
			InitializeComponent();

			Audit.ItemsSource = DataWorking.SelectActionById(CurrentUser.Id).OrderBy(x => x.Time).ToList();
		}

		private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
		{

		}
	}
}
