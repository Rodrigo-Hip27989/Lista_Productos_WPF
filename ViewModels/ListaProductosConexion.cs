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

        private void MostrarProductosExcel()
        {
            try
            {
                slOriginal = new SLDocument(RutaExcel);
                List<string> hojasExcel = slOriginal.GetWorksheetNames();
                if (hojasExcel.Contains(nombreHojaExcelDefault))
                {
                    slOriginal.SelectWorksheet(nombreHojaExcelDefault);
                    int startColumn = 1, startRow = 2; //Sin contar las cabeceras
                    int numeroProductos = getNumeroProductosTabla(slOriginal, startColumn, startRow);

                    for (int countRow = startRow; countRow < (numeroProductos + startRow); countRow++)
                    {
                        sumaPrecios += slOriginal.GetCellValueAsDouble(countRow, (startColumn + 3));
                        ProductosExcel_View.Add(new Producto(
                            slOriginal.GetCellValueAsString(countRow, startColumn),
                            slOriginal.GetCellValueAsString(countRow, (startColumn + 1)),
                            slOriginal.GetCellValueAsString(countRow, (startColumn + 2)),
                            slOriginal.GetCellValueAsDouble(countRow, (startColumn + 3)),
                            slOriginal.GetCellValueAsDateTime(countRow, (startColumn + 4))
                        ));
                    }
                    OnPropertyChanged("SumaPrecios");
                    MessageBoxTemp.Show("Datos cargados correctamente !!!", tituloApp, 2, false);
                }
                else
                {
                    MessageBox.Show("No se encontro la hoja en el excel");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio una Excepción: " + ex.Message, tituloApp);
            }
        }
        private void ActualizarProductosExcel()
        {
            try
            {
                SLDocument slCopia = new SLDocument();
                slCopia.RenameWorksheet(SLDocument.DefaultFirstSheetName, nombreHojaExcelDefault);

                int startColumn = 1, startRow = 2;
                AgregarProductosEnDataGridAExcel(slCopia, nombreHojaExcelDefault, startColumn, startRow);

                List<string> hojasRestantes = slOriginal.GetWorksheetNames();
                hojasRestantes.Sort();
                hojasRestantes.Remove(nombreHojaExcelDefault);

                foreach (string hojaR in hojasRestantes)
                {
                    CopiarHojaNoSeleccionadaProductos(slCopia, hojaR, startColumn, startRow);
                }

                hojasRestantes.Add(nombreHojaExcelDefault);
                hojasRestantes.Sort();
                slCopia.MoveWorksheet(nombreHojaExcelDefault, hojasRestantes.IndexOf(nombreHojaExcelDefault));
                slCopia.SelectWorksheet(nombreHojaExcelDefault);

                slCopia.SaveAs(RutaExcel);
                MessageBoxTemp.Show("Excel actualizo correctamente !!!", tituloApp, 2, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio una Excepción: " + ex.Message, tituloApp);
            }
        }
        public void AgregarProductosEnDataGridAExcel(SLDocument slPtr, string nombreWorksheet, int startColumn, int startRow)
        {
            slPtr.SelectWorksheet(nombreWorksheet);
            AgregarEstilosColumnaEnExcel(slPtr, startColumn);
            AgregarEstilosCabecerasEnExcel(slPtr, startColumn, startRow);

            int countRow = startRow;
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
        public void CopiarHojaNoSeleccionadaProductos(SLDocument slCopia, string nombreWorksheet, int startColumn, int startRow)
        {
            int countRow = startRow;
            slCopia.AddWorksheet(nombreWorksheet);
            AgregarEstilosColumnaEnExcel(slCopia, startColumn);
            AgregarEstilosCabecerasEnExcel(slCopia, startColumn, startRow);
            slOriginal.SelectWorksheet(nombreWorksheet);

            int numeroProductos = getNumeroProductosTabla(slOriginal, startColumn, startRow);
            for (countRow = startRow; countRow < (numeroProductos + startRow); countRow++)
            {
                slCopia.SetCellValue(countRow, startColumn, slOriginal.GetCellValueAsString(countRow, startColumn));
                slCopia.SetCellValue(countRow, (startColumn + 1), slOriginal.GetCellValueAsString(countRow, (startColumn + 1)));
                slCopia.SetCellValue(countRow, (startColumn + 2), slOriginal.GetCellValueAsString(countRow, (startColumn + 2)));
                slCopia.SetCellValue(countRow, (startColumn + 3), slOriginal.GetCellValueAsDouble(countRow, (startColumn + 3)));
                slCopia.SetCellValue(countRow, (startColumn + 4), slOriginal.GetCellValueAsDateTime(countRow, (startColumn + 4)));
            }
        }
        private int getNumeroProductosTabla(SLDocument slPtr, int startColumn, int startRow)
        {
            int countRow = startRow; //El primer dato son las cabeceras y se omite
            while (!string.IsNullOrEmpty(slPtr.GetCellValueAsString(countRow, startColumn))) { countRow++; }
            return countRow - startRow;
        }
        public void AgregarEstilosCabecerasEnExcel(SLDocument slPtr, int startColumn, int startRow)
        {
            string[] misHeaders = { "Nombre", "Cantidad", "Medida", "Precio", "Fecha" };
            int[] misColumnWidth = { 28, 14, 14, 14, 21 };

            SLTable tbl = slPtr.CreateTable("A1", "E1");
            tbl.SetTableStyle(SLTableStyleTypeValues.Medium9);
            slPtr.InsertTable(tbl);

            SLStyle tempSLStyle = null;
            tempSLStyle = slPtr.CreateStyle();
            tempSLStyle.SetFontBold(true);
            tempSLStyle.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightSeaGreen, System.Drawing.Color.DarkSalmon);
            slPtr.SetCellStyle("A1", "E1", tempSLStyle);

            for (int countRow = startColumn; countRow <= misHeaders.Length; countRow++)
            {
                slPtr.SetColumnWidth(countRow, misColumnWidth[countRow - 1]);
                slPtr.SetCellValue((startRow - 1), countRow, misHeaders[countRow - 1]);
            }
        }
        public void AgregarEstilosColumnaEnExcel(SLDocument slPtr, int startColumn)
        {
            string[] misFormatCode = { "@", "# ??/??", "@", "[$$-80A]#,##0.00;-[$$-80A]#,##0.00", "d mmm yyyy" };
            HorizontalAlignmentValues[] misHAligments = { HorizontalAlignmentValues.Left, HorizontalAlignmentValues.Left, HorizontalAlignmentValues.Left, HorizontalAlignmentValues.Right, HorizontalAlignmentValues.Right };

            SLStyle tempSLStyle = null;
            for (int countRow = startColumn; countRow <= misFormatCode.Length; countRow++)
            {
                tempSLStyle = slPtr.CreateStyle();
                tempSLStyle.FormatCode = misFormatCode[countRow - 1];
                tempSLStyle.Alignment.Horizontal = misHAligments[countRow - 1];
                slPtr.SetColumnStyle(countRow, tempSLStyle);
            }
        }
    }
}
