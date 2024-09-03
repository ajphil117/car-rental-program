using System;
using System.Collections.Generic;
using System.Linq;
using MRRCManagement;


namespace MRRC
{
    /// <summary>
    /// 
    /// The CLI_Menus (Command Line Interface Menus) class holds the methods for printing the menus to the console,
    /// constructing a menu. It also holds the code for printing the menu header, the main menu, the management menus,
    /// the display menus, the addition, modification, and deletion of customers/vehicles menus, and the search for, rent,
    /// and return vehicle menus.
    /// 
    /// Author Ash Phillips June 2020
    /// 
    /// </summary>
    public class CLI_Menus
    {
        /*** Set up constants and variables needed in the below methods ***/

        /** Set Variables from other classes **/

        // Customer, vehicle, and rental lists:
        public static List<Customer> customers = CRM.customers;

        public static List<Vehicle> vehicles = Fleet.vehicles;
        public static Dictionary<string, int> rentals = Fleet.rentals;


        /** Set Constants **/

        // Interface staples:
        public static string TYPE_ICON = "> ";
        public static string VALIDITY_CATCH = "*** WARNING: Input not recognised. Please enter a valid input. ***\n";
        public static string COMMANDS_DISABLED = "*** ESC and BACKSPACE commands disabled, must fill feild(s). ***\n";
        public static string COMMANDS_ENABLED = "\n*** ESC and BACKSPACE commands re-enabled. ***\n";

        public static string BLANK_FIELDS = "\nField(s) left blank. Returning to previous menu.";

        // Menus:
        static string MAIN_MENU = "a) Customer Management\n" +
                                  "b) Fleet Management\n" +
                                  "c) Rental Management\n";

        static string CUSTOMER_MENU = "a) Display Customers\n" +
                                      "b) New Customer\n" +
                                      "c) Modify Customer\n" +
                                      "d) Delete Customer\n";

        static string FLEET_MENU = "a) Display Fleet\n" +
                                   "b) New Vehicle\n" +
                                   "c) Modify Vehicle\n" +
                                   "d) Delete Vehicle\n";

        static string RENTAL_MENU = "a) Display Rentals\n" +
                                    "b) Search Vehicles\n" +
                                    "c) Rent Vehicle\n" +
                                    "d) Return Vehicle\n";

        static string CUSTOMER_ATTRIBUTES = "\tID Number: {0}\n" +
                                            "\ta) Title: {1}\n" +
                                            "\tb) First Name: {2}\n" +
                                            "\tc) Last Name: {3}\n" +
                                            "\td) Gender (M/F/Other): {4}\n" +
                                            "\te) DOB (dd/mm/yyyy): {5}\n";

        static string VEHICLE_ATTRIBUTES = "\ta) Registration: {0}\n" +
                                           "\tb) Grade (Economy/Family/Luxury/Commercial): {1}\n" +
                                           "\tc) Make: {2}\n" +
                                           "\td) Model: {3}\n" +
                                           "\te) Year: {4}\n" +
                                           "\tf) Number of Seats: {5}\n" +
                                           "\tg) Transmission Type (Automatic/Manual): {6}\n" +
                                           "\th) Fuel Type (Petrol/Diesel): {7}\n" +
                                           "\ti) GPS (Y/N): {8}\n" +
                                           "\tj) Sunroof (Y/N): {9}\n" +
                                           "\tk) Daily Rate: {10}\n" +
                                           "\tl) Colour: {11}\n";

        static string RENTAL_ATTRIBUTES = "\tCustomer ID: {0}\n" +
                                          "\tRegistration: {1}\n";


        /** Declare Variables: **/

        // Interface staples:
        public static string textInput;
        static int optionCount;
        public static int validFields = 0;

        // Customer attribute inputs: 
        public static bool inputValid;
        public static string titleInput;
        public static string FNInput;
        public static string LNInput;
        public static string genderInput;
        public static string DOBInput;

        public static Gender genderInputConverted;
        public static DateTime DOBInputConverted;

        // Randomised ID for new customer:
        static int generatedID;

        // Customer attribute modify checkers:
        public static bool titleModded = false;
        public static bool FNModded = false;
        public static bool LNModded = false;
        public static bool genderModded = false;
        public static bool DOBModded = false;

        // Vehicle attribute inputs:
        public static string regoInput;
        public static string gradeInput;
        public static string makeInput;
        public static string modelInput;
        public static string yearInput;
        public static string seatsInput;
        public static string transmissionInput;
        public static string fuelInput;
        public static string GPSInput;
        public static string sunroofInput;
        public static string rateInput;
        public static string colourInput;

        public static VehicleGrade gradeInputConverted;
        public static int yearInputConverted;
        public static int seatsInputConverted;
        public static TransmissionType transmissionInputConverted;
        public static FuelType fuelInputConverted;
        public static bool GPSInputConverted;
        public static bool sunroofInputConverted;
        public static double rateInputConverted;

        // Vehicle attribute modify checkers:
        public static bool regoModded = false;
        public static bool gradeModded = false;
        public static bool makeModded = false;
        public static bool modelModded = false;
        public static bool yearModded = false;
        public static bool seatsModded = false;
        public static bool transmissionModded = false;
        public static bool fuelModded = false;
        public static bool GPSModded = false;
        public static bool sunroofModded = false;
        public static bool rateModded = false;
        public static bool colourModded = false;

        // Rental attribute inputs:
        public static string IDInput;
        public static string rentalTimeInput;

        public static int IDInputConverted;
        public static double rentalTimeInputConverted;


        /*** METHODS ***/

        /// <summary>
        /// This method creates the menu text that prints on startup.
        /// </summary>
        public static void Menu_Text()
        {
            // Print the header and main menu to console:
            Menu_Header();
            Main_Menu();
        }


        /// <summary>
        /// This method creates the menu format.
        /// </summary>
        /// 
        /// <param name="menu"> The menu string to print. </param>
        /// <param name="optionBound"> The number of options in the menu. </param>
        public static void Make_Menu(string menu, int optionBound)
        {
            // Constants:
            string MENU_NAVIGATION = "Please enter a letter from the options below.\n";

            // Set optionBound variable:
            optionCount = optionBound;

            // Print Menu:
            Console.WriteLine(MENU_NAVIGATION);
            Console.WriteLine(menu);
            Console.Write(TYPE_ICON);
        }


        /// <summary>
        /// This method prints the header text to the console.
        /// </summary>
        public static void Menu_Header()
        {
            // Constants:
            string TITLE = "\n### Mates-Rates Rent-A-Car Operation Menu ###\n";
            string INFO = "You may press the ESC key at any menu to exit.\n\n"
                          + "Press the BACKSPACE key to return to the previous menu. Press the H key to return to the main menu.\n";

            // Create menu header:
            Console.WriteLine(TITLE);
            Console.WriteLine(INFO);
        }


        /// <summary>
        /// This method prints the main menu text to the console and switches the menu on valid input.
        /// </summary>
        public static void Main_Menu()
        {
            // Variables:
            string previousMenu = "Main_Menu";
            int options = 3;

            // Reset inputValid and textInput:
            inputValid = false;
            textInput = "";

            // Make menu:
            Make_Menu(MAIN_MENU, options);

            // Read users keypress:
            textInput = CLI_Inputs.Check_Key_Press(previousMenu);

            // Check if user input is vaild and change to menu:
            while (!inputValid)
            {
                if (textInput.ToUpper() == "A")
                {
                    Console.WriteLine();
                    Customer_Management_Menu();
                    inputValid = true;
                }
                else if (textInput.ToUpper() == "B")
                {
                    Console.WriteLine();
                    Fleet_Management_Menu();
                    inputValid = true;
                }
                else if (textInput.ToUpper() == "C")
                {
                    Console.WriteLine();
                    Rental_Management_Menu();
                    inputValid = true;
                }
                else
                {
                    // Reset textInput:
                    textInput = "";

                    // Ask to enter valid letter:
                    Console.WriteLine();
                    Console.WriteLine(VALIDITY_CATCH);
                    Console.Write(TYPE_ICON);

                    // Check input:
                    textInput = CLI_Inputs.Check_Key_Press(previousMenu);
                }
            }            
        }


