namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class HewnLogDoorObject : 
        DoorObject, 
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Hewn Log Door"); } } 

        public override Type RepresentedItemType { get { return typeof(HewnLogDoorItem); } } 


        protected override void Initialize()
        {
            base.Initialize(); 


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1000)]
    public partial class HewnLogDoorItem :
        WorldObjectItem<HewnLogDoorObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Hewn Log Door"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A door made from roughly hewn logs."); } }

        [Tooltip(100)]
        public string TierTooltip()
        {
            return "<i>Tier 1 building material</i>";
        }


        static HewnLogDoorItem()
        {
            
        }

    }


    [RequiresSkill(typeof(WoodworkingSkill), 2)]
    public partial class HewnLogDoorRecipe : Recipe
    {
        public HewnLogDoorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HewnLogDoorItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(WoodworkingEfficiencySkill), 10, WoodworkingEfficiencySkill.MultiplicativeStrategy),   
                new CraftingElement<HingeItem>(typeof(WoodworkingEfficiencySkill), 2, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(WoodworkingEfficiencySkill), 5, WoodworkingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(3, WoodworkingSpeedSkill.MultiplicativeStrategy, typeof(WoodworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(HewnLogDoorRecipe), Item.Get<HewnLogDoorItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<HewnLogDoorItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Hewn Log Door"), typeof(HewnLogDoorRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}