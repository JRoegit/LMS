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
	/// Interaction logic for Login.xaml
	/// </summary>
	public partial class Login : Page
	{
		public Login()
		{
			InitializeComponent();
		}

		private void Sign_Up_Button_Click(object sender, RoutedEventArgs e)
		{
			this.NavigationService.Navigate(new Uri("/SignUp.xaml", UriKind.Relative));
		}
		private string username;
		private string password;
		private void Login_Button_Click(object sender, RoutedEventArgs e)
		{
			username = txtUser.Text;
			password = txtPass.Password;
			
			//ADD CHECKS FOR SUCCESSFUL LOGIN BEFORE CHANGING TABS
			this.NavigationService.Navigate(new Uri("/Library.xaml", UriKind.Relative));
		}
	}
}
