using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics.CodeAnalysis;

namespace EscapeLibrary
{
    [ExcludeFromCodeCoverage] // так как класс создан для работы с кнопками, смысла тестировать его нет
    public class ButtonManager
    {
        private readonly Button[] _pathButtons; // массив кнопок

        private static readonly Color BackgroundColor = Color.Black;
        private static readonly Color ForegroundColor = Color.White;
        private static readonly Color BorderColor = Color.White;
        private static readonly Color HoverColor = Color.Red;
        private static readonly Font ButtonFont = new Font("Algerian", 14, FontStyle.Bold);
        private const int ButtonSpacing = 10; 

        public ButtonManager(Button[] buttons) // принимает массив кнопок из формы
        {
            if (buttons == null)
                throw new ArgumentNullException(nameof(buttons));

            _pathButtons = buttons; // сохраняем переданный массив
            InitializeButtons(); 
        }

        private void InitializeButtons() 
        {
            foreach (var btn in _pathButtons) 
            {
                if (btn != null)
                {
                    CourseStyle(btn); 
                    ObvEffects(btn); 
                }
            }
        }

        private void CourseStyle(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = BackgroundColor; 
            btn.ForeColor = ForegroundColor;
            btn.FlatAppearance.BorderSize = 2;
            btn.Font = ButtonFont;
            btn.Cursor = Cursors.Hand;
        }

        private void ObvEffects(Button btn)
        {
            btn.MouseEnter += (s, e) =>
            {
                if (s is Button button)
                    button.FlatAppearance.BorderColor = HoverColor;
            };

            btn.MouseLeave += (s, e) =>
            {
                if (s is Button button)
                    button.FlatAppearance.BorderColor = BorderColor;
            };
        }

        public void Clicks(EventHandler onClick)
        {
            foreach (var btn in _pathButtons)
            {
                if (btn != null)
                    btn.Click += onClick;
            }
        }

        public void PathButton(int index, int caveId, int time) // настройки кнопки возможных путей в пещеры
        {
            if (IsValidIndex(index))
            {
                var btn = _pathButtons[index];
                btn.Text = $"Cave {caveId} (Time: {time})";
                btn.Tag = caveId;
                btn.Enabled = true;
                btn.Visible = true;
                btn.BringToFront();
            }
        }

        public void BackButton(int index)
        {
            if (IsValidIndex(index))
            {
                var btn = _pathButtons[index];
                btn.Text = "Back";
                btn.Tag = -2; // специальный id для возврата
                btn.Enabled = true;
                btn.Visible = true;
                btn.BringToFront();
            }
        }

        public void ScreamButton(int index)
        {
            if (IsValidIndex(index))
            {
                var btn = _pathButtons[index];
                btn.Text = "Scream";
                btn.Tag = -1; // специальный id для крика
                btn.Enabled = true;
                btn.Visible = true;
                btn.BringToFront();
            }
        }

        public void HideButton(int index) // если надо убрать кнопку
        {
            if (IsValidIndex(index))
                _pathButtons[index].Visible = false;
        }

        public void SetupExitButton(Action onClick, Panel panel) // настройки для конца игры, чтобы кнопка была на всю панель
        {
            var exitBtn = _pathButtons[3];
            exitBtn.Text = "Exit the game";
            exitBtn.Tag = -3;
            exitBtn.Click += (s, e) => onClick?.Invoke();

            
            int spacing = ButtonSpacing;
            exitBtn.Location = new Point(spacing, spacing);
            exitBtn.Size = new Size(panel.Width - (spacing * 2),panel.Height - (spacing * 2));

            exitBtn.Visible = true;
            exitBtn.Enabled = true;
            exitBtn.BringToFront();
        }


        public void PositionButtons(Form form, Panel panel) // позиция кнопок на форме
        {
            panel.Width = form.Width; // настройка панели
            panel.Height = 120;
            panel.Top = form.Height - panel.Height;
            panel.Left = 0;

            int spacing = ButtonSpacing; // размеры кнопок
            int buttonWidth = (form.Width - (3 * spacing)) / 2;
            int buttonHeight = (panel.Height - (3 * spacing)) / 2;

            for (int i = 0; i < 4; i++) // позиция каждой кнопки
            {
                int column = i % 2;
                int row = i / 2;

                int x = spacing + column * (buttonWidth + spacing);
                int y = spacing + row * (buttonHeight + spacing);

                _pathButtons[i].Size = new Size(buttonWidth, buttonHeight);
                _pathButtons[i].Location = new Point(x, y);
            }
        }
        private bool IsValidIndex(int index) 
        {
            if (index < 0 || index >= _pathButtons.Length)
            {
                return false; 
            }
            return true;
        }

        //public Button GetButton(int index)
        //{
        //    if (IsValidIndex(index))
        //    {
        //        return _pathButtons[index];
        //    }
        //    return null;
        //}
    }
}