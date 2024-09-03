using System;


namespace MRRCSearchAlgorithm
{
    /// <summary>
    /// 
    /// The Left_Parenthesis_Token class provides the method to add the left parenthesis.
    /// 
    /// Author Ash Phillips June 2020
    /// 
    /// </summary>
    public class Left_Parenthesis_Token : IToken
    {
        /// <summary>
        /// This method gives the left parenthesis to the token list.
        /// </summary>
        /// 
        /// <returns> Returns the left parenthesis. </returns>
        public override string ToString()
        {
            // Return left parenthesis:
            return "(";
        }


    }//end Left_Parenthesis_Token class
}//end namespace
