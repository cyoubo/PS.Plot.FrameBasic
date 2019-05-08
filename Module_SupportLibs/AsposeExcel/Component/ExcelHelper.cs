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
    /// <summary>
    /// Excel 处理帮助类
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 当前EXCEL文档对象
        /// </summary>
        public Workbook WorkBook {get;protected set;}
        /// <summary>
        /// 当前处理的WorkSheet
        /// </summary>
        public Worksheet CurrrentSheet { get; protected set; }
        /// <summary>
        /// 当前Excel文件的文件路径
        /// </summary>
        public string FilePath {get;protected set;}
        /// <summary>
        /// 操作错误因袭
        /// </summary>
        public string ErrorMessage { get; protected set; }
        /// <summary>
        /// Excel 处理帮助类，若FilePath指向的文件不存在，将在内存中创建一个新的Excel文件
        /// </summary>
        /// <param name="FilePath">待操作的excel文件</param>
        public ExcelHelper (string FilePath)
	    {
           this.FilePath = FilePath;
           if (new FileInfo(FilePath).Exists)
               this.WorkBook = new Workbook(FilePath);
           else
               this.WorkBook = new Workbook();
	    }
        /// <summary>
        /// 开始编辑
        /// </summary>
        /// <param name="CurrentSheetIndex">当前sheet的序号</param>
        /// <returns></returns>
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
        /// <summary>
        /// 保存编辑
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 向当前Sheet中插入一个DataTable
        /// </summary>
        /// <param name="table"></param>
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
        /// <summary>
        ///  向当前Sheet中插入一个DataTable
        /// </summary>
        /// <param name="table"></param>
        /// <param name="formatCommand">输出格式化命令接口</param>
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

        /// <summary>
        /// 从当前Sheet中读取一个DataTable
        /// </summary>
        /// <param name="startRow"></param>
        /// <returns></returns>
        public DataTable ReadDataTable(int startRow = 0)
        {
            Cells cell = CurrrentSheet.Cells;
            return cell.ExportDataTable(startRow, 0, cell.MaxDataRow + 1, cell.MaxColumn + 1);
        }
        /// <summary>
        /// 从当前Sheet中读取一个DataTable，并将起始行作为DataTable的标题
        /// </summary>
        /// <param name="startRow"></param>
        /// <returns></returns>
        public DataTable ReadDataTable_FirstRowAsCaption(int startRow = 0)
        {
            Cells cell = CurrrentSheet.Cells;
            DataTable resultDt = cell.ExportDataTable(1, 0, cell.MaxDataRow + 1, cell.MaxDataColumn + 1);
            for (int index = 0; index < cell.MaxDataColumn; index++)
            {
                object tempObj = cell[0, index].Value;
                if (tempObj != null && string.IsNullOrEmpty(tempObj.ToString()) == false)
                {
                    resultDt.Columns[index].Caption = tempObj.ToString();
                    resultDt.Columns[index].ColumnName = tempObj.ToString();
                }
                else
                {
                    resultDt.Columns[index].Caption = "UnknowColName"+index;
                    resultDt.Columns[index].ColumnName = "UnknowColName" + index;
                }
            }
            return resultDt;
        }
    }

    public interface IFormatInsertDataTableCommand
    {
        string onFormat(int rowIndex, int colIndex, object srcValue);
    }
}
