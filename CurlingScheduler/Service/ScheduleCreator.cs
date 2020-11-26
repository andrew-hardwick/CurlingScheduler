using CurlingScheduler.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurlingScheduler.Service
{
    public class ScheduleCreator
    {
        private readonly GameScheduler _gameScheduler = new GameScheduler();
        private readonly DrawBalancer _drawScheduler = new DrawBalancer();
        private readonly SheetBalancer _sheetBalancer = new SheetBalancer();

        private readonly OutputWriter _outputWriter = new OutputWriter();

        private Random _random = new Random();

        public void CreateSchedule(
            IEnumerable<string> teamNames,
            int sheetCount,
            int drawCount,
            int weekCount,
            DrawAlignment drawAlignment)
        {
            var teams = teamNames.OrderBy(n => _random.Next(10000))
                                 .Select(n => new Team(n, teamNames, drawCount, sheetCount))
                                 .ToDictionary(n => n.Name, n => n);

            //Schedule Games
            var schedule = _gameScheduler.Schedule(ref teams, weekCount);

            //Balance Draws

            _drawScheduler.Schedule(
                ref teams, 
                ref schedule, 
                weekCount, 
                drawCount,
                sheetCount,
                drawAlignment);

            //Balance Sheets
            _sheetBalancer.Schedule(
                ref teams,
                ref schedule,
                weekCount,
                drawCount,
                sheetCount);

            //Balance Stones

            _outputWriter.Write(
                schedule,
                "C:\\Users\\drewh\\Desktop\\testSchedule.dat");

            //_outputWriter.Write(
            //    teams,
            //    "C:\\Users\\drewh\\Desktop\\testTeams.dat");
        }
    }
}