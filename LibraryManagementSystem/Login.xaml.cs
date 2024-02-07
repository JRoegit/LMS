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
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace LibraryManagementSystem
{
	/// <summary>
	/// Interaction logic for Login.xaml
	/// </summary>
	public partial class Login : Page
	{
		public string connStr = "Server=localhost\\SQLEXPRESS;Database=Library;Trusted_Connection=True";
		public SqlConnection conn;
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
				conn = new SqlConnection(connStr);
				try
				{
					conn.Open();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
				username = txtUser.Text;
				password = txtPass.Password;

				string hashword = hash(username, password);
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = conn;
				sqlCommand.CommandText = @"SELECT * FROM Users WHERE Username = @username";
				sqlCommand.Parameters.AddWithValue("username", username);
				SqlDataReader reader = sqlCommand.ExecuteReader();
				if(reader == null)
				{
					MessageBox.Show("NO USER WITH THAT NAME");
				}
				else if(reader.Read())
				{
					string uname = reader["Username"].ToString();
					string pword = reader["password"].ToString();
					
					if (check_pass(hashword, pword))
					{	
						reader.Close();
						conn.Close();
						this.NavigationService.Navigate(new Uri("/Library.xaml", UriKind.Relative));
					}
					else
					{
						MessageBox.Show("Incorrect username or password.");
					}
				}
				conn.Close();
				reader.Close();
			}
		}
		private string hash(string username, string password)
		{
			byte[] tmpSource;
			byte[] tmpHash;

			tmpSource = ASCIIEncoding.ASCII.GetBytes(password + username); //use username as salt
			tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

			StringBuilder sb = new StringBuilder();
			foreach (byte b in tmpHash)
			{
				sb.Append(b);
			}
			return sb.ToString();
		}
		private bool check_pass(string entered, string real)
		{
			return entered.Equals(real);
		}

	}
}
