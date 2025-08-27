using System;
using System.Windows.Threading;
using quiz.ViewModel.BaseClass;


namespace World_Game.ViewModels.utils
{
    public  class TimerViewModel : ViewModelBase
    {

        private DispatcherTimer _timer;
        private int _secondsPassed = 0;
        private string _elapsedTime = "00:00";

        public string ElapsedTime
        {
            get => "Czas: "+ _elapsedTime.ToString();
            private set
            {
                _elapsedTime = value;
                onPropertyChanged(nameof(ElapsedTime));
            }
        }

        public TimerViewModel()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += _timer_Tick;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _secondsPassed++;
            ElapsedTime = TimeSpan.FromSeconds(_secondsPassed).ToString(@"mm\:ss");
        }

        public void Start()
        {
            _secondsPassed = 0;
            ElapsedTime = "00:00";
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

    }
}
