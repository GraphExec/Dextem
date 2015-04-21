using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

namespace Dextem
{
    internal class InternalConverter : BaseConverter
    {
        private Dictionary<XName, string> m_context;
        private ProcessorRegistry m_registry;

        internal InternalConverter(ProcessorRegistry registry)
        {
            this.m_registry = registry;
            this.m_context = new Dictionary<XName, string>();

            this.m_registry.Setup();
            this.m_context["lastNode"] = null;
        }

        internal override string Convert(Stream stream)
        {
            Args.IsNotNull(() => stream);

            var xdoc = XDocument.Load(stream);
            var md = string.Empty;

            using (var writer = new StringWriter(CultureInfo.CurrentCulture))
            {
                this.InternalConvert(writer, xdoc.Root);
                md = writer.ToString();
            }
            return md;
        }

        private void InternalConvert(StringWriter writer, XElement root)
        {
            Args.IsNotNull(() => writer, () => root);

            var processor = this.m_registry.Resolve(root.Name);

            if (processor != null)
            {
                processor.Process(writer, root, this.m_context);
            }

            this.m_context["lastNode"] = root.Name.ToString();
        }
    }
}
