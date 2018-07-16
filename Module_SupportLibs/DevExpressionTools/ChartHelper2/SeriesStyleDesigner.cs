using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools.ChartHelper2
{
    public class SeriesStyleDesigner
    {
        public SeriesStyleDesigner(Series DataSeries)
        {
            this.TargetSeries = DataSeries;
        }

        public Series TargetSeries {get ; protected set;}


        public void SetSeriesLengedOption(bool isShowInLenged, string Legend = "")
        {
            TargetSeries.ShowInLegend = isShowInLenged;
            TargetSeries.LegendText = Legend;
        }

        public void SetLineSeriesMarkerStyle(MarkerKind markerKind, int markerSize, Color markerColor)
        {
            if (TargetSeries.View is LineSeriesView)
            {
                LineSeriesView view = TargetSeries.View as LineSeriesView;
                view.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                view.LineMarkerOptions.Kind = markerKind;
                view.LineMarkerOptions.Size = markerSize;
                view.LineMarkerOptions.Color = markerColor;
            }
        }

        public void SetLineSeriesStyle(DashStyle LineStype, int Width, Color LineColor)
        {
            if (TargetSeries.View is LineSeriesView)
            {
                LineSeriesView view = TargetSeries.View as LineSeriesView;
                view.LineStyle.DashStyle = LineStype;
                view.Color = LineColor;
                view.LineStyle.Thickness = Width;
            }
        }

        public void SetBarSeriesStyle(Color BarColor,int barWidth = 0)
        {
            if (TargetSeries.View is BarSeriesView)
            {
                BarSeriesView view = TargetSeries.View as BarSeriesView;
                view.Color = BarColor;
                if(barWidth!=0)
                    view.BarWidth = barWidth;
            }
        }

        public void SetPieLengedFormat(string format)
        {
            if (TargetSeries != null)
            {
                TargetSeries.LegendTextPattern = format;
            }
        }

        public void SetBarSeriesEachColor(bool p)
        {
            if (TargetSeries != null)
            {
                ((BarSeriesView)TargetSeries.View).ColorEach = true;
            }
        }
    }
}
