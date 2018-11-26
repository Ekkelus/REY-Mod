namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [Weight(10)]                                          
     [Yield(typeof(PricklyPearFruitItem), typeof(DesertDrifterSkill), new float[] {1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f})]      
    public partial class PricklyPearFruitItem :
        FoodItem            
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Prickly Pear Fruit"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A succulent fruit coated in a rather terrifying array of spines."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 2, Fat = 1, Protein = 1, Vitamins = 3};
        public override float Calories                          { get { return 190; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}