namespace Web.Models
{
    public class LoginViewModel
    {
        public string? ErrorClaveOMail { get; set; }
        public bool Recuerdame { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}