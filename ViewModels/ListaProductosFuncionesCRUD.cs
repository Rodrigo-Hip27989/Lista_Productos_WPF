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
            //OnPropertyChanged("ProductosExcel_View");
        }
        private void AgregarProducto()
        {
            /*Producto nuevoProducto = new Producto(
                Current__Producto.Nombre,
                Current__Producto.Cantidad,
                Current__Producto.Medida,
                Current__Producto.Precio,
                Current__Producto.Fecha
            );*/
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
