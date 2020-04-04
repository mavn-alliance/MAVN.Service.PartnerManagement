using System;

namespace MAVN.Service.PartnerManagement.Domain.Exceptions
{
    public class PartnerRegistrationFailedException: Exception
    {
        public PartnerRegistrationFailedException()
        {
        }

        public PartnerRegistrationFailedException(string message): base(message)
        {
        }
    }
}
