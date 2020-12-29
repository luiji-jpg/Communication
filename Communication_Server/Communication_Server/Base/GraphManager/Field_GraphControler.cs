using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Communication_Server.Base.GraphManager
{
    public partial class cGraphControler : Chart
    {
        cGraphControler mInst;

        public delegate void FileInfoToGraph(cGraphControler Graph, string FileName);
        public delegate void Point_Label(double[] dataValuesX, double xPoint, int FineNearValue_Index);

        public delegate void GetValue_XY(double xValue, List<double>yValueList);

        FileInfoToGraph fnFIleInfoToGraph;
        Point_Label fnPoint_Label;
        GetValue_XY fnGetValue_XY;

        List<Point> mPicLocList;

        ChartArea mArea;

        Point mLocation;

        Size mSize;

        
        


        bool mIsKey_Ctrl;

        bool mIsClick;

        int mClickPosX;
        int mClickPosY;

        double mStartPosX;
        double mEndPosX;

        double mStartPosY;
        double mEndPosY;

        List<double> mPrePosStartZoomX;
        List<double> mPrePosEndZoomX;

        List<double> mPrePosStartZoomY;
        List<double> mPrePosEndZoomY;

        int PrePosIndex_X;
        int PrePosIndex_Y;

        Point mPreMoveXY;

        double MinX;
        double MaxX;

        double MinY;
        double MaxY;

        bool IsMoveMouseWheel;

        StripLine xStripLine;
        StripLine yStripLine;


        double dPrePointX = 0;

        List<double> mValueList_Y;
        double mValue_X;
    }
}
