using System;
using System.Collections.Generic;


namespace MRRCManagement
{
    /*** Set Enumeration Types ***/

    public enum VehicleGrade
    {
        Economy,
        Family,
        Luxury,
        Commercial
    }

    public enum TransmissionType
    {
        Automatic,
        Manual
    }

    public enum FuelType
    {
        Petrol,
        Diesel
    }


    /*** Classes ***/    

    /// <summary>
    ///  
    /// The Vehicle class provides the methods for constructing a vehicle,
    /// converting the vehicles list to a csv string, converting the vehicles
    /// list to string, and for getting a list of the attributes of a vehicle
    /// in string format.
    /// 
    /// Author Ash Phillips April 2020
    /// 
    /// </summary>
    public class Vehicle
    {
        /*** Set up all variables needed in the below methods ***/

        /** Set Variables **/

        // Vehicle attributes in their required format:
        public string VehicleRego { get; set; }
        public VehicleGrade VehicleGrade { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public int VehicleYear { get; set; }
        public int VehicleSeats { get; set; }
        public TransmissionType VehicleTransmission { get; set; }
        public FuelType VehicleFuel { get; set; }
        public bool VehicleGPS { get; set; }
        public bool VehicleSunroof { get; set; }
        public double VehicleRate { get; set; }
        public string VehicleColour { get; set; }


        // Vehicle attributes in string format:
        public static string stringRego;
        public static string stringGrade;
        public static string stringMake;
        public static string stringModel;
        public static string stringYear;
        public static string stringSeats;
        public static string stringTransmission;
        public static string stringFuel;
        public static string stringGPS;
        public static string stringSunroof;
        public static string stringRate;
        public static string stringColour;


        /*** METHODS ***/

        /// <summary> 
        /// This constructor constructs a vehicle using values for all parameters
        /// of the vehicle (each attribute).
        /// </summary>
        /// 
        /// <param name="registration"> The value of the registration attribute. </param>
        /// <param name="grade"> The value of the grade attribute. </param>
        /// <param name="make"> The value of the make attribute. </param>
        /// <param name="model"> The value of the model attribute. </param>
        /// <param name="year"> The value of the year attribute. </param>
        /// <param name="numSeats"> The value of the number of seats attribute. </param>
        /// <param name="transmission"> The value of the transmission attribute. </param>
        /// <param name="fuel"> The value of the fuel attribute. </param>
        /// <param name="GPS"> The value of the GPS attribute. </param>
        /// <param name="sunRoof"> The value of the sunroof attribute. </param>
        /// <param name="dailyRate"> The value of the daily rate attribute. </param>
        /// <param name="colour"> The value of the colour attribute. </param>
        public Vehicle(string registration, VehicleGrade grade, string make, string model,
                       int year, int numSeats, TransmissionType transmission, FuelType fuel,
                       bool GPS, bool sunRoof, double dailyRate, string colour)
        {
            // Assign all attribute values to attributes:
            VehicleRego = registration;
            VehicleGrade = grade;
            VehicleMake = make;
            VehicleModel = model;
            VehicleYear = year;
            VehicleSeats = numSeats;
            VehicleTransmission = transmission;
            VehicleFuel = fuel;
            VehicleGPS = GPS;
            VehicleSunroof = sunRoof;
            VehicleRate = dailyRate;
            VehicleColour = colour;
        }


        /// <summary>
        /// This constructor constructs a vehicle using values for on;y the required parameters
        /// of the vehicle (each attribute) and sets all other attributes to their default values.
        /// </summary> 
        /// 
        /// <param name="registration"> The value of the registration attribute. </param>
        /// <param name="grade"> The value of the grade attribute. </param>
        /// <param name="make"> The value of the make attribute. </param>
        /// <param name="model"> The value of the model attribute. </param>
        /// <param name="year"> The value of the year attribute. </param>
        public Vehicle(string registration, VehicleGrade grade, string make, string model,
                       int year)
        {
            // Assign required attribute values to attributes:
            VehicleRego = registration;
            VehicleGrade = grade;
            VehicleMake = make;
            VehicleModel = model;
            VehicleYear = year;
            
            // Set all other attributes to their default values:
            // Commercial:
            if (VehicleGrade == VehicleGrade.Commercial)
            {
                VehicleSeats = 4; // 4-seater
                VehicleTransmission = TransmissionType.Manual;
                VehicleFuel = FuelType.Diesel;
                VehicleGPS = false;
                VehicleSunroof = false;
                VehicleRate = 130; // $130/day
                VehicleColour = "Black";
            }
            // Economy:
            else if (VehicleGrade == VehicleGrade.Economy)
            {
                VehicleSeats = 4; // 4-seater
                VehicleTransmission = TransmissionType.Automatic;
                VehicleFuel = FuelType.Petrol;
                VehicleGPS = false;
                VehicleSunroof = false;
                VehicleRate = 50; // $50/day
                VehicleColour = "Black";
            }
            // Family:
            else if (VehicleGrade == VehicleGrade.Family)
            {
                VehicleSeats = 4; // 4-seater
                VehicleTransmission = TransmissionType.Manual;
                VehicleFuel = FuelType.Petrol;
                VehicleGPS = false;
                VehicleSunroof = false;
                VehicleRate = 80; // $80/day
                VehicleColour = "Black";
            }
            // Luxury:
            else
            {
                VehicleSeats = 4; // 4-seater
                VehicleTransmission = TransmissionType.Manual;
                VehicleFuel = FuelType.Petrol;
                VehicleGPS = true;
                VehicleSunroof = true;
                VehicleRate = 120; // $120/day
                VehicleColour = "Black";
            }
        }


        /// <summary>
        /// This method converts the vehicles list to a csv string that is consistent
        /// with the provided data files.
        /// </summary>
        /// 
        /// <returns> The vehicles list as a csv string. </returns>
        public string ToCSVString()
        {
            // Variables:
            string vehiclesCSV;

            // Convert vehicle details to string format:
            vehiclesCSV = ToString();

            // Add together in form of csv string:
            vehiclesCSV = vehiclesCSV.Replace(" ", ",");

            // If there were spaces in vehicle model, add them back in:
            vehiclesCSV = vehiclesCSV.Replace("!", " ");

            // Return csv representation:
            return vehiclesCSV;
        }


        /// <summary>
        /// This method converts the vehicles list to a string representation of the
        /// attributes of the vehicle. 
        /// </summary>
        /// 
        /// <returns> The vehicles list as a string. </returns>
        public override string ToString()
        {
            // Variables:
            string vehicleString;            

            // Convert all attributes to strings:
            stringRego = VehicleRego;
            stringGrade = Convert.ToString(VehicleGrade);
            stringMake = VehicleMake;
            stringModel = VehicleModel;
            stringYear = Convert.ToString(VehicleYear);
            stringSeats = Convert.ToString(VehicleSeats);
            stringTransmission = Convert.ToString(VehicleTransmission);
            stringFuel = Convert.ToString(VehicleFuel);
            stringGPS = Convert.ToString(VehicleGPS);
            stringSunroof = Convert.ToString(VehicleSunroof);
            stringRate = Convert.ToString(VehicleRate);
            stringColour = VehicleColour;

            // If there are spaces in the vehicleModel, replace with ! to preserve them:
            stringModel = stringModel.Replace(" ", "!");

            // Add details to single string:
            vehicleString = string.Join(" ", stringRego, stringGrade, stringMake, stringModel, stringYear, stringSeats, stringTransmission,
                                        stringFuel, stringGPS, stringSunroof, stringRate, stringColour);

            // Return string representation:
            return vehicleString;
        }


        /// <summary>
        /// This method converts each vehicle attribute into their string representation.
        /// </summary>
        /// 
        /// <returns> A list of strings which represent each vehicle attribute. </returns>
        public static List<string> GetAttributeList(Vehicle vehicle)
        {
            // Variables:
            List<string> attributesList = new List<string>();

            // Convert each required attribute to string format:
            stringRego = vehicle.VehicleRego.ToUpper();
            stringGrade = Convert.ToString(vehicle.VehicleGrade).ToUpper();
            stringMake = vehicle.VehicleMake.ToUpper();
            stringModel = vehicle.VehicleModel.ToUpper();
            stringYear = Convert.ToString(vehicle.VehicleYear).ToUpper();
            stringSeats = Convert.ToString(vehicle.VehicleSeats).ToUpper();
            stringTransmission = Convert.ToString(vehicle.VehicleTransmission).ToUpper();
            stringFuel = Convert.ToString(vehicle.VehicleFuel).ToUpper();            
            stringColour = vehicle.VehicleColour.ToUpper();

            // Add " Seater" onto stringSeats:
            stringSeats += "-SEATER";            

            // Add above attributes to list, excluding stringColour:
            attributesList.Add(stringRego);
            attributesList.Add(stringGrade);
            attributesList.Add(stringMake);
            attributesList.Add(stringModel);
            attributesList.Add(stringYear);
            attributesList.Add(stringSeats);
            attributesList.Add(stringTransmission);
            attributesList.Add(stringFuel);

            // Convert GPS attribute and add the list if true:
            if (vehicle.VehicleGPS)
            {
                stringGPS = "GPS";
                attributesList.Add(stringGPS);
            }

            // Convert Sunroof attribute and add if true:
            if (vehicle.VehicleSunroof)
            {
                stringSunroof = "SUNROOF";
                attributesList.Add(stringSunroof);
            }

            // Add stringColour attribute to list:
            attributesList.Add(stringColour);

            // Return attributes list:
            return attributesList;
        }   
        

    }//end Vehicle class
}//end namespace
