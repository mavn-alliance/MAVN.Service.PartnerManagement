using System;
using System.Threading.Tasks;
using MAVN.Service.PartnerManagement.Domain.Models.Enums;

namespace MAVN.Service.PartnerManagement.Domain.Services
{
    public interface IPartnerAbilityCheckService
    {
        Task<PartnerInabilityErrorCodes> CheckPartnerAbility(PartnerAbility abilityToCheck, Guid partnerId);
    }
}
