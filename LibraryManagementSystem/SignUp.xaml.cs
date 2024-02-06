using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
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
	/// Interaction logic for SignUp.xaml
	/// </summary>
	public partial class SignUp : Page
	{

		public string connStr = "Server=localhost\\SQLEXPRESS;Database=Library;Trusted_Connection=True";
		public SqlConnection conn;
		public SignUp()
		{
			InitializeComponent();

			conn = new SqlConnection(connStr);
			try
			{
				conn.Open();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void Back_Button_Click(object sender, RoutedEventArgs e)
		{
			conn.Close();
			this.NavigationService.GoBack();
        }
		private string username;
		private string password;
		private void Sign_Up_Button_Click(object sender, RoutedEventArgs e)
		{
			username = txtUser.Text;
			password = txtPass.Password;

			string hashword = hash(username,password);
			SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Users (Username, password) VALUES (@username, @password)",conn);
			sqlCommand.Parameters.AddWithValue("username", username);
			sqlCommand.Parameters.AddWithValue("password", hash(username, password));
			try
			{
				int effect = sqlCommand.ExecuteNonQuery();
				MessageBox.Show("Account created successfully.");
				conn.Close();
				this.NavigationService.GoBack();
			}
			catch
			{
				MessageBox.Show("An account with that username already exists. Try another username.");
			}
			
			//MessageBox.Show(username,password);
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
	}
}
