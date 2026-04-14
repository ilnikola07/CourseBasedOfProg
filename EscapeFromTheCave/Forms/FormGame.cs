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
        private Button[] _pathButtons; // Массив для быстрого управления 4-мя кнопками

        private TimeManager _timeManager;

        private MapManager _mapManager = new MapManager(); // Загрузчик карты
        private List<CavePath> _allPaths = new List<CavePath>(); // Весь список дорог
        private Stack<int> _history = new Stack<int>(); // история посещения
        private System.Windows.Forms.Timer _fadeInTimer; // таймер для плавного появления


        private void MainForm_KeyDown(object sender, KeyEventArgs e) // выход по кнопке esc (НУЖЕН БЫЛ ПОКА НЕ БЫЛО ОФИЦИАЛЬНОГО КОНЦА ИГРЫ)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }

        private LoadImage _imageLoader;

        public FormGame(int startCaveId)
        {
            InitializeComponent();

            labelEnd.Visible = false;

            _imageLoader = new LoadImage();

            _timeManager = new TimeManager(300); // 5 минут
            _timeManager.TimeChanged += (text) => labelTime.Text = text;
            _timeManager.TimeElapsed += () => EndGame("You died without ever getting out!", "smert.jpg");
            _timeManager.Start();

            this.KeyPreview = true;
            this.FormBorderStyle = FormBorderStyle.None; // нет рамок
            this.WindowState = FormWindowState.Maximized; // на весь экран
            this.BackgroundImageLayout = ImageLayout.Stretch; // растяжение
            this.DoubleBuffered = true; // от мерцания картинки
            this.BackColor = Color.Black;

            _currentCaveId = startCaveId; // Ставим игрока в начальную пещеру

            _pathButtons = new Button[] { buttonPath1, buttonPath2, buttonPath3, buttonPath4 };

            foreach (var btn in _pathButtons) // свойства для кнопок
            {
                if (btn != null)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.BackColor = Color.Black;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderSize = 2;
                    btn.FlatAppearance.BorderColor = Color.White;
                    btn.Font = new Font("Algerian", 14, FontStyle.Bold);
                    btn.Cursor = Cursors.Hand;
                    btn.Click += OnPathButton_Click;
                    btn.MouseEnter += (s, e) => { ((Button)s).FlatAppearance.BorderColor = Color.Red; };
                    btn.MouseLeave += (s, e) => { ((Button)s).FlatAppearance.BorderColor = Color.White; };
                }
            }

            string mapPath = Path.Combine(Application.StartupPath, "caves_graph.txt"); // Чтение графа из файла

            _allPaths = _mapManager.ReadMap(mapPath);

            this.Resize += (s, e) => UpdateCaveState();
            UpdateCaveState(); // Отрисовка в 1 раз

            this.Load += (s, e) => // Затемнение плавное
            {
                _fadeInTimer = new System.Windows.Forms.Timer { Interval = 30 };
                _fadeInTimer.Tick += (sender, args) =>
                {
                    if (this.Opacity < 1) this.Opacity += 0.05;
                    else _fadeInTimer.Stop();
                };
                _fadeInTimer.Start();
            };
        }

        private void UpdateCaveState()
        {
            var type = _mapManager.GetCaveType(_currentCaveId);

            if (type == CaveType.Victory) 
            {
                EndGame("VICTORY! You got out and you were saved...", "win_screen.jpg");
                return;
            }

            if (type == CaveType.Death) 
            {
                EndGame(" YOU DIED!" +
                    "\nreason: you burned in lava", "death_screen.jpg");
                return;
            }

            labelQuestion.Text = $"You are in a cave {_currentCaveId}. Where are we going?"; // ПЕРВОНАЧАЛЬНЫЙ ТЕКСТ, ПОЗЖЕ БУДЕТ НОРМАЛЬНЫЙ ВЫВОД

            this.BackgroundImage = _imageLoader.LoadCaveImage(_currentCaveId);

            var availablePaths = _mapManager.GetAvailablePaths(_allPaths, _currentCaveId); // ПОЛУЧАЕМ список дорог в текущей пещере
            
            for (int i = 0; i < 4; i++)
            {
                // Если это последняя кнопка и мы не в первой пещере
                if (i == 3 && _currentCaveId != 1)
                {
                    _pathButtons[i].Text = "Back";
                    _pathButtons[i].Tag = -2; // Специальный ID для возврата
                    _pathButtons[i].Enabled = true;
                }
                else if (i < availablePaths.Count) 
                {
                    var path = availablePaths[i];
                    _pathButtons[i].Text = $"Cave {path.ToId} (Time: {path.Time})";
                    _pathButtons[i].Tag = path.ToId;
                    _pathButtons[i].Enabled = true;
                }
                else
                {
                    _pathButtons[i].Text = "Scream"; // ПОЗЖЕ БУДУТ ДРУГИЕ ДЕЙ-Я ПОМИМО КРИКА
                    _pathButtons[i].Tag = -1;
                    _pathButtons[i].Enabled = true;
                }

                _pathButtons[i].Visible = true;
                _pathButtons[i].BringToFront();
            }

            panelButtons.BringToFront(); // На всякий случай выносим панель с кнопками и лейбл поверх фона 
            labelQuestion.BringToFront();

            // КОСМЕТИКА ДЛЯ КНОПОК
            panelButtons.Width = this.Width;
            panelButtons.Height = 120; // Высота панели под две строки кнопок
            panelButtons.Top = this.Height - panelButtons.Height;
            panelButtons.Left = 0;
            int spacing = 10; // Отступ между кнопками и от краев
            int buttonWidth = (this.Width - (3 * spacing)) / 2;
            int buttonHeight = (panelButtons.Height - (3 * spacing)) / 2;

            for (int i = 0; i < 4; i++)
            {
                int column = i % 2;
                int row = i / 2;   

                int x = spacing + column * (buttonWidth + spacing);
                int y = spacing + row * (buttonHeight + spacing);

                _pathButtons[i].Size = new Size(buttonWidth, buttonHeight); // Гарант, что кнопка внутри панели
                _pathButtons[i].Location = new Point(x, y);
            }
        }

        private void OnPathButton_Click(object sender, EventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.Tag is int nextCPathId)
            {
                if (nextCPathId == -3) // Выход
                {
                    Application.Exit();
                }
                else if (nextCPathId == -2) // Назад
                {
                    if (_history.Count > 0)
                    {
                        _currentCaveId = _history.Pop();
                        UpdateCaveState();
                    }
                }
                else if (nextCPathId == -1) // Крик
                {
                    MessageBox.Show("You screamed...");
                }
                else // Переход в другую пещеру
                {
                    // Находим путь с текущим и следующим ID
                    var path = _allPaths.FirstOrDefault(p =>
                        p.FromId == _currentCaveId && p.ToId == nextCPathId);

                    if (path != null)
                    {
                        // Вычитаем время перехода
                        _timeManager.SpendWeight(path.Time);

                        // Сохраняем историю и переходим
                        _history.Push(_currentCaveId);
                        _currentCaveId = nextCPathId;
                        UpdateCaveState();
                    }
                }
            }
        }


        private void FormGame_Load(object sender, EventArgs e)
        {
            try
            {
                _allPaths = _mapManager.ReadMap("caves_graph.txt");                 
                UpdateCaveState(); // Обновляем состояние
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке карты: {ex.Message}");
            }
        }


        private void EndGame(string message, string imageName) //метод обработки конца игры
        {
            Image endImage = _imageLoader.LoadEndGameImage(imageName);
            if (endImage != null)
            {
                this.BackgroundImage = endImage;
            }
            else
            {
                this.BackColor = (imageName.Contains("win")) ? Color.DarkGreen : Color.DarkRed;
            }
            for (int i = 0; i < 3; i++) // скрываем 3 кнопки
            {
                _pathButtons[i].Visible = false;
            }
            Button exitBtn = _pathButtons[3];
            exitBtn.Text = "Exit the game"; // Переделываем 4-ю кнопку на всю панель
            exitBtn.Tag = -3;

            // КОСМЕТИКА для 4-ой кнопки
            int spacing = 10;
            exitBtn.Location = new Point(spacing, spacing);
            exitBtn.Size = new Size(panelButtons.Width - (spacing * 2), panelButtons.Height - (spacing * 2));
            exitBtn.Visible = true;
            exitBtn.Enabled = true;

            labelEnd.Visible = true;
            labelEnd.Text = message; // вывод текста в лейбл
            labelEnd.ForeColor = Color.White;
            labelEnd.BringToFront();
            labelQuestion.Visible = false;
        }
    }
}