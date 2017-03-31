using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFive
{
    class Program
    {
        public static string[,] board = new string[8, 8];

        static void Main(string[] args)
        {
            //fill array with *
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int d = 0; d < board.GetLength(1); d++)
                {
                    board[i, d] = "*";       //star means index is empty
                }
            }
            printBoard(board);

            Console.ReadKey();
        }

        static void printBoard(string[,] size)
        {
            //this function prints the board
            Console.Clear();
            Console.WriteLine("\n          ~~Connect Five~~");
            Console.WriteLine(" =================================");    //top of the board
            for (int y = 0; y < size.GetLength(0); y++)
            {
                //Console.Write("");
                for (int x = 0; x < size.GetLength(1); x++)
                {
                    Console.Write(" | ");
                    if (size[y, x] == "*")  //check if index is empty
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        if (board[y, x] == "A")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if (board[y, x] == "B")
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        Console.Write(size[y, x]);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                Console.Write(" |\n");
                Console.WriteLine(" =================================");    //bottom of the board
            }
            Console.WriteLine(" | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 |");

            CheckForWin();
            SwitchPlayers();

            CheckForInput();


        }
        static void AddThingToTheTwoDimensionalArray(int column)
        {
            column--;      //the user enters a value of 1-8 but the for loop uses 0-7. This solves that
            for (int i = 7; i >= 0; i--)
            {
                if (board[i, column] == "*")
                {
                    board[i, column] = variables.PlayerLetter;
                    break;
                }
            }
            printBoard(board);
        }

        static void CheckForInput()
        {
            //todo: add input validation
            Console.Write("\n\nReady {0}\nWhich column would you like to add to? (1-8)\n>", variables.PlayerIDstring);
            //int input = int.Parse(Console.ReadLine());
            int input;


            while (!(int.TryParse(Console.ReadLine(), out input)))
            {
                Console.Write("Invalid input. Try again. \n>");
            }
            AddThingToTheTwoDimensionalArray(input);

        }
        static void SwitchPlayers()
        {

            if (variables.player1 == true)
            {
                variables.PlayerIDstring = "Player One";
                variables.PlayerLetter = "A";
                variables.player1 = false;
            }
            else if (variables.player1 == false)
            {
                variables.PlayerIDstring = "Player Two";
                variables.PlayerLetter = "B";
                variables.player1 = true;
            }
        }


        static void CheckForWin() //THIS IS ALL BROKEN
        {
            //this method checks horizontaly, vertically, or diagonally for 5 checkers and determines the winner
            
            int win = 0;


            //vertical THIS IS BROKEN
            for (int y = 0; y < board.GetLength(0); y++)
            {
                for (int x = 0; x < (board.GetLength(1) - 3); x++)
                {
                    if (x <= 4)
                    {
                        if (board[x, y] == variables.PlayerLetter &&
                        board[x + 1, y] == variables.PlayerLetter &&
                        board[x + 2, y] == variables.PlayerLetter &&
                        board[x + 3, y] == variables.PlayerLetter &&
                        board[x + 4, y] == variables.PlayerLetter)
                        {
                            win = 1;
                        }
                    }
                }
            }
            //horizontal
            for (int x = 0; x < board.GetLength(1); x++)
            {
                for (int y = 0; y < (board.GetLength(0) - 3); y++)
                {
                    if (y <= 4)
                    {
                        if (board[x, y] == variables.PlayerLetter &&
                        board[x, y + 1] == variables.PlayerLetter &&
                        board[x, y + 2] == variables.PlayerLetter &&
                        board[x, y + 3] == variables.PlayerLetter &&
                        board[x, y + 4] == variables.PlayerLetter)
                        {
                            win = 1;
                        }
                    }
                }
            }
            //if x becomes 7 then stop
            //diagonal

            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (x <= 4 && y <= 4)
                    {
                        if ((board[x, y] == variables.PlayerLetter &&
                            board[x + 1, y + 1] == variables.PlayerLetter &&
                            board[x + 2, y + 2] == variables.PlayerLetter &&
                            board[x + 3, y + 3] == variables.PlayerLetter &&
                            board[x + 4, y + 4] == variables.PlayerLetter)
                             ||
                            (board[x + 3, y] == variables.PlayerLetter &&
                            board[x + 2, y + 1] == variables.PlayerLetter &&
                            board[x + 1, y + 2] == variables.PlayerLetter &&
                            board[x, y + 3] == variables.PlayerLetter &&
                            board[x, y + 4] == variables.PlayerLetter))
                        {
                            win = 1;
                        }
                    }
                }
            }


            if (win == 1)
            {
                EndGame(variables.PlayerLetter);
                Console.ReadKey();
            }

        }


        static void EndGame(string winner)
        {
            Console.WriteLine("Winner is " + winner);


        }
        class variables
        {
            public static int x = 7;
            public static string PlayerIDstring;
            public static bool player1 = true;
            public static string PlayerLetter;



        }
    }
}
/*
          ~~Connect Five~~
 =================================
 |   |   |   |   |   |   |   |   |
 =================================
 |   |   |   |   |   |   |   |   |
 =================================
 |   |   |   |   |   |   |   |   |
 =================================
 |   |   |   |   |   |   |   |   |
 =================================
 |   |   |   |   |   |   |   |   |
 =================================
 |   |   |   |   |   |   |   |   |
 =================================
 |   |   |   |   |   |   |   |   |
 =================================
 |   |   |   |   |   |   |   |   |
 =================================
Which column would you like to add to?
*/
