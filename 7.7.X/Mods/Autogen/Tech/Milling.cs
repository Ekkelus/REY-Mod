namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequiresSkill(typeof(FarmerSkill), 0)]    
    public partial class MillingSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Milling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class MillingSkillBook : SkillBook<MillingSkill, MillingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Milling Skill Book"); } }
    }

    [Serialized]
    public partial class MillingSkillScroll : SkillScroll<MillingSkill, MillingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Milling Skill Scroll"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 0)] 
    public partial class MillingSkillBookRecipe : Recipe
    {
        public MillingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<MillingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<MortaredStoneItem>(typeof(ResearchEfficiencySkill), 15, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<HewnLogItem>(typeof(ResearchEfficiencySkill), 15, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<WheatItem>(typeof(ResearchEfficiencySkill), 30, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CornItem>(typeof(ResearchEfficiencySkill), 30, ResearchEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(15);

            this.Initialize(Localizer.DoStr("Milling Skill Book"), typeof(MillingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}