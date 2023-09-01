using System;
using System.Collections.Generic;
using System.Linq;

namespace GabosPacsUpdater.Model
{
    public class UpdateService
    {
        public static void UpdatePacsWadoRSAsync(string sourcePath, string destinationPath)
        {
            try
            {
                System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(sourcePath);
                System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(destinationPath);

                IEnumerable<System.IO.FileInfo> list1 = dir1.GetFiles("*.*",
                System.IO.SearchOption.AllDirectories);

                IEnumerable<System.IO.FileInfo> list2 = dir2.GetFiles("*.*",
                System.IO.SearchOption.AllDirectories);

                bool IsInDestination = false;
                bool IsInSource = false;

                foreach (System.IO.FileInfo s in list1)
                {
                    IsInDestination = true;

                    foreach (System.IO.FileInfo s2 in list2)
                    {
                        if (s.Name == s2.Name)
                        {
                            IsInDestination = true;
                            break;
                        }
                        else
                        {
                            IsInDestination = false;
                        }
                    }

                    if (IsInDestination)
                    {
                        System.IO.File.Copy(s.FullName, System.IO.Path.Combine(destinationPath, s.Name), true);
                    }
                }

                list1 = dir1.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

                list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

                bool areIdentical = list1.SequenceEqual(list2, new FileCompare());

                if (!areIdentical)
                {
                    foreach (System.IO.FileInfo s in list2)
                    {
                        IsInSource = true;

                        foreach (System.IO.FileInfo s2 in list1)
                        {
                            if (s.Name == s2.Name)
                            {
                                IsInSource = true;
                                break;
                            }
                            else
                            {
                                IsInSource = false;
                            }
                        }

                        if (!IsInSource)
                        {
                            System.IO.File.Copy(s.FullName, System.IO.Path.Combine(sourcePath, s.Name), true);
                        }
                    }
                }

                Console.WriteLine("Press any key to exit.");

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

    
}
