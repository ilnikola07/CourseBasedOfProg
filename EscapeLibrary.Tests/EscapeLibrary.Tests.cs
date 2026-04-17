using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EscapeLibrary;

namespace EscapeLibrary.Tests
{
    [TestClass]
    public class MapManagerTests
    {
        private MapManager _mapManager;

        [TestInitialize]
        public void Setup()
        {
            _mapManager = new MapManager();
        }
        
        [TestMethod]
        public void GetCaveType_VictoryCaveId_ReturnsVictory() // GetCaveType возвращает Victory для id = 12
        {
            int caveId = 12;

            var result = _mapManager.GetCaveType(caveId);

            Assert.AreEqual(CaveType.Victory, result);
        }
       
        [TestMethod]
        public void GetCaveType_DeathCaveId_ReturnsDeath()  // GetCaveType возвращает Death для id 19
        {
            int caveId = 19;

            var result = _mapManager.GetCaveType(caveId);

            Assert.AreEqual(CaveType.Death, result);
        }

        [TestMethod]
        public void GetCaveType_NormalCaveId_ReturnsNormal() // GetCaveType возвращает Normal для любого другого id
        {
            int caveId = 5;

            var result = _mapManager.GetCaveType(caveId);

            Assert.AreEqual(CaveType.Normal, result);
        }
        
        [TestMethod]
        public void GetPaths_ValidList_ReturnsFilteredPaths() // GetPaths возвращает только пути от текущей пещеры
        {
            var allPaths = new List<CavePath>
            {
                new CavePath { FromId = 1, ToId = 2, Time = 10 },
                new CavePath { FromId = 1, ToId = 3, Time = 15 },
                new CavePath { FromId = 2, ToId = 4, Time = 20 }
            };
            int currentCaveId = 1;

            var result = _mapManager.GetPaths(allPaths, currentCaveId);

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(p => p.FromId == currentCaveId));
        }

        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetPaths_NullList_ThrowsArgumentNullException() // GetPaths выбрасывает ошибку при null
        {
            List<CavePath> nullPaths = null;

            _mapManager.GetPaths(nullPaths, 1);
        }

        [TestMethod]
        public void ReadMap_Valid_ReturnsPaths() // спешный парсинг
        {
            string fileName = "map.txt";
            File.WriteAllLines(fileName, new[] { "1,2,10", "3,4,20" });

            var result = _mapManager.ReadMap(fileName);

            Assert.AreEqual(2, result.Count);
            File.Delete(fileName);
        }

