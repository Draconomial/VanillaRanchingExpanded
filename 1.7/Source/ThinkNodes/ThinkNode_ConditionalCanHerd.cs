using Verse;
using Verse.AI;

namespace VanillaRanchingExpanded
{
    public class ThinkNode_ConditionalCanHerd : ThinkNode_Conditional
    {
        protected override bool Satisfied(Pawn pawn)
        {
            // RimWorld creates the training tracker conditionally, even for races with training tags.
            return pawn.def.race.trainableTags?.Contains(InternalDefOf.VRE_Herd.defName) == true
                && pawn.training?.HasLearned(InternalDefOf.VRE_Herd) == true;
        }
    }
}
