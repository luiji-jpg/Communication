using Communication_Server.Base.UDPManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Communication_Server.Base
{
    public class cFileManager
    {
        static private cFileManager mInst = null;
        static public cFileManager GetInst { get { if (mInst == null) mInst = new cFileManager(); return mInst; } }

        string RootPath = System.Environment.CurrentDirectory;

        delegate void CollSetIni();
        delegate void CollGetIni();

        static public void CreateFolder(string _dirName)
        {
            string sDirPath = Application.StartupPath + "\\" + _dirName;
            DirectoryInfo di = new DirectoryInfo(sDirPath);
            Console.WriteLine(sDirPath);
            if (di.Exists == false)
            {
                di.Create();
            }
        }

        public byte[] LoadBinaryFile(out string _name)
        {
            _name = " ";
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _name = dlg.FileName.Split('\\').Last();
                    return File.ReadAllBytes(@dlg.FileName);
                }
            }
            return new byte[0];
        }

        public string SaveFile(string[] _data, string _filename = null)
        {
            if (_data.Length == 0)
                return "";

            if (_filename != null)
            {
                string[] path = _filename.Split('\\');
                if (path.Length > 1)
                {
                    CreateFolder(path[0]);
                }

                using (StreamWriter outputFile = new StreamWriter(@_filename))
                {
                    foreach (string line in _data)
                    {
                        outputFile.WriteLine(line);
                    }
                }
                return _filename;
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "csv(*.csv)|*.csv";
            saveFileDialog1.Title = DateTime.Now.ToString("HHmmss");
            saveFileDialog1.ShowDialog();

            _filename = saveFileDialog1.FileName.Split('\\').Last();

            if (_filename != "")
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(_filename);
                if (fi.Exists)
                {
                    System.IO.File.Delete(_filename);
                    File.WriteAllLines(saveFileDialog1.FileName, _data);
                }
                else
                {
                    File.WriteAllLines(saveFileDialog1.FileName, _data);

                }

                saveFileDialog1.Dispose();

                return _filename;
            }
            return "";
        }

        public string[] LoadFile(out string _name)
        {
            string[] dataList = new string[0];
            _name = " ";
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "csv(*.csv)|*.csv";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var attr = File.GetAttributes(dlg.FileName);
                        attr = attr | FileAttributes.ReadOnly;
                        File.SetAttributes(dlg.FileName, attr);

                        StreamReader sr = new StreamReader(dlg.FileName);
                        _name = dlg.FileName.Split('\\').Last();

                        List<string> vec = new List<string>();
                        while (sr.Peek() != -1)
                        {
                            string line = sr.ReadLine();
                            vec.Add(line);
                        }
                        dataList = vec.ToArray();
                        File.SetAttributes(dlg.FileName, File.GetAttributes(dlg.FileName) & ~FileAttributes.ReadOnly);
                    }
                    catch { MessageBox.Show("파일이 열려있습니다."); }
                }
            }
            return dataList;
        }

        public bool LoadFile(DataGridView _view, out string _name)
        {
            bool chkResult = false;
            _name = "";
            _view.RowHeadersVisible = false;
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "csv(*.csv)|*.csv";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var attr = File.GetAttributes(dlg.FileName);
                        attr = attr | FileAttributes.ReadOnly;
                        File.SetAttributes(dlg.FileName, attr);

                        _name = dlg.FileName;
                        StreamReader sr = new StreamReader(dlg.FileName);

                        _view.Columns.Clear();
                        string firstLine = sr.ReadLine();
                        string[] tops = firstLine.Split(',');

                        if (_view.Name.Equals("gvNFScenario"))
                        {
                            for (int j = 0; j < tops.Length; ++j)
                            {
                                if (!(tops[j].Equals("Number") ||
                                      tops[j].Equals("Freq(MHz)") ||
                                      tops[j].Equals("theta(degree)") ||
                                      tops[j].Equals("Phi(degree)") ||
                                      tops[j].Equals("PW(us)") ||
                                      tops[j].Equals("Ch Number") ||
                                      tops[j].Equals("DTRB#")))
                                {
                                    MessageBox.Show("다른 파일을 선택하여 주십시요.");
                                    sr.Dispose();
                                    return false;
                                }
                            }
                        }

                        for (int i = 0; i < tops.Length; ++i)
                        {
                            _view.Columns.Add(tops[i], tops[i]);
                            _view.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                        }


                        _view.Rows.Clear();
                        while (sr.Peek() != -1)
                        {
                            string line = sr.ReadLine();
                            string[] cols = line.Split(',');
                            _view.Rows.Add(cols);
                        }
                        File.SetAttributes(dlg.FileName, File.GetAttributes(dlg.FileName) & ~FileAttributes.ReadOnly);
                        //return true;
                        chkResult = true;
                    }
                    catch { MessageBox.Show("파일이 열려있습니다."); }
                }
            }
            return chkResult;
        }
        
        public string[] LoadFile_CAL(out string _name, string FolderName, string FileName)
        {
            string[] dataList = new string[0];
            string progPath = "";
            string filePath = "";
            string fileFilter = ".csv";

            progPath = Directory.GetCurrentDirectory();
            filePath += progPath + FolderName + FileName + fileFilter;

            try
            {
                var attr = File.GetAttributes(filePath);
                attr = attr | FileAttributes.ReadOnly;
                File.SetAttributes(filePath, attr);

                StreamReader sr = new StreamReader(filePath);
                _name = filePath.Split('\\').Last();

                List<string> vec = new List<string>();
                while (sr.Peek() != -1)
                {
                    string line = sr.ReadLine();
                    vec.Add(line);
                }

                dataList = vec.ToArray();
                File.SetAttributes(filePath, File.GetAttributes(filePath) & ~FileAttributes.ReadOnly);
            }
            catch 
            {
                _name = "";
                MessageBox.Show("파일이 열려있습니다."); 
            }

            return dataList;
        }
        
        public string[] readRow(DataGridView _gridview, int rowIndex)
        {
            if (rowIndex >= _gridview.Rows.Count)
                return new string[0];

            var row = _gridview.Rows[rowIndex].Cells;
            List<string> vec = new List<string>();
            //한 줄을 vec변수에 넣는 for문. Cell을 string으로 변환 (1개씩)
            for (int i = 0; i < row.Count; i++)
            {
                string item = row[i].Value.ToString();
                vec.Add(item);
            }

            return vec.ToArray();//모든 데이터가 입력된 후 배열로 변환하여 리턴
        }

        public string[] readRow(ListView _lv, int rowIndex)
        {
            if (rowIndex >= _lv.Items.Count)
                return new string[0];

            int count = _lv.Items[rowIndex].SubItems.Count;
            string[] ret = new string[count];
            for (int i = 0; i < count; i++)
                ret[i] = _lv.Items[rowIndex].SubItems[i].Text;

            return ret;
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        // 파일에 작성할때
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        // 파일로부터 읽어올때
        public void SetIni(string section, string key, string value, string filePath)
        {
            string UseFilePath = Application.StartupPath + filePath;
            WritePrivateProfileString(section, key, value, UseFilePath);
        }

        public string GetIni(string section, string key, StringBuilder retvalue, int size, string filePath)
        {
            string UseFilePath = Application.StartupPath + filePath;
            GetPrivateProfileString(section, key, "(NONE)", retvalue, size, UseFilePath);
            return retvalue.ToString();
        }

        public bool IsDirectory(string path)
        {
            bool isChk = false;

            DirectoryInfo DirInfo = new DirectoryInfo(path);

            isChk = DirInfo.Exists;

            return isChk;
        }

        public bool MakeDirectory(string Path)
        {
            bool bChk = false;

            DirectoryInfo DirInfo = null;

            DirInfo = new DirectoryInfo(Path);

            if (!DirInfo.Exists) DirInfo.Create();

            cGDef.objTimeManager.Delay(300);

            bChk = DirInfo.Exists;

            return bChk;
        }

        public bool IsFile(string Path)
        {
            bool isChk = false;

            System.IO.FileInfo FileInfo = null;

            FileInfo = new System.IO.FileInfo(Path);

            isChk = FileInfo.Exists;

            return isChk;
        }

        public bool MakeFile(string Path)
        {
            bool bChk = false;

            FileStream FileInfo = null;

            FileInfo = new FileInfo(Path).Open(FileMode.OpenOrCreate, FileAccess.ReadWrite);
            cGDef.objTimeManager.Delay(300);
            bChk = IsFile(Path);

            if (bChk)
            {
                FileInfo.Close();
                FileInfo.Dispose();
            }

            return bChk;
        }

        public void Create_ini(out string StatusMsg, string DirPath, string FilePath)
        {
            bool bIsDir = false;
            bool bIsFile = false;
            bool bIsMakeFile = false;

            string UseDirPath = Application.StartupPath + DirPath;
            string UseFilePath = Application.StartupPath + FilePath;

            bIsDir = IsDirectory(UseDirPath);

            if (!(bIsDir))
            {
                MakeDirectory(UseDirPath);
            }

            bIsDir = IsDirectory(UseDirPath);

            if (!(bIsDir))
            {
                StatusMsg = "해당 위치에 폴더가 없습니다.";
                return;
            }

            bIsFile = IsFile(UseFilePath);

            if (!(bIsFile))
            {
                bIsFile = MakeFile(UseFilePath);
                bIsMakeFile = true;
            }

            bIsFile = cGDef.objFileManager.IsFile(UseFilePath);

            if (!(bIsFile))
            {
                StatusMsg = "해당 위치에 파일이 없습니다.";
                return;
            }

            if (bIsMakeFile)
            {
                StatusMsg = "Create";
            }
            else
            {
                StatusMsg = "Exists";
            }
        }
    }
}
