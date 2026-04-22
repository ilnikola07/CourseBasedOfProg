using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics.CodeAnalysis;

namespace EscapeLibrary
{
    [ExcludeFromCodeCoverage]
    public class ButtonManager
    {
        private readonly Button[] _pathButtons;

        private static readonly Color BackgroundColor = Color.Black;
        private static readonly Color ForegroundColor = Color.White;
        private static readonly Color BorderColor = Color.White;
        private static readonly Color HoverColor = Color.Red;
        private static readonly Font ButtonFont = new Font("Algerian", 14, FontStyle.Bold);
        private const int ButtonSpacing = 10;
        
        private Action _exitAction; // поле для хранения действия выхода 

        public ButtonManager(Button[] buttons)
        {
            if (buttons == null)
                throw new ArgumentNullException(nameof(buttons));

            _pathButtons = buttons;
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
            btn.FlatAppearance.BorderColor = BorderColor; 
        }

        private void ObvEffects(Button btn)
        {
            btn.MouseEnter += OnButtonMouseEnter;
            btn.MouseLeave += OnButtonMouseLeave;
        }

        private void OnButtonMouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null) btn.FlatAppearance.BorderColor = HoverColor;
        }

        private void OnButtonMouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null) btn.FlatAppearance.BorderColor = BorderColor;
        }

        public void Clicks(EventHandler onClick)
        {
            foreach (var btn in _pathButtons)
            {
                if (btn != null) btn.Click += onClick;
            }
        }

        public void PathButton(int index, int caveId, int time)
        {
            if (IsValidIndex(index))
            {
                var btn = _pathButtons[index];
                btn.Text = "Cave " + caveId + " (Time: " + time + ")";
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
                btn.Tag = -2;
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
                btn.Tag = -1;
                btn.Enabled = true;
                btn.Visible = true;
                btn.BringToFront();
            }
        }

        public void HideButton(int index)
        {
            if (IsValidIndex(index))
                _pathButtons[index].Visible = false;
        }

        public void SetupExitButton(Action onClick, Panel panel)
        {
            int lastIndex = _pathButtons.Length - 1;

            if (IsValidIndex(lastIndex))
            {
                var exitBtn = _pathButtons[lastIndex];
                _exitAction = onClick; 

                exitBtn.Text = "Exit the game";
                exitBtn.Tag = -3;

                exitBtn.Click -= OnExitButtonClick;
                exitBtn.Click += OnExitButtonClick;

                int spacing = ButtonSpacing;
                exitBtn.Location = new Point(spacing, spacing);
                exitBtn.Size = new Size(panel.Width - (spacing * 2), panel.Height - (spacing * 2));

                exitBtn.Visible = true;
                exitBtn.Enabled = true;
                exitBtn.BringToFront();
            }
        }

        private void OnExitButtonClick(object sender, EventArgs e)
        {
            if (_exitAction != null)
            {
                _exitAction.Invoke();
            }
        }

        public void PositionButtons(Form form, Panel panel)
        {
            panel.Width = form.Width;
            panel.Height = 120;
            panel.Top = form.Height - panel.Height;
            panel.Left = 0;

            int count = _pathButtons.Length; 
            int spacing = ButtonSpacing;
            int buttonWidth = (form.Width - (3 * spacing)) / 2;
            int buttonHeight = (panel.Height - (3 * spacing)) / 2;

            for (int i = 0; i < count; i++)
            {
                if (_pathButtons[i] == null) continue;

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
            return index >= 0 && index < _pathButtons.Length;
        }
    }
}