using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{

    public class CommandLineException : Exception
    {
        /// <summary> 
        /// This constructor defines the TextParseException exception.
        /// </summary>
        public CommandLineException() : base(String.Format("Error: Insufficient command line arguments."))
        {

        }
    }


    public class TextParseException : Exception
    {
        /// <summary> 
        /// This constructor defines the TextParseException exception.
        /// </summary>
		public TextParseException(string expression) : base(String.Format("Error: Expression '{0}' cannot be parsed into tokens.", expression.Replace(" ","")))
		{
                
        }
    }

        
    public class ShuntingYardException : Exception
    {
        /// <summary> 
        /// This constructor defines the ShuntingYardException exception.
        /// </summary>
        public ShuntingYardException() : base(String.Format("Error: Mismatched parenthesis in expression."))
        {

        }
    }


    public class EvaluatorException : Exception
        {
            /// <summary> 
            /// This constructor defines the EvaluatorException exception.
            /// </summary>
            public EvaluatorException() : base(String.Format("Error: Invalid operator use in expression."))
            {

            }
        }
    
}
