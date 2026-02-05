using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Feng.Excel.Extend
{
    public class ReticleDrawExtend
    {
        private static Dictionary<DataExcel, ReticleDrawExtend> List = new Dictionary<DataExcel, ReticleDrawExtend>();
        DataExcel _grid = null;

        public static bool Reticled(DataExcel grid)
        {
            if (!Contains(grid))
            {
                ReticleDrawExtend extend = new ReticleDrawExtend();
                extend.Init(grid);
            }
            return true;
        }

        public static bool Contains(DataExcel grid)
        {
            return List.ContainsKey(grid);
        }

        public void Init(DataExcel grid)
        {
            _grid = grid;
            grid.Painted += grid_Painted;
            //grid.MouseMove += grid_MouseMove;
            List.Add(grid,this);
        }

        public void UnInit()
        {
            _grid.Painted -= grid_Painted;
            //_grid.MouseMove -= grid_MouseMove;
            List.Remove(_grid);
        }

        void grid_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            try
            { 
                DataExcel grid = sender as DataExcel;
                grid.Invalidate();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
  
        }

        void grid_Painted(object sender, Feng.Drawing.GraphicsObject graphicsobject)
        {

            try
            {
                DataExcel grid=sender as DataExcel ;
                Point pt = graphicsobject.ClientPoint;
                graphicsobject.Graphics.DrawLine(Pens.AntiqueWhite, new Point(pt.X, grid.TopSideHeight), new Point(pt.X, grid.Height - grid.BottomSideHeight));
                graphicsobject.Graphics.DrawLine(Pens.AntiqueWhite, new Point(grid.LeftSideWidth, pt.Y), new Point(grid.Width - grid.LeftSideWidth, pt.Y));
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
            }
  
        }



        public static void UnReticled(DataExcel grid)
        {
            if (Contains(grid))
            {
                ReticleDrawExtend extend = List[grid];
                extend.UnInit();
            }
        }
    }
}