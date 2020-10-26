using ImportFile.Core.Inventory.Ports;
using System;
using System.IO;
using System.Text.Json;
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
            return stream.WriteAsync(JsonSerializer.Serialize(obj));
        }

        public Task WriteArrayEndToken(StreamWriter stream)
        {
            return stream.WriteAsync("]");
        }
    }
}
