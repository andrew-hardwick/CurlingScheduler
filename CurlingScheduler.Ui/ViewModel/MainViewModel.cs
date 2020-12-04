using CurlingScheduler.Model;
using CurlingScheduler.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CurlingScheduler.Ui.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private RelayCommand _generateSchedule;

        private ObservableCollection<string> _availableDrawAlignment =
            new ObservableCollection<string>(new string[] { "Balanced", "Squished" });

        private string _drawAlignment = "Squished";
        private string _teamsText = string.Empty;
        private string _gameSchedule = string.Empty;
        private string _stoneSchedule = string.Empty;

        private int _sheetCount = 4;
        private int _weekCount = 8;
        private int _drawCount = 1;
        private int _drawCountMinimum = 1;

        private bool _balanceStones = true;

        private IEnumerable<string> _teams;
        private ScheduleCreator _scheduleCreator;

        public MainViewModel(
            ScheduleCreator scheduleCreator)
        {
            _scheduleCreator = scheduleCreator;
        }

        public RelayCommand GenerateSchedule => _generateSchedule ?? (_generateSchedule = new RelayCommand(() =>
        {
            var alignment = (DrawAlignment)Enum.Parse(typeof(DrawAlignment), DrawAlignment);

            (GameSchedule, StoneSchedule) = _scheduleCreator.CreateSchedule(_teams, SheetCount, DrawCount, WeekCount, alignment, BalanceStones);
        }));

        private void UpdateDrawCountMinimum()
        {
            var teamCount = _teams.Count();

            var notEven = teamCount % (2 * SheetCount) != 0;

            DrawCountMinimum = teamCount / (2 * SheetCount) + (notEven ? 1 : 0);

            if (DrawCount < DrawCountMinimum)
            {
                DrawCount = DrawCountMinimum;
            }
        }

        public ObservableCollection<string> AvailableDrawAlignment
        {
            get => _availableDrawAlignment;
            set => Set(() => AvailableDrawAlignment, ref _availableDrawAlignment, value);
        }

        public string DrawAlignment
        {
            get => _drawAlignment;
            set => Set(() => DrawAlignment, ref _drawAlignment, value);
        }

        public string TeamsText
        {
            get => _teamsText;
            set
            {
                Set(() => TeamsText, ref _teamsText, value);

                _teams = TeamsText.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                                  .ToHashSet() ;

                UpdateDrawCountMinimum();
            }
        }

        public string GameSchedule
        {
            get => _gameSchedule;
            set => Set(() => GameSchedule, ref _gameSchedule, value);
        }

        public string StoneSchedule
        {
            get => _stoneSchedule;
            set => Set(() => StoneSchedule, ref _stoneSchedule, value);
        }

        public int SheetCount
        {
            get => _sheetCount;
            set
            {
                Set(() => SheetCount, ref _sheetCount, value);
                UpdateDrawCountMinimum();
            }
        }

        public int WeekCount
        {
            get => _weekCount;
            set => Set(() => WeekCount, ref _weekCount, value);
        }

        public int DrawCount
        {
            get => _drawCount;
            set => Set(() => DrawCount, ref _drawCount, value);
        }

        public int DrawCountMinimum
        {
            get => _drawCountMinimum;
            set => Set(() => DrawCountMinimum, ref _drawCountMinimum, value);
        }

        public bool BalanceStones
        {
            get => _balanceStones;
            set => Set(() => BalanceStones, ref _balanceStones, value);
        }
    }
}