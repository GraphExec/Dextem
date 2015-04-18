using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Dextem
{
    /// <summary>
    /// Processes &lt;remarks&gt; nodes. This class cannot be inherited.
    /// </summary>
    public sealed class RemarksProcessor : BaseProcessor
    {
        /// <summary>
        /// Creates a new instance of RemarksProcessor using the given ProcessorRegistry.
        /// </summary>
        /// <param name="registry">The ProcessorRegistry instance to use.</param>
        public RemarksProcessor(ProcessorRegistry registry) : base(registry) { }

        /// <summary>
        /// Executes processing of the current &lt;remarks&gt; element.
        /// </summary>
        /// <param name="writer">The current StringWriter to use.</param>
        /// <param name="root">The current root element to process.</param>
        /// <param name="context">The current processing context.</param>
        /// <returns>The updated processing context.</returns>
        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            writer.WriteLine("\n###### Remarks\n");
            writer.WriteLine("{0}\n", Regex.Replace(root.Value, "\\s+", " ", RegexOptions.Multiline));

            return base.Process(writer, root, context);
        }
    }
}
