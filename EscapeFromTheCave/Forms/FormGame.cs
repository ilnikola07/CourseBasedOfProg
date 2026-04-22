using EscapeLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EscapeFromTheCave.Forms
{
    public partial class FormGame : Form
    {
        private int _currentCaveId;
        private TimeManager _timeManager;
        private ButtonManager _buttonManager;

        private MapManager _mapManager = new MapManager(); // загрузчик карты
        private List<CavePath> _allPaths = new List<CavePath>(); // список дорог
        private QuestManager _questManager = new QuestManager();

        private Stack<(int caveId, int time)> _history = new Stack<(int, int)>(); // история посещений (очередь для возвращения назад)
        private System.Windows.Forms.Timer _fadeInTimer; // таймер для плавного появления        
        private System.Windows.Forms.Timer timerTypewriter; // для эффекта появляения

        private void MainForm_KeyDown(object sender, KeyEventArgs e) // выход по кнопке esc (НУЖЕН БЫЛ ПОКА НЕ БЫЛО ОФИЦИАЛЬНОЙ СМЕРТИ ИЛИ ПОБЕДЫ В ИГРЕ)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }

        private LoadImage _imageLoader;

        public FormGame(int startCaveId)
        {
            InitializeComponent();

            labelEnd.Visible = false;

            _imageLoader = new LoadImage(); // Создаём экземпляры классов
            _timeManager = new TimeManager(600); // 10 минут

            _questManager.LoadQuestsFromFile("quests.txt"); // Читается файл с текстом квестов          

            _timeManager.TimeChanged += OnTimeChanged; // подписываемся на события (время изменилось/кончилось)
            _timeManager.TimeElapsed += OnTimeElapsed;
            _timeManager.Start();

            // КОСМЕТИКА
            this.KeyPreview = true;
            this.FormBorderStyle = FormBorderStyle.None; // нет рамок
            this.WindowState = FormWindowState.Maximized; // на весь экран
            this.BackgroundImageLayout = ImageLayout.Stretch; // растяжение
            this.DoubleBuffered = true; // от мерцания картинки
            this.BackColor = Color.Black;


            _buttonManager = new ButtonManager(new Button[] { buttonPath1, buttonPath2, buttonPath3, buttonPath4 }); // Создаём массив кнопок и передаём в класс управления кнопок
            _buttonManager.Clicks(OnPathButton_Click); // Обработчик клика

            timerTypewriter = new System.Windows.Forms.Timer(); // Создаём объект таймера для печати
            timerTypewriter.Interval = 50; // Скорость печати (50 мс на символ)
            timerTypewriter.Tick += timerTypewriter_Tick; // подписываемся на событие


            string mapPath = Path.Combine(Application.StartupPath, "caves_graph.txt"); // чтение графа из файла 

            _allPaths = _mapManager.ReadMap(mapPath); // содержит в себе всю структуру уровней
            _currentCaveId = startCaveId; // Ставим игрока в начальную пещеру

            this.Resize += OnFormResize;
            UpdateCaveState(); // Отрисовка в самый 1 раз

            this.Load += OnFormLoad;  // Для проявления         
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            _fadeInTimer = new System.Windows.Forms.Timer(); // Настройки таймера
            _fadeInTimer.Interval = 30;
            _fadeInTimer.Tick += OnFadeInTick; // Подписываемся на тики
            this.Opacity = 0; // начиная с полной прозрачности
            _fadeInTimer.Start();
        }

        private void OnFadeInTick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
            {
                this.Opacity += 0.05;
            }
            else
            {
                _fadeInTimer.Stop();
            }
        }

        private void OnFormResize(object sender, EventArgs e)
        {
            UpdateCaveState();
        }

        private void UpdateCaveState()
        {
            var type = _mapManager.GetCaveType(_currentCaveId);

            if (type == CaveType.Victory)
            {
                EndGame("VICTORY! " +
                    "\nYou got out and you were saved...", "win_screen.jpg");
                labelTime.Visible = false;
                return;
            }

            if (type == CaveType.Death)
            {
                EndGame(" YOU DIED!" +
                    "\nreason: you burned in lava", "death_screen.jpg");
                labelTime.Visible = false;
                return;
            }

            _questManager.SetCurrentQuest(_currentCaveId);
            labelQuestion.Text = "";
            timerTypewriter.Start(); // Запускаем анимацию печати 

            this.BackgroundImage = _imageLoader.LoadPhoto(_currentCaveId);

            var availablePaths = _mapManager.GetPaths(_allPaths, _currentCaveId); // ПОЛУЧАЕМ список дорог в текущей пещере

            SetupButtons(availablePaths); // Настройка кнопок через ButtonManager

            _buttonManager.PositionButtons(this, panelButtons); // Позиционирование кнопок

            panelButtons.BringToFront(); // Выносим на передний план
            labelQuestion.BringToFront();
        }
        private void OnTimeChanged(string text) // изменения времени
        {
            labelTime.Text = text;
        }

        private void OnTimeElapsed() // время кончилось и вы задохнулись
        {
            EndGame("You died without ever getting out!", "smert.jpg");
        }

        private void OnPathButton_Click(object sender, EventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.Tag is int nextCPathId)
            {
                if (nextCPathId == -3) // выход
                {
                    Application.Exit();
                }
                else if (nextCPathId == -2) // назад
                {
                    if (_history.Count > 0)
                    {
                        var (previousCaveId, timeSpent) = _history.Pop();
                        _timeManager.SpendWeight(timeSpent); // вычитаем время перехода
                        _currentCaveId = previousCaveId;
                        UpdateCaveState();
                    }
                }
                else if (nextCPathId == -1) // крик
                {
                    MessageBox.Show("You screamed, but screaming only takes away your time and energy...");
                    _timeManager.SpendWeight(5); // для отнятия 5 секунд
                }
                else // переход вперед
                {
                    var path = _allPaths.FirstOrDefault(p =>
                        p.FromId == _currentCaveId && p.ToId == nextCPathId);

                    if (path != null)
                    {
                        if (_timeManager.GetRemainingTime() < path.Time)  // проверяем достаточно ли времени для перехода
                        {
                            _timeManager.SpendWeight(_timeManager.GetRemainingTime()); // тратим оставшееся время
                            _timeManager.TriggerTimeElapsed();
                            return;
                        }
                        _timeManager.SpendWeight(path.Time);// вычитаем время перехода                        
                        _history.Push((_currentCaveId, path.Time)); // сохраняем информацию для возврата
                        _currentCaveId = nextCPathId;
                        UpdateCaveState();
                    }
                }
            }
        }

        private void EndGame(string message, string imageName) //метод обработки конца игры
        {
            _timeManager.Stop(); 
            timerTypewriter.Stop(); // останавливаем печать текста

            Image endImage = _imageLoader.LoadEndPhoto(imageName);

            if (endImage != null)
            {
                this.BackgroundImage = endImage;
            }
            else
            {
                this.BackColor = (imageName.Contains("win")) ? Color.DarkGreen : Color.DarkRed;
            }
            
            for (int i = 0; i < 4; i++)
            {
                _buttonManager.HideButton(i); // прячем все кнопки перед настройкой выхода
            }

            _buttonManager.SetupExitButton(OnExitAction, panelButtons);

            labelEnd.Visible = true;
            labelEnd.Text = message; // вывод текста в лейбл
            labelEnd.ForeColor = Color.White;
            labelEnd.BringToFront();
            labelQuestion.Visible = false;
        }

        private void OnExitAction()
        {
            Application.Exit();
        }

        private void timerTypewriter_Tick(object sender, EventArgs e)
        {
            char? next = _questManager.GetNextLetter();
            if (next != null)
                labelQuestion.Text += next;
            else
                timerTypewriter.Stop();
        }


        private void SetupButtons(List<CavePath> availablePaths)
        {            
            for (int i = 0; i < 4; i++) // сначала очищаем/скрываем все кнопки
            {
                _buttonManager.HideButton(i);
            }

            int currentBtn = 0;
                        
            for (int i = 0; i < availablePaths.Count && currentBtn < 4; i++) // выводим доступные пути вперед
            {
                var path = availablePaths[i];
                _buttonManager.PathButton(currentBtn, path.ToId, path.Time);
                currentBtn++;
            }

            if (_currentCaveId != 1 && currentBtn < 4) 
            {
                _buttonManager.BackButton(currentBtn);
                currentBtn++;
            }

            while (currentBtn < 4)
            {
                _buttonManager.ScreamButton(currentBtn);
                currentBtn++;
            }
        }
    }
}