        /// <summary>
        /// This method prints the customer management menu text to the console and switches the menu on valid input.
        /// </summary>
        public static void Customer_Management_Menu()
        {
            // Variables:
            string previousMenu = "Main_Menu";
            int options = 4;

            // Reset inputValid and textInput:
            inputValid = false;
            textInput = "";

            // Make menu:
            Make_Menu(CUSTOMER_MENU, options);

            // Check if user presses ESC or BACKSPACE:
            textInput = CLI_Inputs.Check_Key_Press(previousMenu);

            // Check if user input is vaild nad change to menu:
            while (!inputValid)
            {
                if (textInput.ToUpper() == "A")
                {
                    Console.WriteLine();
                    Display_Customers();
                    inputValid = true;
                }
                else if (textInput.ToUpper() == "B")
                {
                    Console.WriteLine();
                    New_Customer();
                    inputValid = true;
                }
                else if (textInput.ToUpper() == "C")
                {
                    Console.WriteLine();
                    Modify_Customer();
                    inputValid = true;
                }
                else if (textInput.ToUpper() == "D")
                {
                    Console.WriteLine();
                    Delete_Customer();
                    inputValid = true;
                }
                else
                {
                    // Reset textInput:
                    textInput = "";

                    // Ask to enter valid letter:
                    Console.WriteLine();
                    Console.WriteLine(VALIDITY_CATCH);
                    Console.Write(TYPE_ICON);

                    // Check input:
                    textInput = CLI_Inputs.Check_Key_Press(previousMenu);
                }
            }            
        }


        /// <summary>
        /// This method prints the customers table to the console.
        /// </summary>
        public static void Display_Customers()
        {
            // Refresh customers list:
            customers = CRM.GetCustomers();

            // Check if there are any customers to display:
            if (customers.Count != 0)
            {
                // Print table:
                CLI_Tables.Create_Customers_Table(customers);
            }
            else
            {
                // Print message:
                Console.WriteLine("There are no customers to display.\n");
            }            

            // Go back to previous menu:
            Customer_Management_Menu();
        }


        /// <summary>
        /// This method creates a new customer.
        /// </summary>
        public static void New_Customer()
        {
            // Constants:          
            string SUCCESS = "\nSuccessfully created new customer '{0} - {1} {2} {3}' and added to customers list."; // ID, title, FN, LN

            string FAIL_DETAILS = "\n*** WARNING: Failed to create new customer. Please enter valid details. ***\n";
            string FAIL_ID = "\n*** WARNING: Failed to create new customer. Customer is already in customers list. ***\n";
            string FAIL_SAVING = "\n*** WARNING: Failed to create new customer. Could not add to customers list/file. ***\n";            

            int ATTRIBUTE_COUNT = 5;

            // Varibles:
            Customer myCustomer;
            bool addSuccessful;

            // Reset validFields:
            validFields = 0;

            // Print New Customer attributes to screen and read input:
            Console.WriteLine(COMMANDS_DISABLED);
            while (validFields < ATTRIBUTE_COUNT)
            {                
                validFields = CLI_Modifiers.Customer_Attributes_All(ATTRIBUTE_COUNT, FAIL_DETAILS);
            }

            // Generate ID:
            generatedID = CLI_Modifiers.Generate_ID();

            // Create new customer:
            myCustomer = new Customer(generatedID, titleInput, FNInput, LNInput, genderInputConverted, DOBInputConverted);

            // Add customer to csv file:
            try
            {
                addSuccessful = CRM.AddCustomer(myCustomer);

                // Check if ID exists:
                if (addSuccessful)
                {
                    // Print success message to console:
                    Console.WriteLine(SUCCESS, generatedID, titleInput, FNInput, LNInput);                    
                }
                else
                {
                    // Failed to add:
                    Console.WriteLine(FAIL_ID);
                }
            }
            catch
            {
                // Failed to add:
                Console.WriteLine(FAIL_SAVING);
            }

            // Go back to previous menu:
            Console.WriteLine(COMMANDS_ENABLED);
            Customer_Management_Menu();
        }


