# How to export multiple DataGrid to one PDF document in MAUI SfDataGrid?    

You can export multiple DataGrid in a single PDF document one at a time using the [DataGridPdfExportingController.ExportToPdfGrid](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.DataGrid.Exporting.DataGridPdfExportingController.html#Syncfusion_Maui_DataGrid_Exporting_DataGridPdfExportingController_ExportToPdfGrid_Syncfusion_Maui_DataGrid_SfDataGrid_Syncfusion_Maui_Data_ICollectionViewAdv_Syncfusion_Maui_DataGrid_Exporting_DataGridPdfExportingOption_Syncfusion_Pdf_PdfDocument_) method. Draw the exported PDF sequentially into a single PDF document using the PdfGrid.Draw.

# XAML

```XML
<StackLayout>
    <Button x:Name="button" Text="Export to PDF" >
        <Button.Behaviors>
            <behaviors:ButtonBehavior/>
        </Button.Behaviors>
    </Button>

    <syncfusion:SfDataGrid  x:Name="dataGrid1" ItemsSource="{Binding Employees}" AutoGenerateColumnsMode="None">
        <syncfusion:SfDataGrid.Columns>
            <syncfusion:DataGridNumericColumn MappingName="EmployeeID" HeaderText="New Employee ID "/>
            <syncfusion:DataGridTextColumn MappingName="Name" HeaderText="New Employee Name "/>
            <syncfusion:DataGridTextColumn MappingName="IDNumber" HeaderText="New ID Number"/>                
        </syncfusion:SfDataGrid.Columns>
    </syncfusion:SfDataGrid>

    <syncfusion:SfDataGrid  x:Name="dataGrid2" ItemsSource="{Binding Employees}" AutoGenerateColumnsMode="None">
        <syncfusion:SfDataGrid.Columns>
            <syncfusion:DataGridNumericColumn MappingName="EmployeeID" HeaderText="Old Employee ID"/>
            <syncfusion:DataGridTextColumn MappingName="Name" HeaderText="Old Name"/>
            <syncfusion:DataGridTextColumn MappingName="IDNumber" HeaderText="Old ID Number"/>
        </syncfusion:SfDataGrid.Columns>
    </syncfusion:SfDataGrid>

</StackLayout>
```

## C#

Create a PDF document using the PdfDocument and export the SfDataGrid into a PdfGrid using the extension method ExportToPdfGrid with the DataGridPdfExportingOption, which allows customization of the data exported to the PDF.

Sequentially draw the exported PdfGrid in the PdfPage using the PdfGrid.Draw method and save the created PDF document.

```C#
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
```

[View sample in GitHub](https://github.com/SyncfusionExamples/How-to-export-multiple-DataGrid-to-one-PDF-document-in-MAUI-SfDataGrid)

Take a moment to pursue this [documentation](https://help.syncfusion.com/maui/datagrid/overview), where you can find more about Syncfusion .NET MAUI DataGrid (SfDataGrid) with code examples.
Please refer to this [link](https://www.syncfusion.com/maui-controls/maui-datagrid) to learn about the essential features of Syncfusion .NET MAUI DataGrid(SfDataGrid).

### Conclusion
I hope you enjoyed learning about how to export multiple datagrid to one PDF document in MAUI SfDataGrid.

You can refer to our [.NET MAUI DataGrid's feature tour](https://www.syncfusion.com/maui-controls/maui-datagrid) page to know about its other groundbreaking feature representations. You can also explore our .NET MAUI DataGrid Documentation to understand how to present and manipulate data.
For current customers, you can check out our .NET MAUI components from the [License and Downloads](https://www.syncfusion.com/account/downloads) page. If you are new to Syncfusion, you can try our 30-day free trial to check out our .NET MAUI DataGrid and other .NET MAUI components.
If you have any queries or require clarifications, please let us know in comments below. You can also contact us through our [support forums](https://www.syncfusion.com/forums), [Direct-Trac](https://support.syncfusion.com/account/login?ReturnUrl=%2Faccount%2Fconnect%2Fauthorize%2Fcallback%3Fclient_id%3Dc54e52f3eb3cde0c3f20474f1bc179ed%26redirect_uri%3Dhttps%253A%252F%252Fsupport.syncfusion.com%252Fagent%252Flogincallback%26response_type%3Dcode%26scope%3Dopenid%2520profile%2520agent.api%2520integration.api%2520offline_access%2520kb.api%26state%3D8db41f98953a4d9ba40407b150ad4cf2%26code_challenge%3DvwHoT64z2h21eP_A9g7JWtr3vp3iPrvSjfh5hN5C7IE%26code_challenge_method%3DS256%26response_mode%3Dquery) or [feedback portal](https://www.syncfusion.com/feedback/maui?control=sfdatagrid). We are always happy to assist you!