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
            //Bebidas
            productosF_List.Add("Agua de Garrafon");
            productosF_List.Add("Horchata");
            productosF_List.Add("Jamaica");
            //Verduras y Hortalizas
            productosF_List.Add("Aguacate");
            productosF_List.Add("Betabel");
            productosF_List.Add("Brocoli");
            productosF_List.Add("Calabacita");
            productosF_List.Add("Cebolla");
            productosF_List.Add("Chayote");
            productosF_List.Add("Chile ancho");
            productosF_List.Add("Chile huajillo");
            productosF_List.Add("Ejote");
            productosF_List.Add("Papa");
            productosF_List.Add("Rabano");
            productosF_List.Add("Pepino");
            productosF_List.Add("Tomate");
            productosF_List.Add("Zanahoria");
            //Verduras de Hoja
            productosF_List.Add("Acelga");
            productosF_List.Add("Berro");
            productosF_List.Add("Cilantro");
            productosF_List.Add("Chipilin");
            productosF_List.Add("Espinaca");
            productosF_List.Add("Repollo");
            productosF_List.Add("Nabo");
            //Frutas para aperitivos y jugos
            productosF_List.Add("Guineo");
            productosF_List.Add("Platano");
            productosF_List.Add("Naranja");
            productosF_List.Add("Mango");
            productosF_List.Add("Papaya");
            productosF_List.Add("Piña");
            productosF_List.Add("Limón");
            //Cereales
            productosF_List.Add("Avena");
            productosF_List.Add("Atole de maizena");
            productosF_List.Add("Arroz");
            productosF_List.Add("Maiz");
            productosF_List.Add("Elote");
            productosF_List.Add("Avena");
            //Legumbres y semillas
            productosF_List.Add("Frijol");
            productosF_List.Add("Lenteja");
            productosF_List.Add("Chicharo");
            productosF_List.Add("Cacahuate");
            productosF_List.Add("Pepita de calabaza");
            //Carnes
            productosF_List.Add("Pollo - Crudo");
            productosF_List.Add("Pollo - Asado");
            productosF_List.Add("Pollo - Gringo");
            productosF_List.Add("Pescado");
            productosF_List.Add("Res - Bisteces");
            productosF_List.Add("Res - Carne molida");
            productosF_List.Add("Res - Otros");
            productosF_List.Add("Puerco - Chorizo");
            productosF_List.Add("Puerco - Chulete");
            productosF_List.Add("Puerco - Costilla");
            //Huevo, Lacteos y derivados
            productosF_List.Add("Huevo");
            productosF_List.Add("Crema");
            productosF_List.Add("Leche");
            productosF_List.Add("Queso");
            productosF_List.Add("Quesillo");
            //Procesados
            productosF_List.Add("Atun");
            productosF_List.Add("Sardina");
            //Aperitivos
            productosF_List.Add("Pan dulce");
            productosF_List.Add("Galletas de rollo");
            productosF_List.Add("Tostadas");

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
