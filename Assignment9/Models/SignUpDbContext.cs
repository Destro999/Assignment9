using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Assignment9.Models
{
    public class SignUpDbContext : DbContext
    {
        public SignUpDbContext(DbContextOptions<SignUpDbContext> options) : base(options)
        {

        }

        public DbSet<MovieModel> MovieModels { get; set; }
    }
}
