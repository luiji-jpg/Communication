using Communication_Server.Base.UDPManager;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Communication_Server.Base
{
    public class cControlManager
    {
        private static cControlManager mInst;
        public static cControlManager GetInst { get { if (mInst == null) mInst = new cControlManager(); return mInst; } }

        public void FormUploadPanel(ref Panel panInfo, ref Form formInfo)
        {

            formInfo.TopLevel = false;
            formInfo.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            panInfo.Controls.Add(formInfo);
            formInfo.Visible = true;
            formInfo.Show();

        }

        public void SetControlLayout(ref Control currItem)
        {

            string[] changeList = null;
            Point Location = new Point();
            Size size = new Size();

            float fontSizeRes = 0;

            if (cGDef.mRes_X > 1)
            {
                fontSizeRes = (float)(cGDef.mRes_X - 1.0);
                fontSizeRes *= 4;
            }

            float fontSize = (float)((double)currItem.Font.Size - fontSizeRes);
            FontFamily fontFamily = currItem.Font.FontFamily;

            Font font = new Font(fontFamily, fontSize, currItem.Font.Style, System.Drawing.GraphicsUnit.Point, ((byte)(129)));

            changeList = ((string)currItem.Tag).Split(',');

            foreach (string item in changeList)
            {
                switch (item)
                {
                    case "X":
                        Location.X = (int)((double)currItem.Location.X / cGDef.mRes_X);
                        break;

                    case "Y":
                        Location.Y = (int)((double)currItem.Location.Y / cGDef.mRes_Y);
                        break;

                    case "W":
                        size.Width = (int)(currItem.Width / cGDef.mRes_X);
                        break;

                    case "H":
                        size.Height = (int)(currItem.Height / cGDef.mRes_Y);
                        break;

                }
            }

            if (changeList.Contains("X") && changeList.Contains("Y"))
            {
                currItem.Location = Location;
            }
            else if (changeList.Contains("X"))
            {
                Location.Y = currItem.Location.Y;
                currItem.Location = Location;
            }
            else if (changeList.Contains("Y"))
            {
                Location.X = currItem.Location.X;
                currItem.Location = Location;
            }

            if (changeList.Contains("W") && changeList.Contains("H"))
            {
                currItem.Size = size;
            }
            else if (changeList.Contains("W"))
            {
                size.Height = currItem.Size.Height;
                currItem.Size = size;
            }
            else if (changeList.Contains("H"))
            {
                size.Width = currItem.Size.Width;
                currItem.Size = size;
            }

            currItem.Font = font;

        }
    }
}
