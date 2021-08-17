namespace Coterie.Core.Quotes
{
    public interface IQuoteRequest
    {
        double Revenue { get; set; }
        State State { get; set; }
        BusinessType Business { get; set; }
    }
}
