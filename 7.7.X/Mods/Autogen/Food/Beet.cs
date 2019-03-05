namespace Eco.Mods.TechTree
{
    using Gameplay.Items;
    using Gameplay.Players;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]
    [Weight(10)]                                          
     [Yield(typeof(BeetItem), typeof(GatheringSkill), new[] {1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f})]      
    public partial class BeetItem :
        FoodItem            
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Beet"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A brilliantly colored bulb with an earthy sweetness."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 3, Fat = 1, Protein = 1, Vitamins = 3};
        public override float Calories                          { get { return 230; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}