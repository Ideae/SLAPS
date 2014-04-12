using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace iteration3wpf
{
    public class LFile : Loadable<LFile>
    {
        [Synchronize(true)]
        private int _Id;
        public override int Id { get {return syncDown("Id", _Id); } set { _Id = syncUp("Id", value); } }

        [Synchronize(true)]
        private string _FileName;
        public string FileName { get { return syncDown("FileName", _FileName); } set { _FileName = syncUp("FileName", value); } }

        [Synchronize(true)]
        private string _Path;
        public string Path { get { return syncDown("Path", _Path); } set { _Path = syncUp("Path", value); } }

        public string relativePath { get { return Path + "\\" + FileName; } }
        protected LFile(int id)
            : base(id)
        {
            _Id = id;
        }

        public override string ToString()
        {
            return FileName;
        }

        internal void Download()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "Save Document";
            saveFileDialog1.FileName = FileName;
            if (saveFileDialog1.ShowDialog() == true)
            {
                if (saveFileDialog1.FileName != "")
                {
                    string fileName = saveFileDialog1.FileName;
                    string fullpath = System.IO.Path.Combine(Path, FileName);
                    if (File.Exists(fileName))
                    {
                        MessageBox.Show("File already exists!");
                        return;
                    }
                    File.Copy(fullpath, fileName);
                    MessageBox.Show("File Saved!");
                    return;
                }
                MessageBox.Show("No filename Entered!");
            }
        }
    }
}
