using JetBrains.Annotations;

namespace MAVN.Service.PartnerManagement.Client.Enums
{
    /// <summary>
    /// Enum for admin management errors.
    /// </summary>
    [PublicAPI]
    public enum PartnerManagementError
    {
        /// <summary>
        /// No error
        /// </summary>
        None = 0,
        /// <summary>
        /// Partner was not found
        /// </summary>
        PartnerNotFound,
        /// <summary>
        /// Login for the partner was not found
        /// </summary>
        LoginNotFound,
        /// <summary>
        /// Partner with this id is already registered
        /// </summary>
        AlreadyRegistered,
        /// <summary>
        /// Registration of the partner failed
        /// </summary>
        RegistrationFailed,
        /// <summary>
        /// Location's external identifier is not unique
        /// </summary>
        LocationExternalIdNotUnique
    }
}
