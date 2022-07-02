using System;
using System.Collections.ObjectModel;
using System.Windows;

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
            set
            {
                try
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        MessageBox.Show("\nEl NOMBRE es nulo o vacio: \n\n");
                    }
                    else
                    {
                        nombre = value;
                    }
                }
                catch (Exception e) { MessageBox.Show("\nError de asignación a NOMBRE: \n\n" + e.ToString()); }
            }
        }
        public string Cantidad
        {
            get { return cantidad; }
            set
            {
                try
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        //MessageBox.Show("La CANTIDAD es nulo o vacio: \n\n");
                        cantidad = ".";
                    }
                    else
                    {
                        cantidad = value;
                    }
                }
                catch (Exception e) { MessageBox.Show("\nError de asignación a CANTIDAD: \n\n" + e.ToString()); }
            }
        }
        public string Medida
        {
            get { return medida; }
            set
            {
                try
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        //MessageBox.Show("\nLa unidad de MEDIDA es nulo o vacio: \n\n");
                        medida = ".";
                    }
                    else
                    {
                        medida = value;
                    }
                }
                catch (Exception e) { MessageBox.Show("\nError de asignación a MEDIDA: \n\n" + e.ToString()); }
            }
        }
        public double Precio
        {
            get { return precio; }
            set
            {
                try
                {
                    if(double.IsNaN(value))
                    {
                        MessageBox.Show("\nEl PRECIO no es un numero: \n\n");
                        precio = 0;
                    }
                    else
                    {
                        precio = value;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("\nError de asignación a PRECIO: \n\n" + e.ToString());
                }
            }
        }
        public DateTime Fecha
        {
            get { return fecha; }
            set
            {
                try
                {
                    if (string.IsNullOrEmpty(value.ToString()))
                    {
                        MessageBox.Show("\nLa FECHA es nulo o vacio: \n\n");
                    }
                    else
                    {
                        fecha = value;
                    }
                    fecha = value;
                }
                catch (Exception e)
                {
                    MessageBox.Show("\nError de asignación a FECHA: \n\n" + e.ToString());
                }
            }
        }

        public override string ToString()
        {
            string tsObj = "";
            tsObj += "\n - Nombre: " + Nombre;
            tsObj += "\n - Cantidad: " + Cantidad;
            tsObj += "\n - Medida: " + Medida;
            tsObj += "\n - Precio" + Precio;
            tsObj += "\n - Fecha" + fecha;
            return tsObj;
        }
    }
}
