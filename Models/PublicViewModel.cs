using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Catalog;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.HomePageNewProducts.Models
{
    public class PublicViewModel : BaseNopModel
    {
        public IEnumerable<ProductOverviewModel> Products { get; set; }
    }
}
