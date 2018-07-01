using PizzaShop.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PizzaShop.Testing
{
    public class IngredientDirectoryAccessorTest
    {

        
        [Fact]
        public void IngredientDirectoryAccessorActuallyMakesAIngredientDirectoryInstance()
        { 

            Assert.True(!(IngredientDirectoryAccessor.ingDir.Equals(null)));
        }
        
        [Theory]
        [InlineData("thin crust")]
        [InlineData("deep dish crust")]
        public void AddIngredientShouldAddCrustUnderProperSet(string name)
        {
            Crust c = new Crust(name, 1);

            IngredientDirectoryAccessor.ingDir.AddIngredient(c);

            Assert.Contains(c.Name, IngredientDirectoryAccessor.ingDir.Crusts);
        }

        [Theory]
        [InlineData("garlic white sauce")]
        [InlineData("bbq sauce")]
        public void AddIngredientShouldAddSauceUnderProperSet(string name)
        {
            Sauce s = new Sauce(name, 1);

            IngredientDirectoryAccessor.ingDir.AddIngredient(s);

            Assert.Contains(s.Name, IngredientDirectoryAccessor.ingDir.Sauces);
        }

        [Theory]
        [InlineData("garlic")]
        [InlineData("chicken")]
        public void AddIngredientShouldAddToppingUnderProperSet(string name)
        {
            Topping t = new Topping(name, 1);

            IngredientDirectoryAccessor.ingDir.AddIngredient(t);

            Assert.Contains(t.Name, IngredientDirectoryAccessor.ingDir.Toppings);
        }
    }
}
