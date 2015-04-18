using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Dextem
{
    /// <summary>
    /// Controls name processing of method 'M:' and type 'T:' &lt;member&gt; nodes. This class cannot be inherited.
    /// </summary>
    public sealed class MethodTypeProcessor : BaseProcessor
    {
        /// <summary>
        /// Creates a new instance of MethodTypeProcessor using the given ProcessorRegistry.
        /// </summary>
        /// <param name="registry">The ProcessorRegistry instance to use.</param>
        public MethodTypeProcessor(ProcessorRegistry registry) : base(registry) { }

        /// <summary>
        /// Executes name processing of the curernt method 'M:' or type 'T:' &lt;member&gt; element.
        /// </summary>
        /// <param name="writer">The current StringWriter to use.</param>
        /// <param name="root">The current root element to process.</param>
        /// <param name="context">The current processing context.</param>
        /// <returns>The updated processing context.</returns>
        public override Dictionary<XName, string> Process(StringWriter writer, XElement root, Dictionary<XName, string> context)
        {
            var memberName = root.Attribute(XName.Get("name")).Value;
            char memberType = memberName[0];

            if (memberType == 'M')
            {
                memberName = RearrangeTypeParametersInContext(root, memberName, context, false);
                memberName = RearrangeParametersInContext(root, memberName, context);
            }

            context["memberName"] = memberName;

            if (memberType == 'T')
            {
                var typeNameStartIndex = 3 + context["assembly"].Length;
                var scrubbedName = this.ReplaceForTypeParameters(root, memberName, false, context);
                var shortMemberName = scrubbedName.Substring(typeNameStartIndex);
                writer.WriteLine("\n## {0}\n", shortMemberName);
                context["typeName"] = shortMemberName;
            }

            context["memberType"] = memberType.ToString();

            context["lastNode"] = memberName;

            return base.Process(writer, root, context);
        }

        private string RearrangeParametersInContext(XElement methodMember, string memberName, Dictionary<XName, string> context)
        {
            string methodPrototype = memberName;
            Match match = Regex.Match(methodPrototype, "\\((.*)\\)");
            string parameterString = match.Groups[1].Value.Replace(" ", "");
            string[] parameterTypes = parameterString.Split(',');

            if (parameterTypes.Length == 0)
            {
                // nothing to do...
                return methodPrototype;
            }

            List<XElement> paramElems = new List<XElement>(methodMember.Elements("param"));
            if (parameterTypes.Length != paramElems.Count)
            {
                // the parameter count do not match, we can't do the rearrangement.
                return methodPrototype;
            }

            string newParamString = "";
            for (int i = 0; i < paramElems.Count; i++)
            {
                XElement paramElem = paramElems[i];
                string paramName = paramElem.Attribute(XName.Get("name")).Value;
                string paramType = parameterTypes[i];
                if (newParamString != "")
                {
                    newParamString += ", ";
                }
                newParamString += paramName;
                context[paramName] = paramType;
            }

            string newMethodPrototype = Regex.Replace(methodPrototype,
                "\\(.*\\)",
                "(" + newParamString + ")");

            return newMethodPrototype;
        }

        private string RearrangeTypeParametersInContext(XElement member, string memberName, Dictionary<XName, string> context, bool skipReplace)
        {
            var methodPrototype = memberName;
            if (!skipReplace)
            {
                methodPrototype = this.ReplaceForTypeParameters(member, memberName, true, context);
            }

            var matches = Regex.Matches(methodPrototype, "\\{(`\\d)+}"); //Matches: {'0} && {'1'2} //M:GraphExec.BaseEventAggregator.GetEventType(GraphExec.IHandle{'0})

            var typedParams = matches.ToList();
            var replaceTypedParamString = typedParams.Select(x => x.Groups[0].Value);

            if (!replaceTypedParamString.Any())
            {
                // nothing to do...
                return methodPrototype;
            }

            var paramElems = new List<XElement>(member.Elements("typeparam"));

            var newParamString = "";
            var indexList = new List<int>();

            foreach (var replaceString in replaceTypedParamString)
            {
                newParamString = "&lt;";

                var scrubBrackets = replaceString.Substring(1, replaceString.Length - 3);

                indexList = scrubBrackets.Split('\'').Cast<int>().ToList(); // "1, 2"

                if (indexList.Count() <= paramElems.Count)
                {
                    foreach (var index in indexList)
                    {
                        if (newParamString != "&lt;")
                        {
                            newParamString += ", ";
                        }

                        var typeParam = paramElems[index];

                        var paramType = typeParam.Attribute(XName.Get("name")).Value;

                        newParamString += paramType;
                    }
                }
                else
                {
                    newParamString += "*Unknown*";
                }

                newParamString += "&gt;";

                methodPrototype = methodPrototype.Replace(replaceString, newParamString);
            }

            return methodPrototype;
        }

        private string ReplaceForTypeParameters(XElement methodMember, string memberName, bool methodType, Dictionary<XName, string> context)
        {
            string methodPrototype = memberName;
            var matches = Regex.Matches(methodPrototype, "\\`(\\d)"); //Matches: '1 and 1 //M:GraphExec.BaseEventAggregator.GetEventType`1

            // Match 1 = Type Parameter Count ('1)

            if (matches.Count == 0)
            {
                return methodPrototype;
            }

            var typeParamCount = Convert.ToInt32(matches[0].Groups[1].Value);

            List<XElement> paramElems = new List<XElement>(methodMember.Elements("typeparam"));
            if (typeParamCount != paramElems.Count)
            {
                System.Diagnostics.Debug.WriteLine("Type Parameters and TypeParamList not equal for replacing generic types' type parameters.");
                // the parameter count do not match, we can't do the rearrangement.
                return methodPrototype;
            }

            string newParamString = "";
            for (int i = 0; i < paramElems.Count; i++)
            {
                XElement paramElem = paramElems[i];
                string paramType = paramElem.Attribute(XName.Get("name")).Value;
                if (newParamString != "")
                {
                    newParamString += ", ";
                }
                newParamString += paramType;
                context[paramType] = paramType;
            }

            var paramMatches = Regex.Matches(methodPrototype, "\\{``\\d}");
            if (paramMatches.Count > 0) // {``0} and {``1} and {``2``3}
            {
                methodPrototype = this.RearrangeTypeParametersInContext(methodMember, methodPrototype, context, true);
            }

            if (methodType)
            {
                string newMethodPrototype = Regex.Replace(methodPrototype,
                    "\\``\\d",
                    "&lt;" + newParamString + "&gt;");

                return newMethodPrototype;
            }
            else
            {
                string newMethodPrototype = Regex.Replace(methodPrototype,
                    "\\`\\d",
                    "&lt;" + newParamString + "&gt;");

                return newMethodPrototype;
            }
        }
    }
}
