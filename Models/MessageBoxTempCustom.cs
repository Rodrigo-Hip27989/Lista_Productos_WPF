using System;
using System.Windows;

namespace WPF_Productos.Models
{
    public class MessageBoxTempCustom
    {
        System.Threading.Timer IntervaloTiempo;
        string tituloMessageB;
        string TextoMessageBox;
        int TiempoMaximo;
        bool MostrarContador;
        IntPtr hndLabel = IntPtr.Zero;

        MessageBoxButton mbButton = MessageBoxButton.OK;
        MessageBoxImage mbImage = MessageBoxImage.Information;
        MessageBoxTempCustom(string texto, string titulo, int tiempo, bool contador)
        {
            tituloMessageB = titulo;
            TextoMessageBox = texto;
            TiempoMaximo = tiempo;
            MostrarContador = contador;

            if (TiempoMaximo > 99) return; //Máximo 99 segundos
            IntervaloTiempo = new System.Threading.Timer(EjecutaCada1Segundo, null, 1000, 1000);
            if (contador)
            {
                MessageBoxResult ResultadoMensaje = MessageBox.Show(texto + "\n\n"
                    /*+ "\r\nEste mensaje se cerrará dentro de "*/ + TiempoMaximo.ToString("00") + " segundos ..."
                    , titulo, mbButton, mbImage);
                if (ResultadoMensaje == MessageBoxResult.OK) IntervaloTiempo.Dispose();
            }
            else
            {
                MessageBoxResult ResultadoMensaje = MessageBox.Show(texto + "\n\n"
                    , titulo, mbButton, mbImage);
                if (ResultadoMensaje == MessageBoxResult.OK) IntervaloTiempo.Dispose();
            }
        }
        MessageBoxTempCustom(string texto, string titulo, int tiempo, bool contador, MessageBoxImage mbImage)
        {
            this.mbImage = mbImage;
            this.mbButton = MessageBoxButton.OK;
            new MessageBoxTempCustom(texto, titulo, tiempo, contador);
        }
        public static void Show(string texto, string titulo, int tiempo, bool contador)
        {
            new MessageBoxTempCustom(texto, titulo, tiempo, contador);
        }
        public static void Show(string texto, string titulo, int tiempo, bool contador, MessageBoxImage mbImage)
        {
            new MessageBoxTempCustom(texto, titulo, tiempo, contador, mbImage);
        }
        MessageBoxTempCustom(string texto, string titulo, int tiempo, bool contador, MessageBoxButton mbButton, MessageBoxImage mbImage)
        {
            this.mbImage = mbImage;
            this.mbButton = mbButton;
            new MessageBoxTempCustom(texto, titulo, tiempo, contador);
        }
        public static void Show(string texto, string titulo, int tiempo, bool contador, MessageBoxButton mbButton, MessageBoxImage mbImage)
        {
            new MessageBoxTempCustom(texto, titulo, tiempo, contador, mbButton, mbImage);
        }
        void EjecutaCada1Segundo(object state)
        {
            TiempoMaximo--;
            if (TiempoMaximo <= 0)
            {
                IntPtr hndMBox = FindWindow(null, tituloMessageB);
                if (hndMBox != IntPtr.Zero)
                {
                    SendMessage(hndMBox, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                    IntervaloTiempo.Dispose();
                }
            }
            else if (MostrarContador)
            {
                // Ha pasado un intervalo de 1 seg:
                if (hndLabel != IntPtr.Zero)
                {
                    SetWindowText(hndLabel, TextoMessageBox +
                        "\r\nEste mensaje se cerrará dentro de " +
                        TiempoMaximo.ToString("00") + " segundos");
                }
                else
                {
                    IntPtr hndMBox = FindWindow(null, tituloMessageB);
                    if (hndMBox != IntPtr.Zero)
                    {
                        // Ha encontrado el MessageBox, busca ahora el texto
                        hndLabel = FindWindowEx(hndMBox, IntPtr.Zero, "Static", null);
                        if (hndLabel != IntPtr.Zero)
                        {
                            // Ha encontrado el texto porque el MessageBox
                            // solo tiene un control "Static".
                            SetWindowText(hndLabel, TextoMessageBox +
                                "\r\nEste mensaje se cerrará dentro de " +
                                TiempoMaximo.ToString("00") + " segundos");
                        }
                    }
                }
            }
        }
        const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll",
            CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true,
            CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter,
            string lpszClass, string lpszWindow);
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true,
            CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern bool SetWindowText(IntPtr hwnd, string lpString);
    }
}
