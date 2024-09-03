using System;


namespace MRRCSearchAlgorithm
{
    /// <summary>
    /// 
    /// The Operator_Token class provides the methods for constructing the Operator_Token, overriding the operatorName to a string, 
    /// and invoking and returning the operator functionality.
    /// 
    /// Author Ash Phillips June 2020
    /// 
    /// </summary>
	public class Operator_Token : IToken
    {
        /*** Set up variables needed in the below methods ***/

        /** Set Variables **/

        // Variables:
        public string operatorName;
		public int Precedence { get; set; }
		public Func<bool, bool, bool> Function;


        /*** METHODS ***/

        /// <summary>
        /// This constuctor assigns the values the operatorName, Precedence, and Function.
        /// </summary>
        /// 
        /// <param name="operatorName"> The type of operator token (AND or OR). </param>
        /// <param name="precedence"> The precedence of the operator token (priority). </param>
        /// <param name="function"> How the operator functions. </param>
        public Operator_Token(string operatorName, int precedence, Func<bool, bool, bool> function)
        {
            // Assign values to Operator_Token variables:
            this.operatorName = operatorName;
            Precedence = precedence;
			Function = function;
        }


        /// <summary>
        /// This method overrides the operatorName to a string.
        /// </summary>
        /// 
        /// <returns> The string of the operator token. </returns>
        public override string ToString()
        {
            // Return string for operator:
            return operatorName;
        }


        /// <summary>
        /// This method invokes the functionality of the operator token.
        /// </summary>
        /// 
        /// <param name="attributeA"> First attribute, in front of the operator. </param>
        /// <param name="attributeB"> Second attribute, after the operator. </param>
        /// <returns> The functionality of the operator. </returns>
		public bool Evaluate(bool attributeA, bool attributeB)
		{
            // Variables:
            bool operatorFunction;
			
            // Invoke the functionality of the operator:
            operatorFunction = Function.Invoke(attributeA, attributeB);

			return operatorFunction;
		}


    }//end Operator_Token class
}//end namespace
