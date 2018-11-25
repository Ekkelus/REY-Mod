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



    [Serialized]
    [Weight(10)]      
    [Yield(typeof(PlantFibersItem), typeof(GrasslandGathererSkill), new float[] { 1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f })]  
    [Currency]              
    public partial class PlantFibersItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Plant Fibers"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Harvested from a number of plants, these fibers are useful for a suprising number of things."); } }

    }

}