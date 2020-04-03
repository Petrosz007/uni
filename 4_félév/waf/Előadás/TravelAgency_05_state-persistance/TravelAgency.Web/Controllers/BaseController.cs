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
	    // a logikát modell osztályok mögé rejtjük
		protected readonly IAccountService _accountService;
        protected readonly ITravelService _travelService;

        public BaseController(IAccountService accountService, ITravelService travelService)
        {
            _accountService = accountService;
            _travelService = travelService;
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
		    ViewBag.UserCount = _accountService.UserCount;

		    if (_accountService.CurrentUserName != null)
			    ViewBag.CurrentGuestName = _accountService.GetGuest(_accountService.CurrentUserName).Name;
		}
	}
}