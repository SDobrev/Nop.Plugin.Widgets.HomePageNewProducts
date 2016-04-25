using System.Collections.Generic;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using System.Web.Routing;

namespace Nop.Plugin.Widgets.HomePageNewProducts
{
    public class HomePageNewProductsPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ISettingService _settingService;

        public HomePageNewProductsPlugin(ISettingService settingService)
        {
            this._settingService = settingService;
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "WidgetsHomePageNewProducts";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Widgets.HomePageNewProducts.Controllers" }, { "area", null } };
        }

        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "WidgetsHomePageNewProducts";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces", "Nop.Plugin.Widgets.HomePageNewProducts.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }

        public IList<string> GetWidgetZones()
        {
            return new List<string> { this._settingService.GetSettingByKey<string>("HomePageNewProductsSettings.WidgetZone") };
        }

        public override void Install()
        {
            var defaultSettings = new HomePageNewProductsSettings
            {
                WidgetZone = WidgetZone.home_page_before_news,
                ProductsCount = 10
            };
            _settingService.SaveSetting(defaultSettings);

            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.HomePageNewProducts.WidgetZone", "Widget Zone");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.HomePageNewProducts.ProductsCount", "Products Count");

            base.Install();
        }

        public override void Uninstall()
        {
            _settingService.DeleteSetting<HomePageNewProductsSettings>();

            this.DeletePluginLocaleResource("Plugins.Widgets.HomePageNewProducts.WidgetZone");
            this.DeletePluginLocaleResource("Plugins.Widgets.HomePageNewProducts.ProductsCount");

            base.Uninstall();
        }
    }
}
