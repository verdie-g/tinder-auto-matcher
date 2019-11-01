using System;

namespace Tinder.Exceptions
{
    public class TinderException : Exception
    {
        public TinderException() { }
        public TinderException(string message) : base(message) { }
    }
}