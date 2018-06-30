using PizzaShop.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PizzaShop.Testing
{
    public class LocationsTest
    {
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
    }
}
