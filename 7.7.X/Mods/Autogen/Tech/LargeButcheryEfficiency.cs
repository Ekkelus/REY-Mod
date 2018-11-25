namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Core.Utils;
    using Eco.Core.Utils.AtomicAction;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
	using Eco.Shared.Localization;
    using Eco.Shared.Services;
    using Eco.Shared.Utils;
    using Gameplay.Systems.Tooltip;
	using REYmod.Utils;

    [Serialized]
    [RequiresSkill(typeof(LargeButcherySkill), 1)]    
    public partial class LargeButcheryEfficiencySkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Butchery Efficiency"); } }
        public override string Description { get { return Localizer.DoStr(""); } }

        public static ModificationStrategy MultiplicativeStrategy = 
            new MultiplicativeStrategy(new float[] { 1, 1 - 0.1f, 1 - 0.2f, 1 - 0.3f, 1 - 0.4f, 1 - 0.5f, 1 - 0.55f, 1 - 0.6f, 1 - 0.65f, 1 - 0.7f, 1 - 0.8f });
        public static ModificationStrategy AdditiveStrategy =
            new AdditiveStrategy(new float[] { 0, 0.2f, 0.35f, 0.5f, 0.65f, 0.8f });
        public static int[] SkillPointCost = { 5, 10, 15, 20, 25, 25, 25, 25, 25, 25 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 10; } }

        public override IAtomicAction CreateLevelUpAction(Player player)
        {
            return SkillUtils.SuperSkillLevelUp(this, player);
        }
    }

}
