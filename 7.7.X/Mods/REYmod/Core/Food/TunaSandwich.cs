namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;
    using Eco.Shared.Localization;

    [Serialized]
    [Weight(200)]
    public partial class TunaSandwichItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tuna Sandwich"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Tuna Sandwich! Delicious!"); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 10, Fat = 12, Protein = 13, Vitamins =9 };
        public override float Calories { get { return 800; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(CulinaryArtsSkill), 1)]
    public partial class TunaSandwichRecipe : Recipe
    {
        public TunaSandwichRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TunaSandwichItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TunaFilletItem>(typeof(CulinaryArtsEfficiencySkill), 5, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BreadItem>(typeof(CulinaryArtsEfficiencySkill), 1, CulinaryArtsEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(TunaSandwichRecipe), Item.Get<TunaSandwichItem>().UILink(), 10, typeof(CulinaryArtsSpeedSkill));
            this.Initialize("Tuna Sandwich", typeof(TunaSandwichRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}