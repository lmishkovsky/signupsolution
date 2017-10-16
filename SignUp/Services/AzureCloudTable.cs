using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SignUp.Abstractions;

namespace SignUp.Services
{
    /// <summary>
    /// Azure cloud table.
    /// </summary>
	public class AzureCloudTable<T> : ICloudTable<T> where T : RowData
    {
        /// <summary>
        /// The client.
        /// </summary>
		MobileServiceClient client;

        /// <summary>
        /// The table.
        /// </summary>
		IMobileServiceTable<T> table;

        public IMobileServiceTable<T> GetTheMobileServiceTable() {
            return this.table;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SignUp.Services.AzureCloudTable`1"/> class.
        /// </summary>
        /// <param name="client">Client.</param>
		public AzureCloudTable(MobileServiceClient client)
        {
            this.client = client;
            this.table = client.GetTable<T>();
        }

        /// <summary>
        /// Creates the item async.
        /// </summary>
        /// <returns>The item async.</returns>
        /// <param name="item">Item.</param>
		public async Task<T> CreateItemAsync(T item)
        {
            await table.InsertAsync(item);
            return item;
        }

        /// <summary>
        /// Deletes the item async.
        /// </summary>
        /// <returns>The item async.</returns>
        /// <param name="item">Item.</param>
		public async Task DeleteItemAsync(T item)
        {
            await table.DeleteAsync(item);
        }

        /// <summary>
        /// Reads all items async.
        /// </summary>
        /// <returns>The all items async.</returns>
		public async Task<ICollection<T>> ReadAllItemsAsync()
        {
            return await table.ToListAsync();
        }

        /// <summary>
        /// Reads the item async.
        /// </summary>
        /// <returns>The item async.</returns>
        /// <param name="id">Identifier.</param>
		public async Task<T> ReadItemAsync(string id)
        {
            return await table.LookupAsync(id);
        }

        /// <summary>
        /// Updates the item async.
        /// </summary>
        /// <returns>The item async.</returns>
        /// <param name="item">Item.</param>
		public async Task<T> UpdateItemAsync(T item)
        {
            await table.UpdateAsync(item);
            return item;
        }
    }
}
