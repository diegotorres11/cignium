using System;

namespace Domain.Exceptions
{
    public class SearchTermException : Exception
    {
        public SearchTermException(string message) : base(message)
        {

        }
    }
}
