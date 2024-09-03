using System;


namespace MRRCManagement
{
    /*** Set Enumeration Types ***/

    public enum Gender
    {
        Male,
        Female,
        Other
    }


    /*** Classes ***/

    /// <summary>
    /// 
    /// The Customer class provides the methods for constructing a customer,
    /// converting the customers list to a csv string, and converting the
    /// customers list to a string.
    /// 
    /// Author Ash Phillips April 2020
    /// 
    /// </summary>
    public class Customer
    {
        /*** Set up all variables needed in the below methods ***/
        
        /** Set Variables **/

        // Customer attributes in their required format:
        public int CustomerID { get; set; }
        public string CustomerTitle { get; set; }
        public string CustomerFN { get; set; }
        public string CustomerLN { get; set; }
        public Gender CustomerGender { get; set; }
        public DateTime CustomerDOB { get; set; }        


        /*** METHODS ***/

        /// <summary>
        /// This constructor constructs a customer with the provided attributes.
        /// </summary>
        /// 
        /// <param name="ID"> The value of the ID attribute. </param>
        /// <param name="title"> The value of the title attribute. </param>
        /// <param name="firstName">The value of the first name attribute. </param>
        /// <param name="lastName"> The value of the last name attribute. </param>
        /// <param name="gender"> The value of the gender attribute. </param>
        /// <param name="DOB"> The value of the DOB attribute. </param>
        public Customer (int ID, string title, string firstName, string lastName, Gender gender,
                        DateTime DOB)
        {
            // Assign all attribute values to attributes:
            CustomerID = ID;
            CustomerTitle = title;
            CustomerFN = firstName;
            CustomerLN = lastName;
            CustomerGender = gender;
            CustomerDOB = DOB;
        }


        /// <summary>
        /// This method converts the customers list to a csv string that is consistent
        /// with the provided data files.
        /// </summary>
        /// 
        /// <returns> The customers list as a csv string. </returns>
        public string ToCSVString()
        {
            // Variables:
            string customersCSV;
            
            // Convert customer details to string:
            customersCSV = ToString();

            // Add together in form of csv string:
            customersCSV = customersCSV.Replace(" ", ",");

            // Return csv representation:
            return customersCSV;
        }


        /// <summary>
        /// This method converts the customers list to a string representation of the
        /// attributes of the customer. 
        /// </summary>
        /// 
        /// <returns> The customers list as a string. </returns>
        public override string ToString()
        {
            // Variables:
            string customerString;
            int timeLength = 12; // length of time at end of DOB and space between,
                                 // eg. 12:00:00 AM (is 11, add space at start to get 12)

            // Customer attributes in string format:
            string stringID;
            string stringTitle;
            string stringFN;
            string stringLN;
            string stringGender;
            string stringDOB;

            // Convert all attributes to strings:
            stringID = Convert.ToString(CustomerID);
            stringTitle = CustomerTitle;
            stringFN = CustomerFN;
            stringLN = CustomerLN;
            stringGender = Convert.ToString(CustomerGender);
            stringDOB = Convert.ToString(CustomerDOB);

            // Remove time at end of DOB:
            stringDOB = stringDOB.Remove(stringDOB.Length - timeLength);

            // Add attributes to single string:
            customerString = string.Join(" ", stringID, stringTitle, stringFN, stringLN, stringGender, stringDOB);

            // Return string representation:
            return customerString;
        }


    }//end Customer class
}//end namespace
