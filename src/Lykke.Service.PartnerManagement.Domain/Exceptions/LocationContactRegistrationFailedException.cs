using System;

namespace Lykke.Service.PartnerManagement.Domain.Exceptions
{
    public class LocationContactRegistrationFailedException: Exception
    {
        public LocationContactRegistrationFailedException()
        {
        }

        public LocationContactRegistrationFailedException(string message): base(message)
        {
        }
    }
}
