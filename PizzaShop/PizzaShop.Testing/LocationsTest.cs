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
        public void AddStockShouldAddNewItemToDictionaryWhenNotAlreadyInSide()
        {
            // Arrange
            var location = new Location();
            var peppers = new Topping("peppers", 1);;

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
            location.AddStock(peppers1);

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

    }
}
