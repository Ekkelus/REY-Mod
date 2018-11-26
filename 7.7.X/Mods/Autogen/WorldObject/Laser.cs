namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;


    [Serialized]
    [Weight(5000)]
    public partial class LaserItem : WorldObjectItem<LaserObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Laser"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("AVOID DIRECT EYE EXPOSURE. Needs to be placed on 3x3 reinforced concrete."); } }

        static LaserItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(15000))); } }  
    }


    [RequiresSkill(typeof(ElectronicEngineeringSkill), 4)]
    public partial class LaserRecipe : Recipe
    {
        public LaserRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LaserItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GoldIngotItem>(typeof(ElectronicEngineeringEfficiencySkill), 125, ElectronicEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(ElectronicEngineeringEfficiencySkill), 125, ElectronicEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(ElectronicEngineeringEfficiencySkill), 70, ElectronicEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<DiamondCutItem>(typeof(ElectronicEngineeringEfficiencySkill), 50, ElectronicEngineeringEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(240, ElectronicEngineeringSpeedSkill.MultiplicativeStrategy, typeof(ElectronicEngineeringSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(LaserRecipe), Item.Get<LaserItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<LaserItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Laser"), typeof(LaserRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}