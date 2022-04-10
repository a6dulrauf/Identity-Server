using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nami.DXP.Common;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Nami.DXP.IdentityServer
{
    public class BaseController : Controller
    {
        private readonly ILogger _logger;
        protected readonly IdentityServerOptions _config;

        public BaseController(ILogger logger, IOptions<IdentityServerOptions> options)
        {
            _logger = logger;
            _config = options.Value;
        }

        protected async Task<IActionResult> ExecuteAjaxRequest<T>(T funcData, Func<T, Task<IActionResult>> process)
        {
            try
            {
                if (ModelState.IsValid)
                    return await process(funcData);
                
                string erroMessages = String.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors)
                                                           .Select(v => v.ErrorMessage + " " + v.Exception));
                return BadRequest(erroMessages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                string errorMessage = _config.ExposeInternalError ? ex.Message : ErrorMessages.DefaultErrorMessage;
                return StatusCode((int)HttpStatusCode.InternalServerError, errorMessage);
            }
        }

    }
}
