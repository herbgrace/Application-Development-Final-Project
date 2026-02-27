
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.Models
{
    public class MovieResponse
    {
        public List<Movie> Results { get; set; } = new List<Movie>();
    }
}
