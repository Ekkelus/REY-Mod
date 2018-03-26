namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    using Eco.Shared.Localization;

    [Serialized]    
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(HousingComponent))]
    [RequireRoomVolume(4)]
    public partial class PlanterPotSquareObject : WorldObject
    {
        public override string FriendlyName { get { return "Square Pot"; } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Housing");                                 
            this.GetComponent<HousingComponent>().Set(PlanterPotSquareItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class PlanterPotSquareItem : WorldObjectItem<PlanterPotSquareObject>
    {
        public override string FriendlyName { get { return "Square Pot"; } } 
        public override string Description { get { return "Sometimes you just want to bring a little bit of nature into your house."; } }

        static PlanterPotSquareItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 0.5f,
                                                    TypeForRoomLimit = "",
                                                    DiminishingReturnPercent = 0.75f
                                                };}}       
    }


    [RequiresSkill(typeof(StoneworkingSkill), 3)]
    public partial class PlanterPotSquareRecipe : Recipe
    {
        public PlanterPotSquareRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PlanterPotSquareItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(StoneworkingEfficiencySkill), 10, StoneworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PulpFillerItem>(typeof(StoneworkingEfficiencySkill), 5, StoneworkingEfficiencySkill.MultiplicativeStrategy)
            };
            SkillModifiedValue value = new SkillModifiedValue(5, StoneworkingSpeedSkill.MultiplicativeStrategy, typeof(StoneworkingSpeedSkill), Localizer.Do("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(PlanterPotSquareRecipe), Item.Get<PlanterPotSquareItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<PlanterPotSquareItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Planter Pot Square", typeof(PlanterPotSquareRecipe));
            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }
}