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
            OrderBuilder ob = new OrderBuilder("user", "store");

            ob.StartNewPizza("large");

            Assert.True(ob.ActivePizza != null);
        }

        [Fact]
        public void StartNewPizzaShouldSetActivePizzaToAppropiateSize()
        {
            OrderBuilder ob = new OrderBuilder("user", "store");

            ob.StartNewPizza("large");

            Assert.Equal("large", ob.ActivePizza.Size);
        }

        //Testing of DuplicatePizza
        [Fact]
        public void DuplicatePizzaShouldAddExactlyOneAdditionalPizzaToPizzas()
        {
            OrderBuilder ob = new OrderBuilder("user", "store");
            ob.order.Pizzas.Add(new BuildYourOwnPizza("small"));

            ob.DuplicatePizza(0);

            Assert.True(ob.order.Pizzas.Count == 2);
        }

        [Fact]
        public void DuplicatePizzaShouldAddExactPizzaToEndOfPizzas()
        {
            OrderBuilder ob = new OrderBuilder("user", "store");
            IPizza p = new BuildYourOwnPizza("small");
            ob.order.Pizzas.Add(p);

            ob.DuplicatePizza(0);
            IPizza result = ob.order.Pizzas[ob.order.Pizzas.Count - 1];

            Assert.Equal(p, result);
        }

        [Fact]
        public void DuplicatePizzaShouldDoNothingIfIndexIsOutOfBounds()
        {
            OrderBuilder ob = new OrderBuilder("user", "store");
            IPizza p = new BuildYourOwnPizza("small");
            ob.order.Pizzas.Add(p);

            ob.DuplicatePizza(7);

            Assert.True(ob.order.Pizzas.Count == 1);
        }

        //Testing of AddPizza
        [Fact]
        public void AddPizzaShouldAddExactlyOneAdditionalPizzaToPizzas()
        {
            OrderBuilder ob = new OrderBuilder("user", "store");
            ob.order.Pizzas.Add(new BuildYourOwnPizza("small"));

            ob.AddPizza(new BuildYourOwnPizza("small"));

            Assert.True(ob.order.Pizzas.Count == 2);
        }

        [Fact]
        public void AddPizzaShouldAddExactPizzaToEndOfPizzas()
        {
            OrderBuilder ob = new OrderBuilder("user", "store");
            IPizza p = new BuildYourOwnPizza("small");
            ob.order.Pizzas.Add(new BuildYourOwnPizza("medium"));

            ob.AddPizza(p);
            IPizza result = ob.order.Pizzas[ob.order.Pizzas.Count - 1];

            Assert.Equal(result, p);
        }

        [Fact]
        public void AddPizzaShouldDoNothingIfPassedNullIPizza()
        {
            OrderBuilder ob = new OrderBuilder("user", "store");
            ob.order.Pizzas.Add(new BuildYourOwnPizza("medium"));

            ob.AddPizza(null);

            Assert.True(ob.order.Pizzas.Count == 1);
        }

        //SwitchActivePizza Testing
        [Fact]
        public void SwitchActivePizzaShouldNotLeaveActivePizzaNull()
        {
            OrderBuilder ob = new OrderBuilder("user", "store");

            ob.SwitchActivePizza(1);
            IPizza result = ob.ActivePizza;
            
            Assert.True(result != null);
        }

        [Fact]
        public void SwitchActivePizzaShouldCorrectlySetActivePizza()
        {
            OrderBuilder ob = new OrderBuilder("user", "store");
            IPizza p = new BuildYourOwnPizza("small");
            ob.order.Pizzas.Add(p);

            ob.SwitchActivePizza(0);
            IPizza result = ob.ActivePizza;

            Assert.Equal(p, result);
        }

        [Fact]
        public void SwitchActivePizzaShouldDoNothingIfIndexIsOutOfBounds()
        {
            OrderBuilder ob = new OrderBuilder("user", "store");
            IPizza p = ob.ActivePizza;
            ob.order.Pizzas.Add(p);
            
            ob.SwitchActivePizza(7);

            Assert.Equal(p, ob.ActivePizza);
        }
    }
}
