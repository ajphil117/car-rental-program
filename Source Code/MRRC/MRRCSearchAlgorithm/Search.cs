using System;
using System.Collections.Generic;
using System.Linq;


namespace MRRCSearchAlgorithm
{
	/// <summary>
	/// 
	/// The Search class provides the methods for parsing the query to a token list, the Shunting Algorithm, and recieving results
	/// as to if the search attribute exists in the vehicle being testing.
	/// 
	/// References:
	/// This class implements features given in the worksheet 9 solutions that have been adapted for use here.
	/// 
	/// Author Ash Phillips June 2020
	/// 
	/// </summary>
	public class Search
    {
		/// <summary>
		/// This method take a search query string and parses it (splits) to a list of single attributes, operators, and parenthesis.
		/// 
		/// References:
		/// Stucture for splitting at delimeters.
		/// Source: https://stackoverflow.com/questions/4680128/split-a-string-with-delimiters-but-keep-the-delimiters-in-the-result-in-c-sharp
		/// </summary>
		/// 
		/// <param name="query"> Search query string to parse. </param>
		/// <returns> Returns the infixTokens list (list of split query) or null if invalid query. </returns>
		public static List<IToken> ParseText(string query)
		{
			// Variables:
			char[] delimiters = { '(', ')', ' ', '\"' };

			var queryParts = new List<string>();
			List<IToken> InfixTokens = new List<IToken>();

			int ANDPrecedence = 1; //default = 1 
			int ORPrecedence = 1; //default = 1

			string uppercaseQuery;

			bool confirmation = false;
			string desiredANDPriority;

			int startDelimeterIndex;
			int endDelimeterIndex;

			int startQuotesIndex;

			// Make query all upper case:
			uppercaseQuery = query.ToUpper();

			// Check query type:
			// Combination, no parenthesis:
			if (uppercaseQuery.Contains("AND") && uppercaseQuery.Contains("OR"))
			{
				while (!confirmation)
				{
					// Check desired precendence of AND:
					Console.Write("\nDoes \"AND\" have priority? Y/N: ");
					desiredANDPriority = Console.ReadLine();

					// AND has priority:
					if (desiredANDPriority.ToUpper() == "N")
					{
						// Do not change ANDPrecedence value, exit loop:
						confirmation = true;
					}
					else if (desiredANDPriority.ToUpper() == "Y")
					{
						// Increase the ANDPrecedence value:
						ANDPrecedence = 2;

						//Exit loop:
						confirmation = true;
					}
					else
					{
						// Invaild input, print error message to console and repeat:
						Console.WriteLine("\nPlease enter either Y or N.\n");
					}
				}
			}

			// Set startDelimeterIndex:
			startDelimeterIndex = 0;

			// Set endDelimeterIndex:
			do
			{
				endDelimeterIndex = uppercaseQuery.IndexOfAny(delimiters, startDelimeterIndex);

				// If delimiters exist after 0:
				if (endDelimeterIndex >= 0)
				{
					if (endDelimeterIndex > startDelimeterIndex)
                    {
						// Add the first part of query before the delimeter:
						queryParts.Add(uppercaseQuery.Substring(startDelimeterIndex, endDelimeterIndex - startDelimeterIndex));
                    }

					// Check if query contains attribute with quotes:
					if (uppercaseQuery[endDelimeterIndex] == '\"')
					{
						// Get what's after the first quote:							
						startDelimeterIndex = endDelimeterIndex + 1;
						startQuotesIndex = startDelimeterIndex;

						// Get next quote:
						do
						{
							endDelimeterIndex = uppercaseQuery.IndexOfAny(delimiters, startQuotesIndex);
							startQuotesIndex = endDelimeterIndex + 1;

						} while (uppercaseQuery[endDelimeterIndex] != '\"');

						// Add the attribute inside quotes to queryParts, excluding the quotes:
						queryParts.Add(uppercaseQuery.Substring(startDelimeterIndex, endDelimeterIndex - startDelimeterIndex));
					}
					else
					{
						// Add delimeter:
						queryParts.Add(new string(uppercaseQuery[endDelimeterIndex], 1));
					}

					// Increase indexFirst to index after the delimeter:
					startDelimeterIndex = endDelimeterIndex + 1;

					continue;
				}

				// Check if no more delimiters exist, add the last part of the query:
				queryParts.Add(uppercaseQuery.Substring(startDelimeterIndex, uppercaseQuery.Length - startDelimeterIndex));

				// End:
				break;

			} while (startDelimeterIndex < uppercaseQuery.Length);			

            // Sort through query to find attributes and operators, split on delimeters: 
            foreach (string part in queryParts) 
			{
				if (!string.IsNullOrEmpty(part))
				{
					switch (part)
					{
						case " ":
							break;						
						case "OR":
							InfixTokens.Add(new Operator_Token("OR", ORPrecedence, (a, b) => { return a || b; }));
							break;
						case "AND":
							InfixTokens.Add(new Operator_Token("AND", ANDPrecedence,  (a, b) => { return a && b; }));
							break;
						case "(":
							InfixTokens.Add(new Left_Parenthesis_Token());
							break;
						case ")":
							InfixTokens.Add(new Right_Parenthesis_Token());
							break;

						default:
							InfixTokens.Add(new Attribute_Token(part));
							break;   							
					}
				}
			}

			// Return split query, InfixTokens list:
			return InfixTokens;
		}


