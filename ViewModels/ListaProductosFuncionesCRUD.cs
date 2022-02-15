using System;
using WPF_Productos.Core;
using WPF_Productos.Models;

namespace WPF_Productos.ViewModels
{
    public partial class ListaProductosViewModel : ViewModelBase
    {
        #region FUNCIONES DE COMANDOS - OPCIONES DE CRUD PARA EL EXCEL
        private void CrearNuevoProducto()
        {
            Current__Producto = new Producto(
                Guid.NewGuid().ToString().Substring(0, 5),
                "1",
                "Kilo(s)",
                10.0f,
                DateTime.Parse(DateTime.Now.ToShortDateString())
            );
        }
        private void AgregarProducto()
        {
            ProductosExcel_View.Add(Current__Producto);
            Current__Producto = ProductosExcel_View[ProductosExcel_View.Count - 1];
        }
        private void EliminarProducto()
        {
            ProductosExcel_View.Remove(Current__Producto);
            if (ProductosExcel_View.Count > 0)
            {
                Current__Producto = ProductosExcel_View[ProductosExcel_View.Count - 1];
            }
        }
        #endregion
    }
}
