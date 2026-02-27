using MovieTracker.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Controllers
{
    static class TrackerController
    {
        public static List<Movie> movies = new List<Movie>() {
            new Movie()
            {
                Title = "Guardians of the Galaxy Vol. 3"
            },

            new Movie()
            {
                Title = "Interstellar"
            }
        };
    }
}
