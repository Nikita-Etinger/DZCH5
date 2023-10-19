using ConsoleApp3;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class MorseCode
    {
        private Dictionary<char, string> textToMorse = new Dictionary<char, string>
        {
            {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."}, {'F', "..-."},
            {'G', "--."}, {'H', "...."}, {'I', ".."}, {'J', ".---"}, {'K', "-.-"}, {'L', ".-.."},
            {'M', "--"}, {'N', "-."}, {'O', "---"}, {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."},
            {'S', "..."}, {'T', "-"}, {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"},
            {'Y', "-.--"}, {'Z', "--.."},
            {'1', ".----"}, {'2', "..---"}, {'3', "...--"}, {'4', "....-"}, {'5', "....."},
            {'6', "-...."}, {'7', "--..."}, {'8', "---.."}, {'9', "----."}, {'0', "-----"},
            {' ', "/"}
        };

        private Dictionary<string, char> morseToText;

        public MorseCode()
        {
            morseToText = new Dictionary<string, char>();
            foreach (var kvp in textToMorse)
            {
                morseToText[kvp.Value] = kvp.Key;
            }
        }

        public string ConvertTextToMorse(string text)
        {
            text = text.ToUpper();
            string morse = "";
            foreach (char c in text)
            {
                if (textToMorse.ContainsKey(c))
                {
                    morse += textToMorse[c] + " ";
                }
                else if (c == '\n')
                {
                    morse += "\n";
                }
                else
                {
                    morse += c;
                }
            }
            return morse.Trim();
        }

        public string ConvertMorseToText(string morseCode)
        {
            string[] words = morseCode.Split(new[] { " / " }, StringSplitOptions.None);
            string text = "";
            foreach (var word in words)
            {
                string[] letters = word.Split(' ');
                foreach (var letter in letters)
                {
                    if (morseToText.ContainsKey(letter))
                    {
                        text += morseToText[letter];
                    }
                }
                text += " ";
            }
            return text.Trim();
        }
    }

    class Program
    {
        static void ticTacToeMenu()
        {
            TicTacToe.TicTacToeGame game = new TicTacToe.TicTacToeGame();
            game.Run();
        }
        static void morseMenu()
        {
            MorseCode converter = new MorseCode();

            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Текст в азбуку Морзе");
            Console.WriteLine("2. Азбука Морзе в текст");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Console.Write("Введите текст для перевода в азбуку Морзе: ");
                string text = Console.ReadLine();
                string morse = converter.ConvertTextToMorse(text);
                Console.WriteLine("Результат: " + morse);
            }
            else if (choice == 2)
            {
                Console.Write("Введите азбуку Морзе для перевода в текст: ");
                string morse = Console.ReadLine();
                string text = converter.ConvertMorseToText(morse);
                Console.WriteLine("Результат: " + text);
            }
        }
        static void Main()
        {
            ticTacToeMenu();
            morseMenu();
        }

    }
}
namespace TicTacToe
{
    public class TicTacToeGame
    {
        private char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private int currentPlayer = 1; // 1 - игрок, 2 - компьютер
        private bool isGameOver = false;

        public void Run()
        {
            Console.WriteLine("Крестики-Нолики");

            Console.Write("Выберите режим игры (1 - против игрока, 2 - против компьютера): ");
            int gameMode = int.Parse(Console.ReadLine());

            do
            {
                Console.Clear();
                Console.WriteLine("  Крестики-Нолики\n");

                DrawBoard();

                int choice;
                if (currentPlayer == 1 || (gameMode == 1 && currentPlayer == 2))
                {
                    Console.WriteLine($"\n Игрок {currentPlayer}, выберите свободное поле: ");
                    choice = int.Parse(Console.ReadLine());
                }
                else
                {
                    choice = ComputerMove();
                }

                if (choice < 1 || choice > 9 || board[choice - 1] == 'X' || board[choice - 1] == 'O')
                {
                    Console.WriteLine(" Неверный ход! Пожалуйста, повторите.");
                    Console.ReadLine();
                }
                else
                {
                    board[choice - 1] = (currentPlayer == 1) ? 'X' : 'O';
                    currentPlayer = (currentPlayer == 1) ? 2 : 1;

                    isGameOver = CheckWin() || CheckDraw();
                }

            } while (!isGameOver);

            Console.Clear();
            DrawBoard();
            if (CheckWin())
            {
                Console.WriteLine($" Победил игрок {currentPlayer}");
            }
            else
            {
                Console.WriteLine(" Ничья!");
            }

            Console.ReadLine();
        }

        private void DrawBoard()
        {
            Console.WriteLine($" {board[0]} | {board[1]} | {board[2]} ");
            Console.WriteLine("---|---|---");
            Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
            Console.WriteLine("---|---|---");
            Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} ");
        }

        private bool CheckWin()
        {
            for (int i = 0; i < 8; i++)
            {
                string line = "";
                switch (i)
                {
                    case 0:
                        line = "" + board[0] + board[1] + board[2];
                        break;
                    case 1:
                        line = "" + board[3] + board[4] + board[5];
                        break;
                    case 2:
                        line = "" + board[6] + board[7] + board[8];
                        break;
                    case 3:
                        line = "" + board[0] + board[3] + board[6];
                        break;
                    case 4:
                        line = "" + board[1] + board[4] + board[7];
                        break;
                    case 5:
                        line = "" + board[2] + board[5] + board[8];
                        break;
                    case 6:
                        line = "" + board[0] + board[4] + board[8];
                        break;
                    case 7:
                        line = "" + board[2] + board[4] + board[6];
                        break;
                }

                if (line == "XXX" || line == "OOO")
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckDraw()
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i] != 'X' && board[i] != 'O')
                {
                    return false;
                }
            }
            return true;
        }

        private int ComputerMove()
        {
            Random random = new Random();
            int move;
            do
            {
                move = random.Next(1, 10);
            } while (board[move - 1] == 'X' || board[move - 1] == 'O');

            return move;
        }

    }
    
}


