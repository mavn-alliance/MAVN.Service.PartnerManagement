using System;

namespace Lykke.Service.PartnerManagement.Domain.Exceptions
{
    public class LocationContactUpdateFailedException : Exception
    {
        public LocationContactUpdateFailedException()
        {
        }

        public LocationContactUpdateFailedException(string message): base(message)
        {
        }
    }
}
