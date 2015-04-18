using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Dextem
{
    public abstract class BaseProcessor : IElementProcessor
    {
        public BaseProcessor(ProcessorRegistry registry)
        {
            this.Registry = registry;
        }

        protected ProcessorRegistry Registry { get; private set; }

        public virtual Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            return context;
        }
    }
}
