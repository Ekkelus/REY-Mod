namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequiresSkill(typeof(CarpenterSkill), 0)]    
    public partial class PaperMillingSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Paper Milling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        public override int RequiredPoint { get { return 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class PaperMillingSkillBook : SkillBook<PaperMillingSkill, PaperMillingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Paper Milling Skill Book"); } }
    }

    [Serialized]
    public partial class PaperMillingSkillScroll : SkillScroll<PaperMillingSkill, PaperMillingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Paper Milling Skill Scroll"); } }
    }

    public partial class PaperMillingSkillBookRecipe : Recipe
    {
        public PaperMillingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PaperMillingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(ResearchEfficiencySkill), 30, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<MortaredStoneItem>(typeof(ResearchEfficiencySkill), 30, ResearchEfficiencySkill.MultiplicativeStrategy), 
            };
            this.CraftMinutes = new ConstantValue(15);

            this.Initialize(Localizer.DoStr("Paper Milling Skill Book"), typeof(PaperMillingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}