
namespace Dextem
{
    internal static class StringExtensions
    {
        internal static string EscapeXml(this string _this)
        {
            return _this.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
        }

        internal static string EscapeRawGenerics(this string _this)
        {
            return _this.Replace("{", "<").Replace("}", ">").EscapeXml();
        }

        internal static bool Any(this string _this)
        {
            return _this.Length > 0;
        }
    }
}
