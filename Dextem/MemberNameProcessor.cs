using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Dextem
{
    public sealed class MemberNameProcessor : BaseProcessor
    {
        public MemberNameProcessor(ProcessorRegistry registry) : base(registry) { }

        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            var methodTypeProcessor = this.Registry.Resolve("methodType");

            if (methodTypeProcessor != null)
            {
                context = methodTypeProcessor.Process(writer, root, context);
            }

            var memberName = context["memberName"];
            var memberType = context["memberType"];

            if (memberType != "T")
            {
                int memberNameStartIndex = this.GetMemberNameStartIndex(memberName, context["typeName"], context["assembly"]);
                string shortMemberName = this.GetShortName(memberName, memberNameStartIndex);
                writer.WriteLine("\n### {0}\n", shortMemberName);
            }

            return base.Process(writer, root, context);
        }

        private string GetShortName(string memberName, int nameStartIndex)
        {
            var shortMemberName = memberName.Substring(nameStartIndex);

            if (shortMemberName.StartsWith("#ctor"))
            {
                return shortMemberName.Replace("#ctor", "Constructor");
            }

            return shortMemberName;
        }

        private int GetMemberNameStartIndex(string memberName, string typeName, string assemblyName)
        {
            var index = 4 + assemblyName.Length;

            if (memberName.Contains(typeName))
            {
                index += typeName.Length;
            }
            else
            {
                index += this.GetTypeName(memberName).Length;
            }

            return index;
        }

        private string GetTypeName(string memberName)
        {
            var result = memberName;

            if (memberName.Contains("."))
            {
                var items = memberName.Split('.');
                result = items[items.Length - 2];
            }

            return result;
        }
    }
}
