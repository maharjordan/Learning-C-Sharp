using System;
using System.Threading;

namespace TicTacToe
{
    class Program
    {
        static void clear_board(int[] game_board)
        {
            //Check that the game board array is of correct length
            if (game_board.Length != 9)
                throw new System.ArgumentException();

            for (int i = 0; i < game_board.Length; i++)
            {
                game_board[i] = 0;
            }
        }

        static bool player_choice(int choice, int[] game_board)
        {
            if(choice > 0 && choice < 10)
            {
                if (game_board[choice-1] == 0)
                    return true;
                else
                    return false;
            }
            else
            {
                throw new ArgumentException("Incorrect user choice. ");
            }
        }

        static int opponent_choice(int[] game_board)
        {
            Random rnd = new Random();
            int opp_choice = 0;
            while(true)
            {
               opp_choice = rnd.Next(9);
               if(game_board[opp_choice] == 0)
                {
                    return opp_choice;
                }
            }
        }

        static void print_board(int[] game_board)
        {
            //Check that the game board array is of correct length
            if (game_board.Length != 9)
                throw new System.ArgumentException();
            //Create an array of characters
            char[] game_tiles = new char[9];
            for (int i = 0; i < game_board.Length; i++)
            {
                if (game_board[i] == 0)
                {
                    game_tiles[i] = (char)(i + 49
                        );
                }
                else if (game_board[i] == 1)
                {
                    game_tiles[i] = 'X';
                }
                else if (game_board[i] == 2)
                {
                    game_tiles[i] = 'O';
                }
            }
            print_square(game_tiles);
        }
            static void print_square(char[] char_array)
        {
            //Set dimenion values
            int array_length = char_array.Length;
            double square_check = Math.Sqrt(array_length);
            int square_length = Convert.ToInt32(square_check);
            int counter = 0;

            //Check that the array represents a square
            if (square_length * square_length != array_length)
            {
                throw new ArgumentException("Incorrect array length");
            }

            for (int i = 0; i < square_length; i++)
            {
                for (int j = 0; j < square_length; j++)
                {
                    if(char_array[counter] == 'X')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if(char_array[counter] == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    if (j == 0)
                    {
                        Console.Write($" {char_array[counter]} ");
                        Console.ResetColor();
                        Console.Write("| ");
                    }
                    else if (j == square_length - 1)
                    {
                        Console.Write($"{char_array[counter]}");
                    }
                    else
                    {
                        Console.Write($"{char_array[counter]} ");
                        Console.ResetColor();
                        Console.Write("| ");
                    }
                    counter++;
                    Console.ResetColor();
                }
                Console.Write("\n");
                if (i != square_length - 1)
                {
                    for (int k = 0; k < square_length * 3 + 1; k++)
                        Console.Write("-");
                    Console.Write("\n");
                }
            }
        }

        static bool check_victory(int[] game_board)
        {
            //Check if player won
            if(game_board[0] == 1 && game_board[1] == 1 && game_board[2] == 1)
                return true;
            if (game_board[3] == 1 && game_board[4] == 1 && game_board[5] == 1)
                return true;
            if (game_board[6] == 1 && game_board[7] == 1 && game_board[8] == 1)
                return true;
            if (game_board[0] == 1 && game_board[3] == 1 && game_board[6] == 1)
                return true;
            if (game_board[1] == 1 && game_board[4] == 1 && game_board[7] == 1)
                return true;
            if (game_board[2] == 1 && game_board[5] == 1 && game_board[8] == 1)
                return true;
            if (game_board[0] == 1 && game_board[4] == 1 && game_board[8] == 1)
                return true;
            if (game_board[2] == 1 && game_board[4] == 1 && game_board[6] == 1)
                return true;
            //Check if opponent won
            if (game_board[0] == 2 && game_board[1] == 2 && game_board[2] == 2)
                return true;
            if (game_board[3] == 2 && game_board[4] == 2 && game_board[5] == 2)
                return true;
            if (game_board[6] == 2 && game_board[7] == 2 && game_board[8] == 2)
                return true;
            if (game_board[0] == 2 && game_board[3] == 2 && game_board[6] == 2)
                return true;
            if (game_board[1] == 2 && game_board[4] == 2 && game_board[7] == 2)
                return true;
            if (game_board[2] == 2 && game_board[5] == 2 && game_board[8] == 2)
                return true;
            if (game_board[0] == 2 && game_board[4] == 2 && game_board[8] == 2)
                return true;
            if (game_board[2] == 2 && game_board[4] == 2 && game_board[6] == 2)
                return true;

            //If none of the victory conditions are met, no one must be a winner
            return false;
        }


        static void Main(string[] args)
        {
            //Create array that holds game board information
            int[] game_board = new int[9];
            clear_board(game_board);

            //Win values
            bool player_win = false;
            bool opponent_win = false;
            bool game_tie = false;

            //Print Title Screen
            Console.WriteLine("***************");
            Console.WriteLine("* Tic Tac Toe *");
            Console.WriteLine("* 1. New Game *");
            Console.WriteLine("* 2. Quit     *");
            Console.WriteLine("***************");

            //Get user input
            Console.Write("Option: ");
            string selection = Console.ReadLine();
            int selection_numeric = 0;


            //Start Game
            if (selection == "1")
            {
                while(true)
                {
                    //Print the current game board
                    Console.Clear();
                    print_board(game_board);

                    //Player makes their selection
                    Console.Write("Option: ");
                    selection = Console.ReadLine();
                    
                    switch(selection)
                    {
                        case "1":
                            selection_numeric = 1;
                            break;
                        case "2":
                            selection_numeric = 2;
                            break;
                        case "3":
                            selection_numeric = 3;
                            break;
                        case "4":
                            selection_numeric = 4;
                            break;
                        case "5":
                            selection_numeric = 5;
                            break;
                        case "6":
                            selection_numeric = 6;
                            break;
                        case "7":
                            selection_numeric = 7;
                            break;
                        case "8":
                            selection_numeric = 8;
                            break;
                        case "9":
                            selection_numeric = 9;
                            break;
                        default:
                            selection_numeric = 0;
                            break;
                    }
                    if(selection_numeric != 0 && game_board[selection_numeric - 1] == 0)
                    {
                        if (player_choice(selection_numeric, game_board))
                        {
                            game_board[selection_numeric - 1] = 1;
                        }

                        if(check_victory(game_board))
                        {
                            player_win = true;
                            break;
                        }

                        //Check for a tie
                        if (
                            game_board[0] != 0 &&
                            game_board[1] != 0 &&
                            game_board[2] != 0 &&
                            game_board[3] != 0 &&
                            game_board[4] != 0 &&
                            game_board[5] != 0 &&
                            game_board[6] != 0 &&
                            game_board[7] != 0 &&
                            game_board[8] != 0
                            )
                        {
                            game_tie = true;
                            break;
                        }

                        //Opponent makes there selection
                        Console.Clear();
                        print_board(game_board);

                        Thread.Sleep(1000);

                        game_board[opponent_choice(game_board)] = 2;

                        if(check_victory(game_board))
                        {
                            opponent_win = true;
                            break;
                        }

                        //Check for a tie
                        if(
                            game_board[0] != 0 &&
                            game_board[1] != 0 &&
                            game_board[2] != 0 &&
                            game_board[3] != 0 &&
                            game_board[4] != 0 &&
                            game_board[5] != 0 &&
                            game_board[6] != 0 &&
                            game_board[7] != 0 &&
                            game_board[8] != 0 
                            )
                        {
                            game_tie = true;
                            break;
                        }

                    }
                }
                //Game is ended
                Console.Clear();
                print_board(game_board);

                if(player_win)
                {
                    Console.WriteLine("\nCongratulations! You Won!");
                }
                else if(opponent_win)
                {
                    Console.WriteLine("\nSorry... You Lose.");
                }
                else
                {
                    Console.WriteLine("\nIt is a tie.");
                }

                Console.WriteLine("Press any key to end. ");
                Console.ReadKey();

            }
        }
    }
}
