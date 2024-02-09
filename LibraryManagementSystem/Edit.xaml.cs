using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
		public BookInfo resultbook;
		public Edit()
		{
			InitializeComponent();

			resultGrid.ItemsSource = books;
			resultGrid.MaxColumnWidth = 150;
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
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}

				bkTitle.Text = "";
				bkAuthor.Text = "";
				bkLanguage.Text = "";
				bkISBN.Text = "";
				bkDesc.Text = "";
				bkPub.Text = "";
				bkGenre.Text = "";

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
		private void Search_ISBN_Button_Click(object sender, RoutedEventArgs e)
		{
			
			ISBN = ISBNSearch.Text;
			if (Is_ISBN(ISBN))
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
				
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = conn;
				sqlCommand.CommandText = "SELECT * FROM Book WHERE ISBN = " + ISBN;
				SqlDataReader reader = sqlCommand.ExecuteReader();

				if (reader != null)
				{
					if (reader.Read())
					{
						resultbook = new BookInfo(
							reader["Title"].ToString(),
							reader["Author"].ToString(),
							reader["Descrip"].ToString(),
							reader["ISBN"].ToString(),
							reader["Pub"].ToString(),
							reader["Lang"].ToString(),
							reader["Genres"].ToString()
							);
					}
					else
					{
						MessageBox.Show("No book with that ISBN number", "ISBN Error");
					}
				}

				TitleSearch.Text = resultbook.Title;
				AuthorSearch.Text = resultbook.Author;
				GenreSearch.Text = resultbook.Genres;
				ISBNSear.Text = resultbook.ISBN;
				LanguageSearch.Text = resultbook.Language;
				PubDateSearch.Text = resultbook.PubDate;
				DescSearch.Text = resultbook.Description;
				reader.Close();
				conn.Close();
			}
			else
			{
				MessageBox.Show("Enter a valid ISBN number.", "Input Error");
			}

		}
		private void Update_Button_Click(object sender, RoutedEventArgs e)
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

			SqlCommand sqlCommand = new SqlCommand(
				"UPDATE Book SET " +
				"Title = '" + TitleSearch.Text + "', " +
				"Author = '" + AuthorSearch.Text + "', " +
				"Genres = '" + GenreSearch.Text + "', " +
				"Lang = '" + LanguageSearch.Text + "', " +
				"Pub = '" + PubDateSearch.Text + "', " +
				"Descrip = '" + DescSearch.Text + "' " +
				"WHERE ISBN = '" + resultbook.ISBN + "' "
				);
			sqlCommand.Connection = conn;

			try
			{
				sqlCommand.ExecuteNonQuery();
				MessageBox.Show("Book updated successfully.");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			conn.Close();
		}
		private void Delete_Button_Click(object sender, RoutedEventArgs e)
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

			SqlCommand sqlCommand = new SqlCommand(
				"DELETE FROM Book WHERE ISBN = '" + resultbook.ISBN + "'" 	
				);
			sqlCommand.Connection = conn;

			try
			{
				sqlCommand.ExecuteNonQuery();
				MessageBox.Show("Book updated successfully.");
				ISBNSearch.Text = "";
				TitleSearch.Text = "";
				AuthorSearch.Text = "";
				DescSearch.Text = "";
				ISBNSear.Text = "";
				PubDateSearch.Text = "";
				GenreSearch.Text = "";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}

			resultbook = new BookInfo("", "", "", "", "", "", "");
			conn.Close();
			//remember to null out the result book.
		}
		private bool Is_ISBN(string ISBN)
		{
			short ISBNSize = 0;
			foreach (char c in ISBN) // COULD PROBABLY CONDENSE THESE TWO INTO ONE
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
		
		public class BookInfo
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
