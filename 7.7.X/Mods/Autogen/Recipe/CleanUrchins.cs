/* namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Shared.Localization;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(HuntingSkill), 4)] 
    public class CleanUrchinsRecipe : Recipe
    {
        public CleanUrchinsRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawFishItem>(1),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<UrchinItem>(typeof(HuntingSkill), 4, HuntingSkill.MultiplicativeStrategy), 
            };
            this.Initialize(Localizer.DoStr("Clean Urchins"), typeof(CleanUrchinsRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(CleanUrchinsRecipe), this.UILink(), 0.2f, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
} */