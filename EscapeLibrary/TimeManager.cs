using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeLibrary
{
    public class TimeManager
    {
        private int _timeLeft; // Тут хранятся оставшиеся секунды

        private System.Windows.Forms.Timer _timer;
        public event Action<string> TimeChanged; // событие время изменилось (передаёт строку в форму) и время вышло
        public event Action TimeElapsed;

        public TimeManager(int second)
        {
            _timeLeft = second;
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick; // Привязываем метод Timer_Tick к каждому тику
        }

        public void Start()  // Начало отсчёта таймера
        {
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_timeLeft > 0) // Если время ещё есть
            {
                _timeLeft--; // то отнимаем секунду
                string timeString = string.Format("{0:00}:{1:00}", _timeLeft / 60, _timeLeft % 60);  // формат вывода
                TimeChanged?.Invoke($"Your time: {timeString}"); // Вывод в форму
            }
            else
            {
                _timer.Stop();
                TimeElapsed?.Invoke(); // иначе время кончилось
            }
        }



        public void SpendWeight(int weight) // Метод вычета времени (для перехода по пещерам)
        {
            // Вычитаем вес перехода (время) из общего запаса
            _timeLeft -= weight;

            // Если время ушло в минус, приравниваем к 0
            if (_timeLeft < 0) _timeLeft = 0;

            // Сразу вызываем событие, чтобы Label на форме обновился мгновенно
            string timeString = string.Format("{0:00}:{1:00}", _timeLeft / 60, _timeLeft % 60);
            TimeChanged?.Invoke($"Your time: {timeString}");

            // Если время закончилось после перехода
            if (_timeLeft <= 0)
            {
                _timer.Stop();
                TimeElapsed?.Invoke();
            }
        }
    }
}
