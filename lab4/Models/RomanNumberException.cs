using System;

namespace lab4.Models
{
    public class RomanNumberException : Exception
    {
        public RomanNumberException(string message)
            : base(message)
        { }
    }
}
