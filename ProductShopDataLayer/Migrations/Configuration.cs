namespace ProductShopDataLayer.Migrations
{
    using ProductShopDataLayer.Classes;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductShopDataLayer.ProductShopDataModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProductShopDataLayer.ProductShopDataModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


            context.Products.AddOrUpdate(new[]
            {
                new Product()
                {
                    Id = 1,
                    Price = 2.35M,
                    Title = "Wood",
                    Description = "Strong wood",
                    ImagePath = "/images/joel-jasmin-forestbird-xzPMUMDDsfk-unsplash.jpg"
                },
                new Product()
                {
                    Id = 2,
                    Price = 8.13M,
                    Title = "Piano",
                    Description = "Amazing piano",
                    ImagePath = "/images/markus-gjengaar-v3l8kTbPhzA-unsplash.jpg"
                },
                new Product()
                {
                    Id = 3,
                    Price = 21.34M,
                    Title = "Bubble",
                    Description = "Burst the bubble",
                    ImagePath = "/images/kai-dahms-IBo9Ehrzcfs-unsplash.jpg"
                }
            });

        }
    }
}
