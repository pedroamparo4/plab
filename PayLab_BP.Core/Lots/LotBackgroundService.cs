using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayLab_BP.Lots
{
    public class LotBackgroundService : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IRepository<Lot> _lotRepository;
        public LotBackgroundService(AbpTimer timer, IRepository<Lot> lotRepository)
            : base(timer)
        {
            _lotRepository = lotRepository;
            Timer.Period = 5000;
        }

        [UnitOfWork]
        protected override void DoWork()
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MustHaveTenant, AbpDataFilters.MayHaveTenant))
            {
                var lotNumberOfToday = String.Format("{0}", DateTime.Now.ToString("yyyyMMdd"));

                try
                {
                    var lastLot = _lotRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
                    var lastLotNumber = (lastLot == null ? string.Empty : _lotRepository.Get(lastLot.Id).Number);

                    if (lotNumberOfToday != lastLotNumber)
                    {
                        Lot _todayLot = new Lot();
                        _lotRepository.Insert(_todayLot);
                        Logger.InfoFormat("New Lot Added to DB: {0}, at {1}", _todayLot.Number, DateTime.Now);
                    }
                }
                catch (Exception ex)
                {
                    Logger.InfoFormat("Could not create lot with number: {0}, at {1} | Extra info: {2}", lotNumberOfToday, DateTime.Now, ex.Message);
                }

                CurrentUnitOfWork.SaveChanges();
            }
        }
    }
}
