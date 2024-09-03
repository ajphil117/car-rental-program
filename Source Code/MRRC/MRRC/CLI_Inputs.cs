using System;


namespace MRRC
{
    /// <summary>
    /// 
    /// The CLI_Inputs (Command Line Interface Inputs) class provides the methods for escaping the program on
    /// ESC key press when files cannot be loaded, monitoring the users key presses when navigating menus,
    /// and checking the validity of the users inputs. 
    /// 
    /// Author Ash Phillips April 2020
    /// 
    /// </summary>
    public class CLI_Inputs
    {
        /// <summary>
        /// This method escapes the program if the user presses ESC.
        /// 
        /// References:
        /// The readkey method was taken from: 
        ///     https://stackoverflow.com/questions/8789646/using-readline-and-readkey-simultaneously  
        /// </summary>
        public static void Escape_Program()
        {
            // Variables:
            ConsoleKeyInfo readKeyResult;

            // Write escape message:
            Console.WriteLine("Please press ESC to exit.");

            // Set readKeyResult to true:
            readKeyResult = Console.ReadKey(true);

            // Check if user presses ESC: 
            if (readKeyResult.Key == ConsoleKey.Escape)
            {
                // Close console:
                Environment.Exit(0);
            }
        }


        /// <summary>
        /// This method checks the key press of the user. If ESC, the console is closed. If BACKSPACE, the console
        /// peints the previous menu. And if any other input it returns it in string format.
        /// 
        /// References:
        /// The readkey method was taken from: 
        ///     https://stackoverflow.com/questions/8789646/using-readline-and-readkey-simultaneously  
        /// </summary>
        /// 
        /// <param name="previousMenu"> The previous menu to return to. </param>
        /// <returns> Null if user presses ESC or BACKSPACE, otherwise it returns the input in string format. </returns>        
        public static string Check_Key_Press(string previousMenu)
        {
            // Variables:
            string textInput;
            ConsoleKeyInfo readKeyResult = Console.ReadKey(true);

            // Check if user presses ESC or BACKSPACE: 
            if (readKeyResult.Key == ConsoleKey.Escape)
            {
                // Close console:
                Environment.Exit(0);

                return null;
            }
            else if (readKeyResult.Key == ConsoleKey.Backspace)
            {
                Console.WriteLine("\n");

                // Go to previous menu:
                if (previousMenu == "Main_Menu")
                {
                    Console.Clear();
                    CLI_Menus.Menu_Text();
                }
                else if (previousMenu == "Customer_Management")
                {
                    CLI_Menus.Customer_Management_Menu();
                }
                else if (previousMenu == "Fleet_Management")
                {
                    CLI_Menus.Fleet_Management_Menu();
                }
                else if (previousMenu == "Rental_Management")
                {
                    CLI_Menus.Rental_Management_Menu();
                }

                return null;
            }
            else if (readKeyResult.Key == ConsoleKey.H)
            {                
                // Clear console and go to main menu:                
                Console.Clear();
                CLI_Menus.Menu_Text();

                return null;
            }
            // If user enters anything other than ESC or BACKSPACE:
            else
            {
                // Read input into textInput:
                textInput = (readKeyResult.KeyChar).ToString();
                // Print key pressed to console:
                Console.WriteLine(textInput);

                return textInput;
            }
        }


    }//end CLI_Inputs class    
}//end namepace
