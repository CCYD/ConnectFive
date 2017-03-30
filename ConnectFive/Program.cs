using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFive
{
    class Program
    {
        public static string[,] size = new string[8, 8];

        static void Main(string[] args)
        {


            //fill array with *
            for (int i = 0; i < size.GetLength(0); i++)
            {
                for (int d = 0; d < size.GetLength(1); d++)
                {
                    size[i, d] = "*";       //star means index is empty
                }
            }
            printBoard(size);
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
            CheckForInput();
        }
        static void AddThingToTheTwoDimensionalArray(int input)
        {
            input--;
            if (variables.lastx != input)
            {
                variables.x = 7;

            }
            else
            {
                

            }



            //size[variables.x, input] = "A";
            if (size[variables.x,input] == "*" ) //if index is empty then place player piece there..
            {
                
                
                size[variables.x, input] = "A";


            }
            else if (size[variables.x, input] == "A" || size[variables.x,input] == "B")  //
            {


                size[variables.x - 1, input] = "A";
                variables.x--;


            }
            variables.lastx = input;


            printBoard(size);
        }

        static void CheckForInput()
        {
            Console.WriteLine("\nWhich column would you like to add to?");
            int input = int.Parse(Console.ReadLine());

            AddThingToTheTwoDimensionalArray(input);



        }

        static bool CheckLastX()
        {
            if (variables.x != variables.lastx)
            {

                return false;
            }

            return true;
        }
        

    }
    class variables
    {
        public static int x = 7;
        public static int lastx;
        public static int PlayerID = 0;     //0 is player 1//1 is player 2

        public static string GetPlayerID()
        {
            if (PlayerID == 0)
            {
                return "Player 1";
            }
            else if (PlayerID == 1)
            {
                return "Player 2";
            }
            return null;
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
