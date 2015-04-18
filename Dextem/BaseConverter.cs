using System.IO;

namespace Dextem
{
    internal abstract class BaseConverter
    {
        internal abstract string Convert(Stream stream);
    }
}
