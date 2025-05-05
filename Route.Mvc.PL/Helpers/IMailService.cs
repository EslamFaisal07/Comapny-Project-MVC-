using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Route.Mvc.PL.Utilites;
using Email = Route.Mvc.PL.Utilites.Email;

namespace Route.Mvc.PL.Helpers
{
    public interface IMailService
    {

        void Send(Email email);
    }
}
