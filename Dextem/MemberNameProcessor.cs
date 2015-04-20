using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Dextem
{
    /// <summary>
    /// Processes the name of &lt;member&gt; nodes. This class cannot be inherited.
    /// </summary>
    public sealed class MemberNameProcessor : BaseProcessor
    {
        /// <summary>
        /// Creates a new instance of MemberNameProcessor using the given ProcessorRegistry.
        /// </summary>
        /// <param name="registry">The ProcessorRegistry instance to use.</param>
        public MemberNameProcessor(ProcessorRegistry registry) : base(registry) { }

        /// <summary>
        /// Executes name processing of the current &lt;member&gt; element.
        /// </summary>
        /// <param name="writer">The current StringWriter to use.</param>
        /// <param name="root">The current root element to process.</param>
        /// <param name="context">The current processing context.</param>
        /// <returns>The updated processing context.</returns>
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
                string shortMemberName = this.GetShortName(memberName, memberType, memberNameStartIndex);
                writer.WriteLine("\n### {0}\n", shortMemberName);
            }

            return base.Process(writer, root, context);
        }

        private string GetShortName(string memberName, string memberType, int nameStartIndex)
        {
            var shortMemberName = memberName.Substring(nameStartIndex);

            if (shortMemberName.StartsWith("#ctor"))
            {
                shortMemberName = shortMemberName.Replace("#ctor", "Constructor");
            }

            if (memberType == "M" && !shortMemberName.Contains("(") && !shortMemberName.Contains(")"))
            {
                shortMemberName += "()";
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
