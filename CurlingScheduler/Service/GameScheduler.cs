using CurlingScheduler.Model;
using System.Collections.Generic;
using System.Linq;

namespace CurlingScheduler.Service
{
    internal class GameScheduler
    {
        internal Schedule Schedule(
            ref Dictionary<string, Team> teams,
            int weekCount)
        {
            var weeklyGameCount = teams.Count() / 2;

            var weeks = new List<Week>();

            foreach (var i in Enumerable.Range(0, weekCount))
            {
                weeks.Add(ScheduleWeek(ref teams, weeklyGameCount));
            }

            return new Schedule { Weeks = weeks };
        }

        private Week ScheduleWeek(
            ref Dictionary<string, Team> teams,
            int gameCount)
        {
            var games = new List<Game>();

            var teamsByGames = 
                teams.Values.OrderBy(t => t.OpposingTeamCounts.Sum(c => c.Value))
                            .ToList();

            var week = new Week();

            foreach (var gameIndex in Enumerable.Range(0, gameCount))
            {
                var primary = teamsByGames[0];
                teamsByGames.Remove(primary);

                var opponent = SelectOpponent(teamsByGames, primary);
                teamsByGames.Remove(opponent);

                teams[primary.Name].OpposingTeamCounts[opponent.Name] ++;
                teams[opponent.Name].OpposingTeamCounts[primary.Name] ++;

                games.Add(new Game { Teams = new Team[] { primary, opponent } });
            }

            week.GamesWithoutDrawAssignment = games;

            return week;
        }

        private Team SelectOpponent(
            IEnumerable<Team> teams,
            Team primary)
        {
            var teamsByOpponentCount =
                teams.OrderBy(t => t.OpposingTeamCounts[primary.Name]);

            return teamsByOpponentCount.ElementAt(0);
        }
    }
}