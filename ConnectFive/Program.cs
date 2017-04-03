using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
//Yuri Doubas
//Unit 2 Assignment 3
//This program is a game played by two players that alternate placing checkers on an 8x8 board. The first player to place 5 checkers in a row horizontally, vertically,
//or diagonally wins the game.
//ye be warned for spaghetti lies ahead.
//good luck lol
namespace ConnectFive
{
    public class Program
    {
        //create 2d array of size 8x8 which will hold the board and checkers
        public static string[,] board = new string[8, 8];

        static void Main(string[] args)
        {
            ClearBoard(); //call ClearBoard(); to fill the array with asteriks
            Menu();  //call Menu() method            
        }

        static void Menu()
        {
            //This method presents the user with 3 choices 
            Console.Clear();
            Console.Write("What would you like to do?\n1. PvP\n2. Load File\n3. PvAI (Not implemented yet..)\n>");
            int input;

            while (!(int.TryParse(Console.ReadLine(), out input)))  //wait for user to enter correct input
            {
                Console.Write("Invalid input. Try again. \n>");
            }
            if (input == 1) //option 1
            {
                printBoard(board);
            }
            else if (input == 2)  //option 2
            {
                ReadFromFile();
            }
            else if (input == 3) //option 3
            {
                Console.WriteLine("It says not implemented..");
                Console.ReadKey();
            }
            Menu(); //loop back to Menu()....recursion?

        }

        static void printBoard(string[,] size)
        {
            //this function prints the board and all player pieces on it
            Console.Clear();
            Console.WriteLine("\n          ~~Connect Five~~");
            Console.WriteLine(" =================================");    //top of the board
            for (int y = 0; y < size.GetLength(0); y++)     //print each row
            {
                for (int x = 0; x < size.GetLength(1); x++) //print each column
                {
                    Console.Write(" | ");
                    if (size[y, x] == "*")  //check if index is empty
                    {
                        Console.Write(" "); //print an empty space where there are no pieces
                    }
                    else //if the index is not an askterix then print colored checkers
                    {
                        if (board[y, x] == "A") //set text color to red
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if (board[y, x] == "B") //set text color to blue    
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        Console.Write(size[y, x]);  //print a colored checker
                        Console.ForegroundColor = ConsoleColor.Gray; //reset text color to gray
                    }
                }
                Console.Write(" |\n");
                Console.WriteLine(" =================================");    //bottom of the board
            }
            Console.WriteLine(" | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 |");        //something to help users see which column is which

            CheckForWin(false);  //call CheckForWin() to check if 5 pieces are in a row
            SwitchPlayers(); //call SwitchPlayers() to switch players
            CheckForInput(); //call CheckForInput() to check user input
        }
        static void DropChecker(int column, bool ReadFromFile)
        {
            //this method drops a piece in the user selected column
            column--;      //the user enters a value of 1-8 but the for loop uses 0-7. This solves that
            for (int i = 7; i >= 0; i--)    //this places the player letter in the bottom most index
            {
                if (board[i, column] == "*")    //check if index is empty 
                {
                    board[i, column] = Player.PlayerLetter; //drop a checker at the index
                    break;
                }
            }                 
            if (ReadFromFile == false)  //if the program is reading from a file then dont show the board
            {
                printBoard(board);  //call the printBoard() method to print the board
            }
        }

