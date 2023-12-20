using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.DataGrid.Exporting;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfDataGridSample.Behavior
{
    internal class ButtonBehavior : Behavior<Button>
    {
        private Button button;
        private SfDataGrid dataGrid1;
        private SfDataGrid dataGrid2;

        protected override void OnAttachedTo(BindableObject bindable)
        {
            button = bindable as Button;
            this.button.Clicked += Button_Clicked;
            base.OnAttachedTo(bindable);
        }


        protected override void OnDetachingFrom(BindableObject bindable)
        {
            this.button.Clicked -= Button_Clicked;
            base.OnDetachingFrom(bindable);
        }
        private void Button_Clicked(object? sender, EventArgs e)
        {
            var button = sender as Button;
            var stackLayout = button.Parent as StackLayout;
            dataGrid1 = stackLayout.FindByName<SfDataGrid>("dataGrid1");
            dataGrid2 = stackLayout.FindByName<SfDataGrid>("dataGrid2");
            MemoryStream stream = new MemoryStream();
            DataGridPdfExportingController pdfExport = new DataGridPdfExportingController();
            DataGridPdfExportingOption option = new DataGridPdfExportingOption();
            option.CanFitAllColumnsInOnePage = true;


            var pdfDoc = new PdfDocument();
            PdfPage page = pdfDoc.Pages.Add();
            var exportToPdfGrid = pdfExport.ExportToPdfGrid(this.dataGrid1, this.dataGrid1.View,option, pdfDoc);
            var exportToPdfGrid1 = pdfExport.ExportToPdfGrid(this.dataGrid2, this.dataGrid2.View, option, pdfDoc);

            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            layoutFormat.Layout = PdfLayoutType.Paginate;


            PdfLayoutResult result = exportToPdfGrid.Draw(page, 10, 10, layoutFormat);
            exportToPdfGrid1.Draw(result.Page, 10, result.Bounds.Height + 20);


            pdfDoc.Save(stream);
            pdfDoc.Close(true);
            SaveService saveService = new();
            saveService.SaveAndView("ExportFeature.pdf", "application/pdf", stream);
        }
    }
}
