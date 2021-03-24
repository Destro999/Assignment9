using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Storage area for edits to be made
/// </summary>
namespace Assignment9.Models
{
    public static class TempStorage
    {
        private static List<MovieModel> movies = new List<MovieModel>();
        public static IEnumerable<MovieModel> Applications => movies;
        public static void AddApplication(MovieModel newMovie)
        {
            movies.Add(newMovie);
        }
    }
}

