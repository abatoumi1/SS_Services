using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportServiceApp.Models;

namespace SportServiceApp
{

    public class SeedData {

        public static void SeedDatabase(DataContext context) {
            //context.Database.Migrate();
            try
            {
                if (context.Products.Count() == 0)
                {
                    var s1 = new Supplier
                    {
                        Name = "Splash Dudes",
                        City = "San Jose",
                        State = "CA"
                    };
                    var s2 = new Supplier
                    {
                        Name = "Soccer Town",
                        City = "Chicago",
                        State = "IL"
                    };
                    var s3 = new Supplier
                    {
                        Name = "Chess Co",
                        City = "New York",
                        State = "NY"
                    };


                    var c1 = new Category
                    {
                        Name = "Watersports",
                        
                    };
                    var c2 = new Category
                    {
                        Name = "Soccer",
                       
                    };
                    var c3 = new Category
                    {
                        Name = "Chess",
                       
                    };



                    context.Products.AddRange(
                        new Product
                        {
                            Name = "Kayak",
                            Description = "A boat for one person",
                            Category = c1,
                            Price = 275,
                            Supplier = s1,
                            Ratings = new List<Rating> {
                             new Rating { Stars = 4 }, new Rating { Stars = 3 }}
                        },
                         new Product
                         {
                             Name = "Lifejacket",
                             Description = "Protective and fashionable",
                             Category = c1,
                             Price = 48.95m,
                             Supplier = s1,
                             Ratings = new List<Rating> {
                             new Rating { Stars = 2 }, new Rating { Stars = 5 }}
                         },
                         new Product
                         {
                             Name = "Soccer Ball",
                             Description = "FIFA-approved size and weight",
                             Category = c2,
                             Price = 19.50m,
                             Supplier = s2,
                             Ratings = new List<Rating> {
                             new Rating { Stars = 1 }, new Rating { Stars = 3 }}
                         },
                         new Product
                         {
                             Name = "Corner Flags",
                             Description = "Give your pitch a professional touch",
                             Category = c2,
                             Price = 34.95m,
                             Supplier = s2,
                             Ratings = new List<Rating> { new Rating { Stars = 3 } }
                         },
                         new Product
                         {
                             Name = "Stadium",
                             Description = "Flat-packed 35,000-seat stadium",
                             Category = c2,
                             Price = 79500,
                             Supplier = s2,
                             Ratings = new List<Rating> { new Rating { Stars = 1 },
                             new Rating { Stars = 4 }, new Rating { Stars = 3 }}
                         },
                         new Product
                         {
                             Name = "Thinking Cap",
                             Description = "Improve brain efficiency by 75%",
                             Category = c3,
                             Price = 16,
                             Supplier = s3,
                             Ratings = new List<Rating> { new Rating { Stars = 5 },
                             new Rating { Stars = 4 }}
                         },
                         new Product
                         {
                             Name = "Unsteady Chair",
                             Description = "Secretly give your opponent a disadvantage",
                             Category = c2,
                             Price = 29.95m,
                             Supplier = s3,
                             Ratings = new List<Rating> { new Rating { Stars = 3 } }
                         },
                         new Product
                         {
                             Name = "Human Chess Board",
                             Description = "A fun game for the family",
                             Category = c3,
                             Price = 75,
                             Supplier = s3
                         },
                         new Product
                         {
                             Name = "Bling-Bling King",
                             Description = "Gold-plated, diamond-studded King",
                             Category = c3,
                             Price = 1200,
                             Supplier = s3
                         });
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var log = ex;
            }
            
        }
    }
}
