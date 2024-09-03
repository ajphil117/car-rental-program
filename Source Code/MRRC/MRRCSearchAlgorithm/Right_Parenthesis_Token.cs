using System;


namespace MRRCSearchAlgorithm
{
    /// <summary>
    /// 
    /// The Right_Parenthesis_Token class provides the method to add the right parenthesis.
    /// 
    /// Author Ash Phillips June 2020
    /// 
    /// </summary>
    public class Right_Parenthesis_Token : IToken
    {
        /// <summary>
        /// This method gives the right parenthesis to the token list.
        /// </summary>
        /// 
        /// <returns> Returns the right parenthesis. </returns>
        public override string ToString()
        {
            // Return right parenthesis:
            return ")";
        }


    }//end Right_Parenthesis_Token class
}//end namespace
