using Moon.Orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.MoonORM.Controller
{
    /// <summary>
    /// 实体、关系\目标类实体类 载入、新建和删除的控制器
    /// </summary>
    /// <typeparam name="T">主体实体</typeparam>
    /// <typeparam name="R">关系类实体</typeparam>
    /// <typeparam name="V">关系类实体</typeparam>
    public abstract class EditRelationTargetController<T,R,V> : EditRelationController<T, R> 
        where V : EntityBase , new() 
        where R : EntityBase , new()
        where T : EntityBase , new()
    {
        public V Target { get; set; }

        protected override void onAfterLoadEntry(T entry)
        {
            base.onAfterLoadEntry(entry);
            using (var db = this.dbFactory.OpenDefalutDataBase())
            {
                relation = db.GetEntity<R>(onCreateMQL_QueryRelationByEntry(entry));
                if (relation != null)
                {
                    Target = db.GetEntity<V>(onCreateMQL_QueryTaregetByRelation(relation));
                }
            }
        }

        public abstract MQLBase onCreateMQL_QueryTaregetByRelation(R Relation);

    }
}
