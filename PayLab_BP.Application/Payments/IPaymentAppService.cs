using System.Threading.Tasks;
using Abp.Application.Services;
using System.Collections.Generic;
using PayLab_BP.Payments.Dto;

namespace PayLab_BP.Users
{
    public interface IPaymentAppService : IApplicationService
    {
        Task <IEnumerable<PaymentDto>> GetPayments(string dtFrom, string dtTo, int? lotId = null);

        string GetPaymentTypeName(int id);

        IEnumerable<PaymentDto> GetTodayPayments();
    }
}