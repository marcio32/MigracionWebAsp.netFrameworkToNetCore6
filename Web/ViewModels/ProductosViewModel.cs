using Hanssens.Net;
using System.ComponentModel.DataAnnotations;
using Web.Data.Entities;

namespace Web.ViewModels
{
    public class ProductosViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Imagen { get; set; }
        public bool Activo { get; set; }
        public IFormFile File { get; set; }

        public static implicit operator ProductosViewModel(Productos v)
        {
            var usuViewModel = new ProductosViewModel();
            usuViewModel.Id = v.Id;
            usuViewModel.Descripcion = v.Descripcion;
            usuViewModel.Stock = v.Stock;
            usuViewModel.Precio = v.Precio;
            usuViewModel.Imagen = v.Imagen;
            usuViewModel.Activo = v.Activo;
            usuViewModel.File = v.File;
            return usuViewModel;
        }
    }
}
