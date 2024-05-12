using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;

namespace System.Security.Management.Automation
{
    public static class PsCmdletExtensions
    {
        /// <summary>
        /// Determines if a parameter has been provided by the user.
        /// </summary>
        /// <param name="cmdlet">The executing cmdlet.</param>
        /// <param name="parameterName">The name of the parameter to check.</param>
        /// <returns>True is the parameter was set by the user, otherwise false.</returns>
        public static bool IsParameterBound(this PSCmdlet cmdlet, string parameterName)
        {
            return cmdlet.MyInvocation?.BoundParameters.ContainsKey(parameterName) ?? false;
        }

        /// <summary>
        /// Determines if a parameter has been provided by the user.
        /// </summary>
        /// <typeparam name="TPsCmdlet">Cmdlet type.</typeparam>
        /// <typeparam name="TProp">Property type.</typeparam>
        /// <param name="cmdlet">The executing cmdlet.</param>
        /// <param name="propertySelector">The parameter to check</param>
        /// <returns>True is the parameter was set by the user, otherwise false.</returns>
        internal static bool IsParameterBound<TPsCmdlet, TProp>(this TPsCmdlet cmdlet, Expression<Func<TPsCmdlet, TProp>> propertySelector) where TPsCmdlet : PSCmdlet
        {
            var propName = ((MemberExpression)propertySelector.Body).Member.Name;
            return cmdlet.IsParameterBound(propName);
        }

        internal static void ThrowParameterError(this PSCmdlet cmdlet, string message, params object[] parameters)
        {
            cmdlet.ThrowError(string.Format(message, parameters), ErrorCategory.InvalidArgument);
        }

        internal static void ThrowParameterError(this PSCmdlet cmdlet, string parameterName)
        {
            cmdlet.ThrowError($"Must specify '{parameterName}'.", ErrorCategory.InvalidArgument);
        }

        internal static void ThrowError(this PSCmdlet cmdlet, string message, ErrorCategory errorCategory)
        {
            cmdlet.ThrowTerminatingError(new ErrorRecord(
                new ArgumentException(message), Guid.NewGuid().ToString(), errorCategory, null)
            );
        }

        internal static IEnumerable<T> RunScript<T>(string script)
            => PowerShell.Create().AddScript(script).Invoke<T>();

        internal static IEnumerable<T> RunScript<T>(this PSCmdlet cmdlet, string script)
            => cmdlet?.InvokeCommand.RunScript<T>(script) ?? RunScript<T>(script);

        internal static IEnumerable<T> RunScript<T>(this CommandInvocationIntrinsics cii, string script)
            => cii.InvokeScript(script).Select(o => o?.BaseObject).Where(o => o != null).OfType<T>();
    }
}
