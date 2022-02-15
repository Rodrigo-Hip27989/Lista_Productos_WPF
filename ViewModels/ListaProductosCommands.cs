using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Input;
using WPF_Productos.Core;
using WPF_Productos.Models;

namespace WPF_Productos.ViewModels
{
    public partial class ListaProductosViewModel : ViewModelBase
    {
        private ICommand mostrarProductos_Command;
        private ICommand cambiarRutaExcel_Command;
        private ICommand reiniciarPantalla_Command;
        private ICommand actualizarProductos_Command;

        private ICommand nuevoProducto_Command;
        private ICommand agregarProducto_Command;
        private ICommand eliminarProducto_Command;

        #region COMANDOS - OPCIONES GENERALES
        public ICommand MostrarProductos_Command
        {
            get
            {
                if (mostrarProductos_Command == null)
                {
                    mostrarProductos_Command = new RelayCommand(new Action(MostrarProductosExcel), () => PuedoMostrarProductos());
                }
                return mostrarProductos_Command;
            }
        }
        public ICommand CambiarRutaExcel_Command
        {
            get
            {
                if (cambiarRutaExcel_Command == null)
                {
                    cambiarRutaExcel_Command = new RelayCommand(new Action(CambiarRutaExcel));
                }
                return cambiarRutaExcel_Command;
            }
        }
        public ICommand ReiniciarPantalla_Command
        {
            get
            {
                if (reiniciarPantalla_Command == null)
                {
                    reiniciarPantalla_Command = new RelayCommand(new Action(ReiniciarPantalla));
                }
                return reiniciarPantalla_Command;
            }
        }
        public ICommand ActualizarProductos_Command
        {
            get
            {
                if (actualizarProductos_Command == null)
                {
                    actualizarProductos_Command = new RelayCommand(new Action(ActualizarProductosExcel), () => PuedoActualizarProductos());
                }
                return actualizarProductos_Command;
            }
        }
        #endregion

        #region COMANDOS - OPCIONES DE CRUD PARA EL EXCEL
        public ICommand NuevoProducto_Command
        {
            get
            {
                if (nuevoProducto_Command == null)
                {
                    nuevoProducto_Command = new RelayCommand(new Action(CrearNuevoProducto));
                }
                return nuevoProducto_Command;
            }
        }
        public ICommand AgregarProducto_Command
        {
            get
            {
                if (agregarProducto_Command == null)
                {
                    agregarProducto_Command = new RelayCommand(new Action(AgregarProducto), () => PuedoAgregarProducto());
                }
                return agregarProducto_Command;
            }
        }
        public ICommand EliminarProducto_Command
        {
            get
            {
                if (eliminarProducto_Command == null)
                {
                    eliminarProducto_Command = new RelayCommand(new Action(EliminarProducto), () => PuedoEliminarProducto());
                }
                return eliminarProducto_Command;
            }
        }
        #endregion

        #region CONDICIONES PARA EJECUTAR COMMANDOS
        private bool PuedoMostrarProductos()
        {
            return ((File.Exists(RutaExcel)) && ((ProductosExcel_View.Count<1)));
        }
        private bool PuedoActualizarProductos()
        {
            //(ProductosExcel_View.Count > 0) Esta verificación no es necesario si se lleva un registro de cambios en el documento de otra manera
            return ((ProductosExcel_View != null) && (ProductosExcel_View.Count > 0));
        }
        private bool PuedoAgregarProducto()
        {
            return (!ProductosExcel_View.Contains(Current__Producto) && Current__Producto != null);
        }
        private bool PuedoEliminarProducto()
        {
            return (ProductosExcel_View.Contains(Current__Producto));
        }

        #endregion
    }
}
