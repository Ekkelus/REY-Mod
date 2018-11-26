namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;

    [Serialized]    
    [RequireComponent(typeof(PipeComponent))]                    
    [RequireComponent(typeof(AttachmentComponent))]              
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(FuelSupplyComponent))]                      
    [RequireComponent(typeof(FuelConsumptionComponent))]                 
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(45)]                              
    [RequireRoomMaterialTier(1.9f)]        
    public partial class CementKilnObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cement Kiln"); } } 

        public virtual Type RepresentedItemType { get { return typeof(CementKilnItem); } } 

        private static Type[] fuelTypeList = new Type[]
        {
            typeof(LogItem),
            typeof(LumberItem),
            typeof(CharcoalItem),
            typeof(ArrowItem),
            typeof(BoardItem),
            typeof(CoalItem),
        };

        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Crafting");                                 
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            this.GetComponent<FuelConsumptionComponent>().Initialize(50);                    
            this.GetComponent<HousingComponent>().Set(CementKilnItem.HousingVal);
            this.GetComponent<PropertyAuthComponent>().Initialize();


            var tankList = new List<LiquidTank>();
            
            tankList.Add(new LiquidProducer("Chimney", typeof(SmogItem), 100,
                    null,                                                                
                    this.Occupancy.Find(x => x.Name == "ChimneyOut"),   
                        (float)(0.8f * SmogItem.SmogItemsPerCO2PPM) / TimeUtil.SecondsPerHour)); 
            
            
            
            this.GetComponent<PipeComponent>().Initialize(tankList);

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class CementKilnItem : WorldObjectItem<CementKilnObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cement Kiln"); } } 
        public override string Description  { get { return  "A rotary kiln that produces cement and concrete products."; } }

        static CementKilnItem()
        {
            
            
            
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(50))); } } 
    }


    [RequiresSkill(typeof(MetalworkingSkill), 1)]
    public partial class CementKilnRecipe : Recipe
    {
        public CementKilnRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CementKilnItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(MetalworkingEfficiencySkill), 30, MetalworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GearboxItem>(typeof(MetalworkingEfficiencySkill), 10, MetalworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PistonItem>(typeof(MetalworkingEfficiencySkill), 5, MetalworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(240, MetalworkingSpeedSkill.MultiplicativeStrategy, typeof(MetalworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(CementKilnRecipe), Item.Get<CementKilnItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<CementKilnItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Cement Kiln", typeof(CementKilnRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }
}