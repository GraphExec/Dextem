using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Dextem
{
    /// <summary>
    /// Processes &lt;typeparam&gt; nodes. This class cannot be inherited.
    /// </summary>
    public sealed class TypeParameterProcessor : BaseProcessor
    {
        /// <summary>
        /// Creates a new instance of TypeParameterProcessor using the given ProcessorRegistry.
        /// </summary>
        /// <param name="registry">The ProcessorRegistry instance to use.</param>
        public TypeParameterProcessor(ProcessorRegistry registry) : base(registry) { }

        /// <summary>
        /// Executes processing of the current &lt;typeparam&gt; element.
        /// </summary>
        /// <param name="writer">The current StringWriter to use.</param>
        /// <param name="root">The current root element to process.</param>
        /// <param name="context">The current processing context.</param>
        /// <returns>The updated processing context.</returns>
        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            Args.IsNotNull(() => writer, () => root, () => context);

            if (context["lastNode"] != "typeparam")
            {
                writer.WriteLine("###### Type Parameters");
                writer.WriteLine("| Name | Description |");
                writer.WriteLine("| ---- | ----------- |");
            }

            string paramName = root.Attribute(XName.Get("name")).Value;
            if (context.ContainsKey(paramName))
            {
                writer.WriteLine("| *{0}* | {1} |",
                    context[paramName],
                    Regex.Replace(root.Value, "\\s+", " ", RegexOptions.Multiline));
            }
            else
            {
                writer.WriteLine("| {0} | {1} |",
                    paramName,
                    Regex.Replace(root.Value, "\\s+", " ", RegexOptions.Multiline));
            }

            context["lastNode"] = "typeparam";

            return base.Process(writer, root, context);
        }
    }
}
