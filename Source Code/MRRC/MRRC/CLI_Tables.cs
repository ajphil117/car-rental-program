using System;
using System.Collections.Generic;
using System.Linq;
using MRRCManagement;


namespace MRRC
{
    /// <summary>
    /// 
    /// The CLI_Tables (Command Line Interface Tables) class holds the methods for creating the 
    /// customers, vehicles, and rentals tables.
    /// 
    /// References: (Used in each of the below methods)     
    /// "tableHorizontalLines = String.Concat(Enumerable.Repeat("-", horizontalLength));"
    ///     - taken from: https://stackoverflow.com/questions/411752/best-way-to-repeat-a-character-in-c-sharp
    ///     
    /// "String.Format(LENGTHS)"
    ///     - taken from: https://stackoverflow.com/questions/9542994/variable-string-format-length
    ///              AND: https://www.csharp-examples.net/align-string-with-spaces/
    /// 
    /// Author Ash Phillips April 2020
    /// 
    /// </summary>
    class CLI_Tables
    {   
        /// <summary>
        /// This method creates a table for the customers list.
        /// </summary>
        /// 
        /// <param name="customers"> The customers list. </param>
        /// <param name="attributeCount"> The number of attributes a customer has. </param>
        public static void Create_Customers_Table(List<Customer> customers)
        {
            // Constants:
            int PADDING = 19; // 19 padding -> eg. | ID | -> 3 padding around detail, excluding 2nd | as it counts for next attribute
                              // horizontalLength + 3 for each attribute + 1 for final |

            int ID_HEADER_LENGTH = 2; // ID
            int TITLE_HEADER_LENGTH = 5; // Title
            int FN_HEADER_LENGTH = 9; //FirstName
            int LN_HEADER_LENGTH = 8; //LastName
            int GENDER_HEADER_LENGTH = 6; //Gender
            int DATE_LENGTH = 10; // eg. 11/01/2020

            int DATE_TWO_DIGITS = 22; // eg. 11/01/2020 12:00:00 AM
            int DATE_ONE_DIGIT = 21; // eg. 1/01/2020 12:00:00 AM

            // Variables:
            string tableHorizontalLines;
            string tableHeader;
            string tableData;

            int IDLength;
            int titleLength;
            int FNLength;
            int LNLength;
            int genderLength;
            int DOBLength;
            int horizontalLength;

            string customerID;
            string customerGender;
            string customerDOB = "";

            // Set attribute lengths to length of header titles:
            IDLength = ID_HEADER_LENGTH;
            titleLength = TITLE_HEADER_LENGTH;
            FNLength = FN_HEADER_LENGTH;
            LNLength = LN_HEADER_LENGTH;
            genderLength = GENDER_HEADER_LENGTH;

            // Set DOBLength to length of a date:
            DOBLength = DATE_LENGTH;

            // Loop through customer attributes to get longest length in each attribute column:
            //      (excluding DOB as the date will ALWAYS be longer than the header title)
            foreach (var customer in customers)
            {
                // Change each variable to strings where required:
                customerID = customer.CustomerID.ToString();
                customerGender = customer.CustomerGender.ToString();
                customerDOB = (customer.CustomerDOB.ToString()).Remove(11);

                // Check lengths:
                // ID:
                if (customerID.Length > IDLength)
                {
                    IDLength = customerID.Length;
                }
                // Title:
                if (customer.CustomerTitle.Length > titleLength)
                {
                    titleLength = customer.CustomerTitle.Length;
                }
                // First name:
                if (customer.CustomerFN.Length > FNLength)
                {
                    FNLength = customer.CustomerFN.Length;
                }
                // Last name:
                if (customer.CustomerLN.Length > LNLength)
                {
                    LNLength = customer.CustomerLN.Length;
                }
                // Gender:
                if (customerGender.Length > genderLength)
                {
                    genderLength = customerGender.Length;
                }
            }

            // Set horizontalLength:
            horizontalLength = IDLength + titleLength + FNLength + LNLength + genderLength + DOBLength + PADDING;

            // Create table horizontal lines:
            tableHorizontalLines = String.Concat(Enumerable.Repeat("-", horizontalLength));

            // Create table header:
            tableHeader = String.Format("| {0, " + -IDLength + "} | {1, " + -titleLength + "} | {2, " + -FNLength + "} | {3, "
                                        + -LNLength + "} | {4, " + -genderLength + "} | {5, " + -DOBLength + "} |\n",
                                        "ID", "Title", "FirstName", "LastName", "Gender", "DOB");

            // Create table data line layout:
            tableData = "| {0, " + -IDLength + "} | {1, " + -titleLength + "} | {2, " + -FNLength + "} | {3, "
                        + -LNLength + "} | {4, " + -genderLength + "} | {5, " + -DOBLength + "} |\n";

            // Print table to console:
            Console.WriteLine(tableHorizontalLines);
            Console.Write(tableHeader);
            Console.WriteLine(tableHorizontalLines);
            foreach (var customer in customers)
            {
                // Set DOB:
                if ((customer.CustomerDOB.ToString()).Length == DATE_TWO_DIGITS)
                {
                    // Remove time:
                    customerDOB = (customer.CustomerDOB.ToString()).Remove(10); // time starts at index 10
                }
                if ((customer.CustomerDOB.ToString()).Length == DATE_ONE_DIGIT)
                {
                    // Remove time:
                    customerDOB = (customer.CustomerDOB.ToString()).Remove(9); // time starts at index 9
                }

                // Print line of data:
                Console.Write(String.Format(tableData, customer.CustomerID, customer.CustomerTitle, customer.CustomerFN,
                                            customer.CustomerLN, customer.CustomerGender, customerDOB));
            }
            Console.WriteLine(tableHorizontalLines);
            Console.WriteLine();
        }


