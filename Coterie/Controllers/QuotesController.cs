using Coterie.Api.Controllers;
using Coterie.Core;
using Coterie.Core.API;
using Coterie.Core.Quotes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coterie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : ApiControllerBase
    {
        private readonly ILogger _logger;
        private readonly IQuotesManager _quotesManager;
        
        public QuotesController(ILogger<Quote> logger, IQuotesManager quoteManager)
        {
            _logger = logger;
            _quotesManager = quoteManager;
        }

        [HttpGet]
        public ActionResult Get() 
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(QuoteResponse), StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] QuoteRequest quoteRequest)
        {
            ManagerResponse<Quote> response = _quotesManager.GenerateQuote(quoteRequest);
            if (!response.Succeeded)
            {
                _logger.LogDebug(response.Message, response.ValidationErrors, quoteRequest);
                return Failed(response);
            }
            return Ok(new QuoteResponse { Premium = response.Result.Premium } );
        }
    }
}
