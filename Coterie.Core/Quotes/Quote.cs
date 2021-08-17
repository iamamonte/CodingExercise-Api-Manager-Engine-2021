using System;

namespace Coterie.Core.Quotes
{

    public class Quote
    {
        public double Premium { get; set; }
        public double Revenue { get; set; }
        public State State { get; set; }
        public BusinessType Business { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
