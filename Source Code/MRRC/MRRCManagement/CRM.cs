using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace MRRCManagement
{
    /// <summary>
    /// 
    /// The CRM class provides the methods for loading the customers csv file,
    /// adding, modifying, and removing customers from the csv file, getting the customers
    /// list, getting a customer based on their ID number, saving state of customers csv file,
    /// and loading customers list/creating customers csv file.
    /// 
    /// Author Ash Phillips April 2020
    /// 
    /// </summary>
    public class CRM
    {
        /*** Set up all constants and variables needed in the below methods ***/

        /** Set Constants **/

        public static string CUSTOMERS_CSV_HEADER = "ID,Title,FirstName,LastName,Gender,DOB";
        

        /** Set Variables **/
        
        public static string customers_file_path;
        
        public static List<Customer> customers = new List<Customer>();
        

        /*** METHODS ***/

        /// <summary>
        /// This constructor loads the customers file using the method LoadFile().
        /// </summary>
        public CRM()
        {
            // Load customers file:            
            LoadFromFile();
        }


        /// <summary>
        /// This method adds the provided customer to the customers list and updates the customers
        /// csv file accordingly, assuming the customer ID does not already exist in the file.
        /// </summary>
        /// 
        /// <param name="customer"> The customer to add to the customers csv file. </param>
        /// <returns> True if addition was successful (ID did not exists in the CRM),
        /// false otherwise. </returns>
        public static bool AddCustomer(Customer customer)
        {
            // Variables:
            List<int> IDs = new List<int>();
            
            // Add all existing IDs into IDs list:
            foreach (var customerSaved in customers)
            {
                IDs.Add(customerSaved.CustomerID);
            }

            // Check that the customer ID does not match an ID in the list:
            if (!IDs.Contains(customer.CustomerID))
            {
                // Add to customers list:
                customers.Add(customer);

                // Save and update csv file:
                SaveToFile(customers);

                // Addition successful:
                return true;
            }
            else
            {
                // ID already exists, cannot add customer:
                return false;
            }
        }


        /// <summary>
        /// This method modifies the provided customer in the customers list and updates the customers
        /// csv file accordingly.
        /// 
        /// References:
        /// "LIST_NAME.RemoveAt(INDEX);" and "LIST/DICTIONARY_NAME.Insert(INDEX, MY VARIABLE);"
        ///     - taken from: https://stackoverflow.com/questions/4914802/editing-an-item-in-a-listt
        /// </summary>
        /// 
        /// <param name="myCustomer"> The customer to modify. </param>
        /// <param name="customerIndex"> The index at which the customer provided lies. </param>
        /// <returns> True if modification was successful, and false otherwise. </returns>
        public static bool ModifyCustomer(Customer myCustomer, int customerIndex)
        {   
            // Reload customers list into myCustomers list:
            List<Customer> myCustomers = GetCustomers();

            // Replace old customer details:
            try
            {
                customers.RemoveAt(customerIndex);
                customers.Insert(customerIndex, myCustomer);

                // Update and save csv file:
                SaveToFile(myCustomers);

                // Modificaion successful:
                return true;
            }
            catch
            {
                // Modification failed:
                return false;
            }            
        }


        /// <summary>
        /// This method removes the customer with the provided ID from the customers list, and
        /// updates the customers csv file accordingly, if they are not currently renting a vehicle.
        /// </summary>
        /// 
        /// <param name="ID"> The ID number of the customer to remove. </param>
        /// <returns> True if removal was successful, false otherwise. </returns>
        public static bool RemoveCustomer(int ID)
        {
            // Variables:
            bool renting;
            Customer myCustomer = null;

            // Reload customers list into myCustomers list:
            List<Customer> myCustomers = GetCustomers();

            // Check if the customer is renting:
            renting = Fleet.IsRenting(ID);

            if (!renting)
            {
                // Get customer details:
                foreach (var customer in customers)
                {
                    if (customer.CustomerID == ID)
                    {
                        myCustomer = customer;
                    }
                }

                // Remove customer from customers list:
                customers.Remove(myCustomer);

                // Update and save csv file:
                SaveToFile(myCustomers);

                // Customer was successfully removed:
                return true;
            }
            else
            {
                // Customer is renting, cannot remove:
                return false;
            }
        }


        /// <summary>
        /// This method returns the customers (as a list of customers).
        /// </summary>
        /// 
        /// <returns> Customers list. </returns>
        public static List<Customer> GetCustomers()
        {
            // Load csv file and refresh customers list:
            LoadFromFile();

            // Return current customers list:
            return customers;
        }


        /// <summary>
        /// This method gets the customer that matches the provided ID.
        /// </summary>
        /// 
        /// <param name="ID"> The ID of the customer to get. </param>
        /// <returns> Customer that matches provided ID. </returns>
        public static Customer GetCustomer(int ID)
        {
            // Variables:
            Customer myCustomer = null;

            // Refresh customers list:
            customers = GetCustomers();

            // Find ID in list:
            foreach (var customer in customers)
            {
                if (customer.CustomerID == ID)
                {
                    // Save customer details to myCustomer: 
                    myCustomer = customer;
                }
            }

            // Return the customer:
            return myCustomer;
        }


        /// <summary>
        /// This method saves the current state of the CRM to the csv file.
        /// </summary> 
        /// 
        ///<param name = "myCustomers" > The list of customers to save to the csv file. </param>
        public static void SaveToFile(List<Customer> myCustomers)
        {
            // Variables:
            string customerCSV;

            // Replace/Update customers csv file:
            File.Delete(customers_file_path);
            File.WriteAllText(customers_file_path, CUSTOMERS_CSV_HEADER);
            File.AppendAllText(customers_file_path, "\n");
            foreach (var customer in myCustomers)
            {
                // Write each customer back to CSV format:
                customerCSV = customer.ToCSVString();

                // Add to file:
                File.AppendAllText(customers_file_path, customerCSV);
                File.AppendAllText(customers_file_path, "\n");
            }
        }


        /// <summary>
        /// This method loads the list of customers from the file.
        /// If there is no file at the specified location, this method creates an empty customers csv file.
        /// 
        /// References:
        /// The main structure of this method was taken from:
        ///     https://teamtreehouse.com/community/c-reading-data-from-csv-file-into-a-list-object-and-display-in-listview-in-windows-form
        /// </summary>
        /// 
        /// <returns> True if file is not open, false if file is open. </returns>
        public static void LoadFromFile()
        {
            // Variables:
            List<string> customersFileData;
            string[] data;
            Gender dataGender;            

            // Reset customers list;
            customers.Clear();

            // If file does not exist, create new file and add header:
            if (!File.Exists(customers_file_path))
            {
                File.WriteAllText(customers_file_path, CUSTOMERS_CSV_HEADER);
                File.AppendAllText(customers_file_path, "\n");

                // Clear the rentals file (as there are no cutomers):
                File.Delete(Fleet.rentals_file_path);
                File.WriteAllText(Fleet.rentals_file_path, Fleet.RENTALS_CSV_HEADER);
                File.AppendAllText(Fleet.rentals_file_path, "\n");
            }
            
            // Load file and assign to variable:
            customersFileData = File.ReadAllLines(customers_file_path).ToList();

            // Remove the top line (headings):
            customersFileData.RemoveAt(0);

            // Split the data into single customers:
            foreach (var line in customersFileData)
            {
                data = line.Split(',');

                // Assign attributes to enum types:
                dataGender = (Gender)Enum.Parse(typeof(Gender), data[4]);

                // Write data to customerAttributes:
                Customer customerAttributes = new Customer(int.Parse(data[0]), data[1], data[2], data[3],
                                                            dataGender, DateTime.Parse(data[5]));

                // Add each customer to customers list:
                customers.Add(customerAttributes);
            }             
        }


    }//end CRM class
}//end namespace
