using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Dextem
{
    public sealed class MembersProcessor : BaseProcessor
    {
        public MembersProcessor(ProcessorRegistry registry) : base(registry) { }

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
