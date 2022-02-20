using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Windows;
using WPF_Productos.Core;
using WPF_Productos.Models;

namespace WPF_Productos.ViewModels
{
    public partial class ListaProductosViewModel : ViewModelBase
    {
        SLDocument sl;
        private string miWorksheetDefault = "Despensa_Febrero";
        private void MostrarProductosExcel()
        {
            sl = new SLDocument(RutaExcel, miWorksheetDefault);
            //sl1.SelectWorksheet(miWorksheetDefault);

            int startColumn = 1, startRow = 1;
            int countRow = startRow + 1;

            string primerColumna = "";
            while (!string.IsNullOrEmpty(primerColumna = sl.GetCellValueAsString(countRow, startColumn)))
            {
                ProductosExcel_View.Add(new Producto(
                    primerColumna,
                    sl.GetCellValueAsString(countRow, (startColumn + 1)),
                    sl.GetCellValueAsString(countRow, (startColumn + 2)),
                    sl.GetCellValueAsDouble(countRow, (startColumn + 3)),
                    sl.GetCellValueAsDateTime(countRow, (startColumn + 4))
                ));
                countRow++;
            }
        }
        private void ActualizarProductosExcel()
        {
            try
            {
                SLDocument slCopia = new SLDocument();
                slCopia.RenameWorksheet(SLDocument.DefaultFirstSheetName, miWorksheetDefault);

                int startColumn = 1, startRow = 1;
                int countColumn = startColumn, countRow = startRow + 1;

                AgregarEstilosColumnasProducto(slCopia, miWorksheetDefault, startColumn);
                AgregarEstilosCabecerasProducto(slCopia, miWorksheetDefault, startColumn, startRow);

                foreach (Producto productoFilaActual in productosExcel_View)
                {
                    slCopia.SetCellValue(countRow, startColumn, productoFilaActual.Nombre);
                    slCopia.SetCellValue(countRow, (startColumn + 1), productoFilaActual.Cantidad);
                    slCopia.SetCellValue(countRow, (startColumn + 2), productoFilaActual.Medida);
                    slCopia.SetCellValue(countRow, (startColumn + 3), productoFilaActual.Precio);
                    slCopia.SetCellValue(countRow, (startColumn + 4), productoFilaActual.Fecha);
                    countRow++;
                }

                MessageBox.Show("Actualización Despensa Febrero Exitosa!!!", tituloApp);

                startColumn = 1;
                startRow = 1;
                countRow = startRow + 1;

                slCopia.AddWorksheet("Despensa_Enero");
                sl.SelectWorksheet("Despensa_Enero");
                AgregarEstilosColumnasProducto(slCopia, slCopia.GetCurrentWorksheetName(), startColumn);
                AgregarEstilosCabecerasProducto(slCopia, slCopia.GetCurrentWorksheetName(), startColumn, startRow);

                string primerColumna = "";
                while (!string.IsNullOrEmpty(primerColumna = sl.GetCellValueAsString(countRow, startColumn)))
                {
                    slCopia.SetCellValue(countRow, startColumn, primerColumna);
                    slCopia.SetCellValue(countRow, (startColumn + 1), sl.GetCellValueAsString(countRow, (startColumn + 1)));
                    slCopia.SetCellValue(countRow, (startColumn + 2), sl.GetCellValueAsString(countRow, (startColumn + 2)));
                    slCopia.SetCellValue(countRow, (startColumn + 3), sl.GetCellValueAsDouble(countRow, (startColumn + 3)));
                    slCopia.SetCellValue(countRow, (startColumn + 4), sl.GetCellValueAsDateTime(countRow, (startColumn + 4)));
                    countRow++;
                }

                slCopia.SaveAs(RutaExcel);
                MessageBox.Show("Actualización Despensa Enero Exitosa!!!", tituloApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio una Excepción: " + ex.Message, tituloApp);
            }
        }
        public void AgregarEstilosCabecerasProducto(SLDocument slPtr, string selectWorksheet, int startColumn, int startRow)
        {
            slPtr.SelectWorksheet(selectWorksheet);
            string[] misHeaders = { "Nombre", "Cantidad", "Medida", "Precio", "Fecha" };
            int[] misColumnWidth = { 25, 10, 10, 10, 20 };
            SLStyle tempSLStyle = null;

            for (int x = startColumn; x <= misHeaders.Length; x++)
            {
                tempSLStyle = slPtr.CreateStyle();
                tempSLStyle.SetFontBold(true);
                slPtr.SetRowStyle(startRow, tempSLStyle);
                slPtr.SetColumnWidth(x, misColumnWidth[x - 1]);
                slPtr.SetCellValue(startRow, x, misHeaders[x - 1]);
            }
        }
        public void AgregarEstilosColumnasProducto(SLDocument slPtr, string selectWorksheet, int startColumn)
        {
            slPtr.SelectWorksheet(selectWorksheet);
            string[] misFormatCode = { "@", "# ??/??", "@", "[$$-80A]#,##0.00;-[$$-80A]#,##0.00", "d mmm yyyy" };
            HorizontalAlignmentValues[] misHAligments = { HorizontalAlignmentValues.Left, HorizontalAlignmentValues.Left, HorizontalAlignmentValues.Left, HorizontalAlignmentValues.Right, HorizontalAlignmentValues.Right };

            SLStyle tempSLStyle = null;
            for (int x = startColumn; x <= misFormatCode.Length; x++)
            {
                tempSLStyle = slPtr.CreateStyle();
                tempSLStyle.FormatCode = misFormatCode[x - 1];
                tempSLStyle.Alignment.Horizontal = misHAligments[x - 1];
                slPtr.SetColumnStyle(x, tempSLStyle);
            }
        }
    }
}
