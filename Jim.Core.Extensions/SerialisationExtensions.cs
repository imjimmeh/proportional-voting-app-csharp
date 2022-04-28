using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Jim.Core.Extensions
{
    public static class SerialisationExtensions
    {
        public static bool TryDeserialise<T>(this string toDeserialize, out T? result, bool handleException = true)
            where T : class
        {
            try
            {
                if (string.IsNullOrEmpty(toDeserialize))
                    throw new ArgumentNullException(nameof(toDeserialize));

                result = Deserialize<T>(toDeserialize);

                return result != null;
            }
            catch
            {
                if (!handleException)
                    throw;

                result = null;
                return false;
            }
        }

        public static T Deserialize<T>(this string result) where T : class
        {
            try
            {
                var deserialised = JsonSerializer.Deserialize<T>(result);

                if (deserialised != null)
                    return deserialised;

                throw new Exception($"Object null after deserialization - likely incorrect type passed, or data issue");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error serializing {result} to {typeof(T)}", ex);
            }
        }

        public static string Serialise<T>(this T toSerialise)
            where T : class
        {
            if (toSerialise == null)
                throw new ArgumentNullException(nameof(toSerialise));

            try
            {
                var serialised = JsonSerializer.Serialize(toSerialise);

                if (!string.IsNullOrEmpty(serialised))
                    return serialised;

                throw new Exception($"String null after serialization");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error serializing {toSerialise} to {typeof(T)}", ex);
            }
        }

    }
}
