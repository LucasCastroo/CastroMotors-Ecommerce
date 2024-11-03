namespace CastroMotors.Migrations
{
    using CastroMotors.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CastroMotors.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CastroMotors.Models.ApplicationDbContext";
        }

        protected override void Seed(CastroMotors.Models.ApplicationDbContext context)
        {
            // Criação de Marcas
            var brands = new List<Brand>
            {
                new Brand { Name = "Toyota", CountryOfOrigin = "Japão", FoundedYear = 1937 },
                new Brand { Name = "Ford", CountryOfOrigin = "Estados Unidos", FoundedYear = 1903 },
                new Brand { Name = "Chevrolet", CountryOfOrigin = "Estados Unidos", FoundedYear = 1911 },
                new Brand { Name = "Honda", CountryOfOrigin = "Japão", FoundedYear = 1948 },
                new Brand { Name = "Volkswagen", CountryOfOrigin = "Alemanha", FoundedYear = 1937 },
                new Brand { Name = "BMW", CountryOfOrigin = "Alemanha", FoundedYear = 1916 },
            };

            brands.ForEach(b => context.Brands.AddOrUpdate(br => br.Name, b));
            context.SaveChanges();

            // Criação de Categorias
            var categories = new List<Category>
            {
                new Category { Name = "SUV", Description = "Veículo utilitário esportivo", Code = "SUV001" },
                new Category { Name = "Sedan", Description = "Carro de passeio com porta-malas separado", Code = "SED001" },
                new Category { Name = "Hatchback", Description = "Carro pequeno com porta-malas integrado", Code = "HAT001" },
                new Category { Name = "Conversível", Description = "Carro com teto removível", Code = "CONV001" },
                new Category { Name = "Esportivo", Description = "Carro de alto desempenho e velocidade", Code = "SPT001" },
            };

            categories.ForEach(c => context.Categories.AddOrUpdate(cat => cat.Name, c));
            context.SaveChanges();

            // Criação de Carros
            var cars = new List<Car>
            {
                new Car { Model = "Corolla", Year = 2020, Color = "Preto", Price = 85000M, Description = "Confortável e econômico", BrandId = brands.Single(b => b.Name == "Toyota").BrandId, CategoryId = categories.Single(c => c.Name == "Sedan").CategoryId },
                new Car { Model = "Hilux", Year = 2021, Color = "Prata", Price = 200000M, Description = "Robusto e durável", BrandId = brands.Single(b => b.Name == "Toyota").BrandId, CategoryId = categories.Single(c => c.Name == "SUV").CategoryId },
                new Car { Model = "Civic", Year = 2019, Color = "Branco", Price = 95000M, Description = "Elegante e moderno", BrandId = brands.Single(b => b.Name == "Honda").BrandId, CategoryId = categories.Single(c => c.Name == "Sedan").CategoryId },
                new Car { Model = "HR-V", Year = 2022, Color = "Cinza", Price = 110000M, Description = "Compacto e eficiente", BrandId = brands.Single(b => b.Name == "Honda").BrandId, CategoryId = categories.Single(c => c.Name == "SUV").CategoryId },
                new Car { Model = "Mustang", Year = 2022, Color = "Vermelho", Price = 300000M, Description = "Esportivo clássico", BrandId = brands.Single(b => b.Name == "Ford").BrandId, CategoryId = categories.Single(c => c.Name == "Esportivo").CategoryId },
                new Car { Model = "Focus", Year = 2018, Color = "Azul", Price = 65000M, Description = "Econômico e espaçoso", BrandId = brands.Single(b => b.Name == "Ford").BrandId, CategoryId = categories.Single(c => c.Name == "Hatchback").CategoryId },
                new Car { Model = "Camaro", Year = 2020, Color = "Amarelo", Price = 280000M, Description = "Esportivo com design imponente", BrandId = brands.Single(b => b.Name == "Chevrolet").BrandId, CategoryId = categories.Single(c => c.Name == "Esportivo").CategoryId },
                new Car { Model = "Onix", Year = 2021, Color = "Prata", Price = 70000M, Description = "Compacto e econômico", BrandId = brands.Single(b => b.Name == "Chevrolet").BrandId, CategoryId = categories.Single(c => c.Name == "Hatchback").CategoryId },
                new Car { Model = "Golf", Year = 2019, Color = "Verde", Price = 75000M, Description = "Compacto e versátil", BrandId = brands.Single(b => b.Name == "Volkswagen").BrandId, CategoryId = categories.Single(c => c.Name == "Hatchback").CategoryId },
                new Car { Model = "Passat", Year = 2020, Color = "Preto", Price = 125000M, Description = "Conforto e tecnologia", BrandId = brands.Single(b => b.Name == "Volkswagen").BrandId, CategoryId = categories.Single(c => c.Name == "Sedan").CategoryId },
                new Car { Model = "X5", Year = 2021, Color = "Branco", Price = 350000M, Description = "Luxuoso e potente", BrandId = brands.Single(b => b.Name == "BMW").BrandId, CategoryId = categories.Single(c => c.Name == "SUV").CategoryId },
                new Car { Model = "Z4", Year = 2022, Color = "Azul", Price = 400000M, Description = "Estilo e performance em um conversível", BrandId = brands.Single(b => b.Name == "BMW").BrandId, CategoryId = categories.Single(c => c.Name == "Conversível").CategoryId }
            };

            cars.ForEach(c => context.Cars.AddOrUpdate(car => car.Model, c));
            context.SaveChanges();
        }
    }
}
