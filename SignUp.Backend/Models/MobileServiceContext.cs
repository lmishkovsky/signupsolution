using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server.Tables;
using SignUp.Backend.DataObjects;

namespace SignUp.Backend.Models
{
    public class MobileServiceContext : DbContext
    {
        /// <summary>
        /// The name of the connection string.
        /// </summary>
		private const string connectionStringName = "Name=MS_TableConnectionString";

		/// <summary>
		/// Gets or sets the todo items.
		/// </summary>
		/// <value>The todo items.</value>
		public DbSet<GroupItem> TodoItems { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SignUp.Backend.Models.MobileServiceContext"/> class.
        /// </summary>
		public MobileServiceContext() : base(connectionStringName)
		{
            
		}

        /// <summary>
        /// Ons the model creating.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Add(
				new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
					"ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
		}
    }
}
