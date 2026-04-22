using System;
using EscapeLibrary;

namespace EscapeFromTheCave.Forms
{
    public partial class FormHist : Form
    {
        private StoryManager _story = new StoryManager(); // объект, хранящий тексты
        private System.Windows.Forms.Timer _fadeOutTimer; // ТАЙМЕР для управления плавным исчезновением

        public FormHist()
        {
            InitializeComponent();
            labelStory.Text = string.Empty; // чтобы убрать надпись в лейбле
            labelNext.Text = string.Empty; // чтобы убрать надпись в кнопке "далее"
        }

        private void FormHist_Load(object sender, EventArgs e) // загрузка формы
        {
            labelStory.Text = ""; // предварительно очищаем        
            labelNext.Text = "Skip"; // текст на кнопке в начале
            labelGetUp.Visible = false; // скрываем кнопку подъема в самом начале
            labelGetUp.Cursor = Cursors.Hand; // меняет курсор на руку при наведении
        }

        private void timerFadeIn_Tick(object sender, EventArgs e) // затемнение для красивого появляения
        {
            timerFadeIn.Interval = 30;
            if (this.Opacity < 1)// постепенно увеличиваем непрозрачность
            {
                this.Opacity += 0.05;
            }
            else
            {
                timerFadeIn.Stop(); // когда форма полностью проявилась
                timerTypewriter.Start(); // запускаем таймер текста (предысторию)
            }
        }

        private void timerTypewriter_Tick(object sender, EventArgs e) // таймер для печати текста 
        {
            char? nextChar = _story.GetNextChar(); // берем следующий символ
            if (nextChar != null)
            {
                labelStory.Text += nextChar;
            }
            else
            {
                timerTypewriter.Stop(); // букв больше нет — останавливаем печать
                labelNext.Text = "Next"; // фраза допечатана
                if (_story.IsLastPhrase)
                {
                    ShowGetUpButton(); // если сюжет кончился — показываем финальную кнопку
                }
                else
                {
                    labelNext.Text = "Next"; // если есть еще фразы, показываем далее
                    labelNext.Visible = true;
                }
            }
        }

        private void ShowGetUpButton() // для появления кнопки подняться с пола
        {
            labelNext.Visible = false;
            labelGetUp.Visible = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (timerTypewriter.Enabled) // игрок нажал пропустить во время печати
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
            else // игрок нажал далее, когда текст уже напечатан
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
            _fadeOutTimer = new System.Windows.Forms.Timer { Interval = 30 };
            _fadeOutTimer.Tick += OnFadeOutTick; // подписываемся на именованный метод
            _fadeOutTimer.Start();
        }

        private void OnFadeOutTick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.05; // уменьшаем видимость
            }
            else
            {
                _fadeOutTimer.Stop();
                _fadeOutTimer.Tick -= OnFadeOutTick; // отписываемся от события

                FormGame game = new FormGame(1);
                game.Opacity = 0; // делаем новую форму пока прозрачной
                game.Show();
                this.Hide();
            }
        }
    }
}