using Rover.Model;
using Rover.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Rover.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRoverNavigationService RoverNavigationService;
        public HomeController(IRoverNavigationService _RoverNavigationService)
        {
            RoverNavigationService = _RoverNavigationService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string RoverInputModel)
        {
            RoverOutputViewModel finalRoverCoordinates = new RoverOutputViewModel();
            try
            {
                //TODO - Serverside validation
                var errorMessage = ValidateInputValues(RoverInputModel);
                                
                if (String.IsNullOrEmpty(errorMessage))
                {
                    finalRoverCoordinates.RoverOutputs =  RoverNavigationService.GetRoverCoordinates(RoverInputModel);
                    
                }
                finalRoverCoordinates.ErrorMessage = errorMessage;                
            }
            catch (Exception ex)
            {
                //Log the error message
                finalRoverCoordinates.ErrorMessage = "Error occured.Please contact Administrator";
            }
            return View(finalRoverCoordinates);
        }
        /// <summary>
        /// TODO Serverside validation
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public string ValidateInputValues(string values)
        {
            var errorMessage = string.Empty;

            return errorMessage;
        }
    }
}