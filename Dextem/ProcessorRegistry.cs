using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Dextem
{
    public class ProcessorRegistry
    {
        private Dictionary<XName, IElementProcessor> m_registry;

        public ProcessorRegistry()
        {
            this.m_registry = new Dictionary<XName, IElementProcessor>();
        }

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
