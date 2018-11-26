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
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class ContractBoardObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Contract Board"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ContractBoardItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Economy");                                 


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1500)]
    public partial class ContractBoardItem : WorldObjectItem<ContractBoardObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Contract Board"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A board to post contracts."); } }

        static ContractBoardItem()
        {
            
        }
        
    }


    [RequiresSkill(typeof(WoodworkingSkill), 1)]
    public partial class ContractBoardRecipe : Recipe
    {
        public ContractBoardRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ContractBoardItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(WoodworkingEfficiencySkill), 10, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PaperItem>(typeof(WoodworkingEfficiencySkill), 10, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                //new CraftingElement<NailsItem>(typeof(WoodworkingEfficiencySkill), 20, WoodworkingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(15, WoodworkingSpeedSkill.MultiplicativeStrategy, typeof(WoodworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(ContractBoardRecipe), Item.Get<ContractBoardItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<ContractBoardItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Contract Board", typeof(ContractBoardRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}