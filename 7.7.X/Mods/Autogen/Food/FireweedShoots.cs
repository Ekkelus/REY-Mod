namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [Weight(10)]                                          
     [Yield(typeof(FireweedShootsItem), typeof(TundraTravellerSkill), new float[] {1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f})]      
    public partial class FireweedShootsItem :
        FoodItem            
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fireweed Shoots"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A bitter, brightly colored shoot similar to asparagus."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 3, Fat = 0, Protein = 0, Vitamins = 4};
        public override float Calories                          { get { return 150; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}