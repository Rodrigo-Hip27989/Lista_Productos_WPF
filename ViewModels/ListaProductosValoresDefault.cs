using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WPF_Productos.Core;

namespace WPF_Productos.ViewModels
{
    public partial class ListaProductosViewModel : ViewModelBase
    {
        private static string rutaDelProyecto = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString();
        private static string rutaDocumento = File.ReadAllText(rutaDelProyecto + @"\RutaDocumentoExcel.txt");
        public string tituloApp = "App Lista Productos";
        public ListaProductosViewModel()
        {
            ReiniciarPantalla();
        }
    }
}
