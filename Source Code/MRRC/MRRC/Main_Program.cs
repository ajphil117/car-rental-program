using System;
using System.IO;
using MRRCManagement;


namespace MRRC
{
    /// <summary>
    /// 
    /// The Main_Program class holds the Main to run the program.
    /// 
    /// Author Ash Phillips June 2020
    /// 
    /// </summary>
    public class Main_Program
    {      
        static void Main(string[] args)
        {
            // Try to get file paths and load files:
            try
            {
                // Set file paths from user inputs in command line arguments debug:
                Fleet.fleet_file_path = args[0];
                Fleet.rentals_file_path = args[1];
                CRM.customers_file_path = args[2];

                // Load files:
                CRM crm = new CRM();
                Fleet fleet = new Fleet();

                // Begin program:
                CLI_Menus.Menu_Text();
            }
            catch (IndexOutOfRangeException)
            {
                // Print error message:
                Console.WriteLine("\n*** Error: Insufficient command line arguments for file paths. ***\n");

                // Tell user to escape:
                CLI_Inputs.Escape_Program();
            }
            catch (IOException)
            {
                // Print error message:
                Console.WriteLine("\n*** Error: csv file(s) in use; must close to continue. ***\n");

                // Tell user to escape:
                CLI_Inputs.Escape_Program();
            }

            // Keep console from closing:
            Console.ReadLine();
        }//end Main


    }//end Main_Program class
}//end namespace
