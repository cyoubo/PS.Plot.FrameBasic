
using Moon.Orm;
using PS.Plot.FrameBasic.Module_SupportLibs.MoonORM.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.MoonORM.Controller
{
    /// <summary>
    /// 支持分页的查询控制器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class QuerySplitPageController<T> : QueryBaseController<T> 
        where T : EntityBase, new() 
    {
        protected int m_CurrentPage = -1;

        protected int m_TotalPage;

        protected int m_PageSize = 50;

        protected int m_RecordCount;

        protected IList<T> m_Result;

        public bool Query(IQueryCommand Command = null)
        {
            bool result = false;
            onPrepareQuery();
            using (var db = this.dbFactory.OpenDefalutDataBase())
            {
                MQLBase mql = Command == null ? onCreateMQL_QueryAllEntities() : Command.CreateQueryMQL();
                string orderField = Command == null ? null : Command.GetOrderFieldName();
                m_Result = new List<T>(db.GetPagerToOwnList<T>(mql, out m_TotalPage, out m_RecordCount, m_CurrentPage, m_PageSize, orderField));
                result = m_Result.Count != 0;
                onAfterQuery(result);
            }
            return result;
        }

        protected override void onAfterGetRecordCount(int count)
        {
            base.onAfterGetRecordCount(count);
            m_RecordCount = count;
            m_TotalPage = m_RecordCount / m_PageSize + 1;
        }

        protected virtual void onPrepareQuery()
        {
            if (Result == null)
                m_Result = new List<T>();
            Result.Clear();
        }

        protected virtual void onAfterQuery(bool IsSuccess) { }


        public int PageSize
        {
            get { return m_PageSize; }
            set { m_PageSize = value; }
        }

        public int CurrentPage
        {
            set { m_CurrentPage = value; }
            get { return m_CurrentPage; }
        }

        public int TotalPage
        {
            get { return m_TotalPage; }
        }

        public int RecordCount
        {
            get { return m_RecordCount; }
        }

        public IList<T> Result
        {
            get { return m_Result; }
        }
    }
}
