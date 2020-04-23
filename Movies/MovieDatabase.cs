using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies = new List<Movie>();

        private static string[] genres;
        public static string[] Genres => genres;
        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        static MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd(); // Puts file into one long string
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);

                HashSet<string> genreSet = new HashSet<string>();
                foreach(Movie movie in movies)
                {
                    if(movie.MajorGenre != null)
                    {
                        genreSet.Add(movie.MajorGenre);
                    }
                }
                genres = genreSet.ToArray();
                
            }
        }

        /// <summary>
        /// Gets all the movies in the database
        /// </summary>
        public static IEnumerable<Movie> All { get { return movies; } } // The list of movies as Ienumerable so you can iterate through them

        /// <summary>
        /// Searches database for movies
        /// </summary>
        /// <param name="terms"></param>
        /// <returns></returns>
        public static IEnumerable<Movie> Search(string terms)
        {
            List<Movie> results = new List<Movie>();

            if (terms == null) return All;

           
            foreach(Movie movie in All)
            {
                if(movie.Title != null && movie.Title.Contains(terms, StringComparison.InvariantCultureIgnoreCase))
                {
                    results.Add(movie);
                }             
            }

            return results;
        }

        //------------------------------------------------------------------------------------------------------------------
        public static string[] MPAARatings
        {
            get => new string[]
            {
                "G",
                "PG",
                "PG-13",
                "R",
                "NC-17"
            };
        }

        public static IEnumerable<Movie> FilterByMPAARating(IEnumerable<Movie> movies, IEnumerable<string> ratings)
        {
            if (ratings == null || ratings.Count() == 0) return movies;

            List<Movie> results = new List<Movie>();

            foreach(Movie movie in movies)
            {
                if(movie.MPAARating != null && ratings.Contains(movie.MPAARating))
                {
                    results.Add(movie);
                }
            }

            return results;
        }

        public static IEnumerable<Movie> FilterByIMDBRating(IEnumerable<Movie> movies, double? min, double? max)
        {
            if (min == null && max == null) return movies;
            var results = new List<Movie>();

            // only a maximum specified
            if (min == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.IMDBRating <= max) results.Add(movie);
                }
                return results;
            }

            // only a minimum specified 
            if (max == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.IMDBRating >= min) results.Add(movie);
                }
                return results;
            }

            // Both minimum and maximum specified
            foreach (Movie movie in movies)
            {
                if (movie.IMDBRating >= min && movie.IMDBRating <= max)
                {
                    results.Add(movie);
                }
            }
            return results;
        }

        public static IEnumerable<Movie> FilterByRTRating(IEnumerable<Movie> movies, double? min, double? max)
        {
            if (min == null && max == null) return movies;
            var results = new List<Movie>();

            // only a maximum specified
            if (min == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.RottenTomatoesRating <= max) results.Add(movie);
                }
                return results;
            }

            // only a minimum specified 
            if (max == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.RottenTomatoesRating >= min) results.Add(movie);
                }
                return results;
            }

            // Both minimum and maximum specified
            foreach (Movie movie in movies)
            {
                if (movie.RottenTomatoesRating >= min && movie.RottenTomatoesRating <= max)
                {
                    results.Add(movie);
                }
            }
            return results;
        }

    }
}
