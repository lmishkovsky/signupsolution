using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using SignUp.Backend.DataObjects;
using SignUp.Backend.Models;

namespace SignUp.Backend.Controllers
{
    /// <summary>
    /// Group item controller.
    /// </summary>
    public class GroupItemController : TableController<GroupItem>
    {
        /// <summary>
        /// Initialize the specified controllerContext.
        /// </summary>
        /// <returns>The initialize.</returns>
        /// <param name="controllerContext">Controller context.</param>
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			MobileServiceContext context = new MobileServiceContext();
			DomainManager = new EntityDomainManager<GroupItem>(context, Request);
		}

		// GET tables/TodoItem
		public IQueryable<GroupItem> GetAllTodoItems() => Query();

		// GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public SingleResult<GroupItem> GetTodoItem(string id) => Lookup(id);

		// PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task<GroupItem> PatchTodoItem(string id, Delta<GroupItem> patch) => UpdateAsync(id, patch);

		// POST tables/TodoItem
		public async Task<IHttpActionResult> PostTodoItem(GroupItem item)
		{
			GroupItem current = await InsertAsync(item);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		// DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task DeleteTodoItem(string id) => DeleteAsync(id);
    }
}
