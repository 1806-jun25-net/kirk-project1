using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PizzaShop.Library;

namespace PizzaShop.Testing
{
    public class OrderBuilderTest
    {
        //Testing of StartNewPizza()
        [Fact]
        public void StartNewPizzaShouldSetActivePizzaToANonNullPizza()
        {
            DataAccessor.Setup(false, false);
            OrderBuilder ob = new OrderBuilder("user", "store");

            ob.StartNewPizza("large");

            Assert.True(ob.ActivePizza != null);
        }

        [Fact]
        public void StartNewPizzaShouldSetActivePizzaToAppropiateSize()
        {
            DataAccessor.Setup(false, false);
            OrderBuilder ob = new OrderBuilder("user", "store");

            ob.StartNewPizza("large");

            Assert.Equal("large", ob.ActivePizza.Size);
        }

        //Testing of DuplicatePizza
        [Fact]
        public void DuplicatePizzaShouldAddExactlyOneAdditionalPizzaToPizzas()
        {
            DataAccessor.Setup(false, false);
            OrderBuilder ob = new OrderBuilder("user", "store");
            ob.order.Pizzas.Add(new Pizza("small"));

            ob.DuplicatePizza(0);

            Assert.True(ob.order.Pizzas.Count == 2);
        }

        [Fact]
        public void DuplicatePizzaShouldAddExactPizzaToEndOfPizzas()
        {
            DataAccessor.Setup(false, false);
            OrderBuilder ob = new OrderBuilder("user", "store");
            Pizza p = new Pizza("small");
            ob.order.Pizzas.Add(p);

            ob.DuplicatePizza(0);
            Pizza result = ob.order.Pizzas[ob.order.Pizzas.Count - 1];

            Assert.Equal(p, result);
        }

        [Fact]
        public void DuplicatePizzaShouldDoNothingIfIndexIsOutOfBounds()
        {
            DataAccessor.Setup(false, false);
            OrderBuilder ob = new OrderBuilder("user", "store");
            Pizza p = new Pizza("small");
            ob.order.Pizzas.Add(p);

            ob.DuplicatePizza(7);

            Assert.True(ob.order.Pizzas.Count == 1);
        }

        //Testing of AddPizza
        [Fact]
        public void AddPizzaShouldAddExactlyOneAdditionalPizzaToPizzas()
        {
            DataAccessor.Setup(false, false);
            OrderBuilder ob = new OrderBuilder("user", "store");
            ob.order.Pizzas.Add(new Pizza("small"));

            ob.AddPizza(new Pizza("small"));

            Assert.True(ob.order.Pizzas.Count == 2);
        }

        [Fact]
        public void AddPizzaShouldAddExactPizzaToEndOfPizzas()
        {
            DataAccessor.Setup(false, false);
            OrderBuilder ob = new OrderBuilder("user", "store");
            Pizza p = new Pizza("small");
            ob.order.Pizzas.Add(new Pizza("medium"));

            ob.AddPizza(p);
            Pizza result = ob.order.Pizzas[ob.order.Pizzas.Count - 1];

            Assert.Equal(result, p);
        }

        [Fact]
        public void AddPizzaShouldDoNothingIfPassedNullIPizza()
        {
            DataAccessor.Setup(false, false);
            OrderBuilder ob = new OrderBuilder("user", "store");
            ob.order.Pizzas.Add(new Pizza("medium"));

            ob.AddPizza(null);

            Assert.True(ob.order.Pizzas.Count == 1);
        }

        //SwitchActivePizza Testing
        [Fact]
        public void SwitchActivePizzaShouldNotLeaveActivePizzaNull()
        {
            DataAccessor.Setup(false, false);
            OrderBuilder ob = new OrderBuilder("user", "store");
            ob.ActivePizza = new Pizza("small");

            ob.SwitchActivePizza(1);
            Pizza result = ob.ActivePizza;
            
            Assert.True(result != null);
        }

        [Fact]
        public void SwitchActivePizzaShouldCorrectlySetActivePizza()
        {
            DataAccessor.Setup(false, false);
            OrderBuilder ob = new OrderBuilder("user", "store");
            Pizza p = new Pizza("small");
            ob.order.Pizzas.Add(p);

            ob.SwitchActivePizza(0);
            Pizza result = ob.ActivePizza;

            Assert.Equal(p, result);
        }

        [Fact]
        public void SwitchActivePizzaShouldDoNothingIfIndexIsOutOfBounds()
        {
            DataAccessor.Setup(false, false);
            OrderBuilder ob = new OrderBuilder("user", "store");
            Pizza p = ob.ActivePizza;
            ob.order.Pizzas.Add(p);
            
            ob.SwitchActivePizza(7);

            Assert.Equal(p, ob.ActivePizza);
        }
    }
}
