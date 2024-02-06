using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	
	public partial class App : Application
	{
		public string connStr = "Server=localhost\\SQLEXPRESS;Database=Library;Trusted_Connection=True";
		public SqlConnection conn;

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			
			MainWindow MainWindow = new MainWindow();
			MainWindow.Show();

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
		private void connectToMySql()
		{
			MySqlConnection conn = new MySqlConnection(connStr);
			try
			{
				MessageBox.Show("connecting...");
				conn.Open();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		
	}
}
