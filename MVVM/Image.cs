using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MVVM
{
    public class MyImage
    {
        public BitmapImage? Source { get; set; }
        public string? FilePath { get; set; }
        public string? FileName { get; set; }
        public long Size { get; set; }
    }

}
