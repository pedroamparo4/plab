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
using PayLab_BP.AbpAuditLogs;

namespace PayLab_BP.Users
{
    public class LotAppService : PayLab_BPAppServiceBase, ILotAppService
    {
        private readonly IRepository<Lot> _lotRepository;
        private readonly ILogAppService _logService;
        private readonly IRepository<Payment> _paymentRepository;
        private readonly ITenantAppService _tenantService;
        private readonly IPaymentAppService _paymentService;

        public LotAppService(IRepository<Lot> lotRepository, IRepository<Payment> paymentRepository, ITenantAppService tenantService, ILogAppService logService, IPaymentAppService paymentService)
        {
            _lotRepository = lotRepository;
            _paymentRepository = paymentRepository;
            _tenantService = tenantService;
            _logService = logService;
            _paymentService = paymentService;
        }

        public IEnumerable<LotDto> GetLots(string dtFrom, string dtTo)
        {
            int? tenantId = AbpSession.TenantId;
            IQueryable<LotDto> lotList;
            DateTime dtF;
            DateTime dtT;

            try
            {
                /*
                dtF = Convert.ToDateTime(dtFrom);
                dtT = Convert.ToDateTime(dtTo);
                */
            }
            catch
            {
                throw new Exception("Could not convert data to date format");
            }

            if(tenantId == null)
            {
                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MayHaveTenant))
                {
                    lotList = _lotRepository.GetAllList().AsQueryable().Select(s => new LotDto
                    {
                        Id = s.Id,
                        DateFrom = s.DateFrom,
                        DateTo = s.DateTo,
                        Number = s.Number,
                        Payments = GetLotPayments(s.Id)
                    });
                }
            }
            else
            {
                lotList = _lotRepository.GetAllList().AsQueryable().Select(s => new LotDto
                {
                    Id = s.Id,
                    DateFrom = s.DateFrom,
                    DateTo = s.DateTo,
                    Number = s.Number,
                    Payments = GetLotPayments(s.Id)
                });
            }

            return lotList;
        }

        private IEnumerable<PaymentDto> GetLotPayments(int lotId)
        {
            int? tenantId = AbpSession.TenantId;
            List<Payment> paymentList = new List<Payment>();

            paymentList = _paymentRepository.GetAllList(x => x.LotId == lotId);

            return paymentList.Select(s => new PaymentDto
            {
                Amount = s.Amount,
                ContractNumber = s.ContractNumber,
                Date = s.Date.Value.ToShortDateString(),
                Time = s.Date.Value.ToShortTimeString(),
                TransactionNumber = s.TransactionNumber,
                LotId = s.LotId,
                TenantName = _tenantService.GetTenantName(s.TenantId),
                PaymentType = _paymentService.GetPaymentTypeName(s.PaymentTypeId)
                
            });
        }

        public IEnumerable<PaymentDto> GetLotPayments(int lotId, string dtFrom, string dtTo)
        {
            int? tenantId = AbpSession.TenantId;
            List<Payment> paymentList = new List<Payment>();
            DateTime dtF;
            DateTime dtT;

            try
            {
                dtF = Convert.ToDateTime(dtFrom);
                dtT = Convert.ToDateTime(dtTo);
            }
            catch
            {
                throw new Exception("Could not convert data to date format");
            }

            if (tenantId == null)
            {// IS ADMIN

                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MayHaveTenant))
                {
                    paymentList = _paymentRepository.GetAllList().AsQueryable().Where(x => x.LotId == lotId && x.Date >= dtF && x.Date <= dtT).ToList();
                }
            }
            else
            {// IS TENANT
                paymentList = _paymentRepository.GetAllList().AsQueryable().Where(x => x.LotId == lotId && x.Date >= dtF && x.Date <= dtT).ToList();
            }

            return paymentList.Select(s => new PaymentDto
            {
                Amount = s.Amount,
                ContractNumber = s.ContractNumber,
                Date = s.Date.Value.ToShortDateString(),
                Time = s.Date.Value.ToShortTimeString(),
                TransactionNumber = s.TransactionNumber,
                LotId = s.LotId,
                TenantName = _tenantService.GetTenantName(s.TenantId),
                PaymentType = _paymentService.GetPaymentTypeName(s.PaymentTypeId)
            });
        }
    }
}