using System;
using System.Collections.Generic;
using System.Linq;
using MRRCManagement;


namespace MRRC
{
    /// <summary>
    /// 
    /// The CLI_Modifiers (Command Line Interface Modifiers) class holds the methods for modifying the
    /// customer and vehicle attributes, and for generating the next highest ID number for a customer.
    /// The methods for modifying the attributes of a customer and vehicle are spilt into modifying all
    /// attributes at once or modifying them one at a time, with an additional method for vehicles to modify
    /// only the required attributes.
    /// It also holds the method for getting the information to rent and return a vehicle.
    /// 
    /// Author Ash Phillips June 2020
    /// 
    /// </summary>
    public class CLI_Modifiers
    {
        /*** Set up constants and variables needed in the below methods ***/

        /** Set Constants **/

        static string FILL_FIELDS = "Please fill the following field(s). (fields marked with * are required)\n"
                                    + "\nTo escape sequence, please leave field(s) blank.\n";
        static string BLANK_FIELDS = CLI_Menus.BLANK_FIELDS;

        static string COMMANDS_DISABLED = CLI_Menus.COMMANDS_DISABLED;
        static string COMMANDS_ENABLED = CLI_Menus.COMMANDS_ENABLED;

        // Customer Attributes:        
        static string TITLE = "*Title: ";
        static string FIRST_NAME = "*First Name: ";
        static string LAST_NAME = "*Last Name: ";
        static string GENDER = "*Gender (M/F/Other): ";
        static string DOB = "*DOB (dd/mm/yyyy): ";

        // Vehicle Attributes:
        static string REGO = "*Registration: ";
        static string GRADE = "*Grade (Economy/Family/Luxury/Commercial): ";
        static string MAKE = "*Make: ";
        static string MODEL = "*Model: ";
        static string YEAR = "*Year: ";
        static string NUM_SEATS = "Number of Seats: ";
        static string TRANSMISSION = "Transmission Type (Automatic/Manual): ";
        static string FUEL = "Fuel Type (Petrol/Diesel): ";
        static string GPS = "GPS (Y/N): ";
        static string SUNROOF = "Sunroof (Y/N): ";
        static string DAILY_RATE = "Daily Rate: $";
        static string COLOUR = "Colour: ";

        // Rental Atrributes:
        static string ID = "*ID: ";
        static string RENTAL_TIME = "*Rental Time (days): ";        


        /*** METHODS ***/

        /* Customer Attributes */

        /// <summary>
        /// This method changes all the attributes of the customer and checks the validity of the inputs.
        /// 
        /// References:
        /// "VARIABLE.All(Char.IsLetter)"
        ///     - taken from: https://stackoverflow.com/questions/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp  
        /// </summary>
        /// 
        /// <param name="failMessage"> The fail message to be printed to the console. </param>
        /// <returns> The number of valid feilds. </returns>
        public static int Customer_Attributes_All(int numFields, string failMessage)
        {
            // Reset valid fields:
            CLI_Menus.validFields = 0;

            // Change all attributes:
            Console.WriteLine(FILL_FIELDS);

            Console.Write(TITLE);
            CLI_Menus.titleInput = Console.ReadLine();

            Console.Write(FIRST_NAME);
            CLI_Menus.FNInput = Console.ReadLine();

            Console.Write(LAST_NAME);
            CLI_Menus.LNInput = Console.ReadLine();

            Console.Write(GENDER);
            CLI_Menus.genderInput = Console.ReadLine();

            Console.Write(DOB);
            CLI_Menus.DOBInput = Console.ReadLine();

            // Check if fields are left blank:
            if (CLI_Menus.titleInput == "" || CLI_Menus.FNInput == "" || CLI_Menus.LNInput == "" || CLI_Menus.genderInput == ""
                || CLI_Menus.DOBInput == "")
            {
                // Print exit message:
                Console.WriteLine(BLANK_FIELDS);
                Console.WriteLine(COMMANDS_ENABLED);

                // Previous menu:
                CLI_Menus.Customer_Management_Menu();

                return 0;
            }

            // Check that all inputs are valid:
            // Tile, First Name, Last Name:
            if (CLI_Menus.titleInput.All(Char.IsLetter) && CLI_Menus.FNInput.All(Char.IsLetter) && CLI_Menus.LNInput.All(Char.IsLetter))
            {               
                CLI_Menus.validFields += 3;

                // Gender:                    
                if (CLI_Menus.genderInput.ToUpper() == "FEMALE" || CLI_Menus.genderInput.ToUpper() == "F")
                {
                    CLI_Menus.genderInputConverted = Gender.Female;
                    CLI_Menus.validFields += 1;
                }
                else if (CLI_Menus.genderInput.ToUpper() == "MALE" || CLI_Menus.genderInput.ToUpper() == "M")
                {
                    CLI_Menus.genderInputConverted = Gender.Male;
                    CLI_Menus.validFields += 1;
                }
                else if (CLI_Menus.genderInput.ToUpper() == "OTHER" || CLI_Menus.genderInput.ToUpper() == "O")
                {
                    CLI_Menus.genderInputConverted = Gender.Other;
                    CLI_Menus.validFields += 1;
                }

                // DOB:
                try
                {
                    CLI_Menus.DOBInputConverted = DateTime.Parse(CLI_Menus.DOBInput);
                        CLI_Menus.validFields += 1;
                    }
                    catch
                    {
                        Console.WriteLine();
                        Console.WriteLine(failMessage);

                        // Return int:
                        return CLI_Menus.validFields;
                    }  
                
            }

            // Check that all fileds entered are valid:
            if (CLI_Menus.validFields != numFields)
            {
                Console.WriteLine(failMessage);
            }

            // Return int:
            return CLI_Menus.validFields;
        }


