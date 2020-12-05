using CurlingScheduler.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CurlingScheduler.Service
{
    internal class OutputWriter
    {
        internal string FormatGameSchedule(
            Schedule schedule)
        {
            var lines = new List<string>();

            for (int weekIndex = 0; weekIndex < schedule.Weeks.Count(); weekIndex++)
            {
                var week = schedule.Weeks[weekIndex];

                if (weekIndex != 0)
                {
                    lines.Add(string.Empty);
                }
                lines.Add($"Week {weekIndex + 1}");

                for(int drawIndex= 0; drawIndex < week.Draws.Count(); drawIndex++)
                {
                    var draw = week.Draws[drawIndex];

                    lines.Add($"  Draw { drawIndex + 1}");

                    foreach (var game in draw.Games)
                    {
                        lines.Add($"    {(char)(game.Sheet + 'A')} - {game.Teams.ElementAt(0).Name} vs {game.Teams.ElementAt(1).Name}");
                    }
                }
            }

            return string.Join(Environment.NewLine, lines);
        }

        internal string FormatStoneSchedule(Schedule schedule)
        {
            var lines = new List<string>();

            for (int weekIndex = 0; weekIndex < schedule.Weeks.Count(); weekIndex++)
            {
                var week = schedule.Weeks[weekIndex];

                if (weekIndex != 0)
                {
                    lines.Add(string.Empty);
                }
                lines.Add($"Week {weekIndex + 1}");

                var sheetStoneMapping = new Dictionary<int, int>();

                for (int drawIndex = 0; drawIndex < week.Draws.Count(); drawIndex++)
                {
                    var draw = week.Draws[drawIndex];

                    foreach (var game in draw.Games)
                    {
                        sheetStoneMapping[game.Sheet] = game.Stones;
                    }
                }

                foreach (var key in sheetStoneMapping.Keys)
                {
                    lines.Add($"  {(char)(key + 'A')} - {sheetStoneMapping[key]}");
                }
            }

            return string.Join(Environment.NewLine, lines);
        }

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