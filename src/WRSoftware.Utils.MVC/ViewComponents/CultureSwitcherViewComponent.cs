using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using WRSoftware.Utils.Common.Models;

namespace WRSoftware.Utils.MVC.ViewComponents
{
    public class CultureSwitcherViewComponent : ViewComponent
    {
        /// <summary>
        /// The localization options
        /// </summary>
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="CultureSwitcherViewComponent"/> class.
        /// </summary>
        /// <param name="localizationOptions">The localization options.</param>
        public CultureSwitcherViewComponent(IOptions<RequestLocalizationOptions> localizationOptions) =>
            _localizationOptions = localizationOptions;

        /// <summary>
        /// Invokes this instance.
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            var cultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
            CultureSwitcherModel cultureSwitcherModel = new CultureSwitcherModel();
            foreach (var item in _localizationOptions.Value.SupportedUICultures.ToList())
            {
                var culture = new SupportedCultureItem
                {
                    CultureInfo = item,
                    ImageCss = "flag-icon" + (item.Name.Contains("-")
                        ? item.Name.Substring(item.Name.LastIndexOf("-",
                            StringComparison.InvariantCultureIgnoreCase))
                        : item.Name).ToLower()
                };
                cultureSwitcherModel.SupportedCultures.Add(culture);
            }

            cultureSwitcherModel.CurrentUICulture =
                cultureSwitcherModel.SupportedCultures.FirstOrDefault(x =>
                    x.CultureInfo.Name == cultureFeature.RequestCulture.UICulture.Name);

            return View(cultureSwitcherModel);
        }
    }
}
