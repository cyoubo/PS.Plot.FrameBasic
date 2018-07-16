
using Moon.Orm;
using PS.Plot.FrameBasic.Module_SupportLibs.MoonORM.ConstDef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.MoonORM.Controller
{
    /// <summary>
    /// 实体、关系实体类 载入、新建和删除的控制器
    /// </summary>
    /// <typeparam name="T">主体实体</typeparam>
    /// <typeparam name="R">关系类实体</typeparam>
    public abstract class EditRelationController<T, R> : EditBaseController<T> 
        where R : EntityBase , new()
        where T : EntityBase, new()
    {

        protected R relation;
        
        /// <summary>
        /// 关系类 实体类
        /// </summary>
        public R Relation
        {
            get { return relation; }
        }

        protected override void onAfterLoadEntry(T entry)
        {
            base.onAfterLoadEntry(entry);
            using (var db = this.dbFactory.OpenDefalutDataBase())
            {
                relation = db.GetEntity<R>(onCreateMQL_QueryRelationByEntry(entry));
            }
        }

        public bool UpdateEntryWithRelation()
        {
            bool result = false;
            using (var db = this.dbFactory.OpenDefalutDataBase())
            {
                db.TransactionEnabled = true;
                try
                {
                    (entry as EntityBase).WhereExpression = onBlindIDWhere(entry);
                    db.Update(entry as EntityBase);
                    Relation.WhereExpression = onBindRelationIDWhere(Relation);
                    db.Update(Relation);
                    db.Transaction.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    db.Transaction.Rollback();
                    this.dbFactory.WriteSystemLog(DBConst.UpdateException, ex);
                    m_ErrorMessage = ex.Message;
                }
            }
            return result;
        }

        public bool InsertMissionWithRelation(params object[] RelationParams)
        {
            bool result = false;
            using (var db = this.dbFactory.OpenDefalutDataBase())
            {
                db.TransactionEnabled = true;
                try
                {
                    int NewOBJID = Int32.Parse(db.Add(Entry).ToString());
                    relation = new R();
                    onFillRelationParam(NewOBJID, RelationParams);
                    db.Add(Relation);
                    db.Transaction.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    db.Transaction.Rollback();
                    this.dbFactory.WriteSystemLog(DBConst.InsertException, ex);
                    m_ErrorMessage = ex.Message;
                }
            }
            return result;
        }

        /// <summary>
        /// 合成Relation的主键查询语句
        /// </summary>
        /// <param name="Relation">目标Relation</param>
        /// <returns></returns>
        protected abstract WhereExpression onBindRelationIDWhere(R Relation);
        /// <summary>
        /// 创建利用从基础实体查询关系实体的查询语句
        /// </summary>
        /// <param name="Entry"></param>
        /// <returns></returns>
        protected abstract MQLBase onCreateMQL_QueryRelationByEntry(T Entry);
        /// <summary>
        /// 填充插入Relation时需要的参数
        /// </summary>
        /// <param name="NewOBJID">主Entry插入后新建的ID</param>
        /// <param name="RelationParams">由InsertMissionWithRelation方法传入的Relation参数</param>
        protected abstract void onFillRelationParam(int NewOBJID, object[] RelationParams);
    }
}
