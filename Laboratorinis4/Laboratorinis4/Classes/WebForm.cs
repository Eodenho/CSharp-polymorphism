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
        /// <summary>
        /// Prints starting info to web
        /// </summary>
        /// <param name="registers">List of registers</param>
        public void PrintInfoToWeb(List<Register> registers)
        {
            string text = "";
            for (int i = 0; i < registers.Count(); i++)
            {
                Table table = new Table();
                Label label = new Label();
                InOut.PrintInfoToWeb(registers[i], table, label, registers[i].GetCity());
                table.GridLines = GridLines.Both;
                label.CssClass = "label";
                table.ID = "Table" + i;
                StringWriter sw = new StringWriter();
                StringWriter sw2 = new StringWriter();
                table.RenderControl(new HtmlTextWriter(sw));
                label.RenderControl(new HtmlTextWriter(sw2));
                string html = sw.ToString();
                string html2 = sw2.ToString();
                text += html2 + "<br>";
                text += html + "<br>";
                InOut.PrintBestTransportsToWeb(registers[i], table, label, registers[i].GetCity());
            }
            rand.InnerHtml += text;
        }
        /// <summary>
        /// Prints best transports to web
        /// </summary>
        /// <param name="registers">List of registers</param>
        public void PrintTransportsToWeb(List<Register> registers)
        {
            string text = "";
            for (int i = 0; i < registers.Count(); i++)
            {
                Table table = new Table();
                Label label = new Label();
                InOut.PrintBestTransportsToWeb(registers[i], table, label, registers[i].GetCity());
                table.GridLines = GridLines.Both;
                label.CssClass = "label";
                table.ID = "Table" + i;
                StringWriter sw = new StringWriter();
                StringWriter sw2 = new StringWriter();
                table.RenderControl(new HtmlTextWriter(sw));
                label.RenderControl(new HtmlTextWriter(sw2));
                string html = sw.ToString();
                string html2 = sw2.ToString();
                text += html2 + "<br>";
                text += html + "<br>";
            }
            rand2.InnerHtml += text;
        }
        /// <summary>
        /// Prints trucks to web
        /// </summary>
        /// <param name="registers">List of registers</param>
        public void PrintTrucksToWeb(List<Register> registers)
        {
            string text = "";
            for (int i = 0; i < registers.Count(); i++)
            {
                Table table = new Table();
                Label label = new Label();
                InOut.PrintTrucksToWeb(registers[i], table, label, registers[i].GetCity());
                table.GridLines = GridLines.Both;
                label.CssClass = "label";
                table.ID = "Table" + i;
                StringWriter sw = new StringWriter();
                StringWriter sw2 = new StringWriter();
                table.RenderControl(new HtmlTextWriter(sw));
                label.RenderControl(new HtmlTextWriter(sw2));
                string html = sw.ToString();
                string html2 = sw2.ToString();
                text += html2 + "<br>";
                text += html + "<br>";
            }
            rand3.InnerHtml += text;
        }
        /// <summary>
        /// Prints outdated transports to web
        /// </summary>
        /// <param name="registers">List of registers</param>a
        public void PrintOutdatedTransportsToWeb(List<Register> registers)
        {
            string text = "";
            for (int i = 0; i < registers.Count(); i++)
            {
                if (registers[i].FindTransportsWithServicingDueDate().Count != 0)
                {
                    Table table = new Table();
                    Label label = new Label();
                    InOut.PrintOutdatedTransportsToWeb(registers[i], table, label, registers[i].GetCity());
                    table.GridLines = GridLines.Both;
                    label.CssClass = "label";
                    table.ID = "Table" + i;
                    StringWriter sw = new StringWriter();
                    StringWriter sw2 = new StringWriter();
                    table.RenderControl(new HtmlTextWriter(sw));
                    label.RenderControl(new HtmlTextWriter(sw2));
                    string html = sw.ToString();
                    string html2 = sw2.ToString();
                    text += html2 + "<br>";
                    text += html + "<br>";
                }
            }
            rand4.InnerHtml += text;
        }
        /// <summary>
        /// Hides info
        /// </summary>
        public void HideInfo()
        {
            Label1.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            Label4.Visible = false;
            Label5.Visible = false;
            Error.Visible = false;
        }
        /// <summary>
        /// Shows info
        /// </summary>
        public void ShowInfo()
        {
            Label1.Visible = true;
            Label2.Visible = true;
            Label3.Visible = true;
            Label4.Visible = true;
            Label5.Visible = true;
            Error.Visible = true;
            Button1.Visible = false;
        }
    }
}