using System;
using System.Collections.Generic;
using System.Linq;
using MRRCManagement;
using MRRCSearchAlgorithm;


namespace MRRC
{
    /// <summary>
    /// 
    /// The CLI_Search (Command Line Interface Searcg) class holds the method for searching the vehicles for attributes in query and displaying the
    /// results to the console in table form.
    /// 
    /// References:    
    /// This class implements features given in the worksheet 9 solutions that have been adapted for use here.
    /// 
    /// Author Ash Phillips June 2020
    /// 
    /// </summary>
    class CLI_Search
    {
        /// <summary>
        /// This method checks the query type, splits the query into parts, reshuffles order if parts based on the SHunting Algorithm, and then
        /// displays the serch results; the able of vehicles that have the attributes stated in the query.
        /// </summary>
        /// 
        /// <param name="query"> The search query the user enters in the console. </param>
        /// <returns> Returns true if the search returned results (valid search), returns false otherwise. </returns>
        public static bool Query_Search(string query)
        {
            // Variables:
            string NO_VEHICLES = "\nThere are no vehicles to display.\n";
            List<Vehicle> unrentedVehicles = Fleet.GetFleet(false);

            List<IToken> infixTokens = new List<IToken>();
            List<IToken> postfixTokens = new List<IToken>();

            List<string> attributeList;
            List<Vehicle> myVehicles = new List<Vehicle>();

            bool attributeMatch;

            // Blank query:
            if (query == "" || query.All(char.IsWhiteSpace))
            {
                // Print all unrented vehicles to console:
                Console.WriteLine();
                CLI_Tables.Create_Vehicles_Table(unrentedVehicles);

                // End search:
                return true;
            }

            // Split query into infixTokens:
            try
            {
                infixTokens = Search.ParseText(query);

                // If malformed:
                if (infixTokens == null)
                {
                    // End search:
                    return false;
                }

                // Perform the shunting yard step:                
                postfixTokens = Search.ShuntingYard(infixTokens);

                // If mismatched left parenthesis:
                if (postfixTokens == null)
                {
                    // End search:
                    return false;
                }
                // Search vehicles list for attribute:
                try
                {
                    foreach (Vehicle vehicle in unrentedVehicles)
                    {
                        // Get attributes for each vehicle:
                        attributeList = Vehicle.GetAttributeList(vehicle);

                        // Check if attribute is in list:
                        attributeMatch = Search.Results(postfixTokens, attributeList);

                        // Add vehicle to list if it has the attribute:
                        if (attributeMatch)
                        {
                            myVehicles.Add(vehicle);
                        }
                    }

                    // Display vehicles table:
                    if (myVehicles.Count() == 0)
                    {
                        // Print no vehicles to display message to console:
                        Console.WriteLine(NO_VEHICLES);

                        // End search:
                        return true;
                    }
                    else
                    {
                        // Show search results:
                        Console.WriteLine();
                        CLI_Tables.Create_Vehicles_Table(myVehicles);

                        // End search:
                        return true;
                    }
                }
                catch (InvalidOperationException)
                {
                    // Print error message:
                    Console.WriteLine("\n*** Error: Query '{0}' cannot be parsed into tokens; malformed. ***\n", query);

                    // End search:
                    return false;
                }                
            }
            catch (IndexOutOfRangeException)
            {
                // Print error message:
                Console.WriteLine("\n*** Error: Mismatched quotes in query. ***\n");

                // End search:
                return false;
            }            
        }


    }//end CLI_Search class
}//end namespace
