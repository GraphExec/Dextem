using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Dextem
{
    /// <summary>
    /// A static class to use for validating arguments of methods
    /// </summary>
    public static class Args
    {
        /// <summary>
        /// A static method to use for validating method arguments of a certain type
        /// </summary>
        /// <typeparam name="TObject">The type of argument</typeparam>
        /// <param name="selector">The argument selector</param>
        public static void IsNotNull<TObject>(Expression<Func<TObject>> selector)
        {
            if (selector == null) throw new ArgumentNullException("selector");

            var memberSelector = selector.Body as MemberExpression;
            if (memberSelector == null) throw new ArgumentNullException("selector");

            var constantSelector = memberSelector.Expression as ConstantExpression;
            if (constantSelector == null) throw new ArgumentNullException("selector");

            var field = memberSelector.Member as FieldInfo;
            var value = field.GetValue(constantSelector.Value);

            if (value == null)
            {
                throw new ArgumentNullException(memberSelector.Member.Name);
            }
        }

        /// <summary>
        /// A static method to use for validating multiple arguments of public methods
        /// </summary>
        /// <param name="selectors">The multiple argument selectors</param>
        public static void IsNotNull(params Expression<Func<object>>[] selectors)
        {
            if (selectors == null) throw new ArgumentNullException("selectors");

            if (!selectors.Any()) throw new ArgumentNullException("selectors");

            foreach (var selector in selectors)
            {
                Args.IsNotNull(selector);
            }
        }
    }
}
