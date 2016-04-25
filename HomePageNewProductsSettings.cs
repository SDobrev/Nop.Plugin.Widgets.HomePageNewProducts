using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.HomePageNewProducts
{
    public class HomePageNewProductsSettings : ISettings
    {
        public WidgetZone WidgetZone { get; set; }

        public byte ProductsCount { get; set; }
    }
}
