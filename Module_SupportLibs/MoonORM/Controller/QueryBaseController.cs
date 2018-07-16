
using Moon.Orm;
using PS.Plot.FrameBasic.Module_SupportLibs.MoonORM.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.MoonORM.Controller
{
    public abstract class QueryBaseController<T> : MQLBaseController
        where T : EntityBase, new() 
    {
        
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

        public T QueryEntryByID(int targetID)
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

        public int GetRecordCount(IQueryCommand Command = null)
        {
            int count = 0;
            using (var db = this.dbFactory.OpenDefalutDataBase())
            {
                if (Command == null)
                    count = db.GetInt32Count(onCreateMQL_QueryAllEntities());
                else
                    count = db.GetInt32Count(Command.CreateQueryMQL());
            }
            onAfterGetRecordCount(count);
            return count;
        }


        protected virtual void onAfterGetRecordCount(int count) { }
        protected abstract MQLBase onCreateMQL_QueryAllEntities();
        protected abstract MQLBase onCreateMQL_QueryEntryByID(int CurrentID);
        
    }
}
