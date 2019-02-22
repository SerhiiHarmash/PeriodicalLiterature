using PeriodicalLiterature.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace PeriodicalLiterature.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PeriodicalLiterature.DAL.PeriodicalLiteratureContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }


        protected override void Seed(PeriodicalLiterature.DAL.PeriodicalLiteratureContext context)
        {
            if (!context.Genres.Any())
            {
                context.Genres.AddRange(GetGenres());
                context.SaveChanges();
            }

        }

        private List<Genre> GetGenres()
        {
            var genre = new List<Genre>()
            {
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "sport"
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "fashion"
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "traveling"
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "art"
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "cars"
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "family"
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "job"
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name ="comics"
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name ="scientific"
                }
            };

            return genre;
        }
    }
}
