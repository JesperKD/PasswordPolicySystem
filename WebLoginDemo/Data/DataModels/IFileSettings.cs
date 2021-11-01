using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLoginDemo.Data.DataModels
{
    public interface IFileSettings
    {
        public string FileName { get; }

        public string Delimiter { get; }

        public string Path { get; }
    }
}
