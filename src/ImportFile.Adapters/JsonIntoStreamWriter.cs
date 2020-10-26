using ImportFile.Core.Inventory.Ports;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ImportFile.Adapters
{
    public class JsonIntoStreamWriter : IWriteJsonIntoStreams
    {
        public Task WriteArrayStartToken(StreamWriter stream)
        {
            return stream.WriteAsync("[");
        }

        public Task WriteArraySeparatorIf(Func<bool> condition, StreamWriter stream)
        {
            condition = condition ?? (() => false);
            if (condition())
            {
                return stream.WriteAsync(",");
            }

            return Task.CompletedTask;
        }

        public Task WriteSerialized(object obj, StreamWriter stream)
        {
            return stream.WriteAsync(JsonConvert.SerializeObject(obj));
        }

        public Task WriteArrayEndToken(StreamWriter stream)
        {
            return stream.WriteAsync("]");
        }
    }
}
