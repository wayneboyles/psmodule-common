using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace System
{
    public static class SerializationExtensions
    {
        public static byte[] ToBase64EncodedByteArray(this object obj) =>
            Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj))));

        public static TObject FromBase64EncodedByteArray<TObject>(this byte[] bytes) =>
            JsonConvert.DeserializeObject<TObject>(Encoding.UTF8.GetString(bytes));
    }
}
