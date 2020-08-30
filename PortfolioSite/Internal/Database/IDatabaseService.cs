using PortfolioSite.Models;

namespace PortfolioSite.Internal.Database
{
    public interface IDatabaseService
    {
        void SaveContactMessage(ContactForm form);

        void SaveErrorMessages(string message);
    }
}
