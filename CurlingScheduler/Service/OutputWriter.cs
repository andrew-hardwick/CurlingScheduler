using CurlingScheduler.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurlingScheduler.Service
{
    internal class OutputWriter
    {
        internal void Write(
            Schedule schedule,
            string filename)
        {
            File.Delete(filename);

            var writer = File.AppendText(filename);

            writer.Write(JsonConvert.SerializeObject(schedule, Formatting.Indented));

            writer.Close();

            Process.Start(filename);
        }
        internal void Write(
            Dictionary<string, Team> teams,
            string filename)
        {
            File.Delete(filename);

            var writer = File.AppendText(filename);

            writer.Write(JsonConvert.SerializeObject(teams, Formatting.Indented));

            writer.Close();

            Process.Start(filename);
        }
    }
}
