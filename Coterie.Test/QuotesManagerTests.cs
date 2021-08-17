using Coterie.Core;
using Coterie.Core.API;
using Coterie.Core.Quotes;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Coterie.Test
{
    public class QuotesManagerTest
    {
        private Mock<IQuotesEngine> mockQuotesEngine;
       
        [Test]
        public void GenerateQuote_Succeeds()
        {
            mockQuotesEngine = new Mock<IQuotesEngine>();
            mockQuotesEngine.Setup(x => x.ValidateQuoteRequest(It.IsAny<IQuoteRequest>()))
                .Returns(new List<string>());
            mockQuotesEngine.Setup(x => x.GenerateQuote(It.IsAny<IQuoteRequest>()))
                .Returns(12345);

            IQuotesManager quotesManager = new QuotesManager(mockQuotesEngine.Object);

            IQuoteRequest quoteRequest = new QuoteRequest { State = State.TX, Business = BusinessType.Plumber, Revenue = 6000000 };
            ManagerResponse<Quote> response = quotesManager.GenerateQuote(quoteRequest);

            Assert.IsTrue(response.Succeeded);
            Quote actualQuote = response.Result;

            Assert.AreEqual(12345, actualQuote.Premium);
            Assert.AreEqual(quoteRequest.Business, actualQuote.Business);
            Assert.AreEqual(quoteRequest.State, actualQuote.State);
            Assert.AreEqual(quoteRequest.Revenue, actualQuote.Revenue);

            // verify date
            DateTime expectedValidTo = DateTime.Now.AddDays(14);
            Assert.That(expectedValidTo, Is.EqualTo(actualQuote.ValidTo).Within(3).Seconds);

        }

        [Test]
        public void GenerateQuote_ValidationErrors() 
        {
            mockQuotesEngine = new Mock<IQuotesEngine>();
            mockQuotesEngine.Setup(x => x.ValidateQuoteRequest(It.IsAny<IQuoteRequest>()))
                .Returns(new List<string>() { "Validation error 1", "Validation error 2" });
            mockQuotesEngine.Setup(x => x.GenerateQuote(It.IsAny<IQuoteRequest>()))
                .Returns(12345);

            IQuotesManager quotesManager = new QuotesManager(mockQuotesEngine.Object);
            ManagerResponse<Quote> response = quotesManager.GenerateQuote(new QuoteRequest { });
            Assert.IsFalse(response.Succeeded);
            Assert.AreEqual(400, response.ResponseCode);
            Assert.AreEqual(2, response.ValidationErrors.Count);

        }

        [Test]
        public void GenerateQuote_InternalError()
        {
            mockQuotesEngine = new Mock<IQuotesEngine>();
            mockQuotesEngine.Setup(x => x.ValidateQuoteRequest(It.IsAny<IQuoteRequest>()))
                .Throws(new Exception("Validation exception"));
            mockQuotesEngine.Setup(x => x.GenerateQuote(It.IsAny<IQuoteRequest>()))
                .Returns(12345);

            IQuotesManager quotesManager = new QuotesManager(mockQuotesEngine.Object);
            ManagerResponse<Quote> response = quotesManager.GenerateQuote(new QuoteRequest { });
            Assert.IsFalse(response.Succeeded);
            Assert.AreEqual(500, response.ResponseCode);
            Assert.IsTrue(response.Message.Contains("Validation exception"));

        }


    }
}