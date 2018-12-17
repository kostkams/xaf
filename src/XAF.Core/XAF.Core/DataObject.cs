using System;

namespace XAF.Core
{
    public abstract class DataObject : IDataObject
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime LastModificationTime { get; set; }
    }
}