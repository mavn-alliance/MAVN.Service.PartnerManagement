using System;

namespace Lykke.Service.PartnerManagement.Domain.Exceptions
{
    public class ClientAlreadyExistException : Exception
    {
        public ClientAlreadyExistException()
        {
        }

        public ClientAlreadyExistException(string message): base(message)
        {
        }
    }
}
