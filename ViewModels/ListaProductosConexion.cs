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
        SLDocument slOriginal;
        private string miWorksheetDefault = "Despensa_Febrero";
        private void MostrarProductosExcel()
        {
            slOriginal = new SLDocument(RutaExcel, miWorksheetDefault);

            int startColumn = 1, startRow = 1;
            int countRow = startRow + 1;

            while (!string.IsNullOrEmpty(slOriginal.GetCellValueAsString(countRow, startColumn)))
            {
                ProductosExcel_View.Add(new Producto(
                    slOriginal.GetCellValueAsString(countRow, startColumn),
                    slOriginal.GetCellValueAsString(countRow, (startColumn + 1)),
                    slOriginal.GetCellValueAsString(countRow, (startColumn + 2)),
                    slOriginal.GetCellValueAsDouble(countRow, (startColumn + 3)),
                    slOriginal.GetCellValueAsDateTime(countRow, (startColumn + 4))
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

                AgregarProductosEnDataGridAExcel(slCopia, miWorksheetDefault, startColumn, startRow);
                MessageBox.Show("Se ha actualizo " + slCopia.GetCurrentWorksheetName() + "!!!", tituloApp);

                CopiarUnaHojaNoSeleccionada(slCopia, "Despensa_Enero", startColumn, startRow);
                MessageBox.Show("Se ha actualizo " + slCopia.GetCurrentWorksheetName() + "!!!", tituloApp);

                slCopia.SaveAs(RutaExcel);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio una Excepción: " + ex.Message, tituloApp);
            }
        }
        public void AgregarProductosEnDataGridAExcel(SLDocument slPtr, string nombreWorksheet, int startColumn, int startRow)
        {
            slPtr.SelectWorksheet(nombreWorksheet);
            AgregarEstilosColumnasProducto(slPtr, startColumn);
            AgregarEstilosCabecerasProducto(slPtr, startColumn, startRow);

            int countRow = startRow + 1;
            foreach (Producto productoFilaActual in productosExcel_View)
            {
                slPtr.SetCellValue(countRow, startColumn, productoFilaActual.Nombre);
                slPtr.SetCellValue(countRow, (startColumn + 1), productoFilaActual.Cantidad);
                slPtr.SetCellValue(countRow, (startColumn + 2), productoFilaActual.Medida);
                slPtr.SetCellValue(countRow, (startColumn + 3), productoFilaActual.Precio);
                slPtr.SetCellValue(countRow, (startColumn + 4), productoFilaActual.Fecha);
                countRow++;
            }
        }
        public void CopiarUnaHojaNoSeleccionada(SLDocument slCopia, string nombreWorksheet, int startColumn, int startRow)
        {
            int countRow = startRow + 1;
            slCopia.AddWorksheet(nombreWorksheet);
            AgregarEstilosColumnasProducto(slCopia, startColumn);
            AgregarEstilosCabecerasProducto(slCopia, startColumn, startRow);
            slOriginal.SelectWorksheet(nombreWorksheet);

            while (!string.IsNullOrEmpty(slOriginal.GetCellValueAsString(countRow, startColumn)))
            {
                slCopia.SetCellValue(countRow, startColumn, slOriginal.GetCellValueAsString(countRow, startColumn));
                slCopia.SetCellValue(countRow, (startColumn + 1), slOriginal.GetCellValueAsString(countRow, (startColumn + 1)));
                slCopia.SetCellValue(countRow, (startColumn + 2), slOriginal.GetCellValueAsString(countRow, (startColumn + 2)));
                slCopia.SetCellValue(countRow, (startColumn + 3), slOriginal.GetCellValueAsDouble(countRow, (startColumn + 3)));
                slCopia.SetCellValue(countRow, (startColumn + 4), slOriginal.GetCellValueAsDateTime(countRow, (startColumn + 4)));
                countRow++;
            }
        }
        public void AgregarEstilosCabecerasProducto(SLDocument slPtr, int startColumn, int startRow)
        {
            //slPtr.SelectWorksheet(selectWorksheet);
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
        public void AgregarEstilosColumnasProducto(SLDocument slPtr, int startColumn)
        {
            //slPtr.SelectWorksheet(selectWorksheet);
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
