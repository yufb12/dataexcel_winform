# DataExcel_WinForm Complete Official Document

# I. Product Introduction

DataExcel_WinForm is a professional spreadsheet calculation component developed based on .NET Framework, natively built for WinForm application development. It provides a complete set of Excel-like capabilities, including table view, editing controls, formula calculation, chart rendering, file formatting, typesetting and printing. It natively supports .NET Framework 2.0 and 4.0 dual versions, and is compatible with all .NET Framework versions. It can be quickly integrated into various WinForm business systems without installing Microsoft Office Excel locally, with no redundant dependencies and high running efficiency.

This component has comprehensive functions, superior to similar controls such as ReoGrid. It supports CBScript functional scripts and can directly use Excel formulas for programming development. It is widely used in manufacturing, inventory management, project management, education and other fields, and can quickly develop various Excel-format reports, ledgers and forms.

# II. Official Resources and Repository Information

|Resource Type|Address/Description|
|---|---|
|Main Repository|https://github.com/yufb12/dataexcel_winform|
|Latest Source Code Update Repository|https://github.com/yufb12/netexcel/tree/main (Note: This link reports "invalid link", please refer to the main repository)|
|NuGet Official Package|https://www.nuget.org/packages/DataExcel/|
|Web Version Repository|https://github.com/yufb12/dataexcel|
|Official Documentation & Demo Site|www.dataexcel.cn|
|Technical Blog|https://dbrwe.blog.csdn.net/article/details/133317079, https://dbrwe.blog.csdn.net/article/details/132655473|
# III. Version and Basic Information

- Latest Stable Release Version: 1.1 (Released on Feb 4, 2026)

- Latest NuGet Version: 2.13.6.59 (Updated on Feb 9, 2026)

- Development Language: C# 100.0%

- Code Contributor: @yufb12

- Open Source License: BSD-3-Clause license

- Compatible Frameworks: All versions of .NET Framework

# IV. Core Functions and Code Examples

## 1. Basic Initialization

```csharp
// Clear all default content and initialize the control
dataexcel1.Clear();
dataexcel1.Init();

// Set the number of rows and columns (5 rows and 4 columns, indexes start from 1)
dataexcel1.RowCount = 5;
dataexcel1.ColumnCount = 4;

// Set row height and column width (unit: pixel)
for (int i = 1; i <= dataexcel1.RowCount; i++)
{
    dataexcel1.SetRowHeight(i, 25);
}
for (int j = 1; j <= dataexcel1.ColumnCount; j++)
{
    dataexcel1.SetColumnWidth(j, 120);
}

// Set table title (merge A1 to D1 cells)
IMergeCell mergeCell = dataexcel1.MergeCell("A1:D1");
mergeCell.Text = "DataExcel Control Demo";
mergeCell.HorizontalAlignment = StringAlignment.Center;
mergeCell.VerticalAlignment = StringAlignment.Center;
mergeCell.Font = new Font("Arial", 14, FontStyle.Bold);
mergeCell.ForeColor = Color.Blue;
mergeCell.BackColor = Color.LightGray;

```

## 2. Style and Format Settings

```csharp
// Set cell background color and foreground color
ICell cell = dataexcel1[2, 1];
cell.BackColor = Color.LightBlue;
cell.ForeColor = Color.DarkBlue;

// Set cell font (font name, size, style)
cell.Font = new Font("Arial", 11, FontStyle.Italic | FontStyle.Underline);

// Set cell alignment (horizontal and vertical)
cell.HorizontalAlignment = StringAlignment.Center;
cell.VerticalAlignment = StringAlignment.Center;

// Set cell read-only permission
cell.ReadOnly = true;

// Set cell border style
cell.BorderStyle = new CellBorderStyle();
cell.BorderStyle.TopLineStyle.Visible = true;
cell.BorderStyle.BottomLineStyle.Visible = true;
cell.BorderStyle.LeftLineStyle.Visible = true;
cell.BorderStyle.RightLineStyle.Visible = true;

```

## 3. Merge and Split Cells

```csharp
// Merge cells (merge A2 to C2)
IMergeCell mergeCell1 = dataexcel1.MergeCell("A2:C2");
mergeCell1.Text = "Merged Cell";
mergeCell1.HorizontalAlignment = StringAlignment.Center;

// Split merged cells (split the merged cell at A2:C2)
dataexcel1.SplitCell("A2:C2");

```

## 4. Custom Edit Controls

```csharp
// Configure drop-down selection box (ComboBox)
Feng.Excel.Edits.CellComboBox comboEdit = new Feng.Excel.Edits.CellComboBox();
comboEdit.Items.AddRange(new string[] { "Option1", "Option2", "Option3", "Option4", "Option5" });
cell.Value = "Option1";
cell.OwnEditControl = comboEdit;

```

## 5. Formula Calculation

```csharp
// Batch fill data
ICell cell = null;
for (int i = 1; i < 6; i++)
{
    for (int j = 1; j < 5; j++)
    {
        cell = dataexcel1[i, j];
        cell.Value = i + j;
    }
}

// Set sum, average, count formulas
dataexcel1[6, 1].Expression = "\"Total：\"&sum(a1:a5)";
dataexcel1[6, 2].Expression = "\"Average：\"&avg(b1:b5)";
dataexcel1[6, 3].Expression = "\"Count：\"&count(c1:c5)";

```

## 6. View Interface Control

```csharp
// Hide column headers and row headers
dataexcel1.ShowColumnHeader = false;
dataexcel1.ShowRowHeader = false;

// Hide grid lines
dataexcel1.ShowGridRowLine = false;
dataexcel1.ShowGridColumnLine = false;

// Show rulers
dataexcel1.ShowHorizontalRuler = true;
dataexcel1.ShowVerticalRuler = true;

// Hide selection border
dataexcel1.ShowSelectRect = false;

```

# V. Application Scenarios

- **Manufacturing Industry**: Production order management, production schedule editing

- **Inventory Management**: Delivery order, incoming order, inventory ledger document production and printing

- **Project Management**: Project weekly report, construction plan, progress report preparation

- **Education Industry**: Exam template production, score statistics table editing

- **General Office**: Rapid development of various Excel format reports, ledgers and forms

# VI. License

The source code of this component follows the corresponding open source license. See the LICENSE file in each repository for details.

# VII. Notes

- The component version number is updated with iterations, and the version number in the DLL file name is subject to the actual released version

- A lightweight demo program DataExcel.exe is provided, which can be run directly to experience all functions of the component

- For the Web version JS Excel online plug-in, please refer to the corresponding open source repository

- If this project is helpful to you, please give it a ? Star
