using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Dextem
{
    /// <summary>
    /// Processes &lt;members&gt; nodes. This class cannot be inherited.
    /// </summary>
    public sealed class MembersProcessor : BaseProcessor
    {
        /// <summary>
        /// Creates a new instance of MembersProcessor using the given ProcessorRegistry.
        /// </summary>
        /// <param name="registry">The ProcessorRegistry instance to use.</param>
        public MembersProcessor(ProcessorRegistry registry) : base(registry) { }

        /// <summary>
        /// Executes processing of the current &lt;members&gt; element.
        /// </summary>
        /// <param name="writer">The current StringWriter to use.</param>
        /// <param name="root">The current root element to process.</param>
        /// <param name="context">The current processing context.</param>
        /// <returns>The updated processing context.</returns>
        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            var members = new List<XElement>(root.Elements("member"));
            members.Sort((a, b) =>
            {
                return
                    a.Attribute(XName.Get("name")).Value.Substring(2).CompareTo(
                    b.Attribute(XName.Get("name")).Value.Substring(2));
            });

            foreach (var member in members)
            {
                writer.WriteLine();

                var processor = this.Registry.Resolve(member.Name);

                if (processor != null)
                {
                    context = processor.Process(writer, member, context);
                }
            }

            return base.Process(writer, root, context);
        }
    }
}
