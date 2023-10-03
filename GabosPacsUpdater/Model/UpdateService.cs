using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Windows;
using static GabosPacsUpdater.ViewModel.MainViewModel;

namespace GabosPacsUpdater.Model
{
    public class UpdateService
    {
        public static void UpdatePacs(string sourcePath, string destinationPath, UpdateProgressBarDelegate info)
        {
            try
            {
                bool updateDataBase = false;
                int counter = 0;
                info(counter, 0, $"Rozpoczęcie sprawdzania plików");
                var dir1 = new DirectoryInfo(sourcePath);
                var dir2 = new DirectoryInfo(destinationPath);

                var listSource = dir1.GetFiles("*.*", SearchOption.AllDirectories);
                var listDestination = dir2.GetFiles("*.*", SearchOption.AllDirectories);
                

                var listToCopy = listSource.Where(x => !listDestination.Any(y => y.Name == x.Name && y.LastWriteTime == x.LastWriteTime)).ToList();
                if (listToCopy.Count != listSource.Count()) listToCopy = listToCopy.Where(x => x.Name != "CommonConfiguration.json").ToList();
                int max = listToCopy.Count;
                updateDataBase = destinationPath == Properties.Settings.Default.LocalPathWadoRS && listToCopy.Any(x => x.Name.ToLower().Contains("module"));
                info(counter, max, $"Rozpoczęcie kopiowania plików do {destinationPath}");
                Thread.Sleep(3000);
                foreach (var item in listToCopy)
                {
                    counter++;
                    info(counter, max, $"Status: Kopiowania pliku {item.Name}");
                    string catalogDestination = item.FullName.Replace(sourcePath, destinationPath).Replace(item.Name, "");
                    string fileDestination = item.FullName.Replace(sourcePath, destinationPath);
                    if (!Directory.Exists(catalogDestination)) Directory.CreateDirectory(catalogDestination);
                    File.Copy(item.FullName, fileDestination, true);
                }

                info(max, max, $"Status: Kopiowanie plików zakończono pomyślnie");
                Thread.Sleep(3000);
                var listToDelete = listDestination.Where(x => !listSource.Any(y => y.Name == x.Name)).ToList();
                max = listToDelete.Count;
                counter = 0;
                if (max > 0) info(counter, max, $"Rozpoczęcie usuwania niepotrzebnych plików z {destinationPath}");
                foreach (var item in listToDelete)
                {
                    counter++;
                    info(counter, max, $"Status: Usuwanie pliku {item.Name}");
                    File.Delete(item.FullName);
                }
                if (max > 0) info(counter, max, $"Operacje na plikach przebiegły pomyślnie");
                if (updateDataBase)
                {
                    info(counter, max, $"Rozpoczynam aktualizację bazy danych.");
                    UpdateDataBase(destinationPath);
                    info(counter, max, $"Aktualizacja zakończona.");
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

                var listDestination = dir2.GetFiles("*.*", SearchOption.AllDirectories);
                if (listDestination.Count() == 0) throw new Exception("Brak pobranych plików");
                var listSource = dir1.GetFiles("*.*", SearchOption.AllDirectories);
                


                var listToCopy = listSource.Where(x => !listDestination.Any(y => y.Name == x.Name && y.LastWriteTime == x.LastWriteTime) && !x.Name.Contains("CommonConfiguration.json")).ToList();
                
                if (listToCopy.Count > 0) return false;
                else return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static void UpdateDataBase(string path)
        {
            string serverPath = $"{path}\\GabosPacs.Blazor.Server.exe";
            try
            {
                ProcessStartInfo info = new ProcessStartInfo(serverPath);
                info.Arguments = "--updateDatabase [--forceUpdate --silent]";
                info.UseShellExecute = true;
                info.Verb = "runas";
                Process.Start(info).WaitForExit();
                Thread.Sleep(3000);                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Błąd podczas aktualizacji bazy danych");
            }
        }
    }

    
}
