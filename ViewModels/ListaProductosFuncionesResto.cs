using Microsoft.Win32;
using System.Windows;
using WPF_Productos.Core;
using WPF_Productos.Models;

namespace WPF_Productos.ViewModels
{
    public partial class ListaProductosViewModel : ViewModelBase
    {
        #region FUNCIONES DE COMANDOS - OPCIONES GENERALES
        private void CambiarRutaExcel()
        {
            OpenFileDialog selectorArchivo = new OpenFileDialog();
            selectorArchivo.ShowDialog();
            if (selectorArchivo.FileName != null && selectorArchivo.FileName != "")
            {
                ProductosExcel_View.Clear();
                RutaExcel = selectorArchivo.FileName;
            }
        }
        private void ReiniciarPantalla()
        {
            ProductosExcel_View = new ProductosCollection();
            SumaPrecios = 0;
            MessageBoxTemp.Show("Se ha reiniciado la pantalla", tituloApp, 2, false);
        }
        #endregion
    }
}
