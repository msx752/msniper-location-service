using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MSniperService.Models;

namespace MSniperService.Statics
{
    public static class Extensions
    {
        /// <summary>
        /// compare with new datas with already been
        /// </summary>
        /// <param name="newData"></param>
        /// <param name="stored"></param>
        /// <returns></returns>
        public static List<EncounterInfo> NonContains(this List<EncounterInfo> newData, List<EncounterInfo> stored)
        {
            List<EncounterInfo> subList = newData.Select((value, index) => new { Value = value, Index = index })
                .Where(w => stored.Skip(w.Index).FirstOrDefault() != w.Value)
                .Select(s => s.Value).ToList();
            return subList;
        }

        public static void Disposing(this List<EncounterInfo> collection)
        {
            foreach (IDisposable item in collection)
            {
                if (item != null)
                {
                    try
                    {
                        item.Dispose();
                    }
                    catch (Exception)
                    {
                        // log exception and continue
                    }
                }
            }
        }
    }
}