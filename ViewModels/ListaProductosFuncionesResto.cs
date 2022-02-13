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
        }
        private void ReiniciarPantalla()
        {
            ProductosExcel_View = new ProductosCollection();
            try
            {
                if (File.Exists(rutaDocumento))
                {
                    RutaExcel = rutaDocumento;
                    sl = new SLDocument(RutaExcel);
                    MessageBox.Show("Se ha reiniciado la pantalla");
                }
                else
                {
                    MessageBox.Show("No se encontro la ruta seleccionada: \n" + RutaExcel.ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Descripción del Error: \n\n" + e, "App Lista Productos");
            }
        }
        #endregion
        public ListaProductosViewModel()
        {
            ReiniciarPantalla();
        }
    }
}
