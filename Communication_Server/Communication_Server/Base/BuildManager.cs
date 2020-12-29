using Communication_Server.Base.UDPManager;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Communication_Server.Base
{
    public class cBuildManager
    {
        static private cBuildManager mInst = null;
        static public cBuildManager GetInst { get { if (mInst == null) mInst = new cBuildManager(); return mInst; } }

        public string FnGetVersion()
        {
            string result = "";
            try
            {
                //1. Assembly.GetExecutingAssembly().FullName의 값은
                //'ApplicationName, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
                //와 같다.
                string data = Assembly.GetExecutingAssembly().FullName;
                string strVersionText = Assembly.GetExecutingAssembly().FullName
                        .Split(',')[1]
                        .Trim()
                        .Split('=')[1];

                string[] version = strVersionText.Split('.');

                //2. Version Text의 세번째 값(Build Number)은 2000년 1월 1일부터
                //Build된 날짜까지의 총 일(Days) 수 이다.
                int intDays = Convert.ToInt32(version[2]);
                DateTime refDate = new DateTime(2000, 1, 1);
                DateTime dtBuildDate = refDate.AddDays(intDays);

                //3. Verion Text의 네번째 값(Revision NUmber)은 자정으로부터 Build된
                //시간까지의 지나간 초(Second) 값 이다.
                int intSeconds = Convert.ToInt32(version[3]);
                intSeconds = intSeconds * 2;
                dtBuildDate = dtBuildDate.AddSeconds(intSeconds);


                //4. 시차조정
                DaylightTime daylingTime = TimeZone.CurrentTimeZone
                        .GetDaylightChanges(dtBuildDate.Year);
                if (TimeZone.IsDaylightSavingTime(dtBuildDate, daylingTime))
                    dtBuildDate = dtBuildDate.Add(daylingTime.Delta);

                result = dtBuildDate.ToString("yyMMdd");
            }
            catch (Exception e)
            {
                string Class = "cBuildManager";
                string Method = "FnGetVersion";
                string Line = Regex.Replace((e.StackTrace).Split(':')[(e.StackTrace).Split(':').Length - 1], @"\D", " ").Trim();

                cGDef.objExcHandler.GetErrMsgList_Class.Add(Class);
                cGDef.objExcHandler.GetErrMsgList_Method.Add(Method);
                cGDef.objExcHandler.GetErrMsgList_Line.Add(Line);

                result = null;
            }

            return result;
        }
    }
}
