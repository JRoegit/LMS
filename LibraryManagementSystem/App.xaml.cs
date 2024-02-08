using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagementSystem
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	
	public partial class App : Application
	{
		private void Application_Startup(object sender, StartupEventArgs e)
		{		
			MainWindow MainWindow = new MainWindow();
			MainWindow.Show();
		}	
	}
}
