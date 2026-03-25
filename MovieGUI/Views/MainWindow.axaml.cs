using Avalonia.Controls;
using Avalonia.Interactivity;
using Lab2_api; // Twój projekt z API i Bazą Danych
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace MovieGUI.Views; // Zmień namespace jeśli masz inny w projekcie!

public partial class MainWindow : Window
{
    // ObservableCollection automatycznie odświeża UI, gdy dodamy nowy film
    private ObservableCollection<Movie> _moviesList;

    public MainWindow()
    {
        InitializeComponent();
        LoadMoviesFromDatabase();
    }

    private void LoadMoviesFromDatabase()
    {
        // Wczytujemy to, co masz już w pliku movies.db
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

        // Wyłączamy przycisk na czas szukania, żeby uniknąć spamu
        SearchButton.IsEnabled = false;

        using (var db = new MovieContext())
        {
            // 1. Sprawdzamy czy film jest już w naszej bazie
            var dbMovie = db.Movies.Include(m => m.ratings)
                .FirstOrDefault(m => m.title.ToLower() == searchTitle.ToLower());

            if (dbMovie == null)
            {
                // 2. Nie ma w bazie? Pytamy OMDb API!
                API api = new API();
                Movie downloadedMovie = await api.GetData(searchTitle);

                // 3. Jeśli API coś zwróciło, zapisujemy to w bazie SQLite i dodajemy do listy na ekranie
                if (downloadedMovie != null && downloadedMovie.title != null)
                {
                    db.Movies.Add(downloadedMovie);
                    await db.SaveChangesAsync(); // Zapis do pliku .db
                    
                    _moviesList.Add(downloadedMovie); // Dodanie do GUI
                }
            }
        }

        // Włączamy przycisk z powrotem i czyścimy pole tekstowe
        SearchButton.IsEnabled = true;
        SearchTextBox.Text = string.Empty;
    }
}