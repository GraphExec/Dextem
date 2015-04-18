using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Dextem
{
    /// <summary>
    /// Processes &lt;assembly&gt; nodes. This class cannot be inherited.
    /// </summary>
    public sealed class AssemblyProcessor : BaseProcessor
    {
        /// <summary>
        /// Creates a new instance of AssemblyProcessor using the given ProcessorRegistry.
        /// </summary>
        /// <param name="registry">The ProcessorRegistry instance to use.</param>
        public AssemblyProcessor(ProcessorRegistry registry) : base(registry) { }

        /// <summary>
        /// Executes processing of the current root element.
        /// </summary>
        /// <param name="writer">The current StringWriter to use.</param>
        /// <param name="root">The current root element to process.</param>
        /// <param name="context">The current processing context.</param>
        /// <returns>The updated processing context.</returns>
        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            context[root.Name] = root.Element("name").Value;

            writer.WriteLine("\n# {0}\n", context[root.Name]);

            return base.Process(writer, root, context);
        }
    }
}
