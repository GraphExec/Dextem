using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Dextem
{
    /// <summary>
    /// The interface from which all element processors are derived.
    /// </summary>
    public interface IElementProcessor
    {
        /// <summary>
        /// When implemented, executes processing of the current root element.
        /// </summary>
        /// <param name="writer">The current StringWriter to use.</param>
        /// <param name="root">The current root element to process.</param>
        /// <param name="context">The current processing context.</param>
        /// <returns>The updated processing context.</returns>
        Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context);
    }
}
