namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing;
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
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(2)]        
    public partial class ElectronicsAssemblyObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Electronics Assembly"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ElectronicsAssemblyItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Crafting");                                 
            this.GetComponent<HousingComponent>().Set(ElectronicsAssemblyItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class ElectronicsAssemblyItem : WorldObjectItem<ElectronicsAssemblyObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Electronics Assembly"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A set of machinery to create electronics."); } }

        static ElectronicsAssemblyItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
    }


    [RequiresSkill(typeof(ElectronicsSkill), 0)]
    public partial class ElectronicsAssemblyRecipe : Recipe
    {
        public ElectronicsAssemblyRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ElectronicsAssemblyItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(ElectronicsSkill), 20, ElectronicsSkill.MultiplicativeStrategy),
				new CraftingElement<RivetItem>(typeof(ElectronicsSkill), 10, ElectronicsSkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(60, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(ElectronicsAssemblyRecipe), Item.Get<ElectronicsAssemblyItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<ElectronicsAssemblyItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Electronics Assembly"), typeof(ElectronicsAssemblyRecipe));
            CraftingComponent.AddRecipe(typeof(ElectricMachinistTableObject), this);
        }
    }
}