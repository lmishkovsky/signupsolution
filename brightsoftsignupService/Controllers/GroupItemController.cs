using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using brightsoftsignupService.DataObjects;
using brightsoftsignupService.Extensions;
using brightsoftsignupService.Models;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;

namespace brightsoftsignupService.Controllers
{
    /// <summary>
    /// Group item controller.
    /// </summary>
    public class GroupItemController : TableController<GroupItem>
    {
		// public string UserId => ((ClaimsPrincipal)User).FindFirst(ClaimTypes.NameIdentifier).Value;

		/// <summary>
		/// Initialize the specified controllerContext.
		/// </summary>
		/// <returns>The initialize.</returns>
		/// <param name="controllerContext">Controller context.</param>
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			brightsoftsignupContext context = new brightsoftsignupContext();
            DomainManager = new EntityDomainManager<GroupItem>(context, Request, enableSoftDelete: true);
		}

		// GET tables/GroupItem
        // [Authorize]
		public IQueryable<GroupItem> GetAllGroupItem()
		{
            // System.Diagnostics.Debug.WriteLine("this.User: ", this.User);

			return Query();
		}

		// GET tables/GroupItem
		public IQueryable<GroupItem> GetAllGroupItem(string groupCode)
		{
            // filter applied through an extension method
            return Query().PerGroupCodeFilter(groupCode);
		}

		// GET tables/GroupItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public SingleResult<GroupItem> GetGroupItem(string id)
		{
			return Lookup(id);
		}

		// PATCH tables/GroupItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task<GroupItem> PatchGroupItem(string id, Delta<GroupItem> patch)
		{
			return UpdateAsync(id, patch);
		}

		// POST tables/GroupItem
		public async Task<IHttpActionResult> PostGroupItem(GroupItem item)
		{
			GroupItem current = await InsertAsync(item);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		// DELETE tables/GroupItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task DeleteGroupItem(string id)
		{
			return DeleteAsync(id);
		}
    }
}
