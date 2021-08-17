using Coterie.Core.Quotes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Coterie.Core.API
{
   public class QuoteRequest : IQuoteRequest
    {
        [Required]
        public double Revenue { get; set; }
        [Required]
        public State State { get; set; }
        [Required]
        public BusinessType Business { get; set; }
    }
}
