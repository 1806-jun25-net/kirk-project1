using PizzaShop.Library;
using System;
using System.Collections.Generic;
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
            var peppers = new Topping("peppers", 1);

            //Act
            location.AddStock(peppers);
            
            //Assert
            Assert.True(location.Stock["peppers"].Equals(peppers));
        }

        [Fact]
        public void AddStockShouldNotAddNewItemForOneAlreadyExistingInDictionary()
        {
            // Arrange
            var location = new Location();
            var peppers1 = new Topping("peppers", 1);
            var peppers2 = new Topping("peppers", 2);
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
            var peppers1 = new Topping("peppers", 1);
            var peppers2 = new Topping("peppers", 2);
            location.AddStock(peppers1);

            //Act
            location.AddStock(peppers2);

            //Assert
            Assert.True(location.Stock["peppers"].Quantity == 3);
        }


        //Testing of RemoveStock()

        [Fact]
        public void RemoveStockShouldNotCauseExceptionIfAttemptingToRemoveFromEmptyStock()
        {
            // Arrange
            var location = new Location();
            var peppers = new Topping("peppers", 1);

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
            var peppers = new Topping("peppers", 1);
            var onions = new Topping("onions", 7);

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
            var peppers = new Topping("peppers", 1);

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
            var peppers1 = new Topping("peppers", 1);
            var peppers2 = new Topping("peppers", 2);

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
            var peppers1 = new Topping("peppers", 1);
            var peppers2 = new Topping("peppers", 2);
            location.AddStock(peppers1);

            //Act
            location.RemoveStock(peppers2);

            //Assert
            Assert.True(location.Stock["peppers"].Quantity == peppers1.Quantity);
        }

        [Fact]
        public void RemoveStockShouldDecrementIngredientQuantityWhenSufficientQuantityExists()
        {
            // Arrange
            var location = new Location();
            var peppers1 = new Topping("peppers", 1);
            var peppers2 = new Topping("peppers", 2);
            location.AddStock(peppers2);

            //Act
            location.RemoveStock(peppers1);

            //Assert
            Assert.True(location.Stock["peppers"].Quantity == 1);
        }

        [Fact]
        public void RemoveStockShouldRemoveIngredientFromStockDictionaryWhenQuantityReachesZero()
        {
            // Arrange
            var location = new Location();
            var peppers = new Topping("peppers", 1);
            location.AddStock(peppers);

            //Act
            location.RemoveStock(peppers);

            //Assert
            Assert.True(!(location.Stock.ContainsKey(peppers.Name)));
        }



        //Testing of AddBulkStock()
        [Fact]
        public void AddBulkStockShouldAddAllItemsToInitiallyEmptyDictionary()
        {
            // Arrange
            var location = new Location();
            var ingredients = new List<IIngredient>
            {
                new Topping("peppers", 1),
                new Topping("onions", 2),
                new Topping("beef", 7),
                new Topping("pepperoni", 50)
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
            IIngredient peppers = new Topping("peppers", 1);
            var ingredients = new List<IIngredient>
            {
                peppers,
                new Topping("onions", 2),
                new Topping("beef", 7),
                new Topping("pepperoni", 50)
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
            var ingredients = new List<IIngredient>
            {
                new Topping("peppers", 1),
                new Topping("onions", 2),
                new Topping("beef", 7),
                new Topping("pepperoni", 50)
            };
            location.AddStock(new Topping("peppers", 1));
            location.AddStock(new Topping("onions", 1));
            //Act
            location.AddBulkStock(ingredients);

            //Assert
            Assert.True(location.Stock["peppers"].Quantity==2 &&location.Stock["onions"].Quantity ==3);
        }


        //Testing of RemoveBulkStock()
        [Fact]
        public void RemoveBulkStockShouldNotReturnNullIfDictionaryIsEmpty()
        {
            var location = new Location();
            var ingredients = new List<IIngredient>
            {
                new Topping("peppers", 1),
                new Topping("onions", 2),
                new Topping("beef", 7),
                new Topping("pepperoni", 50)
            };

            string result = location.RemoveBulkStock(ingredients);

            Assert.True(!result.Equals(null));

        }

        [Fact]
        public void RemoveBulkStockShouldReturnNameOfIngredientNotPresent()
        {
            var location = new Location();
            var ingredients = new List<IIngredient>
            {
                new Topping("peppers", 1),
                new Topping("onions", 1),
                new Topping("beef", 1),
                new Topping("pepperoni", 10)
            };
            location.AddStock(new Topping("peppers", 2));
            location.AddStock(new Topping("onions", 2));
            location.AddStock(new Topping("pepperoni",2));

            String result = location.RemoveBulkStock(ingredients);

            Assert.Equal("beef", result);

        }

        [Fact]
        public void RemoveBulkStockShouldReturnNameOfIngredientLackingSufficientQuantity()
        {
            var location = new Location();
            var ingredients = new List<IIngredient>
            {
                new Topping("peppers", 1),
                new Topping("onions", 1),
                new Topping("beef", 1),
                new Topping("pepperoni", 10)
            };
            location.AddStock(new Topping("peppers", 2));
            location.AddStock(new Topping("onions", 2));
            location.AddStock(new Topping("pepperoni", 2));
            location.AddStock(new Topping("beef", 2));

            String result = location.RemoveBulkStock(ingredients);

            Assert.Equal("pepperoni", result);
        }

        [Fact]
        public void RemoveBulkStockShouldNotChangeAnyQuantityIfSomeIngredientIsMissingFromDictionary()
        {
            var location = new Location();
            var ingredients = new List<IIngredient>
            {
                new Topping("peppers", 1),
                new Topping("onions", 1),
                new Topping("beef", 1),
                new Topping("pepperoni", 10)
            };
            location.AddStock(new Topping("peppers", 2));
            location.AddStock(new Topping("onions", 2));
            location.AddStock(new Topping("pepperoni", 2));

            String result = location.RemoveBulkStock(ingredients);

            Assert.True(location.Stock["peppers"].Quantity == 2 && location.Stock["onions"].Quantity == 2 && location.Stock["pepperoni"].Quantity == 2);
        }

        [Fact]
        public void RemoveBulkStockShouldNotChangeAnyQuantityIfSomeIngredientIsLackingSufficientQuantity()
        {
            var location = new Location();
            var ingredients = new List<IIngredient>
            {
                new Topping("peppers", 1),
                new Topping("onions", 1),
                new Topping("beef", 1),
                new Topping("pepperoni", 10)
            };
            location.AddStock(new Topping("peppers", 2));
            location.AddStock(new Topping("onions", 2));
            location.AddStock(new Topping("pepperoni", 2));
            location.AddStock(new Topping("beef", 2));

            String result = location.RemoveBulkStock(ingredients);

            Assert.True(location.Stock["peppers"].Quantity == 2 && location.Stock["onions"].Quantity == 2 && location.Stock["beef"].Quantity == 2 && location.Stock["pepperoni"].Quantity == 2);
        }

     }
}