        /// <summary>
        /// This method creates a table for the vehicles list.
        /// </summary>
        /// 
        /// <param name="vehicles"> The vehicles list. </param>
        /// <param name="attributeCount"> The number of attributes a vehicle has. </param>
        public static void Create_Vehicles_Table(List<Vehicle> vehicles)
        {
            // Constants:
            int PADDING = 38; // 37 padding -> eg. | GPS | -> 3 padding around detail, excluding 2nd | as it counts for next detial
                              // horizontalLength + 3 for each detail + 1 for final |  and + 1 for $          

            int REGO_HEADER_LENGTH = 12; //Registration
            int GRADE_HEADER_LENGTH = 5; //Grade
            int MAKE_HEADER_LENGTH = 4; //Make
            int MODEL_HEADER_LENGTH = 5; //Model
            int YEAR_HEADER_LENGTH = 4; //Year
            int SEATS_HEADER_LENGTH = 8; //NumSeats
            int TRANSMISSION_HEADER_LENGTH = 12; //Transmission
            int GPS_HEADER_LENGTH = 3; //GPS
            int SUNROOF_HEADER_LENGTH = 7; //Sunroof
            int RATE_HEADER_LENGTH = 9; //DailyRate
            int COLOUR_HEADER_LENGTH = 6; //Colour

            int FUEL_LENGTH = 6; // Fuel = length 4, Petrol = length 6, Diesel = length 6
                                 // Therefore longest possible length = 6 ALWAYS


            // Variables:
            string tableHorizontalLines;
            string tableHeader;
            string tableData;

            int regoLength;
            int gradeLength;
            int makeLength;
            int modelLength;
            int yearLength;
            int seatsLength;
            int transmissionLength;
            int fuelLength;
            int GPSLength;
            int sunroofLength;
            int rateLength;
            int colourLength;
            int horizontalLength;

            string vehicleGrade;
            string vehicleYear;
            string vehicleGPS;

            // Set attribute lengths to length of header titles:
            regoLength = REGO_HEADER_LENGTH;
            gradeLength = GRADE_HEADER_LENGTH;
            makeLength = MAKE_HEADER_LENGTH;
            modelLength = MODEL_HEADER_LENGTH;
            yearLength = YEAR_HEADER_LENGTH;
            seatsLength = SEATS_HEADER_LENGTH;
            transmissionLength = TRANSMISSION_HEADER_LENGTH;
            GPSLength = GPS_HEADER_LENGTH;
            sunroofLength = SUNROOF_HEADER_LENGTH;
            rateLength = RATE_HEADER_LENGTH;
            colourLength = COLOUR_HEADER_LENGTH;

            // Set fuelLength to FUEL_LENGTH:
            fuelLength = FUEL_LENGTH;

            // Loop through vehicles details to get longest length in each detail column:
            foreach (var vehicle in vehicles)
            {
                // Change each variable to strings where required:
                vehicleGrade = vehicle.VehicleGrade.ToString();
                vehicleYear = vehicle.VehicleYear.ToString();
                vehicleGPS = vehicle.VehicleGPS.ToString();

                // Check lengths:
                // Rego will ALWAYS be length of header title
                // Grade:
                if (vehicleGrade.Length > gradeLength)
                {
                    gradeLength = vehicleGrade.Length;
                }
                // Make:
                if (vehicle.VehicleMake.Length > makeLength)
                {
                    makeLength = vehicle.VehicleMake.Length;
                }
                // Model:
                if (vehicle.VehicleModel.Length > modelLength)
                {
                    modelLength = vehicle.VehicleModel.Length;
                }
                // Year:
                if (vehicleYear.Length > yearLength)
                {
                    yearLength = vehicleYear.Length;
                }
                // NumSeats and Transmission will ALWAYS be length of header title
                // Fuel will ALWAYS be FUEL_LENGTH
                // GPS:
                if (vehicleGPS.Length > GPSLength)
                {
                    GPSLength = vehicleGPS.Length;
                }
                // Sunroof and Daily rate will ALWAYS be the length of header title
                // Colour:
                if (vehicle.VehicleColour.Length > colourLength)
                {
                    colourLength = vehicle.VehicleColour.Length;
                }
            }

            // Set horizontalLength:
            horizontalLength = regoLength + gradeLength + makeLength + modelLength + yearLength + seatsLength + transmissionLength
                               + fuelLength + GPSLength + sunroofLength + rateLength + colourLength + PADDING;

            // Create table horizontal lines:
            tableHorizontalLines = String.Concat(Enumerable.Repeat("-", horizontalLength));

            // Create table header:
            tableHeader = String.Format("| {0, " + -regoLength + "} | {1, " + -gradeLength + "} | {2, " + -makeLength + "} | {3, "
                                        + -modelLength + "} | {4, " + -yearLength + "} | {5, " + -seatsLength + "} | {6, "
                                        + -transmissionLength + "} | {7, " + -fuelLength + "} | {8, " + -GPSLength + "} | {9, "
                                        + -sunroofLength + "} | {10, " + -rateLength + "}  | {11, " + -colourLength + "} |\n",
                                        "Registration", "Grade", "Make", "Model", "Year", "NumSeats", "Transmission", "Fuel", "GPS",
                                        "Sunroof", "DailyRate", "Colour");

            // Create table data line layout:
            tableData = "| {0, " + -regoLength + "} | {1, " + -gradeLength + "} | {2, " + -makeLength + "} | {3, "
                        + -modelLength + "} | {4, " + -yearLength + "} | {5, " + -seatsLength + "} | {6, "
                        + -transmissionLength + "} | {7, " + -fuelLength + "} | {8, " + -GPSLength + "} | {9, "
                        + -sunroofLength + "} | ${10, " + -rateLength + ":0.00} | {11, " + -colourLength + "} |\n";

            // Print table to console:
            Console.WriteLine(tableHorizontalLines);
            Console.Write(tableHeader);
            Console.WriteLine(tableHorizontalLines);
            foreach (var vehicle in vehicles)
            {
                // Print line of data:
                Console.Write(String.Format(tableData, vehicle.VehicleRego, vehicle.VehicleGrade, vehicle.VehicleMake, vehicle.VehicleModel,
                                            vehicle.VehicleYear, vehicle.VehicleSeats, vehicle.VehicleTransmission, vehicle.VehicleFuel,
                                            vehicle.VehicleGPS, vehicle.VehicleSunroof, vehicle.VehicleRate, vehicle.VehicleColour));

            }
            Console.WriteLine(tableHorizontalLines);
            Console.WriteLine();
        }


