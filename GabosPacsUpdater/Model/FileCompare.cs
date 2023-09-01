using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabosPacsUpdater.Model
{
    public class FileCompare : IEqualityComparer<System.IO.FileInfo>
    {
        public bool Equals(System.IO.FileInfo f1, System.IO.FileInfo f2)
        {
            return (f1.Name == f2.Name);
        }
        public int GetHashCode(System.IO.FileInfo fi)
        {
            string s = fi.Name;
            return s.GetHashCode();
        }
    }
}
