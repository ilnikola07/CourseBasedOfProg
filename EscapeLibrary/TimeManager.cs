using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeLibrary
{
    public class TimeManager
    {
        private int _timeLeft; // тут хранятся оставшиеся секунды
        private System.Windows.Forms.Timer _timer;
        public event Action<string> TimeChanged; // событие время изменилось (передаёт строку в форму)
        public event Action TimeElapsed; // событие время вышло

        public TimeManager(int second)
        {
            _timeLeft = second;
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick; // привязываем метод Timer_Tick к каждому тику
        }

        public int GetRemainingTime()
        {
            return _timeLeft;
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void TriggerTimeElapsed()
        {
            StopAndNotify(); // безопасно вызываем событие и останавливаем таймер
        }

        public void Start()  // начало отсчёта таймера
        {
            _timer.Start();
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            if (_timeLeft > 0) // Если время ещё есть
            {
                _timeLeft--; // то отнимаем секунду
                UpdateUI(); // вывод в форму
            }
            else
            {
                StopAndNotify(); // иначе время кончилось
            }
        }

        public void SpendWeight(int weight) // метод вычета времени (для перехода по пещерам)
        {
            _timeLeft -= weight; // вычитаем вес перехода из общего запаса            
            if (_timeLeft < 0) // если время ушло в минус, приравниваем к 0
                _timeLeft = 0;

            UpdateUI(); // вызываем событие, чтобы Label на форме обновился мгновенно

            if (_timeLeft <= 0) // если время закончилось после перехода
            {
                StopAndNotify();
            }
        }
        private void UpdateUI()
        {
            string timeString = string.Format("{0:00}:{1:00}", _timeLeft / 60, _timeLeft % 60); // формат вывода
            TimeChanged?.Invoke($"Your time: {timeString}"); // вывод в форму
        }
        
        private void StopAndNotify() // общий метод для остановки и уведомления о конце времени
        {
            _timer.Stop();
            TimeElapsed?.Invoke(); 
        }

        public void Tick() // вспомогательный метод для тестов
        {
            Timer_Tick(null, EventArgs.Empty);
        }
    }
}