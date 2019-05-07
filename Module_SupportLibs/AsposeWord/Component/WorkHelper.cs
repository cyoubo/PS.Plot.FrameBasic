using Aspose.Words;
using Aspose.Words.Saving;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.AsposeWord.Component
{
    public class WorkHelper
    {
        private string mFilePath;
        private Document mdoc;

        private DocumentBuilder mBuilder;

        public String ErrorMessage { get; private set; }

        public WorkHelper(string FilePath)
        {
            mFilePath = FilePath;
            if (new FileInfo(FilePath).Exists)
                mdoc = new Document(FilePath);
            else
                mdoc = new Document();
        }

        public void StartEdit()
        {
            mBuilder = new DocumentBuilder(mdoc);
        }

        public bool Save()
        {
            bool result = true;
            try
            {
                mdoc.Save(mFilePath);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                result = false;
            }
            return result;
        }

        public void WriteTextByBookMark(string BookMarkName, string value)
        {
            if (mdoc.Range.Bookmarks[BookMarkName] != null)
                mdoc.Range.Bookmarks[BookMarkName].Text = value;
        }

        public string ReadTextByBookMark(string BookMarkName, string value)
        {
            if (mdoc.Range.Bookmarks[BookMarkName] != null)
                return mdoc.Range.Bookmarks[BookMarkName].Text;
            return "";
        }

        public void InsertSimpleTable(int RowCount, int ColCount,string[] TableHead)
        {
            mBuilder.StartTable();
            for (int rowIndex = 0; rowIndex < RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < ColCount; colIndex++)
                {
                    mBuilder.InsertCell();
                    if (rowIndex == 0 && colIndex < TableHead.Length)
                        mBuilder.Write(TableHead[colIndex]);
                }
                mBuilder.EndRow();
            }
            mBuilder.EndTable();
        }

        public void InsertDataTable(DataTable table)
        {
            for (int rowIndex = -1; rowIndex < table.Rows.Count; rowIndex++)
            {
                for (int colIndex = 0; colIndex < table.Columns.Count; colIndex++)
                {
                    mBuilder.InsertCell();
                    if (rowIndex == -1)
                        mBuilder.Write(table.Columns[colIndex].Caption);
                    else
                        mBuilder.Write(table.Rows[rowIndex][colIndex].ToString());
                }
                mBuilder.EndRow();
            }
            mBuilder.EndTable();
        }

        public Document CopyNodeToNewDoc(Node node)
        {
            Document docNew = new Document();
            docNew.FirstSection.Body.RemoveAllChildren();
            NodeImporter importer = new NodeImporter(mdoc, docNew, ImportFormatMode.KeepSourceFormatting);
            Node importNode = importer.ImportNode(node, true);
            docNew.FirstSection.Body.AppendChild(importNode);
            return docNew;
        }

        public void CopyNodeToOtherDoc(Node node,Document docNew)
        {
            docNew.FirstSection.Body.RemoveAllChildren();
            NodeImporter importer = new NodeImporter(mdoc, docNew, ImportFormatMode.KeepSourceFormatting);
            Node importNode = importer.ImportNode(node, true);
            docNew.FirstSection.Body.AppendChild(importNode);
        }
    }
}
