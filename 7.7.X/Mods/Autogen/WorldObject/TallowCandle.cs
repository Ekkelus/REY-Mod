namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;

    [Serialized]    
    [RequireComponent(typeof(OnOffComponent))]                   
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(FuelSupplyComponent))]                      
    [RequireComponent(typeof(FuelConsumptionComponent))]                 
    [RequireComponent(typeof(HousingComponent))]                  
    public partial class TallowCandleObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tallow Candle"); } } 

        public virtual Type RepresentedItemType { get { return typeof(TallowCandleItem); } } 

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
            this.GetComponent<HousingComponent>().Set(TallowCandleItem.HousingVal);
            this.GetComponent<PropertyAuthComponent>().Initialize();



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(250)]
    public partial class TallowCandleItem : WorldObjectItem<TallowCandleObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tallow Candle"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A candle which can burn tallow to produce a small amount of light."); } }

        static TallowCandleItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 1,                                   
                                                    TypeForRoomLimit = "Lights", 
                                                    DiminishingReturnPercent = 0.8f    
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(1))); } } 
    }


    public partial class TallowCandleRecipe : Recipe
    {
        public TallowCandleRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TallowCandleItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TallowItem>(3),   
            };
            this.CraftMinutes = new ConstantValue(2.5f);
            this.Initialize(Localizer.DoStr("Tallow Candle"), typeof(TallowCandleRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}