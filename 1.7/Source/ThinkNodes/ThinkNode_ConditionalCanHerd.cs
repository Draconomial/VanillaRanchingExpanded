using Verse;
using Verse.AI;

namespace VanillaRanchingExpanded
{
    public class ThinkNode_ConditionalCanHerd : ThinkNode_Conditional
    {
        protected override bool Satisfied(Pawn pawn)
        {
            if (pawn?.def == null)
            {
                return false;
            }

            if (pawn.def.race?.Animal != true ||
                pawn.def.GetModExtension<HerdingExtension>() == null)
            {
                return false;
            }

            if (ModsConfig.OdysseyActive)
            {
                return pawn.training?.HasLearned(InternalDefOf.VRE_Herd) == true;
            }

            return true;
        }
    }
}
