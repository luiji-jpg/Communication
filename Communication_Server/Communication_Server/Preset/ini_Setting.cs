using Communication_Server.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication_Server.Preset
{
    public class cIni_Setting : cIni_Default
    {
        private static cIni_Setting mInst;
        public static cIni_Setting GetInst() { if (mInst == null) mInst = new cIni_Setting(); return mInst; }

        #region Info
        public string FilePath = cGDef.IniPath_Setting;
        #endregion

        #region Control
        public string[] DefaultSection = 
        {
            "tbServerIP",
            " tbServerPort",

            "tbStartTime",
            "tbEndTime",
        };
        #endregion

        #region Key
        public string[] DefaultKey = 
        {
            "Text",
            "Text",

            "Text",
            "Text"
        };
        #endregion

        #region Value
        public string[] DefaultValue = 
        {
            "192.168.0.81",
            "10000",

            "0",
            "10"
        };
        #endregion

        public cIni_Setting() { }
        ~cIni_Setting() { }

        public override void Create(out string Out_StatusMsg, string Arg_DirPath, string Arg_FilePath)
        {
            base.Create(out Out_StatusMsg, Arg_DirPath, Arg_FilePath);
        }

        public override bool SetData(string Arg_FilePath, string[] Args_Section, string[] Args_Key, string[] Args_Value)
        {
            bool ChkFn = false;

            ChkFn = base.SetData(Arg_FilePath, Args_Section, Args_Key, Args_Value);

            return ChkFn;
        }

        public override bool GetData(out List<string> Out_GetDataList, string Args_FilePath, string[] Args_Section, string[] Args_Key)
        {
            bool ChkFn = false;

            ChkFn = base.GetData(out Out_GetDataList, Args_FilePath, Args_Section, Args_Key);

            return ChkFn;
        }
    }
}
