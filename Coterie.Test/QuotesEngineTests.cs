using Coterie.Core.API;
using Coterie.Core.Quotes;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Coterie.Test
{
    public class QuotesEngineTests
    {
        private IQuotesEngine quotesEngine = new QuotesEngine();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GenerateQuote_Succeeds()
        {
            IQuoteRequest quoteRequest = new QuoteRequest { State = State.TX, Business = BusinessType.Plumber, Revenue = 6000000 };
            double actualPremium = quotesEngine.GenerateQuote(quoteRequest);
            Assert.AreEqual(11316, actualPremium);

        }

        [Test]
        public void ValidateQuoteRequest_Succeeds() 
        {
            IQuoteRequest quoteRequest = new QuoteRequest { State = State.Other, Business = BusinessType.Other, Revenue = 0 };
            List<string> validationErrors = quotesEngine.ValidateQuoteRequest(quoteRequest);
            Assert.AreEqual(3, validationErrors.Count);
            Assert.AreEqual(1, validationErrors.Where(x => x.Contains("Invalid revenue")).Count());
            Assert.AreEqual(1, validationErrors.Where(x => x.Contains("Invalid state")).Count());
            Assert.AreEqual(1, validationErrors.Where(x => x.Contains("Invalid business type")).Count());
        }


    }
}