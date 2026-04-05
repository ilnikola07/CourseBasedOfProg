using System;
using EscapeLibrary;

namespace EscapeFromTheCave.Forms
{
    public partial class FormGame : Form
    {
        public FormGame()
        {
            InitializeComponent();
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            labelStory.Text = ""; // Предварительно очищаем        
            labelNext.Text = "Skip"; // Текст на кнопке в начале            
        }

        private void timerFadeIn_Tick(object sender, EventArgs e)
        {
            timerFadeIn.Interval = 30;
            if (this.Opacity < 1)// Постепенно увеличиваем непрозрачность
            {
                this.Opacity += 0.05; // Шаг проявления 0,05
            }
            else
            {
                timerFadeIn.Stop();// Когда форма полностью проявилась
                timerTypewriter.Start();// Запускаем таймер текста (предысторию)
            }
        }


        private StoryManager _story = new StoryManager();
        private void timerTypewriter_Tick(object sender, EventArgs e) // Таймер для печати текста 
        {
            char? nextChar = _story.GetNextChar();

            if (nextChar != null)
            {
                labelStory.Text += nextChar;
            }
            else
            {
                timerTypewriter.Stop();
                labelNext.Text = "Next"; // Фраза допечатана
                if (_story.IsLastPhrase)
                {
                    ShowGetUpButton(); // Метод для появления твоей «кнопки»
                }

            }
        }

        private void ShowGetUpButton()
        {
            // Скрываем кнопку "Далее" (если она была)
            labelNext.Visible = false;

            //// Показываем твой лейбл-кнопку
            //labelGetUp.Text = "ПОДНЯТЬСЯ С ПОЛА";
            labelGetUp.Visible = true;

        }

        private void btnNext_Click(object sender, EventArgs e) // Кнопка "далее" или "пропустить"
        {
            if (timerTypewriter.Enabled)
            {
                timerTypewriter.Stop(); // Если еще печатает — показываем всё сразу
                labelStory.Text = _story.GetCurrentFullPhrase();
                labelNext.Text = "Next";
            }
            else
            {
                if (_story.MoveToNextPhrase()) // Если фраза уже напечатана полностью — переход к следующей
                {
                    labelStory.Text = "";
                    labelNext.Text = "Skip";
                    timerTypewriter.Start();
                }
                else
                {
                    StartActualGame(); // Метод для запуска игры
                }
            }
        }
        private void StartActualGame()
        {
            labelStory.Visible = false; // 1. Убираем всё, что относилось к тексту?
            labelNext.Visible = false;
            this.Focus();
        }

        private void FormGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }
    }
}
