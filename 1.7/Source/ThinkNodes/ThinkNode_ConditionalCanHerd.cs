using Verse;
using Verse.AI;

namespace VanillaRanchingExpanded
{
    public class ThinkNode_ConditionalCanHerd : ThinkNode_Conditional
    {
        protected override bool Satisfied(Pawn pawn)
        {
            // RimWorld 1.7 restricts specialTrainables by species only when Odyssey is active.
            // trainableTags provides the same XML opt-in for animal races with or without Odyssey.
            if (pawn?.def?.race?.Animal != true ||
                InternalDefOf.VRE_Herd == null ||
                pawn.def.race.trainableTags?.Contains(InternalDefOf.VRE_Herd.defName) != true)
            {
                return false;
            }

            // Herding always requires completing VRE_Herd training, regardless of DLC.
            return pawn.training?.HasLearned(InternalDefOf.VRE_Herd) == true;
        }
    }
}
