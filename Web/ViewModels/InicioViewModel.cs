using System.ComponentModel.DataAnnotations;
using Web.Data.Entities;

namespace Web.ViewModels
{
    public class InicioViewModel
    {
        public string Token { get; set; }

        public static implicit operator InicioViewModel(Login v)
        {
            var loginViewModel = new InicioViewModel();
            loginViewModel.Token = v.Token;
            return loginViewModel;
        }
    }
}
