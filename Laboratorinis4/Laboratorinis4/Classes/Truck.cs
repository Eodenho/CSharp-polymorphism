using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorinis4.Classes
{
    /// <summary>
    /// Truck class
    /// </summary>
    public class Truck : Transport
    {
        public double TrailerCapacity { get; set; }

        public Truck(string licenseNumber, string facturer, string model, DateTime manufactureDate, DateTime techDate,
                   string fuelType, double fuelConsumption, double trailerCapacity)
                   : base(licenseNumber, facturer, model, manufactureDate, techDate, fuelType, fuelConsumption)
        {
            LicenseNumber = licenseNumber;
            Facturer = facturer;
            Model = model;
            ManufactureDate = manufactureDate;
            TechDate = techDate;
            FuelType = fuelType;
            FuelConsumption = fuelConsumption;
            TrailerCapacity = trailerCapacity;
        }
        /// <summary>
        /// Compares two transports
        /// </summary>
        /// <param name="other">Other truck</param>
        /// <returns>A number</returns>
        public override int CompareTo(Transport other)
        {
            if (this.Facturer.CompareTo(other.Facturer) == 0)
            {
                return this.Model.CompareTo(other.Model);
            }
            else
            {
                return this.Facturer.CompareTo(other.Facturer);
            }
        }
        /// <summary>
        /// Overrides ToString method
        /// </summary>
        /// <returns>Formatted string</returns>
        public override string ToString()
        {
            return string.Format("{0} {1,10} | {2,20} | {3,20} |", base.ToString(), "-", this.TrailerCapacity, "-");
        }
        /// <summary>
        /// Overrides second ToString method
        /// </summary>
        /// <returns>Formatted string</returns>
        public override string ToString2()
        {
            return string.Format("{0} {1,21:yyyy-MM} |", base.ToString(), this.TechDate.AddYears(1));
        }
        public override TimeSpan Outdated()
        {
            if (this.TechDate.AddYears(1) <= DateTime.Now)
            {
                return TechDate.Subtract(DateTime.Now.AddMonths(1));
            }
            else if (this.TechDate.AddYears(1) >= DateTime.Now && this.TechDate.AddYears(1) <= DateTime.Now.AddMonths(1))
            {
                return DateTime.Now.Subtract(TechDate);
            }
            else
            {
                return TimeSpan.MaxValue;
            }
        }
        /// <summary>
        /// Finds tech due date
        /// </summary>
        /// <returns>DateTime</returns>
        public override DateTime DueDate()
        {
            return this.TechDate.AddYears(1);
        }
    }
}