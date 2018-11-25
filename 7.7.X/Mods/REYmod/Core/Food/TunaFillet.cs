﻿namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Serialization;
	using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;

    [Serialized]
    [Weight(200)]
    public partial class TunaFilletItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tuna Fillet"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Some fine tuna Fillet​."); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 0 , Fat = 5, Protein = 12, Vitamins = 4 };
        public override float Calories { get { return 500; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(FishingSkill), 3)]
    public partial class TunaFilletRecipe : Recipe
    {
        public TunaFilletRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TunaFilletItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
				new CraftingElement<TunaItem>(typeof(FishCleaningEfficiencySkill), 1, FishCleaningEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(TunaFilletRecipe), Item.Get<TunaFilletItem>().UILink(), 3, typeof(FishCleaningSpeedSkill));
            this.Initialize("Tuna Fillet", typeof(TunaFilletRecipe));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
}