		/// <summary>
		/// This method comes from the Shunting Algorithm by Edsger Dijkstra.
		/// It takes the infixTokens list and reorganises it using a stack to sort the attributes with their operators for searching through the unrented
		/// vehicles list.
		/// 
		/// References:
		/// Based on the ShuntingYard method given in the worksheet 9 solutions that has ben adapted for use with the "AND" and "OR" operators, etc..
		/// Source: http://mathcenter.oxford.emory.edu/site/cs171/shuntingYardAlgorithm/
		/// </summary>
		/// 
		/// <param name="infixTokens"> The split query list to sort. </param>
		/// <returns> Returns the list of attributes sorted with their respective operators (postfixTokens),
		/// or null if invalid attributes/operators. </returns>
		public static List<IToken> ShuntingYard(List<IToken> infixTokens)
		{
			// Variables:
			List<IToken> postfixTokens = new List<IToken>();
			Stack<IToken> operatorStack = new Stack<IToken>();

			int countLeft = 0;
			int countRight = 0;

			string stringItem;

			// Go through infixTokens:
			foreach (IToken token in infixTokens)
			{
				// Attribute:
				if (token is Attribute_Token)
				{
					// Add attribute to posfixTokens list:
					postfixTokens.Add(token);
				}
				// Operator: (AND or OR)
				else if (token is Operator_Token thisOperatorToken)
				{
					// Add after attribute and after operators with lower precedence:
					while (operatorStack.Any() && operatorStack.Peek() is Operator_Token thisNewOperatorToken
						&& thisNewOperatorToken.Precedence >= thisNewOperatorToken.Precedence)
					{
						postfixTokens.Add(operatorStack.Pop());
					}

					// Add to stack:
					operatorStack.Push(token);
				}
				// Parenthesis:
				else if (token is Left_Parenthesis_Token)
				{
					// Add to stack:
					operatorStack.Push(token);

					// Count parenthesis:
					countLeft += 1;
				}
				else if (token is Right_Parenthesis_Token)
				{                    
                    // Check if previous is left parenthesis:
                    try
                    {
						while (!(operatorStack.Peek() is Left_Parenthesis_Token))
						{
							// Add to stack:
							postfixTokens.Add(operatorStack.Pop());
						}

						operatorStack.Pop();

						// Count parenthesis:
						countRight += 1;
					}
					catch (InvalidOperationException)
					{
						// Print error message:
						Console.WriteLine("\n*** Error: Mismatched parenthesis in query. ***\n");

						// End search:
						return null;
					}
				}
			}

			// Add to stack:
			while (operatorStack.Any())
			{
				postfixTokens.Add(operatorStack.Pop());
			}

			// Check for mismatched parenthesis:
			if (countLeft != countRight)
			{
				// Print error message:
				Console.WriteLine("\n*** Error: Mismatched parenthesis in query. ***\n");

				// End search:
				return null;
			}			
			
			// Check operators and attrbiutes are all valid:
			foreach (var item in postfixTokens)
            {
				// Convert to string:
				stringItem = item.ToString();

				// Check if invalid:
				if (!stringItem.All(character => Char.IsWhiteSpace(character) || Char.IsLetterOrDigit(character)))
				{
					// Print error message:
					Console.WriteLine("\n*** Error: Invalid attribute(s) or operator(s) in query. ***\n");

					// End search:
					return null;
				}
            }

			// Return sorted postfixTokens list:
			return postfixTokens;
		}


		/// <summary>
		/// This method takes the sorted postfixTokens list and searches for the vehicles in the unrented vehicles list that have the attributes
		/// stated in the postfixTokens list.
		/// 
		/// References:
		/// Main structure.
		/// Soruce: https://csharpcodewhisperer.blogspot.com/2015/12/infix-notation-parser-via-shunting-yard.html
		/// </summary>
		/// 
		/// <param name="postfixToken"> The sorted query list for searching. </param>
		/// <param name="searchItem"> The attributes of the vehicle to check. </param>
		/// <returns> Return true if the vehicle has the attribute, returns false otherwise. </returns>
		public static bool Results(List<IToken> postfixToken, List<String> searchItem)
		{
			// Variables:
			Stack<bool> resultsStack = new Stack<bool>();
			Attribute_Token attribute;

			bool attributeExists;
			bool operatorExists;

			bool right;
			bool left;

			bool result;
 
			// Search through postfixToken list:
			foreach (IToken token in postfixToken)
			{
				// Attribute:
				if (token is Attribute_Token)
				{
					// Convert token to an Attribute_Token:
					attribute = new Attribute_Token(token.ToString());

					// Check is the attribute exists:
					attributeExists = searchItem.Contains(attribute.ToString());

					// Check if at the end of the list or continue:
					if (postfixToken.Count == 1)
					{
						return attributeExists;
					}
					else
					{
						// Add bool to results stack:
						resultsStack.Push(attributeExists);
					}
				}
				// Operator:
				else if (token is Operator_Token thisOperatorToken)
				{
					// Get what is  
					right = resultsStack.Pop();
					left = resultsStack.Pop();

					// Check if the operator exists:
					operatorExists = thisOperatorToken.Evaluate(right, left);
					
					// Add operator to stack:
					resultsStack.Push(operatorExists);
				}
			}

			// Get the resulting Attribute_Token from stack:
			result = resultsStack.Pop();

			// Return the result:
			return result;		
		}


	}//end Search class
}//end namespace
