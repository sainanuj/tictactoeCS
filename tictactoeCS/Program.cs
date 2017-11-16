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
        static bool GameActive, Win;
        static char[,] Board;
        static char hp;
        
        static void Main(string[] args)
        {
            ResetBoard();
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
            Win = false;
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
            Console.Clear();    // Clears the console.
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
                Console.Write("\nPlayer {0}, enter a column: ", player);
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

        static bool _cfw(char player)
        {
            for (int i = 0; i < 3; i++)
            {
                if (Board[i, 0] == Board[i, 1] && Board[i, 0] == Board[i, 2] && Board[i, 0] == player && GameActive)
                {
                    Win = true;
                }
                if (Board[0, i] == Board[1, i] && Board[0, i] == Board[2, i] && Board[0, i] == player && GameActive)
                {
                    Win = true;
                }
            }
            if (Board[0, 0] == Board[1, 1] && Board[0, 0] == Board[2, 2] && Board[0, 0] == player && GameActive)
            {
                Win = true;
            }
            if (Board[0, 2] == Board[1, 1] && Board[0, 2] == Board[2, 0] && Board[0, 2] == player && GameActive)
            {
                Win = true;
            }
            return Win;
        }
        
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
                    Console.WriteLine("\n\tThe match is a tie.");
                    SetGameActive(false);
                }
            }
        }

        static void RandomMove(char player)
        {
            Random r = new Random();
            int row, col;
            row = r.Next(0, 3);
            col = r.Next(0, 3);
            if (IsEmpty(row, col))
            {
                Board[row, col] = player;
            }
            else
            {
                RandomMove(player);
            }
        }

        /** 
         * The idea is to check every box in the grid whether it's empty or not.
         * If a box is empty, the computer will check whether the human player 
         * would win if he/she makes a move at that position. Here two cases are 
         * possible -
         *  
         *  Case #1: The computer sees that the human player may win at his/her next 
         *  move and it'll take countermeasure to thwart it by making a move at that
         *  position.
         *  
         *  Case #2: The computer sees that there is no imminent chance for the human
         *  player to win the match. Then, it'll try to seek a position where making 
         *  a move may end the game. If it fails it'll make a random valid move. 
         */
        static void _ai(char player, char computer)
        {
            // Case #1
            for (int i = 0; i < 3; i++)         
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] == ' ')
                    {
                        Board[i, j] = player;
                        if (_cfw(player))
                        {
                            Board[i, j] = computer;
                            break;
                        }
                        else
                        {
                            Board[i, j] = ' ';
                        }
                    }
                }
                if (Win)
                {
                    break;
                }
            }

            // Case #2
            for (int i = 0; i < 3; i++)         
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] == ' ')
                    {
                        Board[i, j] = computer;
                        if (_cfw(computer))
                        {
                            break;
                        }
                        else
                        {
                            Board[i, j] = ' ';
                        }
                    }
                }
                if (Win)
                {
                    break;
                }
            }
            if (!Win)
            {
                RandomMove(computer);
            }
        }

        static void OnePlayer()
        {
            ResetBoard();
            UpdateBoard();
            Character();
            if (hp.Equals('X'))
            {
                while (GameActive)
                {
                    if (Counter % 2 == 0)
                    {
                        AskPlayer('X');
                    }
                    else
                    {
                        // Computer will make move as player O.
                        _ai('X', 'O');
                    }
                    UpdateBoard();
                    Counter++;
                    CheckForWinner();
                    if (Counter == 9 && GameActive)
                    {
                        Console.WriteLine("\n\tThe match is a tie.");
                        SetGameActive(false);
                    }
                }
            }
            else
            {
                while (GameActive)
                {
                    if (Counter % 2 == 0)
                    {
                        // Computer will make move as player X.
                        _ai('O', 'X');
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
                        Console.WriteLine("\n\tThe match is a tie.");
                        SetGameActive(false);
                    }
                }
            }
        }

        static void Character()
        {
            if (OneTwo() == 1)
            {
                hp = 'X';
            }
            else
            {
                hp = 'O';
            }
        }

        static void Start()
        {
            String d;
            Console.Write("\n\tStarting game... Good luck!\n" +
                "\nPress 1 to play with the computer otherwise press 2: ");
            switch (OneTwo())
            {
                case 1:
                    OnePlayer();
                    break;
                case 2:
                    TwoPlayers();
                    break;
            }
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
                    Start();
                }
                else
                {
                    Console.WriteLine("\nPlease enter the correct input.");
                    goto _D;
                }
            }
        }

        static int OneTwo()
        {
            string s = Console.ReadLine();
            if (s.Equals("1"))
            {
                return 1;
            }
            else if (s.Equals("2"))
            {
                return 2;
            } else
            {
                Console.WriteLine("\n\tPlease enter the correct input.");
                return OneTwo();
            }
        }
    }
}
