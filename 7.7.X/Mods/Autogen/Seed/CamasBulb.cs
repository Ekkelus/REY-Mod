namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Gameplay.Players;
    using System.ComponentModel;

    [Serialized]
    [Yield(typeof(CamasBulbItem), typeof(GrasslandGathererSkill), new float[] { 1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f  })]  
    [Weight(10)]  
    public partial class CamasBulbItem : SeedItem
    {
        static CamasBulbItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 1, Fat = 5, Protein = 2, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Camas Bulb"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow a camas plant."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Camas"); } }

        public override float Calories { get { return 120; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class CamasBulbPackItem : SeedPackItem
    {
        static CamasBulbPackItem() { }

        public override LocString DisplayName { get { return Localizer.DoStr("Camas Bulb Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow a camas plant."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Camas"); } }
    }

}