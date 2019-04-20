using System;

namespace SparkleTesting.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name) : base($"{name} не найден.")
        {

        }
    }
}
