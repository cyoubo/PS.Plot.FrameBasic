﻿using Moon.Orm;
using PS.Plot.FrameBasic.Module_SupportLibs.MoonORM.ConstDef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.MoonORM.Controller
{
    public abstract class ManageGUIDController <T, V>: MQLBaseController
       where T : EntityBase, new() where V : MQLBase
    {
        protected T entry;
        public T Entry { get { return entry; } }
        public string CurrentGUID { get; set; }
        /// <summary>
        /// 依据CurrentID作为主键条件，载入实体
        /// </summary>
        public void LoadEntry()
        {
            if (string.IsNullOrEmpty(CurrentGUID))
                entry = new T();
            else
            {
                using (var db = this.dbFactory.OpenDefalutDataBase())
                {
                    entry = db.GetEntity<T>(onCreateMQL_QueryEntryByID(CurrentGUID));
                    if (entry == null)
                        entry = new T();
                    else
                        CurrentGUID = onGetCurrentEntryID(entry);
                    onAfterLoadEntry(entry);
                }
            }
        }
        public void ReleaseEntry()
        {
            if (entry is IDisposable)
                (entry as IDisposable).Dispose();
            entry = null;
        }
        /// <summary>
        /// 添加实体记录
        /// </summary>
        /// <returns>若添加成功则返回true</returns>
        public bool InsertEntry()
        {
            bool result = false;
            using (var db = this.dbFactory.OpenDefalutDataBase())
            {
                try
                {
                    onBeforeInsert(entry);
                    object temp = db.Add(Entry).ToString();
                    result = true;
                }
                catch (Exception ex)
                {
                    this.dbFactory.WriteSystemLog(DBConst.InsertException, ex);
                    m_ErrorMessage = ex.Message;
                }
            }
            return result;
        }
        /// <summary>
        /// 添加实体记录,并返回当前记录的主键
        /// </summary>
        /// <returns>若添加成功则返回true</returns>
        public bool InsertEntry(out object PrimaryKey)
        {
            bool result = false;
            using (var db = this.dbFactory.OpenDefalutDataBase())
            {
                try
                {
                    onBeforeInsert(entry);
                    PrimaryKey = db.Add(Entry);
                    result = true;
                }
                catch (Exception ex)
                {
                    PrimaryKey = null;
                    this.dbFactory.WriteSystemLog(DBConst.InsertException, ex);
                    m_ErrorMessage = ex.Message;
                }
            }
            return result;
        }
        /// <summary>
        /// 依据ID更新实体
        /// </summary>
        /// <returns>若添加更新则返回true</returns>
        public bool UpdateEntryByID()
        {
            bool result = false;
            using (var db = this.dbFactory.OpenDefalutDataBase())
            {
                try
                {
                    Entry.WhereExpression = onBlindIDWhere(entry);
                    onBeforeUpdate(entry);
                    db.Update(Entry);
                    result = true;
                }
                catch (Exception ex)
                {
                    this.dbFactory.WriteSystemLog(DBConst.InsertException, ex);
                    m_ErrorMessage = ex.Message;
                }
            }
            return result;
        }

        public bool UpdateEntryByID(EntityBase CurrentEntry)
        {
            bool result = false;
            using (var db = this.dbFactory.OpenDefalutDataBase())
            {
                try
                {
                    db.Update(CurrentEntry);
                    result = true;
                }
                catch (Exception ex)
                {
                    this.dbFactory.WriteSystemLog(DBConst.InsertException, ex);
                    m_ErrorMessage = ex.Message;
                }
            }
            return result;
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <returns></returns>
        public bool DeleteEntry()
        {
            bool result = false;
            using (var db = this.dbFactory.OpenDefalutDataBase())
            {
                try
                {
                    db.Remove<V>(onBlindIDWhere(entry));
                    result = true;
                }
                catch (Exception ex)
                {
                    this.dbFactory.WriteSystemLog(DBConst.DeleteException, ex);
                    m_ErrorMessage = ex.Message;
                }
            }
            return result;
        }
        /// <summary>
        /// 基于ID删除实体
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteEntryByID(string ID)
        {
            bool result = false;
            using (var db = this.dbFactory.OpenDefalutDataBase())
            {
                try
                {
                    db.Remove<V>(onBlindIDWhere(ID));
                    result = true;
                }
                catch (Exception ex)
                {
                    this.dbFactory.WriteSystemLog(DBConst.DeleteException, ex);
                    m_ErrorMessage = ex.Message;
                }
            }
            return result;
        }

        public IList<T> TravleAllEntities()
        {
            using (var db = this.dbFactory.OpenDefalutDataBase())
            {
                MQLBase mql = onCreateMQL_QueryAllEntities();
                IList<T> result = db.GetEntities<T>(mql);
                if (result == null)
                    result = new List<T>();
                return result;
            }
        }

        protected IList<T> TravelEntitiesByWhereCaluse(MQLBase expression)
        {
            using (var db = this.dbFactory.OpenDefalutDataBase())
            {
                IList<T> result = db.GetEntities<T>(expression);
                if (result == null)
                    result = new List<T>();
                return result;
            }
        }

        protected bool ExistByWhereCaluse(WhereExpression where)
        {
            using (var db = this.dbFactory.OpenDefalutDataBase())
            {
                return db.Exist<V>(where);
            }
        }

        public T QueryEntryByID(string targetID)
        {
            T entry = new T();
            using (var db = this.dbFactory.OpenDefalutDataBase())
            {
                entry = db.GetEntity<T>(onCreateMQL_QueryEntryByID(targetID));
                if (entry == null)
                    entry = new T();
            }
            return entry;
        }

        public virtual string FormatDate(DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd");
        }

        public virtual string FormatDateTime(DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd hh:mm:ss");
        }

        /// <summary>
        /// 事件处理：在更新实体前进行的回调
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void onBeforeUpdate(T entry) { }
        /// <summary>
        /// 事件处理：在插入实体前进行的回调
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void onBeforeInsert(T entry) { }
        /// <summary>
        /// 事件处理：在载入实体后的回调
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void onAfterLoadEntry(T entry) { }
        /// <summary>
        /// 功能方法：从entry中获取ID
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        protected abstract string onGetCurrentEntryID(T entry);
        /// <summary>
        /// 功能方法：合成利用entry的ID查询的where子句
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        protected abstract WhereExpression onBlindIDWhere(T entry);
        /// <summary>
        /// 功能方法：合成利用ID查询的where子句
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        protected abstract WhereExpression onBlindIDWhere(string entry);
        /// <summary>
        /// 功能方法：合成用于查询实例的MQL对象
        /// </summary>
        /// <param name="CurrentID"></param>
        /// <returns></returns>
        protected abstract MQLBase onCreateMQL_QueryEntryByID(string CurrentID);

        protected abstract MQLBase onCreateMQL_QueryAllEntities();
    }
}
