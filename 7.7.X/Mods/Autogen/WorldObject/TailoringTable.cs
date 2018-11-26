namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(45)]                              
    [RequireRoomMaterialTier(1)]        
    public partial class TailoringTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tailoring Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(TailoringTableItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Crafting");                                 



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class TailoringTableItem : WorldObjectItem<TailoringTableObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tailoring Table"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Spearhead the fashion movement with the finest clothes and hair!"); } }

        static TailoringTableItem()
        {
            
        }

    }


    [RequiresSkill(typeof(WoodworkingSkill), 1)]
    public partial class TailoringTableRecipe : Recipe
    {
        public TailoringTableRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TailoringTableItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(WoodworkingEfficiencySkill), 40, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PlantFibersItem>(typeof(WoodworkingEfficiencySkill), 50, WoodworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(60, WoodworkingSpeedSkill.MultiplicativeStrategy, typeof(WoodworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(TailoringTableRecipe), Item.Get<TailoringTableItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<TailoringTableItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Tailoring Table"), typeof(TailoringTableRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}