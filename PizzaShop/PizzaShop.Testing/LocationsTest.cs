using PizzaShop.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PizzaShop.Testing
{
    public class LocationsTest
    { 

        //Testing of AddStock()
        [Fact]
        public void AddStockShouldAddNewItemToDictionaryWhenNotAlreadyInside()
        {
            // Arrange
            var location = new Location();
            var peppers = new Ingredient("peppers", 1, "topping");

            //Act
            location.AddStock(peppers);
            
            //Assert
            Assert.True(location.Stock.First(t => t.Name.Equals("peppers")).Equals(peppers));
        }

        [Fact]
        public void AddStockShouldNotAddNewItemForOneAlreadyExistingInDictionary()
        {
            // Arrange
            var location = new Location();
            var peppers1 = new Ingredient("peppers", 1, "topping");
            var peppers2 = new Ingredient("peppers", 2, "topping");
            location.AddStock(peppers1);

            //Act
            location.AddStock(peppers2);

            //Assert
            Assert.True(location.Stock.Count == 1);
        }


        [Fact]
        public void AddStockShouldIncrementQuantityOfExistingItemInDictionary()
        {
            // Arrange
            var location = new Location();
            var peppers1 = new Ingredient("peppers", 1, "topping");
            var peppers2 = new Ingredient("peppers", 2, "topping");
            location.AddStock(peppers1);

            //Act
            location.AddStock(peppers2);

            //Assert
            Assert.True(location.Stock.First(t => t.Name.Equals("peppers")).Quantity == 3);
        }


        //Testing of RemoveStock()

        [Fact]
        public void RemoveStockShouldNotCauseExceptionIfAttemptingToRemoveFromEmptyStock()
        {
            // Arrange
            var location = new Location();
            var peppers = new Ingredient("peppers", 1, "topping");

            //Act
            location.RemoveStock(peppers);

            //Assert
            Assert.True(true);//if code reaches here it didn't crash
        }

        [Fact]
        public void RemoveStockShouldNotCauseKeyNotFoundExceptionIfAttemptingToRemoveStockOfMissingItem()
        {
            // Arrange
            var location = new Location();
            var peppers = new Ingredient("peppers", 1, "topping");
            var onions = new Ingredient("onions", 7, "topping");

            //Act
            location.RemoveStock(peppers);

            //Assert
            Assert.True(true);//if code reaches here it didn't crash
        }

        [Fact]
        public void RemoveStockShouldReturnIngredientNameWhenItDoesNotExistInStock()
        {
            // Arrange
            var location = new Location();
            var peppers = new Ingredient("peppers", 1, "topping");

            //Act
            string name = location.RemoveStock(peppers);

            //Assert
            Assert.Equal(name, peppers.Name);
        }

        [Fact]
        public void RemoveStockShouldReturnIngredientNameWhenSufficientQuantityDoesNotExist()
        {
            // Arrange
            var location = new Location();
            var peppers1 = new Ingredient("peppers", 1, "topping");
            var peppers2 = new Ingredient("peppers", 2, "topping");

            //Act
            string name = location.RemoveStock(peppers2);

            //Assert
            Assert.Equal(name, peppers1.Name);
        }

        [Fact]
        public void RemoveStockShouldNotChangeIngredientQuantityWhenSufficientQuantityDoesNotExist()
        {
            // Arrange
            var location = new Location();
            var peppers1 = new Ingredient("peppers", 1, "topping");
            var peppers2 = new Ingredient("peppers", 2, "topping");
            location.AddStock(peppers1);

            //Act
            location.RemoveStock(peppers2);

            //Assert
            Assert.True(location.Stock.First(t => t.Name.Equals("peppers")).Quantity == peppers1.Quantity);
        }

        [Fact]
        public void RemoveStockShouldDecrementIngredientQuantityWhenSufficientQuantityExists()
        {
            // Arrange
            var location = new Location();
            var peppers1 = new Ingredient("peppers", 1, "topping");
            var peppers2 = new Ingredient("peppers", 2, "topping");
            location.AddStock(peppers2);

            //Act
            location.RemoveStock(peppers1);

            //Assert
            Assert.True(location.Stock.First(t => t.Name.Equals("peppers")).Quantity == 1);
        }

        [Fact]
        public void RemoveStockShouldRemoveIngredientFromStockDictionaryWhenQuantityReachesZero()
        {
            // Arrange
            var location = new Location();
            var peppers = new Ingredient("peppers", 1, "topping");
            location.AddStock(peppers);

            //Act
            location.RemoveStock(peppers);

            //Assert
            Assert.True(!(location.Stock.Any(t => t.Name.Equals(peppers.Name))));
        }



        //Testing of AddBulkStock()
        [Fact]
        public void AddBulkStockShouldAddAllItemsToInitiallyEmptyDictionary()
        {
            // Arrange
            var location = new Location();
            var ingredients = new List<Ingredient>
            {
                new Ingredient("peppers", 1, "topping"),
                new Ingredient("onions", 2, "topping"),
                new Ingredient("beef", 7, "topping"),
                new Ingredient("pepperoni", 50, "topping")
            };

            //Act
            location.AddBulkStock(ingredients);

            //Assert
            Assert.True(location.Stock.Count == ingredients.Count);
        }

        [Fact]
        public void AddBulkStockShouldOnlyAddNewItemsToDictionaryForHoweverManyAreNew()
        {
            // Arrange
            var location = new Location();
            Ingredient peppers = new Ingredient("peppers", 1, "topping");
            var ingredients = new List<Ingredient>
            {
                peppers,
                new Ingredient("onions", 2, "topping"),
                new Ingredient("beef", 7, "topping"),
                new Ingredient("pepperoni", 50, "topping")
            };
            location.AddStock(peppers);

            //Act
            location.AddBulkStock(ingredients);

            //Assert
            Assert.True(location.Stock.Count-1 == ingredients.Count-1);
        }

        [Fact]
        public void AddBulkStockShouldIncrementQuantityOfExistingItemInDictionary()
        {
            // Arrange
            var location = new Location();
            var ingredients = new List<Ingredient>
            {
                new Ingredient("peppers", 1, "topping"),
                new Ingredient("onions", 2, "topping"),
                new Ingredient("beef", 7, "topping"),
                new Ingredient("pepperoni", 50, "topping")
            };
            location.AddStock(new Ingredient("peppers", 1, "topping"));
            location.AddStock(new Ingredient("onions", 1, "topping"));
            //Act
            location.AddBulkStock(ingredients);

            //Assert
            Assert.True(location.Stock.First(t => t.Name.Equals("peppers")).Quantity==2 &&location.Stock.First(t => t.Name.Equals("onions")).Quantity ==3);
        }


        //Testing of RemoveBulkStock()
        [Fact]
        public void RemoveBulkStockShouldNotReturnNullIfDictionaryIsEmpty()
        {
            var location = new Location();
            var ingredients = new List<Ingredient>
            {
                new Ingredient("peppers", 1, "topping"),
                new Ingredient("onions", 2, "topping"),
                new Ingredient("beef", 7, "topping"),
                new Ingredient("pepperoni", 50, "topping")
            };

            string result = location.RemoveBulkStock(ingredients);

            Assert.True(!result.Equals(null));

        }

        [Fact]
        public void RemoveBulkStockShouldReturnNameOfIngredientNotPresent()
        {
            var location = new Location();
            var ingredients = new List<Ingredient>
            {
                new Ingredient("peppers", 1, "topping"),
                new Ingredient("onions", 1, "topping"),
                new Ingredient("beef", 1, "topping"),
                new Ingredient("pepperoni", 10, "topping")
            };
            location.AddStock(new Ingredient("peppers", 2, "topping"));
            location.AddStock(new Ingredient("onions", 2, "topping"));
            location.AddStock(new Ingredient("pepperoni",2, "topping"));

            String result = location.RemoveBulkStock(ingredients);

            Assert.Equal("beef", result);

        }

        [Fact]
        public void RemoveBulkStockShouldReturnNameOfIngredientLackingSufficientQuantity()
        {
            var location = new Location();
            var ingredients = new List<Ingredient>
            {
                new Ingredient("peppers", 1, "topping"),
                new Ingredient("onions", 1, "topping"),
                new Ingredient("beef", 1, "topping"),
                new Ingredient("pepperoni", 10, "topping")
            };
            location.AddStock(new Ingredient("peppers", 2, "topping"));
            location.AddStock(new Ingredient("onions", 2, "topping"));
            location.AddStock(new Ingredient("pepperoni", 2, "topping"));
            location.AddStock(new Ingredient("beef", 2, "topping"));

            String result = location.RemoveBulkStock(ingredients);

            Assert.Equal("pepperoni", result);
        }

        [Fact]
        public void RemoveBulkStockShouldNotChangeAnyQuantityIfSomeIngredientIsMissingFromDictionary()
        {
            var location = new Location();
            var ingredients = new List<Ingredient>
            {
                new Ingredient("peppers", 1, "topping"),
                new Ingredient("onions", 1, "topping"),
                new Ingredient("beef", 1, "topping"),
                new Ingredient("pepperoni", 10, "topping")
            };
            location.AddStock(new Ingredient("peppers", 2, "topping"));
            location.AddStock(new Ingredient("onions", 2, "topping"));
            location.AddStock(new Ingredient("pepperoni", 2, "topping"));

            String result = location.RemoveBulkStock(ingredients);

            Assert.True(location.Stock.First(t => t.Name.Equals("peppers")).Quantity == 2 && location.Stock.First(t => t.Name.Equals("onions")).Quantity == 2 && location.Stock.First(t => t.Name.Equals("pepperoni")).Quantity == 2);
        }

        [Fact]
        public void RemoveBulkStockShouldNotChangeAnyQuantityIfSomeIngredientIsLackingSufficientQuantity()
        {
            var location = new Location();
            var ingredients = new List<Ingredient>
            {
                new Ingredient("peppers", 1, "topping"),
                new Ingredient("onions", 1, "topping"),
                new Ingredient("beef", 1, "topping"),
                new Ingredient("pepperoni", 10, "topping")
            };
            location.AddStock(new Ingredient("peppers", 2, "topping"));
            location.AddStock(new Ingredient("onions", 2, "topping"));
            location.AddStock(new Ingredient("pepperoni", 2, "topping"));
            location.AddStock(new Ingredient("beef", 2, "topping"));

            String result = location.RemoveBulkStock(ingredients);

            Assert.True(location.Stock.First(t => t.Name.Equals("peppers")).Quantity == 2 && location.Stock.First(t => t.Name.Equals("onions")).Quantity == 2 && location.Stock.First(t => t.Name.Equals("beef")).Quantity == 2 && location.Stock.First(t => t.Name.Equals("pepperoni")).Quantity == 2);
        }

     }
}
