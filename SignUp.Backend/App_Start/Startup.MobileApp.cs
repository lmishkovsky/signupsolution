using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Owin;
using SignUp.Backend.DataObjects;
using SignUp.Backend.Models;

namespace SignUp.Backend.App_Start
{
    /// <summary>
    /// Startup.
    /// </summary>
    public partial class Startup
    {
		/// <summary>
		/// The ConfigureMobileApp() method is called to configure Azure Mobile Apps when the service starts. The code tells the SDK that we want to use tables and that those tables are backed with Entity Framework..
		/// </summary>
		/// <param name="app">App.</param>
		public static void ConfigureMobileApp(IAppBuilder app)
		{
			var config = new HttpConfiguration();
			var mobileConfig = new MobileAppConfiguration();

			mobileConfig
				.AddTablesWithEntityFramework()
				.ApplyTo(config);

			Database.SetInitializer(new MobileServiceInitializer());

			app.UseWebApi(config);
		}

		/// <summary>
		/// We also want to initialize the database that we are going to use. That database is going to use a DbContext called MobileServiceContext. The initialization code will create the database and seed it with two new items if it doesn't already exist. If it exists, then we assume that we don't need to seed the database with data.
		/// </summary>
		public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
		{
            /// <summary>
            /// 
            /// </summary>
            /// <returns>The seed.</returns>
            /// <param name="context">Context.</param>
			protected override void Seed(MobileServiceContext context)
			{
				List<GroupItem> todoItems = new List<GroupItem>
        		{
        			new GroupItem { Id = Guid.NewGuid().ToString(), GroupCode = "VAMT" },
        			new GroupItem { Id = Guid.NewGuid().ToString(), GroupCode = "SSMF" }
        		};

                foreach (GroupItem todoItem in todoItems)
				{
                    context.Set<GroupItem>().Add(todoItem);
				}

				base.Seed(context);
			}
		}
    }
}
