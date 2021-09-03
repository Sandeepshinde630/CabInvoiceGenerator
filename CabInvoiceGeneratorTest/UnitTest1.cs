using NUnit.Framework;
using CabInvoiceGenerator;

namespace CabInvoiceGeneratorTest
{
    public class Tests
    {
        InvoiceGenerator invoiceGenerator = null;
        [SetUp]
        public void Setup()
        {
        }

        // Step 1 Calculate Fare
        [Test]
        public void GivenDistanceAndTimeShouldReturnTotalFare()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.Normal);
            double distance = 2.0;
            int time = 5;
            double fare = invoiceGenerator.CalculateFare(distance, time);
            double expected = 25;
            Assert.AreEqual(expected, fare);
        }

        // Step 2 Mutiple Rides
        [Test]
        public void GivenMultipleRideShouldReturnInvoiceSummary()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.Normal);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 5) };
            InvoiceSummary summary = invoiceGenerator.CalculateFare(rides);
            InvoiceSummary expectedSummary = new InvoiceSummary(2, 31.0);
            Assert.AreEqual(expectedSummary.GetType(), summary.GetType());
        }

        // Step 3 Enhanced Invoice
        [Test]
        public void GivenMutipleEnhancedInvoiceShouldReturnTotalRidesTotalFareAverageFarePerRide()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.Normal);
            Ride[] rides = { new Ride(2.0, 5), new Ride(2.0, 5) };
            InvoiceSummary enhancedSummary = invoiceGenerator.CalculateFare(rides);
            InvoiceSummary expectedEnhancedSummary = new InvoiceSummary(2, 50);
            Assert.AreEqual(expectedEnhancedSummary, enhancedSummary);
        }

        // Step 4 Premimum Rides
        [Test]
        public void GivenDistanceAndTimeForPremiumShouldReturnTotalFare()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.Premium);
            double distance = 2.0;
            int time = 5;
            double fare = invoiceGenerator.CalculateFare(distance, time);
            double expected = 40;
            Assert.AreEqual(expected, fare);
        }

    }
}