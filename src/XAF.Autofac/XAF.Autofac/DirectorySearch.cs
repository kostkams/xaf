using System.IO;

namespace XAF.Autofac
{
    public class DirectorySearch
    {
        public DirectoryInfo Directory { get; set; }
        public bool Recursive { get; set; }

        public override string ToString()
        {
            return Directory.FullName;
        }
    }
}