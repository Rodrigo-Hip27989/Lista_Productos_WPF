using System;
using System.Collections.ObjectModel;

namespace WPF_Productos.Models
{
    public class ProductosCollection : ObservableCollection<Producto>
    {
    }
    public class Producto
    {
        private string nombre;
        private string cantidad;
        private string medida;
        private double precio;
        private DateTime fecha;

        public Producto()
        {

        }
        public Producto(string nombre, string cantidad, string medida, double precio, DateTime fecha)
        {
            Nombre = nombre;
            Cantidad = cantidad;
            Medida = medida;
            Precio = precio;
            Fecha = fecha;
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        public string Medida
        {
            get { return medida; }
            set { medida = value; }
        }
        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public override string ToString()
        {
            return "\n - Nombre: " + Nombre + " - Cantidad: " + Cantidad + "\n - Medida: " + Medida + "\n  -  Precio" + Precio + "\n  -  Fecha" + fecha;
        }
    }
}
