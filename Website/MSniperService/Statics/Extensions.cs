using MemoryManaging;
using MSniperService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSniperService.Statics
{
    public static class Extensions
    {
        public static List<EncounterInfo> FindNonContains(this List<EncounterInfo> newData, List<EncounterInfo> stored)
        {
            var subList = newData.Select((value, index) => new { Value = value, Index = index })
                .Where(w => stored.Skip(w.Index).FirstOrDefault() != w.Value)
                .Select(s => s.Value).ToList();
            return subList;
        }

        public static bool CheckAddState(this MemoryStore<EncounterInfo> val, EncounterInfo item)
        {
            try
            {
                var poid = (PokemonId)Enum.Parse(typeof(PokemonId), item.PokemonName);
                if (
                    poid == PokemonId.Pidgey ||
                    poid == PokemonId.Rattata ||
                    poid == PokemonId.Caterpie ||
                    poid == PokemonId.Weedle ||
                    poid == PokemonId.Zubat ||
                    poid == PokemonId.Poliwag ||
                    poid == PokemonId.Exeggcute ||
                    poid == PokemonId.Staryu ||
                    poid == PokemonId.Bellsprout ||
                    poid == PokemonId.Spearow ||
                    poid == PokemonId.Ponyta ||
                    poid == PokemonId.Tentacool ||
                    poid == PokemonId.Golem ||
                    poid == PokemonId.Eevee ||
                    poid == PokemonId.Magikarp ||
                    poid == PokemonId.Growlithe ||
                    poid == PokemonId.Voltorb ||
                    poid == PokemonId.Abra ||
                    poid == PokemonId.Charmander ||
                    poid == PokemonId.Meowth ||
                    poid == PokemonId.Clefairy ||
                    poid == PokemonId.Psyduck ||
                    poid == PokemonId.Tauros ||
                    poid == PokemonId.Shellder ||
                    poid == PokemonId.Machop
                    )
                    return item.Iv >= 85;

                var poGrade = PokemonGradeHelper.GetPokemonGrade(poid);
                switch (poGrade)
                {
                    case PokemonGrades.VeryCommon:
                        return item.Iv >= 65;

                    case PokemonGrades.Common:
                        return item.Iv >= 45;

                    case PokemonGrades.Popular:
                        return item.Iv >= 30;

                    case PokemonGrades.Rare:
                        return item.Iv >= 25;

                    default:
                        switch (poGrade)
                        {
                            case PokemonGrades.Popular:
                                return item.Iv >= 5;

                            case PokemonGrades.Epic:
                                return item.Iv >= 0;

                            case PokemonGrades.Legendary:
                                return item.Iv >= 0;

                            case PokemonGrades.NONE:
                                return item.Iv >= 0;
                        }
                        break;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return false;
        }
    }
}