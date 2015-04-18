using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Dextem
{
    /// <summary>
    /// The element processor class from which all concrete element processors derive.
    /// </summary>
    public abstract class BaseProcessor : IElementProcessor
    {
        /// <summary>
        /// Assigns the given ProcessorRegistry instance.
        /// </summary>
        /// <param name="registry">The ProcessorRegistry instance to use.</param>
        public BaseProcessor(ProcessorRegistry registry)
        {
            this.Registry = registry;
        }

        /// <summary>
        /// Gets the provided ProcessorRegistry instance. 
        /// </summary>
        protected ProcessorRegistry Registry { get; private set; }

        /// <summary>
        /// The base Process implementation. This is generally called on return from inside a Process implementation.
        /// </summary>
        /// <param name="writer">The updated StringWriter instance.</param>
        /// <param name="root">The processed element.</param>
        /// <param name="context">The updated context.</param>
        /// <returns>The updated context.</returns>
        public virtual Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            return context;
        }
    }
}
