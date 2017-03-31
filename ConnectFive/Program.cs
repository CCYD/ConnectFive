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
                        Console.Write(size[y, x]);
                    }
                }
                Console.Write(" |\n");
                Console.WriteLine(" =================================");    //bottom of the board

            }
            SwitchPlayers();
            CheckForInput();
        }
        static void AddThingToTheTwoDimensionalArray(int column)
        {
            column--;

            for (int i = 7; i >= 0; i--)
            {
                if (board[i, column] == "*" )
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
            int input = int.Parse(Console.ReadLine());

            AddThingToTheTwoDimensionalArray(input);

        }
        static void SwitchPlayers()
        {

            if (variables.player1 == true)
            {
                variables.PlayerID = 0;
                variables.PlayerIDstring = "Player One";
                variables.PlayerLetter = "A";
                variables.player1 = false;
            }
            else if (variables.player1 == false)
            {
                variables.PlayerID = 1;
                variables.PlayerIDstring = "Player Two";
                variables.PlayerLetter = "B";
                variables.player1 = true;
            }


        }

        static void CheckForWin()
        {


        }

    }
    class variables
    {
        public static int x = 7;
        public static int PlayerID;
        public static string PlayerIDstring;
        public static bool player1 = true;
        public static string PlayerLetter;



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
