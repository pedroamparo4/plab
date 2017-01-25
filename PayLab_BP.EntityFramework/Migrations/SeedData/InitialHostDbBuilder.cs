using PayLab_BP.EntityFramework;
using EntityFramework.DynamicFilters;

namespace PayLab_BP.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly PayLab_BPDbContext _context;

        public InitialHostDbBuilder(PayLab_BPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
