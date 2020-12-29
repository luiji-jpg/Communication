using System;
using System.Windows.Forms;


namespace Communication_Server.Base
{
    public class cTimeManager
    {
        private static cTimeManager mInst;
        public static cTimeManager GetInst { get { if (mInst == null) mInst = new cTimeManager(); return mInst; } }

        public void Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;

            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);

            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                Application.DoEvents();
                ThisMoment = DateTime.Now;
            }
        }

    }
}
