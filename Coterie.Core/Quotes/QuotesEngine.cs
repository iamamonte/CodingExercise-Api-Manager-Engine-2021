using System;
using System.Collections.Generic;

namespace Coterie.Core.Quotes
{
    public class QuotesEngine : IQuotesEngine
    {
        private Dictionary<BusinessType, double> businessTypeFactors = new Dictionary<BusinessType, double> 
        {
            { BusinessType.Architect, 1 },
            { BusinessType.Plumber, .5 },
            { BusinessType.Programmer, 1.25}
        };

        private Dictionary<State, double> stateFactors = new Dictionary<State, double>
        {
            { State.OH, 1 },
            { State.FL, 1.2 },
            { State.TX, .943}
        };

        private const double HAZARD_FACTOR = 4;

        public double GenerateQuote(IQuoteRequest quoteRequest)
        {
            double basePremium = Math.Ceiling(quoteRequest.Revenue / 1000.00);
            double stateFactor = stateFactors[quoteRequest.State];
            double businessFactor = businessTypeFactors[quoteRequest.Business];

            return (stateFactor * businessFactor * basePremium * HAZARD_FACTOR);
        }

        public List<string> ValidateQuoteRequest(IQuoteRequest quoteRequest)
        {
            List<string> validationErrors = new List<string>();
            // check business types 
            if (!businessTypeFactors.ContainsKey(quoteRequest.Business))
            {
                validationErrors.Add($"Invalid business type: {Enum.GetName(typeof(BusinessType), quoteRequest.Business)}.");
            }

            // check state
            if (!stateFactors.ContainsKey(quoteRequest.State))
            {
                validationErrors.Add($"Invalid state: {Enum.GetName(typeof(State), quoteRequest.State)}.");
            }
            // validate revenue

            if (quoteRequest.Revenue <= 0) 
            {
                validationErrors.Add($"Invalid revenue.");
            }
            return validationErrors;
        }
    }
}
