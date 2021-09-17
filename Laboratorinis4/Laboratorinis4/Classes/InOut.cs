using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Laboratorinis4.Classes
{
    public static class InOut
    {
        /// <summary>
        /// This method reads given information from a file
        /// </summary>
        /// <param name="fileName">Name of the input file</param>
        /// <param name="register">Transports register</param>
        public static Register ReadFile(string fileName)
        {
            List<Transport> register = new List<Transport>();
            string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);
            string city = lines[0];
            string adress = lines[1];
            string email = lines[2];
            foreach (string line in lines.Skip(3))
            {
                string[] values = line.Split(';');
                string type = values[0];
                string registrationPlate = values[1];
                string facturer = values[2];
                string model = values[3];
                DateTime manufactureDate = DateTime.Parse(values[4]);
                DateTime validServicingDate = DateTime.Parse(values[5]);
                string fuelType = values[6];
                double fuelConsumption = double.Parse(values[7]);

                switch (type)
                {
                    case "Car":
                        double odometerReadings = double.Parse(values[8]);
                        Car car = new Car(registrationPlate, facturer, model, manufactureDate, validServicingDate, fuelType,
                            fuelConsumption, odometerReadings);
                        register.Add(car);
                        break;

                    case "Truck":
                        double trailerCapacity = double.Parse(values[8]);
                        Truck lorry = new Truck(registrationPlate, facturer, model, manufactureDate, validServicingDate, fuelType,
                            fuelConsumption, trailerCapacity);
                        register.Add(lorry);
                        break;

                    case "Microbus":
                        int seatsCount = int.Parse(values[8]);
                        Microbus bus = new Microbus(registrationPlate, facturer, model, manufactureDate, validServicingDate, fuelType,
                            fuelConsumption, seatsCount);
                        register.Add(bus);
                        break;

                    default:
                        throw new Exception("Nera tokio tipo");
                }
            }
            return new Register(register, city, adress, email);
        }
        /// <summary>
        /// Reads all files from a direcoty
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <returns>List of registers</returns>
        public static List<Register> ReadFiles(string path, Label label)
        {
            List<Register> data = new List<Register>();
            foreach (string file in Directory.GetFiles(path, "*.txt"))
            {
                string fileName = Path.GetFileName(file);
                try
                {
                    if (fileName != "Rezultatai.txt" && fileName != "Apziura.txt")
                    {
                        Register temp = InOut.ReadFile(file);
                        if (temp.Count() < 1)
                        {
                            throw new Exception("Nera duomenu");
                        } else
                        {
                            data.Add(temp);
                        }
                    }
                }
                catch(Exception ex)
                {
                    label.Text = "Nenuskaitytas failas/ai.";
                }
            }
            if (data.Count == 0)
            {
                throw new NullReferenceException("Nera registrų");
            }
            return data;
        }

        /// <summary>
        /// This method formats a table and prints a list of transport objects to .txt file
        /// </summary>
        /// <param name="fileName">Name of the output file</param>
        /// <param name="register">Transports register</param>
        public static void PrintTransportsToTXTFile(string fileName, Register register)
        {
            List<string> lines = new List<string>();

            lines.Add(string.Format("Miestas: {0}\nAdresas: {1}\nTelefono numeris: {2}", register.GetCity(), register.GetAdress(),
                register.GetEmail()));
            lines.Add(new string('-', 38));
            lines.Add("Filialo turimos transporto priemonės:|");
            lines.Add(new string('-', 211));
            lines.Add(string.Format("| {0} | {1} | {2,-13} | {3} | {4} | {5,-9} | {6} | {7,-10} | {8,-20} | {9,-20} |", "Valstybinis numeris",
                "Gamintojas", "Modelis", "Pagaminimo m. ir mėn.", "Tech. apžiūros galiojimo data", "Kuras",
                "Vid. kuro sąnaudos (l/100 km)", "Rida", "Priekabos talpa (kg)", "Sėdimų vietų skaič."));
            lines.Add(new string('-', 211));
            for (int i = 0; i < register.Count(); i++)
            {
                Transport transport = register.Get(i);
                lines.Add(transport.ToString());
            }
            lines.Add(new string('-', 211));
            lines.Add("");
            lines.Add("");

            File.AppendAllLines(fileName, lines, Encoding.UTF8);
        }
        /// <summary>
        /// Prints best transports of register
        /// </summary>
        /// <param name="fileName">File being printed to</param>
        /// <param name="register">Register searched</param>
        public static void PrintBestTransports(string fileName, Register register)
        {
            List<string> lines = new List<string>();
            lines.Add(new string('-', 10));
            lines.Add("| " + register.GetCity() + " |");
            lines.Add(new string('-', 52));
            lines.Add("| Filialo geriausios turimos transporto priemonės: |");
            lines.Add(new string('-', 211));
            lines.Add(string.Format("| {0} | {1} | {2,-13} | {3} | {4} | {5,-9} | {6} | {7,-10} | {8,-20} | {9,-20} |", "Valstybinis numeris",
                "Gamintojas", "Modelis", "Pagaminimo m. ir mėn.", "Tech. apžiūros galiojimo data", "Kuras",
                "Vid. kuro sąnaudos (l/100 km)","Rida", "Priekabos talpa (kg)", "Sėdimų vietų skaič."));
            lines.Add(new string('-', 211));
            lines.Add(register.FindBestCar().ToString());
            lines.Add(new string('-', 211));
            lines.Add(register.FindBestTruck().ToString());
            lines.Add(new string('-', 211));
            lines.Add(register.FindBestMicrobus().ToString());
            lines.Add(new string('-', 211));
            lines.Add("");
            lines.Add("");

            File.AppendAllLines(fileName, lines, Encoding.UTF8);
        }
        /// <summary>
        /// Prints oldes microbus register to TXT
        /// </summary>
        /// <param name="fileName">File being printed to</param>
        /// <param name="register">Register searched</param>
        public static void PrintOldestMicrobus(string fileName, Register register)
        {
            List<string> lines = new List<string>();
            lines.Add(new string('-', 41));
            lines.Add("Seniausi mikroautobusai šiame filiale:  |");
            lines.Add(string.Format("Miestas: {0,-30} |", register.GetCity()));
            lines.Add(string.Format("Adresas: {0,-30} |", register.GetAdress()));
            lines.Add(string.Format("El. paštas: {0,-27} |", register.GetEmail()));
            lines.Add(new string('-', 41));
            lines.Add(string.Format("{0,-27} {1,-4:f0}d. |", "Vidutinis mikroautobusų amžius: ", register.CalculateMicrobusYearsAverage()));
            lines.Add(new string('-', 41));
            lines.Add("");
            lines.Add("");

            File.AppendAllLines(fileName, lines, Encoding.UTF8);
        }
        /// <summary>
        /// Prints trucks of register
        /// </summary>
        /// <param name="fileName">File being printed to</param>
        /// <param name="register">Register searched</param>
        public static void PrintTrucks(string fileName, Register register)
        {
            List<string> lines = new List<string>();
            lines.Add(new string('-', 34));
            lines.Add(string.Format("{0,-6} krovininiai automobiliai: |", register.GetCity()));
            lines.Add(new string('-', 211));
            lines.Add(string.Format("| {0} | {1} | {2,-13} | {3} | {4} | {5,-9} | {6} | {7,-10} | {8,-20} | {9,-20} |", "Valstybinis numeris",
                "Gamintojas", "Modelis", "Pagaminimo m. ir mėn.", "Tech. apžiūros galiojimo data", "Kuras",
                "Vid. kuro sąnaudos (l/100 km)", "Rida", "Priekabos talpa (kg)", "Sėdimų vietų skaič."));
            lines.Add(new string('-', 211));
            List<Transport> allTrucks = register.FindTrucks();
            foreach (Transport truck in allTrucks)
            {
                lines.Add(truck.ToString());
            }
            lines.Add(new string('-', 211));
            lines.Add("");
            lines.Add("");

            File.AppendAllLines(fileName, lines, Encoding.UTF8);
        }
        /// <summary>
        /// Prints outdated transports
        /// </summary>
        /// <param name="fileName">File being printed to</param>
        /// <param name="register">Register searched</param>
        public static void PrintOutdatedTransports(string fileName, Register register)
        {
            List<string> lines = new List<string>();
            lines.Add(new string('-', 30));
            lines.Add(string.Format("Tech. data greitai baigiasi: |"));
            lines.Add(new string('-', 30));
            lines.Add(register.GetCity() + " |");
            lines.Add(new string('-', 178));
            lines.Add(string.Format("| {0,-19} | {1,-10} | {2,-13} | {3,21:yyyy-MM} | {4,29:yyyy-MM-dd} | {5,-9} | {6,29:f1} | {7,23:yyyy-MM} |", "Valstybinis numeris",
                "Gamintojas", "Modelis", "Pagaminimo m. ir mėn.", "Tech. apžiūros galiojimo data", "Kuras",
                "Vid. kuro sąnaudos (l/100 km)", "Tech. apžiūros pabaiga"));
            lines.Add(new string('-', 178));
            for (int i = 0; i < register.Count(); i++)
            {
                if (register.Get(i).Outdated() != TimeSpan.MaxValue)
                {
                    if (register.Get(i).Outdated() > TimeSpan.Zero)
                    {
                        lines.Add(string.Format("| {0,-19} | {1,-10} | {2,-13} | {3,21:yyyy-MM} | {4,29:yyyy-MM-dd} | {5,-9} | {6,29:f1} | {7,23:yyyy-MM} |", register.Get(i).LicenseNumber,
                    register.Get(i).Facturer, register.Get(i).Model, register.Get(i).ManufactureDate, register.Get(i).TechDate, register.Get(i).FuelType,
                    register.Get(i).FuelConsumption, register.Get(i).DueDate()));
                    }
                    else if (register.Get(i).Outdated() < TimeSpan.Zero)
                    {
                        lines.Add(string.Format("| {0,-19} | {1,-10} | {2,-13} | {3,21:yyyy-MM} | {4,29:yyyy-MM-dd} | {5,-9} | {6,29:f1} | {7,23:yyyy-MM} |", register.Get(i).LicenseNumber,
                    register.Get(i).Facturer, register.Get(i).Model, register.Get(i).ManufactureDate, register.Get(i).TechDate, register.Get(i).FuelType,
                    register.Get(i).FuelConsumption, "*****"));
                    }
                }
            }
            lines.Add(new string('-', 178));
            lines.Add("");
            lines.Add("");

            File.AppendAllLines(fileName, lines, Encoding.UTF8);
        }
        /// <summary>
        /// Prints starting info to web
        /// </summary>
        /// <param name="register">Searched register</param>
        /// <param name="table">Table being printed to</param>
        /// <param name="label">Label for table</param>
        /// <param name="header">Header for label</param>
        public static void PrintInfoToWeb(Register register, Table table, Label label, string header)
        {
            label.Text = header;
            TableRow info = new TableRow();
            TableCell licensePlate = new TableCell();
            licensePlate.Text = "Valstybiniai numeriai";
            info.Cells.Add(licensePlate);
            TableCell facturer = new TableCell();
            facturer.Text = "Gamintojas";
            info.Cells.Add(facturer);
            TableCell model = new TableCell();
            model.Text = "Modelis";
            info.Cells.Add(model);
            TableCell date = new TableCell();
            date.Text = "Pagaminimo data";
            info.Cells.Add(date);
            TableCell techDate = new TableCell();
            techDate.Text = "Tech. apžiūra daryta";
            info.Cells.Add(techDate);
            TableCell fuel = new TableCell();
            fuel.Text = "Kuras";
            info.Cells.Add(fuel);
            TableCell fuelConsumption = new TableCell();
            fuelConsumption.Text = "Kuro sąnaudos l/100 km";
            info.Cells.Add(fuelConsumption);
            TableCell odometerReadings = new TableCell();
            odometerReadings.Text = "Rida";
            info.Cells.Add(odometerReadings);
            TableCell TrailerCapacity = new TableCell();
            TrailerCapacity.Text = "Priekabos talpa l";
            info.Cells.Add(TrailerCapacity);
            TableCell seats = new TableCell();
            seats.Text = "Sedimų vietų skaičius";
            info.Cells.Add(seats);
            table.Rows.Add(info);
            for (int i = 0; i < register.Count(); i++)
            {
                TableRow row = new TableRow();
                TableCell licensePlate2 = new TableCell();
                licensePlate2.Text = register.Get(i).LicenseNumber;
                row.Cells.Add(licensePlate2);
                TableCell facturer2 = new TableCell();
                facturer2.Text = register.Get(i).Facturer;
                row.Cells.Add(facturer2);
                TableCell model2 = new TableCell();
                model2.Text = register.Get(i).Model;
                row.Cells.Add(model2);
                TableCell date2 = new TableCell();
                date2.Text = string.Format("{0,21:yyyy-MM}", register.Get(i).ManufactureDate);
                row.Cells.Add(date2);
                TableCell techDate2 = new TableCell();
                techDate2.Text = string.Format("{0,21:yyyy-MM}", register.Get(i).TechDate);
                row.Cells.Add(techDate2);
                TableCell fuel2 = new TableCell();
                fuel2.Text = register.Get(i).FuelType;
                row.Cells.Add(fuel2);
                TableCell fuelConsumption2 = new TableCell();
                fuelConsumption2.Text = Convert.ToString(register.Get(i).FuelConsumption);
                row.Cells.Add(fuelConsumption2);
                TableCell odometerReadings2 = new TableCell();
                TableCell trailerCapacity2 = new TableCell();
                TableCell seats2 = new TableCell();
                if (register.Get(i) is Car)
                {
                    var obj = register.Get(i) as Car;
                    odometerReadings2.Text = Convert.ToString(obj.OdometerReadings);
                    trailerCapacity2.Text = "-";
                    seats2.Text = "-";
                }
                if (register.Get(i) is Truck)
                {
                    var obj = register.Get(i) as Truck;
                    trailerCapacity2.Text = Convert.ToString(obj.TrailerCapacity);
                    odometerReadings2.Text = "-";
                    seats2.Text = "-";
                }
                if (register.Get(i) is Microbus)
                {
                    var obj = register.Get(i) as Microbus;
                    seats2.Text = Convert.ToString(obj.Seats);
                    odometerReadings2.Text = "-";
                    trailerCapacity2.Text = "-";
                }
                row.Cells.Add(odometerReadings2);
                row.Cells.Add(trailerCapacity2);
                row.Cells.Add(seats2);
                table.Rows.Add(row);
            }
        }
        /// <summary>
        /// Prints best transports to web
        /// </summary>
        /// <param name="register">Searched register</param>
        /// <param name="table">Table being printed to</param>
        /// <param name="label">Label for table</param>
        /// <param name="header">Header for label</param>
        public static void PrintBestTransportsToWeb(Register register, Table table, Label label, string header)
        {
            label.Text = header;
            TableRow info = new TableRow();
            TableCell licensePlate = new TableCell();
            licensePlate.Text = "Valstybiniai numeriai";
            info.Cells.Add(licensePlate);
            TableCell facturer = new TableCell();
            facturer.Text = "Gamintojas";
            info.Cells.Add(facturer);
            TableCell model = new TableCell();
            model.Text = "Modelis";
            info.Cells.Add(model);
            TableCell date = new TableCell();
            date.Text = "Pagaminimo data";
            info.Cells.Add(date);
            TableCell techDate = new TableCell();
            techDate.Text = "Tech. apžiūra daryta";
            info.Cells.Add(techDate);
            TableCell fuel = new TableCell();
            fuel.Text = "Kuras";
            info.Cells.Add(fuel);
            TableCell fuelConsumption = new TableCell();
            fuelConsumption.Text = "Kuro sąnaudos l/100 km";
            info.Cells.Add(fuelConsumption);
            TableCell odometerReadings = new TableCell();
            odometerReadings.Text = "Rida";
            info.Cells.Add(odometerReadings);
            TableCell TrailerCapacity = new TableCell();
            TrailerCapacity.Text = "Priekabos talpa l";
            info.Cells.Add(TrailerCapacity);
            TableCell seats = new TableCell();
            seats.Text = "Sedimų vietų skaičius";
            info.Cells.Add(seats);
            table.Rows.Add(info);
            for (int i = 0; i < register.Count(); i++)
            {
                if (register.Get(i).Equals(register.FindBestCar()) || register.Get(i).Equals(register.FindBestMicrobus()) || register.Get(i).Equals(register.FindBestTruck()))
                {
                    TableRow row = new TableRow();
                    TableCell licensePlate2 = new TableCell();
                    licensePlate2.Text = register.Get(i).LicenseNumber;
                    row.Cells.Add(licensePlate2);
                    TableCell facturer2 = new TableCell();
                    facturer2.Text = register.Get(i).Facturer;
                    row.Cells.Add(facturer2);
                    TableCell model2 = new TableCell();
                    model2.Text = register.Get(i).Model;
                    row.Cells.Add(model2);
                    TableCell date2 = new TableCell();
                    date2.Text = string.Format("{0,21:yyyy-MM}", register.Get(i).ManufactureDate);
                    row.Cells.Add(date2);
                    TableCell techDate2 = new TableCell();
                    techDate2.Text = string.Format("{0,21:yyyy-MM}", register.Get(i).TechDate);
                    row.Cells.Add(techDate2);
                    TableCell fuel2 = new TableCell();
                    fuel2.Text = register.Get(i).FuelType;
                    row.Cells.Add(fuel2);
                    TableCell fuelConsumption2 = new TableCell();
                    fuelConsumption2.Text = Convert.ToString(register.Get(i).FuelConsumption);
                    row.Cells.Add(fuelConsumption2);
                    TableCell odometerReadings2 = new TableCell();
                    TableCell trailerCapacity2 = new TableCell();
                    TableCell seats2 = new TableCell();
                    if (register.Get(i) is Car)
                    {
                        var obj = register.Get(i) as Car;
                        odometerReadings2.Text = Convert.ToString(obj.OdometerReadings);
                        trailerCapacity2.Text = "-";
                        seats2.Text = "-";
                    }
                    if (register.Get(i) is Truck)
                    {
                        var obj = register.Get(i) as Truck;
                        trailerCapacity2.Text = Convert.ToString(obj.TrailerCapacity);
                        odometerReadings2.Text = "-";
                        seats2.Text = "-";
                    }
                    if (register.Get(i) is Microbus)
                    {
                        var obj = register.Get(i) as Microbus;
                        seats2.Text = Convert.ToString(obj.Seats);
                        odometerReadings2.Text = "-";
                        trailerCapacity2.Text = "-";
                    }
                    row.Cells.Add(odometerReadings2);
                    row.Cells.Add(trailerCapacity2);
                    row.Cells.Add(seats2);
                    table.Rows.Add(row);
                }
            }
        }
        /// <summary>
        /// Prints oldest microbuses register to web
        /// </summary>
        /// <param name="register">Register searched</param>
        /// <param name="table">Table being printed to</param>
        public static void PrintOldestMicrobusesRegisterToWeb(Register register, Table table)
        {
            TableRow headerRow = new TableRow();
            TableCell TopCity = new TableCell();
            TopCity.Text = "Miestas";
            headerRow.Cells.Add(TopCity);
            TableCell TopAdress = new TableCell();
            TopAdress.Text = "Adresas";
            headerRow.Cells.Add(TopAdress);
            TableCell TopEmail = new TableCell();
            TopEmail.Text = "El. paštas";
            headerRow.Cells.Add(TopEmail);
            TableCell TopInfo = new TableCell();
            TopInfo.Text = "Vidutinis mikroautobusų amžius, d.";
            headerRow.Cells.Add(TopInfo);
            table.Rows.Add(headerRow);
            TableRow infoRow = new TableRow();
            TableCell InfoCity = new TableCell();
            InfoCity.Text = register.GetCity();
            infoRow.Cells.Add(InfoCity);
            TableCell InfoAdress = new TableCell();
            InfoAdress.Text = register.GetAdress();
            infoRow.Cells.Add(InfoAdress);
            TableCell InfoEmail = new TableCell();
            InfoEmail.Text = register.GetEmail();
            infoRow.Cells.Add(InfoEmail);
            TableCell InfoAge = new TableCell();
            InfoAge.Text = Convert.ToString(register.CalculateMicrobusYearsAverage());
            infoRow.Cells.Add(InfoAge);
            table.Rows.Add(infoRow);
        }
        /// <summary>
        /// Prints trucks to web
        /// </summary>
        /// <param name="register">Searched register</param>
        /// <param name="table">Table being printed to</param>
        /// <param name="label">Label for table</param>
        /// <param name="header">Header for label</param>
        public static void PrintTrucksToWeb(Register register, Table table, Label label, string header)
        {
            label.Text = header;
            TableRow info = new TableRow();
            TableCell licensePlate = new TableCell();
            licensePlate.Text = "Valstybiniai numeriai";
            info.Cells.Add(licensePlate);
            TableCell facturer = new TableCell();
            facturer.Text = "Gamintojas";
            info.Cells.Add(facturer);
            TableCell model = new TableCell();
            model.Text = "Modelis";
            info.Cells.Add(model);
            TableCell date = new TableCell();
            date.Text = "Pagaminimo data";
            info.Cells.Add(date);
            TableCell techDate = new TableCell();
            techDate.Text = "Tech. apžiūra daryta";
            info.Cells.Add(techDate);
            TableCell fuel = new TableCell();
            fuel.Text = "Kuras";
            info.Cells.Add(fuel);
            TableCell fuelConsumption = new TableCell();
            fuelConsumption.Text = "Kuro sąnaudos l/100 km";
            info.Cells.Add(fuelConsumption);
            TableCell TrailerCapacity = new TableCell();
            TrailerCapacity.Text = "Priekabos talpa l";
            info.Cells.Add(TrailerCapacity);
            table.Rows.Add(info);
            Register TrucksRegister = new Register(register.FindTrucks(), register.GetCity(), register.GetAdress(), register.GetEmail());
            TrucksRegister.Sort();
            for (int i = 0; i < TrucksRegister.Count(); i++)
            {
                TableRow row = new TableRow();
                TableCell licensePlate2 = new TableCell();
                licensePlate2.Text = TrucksRegister.Get(i).LicenseNumber;
                row.Cells.Add(licensePlate2);
                TableCell facturer2 = new TableCell();
                facturer2.Text = TrucksRegister.Get(i).Facturer;
                row.Cells.Add(facturer2);
                TableCell model2 = new TableCell();
                model2.Text = TrucksRegister.Get(i).Model;
                row.Cells.Add(model2);
                TableCell date2 = new TableCell();
                date2.Text = string.Format("{0,21:yyyy-MM}", TrucksRegister.Get(i).ManufactureDate);
                row.Cells.Add(date2);
                TableCell techDate2 = new TableCell();
                techDate2.Text = string.Format("{0,21:yyyy-MM}", TrucksRegister.Get(i).TechDate);
                row.Cells.Add(techDate2);
                TableCell fuel2 = new TableCell();
                fuel2.Text = TrucksRegister.Get(i).FuelType;
                row.Cells.Add(fuel2);
                TableCell fuelConsumption2 = new TableCell();
                fuelConsumption2.Text = Convert.ToString(TrucksRegister.Get(i).FuelConsumption);
                row.Cells.Add(fuelConsumption2);
                TableCell trailerCapacity2 = new TableCell();
                if (TrucksRegister.Get(i) is Truck)
                {
                    var obj = TrucksRegister.Get(i) as Truck;
                    trailerCapacity2.Text = Convert.ToString(obj.TrailerCapacity);
                }
                row.Cells.Add(trailerCapacity2);
                table.Rows.Add(row);
            }
        }
        /// <summary>
        /// Prints outdated transports to web
        /// </summary>
        /// <param name="register">Searched register</param>
        /// <param name="table">Table being printed to</param>
        /// <param name="label">Label for table</param>
        /// <param name="header">Header for label</param>
        public static void PrintOutdatedTransportsToWeb(Register register, Table table, Label label, string header)
        {
            label.Text = header;
            TableRow info = new TableRow();
            TableCell licensePlate = new TableCell();
            licensePlate.Text = "Valstybiniai numeriai";
            info.Cells.Add(licensePlate);
            TableCell facturer = new TableCell();
            facturer.Text = "Gamintojas";
            info.Cells.Add(facturer);
            TableCell model = new TableCell();
            model.Text = "Modelis";
            info.Cells.Add(model);
            TableCell date = new TableCell();
            date.Text = "Pagaminimo data";
            info.Cells.Add(date);
            TableCell techDate = new TableCell();
            techDate.Text = "Tech. apžiūra daryta";
            info.Cells.Add(techDate);
            TableCell fuel = new TableCell();
            fuel.Text = "Kuras";
            info.Cells.Add(fuel);
            TableCell fuelConsumption = new TableCell();
            fuelConsumption.Text = "Kuro sąnaudos l/100 km";
            info.Cells.Add(fuelConsumption);
            TableCell DueDateInfo = new TableCell();
            DueDateInfo.Text = "Tech. apžiūra baigiasi";
            info.Cells.Add(DueDateInfo);
            table.Rows.Add(info);
            for (int i = 0; i < register.Count(); i++)
            {
                if (register.Get(i).Outdated() != TimeSpan.MaxValue)
                {
                    TableRow row = new TableRow();
                    TableCell licensePlate2 = new TableCell();
                    licensePlate2.Text = register.Get(i).LicenseNumber;
                    row.Cells.Add(licensePlate2);
                    TableCell facturer2 = new TableCell();
                    facturer2.Text = register.Get(i).Facturer;
                    row.Cells.Add(facturer2);
                    TableCell model2 = new TableCell();
                    model2.Text = register.Get(i).Model;
                    row.Cells.Add(model2);
                    TableCell date2 = new TableCell();
                    date2.Text = string.Format("{0,21:yyyy-MM}", register.Get(i).ManufactureDate);
                    row.Cells.Add(date2);
                    TableCell techDate2 = new TableCell();
                    techDate2.Text = string.Format("{0,21:yyyy-MM}", register.Get(i).TechDate);
                    row.Cells.Add(techDate2);
                    TableCell fuel2 = new TableCell();
                    fuel2.Text = register.Get(i).FuelType;
                    row.Cells.Add(fuel2);
                    TableCell fuelConsumption2 = new TableCell();
                    fuelConsumption2.Text = Convert.ToString(register.Get(i).FuelConsumption);
                    row.Cells.Add(fuelConsumption2);
                    TableCell DueDate = new TableCell();
                    if (register.Get(i).Outdated() != TimeSpan.MaxValue)
                    {
                        if (register.Get(i).Outdated() > TimeSpan.Zero)
                        {
                            DueDate.Text = string.Format("{0,21:yyyy-MM}", register.Get(i).DueDate());
                        }
                        else if (register.Get(i).Outdated() < TimeSpan.Zero)
                        {
                            DueDate.Text = "*****";
                        }
                    }
                    row.Cells.Add(DueDate);
                    table.Rows.Add(row);
                }
            }
        }
    }
}