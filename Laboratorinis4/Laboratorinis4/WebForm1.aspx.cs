using Laboratorinis4.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Laboratorinis4
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HideInfo();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string directory = HttpContext.Current.Server.MapPath(@"App_Data");
            try
            {            
                List<Register> allRegisters = InOut.ReadFiles(directory,Error);
                File.Delete(HttpContext.Current.Server.MapPath("App_Data/Rezultatai.txt"));
                File.Delete(HttpContext.Current.Server.MapPath("App_Data/Apziura.txt"));
                ShowInfo();
                foreach (Register register in allRegisters)
                {
                    InOut.PrintTransportsToTXTFile(HttpContext.Current.Server.MapPath("App_Data/Rezultatai.txt"), register);
                    List<Register> registers = new List<Register>();
                    registers.Add(register);
                    InOut.PrintBestTransports(HttpContext.Current.Server.MapPath("App_Data/Rezultatai.txt"), register);
                    Register TrucksRegister = new Register(register.FindTrucks(),register.GetCity(),register.GetAdress(),register.GetEmail());
                    TrucksRegister.Sort();
                    InOut.PrintTrucks(HttpContext.Current.Server.MapPath("App_Data/Rezultatai.txt"), TrucksRegister);
                    InOut.PrintOutdatedTransports(HttpContext.Current.Server.MapPath("App_Data/Apziura.txt"), register);
                }
                PrintInfoToWeb(allRegisters);
                PrintTransportsToWeb(allRegisters);
                InOut.PrintOldestMicrobusesRegisterToWeb(TaskUtils.FindHighestMicrobusYearsAverage(allRegisters), InfoTable);
                PrintTrucksToWeb(allRegisters);
                PrintOutdatedTransportsToWeb(allRegisters);
                Register highestMicrobusYearsAvgRegister = TaskUtils.FindHighestMicrobusYearsAverage(allRegisters);
                InOut.PrintOldestMicrobus(HttpContext.Current.Server.MapPath("App_Data/Rezultatai.txt"), highestMicrobusYearsAvgRegister);

            } catch(Exception ex)
            {
                Error.Visible = true;
                Error.Text = ex.Message;
            }
        }
    }
}