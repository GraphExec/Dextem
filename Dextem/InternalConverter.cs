using System.Collections.Generic;
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
            var xdoc = XDocument.Load(stream);
            var writer = new StringWriter();
            this.InternalConvert(writer, xdoc.Root);
            return writer.ToString();
        }

        private void InternalConvert(StringWriter writer, XElement root)
        {
            var processor = this.m_registry.Resolve(root.Name);

            if (processor != null)
            {
                processor.Process(writer, root, this.m_context);
            }

            this.m_context["lastNode"] = root.Name.ToString();
        }
    }
}
