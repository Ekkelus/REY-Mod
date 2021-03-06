namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Players;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;
    using Shared.Localization;

    [Serialized]
    [Weight(200)]
    public partial class SushiItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Sushi"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Raw fish with sticky rice, rolled in kelp."); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 15, Fat = 12, Protein = 15, Vitamins = 10 };
        public override float Calories { get { return 950; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 3)]
    public partial class SushiRecipe : Recipe
    {
        public SushiRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SushiItem>(),

            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SalmonFilletItem>(typeof(AdvancedCookingSkill), 10, AdvancedCookingSkill.MultiplicativeStrategy),
                new CraftingElement<TunaFilletItem>(typeof(AdvancedCookingSkill), 10, AdvancedCookingSkill.MultiplicativeStrategy),
                new CraftingElement<TroutFilletItem>(typeof(AdvancedCookingSkill), 10, AdvancedCookingSkill.MultiplicativeStrategy),
                new CraftingElement<ClamItem>(typeof(AdvancedCookingSkill), 4, AdvancedCookingSkill.MultiplicativeStrategy),
                new CraftingElement<UrchinItem>(typeof(AdvancedCookingSkill), 4, AdvancedCookingSkill.MultiplicativeStrategy),
                new CraftingElement<KelpItem>(typeof(AdvancedCookingSkill), 4, AdvancedCookingSkill.MultiplicativeStrategy),
                new CraftingElement<RiceItem>(typeof(AdvancedCookingSkill), 20, AdvancedCookingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(SushiRecipe), Item.Get<SushiItem>().UILink(), 15, typeof(AdvancedCookingSkill), typeof(AdvancedCookingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Sushi"), typeof(SushiRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}