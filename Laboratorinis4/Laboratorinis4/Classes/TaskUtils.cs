using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorinis4.Classes
{
    /// <summary>
    /// Task class
    /// </summary>
    public static class TaskUtils
    {
        /// <summary>
        /// Finds register with highest microbus' years average
        /// </summary>
        /// <param name="registers"></param>
        /// <returns></returns>
        public static Register FindHighestMicrobusYearsAverage(List<Register> registers)
        {
            Register highestAverageRegister = new Register();

            double highestAverage = 0;
            for (int i = 0; i < registers.Count; i++)
            {
                double currentAverage = registers[i].CalculateMicrobusYearsAverage();
                if (currentAverage >= highestAverage)
                {
                    highestAverageRegister = registers[i];
                    highestAverage = currentAverage;
                }
            }

            return highestAverageRegister;
        }

    }
}