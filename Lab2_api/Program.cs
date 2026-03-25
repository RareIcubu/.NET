using Microsoft.EntityFrameworkCore;

namespace Lab2_api;

internal class Program
{
    static void Main(string[] args)
    {
        string searchTitle = "matrix";
        
        using (var db = new MovieContext())
        {
            var dbMovie = db.Movies
                .Include(m => m.ratings)
                .FirstOrDefault(m => m.title.ToLower() == searchTitle.ToLower());

            if (dbMovie != null)
            {
                Console.WriteLine("--- ZNALEZIONO W LOKALNEJ BAZIE DANYCH ---");
                Console.WriteLine(dbMovie.ToString());
                Console.WriteLine($"Liczba zapisanych ocen: {dbMovie.ratings.Count}");
            }
            else
            {
                Console.WriteLine("--- BRAK W BAZIE. POBIERAM Z API ---");
                API api = new API();
                Movie downloadedMovie = api.GetData(searchTitle).Result;

                if (downloadedMovie != null && downloadedMovie.title != null)
                {
                    db.Movies.Add(downloadedMovie); 
                    db.SaveChanges(); 
                    
                    Console.WriteLine("--- ZAPISANO NOWY FILM DO BAZY ---");
                    Console.WriteLine(downloadedMovie.ToString());
                }
            }
        }
    }
}