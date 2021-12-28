#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Eqirl.Models;

namespace Eqirl.Data
{
    public class EqirlContext : DbContext
    {
        public EqirlContext (DbContextOptions<EqirlContext> options)
            : base(options)
        {
        }

        public DbSet<Eqirl.Models.QuizQestion> QuizQestions { get; set; }
    }
}