        /// <summary>
        /// This method modifies an existing customer.
        /// </summary>
        public static void Modify_Customer()
        {
            //Constants:
            string ID_REQUEST = "Please enter the ID number of the customer to modify.\n"
                                + "\nTo escape sequence, please leave field blank.\n";
            string ATTRIBUTE_CHANGING = "Please enter the number assigned to the detail to modify. To modify all details at once enter +.\n";
            string CONTINUE_MODIFYING = "To continue modifying the customer details enter (Y)es. To save and exit enter (N)o.\n";

            string SUCCESS = "\nSuccessfully modified customer '{0} - {1} {2} {3}' and updated customers list.\n";

            string FAIL_ATTRIBUTES = "\n*** WARNING: Failed to modify customer. Please enter valid details. ***\n";
            string FAIL_SAVING = "\n*** WARNING: Failed to modify customer. Could modify customers list/file. ***\n";

            int ATTRIBUTE_COUNT = 5;

            // Variables:
            string previousMenu = "Customer_Management";

            Customer myCustomer = null;
            int customerIndex = 0;

            int IDNumber = -1;
            string IDNumberString = "";
            List<int> IDs = new List<int>();
            bool IDNumberValid = false;

            int modifyLooper = 0;
            string continueModifying;
            bool modified;

            string DOBDate;

            // Reset validFields:
            validFields = 0;

            // Add all existing IDs into IDs list:
            foreach (var customerSaved in customers)
            {
                IDs.Add(customerSaved.CustomerID);
            }

            // Request ID number:
            Console.WriteLine(COMMANDS_DISABLED);
            Console.WriteLine(ID_REQUEST);

            while (!IDNumberValid)
            {
                Console.Write(TYPE_ICON);
                IDNumberString = Console.ReadLine();

                // Check if left blank:
                if (IDNumberString == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Customer_Management_Menu();

                    return;
                }

                // Check validity:
                try
                {
                    IDNumber = Convert.ToInt32(IDNumberString);

                    // Check if ID is in list:
                    if (IDs.Contains(IDNumber))
                    {
                        // Load customer attributes:                        
                        myCustomer = CRM.GetCustomer(IDNumber);

                        // Get index of customer:
                        customerIndex = customers.IndexOf(myCustomer);

                        // Exit loop:
                        IDNumberValid = true;
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
            }

            // Remove the time from the DOB attribute:
            DOBDate = ((myCustomer.CustomerDOB).ToString()).Remove(8);

            // Print attributes list to console:
            Console.WriteLine(COMMANDS_ENABLED);
            Console.WriteLine(ATTRIBUTE_CHANGING);
            Console.WriteLine(CUSTOMER_ATTRIBUTES, myCustomer.CustomerID, myCustomer.CustomerTitle, myCustomer.CustomerFN,
                              myCustomer.CustomerLN, myCustomer.CustomerGender, DOBDate);

            // Check that input is valid, set attribute(s) to modify:            
            while (modifyLooper < 1)
            {
                Console.Write(TYPE_ICON);

                // Read users keypress:
                CLI_Menus.textInput = CLI_Inputs.Check_Key_Press(previousMenu);

                if (CLI_Menus.textInput.ToUpper() == "+")
                {
                    // Loop until all entered fields are valid:
                    Console.WriteLine();
                    Console.WriteLine(COMMANDS_DISABLED);
                    while (validFields < ATTRIBUTE_COUNT)
                    {
                        validFields = CLI_Modifiers.Customer_Attributes_All(ATTRIBUTE_COUNT, FAIL_ATTRIBUTES);
                    }

                    // Create new customer:
                    myCustomer = new Customer(IDNumber, titleInput, FNInput, LNInput, genderInputConverted, DOBInputConverted);

                    // Modify customer list:
                    modified = CRM.ModifyCustomer(myCustomer, customerIndex);

                    if (modified)
                    {
                        // Print success message to console:
                        Console.WriteLine(SUCCESS, IDNumberString, titleInput, FNInput, LNInput);
                    }
                    else
                    {
                        // Print fail message to console:
                        Console.WriteLine(FAIL_SAVING);
                    }

                    // Reset modify checkers:
                    titleModded = false;
                    FNModded = false;
                    LNModded = false;
                    genderModded = false;
                    DOBModded = false;

                    // Change modifyLooper to exit loop:
                    modifyLooper = 1;

                    // Go back to previous menu:
                    Customer_Management_Menu();
                }
                else
                {
                    // Loop until field is valid:
                    while (validFields < 1)
                    {
                        // Edit attribute specified:
                        validFields = CLI_Modifiers.Customer_Attributes_Singular(FAIL_ATTRIBUTES);
                    }

                    // Continue modifying or save:
                    while (modifyLooper < 1)
                    {
                        Console.WriteLine(COMMANDS_ENABLED);
                        Console.WriteLine(CONTINUE_MODIFYING);
                        Console.Write(TYPE_ICON);

                        // Reset modifyLooper:
                        modifyLooper = 0;

                        while (modifyLooper == 0)
                        {
                            // Read input:
                            continueModifying = CLI_Inputs.Check_Key_Press(previousMenu);
                           
                            // Check if input is valid:
                            if (continueModifying.ToUpper() == "Y")
                            {
                                // Reset textInput and validFields:
                                CLI_Menus.textInput = "";
                                validFields = 0;

                                // Check what attributes were modified:
                                // Title:
                                if (!titleModded)
                                {
                                    titleInput = myCustomer.CustomerTitle;
                                }
                                // First name:
                                if (!FNModded)
                                {
                                    FNInput = myCustomer.CustomerFN;
                                }
                                // Last name:
                                if (!LNModded)
                                {
                                    LNInput = myCustomer.CustomerLN;
                                }
                                // Gender:
                                if (!genderModded)
                                {
                                    genderInputConverted = myCustomer.CustomerGender;
                                }
                                // DOB:
                                if (!DOBModded)
                                {
                                    DOBInputConverted = myCustomer.CustomerDOB;
                                }

                                // Remove time from DOB detail:
                                DOBDate = (DOBInputConverted.ToString()).Remove(8);

                                // Print attributes list to console:
                                Console.WriteLine();
                                Console.WriteLine(ATTRIBUTE_CHANGING);
                                Console.WriteLine(CUSTOMER_ATTRIBUTES, IDNumber, titleInput, FNInput,
                                                    LNInput, genderInputConverted, DOBDate);
                                Console.Write(TYPE_ICON);

                                // Read users keypress:
                                textInput = CLI_Inputs.Check_Key_Press(previousMenu);

                                // Loop until field is valid:
                                while (validFields < 1)
                                {
                                    // Edit attribute specified:
                                    validFields = CLI_Modifiers.Customer_Attributes_Singular(FAIL_ATTRIBUTES);
                                }

                                // Exit loop to ask if continue or exit:
                                modifyLooper = -1;
                            }
                            else if (continueModifying.ToUpper() == "N")
                            {
                                // Check what attributes were modified:
                                // Title:
                                if (!titleModded)
                                {
                                    titleInput = myCustomer.CustomerTitle;
                                }
                                // First name:
                                if (!FNModded)
                                {
                                    FNInput = myCustomer.CustomerFN;
                                }
                                // Last name:
                                if (!LNModded)
                                {
                                    LNInput = myCustomer.CustomerLN;
                                }
                                // Gender:
                                if (!genderModded)
                                {
                                    genderInputConverted = myCustomer.CustomerGender;
                                }
                                // DOB:
                                if (!DOBModded)
                                {
                                    DOBInputConverted = myCustomer.CustomerDOB;
                                }

                                // Create new customer:
                                myCustomer = new Customer(IDNumber, titleInput, FNInput, LNInput, genderInputConverted, DOBInputConverted);

                                // Modify customer list:
                                modified = CRM.ModifyCustomer(myCustomer, customerIndex);

                                if (modified)
                                {
                                    // Print success message to console:
                                    Console.WriteLine(SUCCESS, IDNumberString, titleInput, FNInput, LNInput);
                                }
                                else
                                {
                                    // Print fail message to console:
                                    Console.WriteLine(FAIL_SAVING);
                                }

                                // Reset modify checkers:
                                titleModded = false;
                                FNModded = false;
                                LNModded = false;
                                genderModded = false;
                                DOBModded = false;

                                // Change modifyLooper to exit loop:
                                modifyLooper = 1;

                                // Go back to previous menu:
                                Customer_Management_Menu();
                            }
                            else
                            {
                                Console.WriteLine("\n*** WARNING: Input not valid. Please enter either Y or N. ***\n");
                                Console.Write(TYPE_ICON);
                            }
                        }
                    }
                }
            }                    
        }


        /// <summary>
        /// This method deletes an existing customer.
        /// </summary>
        public static void Delete_Customer()
        {
            //Constants:
            string ID_REQUEST = "Please enter the ID number of the customer to delete.\n"
                                + "\nTo escape sequence, please leave field blank.\n";
            string DELETE_REQUEST = "To confirm deletion enter (Y)es. To go back enter (N)o.\n";

            string SUCCESS = "\nSuccessfully deleted customer '{0} - {1} {2} {3}' and removed from customers list.\n"; // ID, title, FN, LN

            string FAIL_DELETING = "\n*** WARNING: Failed to delete customer. Customer is renting a vehicle. ***\n";

            // Variables:
            string previousMenu = "Customer_Management";

            bool deletionCompleted = false;
            bool deleted;
            string confirmation;
            bool confirmationLooper;

            Customer myCustomer = null;
            int customerIndex;

            int IDNumber = -1;
            string IDNumberString;
            List<int> IDs = new List<int>();
            bool IDNumberValid;

            string DOBDate;

            // Add all existing IDs into IDs list:
            foreach (var customerSaved in customers)
            {
                IDs.Add(customerSaved.CustomerID);
            }

            // Loop until deletion is completed:
            while (!deletionCompleted)
            {
                // Reset confirmationLooper and IDNumberValid:
                confirmationLooper = false;
                IDNumberValid = false;

                // Request ID number:
                Console.WriteLine(COMMANDS_DISABLED);
                Console.WriteLine(ID_REQUEST);

                while (!IDNumberValid)
                {
                    Console.Write(TYPE_ICON);
                    IDNumberString = Console.ReadLine();

                    // Check if left blank:
                    if (IDNumberString == "")
                    {
                        // Print exit message:
                        Console.WriteLine(BLANK_FIELDS);
                        Console.WriteLine(COMMANDS_ENABLED);

                        // Previous menu:
                        CLI_Menus.Customer_Management_Menu();

                        return;
                    }

                    // Check validity:
                    try
                    {
                        IDNumber = Convert.ToInt32(IDNumberString);

                        // Check ID is in list:
                        if (IDs.Contains(IDNumber))
                        {
                            // Load customer attributes:                            
                            myCustomer = CRM.GetCustomer(IDNumber);

                            // Get index of customer:
                            customerIndex = customers.IndexOf(myCustomer);

                            // Exit loop:
                            IDNumberValid = true;
                        }
                        else
                        {
                            Console.WriteLine("\n*** WARNING: The ID number could not be found. Please enter a valid ID number. ***\n");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("\n*** WARNING: ID not valid. Please enter valid ID number. ***\n");
                    }
                }

                // Remove the time from the DOB attribute:
                DOBDate = ((myCustomer.CustomerDOB).ToString()).Remove(8);

                // Print attribute list to console:
                Console.WriteLine(COMMANDS_ENABLED);
                Console.WriteLine("Customer Details:\n");
                Console.WriteLine(CUSTOMER_ATTRIBUTES, myCustomer.CustomerID, myCustomer.CustomerTitle, myCustomer.CustomerFN,
                                  myCustomer.CustomerLN, myCustomer.CustomerGender, DOBDate);

                // Confirm deletion:
                Console.WriteLine(DELETE_REQUEST);
                while (!confirmationLooper)
                {
                    Console.Write(TYPE_ICON);
                    confirmation = CLI_Inputs.Check_Key_Press(previousMenu);

                    if (confirmation.ToUpper() == "Y")
                    {
                        // Delete customer:
                        deleted = CRM.RemoveCustomer(IDNumber);

                        if (deleted)
                        {
                            // Print success message:
                            Console.WriteLine(SUCCESS, myCustomer.CustomerID, myCustomer.CustomerTitle, myCustomer.CustomerFN, myCustomer.CustomerLN);
                        }
                        else
                        {
                            // Customer is renting, print fail message:
                            Console.WriteLine(FAIL_DELETING);
                        }

                        // Exit loop:
                        confirmationLooper = true;
                        deletionCompleted = true;
                    }
                    else if (confirmation.ToUpper() == "N")
                    {
                        Console.WriteLine();

                        // Go back to start:                        
                        confirmationLooper = true;
                    }
                    else
                    {
                        Console.WriteLine("\n*** WARNING: Input not valid. Please enter either Y or N. ***\n");
                    }
                }
            }

            // Go back to previous menu:
            Customer_Management_Menu();
        }


        /// <summary>
        /// This method prints the fleet management menu text to the console and switches the menu on valid input.
        /// </summary>
        public static void Fleet_Management_Menu()
        {
            // Variables:
            string previousMenu = "Main_Menu";
            int options = 4;

            // Reset inputValid and textInput:
            inputValid = false;
            textInput = "";

            // Make menu:
            Make_Menu(FLEET_MENU, options);

            // Read input:
            textInput = CLI_Inputs.Check_Key_Press(previousMenu);

            // Check if user input is vaild and change to menu:
            while (!inputValid)
            {
                if (textInput.ToUpper() == "A")
                {
                    Console.WriteLine();
                    Display_Fleet();
                    inputValid = true;
                }
                else if (textInput.ToUpper() == "B")
                {
                    Console.WriteLine();
                    New_Vehicle();
                    inputValid = true;
                }
                else if (textInput.ToUpper() == "C")
                {
                    Console.WriteLine();
                    Modify_Vehicle();
                    inputValid = true;
                }
                else if (textInput.ToUpper() == "D")
                {
                    Console.WriteLine();
                    Delete_Vehicle();
                    inputValid = true;
                }
                else
                {
                    // Reset textInput:
                    textInput = "";

                    // Ask to enter valid letter:
                    Console.WriteLine();
                    Console.WriteLine(VALIDITY_CATCH);
                    Console.Write(TYPE_ICON);

                    // Check input:
                    textInput = CLI_Inputs.Check_Key_Press(previousMenu);
                }
            }            
        }


        /// <summary>
        /// This method prints the vehicles table to the console.
        /// </summary>
        public static void Display_Fleet()
        {            
            // Refresh vehicles list:
            vehicles = Fleet.GetFleet();

            if (vehicles.Count != 0)
            {
                // Print table:
                CLI_Tables.Create_Vehicles_Table(vehicles);
            }
            else
            {
                Console.WriteLine("There are no vehicles to display.\n");
            }

            // Go back to previous menu:
            Fleet_Management_Menu();
        }


        /// <summary>
        /// This method creates a new vehicle.
        /// </summary>
        public static void New_Vehicle()
        {
            // Constants:      
            string OPTIONAL_FIELDS = "a) Fill all fields\n" +
                                     "b) Only fill required fields\n";

            string SUCCESS = "\nSuccessfully created new vehicle '{0} - {1} {2} {3}' and added to vehicle list."; // rego, year, make, model

            string FAIL_DETAILS = "\nFailed to create new vehicle. Please enter valid details.\n";
            string FAIL_REGO = "\nFailed to create new vehicle. Vehicle is already in vehicle list.\n";
            string FAIL_SAVING = "\nFailed to create new vehicle. Could not add to vehicle list.\n";            

            int ATTRIBUTE_COUNT = 12;
            int REQUIRED_ATTRIBUTE_COUNT = 5;

            // Variables:
            string previousMenu = "Fleet_Management";

            Vehicle myVehicle;
            bool addSuccessful;
            int options = 2;

            // Reset inputValid, textInput, and validFields:
            inputValid = false;
            textInput = "";
            validFields = 0;

            // Ask user to fill either all fields or only required fields:
            Make_Menu(OPTIONAL_FIELDS, options);

            // Read input:
            textInput = CLI_Inputs.Check_Key_Press(previousMenu);

            // Print either all or only required fields to input:
            while (!inputValid)
            {
                if (textInput.ToUpper() == "A")
                {
                    Console.WriteLine();

                    // Print New Vehice attributes to screen and read input:
                    Console.WriteLine(COMMANDS_DISABLED);
                    while (validFields < ATTRIBUTE_COUNT)
                    {
                        validFields = CLI_Modifiers.Vehicle_Attributes_All(ATTRIBUTE_COUNT, FAIL_DETAILS);
                    }

                    // Create new vehicle:
                    myVehicle = new Vehicle(regoInput, gradeInputConverted, makeInput, modelInput,
                                            yearInputConverted, seatsInputConverted, transmissionInputConverted, fuelInputConverted,
                                            GPSInputConverted, sunroofInputConverted, rateInputConverted, colourInput);

                    // Add vehicle to csv file:
                    try
                    {
                        addSuccessful = Fleet.AddVehicle(myVehicle);

                        // Check if registration exists:
                        if (addSuccessful)
                        {
                            // Print success message to console:
                            Console.WriteLine(SUCCESS, regoInput, makeInput, modelInput, yearInput);                            
                        }
                        else
                        {
                            // Failed to add:
                            Console.WriteLine(FAIL_REGO);
                        }
                    }
                    catch
                    {
                        // Failed to add:
                        Console.WriteLine(FAIL_SAVING);
                    }

                    // Exit loop:
                    inputValid = true;
                }
                else if (textInput.ToUpper() == "B")
                {
                    Console.WriteLine();

                    // Print New Vehice attributes to screen and read input:
                    Console.WriteLine(COMMANDS_DISABLED);
                    while (validFields < REQUIRED_ATTRIBUTE_COUNT)
                    {
                        validFields = CLI_Modifiers.Vehicle_Attributes_Required(REQUIRED_ATTRIBUTE_COUNT, FAIL_DETAILS);
                    }

                    // Create new vehicle:
                    myVehicle = new Vehicle(regoInput, gradeInputConverted, makeInput, modelInput, yearInputConverted);

                    // Add vehicle to csv file:
                    try
                    {
                        addSuccessful = Fleet.AddVehicle(myVehicle);

                        // Check if registration exists:
                        if (addSuccessful)
                        {
                            // Print success message to console:
                            Console.WriteLine(SUCCESS, regoInput, makeInput, modelInput, yearInput);                            
                        }
                        else
                        {
                            // Failed to add:
                            Console.WriteLine(FAIL_REGO);
                        }
                    }
                    catch
                    {
                        // Failed to add:
                        Console.WriteLine(FAIL_SAVING);
                    }

                    // Exit loop:
                    inputValid = true;
                }
                else
                {
                    // Ask to enter valid input:
                    Console.WriteLine();
                    Console.WriteLine(VALIDITY_CATCH);
                    Console.Write(TYPE_ICON);

                    // Check input:
                    textInput = CLI_Inputs.Check_Key_Press(previousMenu);
                }
            }

            // Go back to previous menu:
            Console.WriteLine(COMMANDS_ENABLED);
            Fleet_Management_Menu();
        }


        /// <summary>
        /// This method modifies an existing vehicle.
        /// </summary>
        public static void Modify_Vehicle()
        {
            //Constants:
            string REGO_REQUEST = "Please enter the registration number of the vehicle to modify.\n"
                                  + "\nTo escape sequence, please leave field blank.\n";
            string ATTRIBUTE_CHANGING = "Please enter the number assigned to the detail to modify.\n" +
                                     "\tOR\n" +
                                     "To modify all details at once enter +. To modify only all required details at once enter *.\n";
            string CONTINUE_MODIFYING = "To continue modifying the vehicle details enter (Y)es. To save and exit enter (N)o.\n";

            string SUCCESS = "\nSuccessfully modified vehicle '{0} - {1} {2} {3}' and updated vehicles list."; //rego, make, model, year

            string FAIL_ATTRIBUTES = "\n*** WARNING: Failed to modify vehicle. Please enter valid details. ***\n";
            string FAIL_SAVING = "\n*** WARNING: Failed to modify vehicle. Could modify vehicles list/file. ***\n";

            int ATTRIBUTE_COUNT = 12;
            int REQUIRED_ATTRIBUTE_COUNT = 5;

            int REGO_LENGTH = 6;

            // Variables:
            string previousMenu = "Fleet_Management";

            Vehicle myVehicle = null;
            int vehicleIndex = 0;

            string regoNumber = "";
            List<string> regos = new List<string>();
            bool regoNumberValid = false;

            int modifyLooper = 0;
            string continueModifying;
            bool modified;
            bool rentalsModified;

            // Reset inputValid and validFields:
            inputValid = false;
            validFields = 0;

            // Add all existing registrations into regos list:
            foreach (var vehicleSaved in vehicles)
            {
                regos.Add(vehicleSaved.VehicleRego);
            }

            // Request registration number:
            Console.WriteLine(COMMANDS_DISABLED);
            Console.WriteLine(REGO_REQUEST);

            while (!regoNumberValid)
            {
                Console.Write(TYPE_ICON);
                regoNumber = Console.ReadLine();

                // Check if left blank:
                if (regoNumber == "")
                {
                    // Print exit message:
                    Console.WriteLine(BLANK_FIELDS);
                    Console.WriteLine(COMMANDS_ENABLED);

                    // Previous menu:
                    CLI_Menus.Fleet_Management_Menu();

                    return;
                }

                // Check validity:
                if (regoNumber.Length == REGO_LENGTH && (regoNumber.Remove(0, 3)).All(Char.IsLetter))
                {
                    try
                    {
                        Convert.ToInt32(regoNumber.Remove(3));

                        // Check registration is in list:
                        if (regos.Contains(regoNumber))
                        {
                            // Load vehicle attributes:                           
                            myVehicle = Fleet.GetVehicle(regoNumber);

                            // Get index of vehicle:
                            vehicleIndex = vehicles.IndexOf(myVehicle);

                            // Exit loop:
                            regoNumberValid = true;
                        }
                        else
                        {
                            Console.WriteLine("\n ** WARNING: The registration number could not be found. Please enter a valid registration number. ***\n");
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

            // Print attributes list to console:
            Console.WriteLine(COMMANDS_ENABLED);
            Console.WriteLine(ATTRIBUTE_CHANGING);
            Console.WriteLine(VEHICLE_ATTRIBUTES, myVehicle.VehicleRego, myVehicle.VehicleGrade, myVehicle.VehicleMake, myVehicle.VehicleModel,
                              myVehicle.VehicleYear, myVehicle.VehicleSeats, myVehicle.VehicleTransmission, myVehicle.VehicleFuel,
                              myVehicle.VehicleGPS, myVehicle.VehicleSunroof, myVehicle.VehicleRate, myVehicle.VehicleColour);

            // Check that input is valid, set attribute(s) to modify:            
            while (modifyLooper < 1)
            {
                Console.Write(TYPE_ICON);

                // Read users keypress:
                CLI_Menus.textInput = CLI_Inputs.Check_Key_Press(previousMenu);

                if (CLI_Menus.textInput.ToUpper() == "+")
                {
                    // Loop until all entered fields are valid:
                    Console.WriteLine();
                    Console.WriteLine(COMMANDS_DISABLED);
                    while (validFields < ATTRIBUTE_COUNT)
                    {
                        validFields = CLI_Modifiers.Vehicle_Attributes_All(ATTRIBUTE_COUNT, FAIL_ATTRIBUTES);
                    }

                    // Create new vehicle:
                    myVehicle = new Vehicle(regoInput, gradeInputConverted, makeInput, modelInput, yearInputConverted,
                                             seatsInputConverted, transmissionInputConverted, fuelInputConverted, GPSInputConverted,
                                             sunroofInputConverted, rateInputConverted, colourInput);

                    // Modify vehicles list:
                    modified = Fleet.ModifyVehicle(myVehicle, vehicleIndex);

                    // Modify rental dictionary if registration was modified:
                    if (regoModded)
                    {
                        rentalsModified = Fleet.ModifyRentals(regoNumber, regoInput);

                        // Save files:
                        if (modified)
                        {
                            // Print success message to console:
                            Console.WriteLine(SUCCESS, regoNumber, makeInput, modelInput, yearInput);
                        }
                        else
                        {
                            // Print fail message to console:
                            Console.WriteLine(FAIL_SAVING);
                        }
                    }
                    else
                    {
                        // Save vehicle file:
                        if (modified)
                        {
                            // Print success message to console:
                            Console.WriteLine(SUCCESS, regoNumber, makeInput, modelInput, yearInput);
                        }
                        else
                        {
                            // Print fail message to console:
                            Console.WriteLine(FAIL_SAVING);
                        }
                    }

                    // Reset modify checkers:
                    regoModded = false;
                    gradeModded = false;
                    makeModded = false;
                    modelModded = false;
                    yearModded = false;
                    seatsModded = false;
                    transmissionModded = false;
                    fuelModded = false;
                    GPSModded = false;
                    sunroofModded = false;
                    rateModded = false;
                    colourModded = false;

                    // Change modifyLooper to exit loop:
                    modifyLooper = 1;

                    // Go back to previous menu:
                    Console.WriteLine(COMMANDS_ENABLED);
                    Fleet_Management_Menu();
                }
                else if (CLI_Menus.textInput.ToUpper() == "*")
                {
                    // Loop until all entered fields are valid:
                    Console.WriteLine();
                    Console.WriteLine(COMMANDS_DISABLED);
                    while (validFields < REQUIRED_ATTRIBUTE_COUNT)
                    {
                        validFields = CLI_Modifiers.Vehicle_Attributes_Required(REQUIRED_ATTRIBUTE_COUNT, FAIL_ATTRIBUTES);
                    }

                    // Set optional attributes to their original values:
                    seatsInputConverted = myVehicle.VehicleSeats;
                    transmissionInputConverted = myVehicle.VehicleTransmission;
                    fuelInputConverted = myVehicle.VehicleFuel;
                    GPSInputConverted = myVehicle.VehicleGPS;
                    sunroofInputConverted = myVehicle.VehicleSunroof;
                    rateInputConverted = myVehicle.VehicleRate;
                    colourInput = myVehicle.VehicleColour;

                    // Create new vehicle:
                    myVehicle = new Vehicle(regoInput, gradeInputConverted, makeInput, modelInput, yearInputConverted,
                                             seatsInputConverted, transmissionInputConverted, fuelInputConverted, GPSInputConverted,
                                             sunroofInputConverted, rateInputConverted, colourInput);

                    // Modify vehicles list:
                    modified = Fleet.ModifyVehicle(myVehicle, vehicleIndex);

                    // Modify rental dictionary if registration was modified:
                    if (regoModded)
                    {
                        rentalsModified = Fleet.ModifyRentals(regoNumber, regoInput);

                        // Save files:
                        if (modified)
                        {
                            // Print success message to console:
                            Console.WriteLine(SUCCESS, regoNumber, makeInput, modelInput, yearInput);
                        }
                        else
                        {
                            // Print fail message to console:
                            Console.WriteLine(FAIL_SAVING);
                        }
                    }
                    else
                    {
                        // Save vehicle file:
                        if (modified)
                        {
                            // Print success message to console:
                            Console.WriteLine(SUCCESS, regoNumber, makeInput, modelInput, yearInput);
                        }
                        else
                        {
                            // Print fail message to console:
                            Console.WriteLine(FAIL_SAVING);
                        }
                    }

                    // Reset modify checkers:
                    regoModded = false;
                    gradeModded = false;
                    makeModded = false;
                    modelModded = false;
                    yearModded = false;
                    seatsModded = false;
                    transmissionModded = false;
                    fuelModded = false;
                    GPSModded = false;
                    sunroofModded = false;
                    rateModded = false;
                    colourModded = false;

                    // Change modifyLooper to exit loop:
                    modifyLooper = 1;

                    // Go back to previous menu:
                    Console.WriteLine(COMMANDS_ENABLED);
                    Fleet_Management_Menu();
                }
                else
                {
                    // Loop until field is valid:
                    while (validFields < 1)
                    {
                        // Edit attribute specified:
                        validFields = CLI_Modifiers.Vehicle_Attributes_Singular(FAIL_ATTRIBUTES);
                    }

                    // Continue modifying or save:
                    while (modifyLooper < 1)
                    {
                        Console.WriteLine(COMMANDS_ENABLED);
                        Console.WriteLine(CONTINUE_MODIFYING);
                        Console.Write(TYPE_ICON);

                        // Reset modifyLooper:
                        modifyLooper = 0;

                        while (modifyLooper == 0)
                        {
                            // Read users keypress:
                            continueModifying = CLI_Inputs.Check_Key_Press(previousMenu);

                            // Check input is valid:
                            if (continueModifying.ToUpper() == "Y")
                            {
                                // Reset textInput and validFields:
                                textInput = "";
                                validFields = 0;

                                // Check what attributes were modified:
                                // Registration:
                                if (!regoModded)
                                {
                                    regoInput = myVehicle.VehicleRego;
                                }
                                // Grade:
                                if (!gradeModded)
                                {
                                    gradeInputConverted = myVehicle.VehicleGrade;
                                }
                                // Make:
                                if (!makeModded)
                                {
                                    makeInput = myVehicle.VehicleMake;
                                }
                                // Model:
                                if (!modelModded)
                                {
                                    modelInput = myVehicle.VehicleModel;
                                }
                                // Year:
                                if (!yearModded)
                                {
                                    yearInputConverted = myVehicle.VehicleYear;
                                }
                                // Seats:
                                if (!seatsModded)
                                {
                                    seatsInputConverted = myVehicle.VehicleSeats;
                                }
                                // Transmission:
                                if (!transmissionModded)
                                {
                                    transmissionInputConverted = myVehicle.VehicleTransmission;
                                }
                                // Fuel:
                                if (!fuelModded)
                                {
                                    fuelInputConverted = myVehicle.VehicleFuel;
                                }
                                // GPS:
                                if (!GPSModded)
                                {
                                    GPSInputConverted = myVehicle.VehicleGPS;
                                }
                                // Sunroof:
                                if (!sunroofModded)
                                {
                                    sunroofInputConverted = myVehicle.VehicleSunroof;
                                }
                                // Daily rate:
                                if (!rateModded)
                                {
                                    rateInputConverted = myVehicle.VehicleRate;
                                }
                                // Colour:
                                if (!colourModded)
                                {
                                    colourInput = myVehicle.VehicleColour;
                                }

                                // Print attributes list to console:
                                Console.WriteLine();
                                Console.WriteLine(ATTRIBUTE_CHANGING);
                                Console.WriteLine(VEHICLE_ATTRIBUTES, regoInput, gradeInputConverted, makeInput, modelInput,
                                                    yearInputConverted, seatsInputConverted, transmissionInputConverted, fuelInputConverted,
                                                    GPSInputConverted, sunroofInputConverted, rateInputConverted, colourInput);
                                Console.Write(TYPE_ICON);

                                // Read users keypress:
                                textInput = CLI_Inputs.Check_Key_Press(previousMenu);

                                // Loop until field is valid:
                                while (validFields < 1)
                                {
                                    // Edit attribute specified:
                                    validFields = CLI_Modifiers.Vehicle_Attributes_Singular(FAIL_ATTRIBUTES);
                                }

                                // Exit loop to ask if continue or exit:
                                modifyLooper = -1;
                            }
                            else if (continueModifying.ToUpper() == "N")
                            {
                                // Check what attributes were modified:
                                // Registration:
                                if (!regoModded)
                                {
                                    regoInput = myVehicle.VehicleRego;
                                }
                                // Grade:
                                if (!gradeModded)
                                {
                                    gradeInputConverted = myVehicle.VehicleGrade;
                                }
                                // Make:
                                if (!makeModded)
                                {
                                    makeInput = myVehicle.VehicleMake;
                                }
                                // Model:
                                if (!modelModded)
                                {
                                    modelInput = myVehicle.VehicleModel;
                                }
                                // Year:
                                if (!yearModded)
                                {
                                    yearInputConverted = myVehicle.VehicleYear;
                                }
                                // Seats:
                                if (!seatsModded)
                                {
                                    seatsInputConverted = myVehicle.VehicleSeats;
                                }
                                // Transmission:
                                if (!transmissionModded)
                                {
                                    transmissionInputConverted = myVehicle.VehicleTransmission;
                                }
                                // Fuel:
                                if (!fuelModded)
                                {
                                    fuelInputConverted = myVehicle.VehicleFuel;
                                }
                                // GPS:
                                if (!GPSModded)
                                {
                                    GPSInputConverted = myVehicle.VehicleGPS;
                                }
                                // Sunroof:
                                if (!sunroofModded)
                                {
                                    sunroofInputConverted = myVehicle.VehicleSunroof;
                                }
                                // Daily rate:
                                if (!rateModded)
                                {
                                    rateInputConverted = myVehicle.VehicleRate;
                                }
                                // Colour:
                                if (!colourModded)
                                {
                                    colourInput = myVehicle.VehicleColour;
                                }

                                // Create new vehicle:
                                myVehicle = new Vehicle(regoInput, gradeInputConverted, makeInput, modelInput, yearInputConverted,
                                                            seatsInputConverted, transmissionInputConverted, fuelInputConverted, GPSInputConverted,
                                                            sunroofInputConverted, rateInputConverted, colourInput);

                                // Modify vehicles list:
                                modified = Fleet.ModifyVehicle(myVehicle, vehicleIndex);

                                // Modify rental dictionary if registration was modified:
                                if (regoModded)
                                {
                                    rentalsModified = Fleet.ModifyRentals(regoNumber, regoInput);

                                    // Save files:
                                    if (modified)
                                    {
                                        // Print success message to console:
                                        Console.WriteLine(SUCCESS, regoNumber, makeInput, modelInput, yearInput);
                                        Console.WriteLine();
                                    }
                                    else
                                    {
                                        // Print fail message to console:
                                        Console.WriteLine(FAIL_SAVING);
                                    }
                                }
                                else
                                {
                                    // Save vehicle file:
                                    if (modified)
                                    {
                                        // Print success message to console:
                                        Console.WriteLine(SUCCESS, regoNumber, makeInput, modelInput, yearInput);
                                        Console.WriteLine();
                                    }
                                    else
                                    {
                                        // Print fail message to console:
                                        Console.WriteLine(FAIL_SAVING);
                                    }
                                }

                                // Reset modify checkers:
                                regoModded = false;
                                gradeModded = false;
                                makeModded = false;
                                modelModded = false;
                                yearModded = false;
                                seatsModded = false;
                                transmissionModded = false;
                                fuelModded = false;
                                GPSModded = false;
                                sunroofModded = false;
                                rateModded = false;
                                colourModded = false;

                                // Change modifyLooper to exit loop:
                                modifyLooper = 1;

                                // Go back to previous menu:
                                Fleet_Management_Menu();
                            }
                            else
                            {
                                Console.WriteLine("\n*** WARNING: Input not valid. Please enter either Y or N. ***\n");
                                Console.Write(TYPE_ICON);
                            }
                        }
                    }                       
                }
            }
        }


        /// <summary>
        /// This method deletes an existing vehicle.
        /// </summary>
        public static void Delete_Vehicle()
        {
            //Constants:
            string REGO_REQUEST = "Please enter the registration number of the vehicle to delete.\n"
                                  + "\nTo escape sequence, please leave field blank.\n";
            string DELETE_REQUEST = "To confirm deletion enter (Y)es. To go back enter (N)o.\n";

            string SUCCESS = "\nSuccessfully deleted vehicle '{0} - {1} {2} {3}' and removed from vehicles list.\n";

            string FAIL_DELETING = "\nFailed to delete vehicle. Vehicle is being rented by a customer.\n";

            int REGO_LENGTH = 6;

            // Variables:
            string previousMenu = "Fleet_Management";
            bool deletionCompleted = false;
            bool deleted;
            string confirmation;
            bool confirmationLooper = false;

            Vehicle myVehicle = null;
            int vehicleIndex = 0;

            string regoNumber = "";
            List<string> regos = new List<string>();
            bool regoNumberValid = false;

            // Add all existing registrations into regos list:
            foreach (var vehicleSaved in vehicles)
            {
                regos.Add(vehicleSaved.VehicleRego);
            }

            // Loop until deletion is completed:
            while (!deletionCompleted)
            {
                // Reset confirmationLooper and regoNumberValid:
                confirmationLooper = false;
                regoNumberValid = false;

                // Request registration number:
                Console.WriteLine(COMMANDS_DISABLED);
                Console.WriteLine(REGO_REQUEST);

                while (!regoNumberValid)
                {
                    Console.Write(TYPE_ICON);
                    regoNumber = Console.ReadLine();

                    // Check if left blank:
                    if (regoNumber == "")
                    {
                        // Print exit message:
                        Console.WriteLine(BLANK_FIELDS);
                        Console.WriteLine(COMMANDS_ENABLED);

                        // Previous menu:
                        CLI_Menus.Fleet_Management_Menu();

                        return;
                    }

                    // Check validity:
                    if (regoNumber.Length == REGO_LENGTH && (regoNumber.Remove(0, 3)).All(Char.IsLetter))
                    {
                        try
                        {
                            Convert.ToInt32(regoNumber.Remove(3));

                            // Check registration is in list:
                            if (regos.Contains(regoNumber))
                            {
                                // Load vehicle details:                            
                                myVehicle = Fleet.GetVehicle(regoNumber);

                                // Get index of vehicle:
                                vehicleIndex = vehicles.IndexOf(myVehicle);

                                // Exit loop:
                                regoNumberValid = true;
                            }
                            else
                            {
                                Console.WriteLine("\n*** WARNING: The registration number could not be found. Please enter a valid registration number. ***\n");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("\n*** WARNING: Registration number not valid. Please enter valid registration number. ***\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n*** WARNING: Registration number not valid. Please enter valid registration number. ***\n");
                    }
                }

                // Print attributes list to console:
                Console.WriteLine(COMMANDS_ENABLED);
                Console.WriteLine("Vehicle Details:\n");
                Console.WriteLine(VEHICLE_ATTRIBUTES, myVehicle.VehicleRego, myVehicle.VehicleGrade, myVehicle.VehicleMake, myVehicle.VehicleModel,
                                  myVehicle.VehicleYear, myVehicle.VehicleSeats, myVehicle.VehicleTransmission, myVehicle.VehicleFuel,
                                  myVehicle.VehicleGPS, myVehicle.VehicleSunroof, myVehicle.VehicleRate, myVehicle.VehicleColour);

                // Confirm deletion:
                Console.WriteLine(DELETE_REQUEST);
                while (!confirmationLooper)
                {
                    Console.Write(TYPE_ICON);
                    confirmation = CLI_Inputs.Check_Key_Press(previousMenu);

                    if (confirmation.ToUpper() == "Y")
                    {
                        // Delete vehicle:
                        deleted = Fleet.RemoveVehicle(regoNumber);

                        if (deleted)
                        {
                            // Print success message:
                            Console.WriteLine(SUCCESS, myVehicle.VehicleRego, myVehicle.VehicleMake, myVehicle.VehicleModel, myVehicle.VehicleYear);
                        }
                        else
                        {
                            // Vehicle is being rented, print fail message:
                            Console.WriteLine(FAIL_DELETING);
                        }

                        // Exit loop:
                        confirmationLooper = true;
                        deletionCompleted = true;
                    }
                    else if (confirmation.ToUpper() == "N")
                    {
                        Console.WriteLine();

                        // Go back to start:                        
                        confirmationLooper = true;
                    }
                    else
                    {
                        Console.WriteLine("\n*** WARNING: Input not valid. Please enter either Y or N. ***\n");
                    }
                }
            }

            // Go back to previous menu:
            Fleet_Management_Menu();
        }


        /// <summary>
        /// This method prints the rental management menu text to the console and switches the menu on valid input.
        /// </summary>
        public static void Rental_Management_Menu()
        {
            // Variables:
            string previousMenu = "Main_Menu";
            int options = 4;

            // Reset inputValid and textInput:
            inputValid = false;
            textInput = "";

            // Make menu:
            Make_Menu(RENTAL_MENU, options);

            // Read input:
            textInput = CLI_Inputs.Check_Key_Press(previousMenu);

            // Check if user input is vaild and change to menu:
            while (!inputValid)
            {
                if (textInput.ToUpper() == "A")
                {
                    Console.WriteLine();
                    Display_Rentals();
                    inputValid = true;
                }
                else if (textInput.ToUpper() == "B")
                {
                    Console.WriteLine();
                    Search_Vehicles();
                    inputValid = true;
                }
                else if (textInput.ToUpper() == "C")
                {
                    Console.WriteLine();
                    Rent_Vehicle();
                    inputValid = true;
                }
                else if (textInput.ToUpper() == "D")
                {
                    Console.WriteLine();
                    Return_Vehicle();
                    inputValid = true;
                }
                else
                {
                    // Reset textInput:
                    textInput = "";

                    // Ask to enter valid letter:
                    Console.WriteLine();
                    Console.WriteLine(VALIDITY_CATCH);
                    Console.Write(TYPE_ICON);

                    // Check input:
                    textInput = CLI_Inputs.Check_Key_Press(previousMenu);
                }
            }            
        }


        /// <summary>
        /// This method prints the rentals table and daily rate of vehicles to the console.
        /// </summary>
        public static void Display_Rentals()
        {
            // Refresh rentals dictionary:
            rentals = Fleet.rentals;

            if (rentals.Count != 0)
            {
                // Print table:
                CLI_Tables.Create_Rentals_Table();
            }
            else
            {
                Console.WriteLine("There are no rentals to display.\n");
            }            

            // Go back to previous menu:
            Rental_Management_Menu();
        }


        /// <summary>
        /// This method searches the list of unrented vehicles and print the results in a table to the console.
        /// It returns to the previous menu after each search.
        /// </summary>
        public static void Search_Vehicles()
        {
            // Constants:
            string SEARCH_REQUEST = "To search for a vehicle please type in the attribute you want to search for.\n\n"
                                    + "To search for more than one attribute please use only the AND, OR, or () operators.\n\n"
                                    + "If the searched attribute has spaces please put quotations (\") around it.\n";
            string SEARCH = "Search vehicles >>> ";

            // Variables:
            string searchQuery;
            bool validSearch = false;

            // Request search:
            Console.WriteLine(COMMANDS_DISABLED);
            Console.WriteLine(SEARCH_REQUEST);

            // Loop until valid search:
            while (!validSearch)
            {
                // Read searchQuery:
                Console.Write(SEARCH);
                searchQuery = Console.ReadLine();

                // Get searched vehicles:                
                validSearch = CLI_Search.Query_Search(searchQuery);
            }

            // Go back to previous menu:
            CLI_Menus.Rental_Management_Menu();
        }


        /// <summary>
        /// This method rents a specified vehicle to a specified customer and saves the new rentl to the rentals csv file,
        /// given that the vehicle is not rented and customer is not rented.
        /// </summary>
        public static void Rent_Vehicle()
        {
            // Constants:
            string TOTAL_COST = "\nThe total cost of this rental  will be ${0:0.00}. Please enter (Y)es to proceed or (N)o to go back.\n";

            string SUCCESS = "\nSuccessfully rented vehicle '{0}' to customer '{1}' and added to rentals list.\n"; // Rego, ID
          
            string FAIL_SAVING = "\n*** WARNING: Failed to rent vehicle. Could not add to rentals list/file. ***\n";

            int ATTRIBUTE_COUNT = 3;

            // Variables:
            string previousMenu = "Rental_Management";

            Vehicle myVehicle;
            double totalCost = 0;

            bool rentalAdded;
            bool addSuccessful;
            bool confirmationLooper;
            string confirmation;

            // Reset inputValid, textInput, and validFields:
            inputValid = false;
            textInput = "";
            validFields = 0;

            // Reset rentalAdded:
            rentalAdded = false;

            // Loop sequence:
            while (!rentalAdded)
            {
                // Reset confirmationLooper and validFields:
                confirmationLooper = false;
                validFields = 0;

                // Print new rental attributes to screen and read input:
                Console.WriteLine(COMMANDS_DISABLED);
                while (validFields < ATTRIBUTE_COUNT)
                {
                    validFields = CLI_Modifiers.Rental_Attributes_Rent();
                }

                // Get vehicle:
                myVehicle = Fleet.GetVehicle(regoInput);

                // Check if returned a vehicle:
                if (myVehicle != null)
                {
                    // Calculate total cost:
                    totalCost = myVehicle.VehicleRate * rentalTimeInputConverted;
                }
                else
                {
                    Console.WriteLine("Could not find vehicle.");
                }

                // Print total cost confirmation to console:
                Console.WriteLine(TOTAL_COST, totalCost);
                while (!confirmationLooper)
                {
                    Console.Write(TYPE_ICON);
                    confirmation = CLI_Inputs.Check_Key_Press(previousMenu);

                    if (confirmation.ToUpper() == "Y")
                    {
                        // Add rental:
                        addSuccessful = Fleet.RentVehicle(regoInput, IDInputConverted);

                        if (addSuccessful)
                        {
                            // Print success message:
                            Console.WriteLine(SUCCESS, regoInput, IDInputConverted);
                        }
                        else
                        {
                            // Cannot add rental, print fail message:
                            Console.WriteLine(FAIL_SAVING);
                        }

                        // Exit loop:
                        confirmationLooper = true;
                        rentalAdded = true;
                    }
                    else if (confirmation.ToUpper() == "N")
                    {
                        Console.WriteLine();

                        // Go back to start:                        
                        confirmationLooper = true;
                    }
                    else
                    {
                        Console.WriteLine("\n*** WARNING: Input not valid. Please enter either Y or N. ***\n");
                    }
                }
            }
            
            // Go back to previous menu:
            Rental_Management_Menu();
        }


        /// <summary>
        /// This method returns a specified vehicle and saves the new rentl to the rentals csv file,
        /// given that the vehicle is not rented and customer is not rented.
        /// </summary>
        public static void Return_Vehicle()
        {
            // Constants:
            string REGO_REQUEST = "Please enter the registration number of the vehicle to return.\n"
                                  + "\nTo escape sequence, please leave field blank.\n";
            string CONFIRMATION = "Please enter (Y)es to confirm return of vehicle. To go back enter (N)o.\n";

            string SUCCESS = "\nSuccessfully returned vehicle '{0}' from customer '{1}' and removed from rentals list.\n"; // Rego, ID

            string FAIL_SAVING = "\n*** WARNING: Failed to return vehicle. Could not remove from rentals list/file. ***\n";
            
            int REGO_LENGTH = 6; // eg. 000XXX

            // Variables:
            string previousMenu = "Rental_Management";

            string regoNumber = "";
            List<string> regos = new List<string>();
            bool regoNumberValid = false;

            int customerID = -1;
            int customerRenting = -1;

            bool rented;
            bool returnSuccessful;
            bool confirmationLooper;
            string confirmation;

            // Reset returnSuccessful:
            returnSuccessful = false;

            // Add all existing registrations into regos list:
            foreach (var vehicleSaved in vehicles)
            {
                regos.Add(vehicleSaved.VehicleRego);
            }

            // Loop sequence:
            while (!returnSuccessful)
            {
                // Reset confirmationLooper, validField, and regoNumberValid:
                confirmationLooper = false;
                validFields = 0;
                regoNumberValid = false;

                // Request registration number:
                Console.WriteLine(COMMANDS_DISABLED);
                Console.WriteLine(REGO_REQUEST);

                while (!regoNumberValid)
                {
                    Console.Write(TYPE_ICON);
                    regoNumber = Console.ReadLine();

                    // Convert regoNumber to uppercae (as is required):
                    regoNumber = regoNumber.ToUpper();

                    // Check if left blank:
                    if (regoNumber == "")
                    {
                        // Print exit message:
                        Console.WriteLine(BLANK_FIELDS);
                        Console.WriteLine(COMMANDS_ENABLED);

                        // Previous menu:
                        CLI_Menus.Fleet_Management_Menu();

                        return;
                    }

                    // Check validity:
                    if (regoNumber.Length == REGO_LENGTH && (regoNumber.Remove(0, 3)).All(Char.IsLetter))
                    {
                        try
                        {
                            Convert.ToInt32(regoNumber.Remove(3));

                            // Check registration is in list:
                            if (regos.Contains(regoNumber))
                            {
                                // Check rented:
                                rented = Fleet.IsRented(regoNumber);
                                if (rented)
                                {
                                    // Get customer renting:
                                    customerRenting = Fleet.RentedBy(regoNumber);

                                    // Exit loop:
                                    regoNumberValid = true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("\n ** WARNING: The registration number could not be found. Please enter a valid registration number. ***\n");
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

                // Print attributes list to console:
                Console.WriteLine(COMMANDS_ENABLED);
                Console.WriteLine("Rental attributes:\n");
                Console.WriteLine(RENTAL_ATTRIBUTES, customerRenting, regoNumber);

                // Print confimation message:
                Console.WriteLine(CONFIRMATION);
                while (!confirmationLooper)
                {
                    Console.Write(TYPE_ICON);
                    confirmation = CLI_Inputs.Check_Key_Press(previousMenu);

                    if (confirmation.ToUpper() == "Y")
                    {
                        // Return rental:
                        customerID = Fleet.ReturnVehicle(regoNumber);

                        if (customerID != -1)
                        {
                            // Print success message:
                            Console.WriteLine(SUCCESS, regoNumber, customerID);
                        }
                        else
                        {
                            // Cannot remove rental, print fail message:
                            Console.WriteLine(FAIL_SAVING);
                        }

                        // Exit loop:
                        confirmationLooper = true;
                        returnSuccessful = true;
                    }
                    else if (confirmation.ToUpper() == "N")
                    {
                        Console.WriteLine();

                        // Go back to start:                        
                        confirmationLooper = true;
                    }
                    else
                    {
                        Console.WriteLine("\n*** WARNING: Input not valid. Please enter either Y or N. ***\n");
                    }
                }
            }

            // Go back to previous menu:
            Rental_Management_Menu();
        }


    }//end CLI_Menus class
}//end namespace
