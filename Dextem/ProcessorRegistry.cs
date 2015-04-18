using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Dextem
{
    /// <summary>
    /// Provides registration and resolution of IElementProcessors. Inherit from this class to add custom registration.
    /// </summary>
    public class ProcessorRegistry
    {
        private Dictionary<XName, IElementProcessor> m_registry;

        /// <summary>
        /// Creates a new ProcessorRegistry instance.
        /// </summary>
        public ProcessorRegistry()
        {
            this.m_registry = new Dictionary<XName, IElementProcessor>();
        }

        /// <summary>
        /// Set up this ProcessorRegistry instance. Override this method in derived classes to set up custom registration.
        /// </summary>
        public virtual void Setup()
        {
            this.Register<DocumentProcessor>("doc");
            this.Register<AssemblyProcessor>("assembly");
            this.Register<MembersProcessor>("members");
            this.Register<MemberProcessor>("member");
            this.Register<MemberNameProcessor>("memberName");
            this.Register<MethodTypeProcessor>("methodType");
            this.Register<SummaryProcessor>("summary");
            this.Register<ParameterProcessor>("param");
            this.Register<TypeParameterProcessor>("typeparam");
            this.Register<ReturnsProcessor>("returns");
            this.Register<RemarksProcessor>("remarks");
            this.Register<ExceptionProcessor>("exception");
        }

        /// <summary>
        /// Register the provided element processor type to process elements of the given element name.
        /// </summary>
        /// <typeparam name="TProcessor">The processor type. The registered processor type must inherit IElementProcessor.</typeparam>
        /// <param name="elementName">The name of the element which the given processor will process.</param>
        public void Register<TProcessor>(string elementName)
            where TProcessor : class, IElementProcessor
        {
            if (string.IsNullOrEmpty(elementName))
            {
                throw new ArgumentNullException("elementName");
            }

            var processor = Activator.CreateInstance(typeof(TProcessor), new object[] { this });

            this.m_registry.Add(elementName, processor as TProcessor);
        }

        /// <summary>
        /// Get the IElementProcessor implementation that is registered to process the given element.
        /// </summary>
        /// <param name="name">The name of the element for which to look for a registered processor.</param>
        /// <returns>The IElementProcessor instance. Returns null if a registered processor for the given element is not found.</returns>
        public IElementProcessor Resolve(XName name)
        {
            if (this.m_registry.ContainsKey(name))
            {
                return this.m_registry[name];
            }

            return null;
        }
    }
}
