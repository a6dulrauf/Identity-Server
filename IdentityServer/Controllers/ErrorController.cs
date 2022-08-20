using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nami.DXP.Common;
using System;

namespace Nami.DXP.IdentityServer
{
    public class ErrorController : BaseController
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger, IOptions<IdentityServerOptions> options)
                              : base(logger, options)
        {
            _logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            string viewName = "500";

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    _logger.LogWarning($"404 Error Occured. Path = {statusCodeResult.OriginalPath}" +
                        $" and QueryString = {statusCodeResult.OriginalQueryString}");
                    viewName = "404";
                    break;
            }

            return View(viewName);
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
         {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            _logger.LogError($"Source: {exceptionDetails.Error.Source}");
            _logger.LogError($"Message: {exceptionDetails.Error.Message}");
            _logger.LogError($"The path {exceptionDetails.Path} threw an exception {exceptionDetails.Error}");

            HandleException(exceptionDetails.Error);

            return View("Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

        private void HandleWebAppException(WebAppException exception)
        {
            ViewBag.ErrorTitle = "Error";
            ViewBag.ErrorMessage = exception.Message;
        }

        private void HandleException(Exception exception)
        {
            if(exception is WebAppException)
            {
                ViewBag.ErrorTitle = "Error";
                ViewBag.ErrorMessage = exception.Message;
            }
            else
            {
                if (_config.ExposeInternalError)
                {
                    ViewBag.ErrorTitle = "Exception";
                    ViewBag.ErrorMessage = exception.Message;
                }
            }
        }
    }
}
