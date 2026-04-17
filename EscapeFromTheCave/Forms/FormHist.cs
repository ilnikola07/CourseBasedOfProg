using System;
using EscapeLibrary;

namespace EscapeFromTheCave.Forms
{
    public partial class FormHist : Form
    {
        public FormHist()
        {
            InitializeComponent();
            labelStory.Text = string.Empty; // Чтобы убрать надпись в лейбле
            labelNext.Text = string.Empty; // Чтобы убрать надпись в кнопке "далее"
        }

        private StoryManager _story = new StoryManager(); // Объект, хранящий тексты

        private void FormHist_Load(object sender, EventArgs e) // Загрузка формы
        {
            labelStory.Text = ""; // Предварительно очищаем        
            labelNext.Text = "Skip"; // Текст на кнопке в начале
            labelGetUp.Visible = false; // Скрываем кнопку подъема в самом начале
            labelGetUp.Cursor = Cursors.Hand; // меняет курсор на руку при наведении
        }

        private void timerFadeIn_Tick(object sender, EventArgs e) // Затемнение для красивого появляения
        {
            timerFadeIn.Interval = 30;
            if (this.Opacity < 1)// Постепенно увеличиваем непрозрачность
            {
                this.Opacity += 0.05; 
            }
            else
            {
                timerFadeIn.Stop(); // Когда форма полностью проявилась
                timerTypewriter.Start(); // Запускаем таймер текста (предысторию)
            }
        }

        private void timerTypewriter_Tick(object sender, EventArgs e) // Таймер для печати текста 
        {
            char? nextChar = _story.GetNextChar(); // Берем следующий символ
            if (nextChar != null)
            {
                labelStory.Text += nextChar; 
            }
            else
            {
                timerTypewriter.Stop(); // Букв больше нет — останавливаем печать
                labelNext.Text = "Next"; // Фраза допечатана
                if (_story.IsLastPhrase)
                {
                    ShowGetUpButton(); // Если сюжет кончился — показываем финальную кнопку
                }
                else
                {
                    labelNext.Text = "Next"; // Если есть еще фразы, показываем далее
                    labelNext.Visible = true;
                }
            }
        }

        private void ShowGetUpButton() // Для появления кнопки подняться с пола
        {
            labelNext.Visible = false; 
            labelGetUp.Visible = true; 
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (timerTypewriter.Enabled) // Игрок нажал пропустить во время печати
            {
                timerTypewriter.Stop();
                labelStory.Text = _story.GetCurrentFullPhrase();

                if (_story.IsLastPhrase)
                {
                    ShowGetUpButton();
                }
                else
                {
                    labelNext.Text = "Next";
                }
            }
            else // Игрок нажал далее, когда текст уже напечатан
            {
                if (_story.MoveToNextPhrase())
                {
                    labelStory.Text = "";
                    labelNext.Text = "Skip";
                    labelGetUp.Visible = false;
                    timerTypewriter.Start();
                }
            }
        }

        private void FormGame_KeyDown(object sender, KeyEventArgs e) // выход из игры по кнопке esc
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }
        private void labelGetUp_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer fadeOutTimer = new System.Windows.Forms.Timer { Interval = 30 }; // Создаем таймер для плавного исчезновения текущей формы
            fadeOutTimer.Tick += (s, ev) =>
            {
                if (this.Opacity > 0)
                {
                    this.Opacity -= 0.05; // Уменьшаем видимость
                }
                else
                {
                    fadeOutTimer.Stop();
                    FormGame game = new FormGame(1);
                    game.Opacity = 0; // Делаем новую форму пока прозрачной
                    game.Show();
                    this.Hide();
                }
            };
            fadeOutTimer.Start();
        }
    }
}
