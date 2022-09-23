using Microsoft.AspNetCore.Mvc;

namespace AspNetAPI.Controllers
{
    [ApiController]
    [Route("film")]
    public class MovieController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            //throw new Exception("PAH!!");
            return "Hello from the Movie API";
        }

        [HttpPost]
        public string Post([FromBody] string title)
        {
            return $"You submitted the film called {title.ToUpper()}";
        }

        // Using query parameters, via https://localhost:7150/film/movieData/ET/Steven%20Spielburg/1982
        [HttpGet]
        [Route("movieData/{title}/{director}/{releaseYear}")]
        public string GetMovieData(string title, string director, int releaseYear)
        {
            return $"The film {title} was released in {releaseYear} and directed by {director}";
        }

        // Collecting information from URL path, via https://localhost:7150/film/movie/22
        [HttpGet]
        [Route("movie/{id}")]
        public object GetMovie([FromRoute] int id)
        {
            //TODO: Add code that interogates Movie DB for movie with the passed in id
            var movie = new
            {
                Title = "Star Wars",
                Director = "George Lucas",
                YearReleased = 1977,
                Revenue = 775800000
            };
            return movie;
        }

        // Receive and return headers
        [HttpPost]
        [Route("headers")]
        public string PostHeaders([FromHeader]int MovieId)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("You submitted headers...");
            string additionalData = string.Empty;
            foreach (string header in Request.Headers.Keys)
            {
                sb.AppendLine($"- {header} = {Request.Headers[header]}");
                if (header == "movieid")
                {
                    //TODO: Code to look up movie from Id
                    Response.Headers.Add(header, Request.Headers[header]);
                    Response.Headers.Add("MovieTitle", "Star Wars");
                    additionalData = $"Returning header:\n{header}: {Request.Headers[header]}";
                    additionalData += $"\nMovie Title: Star Wars";
                }
            }
            sb.AppendLine(additionalData);
            return sb.ToString();
        }

        // Extract payload as strongly-typed JSON object
        [HttpPost]
        [Route("movie")]
        public string PostNewMovie([FromBody] Movie movie)
        {
            return $"{movie.Title} was directed by {movie.Director} " +
                $"it was released on {movie.ReleaseDate} and grossed {movie.Revenue}";
        }
    }

    public class Movie
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Revenue { get; set; }
    }

}
