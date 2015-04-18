using System.Xml.Linq;

namespace Dextem
{
    /// <summary>
    /// Contains extensions for XNode objects.
    /// </summary>
    public static class XNodeExtensions
    {
        /// <summary>
        /// Casts the current XNode to an XElement node and returns the result.
        /// </summary>
        /// <param name="_this">The current XNode.</param>
        /// <returns>The XElement instance.</returns>
        public static XElement AsXElement(this XNode _this)
        {
            return (XElement)_this;
        }
    }
}
