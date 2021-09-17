using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorinis4.Classes
{
    public class Microbus : Transport
    {
        public int Seats { get; set; }

        public Microbus(string licenseNumber, string facturer, string model, DateTime manufactureDate, DateTime techDate,
                   string fuelType, double fuelConsumption, int seats)
                   : base(licenseNumber, facturer, model, manufactureDate, techDate, fuelType, fuelConsumption)
        {
            LicenseNumber = licenseNumber;
            Facturer = facturer;
            Model = model;
            ManufactureDate = manufactureDate;
            TechDate = techDate;
            FuelType = fuelType;
            FuelConsumption = fuelConsumption;
            Seats = seats;
        }
        /// <summary>
        /// Overrides ToString method
        /// </summary>
        /// <returns>Formatted string</returns>
        public override string ToString()
        {
            return string.Format("{0} {1,10} | {2,20} | {3,20} |", base.ToString(), "-" , "-", this.Seats);
        }
        /// <summary>
        /// Overrides second ToString method
        /// </summary>
        /// <returns>Formatted string</returns>
        public override string ToString2()
        {
            return string.Format("{0} {1,21:yyyy-MM} |", base.ToString(), this.TechDate.AddMonths(6));
        }
        /// <summary>
        /// Checks how outdated the tech date is
        /// </summary>
        /// <returns>Timespan</returns>
        public override TimeSpan Outdated()
        {
            if (this.TechDate.AddMonths(6) <= DateTime.Now)
            {
                return TechDate.Subtract(DateTime.Now.AddMonths(1));
            } 
            else if (this.TechDate.AddMonths(6) >= DateTime.Now && this.TechDate.AddMonths(6) <= DateTime.Now.AddMonths(1))
            {
                return DateTime.Now.Subtract(TechDate);
            }
            else
            {
                return TimeSpan.MaxValue;
            }
        }
        /// <summary>
        /// Overrides CompareTo Method
        /// </summary>
        /// <param name="other">Another transport</param>
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
        /// Date of upcoming technical checl
        /// </summary>
        /// <returns>Datetime</returns>
        public override DateTime DueDate()
        {
            return this.TechDate.AddMonths(6);
        }
    }
}