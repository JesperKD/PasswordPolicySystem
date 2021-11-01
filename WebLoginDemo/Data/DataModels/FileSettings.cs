using System;

namespace WebLoginDemo.Data.DataModels
{
    public class FileSettings : IFileSettings
    {
        public string FileName { get; set; }

        public string Delimiter { get; set; }

        public string Path
        {
            get
            {
                return System.IO.Path.Combine(Environment.CurrentDirectory, FileName);
            }
        }

    }
}
