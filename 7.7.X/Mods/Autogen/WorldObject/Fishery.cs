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
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(0.5f)]        
    public partial class FisheryObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fishery"); } } 

        public virtual Type RepresentedItemType { get { return typeof(FisheryItem); } } 


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
    public partial class FisheryItem : WorldObjectItem<FisheryObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fishery"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A place to create fishing poles and traps."); } }

        static FisheryItem()
        {
            
        }
        
    }


    [RequiresSkill(typeof(FishingSkill), 1)]
    public partial class FisheryRecipe : Recipe
    {
        public FisheryRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FisheryItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(FishingSkill), 20, FishingSkill.MultiplicativeStrategy),
				new CraftingElement<RopeItem>(typeof(FishingSkill), 3, FishingSkill.MultiplicativeStrategy),				
            };
            this.CraftMinutes = new ConstantValue(1);
            this.Initialize(Localizer.DoStr("Fishery"), typeof(FisheryRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}