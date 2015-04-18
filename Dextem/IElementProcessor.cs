using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Dextem
{
    public interface IElementProcessor
    {
        Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context);
    }
}
