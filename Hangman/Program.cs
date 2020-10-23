using System;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {

            var input = "";
            var isRunning = true;
            var gameOver = false;
            var strikeCounter = 0;
            var newWindow = true;

            while (isRunning)
            {
                if (newWindow)
                {
                    Console.WriteLine(" -------------------------------\n" +
                                      "|    Willkommen bei Hangman     |\n" +
                                      " -------------------------------");
                }
                newWindow = false;
                Console.WriteLine("\n     ----------------------\n" +
                                  "    |  Was willst du tun?  |\n" +
                                  "    |                      |\n" +
                                  "    |     (s) Spielen      |\n" +
                                  "    |     (b) Beenden      |\n" +
                                  "     ----------------------\n");
                input = Console.ReadLine();
                strikeCounter = 0;
                switch (input)
                {
                    case "s":
                        Word myWord = new Word();
                        //Console.WriteLine(myWord.word);
                        var newGame = true;
                        gameOver = false;

                        while (!gameOver)
                        {
                            Game(myWord, newGame);
                            var letter = TakeGuess();
                            if (!CheckIfLetterAlreadySolved(myWord, letter))
                            {
                                strikeCounter = TellResult(CheckLetter(myWord, letter), strikeCounter);
                                newGame = false;
                                gameOver = CheckIfWordSolved(myWord);
                                if (strikeCounter.Equals(5)) { gameOver = true; }
                            }

                        }
                        CheckGameResult(myWord, strikeCounter);
                        break;

                    case "b":
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Unbekannte Eingabe. Bitte wähle zwischen s und b.");
                        break;
                }
            }
        }

        public static void Game(Word word, bool newGame)
        {
            switch (newGame)
            {
                case true:
                    var wordLength = word.word.Length;
                    Console.WriteLine($"\nDas gesuchte Wort hat {wordLength} Buchstaben: {word.visibleWord}\n\n");
                    break;
                case false:
                    if (CountRemainingLetters(word).Equals(1))
                    {
                        Console.WriteLine($"\nEs fehlt nur noch {CountRemainingLetters(word)} Buchstabe: {word.visibleWord}\n\n");
                    }
                    Console.WriteLine($"\nEs fehlen noch {CountRemainingLetters(word)} Buchstaben: {word.visibleWord}\n\n");
                    break;
            }
        }

        public static String TakeGuess()
        {
            var input = "";
            var inputOk = false;
            while (!inputOk)
            {
                input = Console.ReadLine();
                if (input.Length == 1)
                {
                    inputOk = true;
                }
                else
                {
                    Console.WriteLine("Bitte tippe nur einen Buchstaben ein.");
                }
            }
            return input;
        }

        public static bool CheckLetter(Word word, String input)
        {
            var letterCorrect = false;
            char letter = Convert.ToChar(input.ToUpper());
            char[] wordChar = word.word.ToCharArray();
            char[] visibleWordChar = word.visibleWord.ToCharArray();
            var counter = 0;

            foreach (var element in wordChar)
            {
                if (element.Equals(letter))
                {
                    visibleWordChar[counter] = letter;
                    letterCorrect = true;
                }
                counter++;
            }

            word.visibleWord = new string(visibleWordChar);
            return letterCorrect;
        }

        public static bool CheckIfLetterAlreadySolved(Word word, String input)
        {
            var letterAlreadySolved = false;
            char letter = Convert.ToChar(input.ToUpper());
            char[] visibleWordChar = word.visibleWord.ToCharArray();

            foreach (var element in visibleWordChar)
            {
                if (element.Equals(letter))
                {
                    Console.WriteLine("Diesen Buchstaben hast du bereits gelöst.\n");
                    letterAlreadySolved = true;
                    break;
                }
            }
            return letterAlreadySolved;
        }

        public static int TellResult(bool letterCorrect, int strikeCounter)
        {
            switch (letterCorrect)
            {
                case true:
                    Console.WriteLine("Richtig, dieser Buchstabe ist im Wort enthalten.\n");
                    break;
                case false:
                    Console.WriteLine("Dieser Buchstabe ist leider nicht im Wort enthalten");
                    strikeCounter++;
                    Console.WriteLine($"Du hast noch {5 - strikeCounter} Fehlversuche.\n");
                    break;
            }
            return strikeCounter;
        }

        public static int CountRemainingLetters(Word word)
        {
            var remainingLetters = 0;
            char[] letters = word.visibleWord.ToCharArray();
            foreach (var element in letters)
            {
                if (element.Equals('.'))
                {
                    remainingLetters++;
                }
            }
            return remainingLetters;
        }

        public static bool CheckIfWordSolved(Word word)
        {
            var isSolved = false;
            if (CountRemainingLetters(word).Equals(0))
            {
                isSolved = true;
            }
            else { isSolved = false; }
            return isSolved;
        }

        public static void CheckGameResult(Word word, int strikeCounter)
        {
            if (strikeCounter.Equals(5))
            {
                Console.WriteLine($" ----------------------------\n" +
                                  $"|  Du hast leider verloren.  |\n" +
                                  $"|   Das gesuchte Wort war    |\n" +
                                  $"          {word.word}        \n" +
                                  $" ----------------------------");
            }
            else
            {
                Console.WriteLine(" --------------------------------\n" +
                                  "| Gratulation. Du hast gewonnen! |\n" +
                                 $"|     Das gesuchte Wort war      |\n" +
                                 $"         {word.word}              \n" +
                                 $" --------------------------------");
            }
        }
    }
}
