namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;
    using Eco.World;
    using Eco.World.Blocks;

    [RequiresSkill(typeof(RoadConstructionSkill), 3)]   
    public partial class AsphaltRoadRecipe : Recipe
    {
        public AsphaltRoadRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<AsphaltRoadItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ConcreteItem>(typeof(RoadConstructionEfficiencySkill), 1, RoadConstructionEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<StoneRoadItem>(typeof(RoadConstructionEfficiencySkill), 1, RoadConstructionEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PetroleumItem>(typeof(RoadConstructionEfficiencySkill), 1, RoadConstructionEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(AsphaltRoadRecipe), Item.Get<AsphaltRoadItem>().UILink(), 1, typeof(RoadConstructionSkill));    
            this.Initialize(Localizer.DoStr("Asphalt Road"), typeof(AsphaltRoadRecipe));

            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
        }
    }

    [Serialized]
    [Solid, Wall, Constructed]
    [Road(1)]                                          
    [UsesRamp(typeof(AsphaltRoadWorldObjectBlock))]              
    [RequiresSkill(typeof(RoadConstructionEfficiencySkill), 3)]   
    public partial class AsphaltRoadBlock :
        Block           
    { }

    [Serialized]
    [MaxStackSize(10)]                                      
    [Weight(10000)]      
    [MakesRoads]                                            
    public partial class AsphaltRoadItem :
    RoadItem<AsphaltRoadBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Asphalt Road"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A paved surface constructed with asphalt and concrete. It's durable and extremely efficient for any wheeled vehicle."); } }

    }

}