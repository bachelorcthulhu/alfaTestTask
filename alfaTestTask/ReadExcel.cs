using System.Data;
using IronXL;

namespace alfaTestTask
{
    internal class ReadExcel
    {
        public struct ExcelFile
        {
            /// <summary>
            /// This is consturctor ExcelFile class
            /// </summary>
            /// <param name="filePath">Path to file of format xlsx or xls</param>
            /// <param name="rowsInTable">Enter quantity of rows in excel file</param>
            /// <param name="columnsInTable">Enter quantity of columns in excel file</param>
            public ExcelFile(string filePath, int rowsInTable, int columnsInTable)
            {
                FilePath = filePath;
                RowsInTable = rowsInTable;
                ColumnsInTable = columnsInTable;

                //Initialize DataFromExcelFile (that's jagged array (array of arrays)) with RowsInTable and ColumnsInTable as array size
                DataFromExcelFile = new string[columnsInTable][]; 
                for (int i = 0; i < columnsInTable; i++) 
                {
                    DataFromExcelFile[i] = new string[rowsInTable]; 
                }

                //We will have FilePath variable from users select in standart windows UI, so we will won't have excepction on that point
                //For import in DataTable format will use IronXL;
                var dataSet = WorkBook.Load(FilePath).ToDataSet();
                var firstTable = dataSet.Tables[0]; //We import first sheet - so our data must be on first sheet

                for (int i = 0; i < columnsInTable; i++)
                {
                    for (int j = 0; j < rowsInTable; j++)
                    {
                        DataFromExcelFile[i][j] = firstTable.Rows[j].ItemArray[i].ToString(); // here we import every cell from Excel file
                        //We suppose, that we have non null table, that can call a error
                    }
                }
               
            }
            /// <summary>
            /// Variable that stores the path to the Excel file (in xlsx or xls format)
            /// </summary>
            public string FilePath { get; set; }
            /// <summary>
            /// Variable that shows quantity of non-null rows win the table
            /// </summary>
            public readonly int RowsInTable { get; init; }
            /// <summary>
            /// Variable that shows quantity of non-null columns win the table
            /// </summary>
            public readonly int ColumnsInTable { get; init; }
            /// <summary>
            /// Jagged array of string type, that represent non-null part of table
            /// </summary>
            public string[][] DataFromExcelFile { get; set; }

            /// <summary>
            /// That method for filling with data our DataGrid - for that we interact with jagged array
            /// </summary>
            /// <param name="array">Jagged array cells of Excel</param>
            /// <param name="dataGrid">DataGrid, where information from Excel will be</param>
            public void SetData(string[][] array, System.Windows.Controls.DataGrid dataGrid)
            {
                if (array.Length <= 0)
                    return;
                DataTable table = new DataTable();
                for (int i = 0; i < array[0].Length; i++)
                {
                    table.Columns.Add(i.ToString(), typeof(string));
                }
                for (int i = 0; i < array.Length; i++)
                {
                    DataRow row = table.NewRow();
                    for (int j = 0; j < array[i].Length; j++)
                    {
                        row[j] = array[i][j].ToString();
                    }
                    table.Rows.Add(row);
                }
                dataGrid.ItemsSource = table.AsDataView();
            }
        }
    }
}
