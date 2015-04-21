using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Dextem
{
    /// <summary>
    /// Processes &lt;doc&gt; nodes. This class cannot be inherited.
    /// </summary>
    public sealed class DocumentProcessor : BaseProcessor
    {
        /// <summary>
        /// Creates a new instance of DocumentProcessor using the given ProcessorRegistry.
        /// </summary>
        /// <param name="registry">The ProcessorRegistry instance to use.</param>
        public DocumentProcessor(ProcessorRegistry registry) : base(registry) { }

        /// <summary>
        /// Executes processing of the current &lt;doc&gt; element.
        /// </summary>
        /// <param name="writer">The current StringWriter to use.</param>
        /// <param name="root">The current root element to process.</param>
        /// <param name="context">The current processing context.</param>
        /// <returns>The updated processing context.</returns>
        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            Args.IsNotNull(() => writer, () => root, () => context);

            foreach (var node in root.Nodes())
            {
                var processor = this.Registry.Resolve(node.AsXElement().Name);

                if (processor != null)
                {
                    context = processor.Process(writer, node.AsXElement(), context);
                }
            }

            return base.Process(writer, root, context);
        }
    }
}
