using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTreeView
{

    class DirectoryStructure
    {
        /// <summary>
        /// Get logical drive on the machinke
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            return System.IO.Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
        }

        ///Get the directory top level content
        ///

        public static List<DirectoryItem> GetDirectoryContents(string fullpath)
        {
            //Create empty list
            var items = new List<DirectoryItem>();

            #region Get Folder
            //try and get directories from the folder
            //ignoring any issues doing so
            try
            {
                var dirs = System.IO.Directory.GetDirectories(fullpath);
                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
            }
            catch { }
            #endregion

            #region Get files
            //try and get files from the folder
            //ignoring any issues doing so

            try
            {
                var fs = System.IO.Directory.GetFiles(fullpath);

                if (fs.Length > 0)
                    items.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
            }
            catch { }
            #endregion

            return items;
        }

        #region Helpers
        /// <summary>
        /// Find file or folder from full path
        /// </summary>
        /// <param name="path">the full path</param>
        /// <returns>file or folder name</returns>


        public static string GetFileFolderName(string path)
        {
            //if we have no path return the empty string
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            //make all slashes backslashs
            var normalizedPath = path.Replace('/', '\\');

            //find the last backslash in path
            var lastindex = normalizedPath.LastIndexOf('\\');

            //if we dont find the backslas return the path
            if (lastindex < 0)
                return path;

            // Return the name after last backslash
            return normalizedPath.Substring(lastindex + 1);
        }
        #endregion
    }
}
