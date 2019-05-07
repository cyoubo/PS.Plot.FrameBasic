using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.AsposeExcel.Component
{
    public class ExcelHelper
    {
        public Workbook WorkBook {get;protected set;}

        public Worksheet CurrrentSheet { get; protected set; }

        public string FilePath {get;protected set;}

        public string ErrorMessage { get; protected set; }

        public ExcelHelper (string FilePath)
	    {
           this.FilePath = FilePath;
           if (new FileInfo(FilePath).Exists)
               this.WorkBook = new Workbook(FilePath);
           else
               this.WorkBook = new Workbook();
	    }

        public bool  StartEdit(int CurrentSheetIndex = 0 )
        {
            bool result = false;
            try
            {
                CurrrentSheet = this.WorkBook.Worksheets[CurrentSheetIndex];
                result = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        }

         public bool Save()
        {
            bool result = true;
            try
            {
                this.WorkBook.Save(FilePath);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                result = false;
            }
            return result;
        }

         public void InsertDataTable(DataTable table)
         {
             //输出表头
             for (int i = 0; i < table.Columns.Count; i++)
             {
                 this.CurrrentSheet.Cells[0, i].PutValue(table.Columns[i].Caption);
                 this.CurrrentSheet.Cells.SetColumnWidth(i, 20);//设置宽度
             }
             //输出表内容

             for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
             {
                 for (int colIndex = 0; colIndex < table.Columns.Count; colIndex++)
                     this.CurrrentSheet.Cells[rowIndex + 1, colIndex].PutValue(table.Rows[rowIndex][colIndex]);
             }
         }

         public void InsertDataTable(DataTable table,IFormatInsertDataTableCommand formatCommand)
         {
             //输出表头
             for (int i = 0; i < table.Columns.Count; i++)
             {
                 this.CurrrentSheet.Cells[0, i].PutValue(table.Columns[i].Caption);
                 this.CurrrentSheet.Cells.SetColumnWidth(i, 20);//设置宽度
             }
             //输出表内容

             for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
             {
                 for (int colIndex = 0; colIndex < table.Columns.Count; colIndex++)
                 {
                     string newValue = formatCommand.onFormat(rowIndex, colIndex, table.Rows[rowIndex][colIndex]);
                     this.CurrrentSheet.Cells[rowIndex + 1, colIndex].PutValue(newValue);
                 } 
             }
         }
    }

    public interface IFormatInsertDataTableCommand
    {
        string onFormat(int rowIndex, int colIndex, object srcValue);
    }
}
