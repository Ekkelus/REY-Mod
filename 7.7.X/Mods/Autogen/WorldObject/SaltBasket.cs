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

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
	[RequireComponent(typeof(SolidGroundComponent))] 	
    [RequireComponent(typeof(HousingComponent))]                          
    public partial class SaltBasketObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Salt Basket"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SaltBasketItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Misc");                                 
            this.GetComponent<HousingComponent>().Set(SaltBasketItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(750)]
    public partial class SaltBasketItem : WorldObjectItem<SaltBasketObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Salt Basket"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A basket of salt."); } }

        static SaltBasketItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Kitchen",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Spices", 
                                                    DiminishingReturnPercent = 0.4f    
        };}}
    }


    [RequiresSkill(typeof(FertilizerProductionSkill), 1)]
    public partial class SaltBasketRecipe : Recipe
    {
        public SaltBasketRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SaltBasketItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(FertilizerEfficiencySkill), 10, FertilizerEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<RopeItem>(typeof(FertilizerEfficiencySkill), 6, FertilizerEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(5, FertilizerSpeedSkill.MultiplicativeStrategy, typeof(FertilizerSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(SaltBasketRecipe), Item.Get<SaltBasketItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<SaltBasketItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Salt Basket"), typeof(SaltBasketRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}