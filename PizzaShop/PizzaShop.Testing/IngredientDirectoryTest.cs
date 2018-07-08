using PizzaShop.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PizzaShop.Testing
{
    public class IngredientDirectoryTest
    { 
        
        [Theory]
        [InlineData("brooklyn-style crust")]
        [InlineData("deep dish crust")]
        public void AddIngredientShouldAddCrustUnderProperSet(string name)
        {
            DataAccessor.Setup(false);
            Crust c = new Crust(name, 1);

            DataAccessor.DH.ingDir.AddIngredient(c);

            Assert.Contains(c.Name, DataAccessor.DH.ingDir.Crusts);
        }

        [Theory]
        [InlineData("garlic test sauce")]
        [InlineData("bbq test sauce")]
        public void AddIngredientShouldAddSauceUnderProperSet(string name)
        {
            DataAccessor.Setup(false);
            Sauce s = new Sauce(name, 1);

            DataAccessor.DH.ingDir.AddIngredient(s);

            Assert.Contains(s.Name, DataAccessor.DH.ingDir.Sauces);
        }

        [Theory]
        [InlineData("garlic")]
        [InlineData("chicken")]
        public void AddIngredientShouldAddToppingUnderProperSet(string name)
        {
            DataAccessor.Setup(false);
            Topping t = new Topping(name, 1);

            DataAccessor.DH.ingDir.AddIngredient(t);

            Assert.Contains(t.Name, DataAccessor.DH.ingDir.Toppings);
        }
    }
}
