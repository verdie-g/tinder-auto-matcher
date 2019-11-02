using System;

namespace Tinder.Exceptions
{
    public class TinderSerializationException : TinderException
    {
        public TinderSerializationException() { }
        public TinderSerializationException(string message) : base(message) { }
        public TinderSerializationException(string message, Exception inner) : base(message, inner) { }
    }
}