using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Movie> Movies { get; protected set; }

        // [BindProperty]
        public string SearchTerms { get; set; } = "";
        
        // [BindProperty]
        public string[] MPAARatings { get; set; }

        // [BindProperty]
        public string[] Genres { get; set; }

        // [BindProperty]
        public double? IMDBMin { get; set; }

        // [BindProperty]
        public double? IMDBMax { get; set; }

        // [BindProperty]
        public uint? RTMin { get; set; }

        // [BindProperty]
        public uint? RTMax { get; set; }


        public void OnGet(string SearchTerms, string[] MPAARatings, string[] Genres, double IMDBMin, double IMDBMax, uint RTMin, uint RTMax) // string SearchTerms, string[] MPAARatings, string[] Genre, double IMDBMin, double IMDBMax, uint RTMin, uint RTMax (as parameters)
        {
            //SearchTerms = Request.Query["SearchTerms"];
            Movies = MovieDatabase.Search(SearchTerms);

            //MPAARatings = Request.Query["MPAARatings"];
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);

            //Genres = Request.Query["Genres"];
            Movies = MovieDatabase.FilterByMPAARating(Movies, Genres);

            this.IMDBMax = IMDBMax;
            this.IMDBMin = IMDBMin;
            //IMDBMin = double.Parse(Request.Query["IMDBMin"]);
            //IMDBMax = double.Parse(Request.Query["IMDBMax"]);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);

            this.RTMax = RTMax;
            this.RTMin = RTMin;

            Movies = MovieDatabase.FilterByRTRating(Movies, RTMin, RTMax);
        }
    }
}
