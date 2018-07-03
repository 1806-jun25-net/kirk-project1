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
        [InlineData("thin crust")]
        [InlineData("deep dish crust")]
        public void AddIngredientShouldAddCrustUnderProperSet(string name)
        {
            Crust c = new Crust(name, 1);

            DataAccessor.DH.ingDir.AddIngredient(c);

            Assert.Contains(c.Name, DataAccessor.DH.ingDir.Crusts);
        }

        [Theory]
        [InlineData("garlic white sauce")]
        [InlineData("bbq sauce")]
        public void AddIngredientShouldAddSauceUnderProperSet(string name)
        {
            Sauce s = new Sauce(name, 1);

            DataAccessor.DH.ingDir.AddIngredient(s);

            Assert.Contains(s.Name, DataAccessor.DH.ingDir.Sauces);
        }

        [Theory]
        [InlineData("garlic")]
        [InlineData("chicken")]
        public void AddIngredientShouldAddToppingUnderProperSet(string name)
        {
            Topping t = new Topping(name, 1);

            DataAccessor.DH.ingDir.AddIngredient(t);

            Assert.Contains(t.Name, DataAccessor.DH.ingDir.Toppings);
        }
    }
}
