using System;
using System.IO;
using System.Threading.Tasks;

namespace ImportFile.Core.Inventory.Ports
{
    public interface IWriteJsonIntoStreams
    {
        Task WriteArrayStartToken(StreamWriter stream);
        Task WriteArraySeparatorIf(Func<bool> condition, StreamWriter stream);
        Task WriteSerialized(object obj, StreamWriter stream);
        Task WriteArrayEndToken(StreamWriter stream);
    }
}