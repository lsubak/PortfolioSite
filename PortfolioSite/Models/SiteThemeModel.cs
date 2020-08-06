using PortfolioSite.Models.Enums;

namespace PortfolioSite.Models
{
    public class SiteThemeModel
    {
        public SiteThemeModel()
        {
            SiteTheme = SiteTheme.Default;
        }

        public SiteThemeModel(SiteTheme siteTheme)
        {
            SiteTheme = siteTheme;
        }

        public SiteTheme SiteTheme;

        public string SiteThemeLink
        {
            get {
                switch (SiteTheme)
                {
                    case SiteTheme.Red:
                        return "/css/red-theme.css";
                    case SiteTheme.Default:
                    default:
                        return "/css/site.css";
                }
            }
        }
    }
}
