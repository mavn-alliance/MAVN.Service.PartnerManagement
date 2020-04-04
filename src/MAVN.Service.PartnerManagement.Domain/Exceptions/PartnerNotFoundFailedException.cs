using System;

namespace MAVN.Service.PartnerManagement.Domain.Exceptions
{
    public class PartnerNotFoundFailedException: Exception
    {
        public PartnerNotFoundFailedException()
        {
        }

        public PartnerNotFoundFailedException(string message): base(message)
        {
        }
    }
}
