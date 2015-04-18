using System.Xml.Linq;

namespace Dextem
{
    public static class XNodeExtensions
    {
        public static XElement AsXElement(this XNode _this)
        {
            return (XElement)_this;
        }
    }
}
