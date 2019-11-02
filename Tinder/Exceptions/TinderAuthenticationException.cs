using System;

namespace Tinder.Exceptions
{
    public class TinderAuthenticationException : TinderException
    {
        public TinderAuthenticationException() { }
        public TinderAuthenticationException(string message) : base(message) { }
        public TinderAuthenticationException(string message, Exception inner) : base(message, inner) { }
    }
}