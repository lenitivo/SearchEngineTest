using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public sealed class Constant
    {
        public sealed class SearchEngine
        {
            public const string Google = "Google";
            public const string MSN = "MSN";
        }

        public sealed class EngineUrl
        {
            public const string GoogleUrl = "http://www.google.com/search";
            public const string MSNUrl = "http://www.bing.com/search";
        }

        public sealed class Culture
        {
            public const string US = "en-US";
            public const string ES = "es-ES";            
            public const string PE = "es-PE";
        }
        public sealed class LimitWord
        {
            public const string GoogleStart = "About";
            public const string GoogleStart2 = "resultStats\">";
            public const string GoogleEnd = "results</div>";
            public const string MSNStart = "sb_count\">";
            public const string MSNEnd = "result";
        }
        
    }
}
