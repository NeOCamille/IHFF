using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace IHFF_Websystem.Models
{
    public class InheritanceMappingContext : DbContext
    {
        public DbSet<Evenement> Evenementen { get; set; }
    }
    public class IHFFContext : DbContext
    {
        public IHFFContext()
            : base("IHFFConnection")
        {
           // Database.SetInitializer<IHFFContext>(null);
        }

        public DbSet<Diner> Diners { get; set; }
        public DbSet<Evenement> Evenementen { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Locatie> Locaties { get; set; }
        public DbSet<Medewerker> Medewerkers { get; set; }
        public DbSet<Special> Specials { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistEvenement> WishlistEvenements { get; set; }

    }
}