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

    [RequiresSkill(typeof(HuntingSkill), 3)] 
    public class CleanTunaRecipe : Recipe
    {
        public CleanTunaRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawFishItem>(10),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TunaItem>(typeof(HuntingSkill), 1, HuntingSkill.MultiplicativeStrategy), 
            };
            this.Initialize(Localizer.DoStr("Clean Tuna"), typeof(CleanTunaRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(CleanTunaRecipe), this.UILink(), 2, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
} */