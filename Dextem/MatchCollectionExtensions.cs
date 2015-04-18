using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dextem
{
    internal static class MatchCollectionExtensions
    {
        internal static List<Match> ToList(this MatchCollection _this)
        {
            var list = new List<Match>();

            for (var i = 0; i < _this.Count; i++)
            {
                var match = _this[i];
                list.Add(match);
            }

            return list;
        }
    }
}
