using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using PayLab_BP.Authorization;
using PayLab_BP.Users.Dto;
using Microsoft.AspNet.Identity;
using System;
using Abp.Domain.Uow;
using System.Linq;
using PayLab_BP.MultiTenancy;
using PayLab_BP.MultiTenancy.Dto;
using PayLab_BP.Lots.Dto;
using PayLab_BP.Payments.Dto;
using PayLab_BP.Lots;
using PayLab_BP.Payments;

namespace PayLab_BP.Users
{
    public class PaymentAppService : PayLab_BPAppServiceBase, IPaymentAppService
    {
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IRepository<PaymentType> _paymentTypeRepository;
        private readonly ITenantAppService _tenantService;

        public PaymentAppService(IRepository<Payment> paymentRepository, IRepository<PaymentType> paymentTypeRepository, ITenantAppService tenantService)
        {
            _paymentRepository = paymentRepository;
            _paymentTypeRepository = paymentTypeRepository;
            _tenantService = tenantService;
        }

        public Task<IEnumerable<PaymentDto>> GetPayments(string dtFrom, string dtTo, int? lotId = default(int?))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PaymentDto> GetTodayPayments()
        {
            DateTime today = DateTime.Now.Date;
            int? tenantId = AbpSession.TenantId;
            IQueryable<PaymentDto> payments;

            if (tenantId == null)
            {// IS ADMIN

                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MayHaveTenant))
                {
                    payments = _paymentRepository.GetAllList().AsQueryable().Where(x => x.Date == today).Select(s => new PaymentDto
                    {
                        Amount = s.Amount,
                        ContractNumber = s.ContractNumber,
                        Date = s.Date.Value.ToShortDateString(),
                        Time = s.Date.Value.ToShortTimeString(),
                        TransactionNumber = s.TransactionNumber,
                        LotId = s.LotId,
                        TenantName = _tenantService.GetTenantName(s.TenantId),
                        PaymentType = GetPaymentTypeName(s.PaymentTypeId)
                    });
                }
            }
            else
            {// IS TENANT
                payments = _paymentRepository.GetAllList().AsQueryable().Where(x => x.Date == today).Select(s => new PaymentDto
                {
                    Amount = s.Amount,
                    ContractNumber = s.ContractNumber,
                    Date = s.Date.Value.ToShortDateString(),
                    Time = s.Date.Value.ToShortTimeString(),
                    TransactionNumber = s.TransactionNumber,
                    LotId = s.LotId,
                    TenantName = _tenantService.GetTenantName(s.TenantId),
                    PaymentType = GetPaymentTypeName(s.PaymentTypeId)
                });
            }
            
            return payments;
        }

        public string GetPaymentTypeName(int id)
        {
            var paymentType = _paymentTypeRepository.Get(id);

            if (paymentType != null)
            {
                return paymentType.Name;
            }

            return "?";
        }


    }
}