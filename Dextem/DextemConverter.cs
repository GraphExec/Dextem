using System.IO;

namespace Dextem
{
    public class DextemConverter
    {
        private InternalConverter m_converter;

        public DextemConverter()
        {
            this.m_converter = new InternalConverter(new ProcessorRegistry());
        }

        public DextemConverter(ProcessorRegistry registry)
        {
            this.m_converter = new InternalConverter(registry);
        }

        public virtual string Convert(Stream stream)
        {
            return this.m_converter.Convert(stream);
        }
    }
}
