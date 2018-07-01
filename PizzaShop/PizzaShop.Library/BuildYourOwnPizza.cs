using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaShop.Library
{
    class BuildYourOwnPizza : IPizza
    {
        public BuildYourOwnPizza (String s)
        {
            Size = s;
            CrustType = new Crust("classic crust", SizingPricingAccessor.SPM.GetIngredientUsageScalar(s));
            SauceType = new Sauce("classic sauce", SizingPricingAccessor.SPM.GetIngredientUsageScalar(s));
            Toppings.Add(new Topping("cheese", SizingPricingAccessor.SPM.GetIngredientUsageScalar(s)));
        }
    }
}
