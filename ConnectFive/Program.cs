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
            //SwitchPlayers();

            //fill array with *
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int d = 0; d < board.GetLength(1); d++)
                {
                    board[i, d] = "*";       //star means index is empty
                }
            }
            printBoard(board);
            //string choice = Console.ReadLine();
            


            Console.ReadKey();
        }



        static void printBoard(string[,] size)
        {
            Console.Clear();
            Console.WriteLine("\n          ~~Connect Five~~");
            Console.WriteLine(" =================================");
            for (int y = 0; y < size.GetLength(0); y++)
            {
                //Console.Write("");
                for (int x = 0; x < size.GetLength(1); x++)
                {
                    Console.Write(" | ");                   

                    if (size[y,x] == "*")
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(size[y,x]);
                    }                  
                }
                Console.Write(" |\n");
                Console.WriteLine(" =================================");
                
            }
            SwitchPlayers();
            CheckForInput();
        }
        static void AddThingToTheTwoDimensionalArray(int input)
        {
            input--;
            
            if (variables.lastx != input)       
            {
                variables.x = 7;
            }


            //size[variables.x, input] = "A";
            if (board[variables.x,input] == "*" ) //if index is empty then place player piece there..
            {
                
                
                board[variables.x, input] = variables.PlayerChip;
                variables.x--;

            }
            else if (board[variables.x, input] == "A" || board[variables.x,input] == "B")  //
            {


                board[variables.x - 1, input] = variables.PlayerChip;
                variables.x--;


            }
            variables.lastx = input;


            printBoard(board);
        }


        static void CheckForInput()
        {
            //todo: add input validation
            Console.WriteLine("\nReady {0}\nWhich column would you like to add to?", variables.PlayerIDstring);
            int input = int.Parse(Console.ReadLine());

            AddThingToTheTwoDimensionalArray(input);

        }
        static void SwitchPlayers()
        {
            
            if (variables.player1 == true)
            {
                variables.PlayerID = 0;
                variables.PlayerIDstring = "Player One";
                variables.PlayerChip = "A";
                variables.player1 = false;
            }
            else if (variables.player1 == false)
            {
                variables.PlayerID = 1;
                variables.PlayerIDstring = "Player Two";
                variables.PlayerChip = "B";
                variables.player1 = true;
            }


        }

        

    }
    class variables
    {
        public static int x = 7;
        public static int lastx;
        public static int PlayerID;
        public static string PlayerIDstring;     
        public static bool player1 = true;
        public static string PlayerChip;
        


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
