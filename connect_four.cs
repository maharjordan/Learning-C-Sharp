using System;
using System.Threading;

namespace connect_four
{
    public class Program
    {
        //Function that returns a two dimensional integer array with all values set two zero
        static int[,] initialize_board(int rows, int columns)
        {
            int[,] init_board = new int[rows, columns];

            for (int i = 0; i < init_board.GetLength(0); i++)
            {
                for (int j = 0; j < init_board.GetLength(1); j++)
                {
                    init_board[i, j] = 0;
                }
            }
            return init_board;
        }

        static void print_board_char(int board_value)
        {
            //Print unallocated slots to white
            if (board_value == 0)
            {
                Console.Write("@");
            }
            //Print the players slot to red
            if (board_value == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("@");
                Console.ResetColor();
            }
            //Print the opponents slot to yellow
            if (board_value == 2)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("@");
                Console.ResetColor();
            }
        }

        static void print_board(int[,] game_board)
        {
            //Print Game Title
            Console.WriteLine("CONNECT FOUR");
            for (int i = 0; i < game_board.GetLength(0); i++)
            {
                //Print the row key
                Console.Write(i+1);
                for (int j = 0; j < game_board.GetLength(1); j++)
                {
                    print_board_char(game_board[i,j]);
                }
                Console.Write("\n");
            }
            //Print the Option Key on the bottom
            Console.Write(" ");
            for (int i = 0; i < game_board.GetLength(1); i++)
            {
                Console.Write( (char)(i+65));
            }
        }

        //Method that returns an array. The first element indicates whether the method was successful.
        static int[] player_move(int[,] game_board, string selection)
        {
            int[] result = new int[3];

            if (selection.Length != 1)
            {
                result[0] = 0;
                return result;
            }
            //Get two characters from string
            char char_selection = selection[0];

            int int_selection = (int)char_selection;

            //Convert the row character into an array index
            if (int_selection >= 65 && int_selection <= 90)
                int_selection -= 65;
            else if (int_selection >= 97 && int_selection <= 122)
                int_selection -= 97;
            else
            {
                result[0] = 0;
                return result;
            }


            //Check the index is valid
            if (int_selection >= game_board.GetLength(1))
            {
                result[0] = 0;
                return result;
            }

            if (game_board[0, int_selection] == 0)
            {
                result[0] = 1;
                result[2] = int_selection;

                //Get free row
                for(int i = 0; i < game_board.GetLength(0); i++)
                {
                    if(game_board[game_board.GetLength(0) - 1, int_selection] == 0)
                    {
                        result[1] = game_board.GetLength(0) - 1;
                        break;
                    }
                    else if (game_board[i+1, int_selection] == 1 || game_board[i+1, int_selection] == 2)
                    {
                        result[1] = i;
                        break;
                    }
                }
                return result;
            }
            else
            {
                result[0] = 0;
                return result;
            }
        }

        
        static int[] opponent_move(int[,] game_board)
        {
            int[] result = new int[2];

            //Get first value
            Random rnd = new Random();
            int ran_num_col;
            while (true)
            {
                ran_num_col = rnd.Next(0, game_board.GetLength(0)+1);
                if (game_board[0, ran_num_col] == 0)
                    break;
            }

            result[1] = ran_num_col;

            //Get free row
            for (int i = 0; i < game_board.GetLength(0); i++)
            {
                if (game_board[game_board.GetLength(0) - 1, ran_num_col] == 0)
                {
                    result[0] = game_board.GetLength(0) - 1;
                    break;
                }
                else if (game_board[i + 1, ran_num_col] == 1 || game_board[i + 1, ran_num_col] == 2)
                {
                    result[0] = i;
                    break;
                }
            }

            return result;
        }
        

