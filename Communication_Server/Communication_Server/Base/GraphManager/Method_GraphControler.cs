
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Communication_Server.Base.GraphManager
{
    public partial class cGraphControler : Chart
    {
        public cGraphControler()
        {
            mInst = this;

            SetData();
            SetEvent();
        }

        private void SetData()
        {
            mArea = new ChartArea();
            mLocation = new Point();
            mSize = new Size();

            mPrePosStartZoomX = new List<double>();
            mPrePosEndZoomX = new List<double>();
            
            mPrePosStartZoomY = new List<double>();
            mPrePosEndZoomY = new List<double>();


            xStripLine = new StripLine();
            yStripLine = new StripLine();


            //-- Setting Chart Area 
            mArea.AxisX.ScrollBar.Enabled = false;
            mArea.AxisY.ScrollBar.Enabled = false;

            //mArea.AxisX.StripLines.Add(new StripLine());
            //mArea.AxisY.StripLines.Add(new StripLine());

            mArea.AxisX.LabelStyle.Format = "#.##";
            mArea.AxisY.LabelStyle.Format = "#.##";
            //-- End

            mInst.AllowDrop = true;

            fnPoint_Label += fnMovePoint_Label;

            mPicLocList = new List<Point>();
        }

        private void SetEvent()
        {
            mInst.MouseWheel += evnMouseWheel;
            mInst.MouseDown  += evnMouseDown;
            mInst.MouseMove  += evnMouseMove;
            mInst.MouseUp    += evnMouseUp;

            mInst.KeyDown    += evnKeyDown;
            mInst.KeyUp      += evnKeyUp;

            mInst.DragDrop += evnDragDrop;
            mInst.DragEnter += evnDragEnter;

            mInst.Paint += MInst_Paint;
        }

        
        public void fnInitGraph(stGraphInfo FrameInfo)
        {
            Axis AxisX = null;
            Axis AxisY = null;

            mArea.Name = FrameInfo.AreaName;
            mInst.Name = FrameInfo.ChartName;

            mLocation.X = FrameInfo.PosX;
            mLocation.Y = FrameInfo.PosY;
            mInst.Location = mLocation;

            mSize.Width = FrameInfo.Width;
            mSize.Height = FrameInfo.Height;
            mInst.Size = mSize;

            if ((mInst.ChartAreas.IsUniqueName(mArea.Name)))
            {
                mInst.ChartAreas.Add(mArea);

                mInst.ChartAreas[mArea.Name].AxisX.ScaleView.Zoomable = true;
                mInst.ChartAreas[mArea.Name].AxisY.ScaleView.Zoomable = true;
            }

            AxisX = mInst.ChartAreas[0].AxisX;
            AxisY = mInst.ChartAreas[0].AxisY;

            if (AxisX.StripLines.Count == 0)
            {
                AxisX.StripLines.Add(xStripLine);
                AxisX.StripLines[0].StripWidth = 0.1;
                AxisX.StripLines[0].BackColor = Color.Red;
            }

            if (AxisY.StripLines.Count == 0)
            {
                AxisY.StripLines.Add(yStripLine);
                AxisY.StripLines[0].StripWidth = 0.1;
                AxisY.StripLines[0].BackColor = Color.Red;
            }

            AxisY.Maximum = FrameInfo.MaxY;
            AxisY.Minimum = FrameInfo.MinY;

            AxisX.Maximum = FrameInfo.MaxX;
            AxisX.Minimum = FrameInfo.MinX;
        }

        public void fnInitSeries(string SeriesName)
        {
            if ((mInst.Series.Count == 0) || ((mInst.Series.IsUniqueName(SeriesName))))
            {
                mInst.Series.Add(SeriesName);
            }

            mInst.Series[SeriesName].ChartType = SeriesChartType.FastLine;
        }

        public void fnClear()
        {
            mInst.Invoke(new MethodInvoker(
            delegate ()
            {
                foreach(Series item in mInst.Series)
                {
                    item.Points.Clear();
                    mArea.AxisY.StripLines.Clear();
                    mArea.AxisX.StripLines.Clear();
                }

                mInst.Series.Clear();
            }));
        }

        public void FnInputDataArr(string SeriesName, double[] dataArrX, double[] dataArrY)
        {
            int loop = 0;
            
            //if (mInst.Series.IsUniqueName(SeriesName)) return;

            mInst.Invoke(new MethodInvoker(
            delegate ()
            {
                for (loop = 0; loop < dataArrX.Length; loop++)
                {
                    mInst.Series[SeriesName].Points.AddXY(dataArrX[loop], dataArrY[loop]);
                }
            }));

            
        }

        public void FnClearData(string SeriesName)
        {
            //if (mInst.Series.IsUniqueName(SeriesName)) return;

            mInst.Invoke(new MethodInvoker(
            delegate ()
            {
                if(mInst.Series[SeriesName].Points.Count > 0) mInst.Series[SeriesName].Points.Clear();
                
                mInst.Series[SeriesName].Points.AddXY(0, 0);
                
            }));
        }

        public void FnInputData(string SeriesName, double dataX, double dataY)
        {
            if (mInst.InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {

                    if (mInst.Series.IsUniqueName(SeriesName)) return;
                    mInst.Series[SeriesName].Points.AddXY(dataX, dataY);
                });
            }
            else
            {
                if (mInst.Series.IsUniqueName(SeriesName)) return;
                mInst.Series[SeriesName].Points.AddXY(dataX, dataY);
            }
        }

        private int fnFineNearValue(double[] dataArr, double findData)
        {
            double min = Int32.MaxValue;
            double near = 0;

            int index = 0;

            for(int i=0; i< dataArr.Length; i++)
            {
                double abs = Math.Abs(dataArr[i] - findData);

                if(min > abs)
                {
                    min = abs;
                    near = dataArr[i];
                    index = i;
                }
            }

            return index;
        }


        private void fnGetPoinInfo(MouseEventArgs e)
        {

            int index = 0;

            Chart Chart = mInst;
            Axis AxisX = Chart.ChartAreas[0].AxisX;
            Axis AxisY = Chart.ChartAreas[0].AxisY;

            double dPreX = 0;
            double dPreY = 0;

            DataPointCollection PointsInfo = null;

            double[] dataValuesX = null;

            double xPoint = 0;
            double yPoint = 0;

            int FineNearValue_Index = 0;

            
                if (dPrePointX == e.Location.X) return;

                mPicLocList.Clear();
                mValue_X = 0;
                if (mValueList_Y != null && mValueList_Y.Count > 0) mValueList_Y.Clear();

                dPrePointX = e.Location.X;

                dPreX = AxisX.PixelPositionToValue(e.Location.X);
                dPreY = AxisY.PixelPositionToValue(e.Location.Y);

                PointsInfo = mInst.Series[0].Points;

                if (PointsInfo.Count <= 1)
                {
                    AxisX.StripLines[0].IntervalOffset = dPreX;
                    AxisY.StripLines[0].IntervalOffset = dPreY;

                    return;
                }

                dataValuesX = new double[PointsInfo.Count];

                for (index = 0; index < dataValuesX.Length; index++)
                {
                    dataValuesX[index] = PointsInfo[index].XValue;
                }

                FineNearValue_Index = fnFineNearValue(dataValuesX, dPreX);

                xPoint = PointsInfo[FineNearValue_Index].XValue;
                yPoint = PointsInfo[FineNearValue_Index].YValues[0];

                AxisX.StripLines[0].IntervalOffset = xPoint;
                AxisY.StripLines[0].IntervalOffset = yPoint;


            //fnPoint_Label?.BeginInvoke(dataValuesX, xPoint, FineNearValue_Index, null, null);

        }

        private void fnMovePoint_Label(double[] dataValuesX, double xPoint, int FineNearValue_Index)
        {
            Chart Chart = mInst;
            Axis AxisX = Chart.ChartAreas[0].AxisX;
            Axis AxisY = Chart.ChartAreas[0].AxisY;

            DataPointCollection PointsInfo = null;
            
            double yPoint = 0;

            int xPosition = 0;
            int yPosition = 0;

            Point ptx_Point;

            List<Point> picLocList = new List<Point>();

            List<double> ValueList_Y = new List<double>(); ;
            
            mValue_X = xPoint;

            for (int i = 0; i < mInst.Series.Count; i++)
            {
                PointsInfo = mInst.Series[i].Points;
                if (PointsInfo.Count <= 0) return;
                yPoint = PointsInfo[FineNearValue_Index].YValues[0];

                xPosition = (int)AxisX.ValueToPixelPosition(xPoint);
                yPosition = (int)AxisY.ValueToPixelPosition(yPoint);

                ptx_Point = new Point(xPosition - 4, yPosition - 4);
                
                picLocList.Add(ptx_Point);

                ValueList_Y.Add(yPoint);
            }

            mPicLocList = picLocList;
            GetValueList_Y = ValueList_Y;
        }

        private void fnMoveGraph(MouseEventArgs e)
        {
            double minMoveX = 0;
            double PreMoveX = 0;
            double CurX = 0;
            double MoveX = 0;

            double minMoveY = 0;
            double PreMoveY = 0;
            double CurY = 0;
            double MoveY = 0;

            if (e.Location == mPreMoveXY) return;
            else
            {
                mPreMoveXY = e.Location;
            }

            if (e.Location.X < 0 || e.Location.X >= Size.Width) return;
            if (e.Location.Y < 0 || e.Location.Y >= Size.Height) return;

            //-- X축
            minMoveX = mArea.AxisX.ScaleView.Position;
            PreMoveX = mArea.AxisX.PixelPositionToValue(mClickPosX);
            CurX     = mArea.AxisX.PixelPositionToValue(e.Location.X);
            MoveX    = minMoveX + (PreMoveX - CurX);

            mArea.AxisX.ScaleView.Scroll(MoveX);
            mClickPosX = e.Location.X;

            //-- Y축
            minMoveY = mArea.AxisY.ScaleView.Position;
            PreMoveY = mArea.AxisY.PixelPositionToValue(mClickPosY);
            CurY     = mArea.AxisY.PixelPositionToValue(e.Location.Y);
            MoveY    = minMoveY + (PreMoveY - CurY);

            mArea.AxisY.ScaleView.Scroll(MoveY);
            mClickPosY = e.Location.Y;
        }

        public void drawMarker(double x = 0, double y = 0)
        {
            try
            {

                //clearMarker();

                double minX = this.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                double maxX = this.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                if (double.IsNaN(minX) || double.IsNaN(maxX))
                    return;

                double zoomWidth = maxX - minX;
                double CurX = x;
                if (CurX < minX || CurX > maxX)
                    return;

                if (Series.Count == 0)
                    return;


                double minY = this.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                double maxY = this.ChartAreas[0].AxisY.ScaleView.ViewMaximum;
                if (double.IsNaN(minY) || double.IsNaN(maxY))
                    return;

                double CurY = y;
                if (CurY < minY || CurY > maxY)
                    return;


                //x축 라인 및 툴팁 정보를 전시
                if (this.ChartAreas[0].AxisX.StripLines.Count == 0) this.ChartAreas[0].AxisX.StripLines.Add(new StripLine());
                StripLine xLine = this.ChartAreas[0].AxisX.StripLines[0];
                xLine.IntervalOffset = CurX;
                xLine.StripWidth = zoomWidth * 0.001;
                xLine.BackColor = Color.FromArgb(100, Color.Red);


                //y축 라인 및 툴팁 정보를 전시
                StripLine yLine = this.ChartAreas[0].AxisY.StripLines[0];
                yLine.IntervalOffset = CurY;
                yLine.StripWidth = zoomWidth * 0.001;
                yLine.BackColor = Color.FromArgb(100, Color.Red);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }


        public void clearMarker()
        {
            try
            {

                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        this.ChartAreas[0].AxisX.StripLines[0].StripWidth = 0;

                        

                        for (int i = 0; i < this.ChartAreas[0].AxisY.StripLines.Count; ++i)
                        {
                            StripLine yLine = this.ChartAreas[0].AxisY.StripLines[i];
                            yLine.StripWidth = 0;
                            
                        }
                    }));
                }
                else
                {
                    

                    for (int i = 0; i < this.ChartAreas[0].AxisY.StripLines.Count; ++i)
                    {
                        //StripLine yLine = this.ChartAreas[0].AxisY.StripLines[i];
                        //yLine.StripWidth = 0;
                        
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.StackTrace); }
        }
    }
}
