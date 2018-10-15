using DevExpress.Utils;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Plot.FrameBasic.Module_SupportLibs.DevExpressionTools.GridControlCommand
{
    public abstract class BaseCustomDrawCellCommand : CustomDrawCellCommand
    {
        protected Rectangle CreateBackgroudRectanglebyCellValue(RowCellCustomDrawEventArgs e, int value,int sum)
        {
            int width = (int)((e.Bounds.Width*1.0 * value*1.0) / (sum * 1.0)) ;
            return  new Rectangle(e.Bounds.X, e.Bounds.Y, width, e.Bounds.Height);
        }

        protected void FillCellBackgroud(RowCellCustomDrawEventArgs e, Rectangle backgroud, Brush brush)
        {
            e.Graphics.FillRectangle(brush, backgroud);
        }

        protected void DrawEditor(DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridCellInfo cell = e.Cell as GridCellInfo;
            Point offset = cell.CellValueRect.Location;
            BaseEditPainter pb = cell.ViewInfo.Painter as BaseEditPainter;
            AppearanceObject style = cell.ViewInfo.PaintAppearance;
            if (!offset.IsEmpty)
                cell.ViewInfo.Offset(offset.X, offset.Y);
            try
            {
                pb.Draw(new ControlGraphicsInfoArgs(cell.ViewInfo, e.Cache, cell.Bounds));
            }
            finally
            {
                if (!offset.IsEmpty)
                {
                    cell.ViewInfo.Offset(-offset.X, -offset.Y);
                }
            }
        }
    }
}
