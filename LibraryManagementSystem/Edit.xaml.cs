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
		List<BookInfo> books = new List<BookInfo>();
		public Edit()
		{
			InitializeComponent();
			//MAKE RESULT GRID FOR RETURN FROM DB WHEN SEARCHING
		}
		private string title;
		private string author;
		private string language;
		private string ISBN;
		private string description;
		private string pubDate;
		private string genreSearch;
		private List<string> genres;
		private void Add_Book_Button_Click(object sender, RoutedEventArgs e)
		{
			title = bkTitle.Text;
			author = bkAuthor.Text;
			language = bkLanguage.Text;
			ISBN = bkISBN.Text;
			description = bkDesc.Text;
			pubDate = bkPub.Text;
			genres = bkGenre.Text.Split(',').ToList<string>();
			
			if (Is_ISBN(ISBN))
			{
				//Make a seperate function that adds to the db probably?
				//Check if ISBN already exists in db, then attempt to add book to db, uppon success, pop up window saying success
			}	
			else
			{
				MessageBox.Show("ERROR\nEnter a valid ISBN Number.", "Error");
			}

		}
		private void Search_Book_Button_Click(object sender, RoutedEventArgs e)
		{
			switch (searchOpt.Text)
			{
				case "Title":
					title = searchText.Text;					
					break;
				case "Author":
					author = searchText.Text;
					break;
				case "Genre":
					genreSearch = searchText.Text;
					break;
				case "ISBN":
					ISBN = searchText.Text;
					if (Is_ISBN(ISBN)) 
					{
						//do stuff
					}
					else
					{
						MessageBox.Show("ERROR\nEnter a valid ISBN Number.", "Error");
					}
					break;
			}
			string mashup = title + author + genreSearch + ISBN;
			MessageBox.Show(mashup);
			resultGrid.ItemsSource = books;
		}
		private string Genres_toString(List<string> Genres)
		{
			StringBuilder newString = new StringBuilder();
			foreach (string str in Genres)
			{
				newString.Append(str + ", ");
			}
			return newString.ToString();
		}
		private bool Is_ISBN(string ISBN)
		{
			short ISBNSize = 0;
			foreach (char c in ISBN)
			{
				ISBNSize++;
				if (!Char.IsDigit(c))
				{
					return false;
				}
			}
			if (!(ISBNSize == 10 || ISBNSize == 13)) //ISBN conditions, Must be 10 or 13 chars, and all nums
			{
				return false;
			}
			return true;
		}
		public class BookInfo // FOR RETURN FROM DB????
		{
			public string Title { get; set; }
			public string Author { get; set; }	
			public string Description { get; set; }
			public string ISBN {  get; set; }
			public string PubDate { get; set; }
			public string Language { get; set; }
			public string Genres { get; set; }
		}
	}
}
