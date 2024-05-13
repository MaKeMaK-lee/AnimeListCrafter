using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeListCrafter.Entity
{
    public class FilterSettings
    {
        public Dictionary<string, bool> shiki_status { get; set; } = new Dictionary<string, bool>(){
                {"Plan to Watch", false },
                {"Watching"     , true },
                {"Rewatching"   , true },
                {"Completed"    , true },
                {"On-Hold"      , true },
                {"Dropped"      , true }
        };

        public Dictionary<string, bool> series_type { get; set; } = new Dictionary<string, bool>(){
                {"tv"           , true },
                {"ova"          , false },
                {"special"      , false },
                {"movie"        , true },
                {"ona"          , true },
                {"music"        , false },
                {"tv_special"   , false },
                {"pv"           , false },
                {"cm"           , false },
                {""             , false }
            };

        public FilterSettings()
        {

        }




        FilterSettings(
            bool series_typePlantoWatch,
            bool series_typeWatching,
            bool series_typeRewatching,
            bool series_typeCompleted,
            bool series_typeOnHold,
            bool series_typeDropped,

            bool series_typetv,
            bool series_typeova,
            bool series_typespecial,
            bool series_typemovie,
            bool series_typeona,
            bool series_typemusic,
            bool series_type)
        {



        }

    }
}
