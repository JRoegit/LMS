using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO.Pipelines;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace LibraryManagementSystem
{
	/// <summary>
	/// Interaction logic for Edit.xaml
	/// </summary>
	public partial class Edit : Page
	{
		public string connStr = "Server=localhost\\SQLEXPRESS;Database=Library;Trusted_Connection=True";
		public SqlConnection conn;
		public List<BookInfo> books = new List<BookInfo>();
		public Edit()
		{
			InitializeComponent();

			resultGrid.ItemsSource = books;
			resultGrid.MaxColumnWidth = 150;
			//MAKE RESULT GRID FOR RETURN FROM DB WHEN SEARCHING
		}
		private string title;
		private string author;
		private string language;
		private string ISBN;
		private string description;
		private string pubDate;
		private string genreSearch;
		private string genres;
		private void Add_Book_Button_Click(object sender, RoutedEventArgs e)
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

			title = bkTitle.Text;
			author = bkAuthor.Text;
			language = bkLanguage.Text;
			ISBN = bkISBN.Text;
			description = bkDesc.Text;
			pubDate = bkPub.Text;
			genres = bkGenre.Text;
			
			if (Is_ISBN(ISBN))
			{
				
				SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Book (ISBN, Title, Author, Genres, Lang, Pub, Descrip) VALUES (@isbn, @title, @author, @genres, @lang, @pub, @descrip)", conn);
				sqlCommand.Parameters.AddWithValue("isbn", ISBN);
				sqlCommand.Parameters.AddWithValue("title", title);
				sqlCommand.Parameters.AddWithValue("author", author);
				sqlCommand.Parameters.AddWithValue("genres", genres);
				sqlCommand.Parameters.AddWithValue("lang", language);
				sqlCommand.Parameters.AddWithValue("pub", pubDate);
				sqlCommand.Parameters.AddWithValue("descrip", description);

				try
				{
					sqlCommand.ExecuteNonQuery();
					MessageBox.Show("Book added successfully.");
					this.NavigationService.GoBack();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
				//Make a seperate function that adds to the db probably?
				//Check if ISBN already exists in db, then attempt to add book to db, uppon success, pop up window saying success
			}	
			else
			{
				MessageBox.Show("ERROR\nEnter a valid ISBN Number.", "Error");
			}
			conn.Close();

		}
		private void Search_Book_Button_Click(object sender, RoutedEventArgs e)
		{

			books.Clear();
			conn = new SqlConnection(connStr);
			try
			{
				conn.Open();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}

			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = conn;

			switch (searchOpt.Text)
			{
				case "Title":
					title = searchText.Text;
					sqlCommand.CommandText = "SELECT * FROM Book WHERE Title LIKE " + "'%" + title + "%'";	
					break;
				case "Author":
					author = searchText.Text;
					sqlCommand.CommandText = "SELECT * FROM Book WHERE Author LIKE " + "'%" + author + "%'";
					break;
				case "Genre":
					genreSearch = searchText.Text;
					sqlCommand.CommandText = "SELECT * FROM Book WHERE Genres LIKE " + "'%" + genreSearch + "%'"; 
					break;
				case "ISBN":
					ISBN = searchText.Text;
					if (Is_ISBN(ISBN)) 
					{
						sqlCommand.CommandText = @"SELECT * FROM Book WHERE ISBN = @txt";
						sqlCommand.Parameters.AddWithValue("txt", ISBN);
					}
					else
					{
						MessageBox.Show("ERROR\nEnter a valid ISBN Number.", "Error");
					}
					break;
			}

			SqlDataReader reader = sqlCommand.ExecuteReader();
			if (reader != null)
			{
				while (reader.Read())
				{
					BookInfo tmpbook = new BookInfo(
						reader["Title"].ToString(),
						reader["Author"].ToString(),
						reader["Descrip"].ToString(),
						reader["ISBN"].ToString(),
						reader["Pub"].ToString(),
						reader["Lang"].ToString(),
						reader["Genres"].ToString());
					books.Add(tmpbook);
				}
			}

			reader.Close();

			resultGrid.Items.Refresh();
			conn.Close();
		}
		private void Update_Button_Click(object sender, RoutedEventArgs e)
		{ 
		
		}
		private void Delete_Button_Click(object sender, RoutedEventArgs e)
		{

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
			public string ISBN { get; set; }
			public string PubDate { get; set; }
			public string Language { get; set; }
			public string Genres { get; set; }
			public BookInfo(string T, string A, string D, string I, string P, string L, string G)
			{
				Title = T;
				Author = A;
				Description = D;
				ISBN = I;
				PubDate = P;
				Language = L;
				Genres = G;
			}
			
		}
	}
}
