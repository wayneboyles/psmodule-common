using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Boyles.PowerShell.Common
{
    /// <summary>
    /// Helper class to perform common checks on strings and objects
    /// </summary>
    [DebuggerStepThrough]
    public static class Ensure
    {
        #region Not Null Checks

        public static T NotNull<T>(T? value, string parameterName)
        {
            return NotNull(value, parameterName, string.Empty);
        }

        public static T NotNull<T>(T? value, string parameterName, string message)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, message);
            }

            if (value is string s)
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    throw new ArgumentNullException(parameterName, message);
                }
            }

            return value;
        }

        public static string NotNull(string? value, string parameterName, int maxLength = int.MaxValue, int minLength = 0)
        {
            if (value == null || string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{parameterName} can not be null!", parameterName);
            }

            if (value.Length > maxLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or less than {maxLength}!", parameterName);
            }

            if (minLength > 0 && value.Length < minLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or greater than {minLength}!", parameterName);
            }

            return value;
        }

        public static string NotNullOrWhiteSpace(string? value, string parameterName, int maxLength = int.MaxValue, int minLength = 0)
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentException($"{parameterName} can not be null, empty or white space!", parameterName);
            }

            if (value!.Length > maxLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or less than {maxLength}!", parameterName);
            }

            if (minLength > 0 && value!.Length < minLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or greater than {minLength}!", parameterName);
            }

            return value;
        }

        public static string NotNullOrEmpty(string? value, string parameterName, int maxLength = int.MaxValue, int minLength = 0)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException($"{parameterName} can not be null or empty!", parameterName);
            }

            if (value!.Length > maxLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or less than {maxLength}!", parameterName);
            }

            if (minLength > 0 && value!.Length < minLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or greater than {minLength}!", parameterName);
            }

            return value;
        }

        public static ICollection<T> NotNullOrEmpty<T>(ICollection<T>? value, string parameterName)
        {
            if (value == null || value.Count <= 0)
            {
                throw new ArgumentException(parameterName + " can not be null or empty!", parameterName);
            }

            return value;
        }

        #endregion

        /// <summary>
        /// Ensures the <see cref="T"/> value is not null or default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static T NotDefaultOrNull<T>(T? value, string parameterName) where T : struct
        {
            if (value == null)
            {
                throw new ArgumentException($"{parameterName} is null!", parameterName);
            }

            if (value.Value.Equals(default(T)))
            {
                throw new ArgumentException($"{parameterName} has a default value!", parameterName);
            }

            return value.Value;
        }
    }
}
