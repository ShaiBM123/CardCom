using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CardComSite1;

namespace CardComSite1.Data
{
    public class CardComSite1Context : DbContext
    {
        public CardComSite1Context (DbContextOptions<CardComSite1Context> options)
            : base(options)
        {
        }

        public DbSet<CardComSite1.Person>? Person { get; set; }
 
    }
}
