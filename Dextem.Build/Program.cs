using System.IO;
using System.Text;

namespace Dextem.Build
{
    /// <summary>
    /// Meant to run Post-build, Dextem.Build is a console application that uses the default Dextem API to create a markdown file based on a project's XML Documentation file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Produce a markdown file with the given name and xml documentation output.
        /// </summary>
        /// <param name="args">Required arguments for Dextem.Build. Read Remarks for argument details.</param>
        /// <remarks>
        /// Dextem.Build.exe is meant to run as a Post-Build event. It can also be started from the command prompt. &lt;br&gt; &lt;br&gt;
        /// `Dextem.Build.exe [XmlDocumentationFile] [MarkdownFile]` &lt;br&gt; &lt;br&gt;
        /// **`XmlDocumentationFile`** The xml documentation file from which markdown content should be retrieved &lt;br&gt;
        /// **`MarkdownFile`** The markdown file to generate &lt;br&gt;
        /// &lt;br&gt;
        /// Examples: &lt;br&gt;
        /// `Dextem.Build.exe MyProject.xml MyProject.md` &lt;br&gt;
        /// `Dextem.Build.exe $(TargetDir)$(ProjectName).XML $(ProjectName).md` &lt;br&gt;
        /// </remarks>
        static void Main(string[] args)
        {
            var md = string.Empty;

            using (var file = new FileStream(args[0], FileMode.Open))
            {
                var dextem = new DextemConverter();

                md = dextem.Convert(file);
            }

            var bytes = Encoding.ASCII.GetBytes(md);

            using (var file = new FileStream(args[1], FileMode.Create))
            {
                file.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
