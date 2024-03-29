﻿using System;
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
	/// Interaction logic for Library.xaml
	/// </summary>
	public partial class Library : Page
	{
		public Library()
		{
			InitializeComponent();
		}

		private void Edit_Button_Click(object sender, RoutedEventArgs e)
		{
			this.NavigationService.Navigate(new Uri("/Edit.xaml",UriKind.Relative));
		}
		
		private void Browse_Button_Click(object sender, RoutedEventArgs e)
		{
			this.NavigationService.Navigate(new Uri("/Browse.xaml", UriKind.Relative));
		}
	}
}
