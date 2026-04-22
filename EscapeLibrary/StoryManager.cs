namespace EscapeLibrary
{
    public class StoryManager
    { 
        private readonly string[] _phrasesHist = 
        {
            "Perhaps volcanoes are real branches\n" +
                " of hell on Earth.",
            "What's more, this one was dormant,\n" +
                "and now it has woken up!",
            "Just a few minutes ago, seismologists\n" +
                " detected suspicious activity.",
            "All researchers are ordered to\n" +
                " evacuateimmediately from\n" +
                " all underground complexes.",
            "You need to find another way out before\n" +
                " the cave becomes a grave!",
            "Very soon, the volcanic caves you\n" +
                " were studying will be flooded\n" +
                "with glowing magma once again." ,
            "But one of the scientists got\n" +
                " separated from the group of \n" +
                "volcanologists. And that’s you!",
            "Tired of the heat, you fell asleep,\n" +
                " but now you saw only \n" +
                "  the running scientists",
            "Your task is to get out of this\n" +
                " hell before you burn... \n" +
                "or suffocate... RUN!"
        };

        private int _currentPhrase = 0; // указатели: этот помнит на какой фразе сейчас
        private int _currentChar = 0; // этот - на каком символе внутри

        public string GetCurrentFullPhrase()// возвращает текущую фразу целиком (нужно для пропуска анимации)  
        {
            return _phrasesHist[_currentPhrase];
        }   

        public char? GetNextChar()// возвращает следующую букву текущей фразы
        {
            if (_currentChar < _phrasesHist[_currentPhrase].Length)
                return _phrasesHist[_currentPhrase][_currentChar++];
            return null; // буквы в этой фразе кончились
        } 
        
        public bool MoveToNextPhrase() // переход к следующей фразе, возвращает false, если истории конец
        {
            if (_currentPhrase < _phrasesHist.Length - 1)
            {
                _currentPhrase++;
                _currentChar = 0;
                return true;
            }
            return false;
        }

        public bool IsLastPhrase // проверяет, является ли текущая фраза самой последней в массиве предыстории
        {
            get
            {                
                if (_currentPhrase == _phrasesHist.Length - 1)// совпадает ли индекс текущей фразы с индексом последней в массиве
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
