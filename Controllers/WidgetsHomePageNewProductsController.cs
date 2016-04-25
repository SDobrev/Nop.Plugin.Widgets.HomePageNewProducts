using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Widgets.HomePageNewProducts.Models;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using System.Web.Mvc;
using Nop.Web.Extensions;
using Nop.Services.Security;
using Nop.Services.Tax;
using Nop.Services.Directory;
using Nop.Services.Media;
using Nop.Core.Caching;
using Nop.Core.Domain.Media;

namespace Nop.Plugin.Widgets.HomePageNewProducts.Controllers
{
    public class WidgetsHomePageNewProductsController : BasePluginController
    {
        readonly IProductService _productService;
        readonly ISettingService _settingService;
        readonly IStoreService _storeService;
        readonly IWorkContext _workContext;
        readonly ILocalizationService _localizationService;
        readonly IStoreContext _storeContext;
        readonly ICategoryService _categoryService;
        readonly ISpecificationAttributeService _specificationAttributeService;
        readonly IPriceCalculationService _priceCalculationService;
        readonly IPriceFormatter _priceFormatter;
        readonly IPermissionService _permissionService;
        readonly ITaxService _taxService;
        readonly ICurrencyService _currencyService;
        readonly IPictureService _pictureService;
        readonly IWebHelper _webHelper;
        readonly ICacheManager _cacheManager;
        readonly CatalogSettings _catalogSettings;
        readonly MediaSettings _mediaSettings;

        public WidgetsHomePageNewProductsController(
            IProductService productService,
            ISettingService settingService,
            IStoreService storeService,
            IWorkContext workContext,
            ILocalizationService localizationService,
            IStoreContext storeContext,
            ICategoryService categoryService,
             ISpecificationAttributeService specificationAttributeService,
             IPriceCalculationService priceCalculationService,
             IPriceFormatter priceFormatter,
             IPermissionService permissionService,
             ITaxService taxService,
             ICurrencyService currencyService,
             IPictureService pictureService,
             IWebHelper webHelper,
             ICacheManager cacheManager,
             CatalogSettings catalogSettings,
             MediaSettings mediaSettings)
        {
            _productService = productService;
            _settingService = settingService;
            _storeService = storeService;
            _workContext = workContext;
            _localizationService = localizationService;
            _storeContext = storeContext;
            _categoryService = categoryService;
            _specificationAttributeService = specificationAttributeService;
            _priceCalculationService = priceCalculationService;
            _priceFormatter = priceFormatter;
            _permissionService = permissionService;
            _taxService = taxService;
            _currencyService = currencyService;
            _pictureService = pictureService;
            _webHelper = webHelper;
            _cacheManager = cacheManager;
            _catalogSettings = catalogSettings;
            _mediaSettings = mediaSettings;
        }

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var homePageNewProductsSettings = _settingService.LoadSetting<HomePageNewProductsSettings>(storeScope);
            var model = new ConfigurationModel();
            model.WidgetZone = homePageNewProductsSettings.WidgetZone;
            model.ProductsCount = homePageNewProductsSettings.ProductsCount;

            model.ActiveStoreScopeConfiguration = storeScope;

            return View("~/Plugins/Widgets.HomePageNewProducts/Views/WidgetsHomePageNewProducts/Configure.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var homePageNewProductsSettings = _settingService.LoadSetting<HomePageNewProductsSettings>(storeScope);
            homePageNewProductsSettings.WidgetZone = model.WidgetZone;
            homePageNewProductsSettings.ProductsCount = model.ProductsCount;

            if (storeScope == 0)
            {
                _settingService.SaveSetting(homePageNewProductsSettings, x => x.ProductsCount, storeScope, false);
                _settingService.SaveSetting(homePageNewProductsSettings, x => x.WidgetZone, storeScope, false);
            }
            else if (storeScope > 0)
            {
                _settingService.DeleteSetting(homePageNewProductsSettings, x => x.ProductsCount, storeScope);
                _settingService.DeleteSetting(homePageNewProductsSettings, x => x.WidgetZone, storeScope);
            }

            _settingService.ClearCache();
            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        [ChildActionOnly]
        public ActionResult PublicInfo(string widgetZone, object additionalData = null)
        {
            var productsCount = this._settingService.GetSettingByKey<int>("HomePageNewProductsSettings.ProductsCount");
            var products = _productService.SearchProducts(
                orderBy: ProductSortingEnum.CreatedOn,
                pageSize: productsCount);

            var model = new PublicViewModel();
            model.Products = this.PrepareProductOverviewModels(_workContext,
                _storeContext, _categoryService, _productService, _specificationAttributeService,
                _priceCalculationService, _priceFormatter, _permissionService,
                _localizationService, _taxService, _currencyService,
                _pictureService, _webHelper, _cacheManager,
                _catalogSettings, _mediaSettings, products);


            return View("~/Plugins/Widgets.HomePageNewProducts/Views/WidgetsHomePageNewProducts/PublicInfo.cshtml", model);
        }
    }
}
