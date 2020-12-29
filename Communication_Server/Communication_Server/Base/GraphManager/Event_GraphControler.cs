
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
        private void MInst_Paint(object sender, PaintEventArgs e)
        {
            Graphics gp = e.Graphics;
            Font font = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
            int index = 0;

            

            if (mPicLocList.Count > 0)
            {
                foreach (Point item in mPicLocList)
                {
                    if (!(this.Series[index++].Enabled)) continue;
                    gp.FillEllipse(Brushes.Black, item.X, item.Y, 8, 8);

                    RectangleF rectF1;
                    if (item.X >= (this.Width / 2))
                    {
                        rectF1 = new RectangleF((item.X - 80), (item.Y - 4), 70, 30);
                    }
                    else
                    {
                        rectF1 = new RectangleF((item.X + 10), (item.Y - 4), 70, 30);
                    }

                    string PointName = "";
                    if ((index % 2) == 1) PointName = "I";
                    else PointName = "Q";
                    gp.DrawString(PointName, font, Brushes.Black, rectF1);
                }
            }
        }

        private void evnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) mIsKey_Ctrl = true;
        }

        private void evnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) mIsKey_Ctrl = false;
        }

        private void evnMouseWheel(object sender, MouseEventArgs e)
        {
            Chart Chart = (Chart)sender;
            Axis AxisX = Chart.ChartAreas[0].AxisX;
            Axis AxisY = Chart.ChartAreas[0].AxisY;

            int PreX = 0;
            int PreY = 0;

            double zoomFactor = 0;

            try
            {
                if ((IsMoveMouseWheel) || !(mIsKey_Ctrl)) return;

                IsMoveMouseWheel = true;

                MinX = AxisX.ScaleView.ViewMinimum;
                MaxX = AxisX.ScaleView.ViewMaximum;

                MinY = AxisY.ScaleView.ViewMinimum;
                MaxY = AxisY.ScaleView.ViewMaximum;

                if (double.IsNaN(MinY) || double.IsNaN(MaxY) || MinX == MaxX)
                {
                    IsMoveMouseWheel = false;
                    return;
                }

                if (e.Delta > 0)
                {
                    zoomFactor = 0.8;

                    PreX = (int)AxisX.PixelPositionToValue(e.Location.X);
                    PreY = (int)AxisY.PixelPositionToValue(e.Location.Y);

                    if(PrePosIndex_X == 0)
                    {
                        mStartPosX = MinX;
                        mEndPosX = MaxX;
                    }

                    if (PrePosIndex_Y == 0)
                    {
                        mStartPosY = MinY;
                        mEndPosY = MaxY;
                    }

                    if (mIsKey_Ctrl)
                    {
                        mPrePosStartZoomX.Add(mStartPosX);
                        mPrePosEndZoomX.Add(mEndPosX);

                        mStartPosX = PreX - ((PreX - MinX) * zoomFactor);
                        mEndPosX = PreX + ((MaxX - PreX) * zoomFactor);

                        mStartPosX = Math.Truncate(mStartPosX * 100) / 100;
                        mEndPosX = Math.Truncate(mEndPosX * 100) / 100;
                        
                        PrePosIndex_X++;
                    }

                    if (mIsKey_Ctrl && ((PrePosIndex_X%4) == 0))
                    {
                        mPrePosStartZoomY.Add(mStartPosY);
                        mPrePosEndZoomY.Add(mEndPosY);

                        mStartPosY = PreY - ((PreY - MinY) * zoomFactor);
                        mEndPosY = PreY + ((MaxY - PreY) * zoomFactor);

                        mStartPosY = Math.Truncate(mStartPosY * 100) / 100;
                        mEndPosY = Math.Truncate(mEndPosY * 100) / 100;

                        PrePosIndex_Y++;
                    }

                    if (mStartPosX == 0) mStartPosX = MinX;
                    if (mStartPosY == 0) mStartPosY = MinY;

                    if (mEndPosX == 0) mEndPosX = MaxX;
                    if (mEndPosY == 0) mEndPosY = MaxY;

                    if (mStartPosX < MinX)
                    {
                        mStartPosX = MinX;
                    }

                    if (mEndPosX > MaxX)
                    {
                        mEndPosX = MaxX;
                    }

                    if (mStartPosY < MinY)
                    {
                        mStartPosY = MinY;
                    }

                    if (mEndPosY > MaxY)
                    {
                        mEndPosY = MaxY;
                    }

                    AxisX.ScaleView.Zoom(mStartPosX, mEndPosX);
                    AxisY.ScaleView.Zoom(mStartPosY, mEndPosY);
                }

                else
                {
                    if (mIsKey_Ctrl)
                    {
                        if (PrePosIndex_X > 0) PrePosIndex_X--;
                        if (PrePosIndex_Y > 0) PrePosIndex_Y--;

                        AxisX.ScaleView.Zoom(mPrePosStartZoomX[PrePosIndex_X], mPrePosEndZoomX[PrePosIndex_X]);
                        AxisY.ScaleView.Zoom(mPrePosStartZoomY[PrePosIndex_Y], mPrePosEndZoomY[PrePosIndex_Y]);

                        if (PrePosIndex_X == 0)
                        {
                            mPrePosStartZoomX.Clear();
                            mPrePosEndZoomX.Clear();

                            AxisX.ScaleView.ZoomReset();
                        }

                        if (PrePosIndex_Y == 0)
                        {
                            mPrePosStartZoomY.Clear();
                            mPrePosEndZoomY.Clear();

                            AxisY.ScaleView.ZoomReset();
                        }
                    }
                }

                cTimeManager.GetInst.Delay(100);

                IsMoveMouseWheel = false;
            }
            catch 
            {
                IsMoveMouseWheel = false;
            }
        }

        private void evnMouseUp(object sender, MouseEventArgs e)
        {
            Focus();
            mIsClick = false;
        }

        private void evnMouseDown(object sender, MouseEventArgs e)
        {
            mIsClick = true;

            mClickPosX = e.Location.X;
            mClickPosY = e.Location.Y;
        }

        
        
        private void evnMouseMove(object sender, MouseEventArgs e)
        {

            //clearMarker();
            if (!(mIsClick))
            {
                if (!IsSetGraph) return;
                
                fnGetPoinInfo(e);
            }
            
            if (!(mIsClick)) return;

            fnMoveGraph(e);
        }

        private void evnDragDrop(object sender, DragEventArgs e)
        {
            string[] names = (string[])e.Data.GetData(DataFormats.FileDrop);

            fnFIleInfoToGraph?.BeginInvoke(this, names[0], null, null);
        }

        private void evnDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
    }
}
