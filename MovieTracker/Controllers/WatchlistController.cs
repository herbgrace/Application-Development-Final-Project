using MongoDB.Driver;
using MongoDB.Bson;
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

        public static bool addMovie(Movie movie)
        {
            // LINQ QUERY
            bool duplicate = movies.Find(m => m.Title == movie.Title) != null;

            if (duplicate)
            {
                return false;
            }

            movies.Add(movie);
            return true;
        }

        // TODO - saving movies deltes the first entry and isn't working right now, pls fix
        public static void saveMovies()
        {
            string? connectionString = Environment.GetEnvironmentVariable("DB_URL");
            if (connectionString == null) {
                Console.WriteLine("Connection String is null, cannot save movies");
                return;
            }

            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("MovieTrackerDB");
                var collection = db.GetCollection<Movie>("SavedMovies");
                Console.WriteLine("Connected to Mongo!");

                var currentSaved = collection.Find(Builders<Movie>.Filter.Empty).ToList();
                var moviesCopy = movies;
                foreach (var movie in movies)
                {
                    // LINQ Query
                    // If the movie already exists in the db
                    if (currentSaved.Find(m => m.Title == movie.Title) != null)
                    {
                        // Only update the movie that's in the collection
                        var filter = Builders<Movie>.Filter.Eq("Title", movie.Title);

                        // Only update the watched value
                        var update = Builders<Movie>.Update
                            .Set("Watched", movie.Watched);

                        // Actually update that john
                        collection.UpdateOne(filter, update);
                        continue;
                    }

                    // Movie doesn't already exist, add instead of update
                    collection.InsertOne(movie);
                }
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }
        }

        public static void retrieveMovies()
        {
            string? connectionString = Environment.GetEnvironmentVariable("DB_URL");
            if (connectionString == null)
            {
                Console.WriteLine("Connection String is null, cannot retrieve movies");
                return;
            }

            try
            {
                var client = new MongoClient(connectionString);
                var db = client.GetDatabase("MovieTrackerDB");
                var collection = db.GetCollection<Movie>("SavedMovies");
                Console.WriteLine("Connected to Mongo!");

                var savedMovies = collection.Find(Builders<Movie>.Filter.Empty).ToList();

                foreach (Movie movie in savedMovies) {
                    // LINQ Query
                    Movie? duplicate = movies.Find(m => m.Title == movie.Title);

                    // Loaded movie is not already in the list, add it
                    if (duplicate == null)
                    {
                        movies.Add(movie);
                        continue;
                    }

                    // Loaded movie is already in the list, update it
                    duplicate = movie;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }
        }
    }
}
