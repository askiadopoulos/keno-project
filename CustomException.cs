using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoProject
{
    // Class CustomException is responsible for delivering a custom message for a
    // corresponding exception. The exceptions occur during the process of input data.
    class CustomException : Exception
    {
        // Constructor with Parameters:
        // When creating a new CustomException, it initiates with the following values:
        // A string parameter that takes a custom message in case of a throw new exception.
        // The specific parameter of the method, is passed in the base class Exception.
        public CustomException(string message) : base(message)
        {

        }
        
        // Prints messages according with the corresponding exceptions.
        public static void CheckExceptions(Exception ex)
        {
            if (ex.GetType().ToString().Equals("System.FormatException") || ex.GetType().ToString().Equals("System.OverflowException"))
            {
                Console.WriteLine("Only positive integer numbers are allowed !");
            }
            
        }

    }
}
