using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace Communication_Server.Base.GraphManager
{
    public partial class cGraphControler : Chart
    {
        public FileInfoToGraph GetFileInfoToGraph
        {
            get { return fnFIleInfoToGraph; }
            set { fnFIleInfoToGraph = value; }
        }

        public bool IsSetGraph { get; set; }

        public List<double> GetValueList_Y 
        { 
            get 
            { 
                return mValueList_Y; 
            }
            set
            {
                mValueList_Y = value;

                fpGetValue_XY?.BeginInvoke(mValue_X, mValueList_Y, null, null);
            }
        }
        

        public GetValue_XY fpGetValue_XY { get { return fnGetValue_XY; } set { fnGetValue_XY = value; } }
        
    }
}