        [TestMethod]
        public void ReadMap_Garbage_Skipped() // игнорирование кривых строк
        {
            string fileName = "bad.txt";
            File.WriteAllLines(fileName, new[] { "1,2", "err,err,err", "5,6,30" });

            var result = _mapManager.ReadMap(fileName);

            Assert.AreEqual(1, result.Count);
            File.Delete(fileName);
        }
    }

    [TestClass]
    public class StoryManagerTests
    {
        private StoryManager _storyManager;

        [TestInitialize]
        public void Setup()
        {
            _storyManager = new StoryManager();
        }
       
        [TestMethod]
        public void GetCurrentFullPhrase_InitialPhrase_ReturnsFirstPhrase()  // GetCurrentFullPhrase возвращает текущую фразу целиком
        {
            var expected = "Perhaps volcanoes are real branches\n of hell on Earth.";

            var result = _storyManager.GetCurrentFullPhrase();

            Assert.AreEqual(expected, result);
        }
        
        [TestMethod]
        public void GetNextChar_FirstThreeChars_ReturnsSequentialChars() // GetNextChar возвращает символы последовательно
        {
            var phrase = _storyManager.GetCurrentFullPhrase();

            var char1 = _storyManager.GetNextChar();
            var char2 = _storyManager.GetNextChar();
            var char3 = _storyManager.GetNextChar();

            Assert.AreEqual(phrase[0], char1);
            Assert.AreEqual(phrase[1], char2);
            Assert.AreEqual(phrase[2], char3);
        }
                
        [TestMethod]
        public void GetNextChar_AfterPhraseEnd_ReturnsNull() // GetNextChar возвращает null после конца фразы
        {
            var phrase = _storyManager.GetCurrentFullPhrase();
            for (int i = 0; i < phrase.Length; i++)
                _storyManager.GetNextChar(); // "прокликаем" всю фразу

            var result = _storyManager.GetNextChar();

            Assert.IsNull(result);
        }
        
        [TestMethod]
        public void MoveToNextPhrase_NotLastPhrase_ReturnsTrue() // MoveToNextPhrase возвращает true, пока есть фразы
        {
            // Arrange - начинаем с первой фразы

            var result = _storyManager.MoveToNextPhrase();

            Assert.IsTrue(result);
            Assert.AreEqual(1, _storyManager.GetCurrentFullPhrase().Contains("dormant") ? 1 : 0); // проверка смены фразы
        }
                
        [TestMethod]
        public void IsPhraseFinished_AllCharsRead_ReturnsTrue() //  IsPhraseFinished возвращает true после вывода всех символов
        {
            var phrase = _storyManager.GetCurrentFullPhrase();
            while (_storyManager.GetNextChar() != null) { } // читаем до конца

            var result = _storyManager.IsPhraseFinished;

            Assert.IsTrue(result);
        }
                
        [TestMethod]
        public void IsLastPhrase_InitialPhrase_ReturnsFalse() // IsLastPhrase возвращает false в начале
        {
            // аrrange - первая фраза

            var result = _storyManager.IsLastPhrase;

            Assert.IsFalse(result);
        }
    }

    [TestClass]
    public class QuestManagerTests
    {
        private QuestManager _questManager;

        [TestInitialize]
        public void Setup()
        {
            _questManager = new QuestManager();
        }

        [TestMethod]
        public void LoadQuests_MissingFile_Ignored()
        {
            _questManager.LoadQuestsFromFile("absent.txt");

            var field = typeof(QuestManager).GetField("_caveTexts",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var dict = (Dictionary<int, string>)field.GetValue(_questManager);

            Assert.AreEqual(0, dict.Count);
        }

        [TestMethod]
        public void LoadQuests_InvalidLines_Skipped()
        {
            string fileName = "invalid_quests.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            // строки без id в начале или пустые
            string content = "это мусор\n\n1 норм квест";
            File.WriteAllText(filePath, content);

            _questManager.LoadQuestsFromFile(fileName);

            var field = typeof(QuestManager).GetField("_caveTexts",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var dict = (Dictionary<int, string>)field.GetValue(_questManager);

            Assert.AreEqual(1, dict.Count);
            Assert.AreEqual("норм квест", dict[1]);

            if (File.Exists(filePath)) 
                File.Delete(filePath);
        }

        [TestMethod]
        public void SetCurrentQuest_ExistingCaveId_SetsCorrectText() // SetCurrentQuest устанавливает текст для существующего caveId
        {
            // для теста добавим данные напрямую 
            var field = typeof(QuestManager).GetField("_caveTexts",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var texts = new Dictionary<int, string> { { 5, "Test quest text" } };
            field?.SetValue(_questManager, texts);

            _questManager.SetCurrentQuest(5);
            var letter1 = _questManager.GetNextLetter();
            var letter2 = _questManager.GetNextLetter();

            Assert.AreEqual('T', letter1);
            Assert.AreEqual('e', letter2);
        }
        
        [TestMethod]
        public void SetCurrentQuest_UnknownCaveId_UsesDefaultText() // SetCurrentQuest использует дефолтный текст для неизвестного caveId
        {
            int unknownCaveId = 999;
            string expectedStart = "Описание этой пещеры";

            _questManager.SetCurrentQuest(unknownCaveId);
            var firstLetter = _questManager.GetNextLetter();

            Assert.AreEqual(expectedStart[0], firstLetter);
        }
        
        [TestMethod]
        public void GetNextLetter_AfterTextEnd_ReturnsNull() // GetNextLetter возвращает null после конца текста
        {
            _questManager.SetCurrentQuest(999); // дефолтный текст
            var text = "Описание этой пещеры потеряно во тьме...";
            for (int i = 0; i < text.Length; i++)
                _questManager.GetNextLetter();

            var result = _questManager.GetNextLetter();

            Assert.IsNull(result);
        }
    }

    [TestClass]
    public class LoadImageTests
    {
        private LoadImage _loader;

        [TestInitialize]
        public void Setup()
        {
            _loader = new LoadImage();
        }
                
        [TestMethod]
        public void LoadPhoto_NonExistentFile_ReturnsNull() //  LoadPhoto возвращает null для несуществующего файла
        {
            int fakeCaveId = 99999; // файл точно не существует

            var result = _loader.LoadPhoto(fakeCaveId);

            Assert.IsNull(result);
        }
                
        [TestMethod]
        public void LoadEndPhoto_NonExistentImage_ReturnsNull() // LoadEndPhoto возвращает null для несуществующего изображения
        {
            string fakeImageName = "nonexistent_fake_image.jpg";

            var result = _loader.LoadEndPhoto(fakeImageName);

            Assert.IsNull(result);
        }
    }

    [TestClass]
    public class TimeManagerTests
    {        
        [TestMethod]
        public void Constructor_InitializesTimeCorrectly() // конструктор инициализирует оставшееся время
        {
            int initialSeconds = 120;

            var timeManager = new TimeManager(initialSeconds);
            var result = timeManager.GetRemainingTime();

            Assert.AreEqual(initialSeconds, result);
        }
       
        [TestMethod]
        public void SpendWeight_ValidWeight_ReducesTime()  // SpendWeight уменьшает время корректно
        {
            var timeManager = new TimeManager(100);
            int weightToSpend = 30;
            int expected = 70;

            timeManager.SpendWeight(weightToSpend);
            var result = timeManager.GetRemainingTime();

            Assert.AreEqual(expected, result);
        }
                
        [TestMethod]
        public void SpendWeight_WeightExceedsTime_SetsTimeToZero() // SpendWeight не позволяет времени уйти в минус
        {
            var timeManager = new TimeManager(10);
            int weightToSpend = 50; // больше, чем есть

            timeManager.SpendWeight(weightToSpend);
            var result = timeManager.GetRemainingTime();

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Tick_DecrementsTimeByOne()
        {
            var timeManager = new TimeManager(60);

            timeManager.Tick(); 

            Assert.AreEqual(59, timeManager.GetRemainingTime());
        }

        [TestMethod]
        public void Tick_FiresTimeChangedEvent()
        {
            var timeManager = new TimeManager(65);
            string receivedMessage = null;
            timeManager.TimeChanged += (msg) => receivedMessage = msg;

            timeManager.Tick();

            Assert.AreEqual("Your time: 01:04", receivedMessage);
        }

        [TestMethod]
        public void Tick_FiresTimeElapsed_WhenTimeReachesZero()
        {
            var timeManager = new TimeManager(1);
            bool elapsedFired = false;
            timeManager.TimeElapsed += () => elapsedFired = true;

            timeManager.Tick(); 
            timeManager.Tick(); 

            Assert.IsTrue(elapsedFired, "должно сработать");
        }
    }
}