using Microsoft.Win32;

using SpreadsheetLight;

using System;
using System.IO;
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
        private void MostrarProductosExcel()
        {
            LoadContenidoTabla();
            OnPropertyChanged("ProductosExcel_View");
        }
        private void ReiniciarPantalla()
        {
            ProductosExcel_View = new ProductosCollection();
            RutaExcel = rutaDocumento;
            sl = new SLDocument(RutaExcel);
            MessageBox.Show("Se ha reiniciado la pantalla", tituloApp);
        }
        #endregion
        public ListaProductosViewModel()
        {
            ReiniciarPantalla();
        }
    }
}