        /// <summary>
        /// This method changes a single attribute of the customer  depending on the menu input 
        /// and then checks the validity of the attrbiute input.
        /// 
        ///  References:
        /// "VARIABLE.All(Char.IsLetter)"
        ///     - taken from: https://stackoverflow.com/questions/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp
        /// </summary>
        /// 
        /// <param name="failMessage"> The fail message to be printed to the console. </param>
        /// <returns> Returns 1 if the modified feild is valid, 0 if not valid. </returns>
        public static int Customer_Attributes_Singular(string failMessage)
        {
            // Variables:
            string previousMenu = "Customer_Management";
            
            // Title:
            if (CLI_Menus.textInput.ToUpper() == "A")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(TITLE);
                CLI_Menus.titleInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.titleInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Customer_Management_Menu();

                    return 0;
                }

                // Check validity:
                if (CLI_Menus.titleInput.All(Char.IsLetter))
                {
                    CLI_Menus.validFields = 1;
                    CLI_Menus.titleModded = true;
                }
            }
            // First name:
            else if (CLI_Menus.textInput.ToUpper() == "B")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(FIRST_NAME);
                CLI_Menus.FNInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.FNInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Customer_Management_Menu();

                    return 0;
                }

                // Check validity:
                if (CLI_Menus.FNInput.All(Char.IsLetter))
                {
                    CLI_Menus.validFields = 1;
                    CLI_Menus.FNModded = true;
                }
            }
            // Last name:
            else if (CLI_Menus.textInput.ToUpper() == "C")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(LAST_NAME);
                CLI_Menus.LNInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.LNInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Customer_Management_Menu();

                    return 0;
                }

                // Check validity:
                if (CLI_Menus.LNInput.All(Char.IsLetter))
                {
                    CLI_Menus.validFields = 1;
                    CLI_Menus.LNModded = true;
                }
            }
            // Gender:
            else if (CLI_Menus.textInput.ToUpper() == "D")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(GENDER);
                CLI_Menus.genderInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.genderInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Customer_Management_Menu();

                    return 0;
                }

                // Check Validity:               
                if (CLI_Menus.genderInput.ToUpper() == "FEMALE" || CLI_Menus.genderInput.ToUpper() == "F")
                {
                    CLI_Menus.genderInputConverted = Gender.Female;
                    CLI_Menus.validFields = 1;
                    CLI_Menus.genderModded = true;
                }
                else if (CLI_Menus.genderInput.ToUpper() == "MALE" || CLI_Menus.genderInput.ToUpper() == "M")
                {
                    CLI_Menus.genderInputConverted = Gender.Male;
                    CLI_Menus.validFields = 1;
                    CLI_Menus.genderModded = true;
                }
                else if (CLI_Menus.genderInput.ToUpper() == "OTHER" || CLI_Menus.genderInput.ToUpper() == "O")
                {
                    CLI_Menus.genderInputConverted = Gender.Other;
                    CLI_Menus.validFields = 1;
                    CLI_Menus.genderModded = true;
                }                                  
            }
            // DOB:
            else if (CLI_Menus.textInput.ToUpper() == "E")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(DOB);
                CLI_Menus.DOBInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.DOBInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Customer_Management_Menu();

                    return 0;
                }

                // Check validity:
                try
                {
                    CLI_Menus.DOBInputConverted = DateTime.Parse(CLI_Menus.DOBInput);
                    CLI_Menus.validFields = 1;
                    CLI_Menus.DOBModded = true;
                }
                catch
                {
                    Console.WriteLine();
                    Console.WriteLine(failMessage);

                    // Return int:
                    return CLI_Menus.validFields;
                }
            }
            
            // Not a valid input:
            else
            {
                // Print error statement:
                Console.WriteLine();
                Console.WriteLine(CLI_Menus.VALIDITY_CATCH);
                Console.Write(CLI_Menus.TYPE_ICON);

                // Read users keypress:
                CLI_Menus.textInput = CLI_Inputs.Check_Key_Press(previousMenu);

                // Return int:
                return CLI_Menus.validFields;
            }

            // Check that the single field entered is vaild:
            if (CLI_Menus.validFields != 1)
            {
                Console.WriteLine(failMessage);                
            }

            // Return int:
            return CLI_Menus.validFields;
        }


        /// <summary>
        /// This method generates an ID for the customer. If there are no customers in the list the ID
        /// generated will be 0. If there are customers in the list, the generated ID will be the highest
        /// ID number increased by one.
        /// </summary>
        /// 
        /// <returns> The generated ID for the customer. </returns>
        public static int Generate_ID()
        {
            // Variables:
            List<int> IDs = new List<int>();
            int highestID;            

            // Add all existing IDs into IDs list:
            foreach (var customerSaved in CLI_Menus.customers)
            {
                IDs.Add(customerSaved.CustomerID);
            }

            // Get highest ID number and add 1:
            try
            {
                highestID = IDs.Max();
                highestID += 1;
            }
            catch
            {
                // If there are no IDs in the list:
                highestID = 0;
            }

            // Return new ID number:
            return highestID;
        }


        /* Vehicle Attributes */

        /// <summary>
        /// This method changes all the attributes of the vechicle and checks the validity of the inputs.
        /// 
        /// References:
        /// "VARIABLE.All(Char.IsLetter)"
        ///     - taken from: https://stackoverflow.com/questions/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp
        /// </summary>
        /// 
        /// <param name="failMessage"> The fail message to be printed to the console. </param>
        /// <returns> The number of valid feilds. </returns>
        public static int Vehicle_Attributes_All(int numFields, string failMessage)
        {
            // Constants:
            int REGO_LENGTH = 6; // eg. 000XXX
            int YEAR_LENGTH = 4; // eg. 2020
            int SEATS_LOWER = 2;
            int SEATS_UPPER = 10;

            // Reset valid fields:
            CLI_Menus.validFields = 0;

            // Change all vehicle attributes:
            Console.WriteLine(FILL_FIELDS);

            Console.Write(REGO);
            CLI_Menus.regoInput = Console.ReadLine();

            Console.Write(GRADE);
            CLI_Menus.gradeInput = Console.ReadLine();

            Console.Write(MAKE);
            CLI_Menus.makeInput = Console.ReadLine();

            Console.Write(MODEL);
            CLI_Menus.modelInput = Console.ReadLine();

            Console.Write(YEAR);
            CLI_Menus.yearInput = Console.ReadLine();

            Console.Write(NUM_SEATS);
            CLI_Menus.seatsInput = Console.ReadLine();

            Console.Write(TRANSMISSION);
            CLI_Menus.transmissionInput = Console.ReadLine();

            Console.Write(FUEL);
            CLI_Menus.fuelInput = Console.ReadLine();

            Console.Write(GPS);
            CLI_Menus.GPSInput = Console.ReadLine();

            Console.Write(SUNROOF);
            CLI_Menus.sunroofInput = Console.ReadLine();

            Console.Write(DAILY_RATE);
            CLI_Menus.rateInput = Console.ReadLine();

            Console.Write(COLOUR);
            CLI_Menus.colourInput = Console.ReadLine();

            // Check if field(s) are left blank:
            if (CLI_Menus.regoInput == "" || CLI_Menus.gradeInput == "" || CLI_Menus.makeInput == "" || CLI_Menus.modelInput == ""
                || CLI_Menus.yearInput == "" || CLI_Menus.seatsInput == "" || CLI_Menus.transmissionInput == "" || CLI_Menus.fuelInput == ""
                || CLI_Menus.GPSInput == "" || CLI_Menus.sunroofInput == "" || CLI_Menus.rateInput == "" || CLI_Menus.colourInput == "")
            {
                // Print exit message:
                Console.WriteLine(BLANK_FIELDS);
                Console.WriteLine(COMMANDS_ENABLED);

                // Previous menu:
                CLI_Menus.Fleet_Management_Menu();

                return 0;
            }

            // Check that all inputs are valid:
            // Rego:
            if (CLI_Menus.regoInput.Length == REGO_LENGTH)
            {
                try
                {
                    // Check first 3 characters are integers:
                    Convert.ToInt32(CLI_Menus.regoInput.Remove(3));

                    // Check last 3 characters are letters:
                    if (CLI_Menus.regoInput.Remove(0, 3).All(Char.IsLetter))
                    {
                        CLI_Menus.validFields += 1;

                        // Make, Model, Colour:
                        if (CLI_Menus.makeInput.All(Char.IsLetter) && CLI_Menus.colourInput.All(Char.IsLetter))
                        {                            
                            CLI_Menus.validFields += 3;

                            // Year, Daily rate:
                            if (CLI_Menus.yearInput.Length == YEAR_LENGTH)
                            {
                                try
                                {
                                    CLI_Menus.yearInputConverted = Convert.ToInt32(CLI_Menus.yearInput);                                        
                                    CLI_Menus.rateInputConverted = Convert.ToDouble(CLI_Menus.rateInput);

                                    CLI_Menus.validFields += 2;

                                    // Seats:
                                    CLI_Menus.seatsInputConverted = Convert.ToInt32(CLI_Menus.seatsInput);
                                    if (CLI_Menus.seatsInputConverted >= SEATS_LOWER && CLI_Menus.seatsInputConverted <= SEATS_UPPER)
                                    {
                                        CLI_Menus.validFields += 1;

                                        // GPS:
                                        if (CLI_Menus.GPSInput.ToUpper() == "Y")
                                        {
                                            CLI_Menus.GPSInputConverted = true;
                                            CLI_Menus.validFields += 1;
                                        }
                                        else if (CLI_Menus.GPSInput.ToUpper() == "N")
                                        {
                                            CLI_Menus.GPSInputConverted = false;
                                            CLI_Menus.validFields += 1;
                                        }

                                        // Sunroof:                                            
                                        if (CLI_Menus.sunroofInput.ToUpper() == "Y")
                                        {
                                            CLI_Menus.sunroofInputConverted = true;
                                            CLI_Menus.validFields += 1;
                                        }
                                        else if (CLI_Menus.sunroofInput.ToUpper() == "N")
                                        {
                                            CLI_Menus.sunroofInputConverted = false;
                                            CLI_Menus.validFields += 1;
                                        }

                                        // Grade: 
                                        if (CLI_Menus.gradeInput.ToUpper() == "COMMERICAL" || CLI_Menus.gradeInput.ToUpper() == "C")
                                        {
                                            CLI_Menus.gradeInputConverted = VehicleGrade.Commercial;
                                            CLI_Menus.validFields += 1;
                                        }
                                        else if (CLI_Menus.gradeInput.ToUpper() == "ECONOMY" || CLI_Menus.gradeInput.ToUpper() == "E")
                                        {
                                            CLI_Menus.gradeInputConverted = VehicleGrade.Economy;
                                            CLI_Menus.validFields += 1;
                                        }
                                        else if (CLI_Menus.gradeInput.ToUpper() == "FAMILY" || CLI_Menus.gradeInput.ToUpper() == "F")
                                        {
                                            CLI_Menus.gradeInputConverted = VehicleGrade.Family;
                                            CLI_Menus.validFields += 1;
                                        }
                                        else if (CLI_Menus.gradeInput.ToUpper() == "LUXURY" || CLI_Menus.gradeInput.ToUpper() == "L")
                                        {
                                            CLI_Menus.gradeInputConverted = VehicleGrade.Luxury;
                                            CLI_Menus.validFields += 1;
                                        }

                                        // Transmission:                                                    
                                        if (CLI_Menus.transmissionInput.ToUpper() == "AUTOMATIC" || CLI_Menus.transmissionInput.ToUpper() == "A")
                                        {
                                            CLI_Menus.transmissionInputConverted = TransmissionType.Automatic;
                                            CLI_Menus.validFields += 1;
                                        }
                                        else if (CLI_Menus.transmissionInput.ToUpper() == "MANUAL" || CLI_Menus.transmissionInput.ToUpper() == "M")
                                        {
                                            CLI_Menus.transmissionInputConverted = TransmissionType.Manual;
                                            CLI_Menus.validFields += 1;
                                        }

                                        // Fuel:                                                        
                                        if (CLI_Menus.fuelInput.ToUpper() == "DIESEL" || CLI_Menus.fuelInput.ToUpper() == "D")
                                        {
                                            CLI_Menus.fuelInputConverted = FuelType.Diesel;
                                            CLI_Menus.validFields += 1;
                                        }
                                        else if (CLI_Menus.fuelInput.ToUpper() == "PETROL" || CLI_Menus.fuelInput.ToUpper() == "P")
                                        {
                                            CLI_Menus.fuelInputConverted = FuelType.Petrol;
                                            CLI_Menus.validFields += 1;
                                        }  
                                    }                                                                                
                                }
                                catch
                                {
                                    Console.WriteLine(failMessage);

                                    // Return int:
                                    return CLI_Menus.validFields;
                                }
                            }                                
                                                        
                        }                        
                    }                    
                }
                catch
                {
                    Console.WriteLine(failMessage);

                    // Return int:
                    return CLI_Menus.validFields;
                }
            }

            // Check if all fields are valid:
            if (CLI_Menus.validFields != numFields)
            {
                Console.WriteLine(failMessage);
            }

            // Return int:
            return CLI_Menus.validFields;
        }


        /// <summary>
        /// This method changes only the required attributes of the vechicle and checks the validity of the inputs.
        /// 
        /// References:
        /// "VARIABLE.All(Char.IsLetter)"
        ///     - taken from: https://stackoverflow.com/questions/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp
        /// </summary>
        /// 
        /// <param name="failMessage"> The fail message to be printed to the console. </param>
        /// <returns> The number of valid feilds. </returns>
        public static int Vehicle_Attributes_Required(int numFields, string failMessage)
        {
            // Constants:
            int REGO_LENGTH = 6; // eg. 000XXX
            int YEAR_LENGTH = 4; // eg. 2020

            // Reset valid fields:
            CLI_Menus.validFields = 0;

            // Change required attributes:
            Console.WriteLine(FILL_FIELDS);

            Console.Write(REGO);
            CLI_Menus.regoInput = Console.ReadLine();

            Console.Write(GRADE);
            CLI_Menus.gradeInput = Console.ReadLine();

            Console.Write(MAKE);
            CLI_Menus.makeInput = Console.ReadLine();

            Console.Write(MODEL);
            CLI_Menus.modelInput = Console.ReadLine();

            Console.Write(YEAR);
            CLI_Menus.yearInput = Console.ReadLine();

            // Check if field(s) are left blank:
            if (CLI_Menus.regoInput == "" || CLI_Menus.gradeInput == "" || CLI_Menus.makeInput == "" || CLI_Menus.modelInput == ""
                || CLI_Menus.yearInput == "")
            {
                // Print exit message:
                Console.WriteLine(BLANK_FIELDS);
                Console.WriteLine(COMMANDS_ENABLED);

                // Previous menu:
                CLI_Menus.Fleet_Management_Menu();

                return 0;
            }

            // Check that all inputs are valid:
            // Rego:
            if (CLI_Menus.regoInput.Length == REGO_LENGTH)
            {
                try
                {
                    // Check first 3 characters are integers:
                    Convert.ToInt32(CLI_Menus.regoInput.Remove(3));

                    // Check last 3 characters are letters:
                    if (CLI_Menus.regoInput.Remove(0, 3).All(Char.IsLetter))
                    {
                        CLI_Menus.validFields += 1;

                        // Make, Model:
                        if (CLI_Menus.makeInput.All(Char.IsLetter))
                        {
                            if (CLI_Menus.makeInput != "" && CLI_Menus.modelInput != "")
                            {
                                CLI_Menus.validFields += 2;

                                // Year:
                                if (CLI_Menus.yearInput.Length == YEAR_LENGTH)
                                {
                                    try
                                    {
                                        CLI_Menus.yearInputConverted = Convert.ToInt32(CLI_Menus.yearInput);
                                        CLI_Menus.validFields += 1;
                                        
                                        // Grade:
                                        if (CLI_Menus.gradeInput.ToUpper() == "COMMERCIAL" || CLI_Menus.gradeInput.ToUpper() == "C")
                                        {
                                            CLI_Menus.gradeInputConverted = VehicleGrade.Commercial;
                                            CLI_Menus.validFields += 1;
                                        }
                                        else if (CLI_Menus.gradeInput.ToUpper() == "ECONOMY" || CLI_Menus.gradeInput.ToUpper() == "E")
                                        {
                                            CLI_Menus.gradeInputConverted = VehicleGrade.Economy;
                                            CLI_Menus.validFields += 1;
                                        }
                                        else if (CLI_Menus.gradeInput.ToUpper() == "FAMILY" || CLI_Menus.gradeInput.ToUpper() == "F")
                                        {
                                            CLI_Menus.gradeInputConverted = VehicleGrade.Family;
                                            CLI_Menus.validFields += 1;
                                        }
                                        else if (CLI_Menus.gradeInput.ToUpper() == "LUXURY" || CLI_Menus.gradeInput.ToUpper() == "L")
                                        {
                                            CLI_Menus.gradeInputConverted = VehicleGrade.Luxury;
                                            CLI_Menus.validFields += 1;
                                        }                                       
                                    }
                                    catch
                                    {
                                        Console.WriteLine(failMessage);

                                        // Return int:
                                        return CLI_Menus.validFields;
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    Console.WriteLine(failMessage);

                    // Return int:
                    return CLI_Menus.validFields;
                }
            }

            // Check if all fields are valid:
            if (CLI_Menus.validFields != numFields)
            {
                Console.WriteLine(failMessage);
            }

            // Return int:
            return CLI_Menus.validFields;
        }


        ///// <summary>
        ///// This method changes a single attribute of the vehicle depending on the menu input 
        ///// and then checks the validity of the attrbiute input.
        ///// 
        ///// References:
        ///// "VARIABLE.All(Char.IsLetter)"
        /////     - taken from: https://stackoverflow.com/questions/1181419/verifying-that-a-string-contains-only-letters-in-c-sharp
        ///// </summary>
        ///// 
        ///// <param name="failMessage"> The fail message to be printed to the console. </param>
        ///// <returns> Returns 1 if the modified feild is valid, 0 if not valid. </returns>
        public static int Vehicle_Attributes_Singular(string failMessage)
        {
            // Constants:
            int REGO_LENGTH = 6; // eg. 000XXX
            int YEAR_LENGTH = 4; // eg. 2020
            int SEATS_LOWER = 2;
            int SEATS_UPPER = 10;

            // Variables:
            string previousMenu = "Fleet_Managment";

            // Rego:
            if (CLI_Menus.textInput.ToUpper() == "A")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(REGO);
                CLI_Menus.regoInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.regoInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Fleet_Management_Menu();

                    return 0;
                }

                // Check validity:
                if (CLI_Menus.regoInput.Length == REGO_LENGTH)
                {
                    try
                    {
                        // Check first 3 characters are integers:
                        Convert.ToInt32(CLI_Menus.regoInput.Remove(3));

                        // Check last 3 characters are letters:
                        if ((CLI_Menus.regoInput.Remove(0, 3)).All(Char.IsLetter))
                        {
                            CLI_Menus.validFields = 1;
                            CLI_Menus.regoModded = true;                            
                        }
                    }
                    catch
                    {
                        Console.WriteLine(failMessage);

                        // Return int:
                        return CLI_Menus.validFields;
                    }
                }
            }
            // Grade:
            else if (CLI_Menus.textInput.ToUpper() == "B")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(GRADE);
                CLI_Menus.gradeInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.gradeInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Fleet_Management_Menu();

                    return 0;
                }

                // Check validity:
                if (CLI_Menus.gradeInput.ToUpper() == "COMMERCIAL" || CLI_Menus.gradeInput.ToUpper() == "C")
                {
                    CLI_Menus.gradeInputConverted = VehicleGrade.Commercial;
                    CLI_Menus.validFields = 1;
                    CLI_Menus.gradeModded = true;
                }
                else if (CLI_Menus.gradeInput.ToUpper() == "ECONOMY" || CLI_Menus.gradeInput.ToUpper() == "E")
                {
                    CLI_Menus.gradeInputConverted = VehicleGrade.Economy;
                    CLI_Menus.validFields = 1;
                    CLI_Menus.gradeModded = true;
                }
                else if (CLI_Menus.gradeInput.ToUpper() == "FAMILY" || CLI_Menus.gradeInput.ToUpper() == "F")
                {
                    CLI_Menus.gradeInputConverted = VehicleGrade.Family;
                    CLI_Menus.validFields = 1;
                    CLI_Menus.gradeModded = true;
                }
                else if (CLI_Menus.gradeInput.ToUpper() == "LUXURY" || CLI_Menus.gradeInput.ToUpper() == "L")
                {
                    CLI_Menus.gradeInputConverted = VehicleGrade.Luxury;
                    CLI_Menus.validFields = 1;
                    CLI_Menus.gradeModded = true;
                }
            }
            // Make:
            else if (CLI_Menus.textInput.ToUpper() == "C")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(MAKE);
                CLI_Menus.makeInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.makeInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Fleet_Management_Menu();

                    return 0;
                }

                // Check validity:
                if (CLI_Menus.makeInput.All(Char.IsLetter))
                {
                    CLI_Menus.validFields = 1;
                    CLI_Menus.makeModded = true;
                }
            }
            // Model:
            else if (CLI_Menus.textInput.ToUpper() == "D")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(MODEL);
                CLI_Menus.modelInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.modelInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Fleet_Management_Menu();

                    return 0;
                }

                // Check Validity:
                if (CLI_Menus.modelInput != "")
                {
                    CLI_Menus.validFields = 1;
                    CLI_Menus.modelModded = true;
                }
            }
            // Year:
            else if (CLI_Menus.textInput.ToUpper() == "E")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(YEAR);
                CLI_Menus.yearInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.yearInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Fleet_Management_Menu();

                    return 0;
                }

                // Check validity:
                if (CLI_Menus.yearInput.Length == YEAR_LENGTH)
                {
                    try
                    {
                        CLI_Menus.yearInputConverted = Convert.ToInt32(CLI_Menus.yearInput);
                        CLI_Menus.validFields = 1;
                        CLI_Menus.yearModded = true;
                    }
                    catch
                    {
                        Console.WriteLine(failMessage);

                        // Return int:
                        return CLI_Menus.validFields;
                    }
                }
            }
            // Seats:
            else if (CLI_Menus.textInput.ToUpper() == "F")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(NUM_SEATS);
                CLI_Menus.seatsInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.seatsInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Fleet_Management_Menu();

                    return 0;
                }

                // Check validity:
                try
                {
                    CLI_Menus.seatsInputConverted = Convert.ToInt32(CLI_Menus.seatsInput);

                    if (CLI_Menus.seatsInputConverted >= SEATS_LOWER && CLI_Menus.seatsInputConverted <= SEATS_UPPER)
                    {
                        CLI_Menus.validFields = 1;
                        CLI_Menus.seatsModded = true;
                    }                    
                }
                catch
                {
                    Console.WriteLine(failMessage);

                    // Return int:
                    return CLI_Menus.validFields;
                }
            }
            // Transmission:
            else if (CLI_Menus.textInput.ToUpper() == "G")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(TRANSMISSION);
                CLI_Menus.transmissionInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.transmissionInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Fleet_Management_Menu();

                    return 0;
                }

                // Check validity:
                if (CLI_Menus.transmissionInput.ToUpper() == "AUTOMATIC" || CLI_Menus.transmissionInput.ToUpper() == "A")
                {
                    CLI_Menus.transmissionInputConverted = TransmissionType.Automatic;
                    CLI_Menus.validFields += 1;
                    CLI_Menus.transmissionModded = true;
                }
                else if (CLI_Menus.transmissionInput.ToUpper() == "MANUAL" || CLI_Menus.transmissionInput.ToUpper() == "M")
                {
                    CLI_Menus.transmissionInputConverted = TransmissionType.Manual;
                    CLI_Menus.validFields += 1;
                    CLI_Menus.transmissionModded = true;
                }
            }
            // Fuel:
            else if (CLI_Menus.textInput.ToUpper() == "H")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(FUEL);
                CLI_Menus.fuelInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.fuelInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Fleet_Management_Menu();

                    return 0;
                }

                // Check validity:
                if (CLI_Menus.fuelInput.ToUpper() == "DIESEL" || CLI_Menus.fuelInput.ToUpper() == "D")
                {
                    CLI_Menus.fuelInputConverted = FuelType.Diesel;
                    CLI_Menus.validFields = 1;
                    CLI_Menus.fuelModded = true;
                }
                else if (CLI_Menus.fuelInput.ToUpper() == "PETROL" || CLI_Menus.fuelInput.ToUpper() == "P")
                {
                    CLI_Menus.fuelInputConverted = FuelType.Petrol;
                    CLI_Menus.validFields = 1;
                    CLI_Menus.fuelModded = true;
                }
            }
            // GPS:
            else if (CLI_Menus.textInput.ToUpper() == "I")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(GPS);
                CLI_Menus.GPSInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.GPSInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Fleet_Management_Menu();

                    return 0;
                }

                // Check validity:
                if (CLI_Menus.GPSInput.ToUpper() == "Y")
                {
                    CLI_Menus.GPSInputConverted = true;

                    CLI_Menus.validFields = 1;
                    CLI_Menus.GPSModded = true;
                }
                else if (CLI_Menus.GPSInput.ToUpper() == "N")
                {
                    CLI_Menus.GPSInputConverted = false;

                    CLI_Menus.validFields = 1;
                    CLI_Menus.GPSModded = true;
                }
            }
            // Sunroof:
            else if (CLI_Menus.textInput.ToUpper() == "J")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(SUNROOF);
                CLI_Menus.sunroofInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.sunroofInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Fleet_Management_Menu();

                    return 0;
                }

                // Check validity:
                if (CLI_Menus.sunroofInput.ToUpper() == "Y")
                {
                    CLI_Menus.sunroofInputConverted = true;
                    CLI_Menus.validFields = 1;
                    CLI_Menus.sunroofModded = true;
                }
                else if (CLI_Menus.sunroofInput.ToUpper() == "N")
                {
                    CLI_Menus.sunroofInputConverted = false;
                    CLI_Menus.validFields = 1;
                    CLI_Menus.sunroofModded = true;
                }
            }
            // Rate:
            else if (CLI_Menus.textInput.ToUpper() == "K")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(DAILY_RATE);
                CLI_Menus.rateInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.rateInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Fleet_Management_Menu();

                    return 0;
                }

                // Check validity:
                try
                {
                    CLI_Menus.rateInputConverted = Convert.ToDouble(CLI_Menus.rateInput);
                    CLI_Menus.validFields = 1;
                    CLI_Menus.rateModded = true;
                }
                catch
                {
                    Console.WriteLine(failMessage);

                    // Return int:
                    return CLI_Menus.validFields;
                }
            }
            // Colour:
            else if (CLI_Menus.textInput.ToUpper() == "L")
            {
                // Reset valid fields:
                CLI_Menus.validFields = 0;

                // Print disabled message:
                Console.WriteLine();
                Console.WriteLine(COMMANDS_DISABLED);

                // Print prompt:
                Console.Write(COLOUR);
                CLI_Menus.colourInput = Console.ReadLine();

                // Check if field is left blank:
                if (CLI_Menus.colourInput == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Fleet_Management_Menu();

                    return 0;
                }

                // Check validity:
                if (CLI_Menus.colourInput.All(Char.IsLetter))
                {
                    CLI_Menus.validFields = 1;
                    CLI_Menus.colourModded = true;
                }
            }

            // Not a valid input:
            else
            {
                // Print error statement:
                Console.WriteLine();
                Console.WriteLine(CLI_Menus.VALIDITY_CATCH);
                Console.Write(CLI_Menus.TYPE_ICON);

                // Read users keypress:
                CLI_Menus.textInput = CLI_Inputs.Check_Key_Press(previousMenu);

                // Return int:
                return CLI_Menus.validFields;
            }

            // Check that the single field entered is vaild:
            if (CLI_Menus.validFields != 1)
            {
                Console.WriteLine(failMessage);
            }

            // Return int:
            return CLI_Menus.validFields;
        }


        /// <summary>
        /// This method provides the menu functionality for the Rent Vehicle menu.
        /// User enters the attributes to rent a vehicle. 
        /// </summary>
        /// 
        /// <param name="numFields"> The number of fields to enter/fill in. </param>
        /// <param name="failMessage"> The error message printed to the console. </param>
        /// <returns> The number of valid feilds. </returns>
        public static int Rental_Attributes_Rent()
        {
            // Constants:
            int REGO_LENGTH = 6; // eg. 000XXX

            // Variables:
            List<int> IDs = new List<int>();
            List<string> regos = new List<string>();

            bool rented;
            bool renting;

            // Reset valid fields:
            CLI_Menus.validFields = 0;

            // Add all existing IDs into IDs list:
            foreach (var customerSaved in CLI_Menus.customers)
            {
                IDs.Add(customerSaved.CustomerID);
            }

            // Add all existing registrations into regos list:
            foreach (var vehicleSaved in CLI_Menus.vehicles)
            {
                regos.Add(vehicleSaved.VehicleRego);
            }

            // Enter required attributes:
            Console.WriteLine(FILL_FIELDS);

            Console.Write(ID);
            CLI_Menus.IDInput = Console.ReadLine();

            Console.Write(REGO);
            CLI_Menus.regoInput = Console.ReadLine();

            Console.Write(RENTAL_TIME);
            CLI_Menus.rentalTimeInput = Console.ReadLine();

            // Convert regoInput to uppercae (as is required):
            CLI_Menus.regoInput = CLI_Menus.regoInput.ToUpper();

            // Check if field(s) are left blank:
            if (CLI_Menus.IDInput == "" || CLI_Menus.regoInput == "" || CLI_Menus.rentalTimeInput == "")
            {
                // Print exit message:
                Console.WriteLine(BLANK_FIELDS);
                Console.WriteLine(COMMANDS_ENABLED);

                // Previous menu:
                CLI_Menus.Rental_Management_Menu();

                return 0;
            } 

            // Check validty:
            // ID:
            try
            {
                CLI_Menus.IDInputConverted = Convert.ToInt32(CLI_Menus.IDInput);

                // Check if ID is in list:
                if (IDs.Contains(CLI_Menus.IDInputConverted))
                {
                    // Check if renting:
                    renting = Fleet.IsRenting(CLI_Menus.IDInputConverted);
                    if (!renting)
                    {
                        CLI_Menus.validFields += 1;

                        // Rego:
                        if (CLI_Menus.regoInput.Length == REGO_LENGTH && (CLI_Menus.regoInput.Remove(0, 3)).All(Char.IsLetter))
                        {
                            try
                            {
                                Convert.ToInt32(CLI_Menus.regoInput.Remove(3));

                                // Check registration is in list:
                                if (regos.Contains(CLI_Menus.regoInput))
                                {
                                    // Check if rented:
                                    rented = Fleet.IsRented(CLI_Menus.regoInput);
                                    if (!rented)
                                    {
                                        CLI_Menus.validFields += 1;

                                        // Rental time:
                                        try
                                        {
                                            CLI_Menus.rentalTimeInputConverted = Convert.ToInt32(CLI_Menus.rentalTimeInput);
                                            CLI_Menus.validFields += 1;
                                        }
                                        catch
                                        {
                                            Console.WriteLine("\n*** WARNING: Rental time not valid. Please enter valid rental time. ***\n");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("\n*** WARNING: Could not rent vehicle. Vehicle is being rented. ***\n");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\n*** WARNING: The registration number could not be found. Please enter a valid registration number. ***\n");
                                }
                            }
                            catch
                            {
                                Console.WriteLine("\n*** WARNING: Registraiton number not valid. Please enter valid registration number. ***\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n*** WARNING: Registraiton number not valid. Please enter valid registration number. ***\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n*** WARNING: Could not rent vehicle. The customer is renting a vehicle. ***\n");
                    }
                }
                else
                {
                    Console.WriteLine("\n*** WARNING: The ID number could not be found. Please enter a valid ID number. ***\n");
                }
            }
            catch
            {
                Console.WriteLine("\n*** WARNING: ID is not valid. Please enter valid ID number. ***\n");
            }

            // Return int:
            return CLI_Menus.validFields;
        }


    }// end CLI_Modifiers class
}//end namespace
