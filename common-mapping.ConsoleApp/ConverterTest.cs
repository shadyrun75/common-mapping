using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace common_mapping
{ 
    public class ConverterTest
    {
        /// <summary>
        /// Testing converting json data from file to T models
        /// </summary>
        /// <typeparam name="T">Model for convert</typeparam>
        /// <param name="fileName">File path to convert</param>
        /// <param name="logger"></param>
        public static void Main<T>(string fileName, ILogger logger = null)
            where T: class
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new JsonStringEnumConverter() }
                };

                using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
                {
                    var json = reader.ReadToEnd();
                    logger?.LogInformation(json);
                    var obj = JsonSerializer.Deserialize<T>(json, options);
                    logger?.LogInformation(obj.ToString());
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, ex.Message);
            }
        }
    }
}
