namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequiresSkill(typeof(ChefSkill), 0)]    
    public partial class AdvancedCookingSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Cooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class AdvancedCookingSkillBook : SkillBook<AdvancedCookingSkill, AdvancedCookingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Cooking Skill Book"); } }
    }

    [Serialized]
    public partial class AdvancedCookingSkillScroll : SkillScroll<AdvancedCookingSkill, AdvancedCookingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Cooking Skill Scroll"); } }
    }

    [RequiresSkill(typeof(CookingSkill), 0)] 
    public partial class AdvancedCookingSkillBookRecipe : Recipe
    {
        public AdvancedCookingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<AdvancedCookingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BasicSaladItem>(typeof(ResearchEfficiencySkill), 20, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<FruitSaladItem>(typeof(ResearchEfficiencySkill), 20, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BookItem>(typeof(ResearchEfficiencySkill), 4, ResearchEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(30);

            this.Initialize("Advanced Cooking Skill Book", typeof(AdvancedCookingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
