using System;
using Microsoft.WindowsAzure.MobileServices;
using SignUp.Abstractions;

namespace SignUp.Services
{
    public class AzureCloudService : ICloudService
    {
        MobileServiceClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SignUp.Services.AzureCloudService"/> class.
        /// </summary>
        public AzureCloudService()
        {
            client = new MobileServiceClient(Constants.AZURE_APP_SERVICE_URL);
        }

		public ICloudTable<T> GetTable<T>() where T : RowData
		{
			return new AzureCloudTable<T>(client);
		}
    }
}
