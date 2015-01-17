using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Spam
{
    public class MailListCtrl
    {
        string FileName = "";

        List<string> List = new List<string>();

        public MailListCtrl(string _FileName)
        {
            FileName = _FileName;
        }

        public bool ListExists()
        {
            return File.Exists(FileName);
        }

        public void ReadList()
        {
            if (!ListExists()) return;

            List = (File.ReadAllText(FileName).Split(',')).ToList();
        }

        public void WriteList()
        {

            string fds = string.Join(",", List.ToArray());

            if (!ListExists()) File.Delete(FileName);
            File.WriteAllText(FileName, fds);
        }

        public void ClearList()
        {
            List = new List<string>();
        }

        public int ListCount()
        {
            if (!ListExists()) return 0;

            ReadList();
            return List.Count();
        }

        public bool AddToList(string Mail)
        {

            if (List.Contains(" "+Mail))
                return false;
            else
            {
                List.Add(" " + Mail);
                return true;
            }
        }
    }
}
