using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using brightsoftsignupService.DataObjects;

namespace brightsoftsignupService.Models
{
    public class brightsoftsignupContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        /// <summary>
        /// The name of the connection string.
        /// </summary>
        private const string connectionStringName = "Name=MS_TableConnectionString";

        /// <summary>
        /// Initializes a new instance of the <see cref="T:brightsoftsignupService.Models.brightsoftsignupContext"/> class.
        /// </summary>
        public brightsoftsignupContext() : base(connectionStringName)
        {
            
        } 

        /// <summary>
        /// Gets or sets the todo items.
        /// </summary>
        /// <value>The todo items.</value>
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<GroupItem> GroupItems { get; set; }
        public DbSet<SignupItem> SignupItems { get; set; }
        public DbSet<ForumItem> ForumItems { get; set; }

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
