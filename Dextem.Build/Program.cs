using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dextem.Build
{
    class Program
    {
        static void Main(string[] args)
        {
            var md = string.Empty;

            using (var file = new FileStream(args[0], FileMode.Open))
            {
                var dextem = new DextemConverter();

                md = dextem.Convert(file);
            }

            var bytes = ASCIIEncoding.UTF8.GetBytes(md);

            using (var file = new FileStream("Dextem.md", FileMode.Create))
            {
                file.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
