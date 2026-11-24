using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;
using VEF.AnimalGenes;

namespace VanillaRanchingExpanded
{
    [HarmonyPatch(typeof(CompHatcher))]
    [HarmonyPatch("Hatch")]
    public static class VanillaRanchingExpanded_CompHatcher_Hatch_Patch
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> MoveGenesFromEggToPawn(IEnumerable<CodeInstruction> codeInstructions)
        {
            var moveGenesMethod = AccessTools.Method(typeof(VanillaRanchingExpanded_CompHatcher_Hatch_Patch), nameof(MoveGenes));
            var generatePawn = AccessTools.Method(typeof(PawnGenerator), nameof(PawnGenerator.GeneratePawn),
                new[] { typeof(PawnGenerationRequest) });

            foreach (var instruction in codeInstructions)
            {
                yield return instruction;
                if (instruction.Calls(generatePawn))
                {
                    // Keep the generated pawn on the stack for the original hatch code.
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, moveGenesMethod);
                }
            }
        }

        public static void MoveGenes(Pawn pawn, CompHatcher compHatcher)
        {
            if (pawn?.AnimalGenes is not CompAnimalGenes comp) { return; }
           
            if (compHatcher.parent.AnimalGenes is not CompAnimalGenes compEgg) { return; }
           
            if (compEgg.HasAssignedGenes)
                comp.SetInheritedGenes(compEgg.genes);
           
        }

    }
}
