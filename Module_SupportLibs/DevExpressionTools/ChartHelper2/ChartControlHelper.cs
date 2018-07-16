using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools.ChartHelper2
{
    public class ChartControlHelper
    {
        private ChartControl chartControl;

        public ChartControlHelper(ChartControl chartControl)
        {
            this.chartControl = chartControl; 
        }

        public void AddSeries(Series series)
        {
            this.chartControl.Series.Add(series);
        }

        public void SetYaxisRange(double min, double max, double diff=0)
        {
            ((XYDiagram)(this.chartControl.Diagram)).AxisY.VisualRange.SetMinMaxValues(min - diff, max + diff);
        }



        public void ReplaceSeries(Series series)
        {
            if (this.chartControl.Series.Count(x => x.Name.Equals(series.Name)) != 0)
                this.chartControl.Series.Remove(this.chartControl.Series.First(x => x.Name.Equals(series.Name)) as Series);
            AddSeries(series);
        }
    }
}
