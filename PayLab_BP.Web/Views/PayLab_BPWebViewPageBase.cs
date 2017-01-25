using Abp.Web.Mvc.Views;

namespace PayLab_BP.Web.Views
{
    public abstract class PayLab_BPWebViewPageBase : PayLab_BPWebViewPageBase<dynamic>
    {

    }

    public abstract class PayLab_BPWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected PayLab_BPWebViewPageBase()
        {
            LocalizationSourceName = PayLab_BPConsts.LocalizationSourceName;
        }
    }
}