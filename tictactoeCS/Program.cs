using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoeCS
{
    class Program
    {
        static int Counter = 0;
        static bool GameActive;
        static char[,] Board;

        static void Main(string[] args)
        {
            Start();

        }

        // Sets game active.
        static void SetGameActive(bool input)
        {
            GameActive = input;
        }

        /**
         *  Resets the board and also works as
         *  a constructor for the game.
         */
        static void ResetBoard()
        {
            Counter = 0;
            SetGameActive(true);
            Board = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Board[i, j] = ' ';
                }
            }
            Console.WriteLine("\n");
        }

        // Updates and draws the board.
        static void UpdateBoard()
        {
            Console.Clear();
            Console.WriteLine();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("{0}", Board[i, j]);
                    if (j == 0 || j == 1)
                    {
                        Console.Write("  | ");
                    }
                }
                if (i != 2)
                {
                    Console.WriteLine("\n-------------");
                }
            }
            Console.WriteLine("\n");
        }

        // Makes move
        static void MakeMove(char player, int row, int col)
        {
            Board[row - 1, col - 1] = player;
        }

        /**
         *  Checks wheter any box is empty or not
         *  and returns a boolean value.
         */
        static bool IsEmpty(int row, int col)
        {
            if (Board[row - 1, col - 1] == ' ')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         *  Checks whether any move is valid or not
         *  and returns a boolean value.
         */
        static bool NotValid(int row, int col)
        {
            if (row > 3 || row < 1 || col < 1 || col > 3 || !IsEmpty(row, col))
            {
                Console.WriteLine("\nThis place is not empty. Try again...");
                return true;
            }
            return false;
        }

        //  Asks the player to make a move.
        static void AskPlayer(char player)
        {
            int row, col;
            do
            {
                again1:
                Console.Write("Player {0}, enter a row: ", player);
                String _row = Console.ReadLine();
                if (!Int32.TryParse(_row, out row))
                {
                    Console.WriteLine("Invalid input; try again...");
                    goto again1;
                }
                Console.WriteLine();
                Console.Write("Player {0}, enter a column: ", player);
                String _col = Console.ReadLine();
                if (!Int32.TryParse(_col, out col))
                {
                    Console.WriteLine("\nIvalid input; try again...");
                    goto again1;
                }
            }
            while (NotValid(row, col));
            MakeMove(player, row, col);
        }

        /**
         *  Checks if the game is over or not
         *  and sets the value of the variable
         *  accordingly.
         */
        static void CheckForWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Board[i, 0] == Board[i, 1] && Board[i, 0] == Board[i, 2] && Board[i, 0] != ' ' && GameActive)
                {
                    Console.WriteLine($"\nThe winner is palyer {Board[i, 0]}");
                    SetGameActive(false);
                }
                if (Board[0, i] == Board[1, i] && Board[0, i] == Board[2, i] && Board[0, i] != ' ' && GameActive)
                {
                    Console.WriteLine($"\nThe winner is player {Board[0, i]}");
                    SetGameActive(false);
                }
            }
            if (Board[0, 0] == Board[1, 1] && Board[0, 0] == Board[2, 2] && Board[0, 0] != ' ' && GameActive)
            {
                Console.WriteLine($"\nThe winner is palyer {Board[0, 0]}");
                SetGameActive(false);
            }
            if (Board[0, 2] == Board[1, 1] && Board[0, 2] == Board[2, 0] && Board[0, 2] != ' ' && GameActive)
            {
                Console.WriteLine($"\nThe winner is player {Board[0, 2]}");
                SetGameActive(false);
            }
        }

        //  This is where the game starts for the two human players.
        static void TwoPlayers()
        {
            ResetBoard();
            UpdateBoard();
            while (GameActive)
            {
                if (Counter % 2 == 0)
                {
                    AskPlayer('X');
                }
                else
                {
                    AskPlayer('O');
                }
                UpdateBoard();
                Counter++;
                CheckForWinner();
                if (Counter == 9 && GameActive)
                {
                    SetGameActive(false);
                }
            }
        }

        static void Start()
        {
            String d;
            TwoPlayers();
            _D:
            if (!GameActive)
            {
                Console.Write("Press C to play again or Q to quit: ");
                d = Console.ReadLine();
                if (d.Equals("Q") || d.Equals("q"))
                {
                    Environment.Exit(0);
                }
                else if (d.Equals("C") || d.Equals("c"))
                {
                    TwoPlayers();
                }
                else
                {
                    Console.WriteLine("\nPlease enter the correct input.");
                    goto _D;
                }
            }
        }
    }
}
