namespace EscapeLibrary
{
    public class StoryManager
    { 

        //НОРМАЛЬНЫЙ ТЕКСТ для предыстории:
        //"Жар лавы обжигает лицо...",
        //    "Путь наверх заблокирован обвалом.",
        //    "Нужно найти другой выход, пока пещера не стала могилой!",
        //    "Один из путников отбился от группы вулканологов. И это Вы! ", 
        //    "Совсем скоро вулканические пещеры, которые вы изучали \n" +
        //        " вновь затопит раскалённая магма. " ,
        //    "Ваша задача - выбраться из этого пекла, пока вы не сгорите... \n" +
        //        "или случайно не задохнётесь... БЕГИ!"

        private readonly string[] _phrasesHist = // Массив строк с текстом для предыстории
        {
            "Ж",
            "П.",
            "Нгилой!",
            "О. И это Вы! ", 
            "Сизучали \n" +
                " внгма. " ,
            "Ваша зае... \n" +
                "илиЕГИ!"
        };

        private int _currentPhraseIndex = 0; // Указатели, этот помнит на какой фразе сейчас
        private int _currentCharIndex = 0; // этот - на каком символе внутри

        public string GetCurrentFullPhrase()// Возвращает текущую фразу целиком (нужно для пропуска анимации)  
        {
            return _phrasesHist[_currentPhraseIndex];
        }   

        public char? GetNextChar()// Возвращает следующую букву текущей фразы
        {
            if (_currentCharIndex < _phrasesHist[_currentPhraseIndex].Length)
            {
                return _phrasesHist[_currentPhraseIndex][_currentCharIndex++];
            }
            return null; // Буквы в этой фразе кончились
        }       
        public bool MoveToNextPhrase() // Переход к следующей фразе, возвращает false, если истории конец
        {
            if (_currentPhraseIndex < _phrasesHist.Length - 1)
            {
                _currentPhraseIndex++;
                _currentCharIndex = 0;
                return true;
            }
            return false;
        }

        public bool IsPhraseFinished
        {
            get
            {
                if (_currentCharIndex >= _phrasesHist[_currentPhraseIndex].Length) // Достигнут ли конец текста фразы
                {
                    return true;//если индекс больше или равен длине, значит, все буквы уже выведены на экран (фраза завершена)
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsLastPhrase // Проверяет, является ли текущая фраза самой последней в массиве предыстории
        {
            get
            {                
                if (_currentPhraseIndex == _phrasesHist.Length - 1)// Совпадает ли индекс текущей фразы с индексом последней в массиве
                {
                    return true; // да, это последняя фраза
                }
                else
                {
                    return false; // нет, впереди есть ещё текст
                }
            }
        }
    }
}
