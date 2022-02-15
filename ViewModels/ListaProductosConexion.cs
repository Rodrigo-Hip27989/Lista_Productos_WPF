using SpreadsheetLight;
using System;
using System.Windows;
using WPF_Productos.Core;
using WPF_Productos.Models;

namespace WPF_Productos.ViewModels
{
    public partial class ListaProductosViewModel : ViewModelBase
    {
        private SLDocument sl;
        private void MostrarProductosExcel()
        {
            Producto productoFilaActual;
            int countRow = 2;
            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(countRow, 1)))
            {
                productoFilaActual = new Producto();
                productoFilaActual.Nombre = sl.GetCellValueAsString(countRow, 1);
                productoFilaActual.Cantidad = sl.GetCellValueAsString(countRow, 2);
                productoFilaActual.Medida = sl.GetCellValueAsString(countRow, 3);
                productoFilaActual.Precio = sl.GetCellValueAsDouble(countRow, 4);
                productoFilaActual.Fecha = sl.GetCellValueAsDateTime(countRow, 5);
                ProductosExcel_View.Add(productoFilaActual);
                countRow++;
            }
        }
        private void ActualizarProductosExcel()
        {
            try
            {
                sl = new SLDocument();
                //Cabeceras de la Tabla (Para el docuemento Excel)
                sl.SetCellValue(1, 1, "Nombre");
                sl.SetCellValue(1, 2, "Cantidad");
                sl.SetCellValue(1, 3, "Medida");
                sl.SetCellValue(1, 4, "Precio");
                sl.SetCellValue(1, 5, "Fecha");

                int countRow = 2;
                foreach (Producto productoFilaActual in productosExcel_View)
                {
                    sl.SetCellValue(countRow, 1, productoFilaActual.Nombre);
                    sl.SetCellValue(countRow, 2, productoFilaActual.Cantidad);
                    sl.SetCellValue(countRow, 3, productoFilaActual.Medida);
                    sl.SetCellValue(countRow, 4, productoFilaActual.Precio);
                    sl.SetCellValue(countRow, 5, productoFilaActual.Fecha);
                    countRow++;
                }
                sl.SaveAs(RutaExcel);
                MessageBox.Show("Actualización Exitosa!!!", tituloApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio una Excepción: " + ex.Message, tituloApp);
            }
        }
    }
}
