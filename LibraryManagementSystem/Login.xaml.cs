using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
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
			if(txtUser.Text != "" && txtPass.Password != "") 
			{ 
				username = txtUser.Text;
				password = txtPass.Password;
			
				byte[] tmpSource;
				byte[] tmpHash;
					
				tmpSource = ASCIIEncoding.ASCII.GetBytes(password + username); //use username as salt
				tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

				StringBuilder sb = new StringBuilder();
				foreach (byte b in tmpHash)
				{
					sb.Append(b);
				}
				MessageBox.Show(sb.ToString());
				this.NavigationService.Navigate(new Uri("/Library.xaml", UriKind.Relative));
				
			}
			//ADD CHECKS FOR SUCCESSFUL LOGIN BEFORE CHANGING TABS
			
		}
		private bool Check_Login(string username,string password)
		{
			//compare hashed value to the hashed value stored in the db?
			return false;
		}
	}
}
