using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Dextem
{
    /// <summary>
    /// Processes &lt;member&gt; nodes. This class cannot be inherited.
    /// </summary>
    public sealed class MemberProcessor : BaseProcessor
    {
        /// <summary>
        /// Creates a new instance of MemberProcessor using the given ProcessorRegistry.
        /// </summary>
        /// <param name="registry">The ProcessorRegistry instance to use.</param>
        public MemberProcessor(ProcessorRegistry registry) : base(registry) { }

        /// <summary>
        /// Executes processing of the current &lt;member&gt; element.
        /// </summary>
        /// <param name="writer">The current StringWriter to use.</param>
        /// <param name="root">The current root element to process.</param>
        /// <param name="context">The current processing context.</param>
        /// <returns>The updated processing context.</returns>
        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            Args.IsNotNull(() => writer, () => root, () => context);

            var memberNameProcessor = this.Registry.Resolve("memberName");

            if (memberNameProcessor != null)
            {
                context = memberNameProcessor.Process(writer, root, context);
            }

            foreach (var node in root.Nodes().Where(x => x.NodeType == XmlNodeType.Element))
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
