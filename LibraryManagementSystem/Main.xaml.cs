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

namespace LibraryManagementSystem
{
	/// <summary>
	/// Interaction logic for Main.xaml
	/// </summary>
	public partial class Main : Page
	{
		public Main()
		{
			InitializeComponent();
		}

		private void Library_Button_Click(object sender, RoutedEventArgs e)
		{
			MainPage.Content = new Library();
		}

		private void My_Books_Button_Click(object sender, RoutedEventArgs e)
		{
			MainPage.Content = new MyBooks();
		}

		private void Sign_Out_Button_Click(object sender, RoutedEventArgs e)
		{
			this.NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
		}
	}
}
