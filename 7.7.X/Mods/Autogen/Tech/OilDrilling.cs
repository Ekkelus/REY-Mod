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
    [RequiresSkill(typeof(EngineerSkill), 0)]
    public partial class OilDrillingSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Oil Drilling"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("While it takes some advanced tools and constructions, harvesting and refining oil can be an important step. Level by crafting related recipes."); } }

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
        public static int[] SkillPointCost = {

            1,

            1,

            1,

            1,

            1,

            1,

            1,

        };
        public override int RequiredPoint { get { return Level < SkillPointCost.Length ? SkillPointCost[Level] : 0; } }
        public override int PrevRequiredPoint { get { return Level - 1 >= 0 && Level - 1 < SkillPointCost.Length ? SkillPointCost[Level - 1] : 0; } }
        public override int MaxLevel { get { return 7; } }
        public override int Tier { get { return 4; } }
    }

    [Serialized]
    public partial class OilDrillingSkillBook : SkillBook<OilDrillingSkill, OilDrillingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Oil Drilling Skill Book"); } }
    }

    [Serialized]
    public partial class OilDrillingSkillScroll : SkillScroll<OilDrillingSkill, OilDrillingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Oil Drilling Skill Scroll"); } }
    }

    [RequiresSkill(typeof(MechanicsSkill), 0)]
    public partial class OilDrillingSkillBookRecipe : Recipe
    {
        public OilDrillingSkillBookRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<OilDrillingSkillBook>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<ReinforcedConcreteItem>(50),
                new CraftingElement<CombustionEngineItem>(4),
                new CraftingElement<LumberItem>(80),
                new CraftingElement<BookItem>(16)
            };
            CraftMinutes = new ConstantValue(30);

            Initialize(Localizer.DoStr("Oil Drilling Skill Book"), typeof(OilDrillingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
