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
    public partial class CookingSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class CookingSkillBook : SkillBook<CookingSkill, CookingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cooking Skill Book"); } }
    }

    [Serialized]
    public partial class CookingSkillScroll : SkillScroll<CookingSkill, CookingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cooking Skill Scroll"); } }
    }

    [RequiresSkill(typeof(GatheringSkill), 0)] 
    public partial class CookingSkillBookRecipe : Recipe
    {
        public CookingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CookingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(ResearchEfficiencySkill), 25, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<HewnLogItem>(typeof(ResearchEfficiencySkill), 20, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CampfireRoastItem>(typeof(ResearchEfficiencySkill), 10, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<WheatPorridgeItem>(typeof(ResearchEfficiencySkill), 10, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BookItem>(typeof(ResearchEfficiencySkill), 4, ResearchEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(15);

            this.Initialize(Localizer.DoStr("Cooking Skill Book"), typeof(CookingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}