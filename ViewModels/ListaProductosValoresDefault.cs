using System.IO;
using WPF_Productos.Core;
using WPF_Productos.Models;

namespace WPF_Productos.ViewModels
{
    public partial class ListaProductosViewModel : ViewModelBase
    {
        private static string rutaDelProyecto = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString();
        private static string rutaDocumento = File.ReadAllText(rutaDelProyecto + @"\RutaDocumentoExcel.txt");
        public string tituloApp = "App Lista Productos";
        public ListaProductosViewModel()
        {
            ProductosExcel_View = new ProductosCollection();
            RutaExcel = rutaDocumento;
        }
    }
}
