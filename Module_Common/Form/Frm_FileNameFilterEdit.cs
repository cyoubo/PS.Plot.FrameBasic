using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PS.Plot.FrameBasic.Module_Common.Component.Adapter;
using PS.Plot.FrameBasic.Module_Common.Component.FileNameFilter;
using PS.Plot.FrameBasic.Module_System.DevExpressionTools;
using PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools;

namespace PS.Plot.FrameBasic.Module_Common.Form
{
    public partial class Frm_FileNameFilterEdit : DevExpress.XtraEditors.XtraForm, INewRowCallBack
    {
        private FileNameFilterAdapter adapter;
        private FileNameFilterBuilder builder;
        private GridControlHelper gridHelper;
        private FileNameFilterInvoker invoker;
        
        public string ResultJson { get; protected set; }

        public Frm_FileNameFilterEdit()
        {
            InitializeComponent();
        }

        public Frm_FileNameFilterEdit(string InitalJson)
        {
            InitializeComponent();
            this.ResultJson = InitalJson;
        }

        private void Frm_FileNameFilterEdit_Load(object sender, EventArgs e)
        {
            builder = new FileNameFilterBuilder();
            adapter = new FileNameFilterAdapter();
            adapter.Initial(builder);

            repo_Cmb_Type.Items.Clear();
            repo_Cmb_Type.Items.AddRange(FileNameFilterInvoker.TravelType());
            repo_Cmb_Type.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            repo_Cmb_Location.Items.Clear();
            repo_Cmb_Location.Items.AddRange(FileNameFilterInvoker.TravelLocation());
            repo_Cmb_Location.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            gridHelper = new GridControlHelper(this.gridView1, this.gridControl1);
            gridHelper.GridControl.DataSource = builder.CreateDataTable();

            gridHelper.SetCellResposity(builder.Location, repo_Cmb_Location);
            gridHelper.SetCellResposity(builder.Type, repo_Cmb_Type);
            gridHelper.SetCellResposity(builder.Op_Delete, repo_HLE_Delete);
            gridHelper.SetColMaxWidth(builder.Op_Delete, 80);

            invoker = new FileNameFilterInvoker();
            if (string.IsNullOrEmpty(ResultJson) == false)
            {
                invoker.InitialFilter(ResultJson);
                adapter.NotifyfreshDataTable(invoker.Filters);
                gridHelper.GridControl.DataSource = adapter.ResultTable;
            }

            gridHelper.AddNewRowInputCallBack(this);
        }


        private void btn_sure_Click(object sender, EventArgs e)
        {
            try
            {
                FileNameFilterDeserializion deserializion = new FileNameFilterDeserializion();
                deserializion.ExcuteDesrialize(builder, gridHelper.DataTableSource);
                ResultJson = FileNameFilterInvoker.SerializeFilter(deserializion.DeserializeResult);
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception)
            {
                MessageBoxHelper.ShowDialog("提示", "文件过滤设置失败...");
            }
        }

        private void repo_HLE_Delete_Click(object sender, EventArgs e)
        {
            this.gridHelper.DeleteFocusedRow();
        }

        public void onNewRowAdded(GridControlHelper gridControlHelper, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (e.RowHandle != -1)
            {
                this.gridHelper.GridView.SetRowCellValue(e.RowHandle, builder.Op_Delete, builder.Op_Delete);
                this.gridHelper.GridView.AddNewRow();
            }
            
        }

        public void onValidateNewRow(GridControlHelper gridControlHelper, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e, DataRow NewRow)
        {
            
        }
    }

    public class FileNameFilterBuilder : BaseDataTableBuilder
    {
        public readonly string Location = "标签位置";
        public readonly string Type = "过滤类型";
        public readonly string KeyWord = "关键字";
        public readonly string Op_Delete = "删除";
        protected override void AddDataColumn()
        {
            onCreateDataColumn(Location);
            onCreateDataColumn(Type);
            onCreateDataColumn(KeyWord);
            onCreateDataColumn(Op_Delete);
        }
    }

    public class FileNameFilterAdapter : EditGridControlAdapter<BaseFileNameFilter>
    {

        public override void onCreateDataRow(ref System.Data.DataRow tempRow, BaseDataTableBuilder builder, int RowIndex, BaseFileNameFilter t)
        {

            FileNameFilterBuilder targetBuilder = builder as FileNameFilterBuilder;
            tempRow[targetBuilder.Location] = t.Location;
            tempRow[targetBuilder.Type] = t.Type;
            tempRow[targetBuilder.KeyWord] = t.KeyWord;
            tempRow[targetBuilder.Op_Delete] = targetBuilder.Op_Delete;
        }

        public override BaseFileNameFilter onDesrialize(BaseDataTableBuilder builder, System.Data.DataRow row)
        {

            FileNameFilterBuilder targetBuilder = builder as FileNameFilterBuilder;
            BaseFileNameFilter tempBean = new ContainsFileNameFilter();
            tempBean.Location = row[targetBuilder.Location].ToString();
            tempBean.Type = row[targetBuilder.Type].ToString();
            tempBean.KeyWord = row[targetBuilder.KeyWord].ToString();
            return tempBean;
        }

    }

    public class FileNameFilterDeserializion : BaseTableDeserializion<BaseFileNameFilter>
    {
        public override BaseFileNameFilter onDesrialize(BaseDataTableBuilder builder, System.Data.DataRow row, params object[] otherParam)
        {
            FileNameFilterBuilder targetBuilder = builder as FileNameFilterBuilder;
            BaseFileNameFilter tempBean = new ContainsFileNameFilter();
            tempBean.Location = row[targetBuilder.Location].ToString();
            tempBean.Type = row[targetBuilder.Type].ToString();
            tempBean.KeyWord = row[targetBuilder.KeyWord].ToString();
            return tempBean;
        }
    }
}