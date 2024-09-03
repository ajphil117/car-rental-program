using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace MRRCManagement
{
    /// <summary>
    ///  
    /// The Fleet class provides the methods for loading and creating fleet and
    /// rentals csv files, adding, modifying, and removing vehicles from the csv file,
    /// modifying registration numbers in the rentals csv file, getting the fleet (the vehicles list
    /// and lists of rented of not rented vehicles), getting a vehicle from registration number,
    /// checking if a vehicle is rented, checking if a customer is renting, checking who a vehicle is rented
    /// by, getting the daily rate of a vehicle, renting a vehicle, returning a vehicle, saving vehicles and
    /// rentals to their respective csv files, and loading the fleet and rentals csv files.
    /// 
    /// Author Ash Phillips April 2020
    /// 
    /// </summary>
    public class Fleet
    {
        /*** Set up all constants and variables needed in the below methods ***/

        /** Set Constants **/

        public static string FLEET_CSV_HEADER = "Registration,Grade,Make,Model,Year,NumSeats,Transmission,Fuel,GPS,SunRoof,DailyRate,Colour";
        public static string RENTALS_CSV_HEADER = "Registration,CustomerID";


        /** Set Variables **/

        public static string fleet_file_path;
        public static string rentals_file_path;
      
        public static List<Vehicle> vehicles = new List<Vehicle>();
        
        public static List<Vehicle> rented = new List<Vehicle>();
        public static Dictionary<string, int> rentals = new Dictionary<string, int>();


        /*** METHODS ***/

        /// <summary>
        /// This constructor loads the fleet and rentals files using the methods LoadVehiclesFromFile() and LoadRentalsFromFile().
        /// </summary>
        public Fleet()
        {
            // Load fleet file:
            LoadVehiclesFromFile();

            // Load rentals file:
            LoadRentalsFromFile();            
        }


        /// <summary>
        /// This method adds the provided vehicle to the vehicles list and updates the fleet
        /// csv file accordingly, assuming the vehicle registration does not already exist in the file.
        /// </summary>
        /// 
        /// <param name="vehicle"> The vehicle to add to the fleet csv file. </param>
        /// <returns> True if addition was successful (registration did not exists in the fleet),
        /// false otherwise. </returns>
        public static bool AddVehicle(Vehicle vehicle)
        {
            // Variables:
            List<string> regoNumbers = new List<string>();

            // Add all existing registration numbers into regoNumbers list:
            foreach (var vehicleSaved in vehicles)
            {
                regoNumbers.Add(vehicleSaved.VehicleRego);
            }

            // Check that the registration number does not match a registration number in the list:
            if (!(regoNumbers.Contains(vehicle.VehicleRego)))
            {
                // Add to vehicles list:
                vehicles.Add(vehicle);

                // Save and update csv file:
                SaveVehiclesToFile(vehicles);

                // Addition successful:
                return true;
            }
            else
            {
                // Registration number already exists, cannot add vehicle:
                return false;
            }
        }


        /// <summary>
        /// This method modifies the provided vehicle in the vehicles list and updates the fleet
        /// csv file accordingly.
        /// 
        /// References:
        /// "LIST_NAME.RemoveAt(INDEX);" and "LIST/DICTIONARY_NAME.Insert(INDEX, MY VARIABLE);"
        ///     - taken from: https://stackoverflow.com/questions/4914802/editing-an-item-in-a-listt
        /// </summary>
        /// 
        /// <param name="myVehicle"> The vehicle to modify. </param>
        /// <param name="vehicleIndex"> The index at which the vehicle provided lies. </param>
        /// <returns> True if modification was successful (successfully saved to cvs file),
        /// and false otherwise. </returns>
        public static bool ModifyVehicle(Vehicle myVehicle, int vehicleIndex)
        {
            // Reload vehicles list into myVehicles list:
            List<Vehicle> myVehicles = GetFleet();

            // Replace old vehicle details:
            try
            {
                vehicles.RemoveAt(vehicleIndex);
                vehicles.Insert(vehicleIndex, myVehicle);

                // Update and save csv file:
                SaveVehiclesToFile(myVehicles);

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
        /// This method removes the vehicle with the provided registration from the vehicles list, and
        /// updates the fleet csv file accordingly, if they are not being rented by a customer.
        /// </summary>
        /// 
        /// <param name="registration"> The registration number of the vehicle to remove. </param>
        /// <returns> True if removal was successful, false otherwise. </returns>
        public static bool RemoveVehicle(string registration)
        {
            // Variables:
            bool rented;
            Vehicle myVehicle = null;

            // Reload vehicles list into myVehicles list:
            List<Vehicle> myVehicles = GetFleet();

            // Check if the vehicle is being rented:
            rented = Fleet.IsRented(registration);

            if (!rented)
            {
                // Get vehicle details:
                foreach (var vehicle in vehicles)
                {
                    if (vehicle.VehicleRego == registration)
                    {
                        myVehicle = vehicle;
                    }
                }

                // Remove vehicle from vehicles list:
                vehicles.Remove(myVehicle);

                // Save state of file:
                SaveVehiclesToFile(myVehicles);

                // Vehicle was successfully removed:
                return true;
            }
            else
            {
                // Vehicle is being rented, cannot remove:
                return false;
            }
        }


        /// <summary>
        /// This method modifies the provided registration in the rentals dictionary and updates the rentals
        /// csv file accordingly.
        /// 
        /// References:
        /// "DICTIONARY_NAME.RemoveAt(INDEX);" and "LIST/DICTIONARY_NAME.Insert(INDEX, MY VARIABLE);"
        ///     - taken from: https://stackoverflow.com/questions/4914802/editing-an-item-in-a-listt
        /// </summary>
        /// 
        /// <param name="originalRegistration"> The original registration of the modified vehicle. </param>
        /// <param name="newRegistration"> The modified registration of the modified vehicle. </param>
        /// <returns> True if modification was successful, false otherwise. </returns>
        public static bool ModifyRentals(string originalRegistration, string newRegistration)
        {
            // Variables:
            int customerID;

            // Reload rentals dictionary into myRentals dictionary:
            Dictionary<string, int> myRentals = rentals;

            // Get customerID:
            customerID = RentedBy(originalRegistration);

            // Replace old rental details:
            try
            {
                rentals.Remove(originalRegistration);
                rentals.Add(newRegistration, customerID);

                // Update and save csv file:
                SaveRentalsToFile(myRentals);

                // Modification successful:
                return true;
            }
            catch
            {
                // Modification failed:
                return false;
            }
        }


        /// <summary>
        /// This method returns the fleet (as a list of vehicles).
        /// </summary>
        /// 
        /// <returns> Vehicles list. </returns>
        public static List<Vehicle> GetFleet()
        {
            // Load csv file and refresh vehicles list:
            LoadVehiclesFromFile();

            // Return current vehicles list:
            return vehicles;
        }


        /// <summary>
        /// This method returns a subset of vehicles in the fleet depending on whether they are
        /// currently rented.
        /// </summary>
        /// 
        /// <param name="rented"> Boolean stating whether the list to return is either the rented vehicles
        /// or the not yet rented vehicles. </param>
        /// <returns> If rented is true, returns all rented vehicles. If false, returns all not yet rented vehicles. </returns>
        public static List<Vehicle> GetFleet(bool rented)
        {
            // Variables:
            List<Vehicle> rentedVehicles = new List<Vehicle>();
            List<Vehicle> unrentedVehicles = new List<Vehicle>();
            
            // Check state of rented:
            if (rented)
            {
                // Get list of rented vehicles:
                foreach (var vehicle in vehicles)
                {
                    if (IsRented(vehicle.VehicleRego))
                    {
                        rentedVehicles.Add(vehicle);
                    }
                }

                // Return vehicles list:
                return rentedVehicles;
            }
            else
            {
                // Get list of rented vehicles:
                foreach (var vehicle in vehicles)
                {
                    if (!IsRented(vehicle.VehicleRego))
                    {
                        unrentedVehicles.Add(vehicle);
                    }
                }

                // Return vehicles list:
                return unrentedVehicles;
            }
        }


        /// <summary>
        /// This method gets the vehicle that matches the provided registration.
        /// </summary>
        /// 
        /// <param name="registration"> The registration of the vehicle to get. </param>
        /// <returns> Vehicle that matches provided registration. </returns>
        public static Vehicle GetVehicle(string registration)
        {
            // Variables:
            Vehicle myVehicle = null;

            // Refresh vehicles list:
            vehicles = GetFleet();

            // Find registration in list:
            foreach (var vehicle in vehicles)
            {
                if (vehicle.VehicleRego == registration)
                {
                    // Save vehicle details to myVehicle: 
                    myVehicle = vehicle;
                }
            }

            // Return the vehicle:
            return myVehicle;
        }


        /// <summary>
        /// This method checks whether the vehicle with the provided registration is currently
        /// being rented.
        /// </summary>
        /// 
        /// <param name="registration"> The registraiton of the vehicle to check. </param>
        /// <returns> True if vehicle is being rented, false otherwose. </returns>
        public static bool IsRented(string registration)
        {
            // Reload rentals dictionary:
            LoadRentalsFromFile();
            
            // Check if registration is in the rentals dictionary:
            if (rentals.ContainsKey(registration))
            {
                // Registration exists, is being rented:
                return true;
            }
            else
            {
                // Registration does not exist, not being rented:
                return false;
            }
        }


        /// <summary>
        /// This method checks whether the customer with the provided ID is currently
        /// renting a vehicle.
        /// </summary>
        /// 
        /// <param name="customerID"> The ID of the customer to check. </param>
        /// <returns> True if customer is renting a vehicle, false otherwise. </returns>
        public static bool IsRenting(int customerID)
        {
            // Reload rentals dictionary:
            LoadRentalsFromFile();

            // Check if ID is in the rentals dictionary:
            if (rentals.ContainsValue(customerID))
            {
                // ID exists, is renting:
                return true;
            }
            else
            {
                // ID does not exist, not renting:
                return false;
            }
        }


        /// <summary>
        /// This method takes the registration of a vehicle to get the customer ID that
        /// belongs to the current renter of the vehicle.
        /// </summary>
        /// 
        /// <param name="registration"> The registration of the vehicle to check. </param>
        /// <returns> If vehicle is rented, returns customer ID. If not rented, returns -1. </returns>
        public static int RentedBy(string registration)
        {
            // Variables:
            bool rented;
            int customerID;

            // Check if the vehicle is being rented:
            rented = IsRented(registration);
            if (rented)
            {
                // Get ID number:
                rentals.TryGetValue(registration, out customerID);

                // Return ID number:
                return customerID;
            }
            else
            {
                // Rented by no one:
                return -1;
            }
        }


        /// <summary>
        /// This method takes the registration of a vehicle to get the daily rate of the vehicle.
        /// </summary>
        /// 
        /// <param name="registration"> The registration of the vehicle to get rate from. </param>
        /// <returns> If vehicle is rented, returns daily rate. If not rented, returns -1. </returns>
        public static double GetRate(string registration)
        {
            // Variable:
            bool rented;
            Vehicle myVehicle;
            double myRate;

            // Check if the vehicle is being rented:
            rented = IsRented(registration);
            if (rented)
            {
                // Get vehicle details:
                myVehicle = GetVehicle(registration);

                // Get the daly rate:
                myRate = myVehicle.VehicleRate;

                // Return daily rate:
                return myRate;
            }
            else
            {
                // Rented by no one:
                return -1;
            }
        }


        /// <summary>
        /// This method rents the vehicle with the provided registration to the customer with
        /// the provided ID, if the vehicle is not currently being rented.
        /// </summary>
        /// 
        /// <param name="registration"> The registraiton number of the vehicle to rent. </param>
        /// <param name="customerID"> The ID of the customer who wants to rent. </param>
        /// <returns> True if rental was possible, false otherwise. </returns>
        public static bool RentVehicle(string registration, int customerID)
        {
            // Variables:
            bool rented;

            // Check if vehicle is being rented:
            rented = IsRented(registration);

            if (!rented)
            {
                // Rent vehicle to customer:
                rentals.Add(registration, customerID);

                // Save state of file:
                SaveRentalsToFile(rentals);

                // Vehicle now rented to customer:
                return true;
            }
            else
            {
                // Vehicle is already rented:
                return false;
            }
        }


        /// <summary>
        ///  This method returns a vehicle.
        /// </summary>
        /// 
        /// <param name="registration"> The registration number of the vehicle to return. </param>
        /// <returns> If return is successful (it was currently being rented) it returns the customer ID
        /// of the customer who was renting it, otherwise it returns -1. </returns>
        public static int ReturnVehicle(string registration)
        {
            // Variables:
            bool rented;
            int customerID;

            // Check if vehicle is being rented:
            rented = IsRented(registration);

            if (rented)
            {
                // Return the customer ID:
                customerID = RentedBy(registration);

                // Return the vehicle:
                rentals.Remove(registration);

                // Save state of file:
                SaveRentalsToFile(rentals);

                // Vehicle successful returned by customer:
                return customerID;
            }
            else
            {
                // Vehicle was not being rented:
                return -1;
            }
        }


        /// <summary>
        /// This method saves the current state of the vehicles list to the fleet csv file.
        /// </summary>
        /// 
        /// <param name="myVehicles"> The list of vehicles to save to the csv file. </param>
        public static void SaveVehiclesToFile(List<Vehicle> myVehicles)
        {
            // Variables:
            string vehicleCSV;

            // Update vehicles csv file:
            File.Delete(fleet_file_path);
            File.WriteAllText(fleet_file_path, FLEET_CSV_HEADER);
            File.AppendAllText(fleet_file_path, "\n");
            foreach (var vehicle in myVehicles)
            {
                // Write each vehicle back to CSV format:
                vehicleCSV = vehicle.ToCSVString();

                // Add to file:
                File.AppendAllText(fleet_file_path, vehicleCSV);
                File.AppendAllText(fleet_file_path, "\n");
            }
        }


        /// <summary>
        /// This method saves the current list of rentals to file.
        /// </summary>
        /// 
        /// <param name="myRentals"> The dictionary of rentals to save to the csv file. </param>
        public static void SaveRentalsToFile(Dictionary<string, int> myRentals)
        {
            // Variables:
            string rentalCSV;

            // Update rentals csv file:
            File.Delete(rentals_file_path);
            File.WriteAllText(rentals_file_path, RENTALS_CSV_HEADER);
            File.AppendAllText(rentals_file_path, "\n");
            foreach (var item in myRentals)
            {
                // Write each rental back to CSV format:
                rentalCSV = (item.Key).ToString() + "," + (item.Value).ToString();

                // Add to file:
                File.AppendAllText(rentals_file_path, rentalCSV);
                File.AppendAllText(rentals_file_path, "\n");
            }
        }


        /// <summary>
        /// This method loads the list of vehicles from the file.
        /// If there is no file at the specified location, this method creates an empty fleet csv file.
        /// 
        /// References:
        /// The main structure of this method was taken from:
        ///     https://teamtreehouse.com/community/c-reading-data-from-csv-file-into-a-list-object-and-display-in-listview-in-windows-form
        /// </summary> 
        public static void LoadVehiclesFromFile()
        {
            // Variables:
            List<string> fleetFileData;
            string[] data;
            VehicleGrade dataGrade;
            TransmissionType dataTransmission;
            FuelType dataFuel;

            // Reset vehicles list:
            vehicles.Clear();

            // If file does not exist, create new file and add header:
            if (!File.Exists(fleet_file_path))
            {
                File.WriteAllText(fleet_file_path, FLEET_CSV_HEADER);
                File.AppendAllText(fleet_file_path, "\n");

                // Clear the rentals file (as there are no vehicles):
                File.Delete(Fleet.rentals_file_path);
                File.WriteAllText(Fleet.rentals_file_path, Fleet.RENTALS_CSV_HEADER);
                File.AppendAllText(Fleet.rentals_file_path, "\n");
            }

            // Load file and assign to variable:
            fleetFileData = File.ReadAllLines(fleet_file_path).ToList();

            // Remove the top line (headings):
            fleetFileData.RemoveAt(0);

            // Split the data into single vehicles:
            foreach (var line in fleetFileData)
            {
                data = line.Split(',');

                // Assign attributes to enum types:
                dataGrade = (VehicleGrade)Enum.Parse(typeof(VehicleGrade), data[1]);
                dataTransmission = (TransmissionType)Enum.Parse(typeof(TransmissionType), data[6]);
                dataFuel = (FuelType)Enum.Parse(typeof(FuelType), data[7]);

                // Write data to vehicleAttributes:
                Vehicle vehicleAttributes = new Vehicle(data[0], dataGrade, data[2], data[3], int.Parse(data[4]),
                                                        int.Parse(data[5]), dataTransmission, dataFuel, bool.Parse(data[8]),
                                                        bool.Parse(data[9]), double.Parse(data[10]), data[11]);

                // Add each vehicle to vehicles list:
                vehicles.Add(vehicleAttributes);
            }
            
        }


        /// <summary>
        /// This method loads the list of rentals from the file.
        /// If there is no file at the specified location, this method creates an empty rentals csv file.
        /// 
        /// References:
        /// The main structure of this method was take from:
        ///     https://teamtreehouse.com/community/c-reading-data-from-csv-file-into-a-list-object-and-display-in-listview-in-windows-form
        /// </summary>
        public static void LoadRentalsFromFile()
        {
            // Variables:
            List<string> rentalsFileData;
            string[] data;

            // Reset rentals dictionary:
            rentals.Clear();

            // If file does not exist, create new file and add header:
            if (!File.Exists(rentals_file_path))
            {                
                File.WriteAllText(rentals_file_path, RENTALS_CSV_HEADER);
                File.AppendAllText(rentals_file_path, "\n");
            }
            
            // Load file and assign to variable:
            rentalsFileData = File.ReadAllLines(rentals_file_path).ToList();

            // Remove the top line (headings):
            rentalsFileData.RemoveAt(0);

            // Split the data into single rentals:
            foreach (var line in rentalsFileData)
            {
                data = line.Split(',');

                // Add registraiton numbers and IDs to dictionary:
                rentals.Add(data[0], int.Parse(data[1]));
            }
        }


    }//end Fleet class
}//end namespace
