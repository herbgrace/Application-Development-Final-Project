using MovieTracker.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Controllers
{
    static class WatchlistController
    {
        public static List<Movie> movies = new List<Movie>();

        public static bool addMovie(string title)
        {
            // LINQ QUERY
            bool duplicate = movies.Find(m => m.Title == title) != null;

            if (duplicate)
            {
                return false;
            }

            movies.Add(new Movie() { Title = title });
            return true;
        }

        public static void saveMovies()
        {

        }

        public static List<Movie> retrieveMovies()
        {
            return movies;
        }
    }
}
