using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CurlingScheduler.Model
{
    public class Team
    {
        public Team(
            IEnumerable<string> allTeamNames,
            string name)
        {
            Name = name;

            foreach (var opponent in allTeamNames.Where(n => !n.Equals(name)))
            {
                OpposingTeamCounts[opponent] = 0;
            }
        }

        public string Name { get; set; }

        [JsonIgnore]
        public Dictionary<string, int> OpposingTeamCounts { get; } =
            new Dictionary<string, int>();

        [JsonIgnore]
        public Dictionary<int, int> DrawCounts { get; } =
            new Dictionary<int, int>();
    }
}