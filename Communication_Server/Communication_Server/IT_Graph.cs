using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Communication_Server
{
     partial class IT_Graph : Chart
    {
        private const int MAX_ZOOM_SCALE = 15; //최대 확대 레벨

        private bool mIsCtrl = false;  //컨트롤 누르면 Y방향으로 확대,축소 가능
        private bool mIsMouseDown = false; //마우스를 누르면 화면 이동 가능
        private int mDownX = 0;
        private int mDownY = 0;
        private int mMaxStamp = 0; //입력된 데이터의 총 개수
        private int mMinData = int.MaxValue; //입력된 데이터들의 최소값
        private int mMaxData = int.MinValue; //입력된 데이터들의 최대값
        private Point mPrePos; //이전 마우스 위치
        private ChartArea mChartArea = null;
        private List<double[]> mDatas = new List<double[]>(); //데이터 layer관리
        private ToolTip mTooltipX = new ToolTip(); //x축 정보 툴팁
        private List<ToolTip> mToolipsY = new List<ToolTip>(); //y축 정보들 툴팁

        /// <summary>
        /// 그래프 초기 설정 값 세팅
        /// </summary>
        /// <param name="form">적용시키고자 하는 form 입력</param>
        /// <param name="posX">그래프 위치 X</param>
        /// <param name="posY">그래프 위치 Y</param>
        /// <param name="width">그래프 폭</param>
        /// <param name="height">그래프 높이</param>
        public void InitGraph(int posX, int posY, int width, int height)
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();

            mChartArea = new ChartArea();
            mChartArea.Name = "ChartArea1";
            mChartArea.AxisX.ScrollBar.Enabled = false;
            mChartArea.AxisY.ScrollBar.Enabled = false;
            mChartArea.AxisX.StripLines.Add(new StripLine());
            mChartArea.AxisX.LabelStyle.Format = "#.##"; //x축 출력label 포멧 세팅: 소수점 둘째짜리까지만 출력
            mChartArea.AxisY.LabelStyle.Format = "#.##"; //y축 출력label 포멧 세팅: 소수점 둘째짜리까지만 출력
            ChartAreas.Add(mChartArea);

            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            Legends.Add(legend1);

            Location = new Point(posX, posY);
            Size = new Size(width, height);
            Name = "chartControl";
            TabIndex = 0;
            Text = "chart1";

            MouseWheel += IT_Graph_MouseWheel;
            MouseDown += IT_Graph_MouseDown;
            MouseMove += IT_Graph_MouseMove;
            MouseUp += IT_Graph_MouseUp;
            KeyDown += IT_Graph_KeyDown;
            KeyUp += IT_Graph_KeyUp;

            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

            mDatas.Clear();
        }

        public void Clear()
        {
            Series.Clear();
            mChartArea.AxisY.StripLines.Clear();

            mMinData = int.MaxValue;
            mMaxData = int.MinValue;
            mDatas.Clear();
            mToolipsY.Clear();
        }

        /// <summary>
        /// 실제 데이터 입력
        /// </summary>
        /// <param name="name">데이터 Series 이름</param>
        /// <param name="data">데이터 배열</param>
        public void AddData(string name, int[] data)
        {
            int overlap = -1;
            double[] array = null;
            //새로운 버퍼로 복사하여 관리
            double[] buf = new double[data.Length];
            Array.Copy(data, 0, buf, 0, data.Length);
            /*
            try
            {
                Series.Add(name);
            }
            catch(Exception ex)
            {
                
                //return;
            }
            */
            overlap = Series.IndexOf(name); // return index, 없으면 -1 return
            if (overlap < 0)
            {
                Series.Add(name);
            }

            if (overlap >= 0)
            {
                array = new double[mDatas[overlap].Length + buf.Length];
                Array.Copy(mDatas[overlap], 0, array, 0, mDatas[overlap].Length);
                Array.Copy(buf, 0, array, mDatas[overlap].Length, buf.Length);
                mDatas[overlap] = array;
            }
            else
            {
                mChartArea.AxisY.StripLines.Add(new StripLine());
                mToolipsY.Add(new ToolTip());
                mDatas.Add(buf);
                overlap = Series.IndexOf(name);
            }

            Series curSeries = Series[name];
            curSeries.ChartType = SeriesChartType.FastLine; //그래프가 Line 형태로 출력
            curSeries.Points.DataBindY(mDatas[overlap]);

            //최대, 최소값들 세팅
            double maxY = curSeries.Points.FindMaxByValue().YValues[0];
            double minY = curSeries.Points.FindMinByValue().YValues[0];
            mMinData = (int)min(minY, mMinData);
            mMaxData = (int)max(maxY, mMaxData);
            // 0으로 시작할 수 있도록 설정
            if (mMaxData <= 0) mMaxData = 40;   // 임의로 추가한 부분
            mChartArea.AxisY.Minimum = mMinData / 1.2;
            mChartArea.AxisY.Maximum = mMaxData * 1.2;
            mMaxStamp = (int)max(mMaxStamp, mDatas[overlap].Length);

            //마우스 이동시 Maker 추가
            //mChartArea.AxisY.StripLines.Add(new StripLine());
            //mToolipsY.Add(new ToolTip());

            //데이터 관리
            //mDatas.Add(buf);

            
        }

        private void IT_Graph_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                mIsCtrl = false;
        }

        private void IT_Graph_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                mIsCtrl = true;
        }

        private void IT_Graph_MouseUp(object sender, MouseEventArgs e)
        {
            Focus();
            mIsMouseDown = false;
        }

        /// <summary>
        /// 마우스 드래그이 화면 이동 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IT_Graph_MouseMove(object sender, MouseEventArgs e)
        {
            //마우스가 움직였을 때만 처리되도록 예외처리
            if (e.Location == mPrePos)
                return;
            else
                mPrePos = e.Location;

            //마우스 좌표가 화면에서 벗어난 경우 예외 처리
            if (e.Location.X < 0 || e.Location.X >= Size.Width)
                return;
            if (e.Location.Y < 0 || e.Location.Y >= Size.Height)
                return;

            if (mIsMouseDown) //마우스 클릭한 상태에서 화면 이동 처리
            {
                //X축 방향 화면 스크롤
                double minX = mChartArea.AxisX.ScaleView.Position;
                double maxX = mChartArea.AxisX.ScaleView.Position + mChartArea.AxisX.ScaleView.Size;
                double PreX = mChartArea.AxisX.PixelPositionToValue(mDownX);
                double CurX = mChartArea.AxisX.PixelPositionToValue(e.Location.X);
                double left = minX + (PreX - CurX);
                double right = maxX + (PreX - CurX);
                mChartArea.AxisX.ScaleView.Scroll(left);
                mDownX = e.Location.X;

                //Y축 방향 화면 스크롤
                double minY = mChartArea.AxisY.ScaleView.Position;
                double maxY = mChartArea.AxisY.ScaleView.Position + mChartArea.AxisY.ScaleView.Size;
                double PreY = mChartArea.AxisY.PixelPositionToValue(mDownY);
                double CurY = mChartArea.AxisY.PixelPositionToValue(e.Location.Y);
                double bottom = minY + (PreY - CurY);
                double top = maxY + (PreY - CurY);
                mChartArea.AxisY.ScaleView.Scroll(bottom);
                mDownY = e.Location.Y;
            }
            else //마우스 좌클릭 비활성화시 Marker 정보 표시
            {
                drawMarker(e.Location.X);
            }
        }

        private void IT_Graph_MouseDown(object sender, MouseEventArgs e)
        {
            mIsMouseDown = true;
            mDownX = e.Location.X;
            mDownY = e.Location.Y;
        }

        /// <summary>
        /// 마우스 휠동작시 확대 축소 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IT_Graph_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!mIsCtrl) //컨트롤 비활성화시 X축 방향 확대 축소
            {
                double zoomFactor = (e.Delta > 0) ? 0.8 : 1.2;
                double minX = mChartArea.AxisX.ScaleView.ViewMinimum;
                double maxX = mChartArea.AxisX.ScaleView.ViewMaximum;
                if (maxX - minX < MAX_ZOOM_SCALE && e.Delta > 0)
                    return;

                double CurX = mChartArea.AxisX.PixelPositionToValue(e.Location.X);
                double left = CurX - ((CurX - minX) * zoomFactor);
                double right = CurX + ((maxX - CurX) * zoomFactor);
                if (left < 0) left = 0;
                if (right > mMaxStamp) right = mMaxStamp;
                mChartArea.AxisX.ScaleView.Zoom((int)left, (int)right);
            }
            else //컨트롤 활성화시 Y축 방향 확대 축소
            {
                double zoomFactor = (e.Delta > 0) ? 0.8 : 1.2;
                double minY = mChartArea.AxisY.ScaleView.ViewMinimum;
                double maxY = mChartArea.AxisY.ScaleView.ViewMaximum;
                if (maxY - minY < MAX_ZOOM_SCALE && e.Delta > 0)
                    return;

                double CurY = mChartArea.AxisY.PixelPositionToValue(e.Location.Y);
                double bottom = CurY - ((CurY - minY) * zoomFactor);
                double top = CurY + ((maxY - CurY) * zoomFactor);
                double curHeight = mMaxData - mMinData;
                double bottomLimit = mMinData * 1.2;
                double topLimit = mMaxData * 1.2;
                if (bottom < bottomLimit) bottom = bottomLimit;
                if (top > topLimit) top = topLimit;
                mChartArea.AxisY.ScaleView.Zoom((int)bottom, (int)top);
            }
        }

        /// <summary>
        /// 마우스 이동시마다 보이는 Marker들 추가
        /// </summary>
        /// <param name="x">마우스 픽셀 X 좌표</param>
        private void drawMarker(int x)
        {
            clearMarker();

            double minX = mChartArea.AxisX.ScaleView.ViewMinimum;
            double maxX = mChartArea.AxisX.ScaleView.ViewMaximum;
            if (double.IsNaN(minX) || double.IsNaN(maxX))
                return;

            double zoomWidth = maxX - minX;
            double CurX = mChartArea.AxisX.PixelPositionToValue(x);
            if (CurX < minX || CurX > maxX)
                return;

            if (mDatas.Count == 0)
                return;

            //x축 라인 및 툴팁 정보를 전시
            int nCurX = (int)Math.Round(CurX);
            StripLine xLine = mChartArea.AxisX.StripLines[0];
            xLine.IntervalOffset = nCurX;
            xLine.StripWidth = zoomWidth * 0.001;
            xLine.BackColor = Color.FromArgb(100, Color.Red);
            mTooltipX.Show("x:" + nCurX.ToString(), this, x + 5, Size.Height - 30);

            //루프를 돌며 y축 라인 및 툴팁 정보를 전시
            double minY = mChartArea.AxisY.ScaleView.ViewMinimum;
            double maxY = mChartArea.AxisY.ScaleView.ViewMaximum;
            double zoomHeight = maxY - minY;
            int Index = (int)clip(nCurX - 1, 0, mMaxStamp - 1);
            for (int i = 0; i < mChartArea.AxisY.StripLines.Count; ++i)
            {
                if (Series[i].Enabled == false)
                    continue;

                StripLine yLine = mChartArea.AxisY.StripLines[i];
                if (Index >= mDatas[i].Length)
                    continue;

                double dataY = mDatas[i][Index];
                yLine.IntervalOffset = dataY;
                yLine.StripWidth = zoomHeight * 0.001;
                yLine.BackColor = Color.FromArgb(100, Color.Red);
                double pixelPosY = mChartArea.AxisY.ValueToPixelPosition(dataY);
                string label = "y" + (i + 1).ToString() + ":" + dataY.ToString();
                mToolipsY[i].Show(label, this, x + 5, (int)pixelPosY);
            }

        }

        private void clearMarker()
        {
            mChartArea.AxisX.StripLines[0].StripWidth = 0;
            mTooltipX.Hide(this);

            for (int i = 0; i < mChartArea.AxisY.StripLines.Count; ++i)
            {
                StripLine yLine = mChartArea.AxisY.StripLines[i];
                yLine.StripWidth = 0;
                mToolipsY[i].Hide(this);
            }
        }

        private double max(double a, double b) { return (a > b) ? a : b; }
        private double min(double a, double b) { return (a > b) ? b : a; }
        private double clip(double x, double min, double max) { return (x <= min) ? min : ((x >= max) ? max : x); }
    }
}
