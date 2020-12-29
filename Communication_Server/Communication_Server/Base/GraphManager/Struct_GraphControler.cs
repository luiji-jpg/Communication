using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace Communication_Server.Base.GraphManager
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct stGraphInfo
    {
        public string ChartName;
        public string AreaName;

        public int PosX;
        public int PosY;

        public int Width;
        public int Height;

        public double MinX;
        public double MaxX;
               
        public double MinY;
        public double MaxY;
    }
}
