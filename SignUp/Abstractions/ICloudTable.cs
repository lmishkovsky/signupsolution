using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignUp.Abstractions
{
	public interface ICloudTable<T> where T : RowData
	{
		Task<T> CreateItemAsync(T item);
		Task<T> ReadItemAsync(string id);
		Task<T> UpdateItemAsync(T item);
		Task DeleteItemAsync(T item);

		Task<ICollection<T>> ReadAllItemsAsync();
	}
}