        /// <summary>
        /// This method creates a table for the rentals dictionary, including daily rate of the rented vehilce.
        /// </summary>
        /// 
        /// <param name="rentals"> The rentals dictionary. </param>
        /// <param name="attributeCount"> The number of attributes a rental entry has. </param>
        public static void Create_Rentals_Table()
        {
            // Constants:
            int PADDING = 11; // 7 padding -> eg. | Registration | -> 3 padding around detail, excluding 2nd | as it counts for next detial
                             // horizontalLength + 3 for each detail + 1 for final | and + 1 for $

            int REGISTRATION_HEADER_LENGTH = 12; //Registration
            int ID_HEADER_LENGTH = 10; //CustomerID
            int RATE_HEADER_LENGTH = 9; //DailyRate

            // Variables:
            string tableHorizontalLines;
            string tableHeader;
            string tableData;

            int regoLength;
            int IDLength;
            int rateLength;
            int horizontalLength;

            string customerID;
            string regoNumber;
            int ID;
            double dailyRate;

            // Set attribute lengths to length of header titles:
            regoLength = REGISTRATION_HEADER_LENGTH;
            IDLength = ID_HEADER_LENGTH;
            rateLength = RATE_HEADER_LENGTH;

            // Loop customerID to get longest length in the column:
            foreach (var item in Fleet.rentals)
            {
                // Change to string:
                customerID = (item.Value).ToString();

                // Check length:
                // Customer ID:
                if (customerID.Length > IDLength)
                {
                    IDLength = customerID.Length;
                }
            }

            // Set horizontalLength:
            horizontalLength = regoLength + IDLength + rateLength + PADDING;

            // Create table horizontal lines:
            tableHorizontalLines = String.Concat(Enumerable.Repeat("-", horizontalLength));

            // Create table header:
            tableHeader = String.Format("| {0, " + -regoLength + "} | {1, " + -IDLength + "} | {2, " + -rateLength + "}  |\n",
                                        "Registration", "CustomerID", "DailyRate");

            // Create table data line layout:
            tableData = "| {0, " + -regoLength + "} | {1, " + -IDLength + "} | ${2, " + -rateLength + ":0.00} |\n";
            
            // Print table to console:
            Console.WriteLine(tableHorizontalLines);
            Console.Write(tableHeader);
            Console.WriteLine(tableHorizontalLines);
            foreach (var item in Fleet.rentals.ToList())
            {
                // Get regoNumber:
                regoNumber = item.Key;

                // Get ID:
                ID = item.Value;

                // Get dailyRate:
                dailyRate = Fleet.GetRate(regoNumber);
                
                // Print line of data:
                Console.Write(String.Format(tableData, regoNumber, ID, dailyRate));
            }
            Console.WriteLine(tableHorizontalLines);
            Console.WriteLine();
        }


    }//end CLI_Tables class
}//end namespace
