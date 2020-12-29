using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Communication_Server
{
    public partial class DlgExampleGraph : Form
    {
        private IT_Graph mGraph = new IT_Graph();
        public DlgExampleGraph()
        {
            try
            {
                InitializeComponent();

                //그래프 생성 : 10,10은 그래프 위치 좌표, 800,400은 크기 width,height 설정
                mGraph.InitGraph(10, 10, 800, 400);
                //mGraph를 현재 폼에 삽입하기 위한 구문
                Controls.Add(mGraph);
                mGraph.Titles.Add("TITLE!!");
            }
            catch
            {

            }
            
        }

        public void fnDispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
             base.Dispose(disposing);
        }

        public void Load_Graph(int[] arr)
        {
           // int start = graph.Length;
            int end = arr.Length; //데이터 총 샘플 개수
            if (end < 1) return;
            List<int[]> valueList = new List<int[]>();

            valueList.Add(new int[end]);

            
            for (int i = 0; i < end; i++)
            {

                valueList[0][i] = arr[i];
            }

            mGraph.Clear();
            //그래프 요소에 추가
            mGraph.AddData("1열 요소", valueList[0]);
            valueList.Clear();//clear를 시킨다.

            mGraph.Focus(); //Focus를 해야만 Ctrl키를 입력 받을 수 있다.
        }
        public void Load_Graph(List<int> List)
        {
            // int start = graph.Length;
            int end = List.Count; //데이터 총 샘플 개수
            if (end < 1) return;
            List<int[]> valueList = new List<int[]>();

            valueList.Add(new int[end]);


            for (int i = 0; i < end; i++)
            {
                valueList[0][i] = List[i];
            }
            //mGraph.Clear();
            //그래프 요소에 추가
            mGraph.AddData("수신받은 신호", valueList[0]);
            valueList.Clear();//clear를 시킨다.

            mGraph.Focus(); //Focus를 해야만 Ctrl키를 입력 받을 수 있다.
        }
        private void evnLoad(object sender, EventArgs e)
        {
            // 그래프 띄우는 창.
        }
    }
}
