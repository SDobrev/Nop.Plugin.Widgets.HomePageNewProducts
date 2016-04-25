using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.HomePageNewProducts.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.HomePageNewProducts.ProductsCount")]
        public byte ProductsCount { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.HomePageNewProducts.WidgetZone")]
        public WidgetZone WidgetZone { get; set; }
    }
}
