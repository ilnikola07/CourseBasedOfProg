using EscapeLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EscapeFromTheCave.Forms
{
    public partial class FormGame : Form
    {
        public FormGame()
        {
            InitializeComponent();
        }

        private int _currentCaveId;
        private List<CavePath> _allPaths;
        private MapManager _mapManager = new MapManager();

        // Кнопки путей (можно положить в массив для удобства)
        private Label[] _pathLabels;

        public FormGame(int startCaveId)
        {
            InitializeComponent();
            _currentCaveId = startCaveId;

            // Инициализируем массив вашими лейблами из дизайнера
            _pathLabels = new Label[] { lblPath1, lblPath2, lblPath3, lblPath4 };

            // Настраиваем каждый лейбл (курсор и события)
            foreach (var lbl in _pathLabels)
            {
                lbl.Cursor = Cursors.Hand; // Курсор-палец
                //lbl.Click += OnPathLabel_Click; // Событие клика
                lbl.MouseEnter += (s, e) => { ((Label)s).ForeColor = Color.Gold; }; // Подсветка при наведении
                lbl.MouseLeave += (s, e) => { ((Label)s).ForeColor = Color.White; }; // Возврат цвета
            }

            _allPaths = _mapManager.ReadMap("map.txt");
            UpdateCaveState();
            // Загружаем карту из файла
            _allPaths = _mapManager.ReadMap("path_to_your_file.txt");
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            UpdateCaveState();
        }

        private void UpdateCaveState()
        {
            // 1. Обновляем фон и текст вопроса (нужно будет создать словарь с описаниями пещер)
            labelQuestion.Text = $"Вы находитесь в пещере №{_currentCaveId}. Куда идем?";
            this.BackgroundImage = LoadCaveImage(_currentCaveId);

            // 2. Ищем доступные пути
            var availablePaths = _mapManager.GetAvailablePaths(_allPaths, _currentCaveId);

            // 3. Настраиваем кнопки
            for (int i = 0; i < _pathLabels.Length; i++)
            {
                if (i < availablePaths.Count)
                {
                    var path = availablePaths[i];
                    _pathLabels[i].Visible = true;
                    _pathLabels[i].Text = $"В пещеру {path.ToId} (Время: {path.Time} мин)";
                    _pathLabels[i].Tag = path.ToId; // Сохраняем ID назначения в Tag
                }
                else
                {
                    _pathLabels[i].Visible = false; // Прячем лишние кнопки
                }
            }
        }

        // Общий обработчик для всех 4-х кнопок выбора пути
        private void OnPathButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            _currentCaveId = (int)clickedButton.Tag; // Получаем ID из Tag
            UpdateCaveState(); // Перерисовываем экран для новой пещеры
        }

        private Image LoadCaveImage(int caveId)
        {
            // Путь к папке с картинками (например, в папке с запущенной игрой)
            // Картинки должны называться "1.jpg", "2.jpg" и т.д.
            string path = Path.Combine(Application.StartupPath, "Resources", "Caves", $"{caveId}.jpg");

            if (File.Exists(path))
            {
                return Image.FromFile(path);
            }
            else
            {
                // Если картинка не найдена, возвращаем стандартный фон (Thurston Lava Tube)
                return Properties.Resources.DefaultCaveBackground;
            }
        }

    }
}
