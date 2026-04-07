using System;
using EscapeLibrary;

namespace EscapeFromTheCave.Forms
{
    public partial class FormHist : Form
    {
        public FormHist()
        {
            InitializeComponent();
        }

        private void FormHist_Load(object sender, EventArgs e)
        {
            labelStory.Text = ""; // Предварительно очищаем        
            labelNext.Text = "Skip"; // Текст на кнопке в начале
            labelGetUp.Visible = false; // Скрываем кнопку подъема в самом начале
            labelGetUp.Cursor = Cursors.Hand; // Меняет курсор на "палец" при наведении
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
                else
                {
                    labelNext.Text = "Next"; // Если есть еще фразы, показываем "Далее"
                    labelNext.Visible = true;
                }

            }
        }

        private void ShowGetUpButton()
        {
            labelNext.Visible = false; // Больше нельзя нажимать "Далее"
            labelGetUp.Visible = true; // Появляется "GET UP" (подняться с пола)

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (timerTypewriter.Enabled) // Игрок нажал "Пропустить" во время печати
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
            else // Игрок нажал "Далее", когда текст уже напечатан
            {
                if (_story.MoveToNextPhrase())
                {
                    labelStory.Text = "";
                    labelNext.Text = "Skip";
                    labelGetUp.Visible = false; // На всякий случай скрываем
                    timerTypewriter.Start();
                }
                else
                {
                    // Если фраз больше нет, кнопка "Далее" уже должна была 
                    // смениться на ShowGetUpButton через код выше
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


        private void labelGetUp_Click(object sender, EventArgs e)
        {
            // Открываем основную игровую форму и передаем ID стартовой пещеры (например, 1)
            FormGame mainGame = new FormGame(1);
            mainGame.Show();
            this.Hide(); // Прячем форму предыстории
        }
    }
}
