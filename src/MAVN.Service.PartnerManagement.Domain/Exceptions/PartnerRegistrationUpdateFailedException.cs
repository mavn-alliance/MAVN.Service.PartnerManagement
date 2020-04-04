using System;

namespace MAVN.Service.PartnerManagement.Domain.Exceptions
{
    public class PartnerRegistrationUpdateFailedException : Exception
    {
        public PartnerRegistrationUpdateFailedException()
        {
        }

        public PartnerRegistrationUpdateFailedException(string message): base(message)
        {
        }
    }
}
