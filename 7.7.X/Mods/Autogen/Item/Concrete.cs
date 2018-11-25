namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Shared.Localization;
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
    using Eco.Shared.Serialization;
	using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    [RequiresSkill(typeof(CementSkill), 1)]   
    public partial class ConcreteRecipe : Recipe
    {
        public ConcreteRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ConcreteItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(CementProductionEfficiencySkill), 10, CementProductionEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<SandItem>(typeof(CementProductionEfficiencySkill), 10, CementProductionEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ConcreteRecipe), Item.Get<ConcreteItem>().UILink(), 2, typeof(CementProductionSpeedSkill));    
            this.Initialize("Concrete", typeof(ConcreteRecipe));

            CraftingComponent.AddRecipe(typeof(CementKilnObject), this);
        }
    }


    [Serialized]
    [Weight(10000)]      
    [Currency]              
    public partial class ConcreteItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Concrete"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Concrete"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A very strong building material made from cement and an aggregate like crushed stone."); } }

    }

}