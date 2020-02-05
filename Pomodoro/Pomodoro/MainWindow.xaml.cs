using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Pomodoro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public int TotalPomodorsPerDay { get; set; }
        private int StoppedThimeAt { get; set; }
        private int Pomodoro { get; set; } = 1500;
        public int ShortBreak { get; set; } = 300;
        public int LongBreak { get; set; } = 600;

        private DispatcherTimer _dt = new DispatcherTimer();
        private int _count = 0;
        private readonly ToastViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            _vm = new ToastViewModel();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void DtShortBreak(object sender, EventArgs e)
        {
            ShortBreak--;
            Pomodoro = 1500;
            TimeSpan elapsed = TimeSpan.FromSeconds(ShortBreak);
            PomodoroTimer.Text = elapsed.ToString(@"m\:ss");
        }

        private void DtLongBreak(object sender, EventArgs e)
        {
            LongBreak--;
            Pomodoro = 1500;
            TimeSpan elapsed = TimeSpan.FromSeconds(LongBreak);
            PomodoroTimer.Text = elapsed.ToString(@"m\:ss");
        }

        private void DtTicket(object sender, EventArgs e)
        {
            Pomodoro--;
            TimeSpan elapsed = TimeSpan.FromSeconds(Pomodoro);
            PomodoroTimer.Text = elapsed.ToString(@"m\:ss");
        }

        private void DtResume(object sender, EventArgs e)
        {
            StoppedThimeAt--;
            TimeSpan elapsed = TimeSpan.FromSeconds(StoppedThimeAt);
            PomodoroTimer.Text = elapsed.ToString(@"m\:ss");
        }

        private void DtPause(object sender, EventArgs e)
        {
            StoppedThimeAt--;
            TimeSpan elapsed = TimeSpan.FromSeconds(StoppedThimeAt);
            PomodoroTimer.Text = elapsed.ToString(@"m\:ss");
        }

        private void Start_Pomodoro(object sender, RoutedEventArgs e)
        {
            this._dt = new DispatcherTimer();
            _dt.Interval = TimeSpan.FromSeconds(1);
            _dt.Tick += DtTicket;
            _dt.Start();
        }

        private void Short_Break(object sender, RoutedEventArgs e)
        {
            _dt = new DispatcherTimer();
            _dt.Interval = TimeSpan.FromSeconds(1);
            _dt.Tick += DtShortBreak;
            _dt.Start();
        }

        private void Long_Break(object sender, RoutedEventArgs e)
        {
            _dt = new DispatcherTimer();
            _dt.Interval = TimeSpan.FromSeconds(1);
            _dt.Tick += DtLongBreak;
            _dt.Start();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Start(object sender, RoutedEventArgs e)
        {
            if (Pomodoro == StoppedThimeAt)
            {
                _dt.Tick += DtResume;
                _dt.Start();
            }
            else
            {
                _dt.Interval = TimeSpan.FromSeconds(1);
                _dt.Tick += DtTicket;
                _dt.Start();
            }

        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            this.StoppedThimeAt = Pomodoro;
            _dt.Stop();
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            Pomodoro = 1500;
            _dt = new DispatcherTimer();
            _dt.Interval = TimeSpan.FromSeconds(1);
            _dt.Tick += DtTicket;
            _dt.Start();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }
    }


}
