
using System.ComponentModel.DataAnnotations;

namespace MovieList.Models
{
    public class Movie
    {
        //we store the movie id in Movie ID 
        public int MovieId { get; set; }
        [Required(ErrorMessage ="Please enter a name.")]
        public string Name { get; set; }
        //we require the year, error message if invalid
        [Required(ErrorMessage = "Please enter a year.")]
        //the range is from 1889 and after. Error message if not.
        [Range(1889,2999,ErrorMessage = "Year must be after 1889.")]
        //year is the year the movie came out
        public int? Year { get; set; }
        //we require a rating to be typed, so we send a message if not.
        [Required(ErrorMessage = "Please enter a rating.")]
        //the range is between 1 and 5, error message if not valid
        [Range(1,5,ErrorMessage = "Please enter a rating between 1 and 5.")]
        //Rating is a number between 1 and 5 
        public int? Rating { get; set; }
        [Required(ErrorMessage ="Please enter a genre.")]
        //property for Genre ID
        public string GenreId { get; set; }
        //property for Genre
        public Genre Genre { get; set; }
        //a string called slug which concatenates the Name and year with dashes in between. All in lower case.
        public string Slug => Name?.Replace(' ', '-').ToLower() + '-' + Year.ToString();
    }
}
