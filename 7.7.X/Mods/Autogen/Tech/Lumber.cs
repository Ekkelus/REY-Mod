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
    public partial class LumberSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lumber"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Forming trees into more structurally sound and sturdy lumber for use in construction or production. Level by crafting related recipes."); } }

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
    public partial class LumberSkillBook : SkillBook<LumberSkill, LumberSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lumber Skill Book"); } }
    }

    [Serialized]
    public partial class LumberSkillScroll : SkillScroll<LumberSkill, LumberSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lumber Skill Scroll"); } }
    }

    public partial class LumberSkillBookRecipe : Recipe
    {
        public LumberSkillBookRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<LumberSkillBook>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(20),
                new CraftingElement<HewnLogItem>(40),
                new CraftingElement<ClothItem>(20),
                new CraftingElement<BookItem>(4)
            };
            CraftMinutes = new ConstantValue(15);

            Initialize(Localizer.DoStr("Lumber Skill Book"), typeof(LumberSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
