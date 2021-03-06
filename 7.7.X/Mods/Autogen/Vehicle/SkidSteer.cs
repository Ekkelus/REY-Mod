namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]
    [Weight(25000)]  
    public class SkidSteerItem : WorldObjectItem<SkidSteerObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skid Steer"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A WHAT?"); } }
    }

    [RequiresSkill(typeof(IndustrySkill), 1)] 
    public class SkidSteerRecipe : Recipe
    {
        public SkidSteerRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SkidSteerItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<AdvancedCombustionEngineItem>(),
                new CraftingElement<RubberWheelItem>(4),
                new CraftingElement<RadiatorItem>(2),
                new CraftingElement<SteelAxleItem>(), 
                new CraftingElement<GearboxItem>(typeof(IndustrySkill), 10, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<CelluloseFiberItem>(typeof(IndustrySkill), 20, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(IndustrySkill), 40, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(IndustrySkill), 16, IndustrySkill.MultiplicativeStrategy),
            };
            CraftMinutes = new ConstantValue(25);

            Initialize(Localizer.DoStr("Skid Steer"), typeof(SkidSteerRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }

}