using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorinis4.Classes
{
    /// <summary>
    /// Registe class
    /// </summary>
    public class Register
    {
        private List<Transport> allVehicles;
        private string City;
        private string Adress;
        private string Email;

        public Register(List<Transport> allvehicles, string city, string adress, string email)
        {
            this.allVehicles = allvehicles;
            City = city;
            Adress = adress;
            Email = email;
        }
        public Register()
        {
            this.allVehicles = new List<Transport>();
        }
        /// <summary>
        /// Adds transport to register
        /// </summary>
        /// <param name="transport">Added transport</param>
        public void Add(Transport transport)
        {
            if (!allVehicles.Contains(transport))
            {
                allVehicles.Add(transport);
            }
        }
        /// <summary>
        /// Checks if register contains transport
        /// </summary>
        /// <param name="transport">Transport object</param>
        /// <returns>True or false</returns>
        public bool Contains(Transport transport)
        {
            for(int i=0; i < allVehicles.Count; i++)
            {
                if (allVehicles[i].Equals(transport))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Counts all vehicles
        /// </summary>
        /// <returns>Number of vehicles</returns>
        public int Count()
        {
            return allVehicles.Count;
        }
        /// <summary>
        /// Returns transport
        /// </summary>
        /// <param name="index">Index of transport</param>
        /// <returns>Transport</returns>
        public Transport Get(int index)
        {
            try
            {
                return allVehicles[index];
            }
            catch(ArgumentException ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Gets city
        /// </summary>
        /// <returns>City string</returns>
        public string GetCity()
        {
            return this.City;
        }
        /// <summary>
        /// Gets adress
        /// </summary>
        /// <returns>Adress string</returns>
        public string GetAdress()
        {
            return this.Adress;
        }
        /// <summary>
        /// Gets email
        /// </summary>
        /// <returns>Email string</returns>
        public string GetEmail()
        {
            return this.Email;
        }
        /// <summary>
        /// Finds all trucks
        /// </summary>
        /// <returns>List of trucks</returns>
        public List<Transport> FindTrucks()
        {
            List<Transport> trucks = new List<Transport>();
            for (int i = 0; i < allVehicles.Count; i++)
            {
                if (allVehicles[i] is Truck)
                {
                    trucks.Add(allVehicles[i]);
                }
            }
            return trucks;
        }
        /// <summary>
        /// Calculates average microbus years
        /// </summary>
        /// <returns>Microbus years' average</returns>
        public double CalculateMicrobusYearsAverage()
        {
            int totalYearsInDays = 0;
            int count = 0;
            foreach (Transport transport in this.allVehicles)
            {
                if (transport is Microbus)
                {
                    totalYearsInDays += transport.CalculateAgeInDays(transport.ManufactureDate);
                    count++;
                }
            }

            if (count != 0)
            {
                return totalYearsInDays / count;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// This method sorts a list
        /// </summary>
        public void Sort()
        {
            bool flag = true;
            while (flag)
            {
                flag = false;

                for (int i = 0; i < this.allVehicles.Count - 1; i++)
                {
                    Transport a = this.allVehicles[i];
                    Transport b = this.allVehicles[i + 1];
                    if (a > b)
                    {
                        this.allVehicles[i] = b;
                        this.allVehicles[i + 1] = a;

                        flag = true;
                    }
                }
            }
        }
        /// <summary>
        /// Finds transports with servicing due date
        /// </summary>
        /// <returns>List of outdated transport</returns>
        public List<Transport> FindTransportsWithServicingDueDate()
        {
            List<Transport> foundTransports = new List<Transport>();

            foreach (Transport transport in this.allVehicles)
            {
                if (transport is Car && transport.TechDate.AddYears(2) < DateTime.Now.AddMonths(1))
                {
                    foundTransports.Add(transport);
                }
                if (transport is Truck && transport.TechDate.AddYears(1) < DateTime.Now.AddMonths(1))
                {
                    foundTransports.Add(transport);
                }
                if (transport is Microbus && transport.TechDate.AddMonths(6) < DateTime.Now.AddMonths(1))
                {
                    foundTransports.Add(transport);
                }
            }

            return foundTransports;
        }
        /// <summary>
        /// Finds the best car
        /// </summary>
        /// <returns>Car</returns>
        public Transport FindBestCar()
        {
            Transport transport = null;
            double max = double.MaxValue;
            for (int i = 0; i < allVehicles.Count; i++)
            {
                if (allVehicles[i] is Car)
                {
                    var obj = allVehicles[i] as Car;
                    if (obj.OdometerReadings < max)
                    {
                        max = obj.OdometerReadings;
                        transport = obj;
                    }
                }
            }
            return transport;
        }
        /// <summary>
        /// Finds best truck
        /// </summary>
        /// <returns>Truck</returns>
        public Transport FindBestTruck()
        {
            Transport transport = null;
            double max = 0;
            for (int i = 0; i < allVehicles.Count; i++)
            {
                if (allVehicles[i] is Truck)
                {
                    var obj = allVehicles[i] as Truck;
                    if (obj.TrailerCapacity > max)
                    {
                        max = obj.TrailerCapacity;
                        transport = obj;
                    }
                }
            }
            return transport;
        }
        /// <summary>
        /// Finds best microbus
        /// </summary>
        /// <returns>Microbus</returns>
        public Transport FindBestMicrobus()
        {
            Transport transport = null;
            double max = 0;
            for (int i = 0; i < allVehicles.Count; i++)
            {
                if (allVehicles[i] is Microbus)
                {
                    var obj = allVehicles[i] as Microbus;
                    if (obj.Seats > max)
                    {
                        max = obj.Seats;
                        transport = obj;
                    }
                }
            }
            return transport;
        }
    }
}