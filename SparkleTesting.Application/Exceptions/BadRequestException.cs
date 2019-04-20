using System;
using System.Collections.Generic;
using System.Text;

namespace SparkleTesting.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string msg) : base(msg)
        {

        }
    }
}
