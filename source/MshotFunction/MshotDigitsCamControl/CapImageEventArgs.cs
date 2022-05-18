using System;

namespace MshotDigitsCamControl
{
    public class CapImageEventArgs : EventArgs
    {
        private string study_no;
        private string file_path;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sno">检查号</param>
        /// <param name="fpath">文件路径</param>
        public CapImageEventArgs(string sno, string fpath)
        {
            this.study_no = sno;
            this.file_path = fpath;
        }

        public string SNO
        {
            get { return study_no; }
        }
        public string FPATH
        {
            get { return file_path; }
        }
    }
}
