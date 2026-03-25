using Avalonia.Controls;
using Avalonia.Interactivity;
using Lab2_api; // Twój projekt z API i Bazą Danych
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace MovieGUI.Views; 

public partial class MainWindow : Window
{
    private ObservableCollection<Movie> _moviesList;

    public MainWindow()
    {
        InitializeComponent();
        LoadMoviesFromDatabase();
    }

    private void LoadMoviesFromDatabase()
    {
        using (var db = new MovieContext())
        {
            var movies = db.Movies.Include(m => m.ratings).ToList();
            _moviesList = new ObservableCollection<Movie>(movies);
            MoviesListBox.ItemsSource = _moviesList;
        }
    }

    private async void SearchButton_Click(object sender, RoutedEventArgs e)
    {
        string searchTitle = SearchTextBox.Text?.Trim();
        if (string.IsNullOrEmpty(searchTitle)) return;

        SearchButton.IsEnabled = false;

        using (var db = new MovieContext())
        {
            var dbMovie = db.Movies.Include(m => m.ratings)
                .FirstOrDefault(m => m.title.ToLower() == searchTitle.ToLower());

            if (dbMovie == null)
            {
                API api = new API();
                Movie downloadedMovie = await api.GetData(searchTitle);

                if (downloadedMovie != null && downloadedMovie.title != null)
                {
                    db.Movies.Add(downloadedMovie);
                    await db.SaveChangesAsync(); // Zapis do pliku .db
                    
                    _moviesList.Add(downloadedMovie); // Dodanie do GUI
                }
            }
        }

        SearchButton.IsEnabled = true;
        SearchTextBox.Text = string.Empty;
    }
}