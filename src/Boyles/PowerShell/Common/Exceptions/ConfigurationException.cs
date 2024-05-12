using System;

namespace Boyles.PowerShell.Common.Exceptions
{
    public sealed class ConfigurationException : Exception
    {
        public string Path { get; }

        public ConfigurationException(string message)
            : base(message)
        {

        }

        public ConfigurationException(string path, string message)
            : base(message)
        {
            Path = path;
        }
    }
}
