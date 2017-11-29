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

namespace brightsoftsignupService.Controllers
{
	/// <summary>
	/// Forum item controller.
	/// </summary>
	public class ForumItemController : TableController<ForumItem>
	{
		/// <summary>
		/// Initialize the specified controllerContext.
		/// </summary>
		/// <returns>The initialize.</returns>
		/// <param name="controllerContext">Controller context.</param>
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			brightsoftsignupContext context = new brightsoftsignupContext();
			DomainManager = new EntityDomainManager<ForumItem>(context, Request, enableSoftDelete: true);
		}

		// GET tables/ForumItem
		public IQueryable<ForumItem> GetAllForumItem()
		{
			return Query();
		}

		// GET tables/ForumItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public SingleResult<ForumItem> GetForumItem(string id)
		{
			return Lookup(id);
		}

		
		public IQueryable<ForumItem> GetAllForumItem(string groupCode, DateTime dtEventDate)
		{
			// filter applied through an extension method
			return Query().PerEventMessageFilter(groupCode, dtEventDate);
		}

		// PATCH tables/ForumItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task<ForumItem> PatchForumItem(string id, Delta<ForumItem> patch)
		{
			return UpdateAsync(id, patch);
		}

		// POST tables/ForumItem
		public async Task<IHttpActionResult> PostSignupItem(ForumItem item)
		{
			ForumItem current = await InsertAsync(item);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		// DELETE tables/ForumItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task DeleteForumItem(string id)
		{
			return DeleteAsync(id);
		}
	}
}
