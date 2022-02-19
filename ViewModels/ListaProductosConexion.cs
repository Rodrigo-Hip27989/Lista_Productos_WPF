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
        private string nameWorksheetDefault = "SinNombre";
        private void MostrarProductosExcel()
        {
            SLDocument sl = new SLDocument(RutaExcel);
            nameWorksheetDefault = sl.GetCurrentWorksheetName();

            int startColumn = 1, startRow = 1;
            int countRow = startRow + 1;

            SLStyle tempSLStyle = null;
            string[] misFormatCode = { "@", "# ??/??", "@", "[$$-80A]#,##0.00;-[$$-80A]#,##0.00", "d mmm yyyy" };
            HorizontalAlignmentValues[] misHAligments = { HorizontalAlignmentValues.Left, HorizontalAlignmentValues.Left, HorizontalAlignmentValues.Left, HorizontalAlignmentValues.Right, HorizontalAlignmentValues.Right };

            for (int x = startColumn; x <= misFormatCode.Length; x++)
            {
                tempSLStyle = sl.CreateStyle();
                tempSLStyle.FormatCode = misFormatCode[x - 1];
                tempSLStyle.Alignment.Horizontal = misHAligments[x - 1];
                sl.SetColumnStyle(x, tempSLStyle);
            }

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
                SLDocument sl = new SLDocument();
                sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, nameWorksheetDefault);

                int startColumn = 1, startRow = 1;
                int countColumn = startColumn, countRow = startRow + 1;

                SLStyle tempSLStyle = null;
                int[] misColumnWidth = { 25, 10, 10, 10, 20};
                string[] misHeaders = { "Nombre", "Cantidad", "Medida", "Precio", "Fecha" };
                string[] misFormatCode = { "@", "# ??/??", "@", "[$$-80A]#,##0.00;-[$$-80A]#,##0.00", "d mmm yyyy" };
                HorizontalAlignmentValues[] misHAligments = { HorizontalAlignmentValues.Left, HorizontalAlignmentValues.Left, HorizontalAlignmentValues.Left, HorizontalAlignmentValues.Right, HorizontalAlignmentValues.Right };

                for (int x = startColumn; x <= misFormatCode.Length; x++)
                {
                    tempSLStyle = sl.CreateStyle();
                    tempSLStyle.FormatCode = misFormatCode[x - 1];
                    tempSLStyle.Alignment.Horizontal = misHAligments[x - 1];
                    sl.SetColumnStyle(x, tempSLStyle);
                }

                for (int x = startColumn; x <= misHeaders.Length; x++) {
                    tempSLStyle = sl.CreateStyle();
                    tempSLStyle.SetFontBold(true);
                    sl.SetRowStyle(startRow, tempSLStyle);
                    sl.SetColumnWidth(x, misColumnWidth[x - 1]);
                    sl.SetCellValue(startRow, x, misHeaders[x - 1]);
                }

                foreach (Producto productoFilaActual in productosExcel_View)
                {
                    sl.SetCellValue(countRow, startColumn, productoFilaActual.Nombre);
                    sl.SetCellValue(countRow, (startColumn + 1), productoFilaActual.Cantidad);
                    sl.SetCellValue(countRow, (startColumn + 2), productoFilaActual.Medida);
                    sl.SetCellValue(countRow, (startColumn + 3), productoFilaActual.Precio);
                    sl.SetCellValue(countRow, (startColumn + 4), productoFilaActual.Fecha);
                    countRow++;
                }

                sl.SaveAs(RutaExcel);
                MessageBox.Show("Actualización Exitosa!!!", tituloApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio una Excepción: " + ex.Message, tituloApp);
            }
        }
    }
}
