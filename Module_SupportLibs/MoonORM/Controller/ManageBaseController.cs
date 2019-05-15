using Moon.Orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.MoonORM.Controller
{
    public abstract class ManageBaseController<T> : EditBaseController<T> where T : EntityBase, new() 
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

        protected override Moon.Orm.MQLBase onCreateMQL_QueryEntryByID(int CurrentID)
        {
            return onCreateMQL_QueryAllEntities().Where(onBlindIDWhere(CurrentID));
        }

        protected abstract MQLBase onCreateMQL_QueryAllEntities();
    }
}
