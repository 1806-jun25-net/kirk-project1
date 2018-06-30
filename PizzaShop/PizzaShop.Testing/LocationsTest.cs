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
        public void AddStockShouldAddItemToDictionary()
        {
            // Arrange
            var location = new Location();
            var peppers = new Topping("peppers", 1);

            //Act
            location.AddStock(peppers);
            
            //Assert
            Assert.True(location.Stock["peppers"].Equals(peppers));
        }
    }
}
