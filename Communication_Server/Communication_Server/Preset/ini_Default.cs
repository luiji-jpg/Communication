using Communication_Server.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication_Server.Preset
{
    public class cIni_Default
    {
        public string DirPath = cGDef.IniPath_Directory;

        public cIni_Default() { }
        ~cIni_Default() { }

        public virtual void Create(out string Out_StatusMsg, string Arg_DirPath, string Arg_FilePath)
        {
            cGDef.objFileManager.Create_ini(out Out_StatusMsg, Arg_DirPath, Arg_FilePath);
        }

        public virtual bool SetData(string Arg_FilePath, string[] Args_Section, string[] Args_Key, string[] Args_Value)
        {
            string Section = "";
            string Key = "";
            string Value = "";
            string FilePath = Arg_FilePath;

            bool ChkFn = false;

            try
            {
                if ((Args_Section.Length != Args_Key.Length) || (Args_Key.Length != Args_Value.Length)) return ChkFn;

                for (int i = 0; i < Args_Section.Length; i++)
                {

                    Section = Args_Section[i];
                    Key = Args_Key[i];
                    Value = Args_Value[i];

                    cGDef.objFileManager.SetIni(Section, Key, Value, FilePath);
                }
            }
            catch
            {
                return ChkFn;
            }

            ChkFn = true;

            return ChkFn;
        }

        public virtual bool SetData(string Arg_FilePath, string[][] Args_Section, string[][] Args_Key, string[][] Args_Value)
        {
            string Section = "";
            string Key = "";
            string Value = "";
            string FilePath = Arg_FilePath;

            bool ChkFn = false;

            try
            {
                if ((Args_Section.Length != Args_Key.Length) || (Args_Key.Length != Args_Value.Length)) return ChkFn;

                for (int i = 0; i < Args_Section.Length; i++)
                {
                    for (int z = 0; z < Args_Section[i].Length; z++)
                    {
                        Section = Args_Section[i][z];
                        Key = Args_Key[i][z];
                        Value = Args_Value[i][z];

                        cGDef.objFileManager.SetIni(Section, Key, Value, FilePath);
                    }
                }
            }
            catch
            {
                return ChkFn;
            }

            ChkFn = true;

            return ChkFn;
        }

        public virtual bool GetData(out List<string> Out_GetDataList, string Args_FilePath, string[] Args_Section, string[] Args_Key)
        {
            string Section = "";
            string Key = "";
            string FilePath = Args_FilePath;

            StringBuilder GetData = new StringBuilder();

            List<string> GetDataLists = new List<string>();

            bool ChkFn = false;

            try
            {
                if ((Args_Section.Length != Args_Key.Length))
                {
                    Out_GetDataList = null;
                    return ChkFn;
                }

                for (int i = 0; i < Args_Section.Length; i++)
                {
                    GetData.Length = 255;

                    Section = Args_Section[i];
                    Key = Args_Key[i];

                    GetDataLists.Add(cGDef.objFileManager.GetIni(Section, Key, GetData, GetData.Length, FilePath));

                }

                Out_GetDataList = GetDataLists;
            }
            catch
            {
                Out_GetDataList = null;
                return ChkFn;
            }

            ChkFn = true;

            return ChkFn;
        }

        public virtual bool GetData(out List<string> Out_GetDataList, string Args_FilePath, string[][] Args_Section, string[][] Args_Key)
        {
            string Section = "";
            string Key = "";
            string FilePath = Args_FilePath;

            bool ChkFn = false;

            StringBuilder GetData = new StringBuilder();

            List<string> GetDataLists = new List<string>();

            try
            {
                if ((Args_Section.Length != Args_Key.Length))
                {
                    Out_GetDataList = null;
                    return ChkFn;
                }

                for (int i = 0; i < Args_Section.Length; i++)
                {
                    for (int z = 0; z < Args_Section[i].Length; z++)
                    {
                        GetData.Length = 255;

                        Section = Args_Section[i][z];
                        Key = Args_Key[i][z];

                        GetDataLists.Add(cGDef.objFileManager.GetIni(Section, Key, GetData, GetData.Length, FilePath));
                    }
                }

                Out_GetDataList = GetDataLists;
            }
            catch
            {
                Out_GetDataList = null;
                return ChkFn;
            }

            ChkFn = true;

            return ChkFn;
        }
    }
}
