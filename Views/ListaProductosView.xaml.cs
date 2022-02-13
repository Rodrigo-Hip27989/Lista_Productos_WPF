using System.Collections.Generic;
using System.Windows;

namespace WPF_Productos.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ListaProductosView : Window
    {
        public ListaProductosView()
        {
            InitializeComponent();
            comboMedidas.ItemsSource = getUnidadesMasaVolumen();
            comboProductos.ItemsSource = getProductosFrecuentes();
            comboMedidas.SelectedIndex = 0;
            comboProductos.SelectedIndex = 0;
        }
        private List<string> getProductosFrecuentes()
        {
            List<string> productosF_List = new List<string>();
            productosF_List.Add("Papa");
            productosF_List.Add("Chayote");
            productosF_List.Add("Zanahoria");
            productosF_List.Add("Tomate");
            productosF_List.Add("Calabaza");
            productosF_List.Add("Rabano");
            productosF_List.Add("Cilantro");
            productosF_List.Add("Repollo");
            productosF_List.Sort();
            return productosF_List;
        }
        private List<string> getUnidadesMasaVolumen()
        {
            List<string> masa_volumen__List = new List<string>();
            masa_volumen__List.Add("Kilo(s)");
            masa_volumen__List.Add("Gramo(s)");
            masa_volumen__List.Add("Medida(s)");
            masa_volumen__List.Add("Bolsa(s)");
            masa_volumen__List.Add("Manojo(s)");
            masa_volumen__List.Add("Paquete(s)");
            masa_volumen__List.Add("Pieza(s)");
            masa_volumen__List.Sort();
            return masa_volumen__List;
        }
    }
}
