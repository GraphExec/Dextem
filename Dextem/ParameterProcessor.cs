using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Dextem
{
    /// <summary>
    /// Processes &lt;param&gt; nodes. This class cannot be inherited.
    /// </summary>
    public sealed class ParameterProcessor : BaseProcessor
    {
        /// <summary>
        /// Creates a new instance of ParameterProcessor using the given ProcessorRegistry.
        /// </summary>
        /// <param name="registry">The ProcessorRegistry instance to use.</param>
        public ParameterProcessor(ProcessorRegistry registry) : base(registry) { }

        /// <summary>
        /// Executes processing of the current &lt;param&gt; element.
        /// </summary>
        /// <param name="writer">The current StringWriter to use.</param>
        /// <param name="root">The current root element to process.</param>
        /// <param name="context">The current processing context.</param>
        /// <returns>The updated processing context.</returns>
        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            Args.IsNotNull(() => writer, () => root, () => context);

            if (context["lastNode"] != "param")
            {
                writer.WriteLine("###### Parameter");
                writer.WriteLine("| Name | Description |");
                writer.WriteLine("| ---- | ----------- |");
            }

            string paramName = root.Attribute(XName.Get("name")).Value;
            if (context.ContainsKey(paramName))
            {
                writer.WriteLine("| {0} | *{1}*<br>{2} |",
                    paramName,
                    context[paramName].EscapeRawGenerics(),
                    Regex.Replace(root.Value, "\\s+", " ", RegexOptions.Multiline));
            }
            else
            {
                writer.WriteLine("| {0} | *Unknown type*<br>{1} |",
                    paramName,
                    Regex.Replace(root.Value, "\\s+", " ", RegexOptions.Multiline));
            }

            context["lastNode"] = "param";

            return base.Process(writer, root, context);
        }
    }
}
