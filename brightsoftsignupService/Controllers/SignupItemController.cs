using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using brightsoftsignupService.DataObjects;
using brightsoftsignupService.Models;
using Microsoft.Azure.Mobile.Server;

namespace brightsoftsignupService.Controllers
{
    /// <summary>
    /// Signup item controller.
    /// </summary>
    public class SignupItemController : TableController<SignupItem>
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
			DomainManager = new EntityDomainManager<SignupItem>(context, Request, enableSoftDelete: true);
		}

		// GET tables/SignupItem
		public IQueryable<SignupItem> GetAllSignupItem()
		{
			return Query();
		}

		// GET tables/SignupItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public SingleResult<SignupItem> GetSignupItem(string id)
		{
			return Lookup(id);
		}

		// PATCH tables/SignupItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task<SignupItem> PatchSignupItem(string id, Delta<SignupItem> patch)
		{
			return UpdateAsync(id, patch);
		}

		// POST tables/SignupItem
		public async Task<IHttpActionResult> PostSignupItem(SignupItem item)
		{
			SignupItem current = await InsertAsync(item);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		// DELETE tables/SignupItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task DeleteSignupItem(string id)
		{
			return DeleteAsync(id);
		}
    }
}
