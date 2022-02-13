using System.IO;
using System.Windows;
using WPF_Productos.Core;
using WPF_Productos.Models;

namespace WPF_Productos.ViewModels
{
    public partial class ListaProductosViewModel : ViewModelBase
    {
        private string rutaExcel;
        private Producto current__Producto;
        private ProductosCollection productosExcel_View;
        public Producto Current__Producto
        {
            get { return current__Producto; }
            set { current__Producto = value; OnPropertyChanged("Current__Producto"); }
        }
        public ProductosCollection ProductosExcel_View
        {
            get { return productosExcel_View; }
            set { productosExcel_View = value; OnPropertyChanged("ProductosExcel_View"); OnPropertyChanged("Current__Producto"); }
        }
        public string RutaExcel 
        {
            get { return rutaExcel; }
            set {
                if (!File.Exists(value))
                {
                    string mensaje = "\n";
                    mensaje += "El archivo no existe " + "\n\n";
                    mensaje += "La rutaExcel proporcionada fue: " + value + "\n\n";
                    //MessageBox.Show(mensaje, "WPF_Productos", MessageBoxButton.OK, MessageBoxImage.Error, 0, 0);
                }
                else { rutaExcel = value; }
                OnPropertyChanged("RutaExcel");
            }
        }
    }
}