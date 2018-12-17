using System;

namespace XAF.Core
{
    public interface IDataObject
    {
        string Id { get; set; }

        string Type { get; set; }

        string Name { get; set; }

        DateTime CreationTime { get; set; }

        DateTime LastModificationTime { get; set; }
    }
}