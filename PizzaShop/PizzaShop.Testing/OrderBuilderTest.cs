using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PizzaShop.Library;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;

namespace PizzaShop.Testing
{
    public class OrderBuilderTest
    {
        //Testing of StartNewPizza()
        [Fact]
        public void StartNewPizzaShouldSetActivePizzaToANonNullPizza()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<Project1DBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Project1"));
            var RH = (new RepositoryHandler(new Project1DBContext(optionsBuilder.Options)));
            OrderBuilder ob = new OrderBuilder("user", "store", RH);

            ob.StartNewPizza("large");

            Assert.True(ob.ActivePizza != null);
        }

        [Theory]
        [InlineData("small")]
        [InlineData("medium")]
        [InlineData("large")]
        public void StartNewPizzaShouldSetActivePizzaToAppropiateSize(string s)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<Project1DBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Project1"));
            var RH = (new RepositoryHandler(new Project1DBContext(optionsBuilder.Options)));
            OrderBuilder ob = new OrderBuilder("user", "store", RH);

            ob.StartNewPizza(s);

            Assert.Equal(s, ob.ActivePizza.Size);
        }

        //Testing of DuplicatePizza
        [Fact]
        public void DuplicatePizzaShouldAddExactlyOneAdditionalPizzaToPizzas()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<Project1DBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Project1"));
            var RH = (new RepositoryHandler(new Project1DBContext(optionsBuilder.Options)));
            OrderBuilder ob = new OrderBuilder("user", "store", RH);
            ob.order.Pizzas.Add(new Pizza("small"));

            ob.DuplicatePizza(0);

            Assert.True(ob.order.Pizzas.Count == 2);
        }

        [Fact]
        public void DuplicatePizzaShouldAddExactPizzaToEndOfPizzas()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<Project1DBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Project1"));
            var RH = (new RepositoryHandler(new Project1DBContext(optionsBuilder.Options)));
            OrderBuilder ob = new OrderBuilder("user", "store", RH);

            Pizza p = new Pizza("small");
            ob.order.Pizzas.Add(p);

            ob.DuplicatePizza(0);
            Pizza result = ob.order.Pizzas[ob.order.Pizzas.Count - 1];

            Assert.Equal(p, result);
        }

        [Fact]
        public void DuplicatePizzaShouldDoNothingIfIndexIsOutOfBounds()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<Project1DBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Project1"));
            var RH = (new RepositoryHandler(new Project1DBContext(optionsBuilder.Options)));
            OrderBuilder ob = new OrderBuilder("user", "store", RH);
            Pizza p = new Pizza("small");
            ob.order.Pizzas.Add(p);

            ob.DuplicatePizza(7);

            Assert.True(ob.order.Pizzas.Count == 1);
        }

        //Testing of AddPizza
        [Fact]
        public void AddPizzaShouldAddExactlyOneAdditionalPizzaToPizzas()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<Project1DBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Project1"));
            var RH = (new RepositoryHandler(new Project1DBContext(optionsBuilder.Options)));
            OrderBuilder ob = new OrderBuilder("user", "store", RH);
            ob.order.Pizzas.Add(new Pizza("small"));

            ob.AddPizza(new Pizza("small"));

            Assert.True(ob.order.Pizzas.Count == 2);
        }

        [Fact]
        public void AddPizzaShouldAddExactPizzaToEndOfPizzas()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<Project1DBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Project1"));
            var RH = (new RepositoryHandler(new Project1DBContext(optionsBuilder.Options)));
            OrderBuilder ob = new OrderBuilder("user", "store", RH);
            Pizza p = new Pizza("small");
            ob.order.Pizzas.Add(new Pizza("medium"));

            ob.AddPizza(p);
            Pizza result = ob.order.Pizzas[ob.order.Pizzas.Count - 1];

            Assert.Equal(result, p);
        }

        [Fact]
        public void AddPizzaShouldDoNothingIfPassedNullIPizza()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<Project1DBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Project1"));
            var RH = (new RepositoryHandler(new Project1DBContext(optionsBuilder.Options)));
            OrderBuilder ob = new OrderBuilder("user", "store", RH);
            ob.order.Pizzas.Add(new Pizza("medium"));

            ob.AddPizza(null);

            Assert.True(ob.order.Pizzas.Count == 1);
        }

        //SwitchActivePizza Testing
        [Fact]
        public void SwitchActivePizzaShouldNotLeaveActivePizzaNull()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<Project1DBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Project1"));
            var RH = (new RepositoryHandler(new Project1DBContext(optionsBuilder.Options)));
            OrderBuilder ob = new OrderBuilder("user", "store", RH);
            ob.ActivePizza = new Pizza("small");

            ob.SwitchActivePizza(1);
            Pizza result = ob.ActivePizza;
            
            Assert.True(result != null);
        }

        [Fact]
        public void SwitchActivePizzaShouldCorrectlySetActivePizza()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<Project1DBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Project1"));
            var RH = (new RepositoryHandler(new Project1DBContext(optionsBuilder.Options)));
            OrderBuilder ob = new OrderBuilder("user", "store", RH);
            Pizza p = new Pizza("small");
            ob.order.Pizzas.Add(p);

            ob.SwitchActivePizza(0);
            Pizza result = ob.ActivePizza;

            Assert.Equal(p, result);
        }

        [Fact]
        public void SwitchActivePizzaShouldDoNothingIfIndexIsOutOfBounds()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<Project1DBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Project1"));
            var RH = (new RepositoryHandler(new Project1DBContext(optionsBuilder.Options)));
            OrderBuilder ob = new OrderBuilder("user", "store", RH);
            Pizza p = ob.ActivePizza;
            ob.order.Pizzas.Add(p);
            
            ob.SwitchActivePizza(7);

            Assert.Equal(p, ob.ActivePizza);
        }
    }
}
