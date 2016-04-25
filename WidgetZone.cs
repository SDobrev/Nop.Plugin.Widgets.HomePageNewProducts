using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Widgets.HomePageNewProducts
{
    public enum WidgetZone
    {
        [Display(Name = "Home page top")]
        home_page_top = 0,

        [Display(Name = "Home page before categories")]
        home_page_before_categories = 1,

        [Display(Name = "Home page before products")]
        home_page_before_products = 2,

        [Display(Name = "Home page before best sellers")]
        home_page_before_best_sellers = 3,

        [Display(Name = "Home page before news")]
        home_page_before_news = 4,

        [Display(Name = "Home page before poll")]
        home_page_before_poll = 5,

        [Display(Name = "Home page before bottom")]
        home_page_bottom = 6
    }
}
