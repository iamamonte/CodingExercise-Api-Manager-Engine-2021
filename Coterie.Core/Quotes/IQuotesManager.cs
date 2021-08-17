namespace Coterie.Core.Quotes
{
    public interface IQuotesManager
    {
        ManagerResponse<Quote> GenerateQuote(IQuoteRequest request);
    }
}
