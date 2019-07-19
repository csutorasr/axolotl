using System;
using System.IO;

namespace Axolotl.Abstractions
{
    public interface IAxolotlWriter
    {
        void WriteApi(FlatApiDescription flatApiDescription, StreamWriter streamWriter);
    }
}
