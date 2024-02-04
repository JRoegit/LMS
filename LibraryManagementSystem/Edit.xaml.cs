using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
	/// Interaction logic for Edit.xaml
	/// </summary>
	public partial class Edit : Page
	{
		public Edit()
		{
			InitializeComponent();

			//resultGrid;  MAKE RESULT GRID FOR RETURN FROM DB WHEN SEARCHING
			//
			//List<BookInfo> books = new List<BookInfo>();
			//
			//
			//
		}
		private string title;
		private string author;
		private string ISBN;
		private string language;
		private string description;
		private string pubDate;
		private List<string> genres;
		private void Add_Book_Button_Click(object sender, RoutedEventArgs e)
		{
			title = bkTitle.Text;
			author = bkAuthor.Text;
			ISBN = bkISBN.Text;
			language = bkLanguage.Text;
			description = bkDesc.Text;
			genres = bkGenre.Text.Split(',',' ').ToList<string>();
			pubDate = bkPub.Text;
			string giantmess = title + author + description + language + ISBN + pubDate + genres;
			MessageBox.Show(giantmess);
			//Check if ISBN already exists in db, then attempt to add book to db, uppon success, pop up window saying success
		
		}

		public class BookInfo // FOR RETURN FROM DB????
		{
			public string Title { get; set; }
			public string Author { get; set; }	
			public string Description { get; set; }
			public string ISBN {  get; set; }
			public string[] Genres { get; set; }
			public DateTime PUBDATE { get; set;}

		}
	}
}
