using System;
using System.Collections.Generic;
using System.Linq;

namespace Coterie.Core.Quotes
{
    public class QuotesManager : IQuotesManager
    {
        private readonly IQuotesEngine _quotesEngine;

        public QuotesManager(IQuotesEngine quotesEngine)
        {
            _quotesEngine = quotesEngine;
        }
        public ManagerResponse<Quote> GenerateQuote(IQuoteRequest request)
        {
            try
            {
                List<string> validationErrors = _quotesEngine.ValidateQuoteRequest(request);
                if (validationErrors.Any())
                {
                    return new ManagerResponse<Quote>(validationErrors);
                }

                double calculatedPremium = _quotesEngine.GenerateQuote(request);

                return new ManagerResponse<Quote>(new Quote
                {
                    Business = request.Business,
                    Revenue = request.Revenue,
                    State = request.State,
                    ValidTo = DateTime.Now.AddDays(14),
                    Premium = calculatedPremium
                });
            }
            catch (Exception e)
            {
                return new ManagerResponse<Quote>(e);
            }
        }
    }
}
