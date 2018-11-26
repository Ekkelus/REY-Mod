namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;

    [RequiresSkill(typeof(GlassworkingSkill), 1)]
    public partial class GlassJarRecipe : Recipe
    {
        public GlassJarRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GlassJarItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GlassItem>(typeof(GlassProductionEfficiencySkill), 2, GlassProductionEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(GlassJarRecipe), Item.Get<GlassJarItem>().UILink(), 2, typeof(GlassProductionSpeedSkill));
            this.Initialize(Localizer.DoStr("Glass Jar"), typeof(GlassJarRecipe));

            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }


    [Serialized]
    [Weight(100)]
    [Currency]
    public partial class GlassJarItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Glass Jar"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A glass container, capable of holding liquids."); } }

    }

}