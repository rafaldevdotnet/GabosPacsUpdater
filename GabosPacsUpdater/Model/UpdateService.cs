using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace GabosPacsUpdater.Model
{
    public class UpdateService
    {
        public static void UpdatePacs(string sourcePath, string destinationPath)
        {
            try
            {
                var dir1 = new DirectoryInfo(sourcePath);
                var dir2 = new DirectoryInfo(destinationPath);

                var listSource = dir1.GetFiles("*.*", SearchOption.AllDirectories);
                var listDestination = dir2.GetFiles("*.*", SearchOption.AllDirectories);


                var listToCopy = listSource.Where(x => !listDestination.Any(y => y.Name == x.Name && y.LastWriteTime == x.LastWriteTime)).ToList();
                if (listToCopy.Count != listSource.Count()) listToCopy = listToCopy.Where(x => x.Name != "CommonConfiguration.json").ToList();
                foreach (var item in listToCopy)
                {
                    string catalogDestination = item.FullName.Replace(sourcePath, destinationPath).Replace(item.Name, "");
                    string fileDestination = item.FullName.Replace(sourcePath, destinationPath);
                    if (!Directory.Exists(catalogDestination)) Directory.CreateDirectory(catalogDestination);
                    File.Copy(item.FullName, fileDestination, true);
                }

                var listToDelete = listDestination.Where(x => !listSource.Any(y => y.Name == x.Name)).ToList();
                foreach (var item in listToDelete)
                {
                    File.Delete(item.FullName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd podczas aktualizacji");
            }
        }

        public static bool IsUpToDate(string sourcePath, string destinationPath)
        {
            try
            {
                var dir1 = new DirectoryInfo(sourcePath);
                var dir2 = new DirectoryInfo(destinationPath);

                var listSource = dir1.GetFiles("*.*", SearchOption.AllDirectories);
                var listDestination = dir2.GetFiles("*.*", SearchOption.AllDirectories);


                var listToCopy = listSource.Where(x => !listDestination.Any(y => y.Name == x.Name && y.LastWriteTime == x.LastWriteTime && y.Name != "CommonConfiguration")).ToList();
                if (listToCopy.Count > 0) return false;
                else return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd podczas sprawdzania aktualizacji");
                throw;
            }
        }
    }

    
}
