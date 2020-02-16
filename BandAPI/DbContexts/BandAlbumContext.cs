using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BandAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BandAPI.DbContexts
{
    public class BandAlbumContext : DbContext
    {
        public BandAlbumContext(DbContextOptions<BandAlbumContext> options) : base(options) { }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Band>().HasData(new Band()
            {
                Id = Guid.Parse("6b1eea43-5597-45a6-bdea-e68c60564247"),
                Name = "Metallica",
                Founded = new DateTime(1980, 1, 1),
                MainGenre = "Heavy Metal"
            },
            new Band
            {
                Id = Guid.Parse("23a19cb4-4ff9-11ea-b77f-2e728ce88125"),
                Name = "A-ha",
                Founded = new DateTime(1981, 1, 1),
                MainGenre = "Pop"
            },
            new Band
            {
                Id = Guid.Parse("48c21186-4ff9-11ea-b77f-2e728ce88125"),
                Name = "Tom",
                Founded = new DateTime(1983, 3, 3),
                MainGenre = "Jazza"
            },
            new Band
            {
                Id = Guid.Parse("48c2147e-4ff9-11ea-b77f-2e728ce88125"),
                Name = "Peter",
                Founded = new DateTime(1984, 4, 4),
                MainGenre = "Disco"
            },
            new Band

            {
                Id = Guid.Parse("48c215f0-4ff9-11ea-b77f-2e728ce88125"),
                Name = "Michel",
                Founded = new DateTime(1985, 5, 5),
                MainGenre = "Rock And Roll"
            });
            modelBuilder.Entity<Album>().HasData(
                new Album()
                    {
                        Id = Guid.Parse("bfa08d6e-4ff9-11ea-b77f-2e728ce88125"),
                        Title = "Master Of Puppets",
                        Description = "One of the best heavy metal albums ever",
                        BandId = Guid.Parse("6b1eea43-5597-45a6-bdea-e68c60564247")
                    },
                 new Album()
                 {
                     Id = Guid.Parse("bfa09066-4ff9-11ea-b77f-2e728ce88125"),
                     Title = "Appetite For Destruction",
                     Description = "One of the best heavy metal albums ever",
                     BandId = Guid.Parse("23a19cb4-4ff9-11ea-b77f-2e728ce88125")
                 },
                  new Album()
                  {
                      Id = Guid.Parse("bfa091c4-4ff9-11ea-b77f-2e728ce88125"),
                      Title = "Be Here Now",
                      Description = "One of the best heavy metal albums ever",
                      BandId = Guid.Parse("48c21186-4ff9-11ea-b77f-2e728ce88125")
                  },
                   new Album()
                   {
                       Id = Guid.Parse("bfa093ae-4ff9-11ea-b77f-2e728ce88125"),
                       Title = "Hunting Hight and low",
                       Description = "One of the best heavy metal albums ever",
                       BandId = Guid.Parse("48c2147e-4ff9-11ea-b77f-2e728ce88125")
                   },
                    new Album()
                    {
                        Id = Guid.Parse("bfa095a2-4ff9-11ea-b77f-2e728ce88125"),
                        Title = "Waterloo",
                        Description = "Very groovy album",
                        BandId = Guid.Parse("48c215f0-4ff9-11ea-b77f-2e728ce88125")
                    });

            base.OnModelCreating(modelBuilder);
        }
    }
}
