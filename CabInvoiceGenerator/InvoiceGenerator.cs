using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    public class InvoiceGenerator
    {
        RideType rideType;
        private RideRepository rideRepository;
        //Constants
        private readonly double Minimum_Cost_Per_Km;
        private readonly int COST_PER_TIME;
        private readonly double MINIMUM_FARE;

        public InvoiceGenerator(RideType rideType)
        {
            this.rideType = rideType;
            this.rideRepository = new RideRepository();
            try
            {
                if (rideType.Equals(RideType.Premium))
                {
                    this.Minimum_Cost_Per_Km = 15;
                    this.COST_PER_TIME = 2;
                    this.MINIMUM_FARE = 5;
                }
               else if (rideType.Equals(RideType.Normal))
                {
                    this.Minimum_Cost_Per_Km = 10;
                    this.COST_PER_TIME = 1;
                    this.MINIMUM_FARE = 5;
                }
            }
            catch (CustomException)
            {

                throw new CustomException(CustomException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Tye");
            }
        }
        public double CalculateFare(double distance, int time)
        {
            double totalFare = 0;
            try
            {
                totalFare = distance * Minimum_Cost_Per_Km + time * COST_PER_TIME;
            }
            catch (CustomException)
            {
                if (rideType.Equals(null))
                {
                    throw new CustomException(CustomException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Type");
                }
                if (distance<=0)
                {
                    throw new CustomException(CustomException.ExceptionType.INVALID_DISTANCE, "Invalid Distance");
                }
                if (time<=0)
                {
                    throw new CustomException(CustomException.ExceptionType.INVALID_TIME, "Invalid Time");
                }
            }
            return Math.Max(totalFare, MINIMUM_FARE);
        }
        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            try
            {
                foreach (Ride ride in rides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);

                }
            }
            catch (CustomException)
            {
                if (rides == null)
                {
                    throw new CustomException(CustomException.ExceptionType.NULL_RIDES, "Rides Are Null");
                }

            }
            return new InvoiceSummary(rides.Length, totalFare);
        }
        public void AddRides(string userId, Ride[] rides)
        {
            try
            {
                rideRepository.AddRide(userId, rides);
            }
            catch (CustomException)
            {
                if (rides == null)
                {
                    throw new CustomException(CustomException.ExceptionType.NULL_RIDES, "Rides are Null");
                }
            }
        }
        public InvoiceSummary GetInvoiceSummary(String userId)
        {
            try
            {
                return this.CalculateFare(rideRepository.GetRides(userId));
            }
            catch (CustomException)
            {
                throw new CustomException(CustomException.ExceptionType.INVALID_USER_ID, "Invalid User Id");
            }
        }

    }
}
