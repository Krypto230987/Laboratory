using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class GuessWordGame
{
    private List<string> words;
    private string selectedWord;
    private string guessedLetters;
    private int maxTries;
    private int remainingTries;
    private string playedWordsFilePath = "C:\\Users\\User\\Desktop\\infa.txt";

    public GuessWordGame(int difficultyLevel)
    {
        words = LoadWordsFromFile("words.txt");
        selectedWord = GetNewWord();
        guessedLetters = new string('*', selectedWord.Length);
        maxTries = 5;
        remainingTries = maxTries;

        
        int numLettersToOpen = 0;
        switch (difficultyLevel)
        {
            case 1:
                numLettersToOpen = 3;
                break;
            case 2:
                numLettersToOpen = 2;
                break;
            case 3:
                numLettersToOpen = 1;
                break;
            default:
                numLettersToOpen = 0;
                break;
        }

        OpenRandomLetters(numLettersToOpen);
    }

    public void Start()
    {
        Console.WriteLine("\r\nДобро пожаловать в игру «Угадай слово»!");

        while (remainingTries > 0)
        {
            Console.WriteLine("\nТекущее слово: " + guessedLetters);
            Console.WriteLine("\r\nОсталось попыток: " + remainingTries);
            Console.Write("\r\nВведите букву или угадайте слово целиком (слово написано на английском): ");
            string input = Console.ReadLine().Trim().ToLower();

            if (input.Length == 1 && char.IsLetter(input[0]))
            {
                char letter = input[0];
                ProcessLetterGuess(letter);
            }
            else if (input.Length > 1)
            {
                ProcessWordGuess(input);
            }
            else
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, введите букву или слово.");
            }

            if (guessedLetters == selectedWord)
            {
                Console.WriteLine("\nТы победил! Было слово \"" + selectedWord + "\". \r\nВы выиграли c " + (maxTries - remainingTries + 1) + " попытки.");
                Console.ReadLine();
                return;
            }
        }

        Console.WriteLine("\nИгра закончена! Слово было \"" + selectedWord + "\".\n" +
     "|----------|\n" +
     "|          |\n" +
     "|          O\n" +
     "|         /|\\\n" +
     "|         / \\\n" +
     "|" +
     "|\\\n_______");

        Console.ReadLine();
    }

    private void ProcessLetterGuess(char letter)
    {
        if (selectedWord.Contains(letter))
        {
            for (int i = 0; i < selectedWord.Length; i++)
            {
                if (selectedWord[i] == letter)
                {
                    guessedLetters = guessedLetters.Substring(0, i) + letter + guessedLetters.Substring(i + 1);
                }
            }
        }
        else
        {
            Console.WriteLine("Нет, такой буквы в этом слове нет.");
            remainingTries--;
        }
    }

    private void ProcessWordGuess(string word)
    {
        if (word == selectedWord)
        {
            guessedLetters = selectedWord;
        }
        else
        {
            Console.WriteLine("Нет, это слово не \"" + word + "\".");
            remainingTries--;
        }
    }

    private List<string> LoadWordsFromFile(string filename)
    {
        List<string> words = new List<string>();

        if (File.Exists(filename))
        {
            words = File.ReadAllLines(filename).ToList();
        }

        return words;
    }

    private string GetNewWord()
    {
        List<string> playedWords = LoadPlayedWords();
        List<string> availableWords = words.Except(playedWords).ToList();
        if (availableWords.Count == 0)
        {
            SavePlayedWords(new List<string>());
            availableWords = words;
        }

        Random random = new Random();
        int index = random.Next(availableWords.Count);
        string word = availableWords[index];
        availableWords.RemoveAt(index);
        SavePlayedWords(playedWords.Concat(new List<string> { word }).ToList());
        return word;
    }

    private List<string> LoadPlayedWords()
    {
        List<string> playedWords = new List<string>();

        if (File.Exists(playedWordsFilePath))
        {
            playedWords = File.ReadAllLines(playedWordsFilePath).ToList();
        }

        return playedWords;
    }

    private void SavePlayedWords(List<string> playedWords)
    {
        File.WriteAllLines(playedWordsFilePath, playedWords);
    }

    private void OpenRandomLetters(int numLetters)
    {
        if (numLetters >= selectedWord.Length)
        {
            guessedLetters = selectedWord;
        }
        else
        {
            Random random = new Random();
            List<int> indices = Enumerable.Range(0, selectedWord.Length).ToList();
            indices = indices.OrderBy(i => random.Next()).ToList();

            for (int i = 0; i < numLetters; i++)
            {
                int index = indices[i];
                guessedLetters = guessedLetters.Substring(0, index) + selectedWord[index] + guessedLetters.Substring(index + 1);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Выберите уровень сложности:");
        Console.WriteLine("1 - Легкий");
        Console.WriteLine("2 - Средний");
        Console.WriteLine("3 - Трудный");
        Console.WriteLine("4 - Очень трудный");
        Console.Write("Введите номер уровня сложности: ");
        int difficultyLevel = int.Parse(Console.ReadLine());

        GuessWordGame game = new GuessWordGame(difficultyLevel);
        game.Start();
    }
}






