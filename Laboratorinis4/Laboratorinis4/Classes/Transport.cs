using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorinis4.Classes
{
    /// <summary>
    /// Transport class
    /// </summary>
    public abstract class Transport : IEquatable<Transport>, IComparable<Transport>
    {
        public string LicenseNumber { get; set; }
        public string Facturer { get; set; }
        public string Model { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime TechDate { get; set; }
        public string FuelType { get; set; }
        public double FuelConsumption { get; set; }

        public Transport()
        {
        }

        public Transport(string licenseNumber, string facturer, string model, DateTime manufactureDate, DateTime techDate, string fuelType, double fuelConsumption)
        {
            LicenseNumber = licenseNumber;
            Facturer = facturer;
            Model = model;
            ManufactureDate = manufactureDate;
            TechDate = techDate;
            FuelType = fuelType;
            FuelConsumption = fuelConsumption;
        }

        /// <summary>
        /// This method overrides Object's class Equals method
        /// </summary>
        /// <param name="other">A transport object</param>
        /// <returns>True if vehicle's registration plate number matches, false if not</returns>
        public bool Equals(Transport other)
        {
            return this.LicenseNumber == other.LicenseNumber;
        }

        /// <summary>
        /// This method overrides Object's class GetHashCode method
        /// </summary>
        /// <returns>The hash code for registration plate</returns>
        public override int GetHashCode()
        {
            return this.LicenseNumber.GetHashCode();
        }
        /// <summary>
        /// Overrides ToStringMethod
        /// </summary>
        /// <returns>Formatted string</returns>
        public override string ToString()
        {
            return string.Format("| {0,-19} | {1,-10} | {2,-13} | {3,21:yyyy-MM} | {4,29:yyyy-MM-dd} | {5,-9} | {6,29:f1} |",
                this.LicenseNumber, this.Facturer, this.Model, this.ManufactureDate, this.TechDate, this.FuelType,
                this.FuelConsumption);
        }
        /// <summary>
        /// Compares objects by manufacturer and model;
        /// </summary>
        /// <param name="other">Other transport</param>
        /// <returns>A number</returns>
        public abstract int CompareTo(Transport other);
     
        /// <summary>
        /// Calculates age of the transport in days
        /// </summary>
        /// <param name="manufactureDate">Date of manufacturing</param>
        /// <returns>A number of days</returns>
        public int CalculateAgeInDays(DateTime manufactureDate)
        {
            int ageInDays = (DateTime.Now - manufactureDate).Days;

            return ageInDays;
        }
        /// <summary>
        /// This method overloads greater-than operator
        /// </summary>
        /// <param name="lhs">Left hand side</param>
        /// <param name="rhs">Right hand side</param>
        /// <returns>True if an integer is greater than zero, false if not</returns>
        public static bool operator >(Transport lhs, Transport rhs)
        {
            if (lhs.CompareTo(rhs) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This method overloads less-than operator
        /// </summary>
        /// <param name="lhs">Left hand side</param>
        /// <param name="rhs">Right hand side</param>
        /// <returns>True if an integer is less than zero, false if not</returns>
        public static bool operator <(Transport lhs, Transport rhs)
        {
            if (lhs.CompareTo(rhs) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public abstract string ToString2();
        public abstract DateTime DueDate();
        public abstract TimeSpan Outdated();
    }
}