        static void CheckForInput()
        {
            //this method evaluates user input
            //this section prints a message identifing the active player
            Console.Write("\n\nReady ");
            if (Player.Player1 == false)    //check if it is player two's turn
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (Player.Player1 == true) //check if it is player one's turn
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            Console.Write(Player.PlayerIDstring);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\nWhich column would you like to add to? (1-8)\n>");

            //one big mess lol
            int input;
            while (!(int.TryParse(Console.ReadLine(), out input)))  //loop until user entered a proper input
            {
                Console.Write("Invalid input. Try again. \n>");
            }
         
            if (input >= 1 && input <= 8)   //check if user entered a number between 1 and 8
            {
                if (board[0, input - 1] == "A" || board[0, input - 1] == "B")   //check if top of board is full
                {
                    Console.WriteLine("Column is full");                      
                    CheckForInput();    //recursion?
                }
                else
                {
                    DropChecker(input, false);  //call DropChecker() method to drop a checker at the inputted column
                }
            }
            else
            {
                CheckForInput();        //more recursion?
            }
        }
        static void SwitchPlayers()
        {
            //this method switches the active player
            if (Player.Player1 == true)
            {
                Player.PlayerIDstring = "Player One";
                Player.PlayerLetter = "A";
                Player.Player1 = false;
            }
            else if (Player.Player1 == false)
            {
                Player.PlayerIDstring = "Player Two";
                Player.PlayerLetter = "B";
                Player.Player1 = true;
            }
        }
        static void CheckForWin(bool ReadFromFile)
        {
            //this method checks horizontaly, vertically, or diagonally for 5 checkers and determines the winner
            //the check starts from the top and goes down

            int win = 0;

            //Check Vertical 
            for (int y = 0; y < board.GetLength(0); y++)
            {
                for (int x = 0; x < (board.GetLength(1) - 3); x++)
                {
                    if (x < 4)
                    {
                        if (board[x, y] == Player.PlayerLetter &&
                        board[x + 1, y] == Player.PlayerLetter &&
                        board[x + 2, y] == Player.PlayerLetter &&
                        board[x + 3, y] == Player.PlayerLetter &&
                        board[x + 4, y] == Player.PlayerLetter)
                        {
                            win = 1;
                        }
                    }
                }
            }
            //Check Horizontal
            for (int x = 0; x < board.GetLength(1); x++)
            {
                for (int y = 0; y < (board.GetLength(0) - 3); y++)
                {
                    if (y < 4)
                    {
                        if (board[x, y] == Player.PlayerLetter &&
                        board[x, y + 1] == Player.PlayerLetter &&
                        board[x, y + 2] == Player.PlayerLetter &&
                        board[x, y + 3] == Player.PlayerLetter &&
                        board[x, y + 4] == Player.PlayerLetter)
                        {
                            win = 1;
                        }
                    }
                }
            }
            //Check Diagonal
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (x < 4 && y < 4) //check if the top most checker is at (4,4) to aviod errors
                    {
                        //Back slash
                        if (board[x, y] == Player.PlayerLetter &&
                            board[x + 1, y + 1] == Player.PlayerLetter &&
                            board[x + 2, y + 2] == Player.PlayerLetter &&
                            board[x + 3, y + 3] == Player.PlayerLetter &&
                            board[x + 4, y + 4] == Player.PlayerLetter)
                        {
                            win = 1;
                        }
                    }
                    if (x <= 3 && y >= 4) //check if the top most checker is at (3,4) to avoid errors
                    {
                        //Forward slash
                        if (board[x, y] == Player.PlayerLetter &&
                            board[x + 1, y - 1] == Player.PlayerLetter &&
                            board[x + 2, y - 2] == Player.PlayerLetter &&
                            board[x + 3, y - 3] == Player.PlayerLetter &&
                            board[x + 4, y - 4] == Player.PlayerLetter)
                        {
                            win = 1;
                        }
                    }
                }
            }
            //check if board is full
            int counter = 0;
            for (int i = 0; i < board.GetLength(0); i++)    
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i,j] == "A" || board[i,j] == "B")
                    {
                        counter++;
                        if (counter == board.GetLength(0) * board.GetLength(1))
                        {
                            EndGame("Board is full. No one");
                        }
                    }
                }
            }
            if (win == 1 && ReadFromFile == true) 
            {
                Console.WriteLine(Player.PlayerLetter + " won this round!");
            }
            else if (win == 1)
            {
                EndGame(Player.PlayerIDstring);
                Console.ReadKey();
            }
            else if (win == 0 & ReadFromFile == true)
            {
                Console.WriteLine("No one won this round");
            }
        }

        static void EndGame(string winner)
        {
            //This method displays the winner and resets the board
            Console.WriteLine("\n" + winner + " won the game!\nPress any key to return to menu.");
            Console.ReadKey();

            Player.Player1 = true;  //set player one as the first player

            ClearBoard();   //reset the array
            Menu();         //return to the main menu
        }
        static void ClearBoard()
        {
            //fill array with asteriks
            for (int i = 0; i < board.GetLength(0); i++)    //for each row
            {
                for (int d = 0; d < board.GetLength(1); d++) //for each column
                {
                    board[i, d] = "*";  //set index as "*"
                }
            }
        }
        static void ReadFromFile()
        {
            //This method reads a number of moves from a file and determines which player wins or if it will be a tie.
            Console.Clear();
            if (File.Exists("Input.txt"))   //check if "Input.txt" exists
            {
                string[] line = File.ReadAllLines("Input.txt"); //place each individual line in an array

                int roundcounter = 0;   
                for (int l = 0; l < line.Length; l++)
                {
                    //thanks patrick for this magic
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
                        //check if it is player one's or player two's turn
                        if (i % 2 == 0) 
                        {
                            Player.PlayerLetter = "A";
                        }
                        else
                        {
                            Player.PlayerLetter = "B";
                        }

                        Moves[i] = int.Parse(LineLength.Split(' ')[i]);
                        DropChecker(Moves[i], true);    //drop checker in column
                    }
                    CheckForWin(true); //check if anyone has won so far
                    ClearBoard();     //reset the board array   
                }
                Console.ReadKey();
            }
            else    //if the file does not exist then kindly ask the user to create it
            {
                Console.WriteLine("Input.txt does not exist. Please create it.");
                Console.ReadKey();
                Menu(); //call Menu() method to return to menu
            }
        }

        class Player
        {
            //This class contains various variables regarding player status and id
            public static string PlayerIDstring = "Player One";    //Either "Player One" or "Player Two"
            public static bool Player1 = true;      //determines if player one is active or not
            public static string PlayerLetter;      //identifies the player on the board and is the player identifier stored in the board[,] array
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
