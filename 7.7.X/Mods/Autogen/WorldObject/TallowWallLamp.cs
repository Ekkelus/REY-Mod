namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;

    [Serialized]    
    [RequireComponent(typeof(OnOffComponent))]                   
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(FuelSupplyComponent))]                      
    [RequireComponent(typeof(FuelConsumptionComponent))]                 
    [RequireComponent(typeof(HousingComponent))]                  
    public partial class TallowWallLampObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tallow Wall Lamp"); } } 

        public virtual Type RepresentedItemType { get { return typeof(TallowWallLampItem); } } 

        private static Type[] fuelTypeList = new Type[]
        {
            typeof(TallowItem),
            typeof(OilItem),
            typeof(BeeswaxItem),
        };

        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Lights");                                 
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            this.GetComponent<FuelConsumptionComponent>().Initialize(0.2f);                    
            this.GetComponent<HousingComponent>().Set(TallowWallLampItem.HousingVal);
            this.GetComponent<PropertyAuthComponent>().Initialize();



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(500)]
    public partial class TallowWallLampItem : WorldObjectItem<TallowWallLampObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tallow Wall Lamp"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A candle mounted on a wall bracket which can burn tallow to produce a small amount of light."); } }

        static TallowWallLampItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Lights", 
                                                    DiminishingReturnPercent = 0.8f    
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(1))); } } 
    }


    [RequiresSkill(typeof(MetalworkingSkill), 2)]
    public partial class TallowWallLampRecipe : Recipe
    {
        public TallowWallLampRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TallowWallLampItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(MetalworkingEfficiencySkill), 5, MetalworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<TallowItem>(typeof(MetalworkingEfficiencySkill), 2, MetalworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(1, MetalworkingSpeedSkill.MultiplicativeStrategy, typeof(MetalworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(TallowWallLampRecipe), Item.Get<TallowWallLampItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<TallowWallLampItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Tallow Wall Lamp"), typeof(TallowWallLampRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}