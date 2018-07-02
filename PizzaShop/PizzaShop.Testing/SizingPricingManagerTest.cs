using PizzaShop.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PizzaShop.Testing
{
    public class SizingPricingManagerTest
    {
        //Testing for AddNewSize method
        [Theory]
        [InlineData("medium-small", 5.5, .5)]
        [InlineData("medium-large", 11.1, .75)]
        public void AddNewSizeShouldAddSizeDataToSizes(string name, decimal bPrice, decimal tPrice)
        {
            var mngr = new SizingPricingManager();

            mngr.AddNewSize(name, bPrice, tPrice);

            Assert.Contains(name, mngr.Sizes);
        }

        [Theory]
        [InlineData("medium-small", 5.5, .5)]
        [InlineData("medium-large", 11.1, .75)]
        public void AddNewSizeShouldAddSizeDataToBasePrices(string name, decimal bPrice, decimal tPrice)
        {
            var mngr = new SizingPricingManager();

            mngr.AddNewSize(name, bPrice, tPrice);

            Assert.Equal(mngr.GetBasePrice(name), bPrice);
        }

        [Theory]
        [InlineData("medium-small", 5.5, .5)]
        [InlineData("medium-large", 11.1, .75)]
        public void AddNewSizeShouldAddSizeDataToToppingPrices(string name, decimal bPrice, decimal tPrice)
        {
            var mngr = new SizingPricingManager();

            mngr.AddNewSize(name, bPrice, tPrice);

            Assert.Equal(mngr.GetToppingPrice(name), tPrice);
        }

        [Theory]
        [InlineData("small", 5.5, .5)]
        [InlineData("large", 11.1, .75)]
        public void AddNewSizeShouldDoNothingIfNameAlreadyExistsInSizes(string name, decimal bPrice, decimal tPrice)
        {
            var mngr = new SizingPricingManager();
            mngr.AddNewSize(name, bPrice, tPrice);
            var originalSizes = mngr.Sizes;

            mngr.AddNewSize(name, bPrice, tPrice);

            Assert.True(mngr.Sizes == originalSizes);
        }

        [Theory]
        [InlineData("medium-small", 0, .5)]
        [InlineData("medium-large", -1.2, .75)]
        public void AddNewSizeShouldDoNothingIfBasePriceIsInvalid(string name, decimal bPrice, decimal tPrice)
        {
            var mngr = new SizingPricingManager();
            var originalSizes = mngr.Sizes;

            mngr.AddNewSize(name, bPrice, tPrice);

            Assert.True(mngr.Sizes == originalSizes);
        }

        [Theory]
        [InlineData("medium-small", 5.5, 0)]
        [InlineData("medium-large", 11.1, -1.2)]
        public void AddNewSizeShouldDoNothingIfIngredientPriceIsInvalid(string name, decimal bPrice, decimal tPrice)
        {
            var mngr = new SizingPricingManager();
            var originalSizes = mngr.Sizes;

            mngr.AddNewSize(name, bPrice, tPrice);

            Assert.True(mngr.Sizes == originalSizes);
        }

        //testing RemoveSize method
        [Theory]
        [InlineData("medium-small", 5.5, .5)]
        [InlineData("medium-large", 11.1, .75)]
        public void RemoveSizeShouldDoNothingIfSizeIsNotInSizes(string name, decimal bPrice, decimal tPrice)
        {
            var mngr = new SizingPricingManager();
            var originalSizes = mngr.Sizes;

            mngr.RemoveSize(name);
            var result = mngr.Sizes;

            Assert.Equal(originalSizes, result);
        }

        [Theory]
        [InlineData("medium-small", 5.5, .5)]
        [InlineData("medium-large", 11.1, .75)]
        public void RemoveSizeShouldRemoveFromSizesWhenSizeIsValid(string name, decimal bPrice, decimal tPrice)
        {
            var mngr = new SizingPricingManager();
            mngr.AddNewSize(name, bPrice, tPrice);

            mngr.RemoveSize(name);

            Assert.True(!mngr.Sizes.Contains(name));
        }

        [Theory]
        [InlineData("small", 5.5, .5)]
        [InlineData("large", 11.1, .75)]
        public void RemoveSizeShouldRemoveFromBasePriceWhenSizeIsValid(string name, decimal bPrice, decimal tPrice)
        {
            var mngr = new SizingPricingManager();
            mngr.AddNewSize(name, bPrice, tPrice);

            mngr.RemoveSize(name);

            Assert.True(mngr.GetBasePrice(name) == -1);
        }

        [Theory]
        [InlineData("small", 5.5, .5)]
        [InlineData("large", 11.1, .75)]
        public void RemoveSizeShouldRemoveFromToppingPriceWhenSizeIsValid(string name, decimal bPrice, decimal tPrice)
        {
            var mngr = new SizingPricingManager();
            mngr.AddNewSize(name, bPrice, tPrice);

            mngr.RemoveSize(name);

            Assert.True(mngr.GetToppingPrice(name) == -1);
        }

        // GetBasePriceTesting
        [Theory]
        [InlineData("medium-small")]
        [InlineData("medium-large")]
        public void GetBasePriceShouldReturnNeg1IfNameNotFound(string name)
        {
            var mngr = new SizingPricingManager();

            var result = mngr.GetBasePrice(name);

            Assert.True(result == -1);
        }

        [Theory]
        [InlineData("medium-small", 5.5, .5)]
        [InlineData("medium-large", 11.1, .75)]
        public void GetBasePriceShouldReturnBasePriceWhenItemFound(string name, decimal bPrice, decimal tPrice)
        {
            var mngr = new SizingPricingManager();
            mngr.AddNewSize(name, bPrice, tPrice);

            var result = mngr.GetBasePrice(name);

            Assert.True(result == bPrice);
        }

        // GetToppingPriceTesting
        [Theory]
        [InlineData("medium-small")]
        [InlineData("medium-large")]
        public void GetToppingPriceShouldReturnNeg1IfNameNotFound(string name)
        {
            var mngr = new SizingPricingManager();

            var result = mngr.GetBasePrice(name);

            Assert.True(mngr.GetBasePrice(name) == -1);
        }

        [Theory]
        [InlineData("medium-small", 5.5, .5)]
        [InlineData("medium-large", 11.1, .75)]
        public void GetToppingPriceShouldReturnToppingPriceWhenItemFound(string name, decimal bPrice, decimal tPrice)
        {
            var mngr = new SizingPricingManager();
            mngr.AddNewSize(name, bPrice, tPrice);

            var result = mngr.GetToppingPrice(name);

            Assert.True(result == tPrice);
        }
    }
}
