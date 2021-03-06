namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Gameplay.Players;
    using Gameplay.Skills;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]
    [RequiresSkill(typeof(CarpenterSkill), 0)]
    public partial class PaperMillingSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Paper Milling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Paper to carry the written word. Or used to stuff in bookshelves. Level by crafting related recipes."); } }

        public override void OnLevelUp(User user)
        {
            user.Skillset.AddExperience(typeof(SelfImprovementSkill), 20, Localizer.DoStr("for leveling up another specialization."));
        }


        public static ModificationStrategy MultiplicativeStrategy =
            new MultiplicativeStrategy(new[] { 1,

                1 - 0.5f,

                1 - 0.55f,

                1 - 0.6f,

                1 - 0.65f,

                1 - 0.7f,

                1 - 0.75f,

                1 - 0.8f,

            });
        public override ModificationStrategy MultiStrategy { get { return MultiplicativeStrategy; } }
        public static ModificationStrategy AdditiveStrategy =
            new AdditiveStrategy(new[] { 0,

                0.5f,

                0.55f,

                0.6f,

                0.65f,

                0.7f,

                0.75f,

                0.8f,

            });
        public override ModificationStrategy AddStrategy { get { return AdditiveStrategy; } }
        public override int RequiredPoint { get { return 0; } }
        public override int MaxLevel { get { return 7; } }
        public override int Tier { get { return 3; } }
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
            Products = new CraftingElement[]
            {
                new CraftingElement<PaperMillingSkillBook>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(30),
                new CraftingElement<MortaredStoneItem>(30)
            };
            CraftMinutes = new ConstantValue(15);

            Initialize(Localizer.DoStr("Paper Milling Skill Book"), typeof(PaperMillingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
