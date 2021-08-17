using Coterie.Core;
using Microsoft.AspNetCore.Mvc;
using System;


namespace Coterie.Api.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult Success(object result) 
        {
            return Ok(result);
        }

        protected IActionResult Failed<T>(ManagerResponse<T> response) where T : class
        {
            switch (response.ResponseCode)
            {
                case 500:
                    return Problem(response.Message);
                case 400:
                    return new BadRequestObjectResult(new { @ValidationErrors = response.ValidationErrors });
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
