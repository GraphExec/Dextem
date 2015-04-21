using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Dextem
{
    /// <summary>
    /// Processes &lt;summary&gt; nodes. This class cannot be inherited.
    /// </summary>
    public sealed class SummaryProcessor : BaseProcessor
    {
        /// <summary>
        /// Creates a new instance of SummaryProcessor using the given ProcessorRegistry.
        /// </summary>
        /// <param name="registry">The ProcessorRegistry instance to use.</param>
        public SummaryProcessor(ProcessorRegistry registry) : base(registry) { }

        /// <summary>
        /// Executes processing of the current &lt;summary&gt; element.
        /// </summary>
        /// <param name="writer">The current StringWriter to use.</param>
        /// <param name="root">The current root element to process.</param>
        /// <param name="context">The current processing context.</param>
        /// <returns>The updated processing context.</returns>
        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            Args.IsNotNull(() => writer, () => root, () => context);

            string summary = Regex.Replace(root.Value, "\\s+", " ", RegexOptions.Multiline);
            writer.WriteLine("{0}\n", summary.Trim().EscapeXml());

            return base.Process(writer, root, context);
        }
    }
}
