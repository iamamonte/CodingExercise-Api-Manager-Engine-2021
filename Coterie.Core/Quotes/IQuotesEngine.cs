using System.Collections.Generic;

namespace Coterie.Core.Quotes
{
    public interface IQuotesEngine
    {
        List<string> ValidateQuoteRequest(IQuoteRequest quoteRequest);
        double GenerateQuote(IQuoteRequest quoteRequest);
    }
}
