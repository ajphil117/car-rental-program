using System;


namespace MRRCSearchAlgorithm
{
    /// <summary>
    /// 
    /// The Attribute_Token class provides the methods for setting an Attribute_Token and for overriding that to a string.
    /// 
    /// Author Ash Phillips June 2020
    /// 
    /// </summary>
    public class Attribute_Token : IToken
    {
        /*** Set up all constants and variables needed in the below methods ***/

        /** Set Variables **/

        public string Attribute;


        /*** METHODS ***/

        /// <summary>
        /// This constructor sets the Attribute string to the value provided.
        /// </summary>
        /// 
        /// <param name="attribute"> The attribute string passed. </param>
        public Attribute_Token(string attribute)
        {
            // Sets Attribute variable;
            this.Attribute = attribute;
        }


        /// <summary>
        /// This method returns the overrrides the attribute token to the string representation.
        /// </summary>
        /// 
        /// <returns> Returns string representation of the attribute token. </returns>
        public override string ToString()
        {    
			// Returns Attribute as string;
            return Attribute;
        }


    }//end Attribute_Token class
}//end namespace
