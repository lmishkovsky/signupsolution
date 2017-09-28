using System;

namespace SignUp.Abstractions
{
    // The first represents a cloud service, which will have a collection of tables we want to access.
    public interface ICloudService
    {
        ICloudTable<T> GetTable<T>() where T : RowData;
    }
}
