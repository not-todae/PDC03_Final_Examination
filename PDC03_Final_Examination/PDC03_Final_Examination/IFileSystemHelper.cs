using System;
using System.Collections.Generic;
using System.Text;

namespace PDC03_Final_Examination
{
    public interface IFileSystemHelper
    {
        string GetAppDataFolderPath(string folderName);
    }
}