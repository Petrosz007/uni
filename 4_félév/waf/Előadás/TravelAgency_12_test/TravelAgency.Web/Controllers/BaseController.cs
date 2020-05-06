using System;
using ELTE.TravelAgency.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ELTE.TravelAgency.Web.Controllers
{
	/// <summary>
	/// Vezrlő ősosztálya.
	/// </summary>
	public class BaseController : Controller
    {
	    // a logikát szolgáltatás osztály mögé rejtjük
        protected readonly ITravelService _travelService;

		// alkalmazás szintű állapot
	    protected readonly ApplicationState _applicationState;

		public BaseController( ITravelService travelService, ApplicationState applicationState)
        {
            _travelService = travelService;
	        _applicationState = applicationState;
        }

	    /// <summary>
	    /// Egy akció meghívása után végrehajtandó metódus.
	    /// </summary>
	    /// <param name="context">Az akció kontextus argumentuma.</param>
	    public override void OnActionExecuted(ActionExecutedContext context)
	    {
		    base.OnActionExecuted(context);

			// a minden oldalról elérhető információkat össze gyűjtjük
			ViewBag.Cities = _travelService.Cities.ToArray();
		    ViewBag.UserCount = _applicationState.UserCount;
		    ViewBag.CurrentGuestName = String.IsNullOrEmpty(User.Identity.Name) ? null : User.Identity.Name;
	    }
	}
}