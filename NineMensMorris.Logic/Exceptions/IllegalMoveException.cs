using System;

namespace NineMensMorris.Logic.Exceptions
{
    public class IllegalMoveException : Exception
    {
        public IllegalMoveException()
        {
        }

        public IllegalMoveException(string message) : base(message)
        {
        }
    }
}