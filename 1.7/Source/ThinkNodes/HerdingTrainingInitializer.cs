using System.Collections.Generic;
using RimWorld;
using Verse;

namespace VanillaRanchingExpanded
{
    /// <summary>
    /// Registers VRE_Herd for opted-in animal races after XML patches and Def loading.
    /// </summary>
    [StaticConstructorOnStartup]
    public static class HerdingTrainingInitializer
    {
        static HerdingTrainingInitializer()
        {
            if (!ModsConfig.OdysseyActive || InternalDefOf.VRE_Herd == null)
            {
                return;
            }

            foreach (ThingDef def in DefDatabase<ThingDef>.AllDefsListForReading)
            {
                if (def.race?.Animal != true || def.GetModExtension<HerdingExtension>() == null)
                {
                    continue;
                }

                if (def.race.specialTrainables == null)
                {
                    def.race.specialTrainables = new List<TrainableDef>();
                }

                if (!def.race.specialTrainables.Contains(InternalDefOf.VRE_Herd))
                {
                    def.race.specialTrainables.Add(InternalDefOf.VRE_Herd);
                }
            }
        }
    }
}
