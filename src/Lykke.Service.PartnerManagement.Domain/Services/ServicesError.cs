namespace Lykke.Service.PartnerManagement.Domain.Services
{
    public enum ServicesError
    {
        /// <summary>
        /// No error :)
        /// </summary>
        None = 0,

        /// <summary>
        /// Login not found
        /// </summary>
        LoginNotFound,

        /// <summary>
        /// Password mismatch (wrong password)
        /// </summary>
        PasswordMismatch,

        /// <summary>
        /// Partner is registered with another password
        /// </summary>
        RegisteredWithAnotherPassword,

        /// <summary>
        /// Partner is already registered
        /// </summary>
        AlreadyRegistered,

        /// <summary>
        /// Invalid credentials
        /// </summary>
        InvalidCredentials
    }
}
