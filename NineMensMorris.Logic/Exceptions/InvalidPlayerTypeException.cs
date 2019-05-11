using System;

namespace NineMensMorris.Logic.Exceptions
{
    public class InvalidPlayerTypeException : Exception
    {
        public InvalidPlayerTypeException()
        {
        }

        public InvalidPlayerTypeException(string message) : base(message)
        {
        }
    }
}