
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Models
{
    public class MovieResponse
    {
        public int Page { get; set; }
        public List<Movie> Results { get; set; }
        public int Total_Results { get; set; }
        public int Total_Pages { get; set; }
    }

}
