using System;

namespace SparkleTesting.Application.Exceptions
{
    class NotFoundException : Exception
    {
        public NotFoundException(string name) : base($"{name} не найден.")
        {

        }
    }
}
