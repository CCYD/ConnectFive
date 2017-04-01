using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//Yuri Doubas
//Unit 2 Assignment 3
namespace ConnectFive
{
    class Program
    {
        //create 2d array which will hold the board 
        public static string[,] board = new string[8, 8];

        static void Main(string[] args)
        {
            ClearBoard();
            Menu();  //call Menu() method
            Console.ReadKey();
        }

        static void Menu()
        {
            //This method presents the user with 3 choices 
            Console.Clear();
            Console.Write("What u want to do?\n1. PvP\n2. Load File\n3. PvAI (Not Implemented)\n>");
            int input;

            while (!(int.TryParse(Console.ReadLine(), out input)))  //wait for user to enter correct input
            {
                Console.Write("Invalid input. Try again. \n>");
            }
            if (input == 1)
            {                
                printBoard(board);
            }
            else if (input == 2)
            {
                ReadFromFile();
            }
            else if (input == 3)
            {

            }
            Menu(); //loop back to Menu()

        }

        static void printBoard(string[,] size)
        {
            //this function prints the board and all player pieces on it
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
                        Console.Write(" "); //print an empty space where there are no pieces
                    }
                    else
                    {
                        if (board[y, x] == "A") //color all player one pieces red
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if (board[y, x] == "B") //color all player two pieces blue    
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        Console.Write(size[y, x]);  //print a player piece
                        Console.ForegroundColor = ConsoleColor.Gray; //reset text color
                    }
                }
                Console.Write(" |\n");
                Console.WriteLine(" =================================");    //bottom of the board
            }
            Console.WriteLine(" | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 |");

            CheckForWin(false);  //call CheckForWin() to check if 5 pieces are in a row
            SwitchPlayers(); //call SwitchPlayers() to switch players

            CheckForInput(); //call CheckForInput() to check user input


        }
        static void DropPlayerPiece(int column, bool ReadFromFile)
        {
            //this method drops a piece in the user selected column
            column--;      //the user enters a value of 1-8 but the for loop uses 0-7. This solves that
            for (int i = 7; i >= 0; i--)    //this places the player letter in the bottommost index
            {
                if (board[i, column] == "*")
                {
                    board[i, column] = variables.PlayerLetter;
                    break;
                }
            }
            if (ReadFromFile == false)
            {
                printBoard(board);  //call the printBoard() method to print the board
            }

        }

        static void CheckForInput()
        {
            //this method evaluates user input
            Console.Write("\n\nReady ");
            if (variables.player1 == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (variables.player1 == true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            Console.Write(variables.PlayerIDstring);
            Console.ForegroundColor = ConsoleColor.Gray;
            
            Console.WriteLine("\nWhich column would you like to add to? (1-8)\n>");

            int input;
            while (!(int.TryParse(Console.ReadLine(), out input)))  //ask user for input 
            {
                Console.Write("Invalid input. Try again. \n>");
            }
            
            if (input >= 1 && input <= 8)   //check if user entered a number between 1 and 8
            {
                DropPlayerPiece(input, false);    //call DropPlayerPiece with input parameter
            }
            else
            {
                CheckForInput();        //recursion?
            }
        }
        static void SwitchPlayers()
        {
            //this method switches the active player
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
        static void CheckForWin(bool ReadFromFile)
        {
            //this method checks horizontaly, vertically, or diagonally for 5 checkers and determines the winner
          
            int win = 0;


            //vertical 
            for (int y = 0; y < board.GetLength(0); y++)
            {
                for (int x = 0; x < (board.GetLength(1) - 3); x++)
                {
                    if (x < 4)
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
                    if (y < 4)
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
            //diagonal
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (x < 4 && y < 4)
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

            if (win == 1 && ReadFromFile == true)
            {
                Console.WriteLine(variables.PlayerLetter + " won the game!");
            }
            else if (win == 1)
            {
                EndGame(variables.PlayerIDstring, false);
                Console.ReadKey();
            }
            else if (win == 0 & ReadFromFile == true)
            {
                Console.WriteLine("No one wins");
            }
            //else if (win == 0 & CheckForTie() == true)
            {
              //  Console.WriteLine("Board is full. Tie");
            }



        }

        static bool CheckForTie()
        {
            int counter = 0;
            for (int i = 0; i < board.GetLength(0); i++)    //for each row
            {
                for (int d = 0; d < board.GetLength(1); d++) //for each column
                {
                    board[i, d] = "*";
                    if (board[i,d] == "A" || board[i,d] == "B")
                    {
                        counter++;

                    }
                    if (counter == board.GetLength(0) * board.GetLength(1))
                    {
                        return true;
                    }
                }
            }
            return false;

        }


        static void EndGame(string winner , bool tie)
        {
            //This method displays the winner and resets the board
            Console.WriteLine("\n" + winner + " Won the game!\n Press any key to return to menu.");
            Console.ReadKey();

            ClearBoard();

            Menu();
        }
        static void ClearBoard()
        {
            //fill array with asteriks
            for (int i = 0; i < board.GetLength(0); i++)    //for each row
            {
                for (int d = 0; d < board.GetLength(1); d++) //for each column
                {
                    board[i, d] = "*";
                }
            }
        }
        static void ReadFromFile()
        {
            string[] line = File.ReadAllLines("Input.txt");



            int roundcounter = 0;
            for (int l = 0; l < line.Length; l++)
            {
                //thanks patrick
                string LineLength = line[l];
                int WhiteSpaceCounter = 0;
                int characterRemovalCounter = 0;
                for (int i = 0; i < LineLength.Length; i++)
                {
                    if (LineLength[i] == ' ')
                    {
                        WhiteSpaceCounter++;
                    }
                    if (i != 0)
                    {
                        if ((LineLength[i] != ' ') && (LineLength[i - 1] != ' '))
                        {
                            characterRemovalCounter++;
                        }
                    }
                }
                int AmountOfMoves = LineLength.Length - WhiteSpaceCounter - characterRemovalCounter;
                int[] Moves = new int[AmountOfMoves];
                roundcounter++;
                Console.WriteLine("Round: " + roundcounter + "/" + line.Length);
                for (int i = 0; i < Moves.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        variables.PlayerLetter = "A";
                    }
                    else
                    {
                        variables.PlayerLetter = "B";
                    } 

                    Moves[i] = int.Parse(LineLength.Split(' ')[i]);
                    Console.WriteLine(Moves[i]);
                    DropPlayerPiece(Moves[i], true);
                    //CheckForWin();

                }
                CheckForWin(true);
                //

                ClearBoard();
                Console.WriteLine("\n");
                Console.ReadKey();
            }
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
*/
