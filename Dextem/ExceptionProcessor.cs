using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Dextem
{
    /// <summary>
    /// Processes &lt;exception&gt; nodes. This class cannot be inherited.
    /// </summary>
    public sealed class ExceptionProcessor : BaseProcessor
    {
        /// <summary>
        /// Creates a new instance of ExceptionProcessor using the given ProcessorRegistry.
        /// </summary>
        /// <param name="registry">The ProcessorRegistry instance to use.</param>
        public ExceptionProcessor(ProcessorRegistry registry) : base(registry) { }

        /// <summary>
        /// Executes processing of the current &lt;exception&gt; element.
        /// </summary>
        /// <param name="writer">The current StringWriter to use.</param>
        /// <param name="root">The current root element to process.</param>
        /// <param name="context">The current processing context.</param>
        /// <returns>The updated processing context.</returns>
        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            Args.IsNotNull(() => writer, () => root, () => context);

            string exName = root.Attribute("cref").Value.Substring(2);
            exName = exName.Replace(context["assembly"] + ".", "");
            exName = exName.Replace(context["typeName"] + ".", "");
            writer.WriteLine("*{0}:* {1}\n",
                exName,
                Regex.Replace(root.Value, "\\s+", " ", RegexOptions.Multiline));

            return base.Process(writer, root, context);
        }
    }
}
