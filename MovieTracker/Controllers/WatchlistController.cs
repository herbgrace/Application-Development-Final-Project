using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
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

        public static async Task removeMovie(Movie movie)
        {
            // Remove locally
            movies.Remove(movie);

            // Remove from the DB
            var collection = connectToMongo();
            var filter = Builders<Movie>.Filter.Eq("Title", movie.Title);
            collection.DeleteOne(filter);
        }

        public static void saveMovies()
        {
            try
            {
                var collection = connectToMongo();
                var currentSaved = collection.Find(Builders<Movie>.Filter.Empty).ToList();

                foreach (var movie in movies)
                {
                    // LINQ Query
                    var duplicate = currentSaved.Find(m => m.Title == movie.Title);

                    // If the movie already exists in the db
                    if (duplicate != null)
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

        public static async Task retrieveMovies()
        {
            try
            {
                var collection = connectToMongo();
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

        private static IMongoCollection<Movie> connectToMongo()
        {
            string? connectionString = Environment.GetEnvironmentVariable("DB_URL");
            if (connectionString == null)
            {
                throw new Exception("Mongo Connection string is null");
            }

            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("MovieTrackerDB");
            var collection = db.GetCollection<Movie>("SavedMovies");
            Console.WriteLine("Connected to Mongo!");

            return collection;
        }
    }
}
