
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools.ChartHelper2
{
    public class BaseSeriesDataBuilder
    {
        public Series DataSeries { get; protected set; }
        public SeriesStyleDesigner StyleDesigner { get; set; }

        public Series CreateEmptySeries(string Name, ViewType seriesType)
        {
            Series pieSeries = new Series(Name, seriesType);
            return pieSeries;
        }

        public virtual void CreateSeries(string Name, ViewType seriesType)
        {
            DataSeries = new Series(Name, seriesType);
            StyleDesigner = new SeriesStyleDesigner(DataSeries);
        }

        public void ClearDataSeries()
        {
            if (DataSeries != null)
                DataSeries.Points.Clear();
        }

        public void RemoveDataSeriesPoint(int index)
        {
            if (DataSeries != null)
                DataSeries.Points.RemoveAt(index);
        }

        protected void onAddSeriesPoint(SeriesPoint point)
        {
            this.DataSeries.Points.Add(point);
        }

        public SeriesPoint this[int index] 
        {
            get 
            {
                return this.DataSeries.Points[index];
            }
        }

        public int PointCount
        {
            get
            {
                return this.DataSeries.Points.Count;
            }
        }

        public string SeriesName
        {
            get
            {
                return this.DataSeries.Name;
            }
        }
    }

    public class SingleDataValueSeriesBuilder : BaseSeriesDataBuilder
    {

        public void AddDataFromTable(DataTable dt, string CatalogFieldName, string ValueFieldName)
        {
            if (DataSeries == null)
                return;

            foreach (DataRow item in dt.Rows)
            {
                string value = string.IsNullOrEmpty(item[ValueFieldName].ToString()) ? "0" : item[ValueFieldName].ToString();
                AddData(item[CatalogFieldName].ToString(), Double.Parse(value));
            }
        }

        public void AddDataFromTable(DataTable dataTable, int CatalogFieldNameIndex, int ValueFieldNameIndex)
        {
            if (DataSeries == null)
                return;

            string CatalogFieldName = dataTable.Columns[CatalogFieldNameIndex].ColumnName;
            string ValueFieldName = dataTable.Columns[ValueFieldNameIndex].ColumnName;

            AddDataFromTable(dataTable, CatalogFieldName, ValueFieldName);
        }

        public void AddDataFromDictonary(IDictionary<string, double> dict)
        {
            if (DataSeries == null)
                return;

            foreach (var item in dict)
                AddData(item.Key.ToString(), item.Value);
        }

        public void AddData(string calalogValue, double datavalue)
        {
            if (DataSeries == null)
                return;
            DataSeries.Points.Add(new SeriesPoint(calalogValue, datavalue));
        }

        public void AddData(DateTime calalogValue, double datavalue)
        {
            if (DataSeries == null)
                return;
            DataSeries.Points.Add(new SeriesPoint(calalogValue, datavalue));
        }

        public double MinValue 
        {
            get
            {
                if (DataSeries == null)
                    return 0;
               return DataSeries.Points.Min(x => x.UserValues[0]);
            }
        }

        public double MaxValue
        {
            get
            {
                if (DataSeries == null)
                    return 0;
                return DataSeries.Points.Max(x => x.UserValues[0]);
            }
        }


    }

    public class DoubleDataValueSeriesBuilder : BaseSeriesDataBuilder
    {

        public void AddData(double x, double y)
        {
            if (DataSeries == null)
                return;
            DataSeries.Points.Add(new SeriesPoint(x, y));
        }

        public void AddData(string catalogName, double x, double y)
        {
            if (DataSeries == null)
                return;
            DataSeries.Points.Add(new SeriesPoint(catalogName, x, y));
        }

        public void AddData(DateTime calalogValue, double x, double y)
        {
            if (DataSeries == null)
                return;
            DataSeries.Points.Add(new SeriesPoint(calalogValue, x, y));
        }

        public void AddDataFromTable(DataTable dt, string CatalogFieldName, string XFieldName,string YFieldName)
        {
            if (DataSeries == null)
                return;
            foreach (DataRow item in dt.Rows)
                AddData(item[CatalogFieldName].ToString(), Double.Parse(item[XFieldName].ToString()), Double.Parse(item[YFieldName].ToString()));
        }
    }

    public abstract class EntryDataValueSeriesBuilder<T> : BaseSeriesDataBuilder
    {
        public void AddDataFromEntries(IList<T> Entries)
        {
            foreach (T item in Entries)
            {
                onAddDataEntry(item);
            }
        }

        public abstract void onAddDataEntry(T item);
    }


    public class MulitSingleDataValueSeriesBuilder
    {
        public IDictionary<string, Series> SeriesDict { get; protected set; }
        public ViewType SeriesViewType { get; protected set; }

        public void CreateSeriesDict(ViewType seriesType)
        {
            SeriesDict = new Dictionary<string, Series>();
            SeriesViewType = seriesType;
        }

        public void ClearAllSeriesPoint()
        {
            if (SeriesDict == null)
                return;
            foreach (var item in SeriesDict)
                item.Value.Points.Clear();
        }

        public void AddSereisFromDictionary(IDictionary<string, KeyValuePair<object,double>[]> dict)
        {
            foreach (string item in dict.Keys)
            {
                Series temp = SeriesDict.ContainsKey(item) == false ? new Series(item, SeriesViewType): SeriesDict[item];
                foreach (var valesPairs in dict[item])
                    temp.Points.Add(new SeriesPoint(valesPairs.Key,valesPairs.Value));
                if (SeriesDict.ContainsKey(item) == false)
                    SeriesDict.Add(item,temp);
            }
        }
    }
}
