using System.Threading.Tasks;
using Abp.Application.Services;
using System.Collections.Generic;
using PayLab_BP.Lots.Dto;
using PayLab_BP.Payments.Dto;

namespace PayLab_BP.Users
{
    public interface ILotAppService : IApplicationService
    {
        IEnumerable<LotDto> GetLots(string dtFrom, string dtTo);
        IEnumerable<PaymentDto> GetLotPayments(int lotId, string dtFrom, string dtTo);
    }
}