        static bool check_victory(int[,] game_board, int win_condition)
        {
            //Check rows
            for (int i = 0; i < game_board.GetLength(0); i++)
            {
                for(int j = 0; j < game_board.GetLength(1) - win_condition + 1; j++)
                {
                    //Check if player won
                    if (game_board[i,j] == 1 && 
                        game_board[i,j] == game_board[i,j+1] && 
                        game_board[i,j+1] == game_board[i,j+2] &&
                        game_board[i, j + 2] == game_board[i, j + 3])
                    {
                        return true;
                    }

                    //Check if opponent won
                    if (game_board[i, j] == 2 &&
                        game_board[i, j] == game_board[i, j + 1] &&
                        game_board[i, j + 1] == game_board[i, j + 2] &&
                        game_board[i, j + 2] == game_board[i, j + 3])
                    {
                        return true;
                    }
                }
            }

            //Check Columns
            for (int j = 0; j < game_board.GetLength(1); j++)
            {
                for (int i = 0; i < game_board.GetLength(0) - win_condition + 1; i++)
                {
                    //Check if player won
                    if (game_board[i, j] == 1 &&
                        game_board[i, j] == game_board[i+1, j] &&
                        game_board[i+1, j] == game_board[i+2, j] &&
                        game_board[i+2, j] == game_board[i+3, j])
                    {
                        return true;
                    }

                    //Check if opponent won
                    if (game_board[i, j] == 2 &&
                        game_board[i, j] == game_board[i+1, j] &&
                        game_board[i+1, j] == game_board[i+2, j] &&
                        game_board[i+2, j] == game_board[i+3, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static bool check_tie(int[,] game_board)
        {
            for(int i = 0; i < game_board.GetLength(1); i++)
            {
                if (game_board[0, i] == 0)
                    return false;
            }
            return true;
        }

        public static void Main(string[] args)
        {
            //Board Settings : MAX = 9
            int rows = 6;
            int columns = 7;
            int win_condition = 4;

            bool player_win = false;
            bool opponent_win = false;

            Console.WriteLine("****************");
            Console.WriteLine("* Connect Four *");
            Console.WriteLine("* 1. New Game  *");
            Console.WriteLine("* 2. Quit      *");
            Console.WriteLine("****************");

            //Get the Users Input
            string selection;
            while (true)
            {
                Console.Write("Option: ");
                selection = Console.ReadLine();
                if (selection == "1" || selection == "2")
                    break;
            }

            //Play the Game
            if (selection == "1")
            {
                //Generate new board
                int[,] game_board = initialize_board(rows, columns);

                //Start Gameplay
                while (true)
                {
                    Console.Clear();

                    //Print the current game board
                    print_board(game_board);

                    

                    //Add the players option to the board
                    while (true)
                    {
                        //Get the players selection
                        Console.Write("\nOption: ");
                        selection = Console.ReadLine();

                        //Get players move and check input is value
                        int[] player_move_array = player_move(game_board, selection);
                        if (player_move_array[0] == 1)
                        {
                            game_board[player_move_array[1], player_move_array[2]] = 1;
                            break;
                        }
                    }

                    Console.Clear();
                    //Print the current board
                    print_board(game_board);

                    //Check victory conditions
                    if (check_victory(game_board, win_condition))
                    {
                        player_win = true;
                        break;
                    }
                    if (check_tie(game_board))
                        break;

                    //Opponents move
                    //Pause for one second so opponents move stands out
                    Thread.Sleep(1000);

                    int[] opponent_selection = new int[2];
                    opponent_selection = opponent_move(game_board);
                    game_board[opponent_selection[0], opponent_selection[1]] = 2;

                    //Check victory conditions
                    if (check_victory(game_board, win_condition))
                    {
                        opponent_win = true;
                        break;
                    }
                    if (check_tie(game_board))
                        break;

                }
                Console.Clear();
                print_board(game_board);
                if(player_win)
                {
                    Console.WriteLine("\nYou Win!");
                }
                else if(opponent_win)
                {
                    Console.WriteLine("\nSorry, you lost...");
                }
                else
                {
                    Console.WriteLine("\nThe game was a tie. ");
                }

                
                Console.WriteLine("Press any key to exit. ");
                Console.ReadKey();

            }
            //Else, exit console
        }
    }
}
