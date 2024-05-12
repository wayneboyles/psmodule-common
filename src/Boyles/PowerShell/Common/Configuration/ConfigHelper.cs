using System;
using System.IO;
using System.Text;
using Boyles.PowerShell.Common.Exceptions;

namespace Boyles.PowerShell.Common.Configuration
{
    /// <summary>
    /// Helper to read and write configuration files
    /// </summary>
    public static class ConfigHelper
    {
        /// <summary>
        /// Reads the configuration file and returns a strongly typed
        /// configuration object
        /// </summary>
        /// <typeparam name="T">The configuration file type</typeparam>
        /// <param name="path">The path of the configuration file</param>
        /// <returns>An instance of <see cref="T"/></returns>
        /// <exception cref="ConfigurationException">Exception thrown when a configuration error occurs</exception>
        public static T GetConfiguration<T>(string path) where T : class
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ConfigurationException("The path is null or empty!  Unable to load configuration.");
            }

            if (!File.Exists(path))
            {
                throw new ConfigurationException($"The configuration file at '{path}' could not be found!");
            }

            var fileContents = File.ReadAllText(path, Encoding.UTF8);
            if (string.IsNullOrWhiteSpace(fileContents))
            {
                throw new ConfigurationException($"The configuration file at '{path}' is empty!  Unable to load configuration.");
            }
            
            var bytes = Convert.FromBase64String(fileContents);
            var configuration = bytes.FromBase64EncodedByteArray<T>();

            return configuration;
        }

        /// <summary>
        /// Saves a configuration object to a file
        /// </summary>
        /// <typeparam name="T">The configuration object</typeparam>
        /// <param name="configuration">The configuration object</param>
        /// <param name="path">The full path to save the file</param>
        /// <returns><c>true</c> if successful, otherwise <c>false</c></returns>
        /// <exception cref="ConfigurationException"></exception>
        public static bool SaveConfiguration<T>(T configuration, string path) where T : class
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ConfigurationException("The path is null or empty!  Unable to save configuration.");
            }

            if (configuration is null)
            {
                throw new ConfigurationException("The configuration object is empty or invalid.  Unable to save configuration.");
            }

            try
            {
                using var stream = File.Open(path, FileMode.Create);
                using var writer = new BinaryWriter(stream, Encoding.UTF8, false);
                writer.Write(configuration.ToBase64EncodedByteArray());

                return true;
            }
            catch (Exception ex)
            {
                throw new ConfigurationException($"An error occured while saving the configuration.  Message is '{ex.Message}'");
            }
        }

        public static bool EraseConfiguration(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
