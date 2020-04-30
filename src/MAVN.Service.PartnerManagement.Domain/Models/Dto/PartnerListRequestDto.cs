using System;

namespace MAVN.Service.PartnerManagement.Domain.Models.Dto
{
    public class PartnerListRequestDto
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public string Name { get; set; }

        public Guid? CreatedBy { get; set; }

        public Vertical? Vertical { get; set; }
    }
}
