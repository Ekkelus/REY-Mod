namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
	using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;
    using Eco.Gameplay.Pipes.LiquidComponents; 

    [RequiresSkill(typeof(PetrolRefiningSkill), 0)]   
    public partial class BarrelRecipe : Recipe
    {
        public BarrelRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BarrelItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(PetrolRefiningEfficiencySkill), 4, PetrolRefiningEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(PetrolRefiningEfficiencySkill), 4, PetrolRefiningEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BarrelRecipe), Item.Get<BarrelItem>().UILink(), 1, typeof(PetrolRefiningSpeedSkill));    
            this.Initialize("Barrel", typeof(BarrelRecipe));

            CraftingComponent.AddRecipe(typeof(ElectricMachinistTableObject), this);
        }
    }

    [Serialized]
    [Solid]
    [RequiresSkill(typeof(PetrolRefiningEfficiencySkill), 0)]   
    public partial class BarrelBlock :
        PickupableBlock     
    { }

    [Serialized]
    [MaxStackSize(10)]                                       
    [Weight(2000)]      
    [Currency]              
    public partial class BarrelItem :
    BlockItem<BarrelBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Barrel"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Expertly crafted from smoothed boards and metal bands, this can carry a variety of substances."); } }

    }

}