using System.IO;

namespace Dextem
{
    /// <summary>
    /// The default Dextem XML-to-Markdown converter or inherit from this class to implement custom conversion logic.
    /// </summary>
    public class DextemConverter
    {
        private InternalConverter m_converter;

        /// <summary>
        /// Creates a new DextemConverter instance.
        /// </summary>
        public DextemConverter()
        {
            this.m_converter = new InternalConverter(new ProcessorRegistry());
        }

        /// <summary>
        /// Creates a new DextemConverter instance using the given ProcessorRegistry instance.
        /// </summary>
        /// <param name="registry">The ProcessorRegistry instance to use.</param>
        public DextemConverter(ProcessorRegistry registry)
        {
            Args.IsNotNull(() => registry);

            this.m_converter = new InternalConverter(registry);
        }

        /// <summary>
        /// Converts the given Stream to Markdown text.
        /// </summary>
        /// <param name="stream">The Stream to process. By default, this is expected to be a Stream of the XML documentation file.</param>
        /// <returns>The string representing the resultant Markdown.</returns>
        public virtual string Convert(Stream stream)
        {
            Args.IsNotNull(() => stream);

            return this.m_converter.Convert(stream);
        }
    }
}
