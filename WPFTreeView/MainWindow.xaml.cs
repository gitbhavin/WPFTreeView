using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;



namespace WPFTreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            //get every logical drive on the machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                //Create new item for it
                var item = new TreeViewItem()
                {
                    //Set The Header
                    Header = drive,

                    //Set the Full Path
                    //We can store coustome information about item using Tag
                    Tag = drive
                };

                //Add Dummy Data
                item.Items.Add(null);

                //listne out for item being expanded
                item.Expanded += Folder_Expanded;

                //Add item to the Main treeView
                FolderView.Items.Add(item);
            }
          

           
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            //if th item only contain the dummey data
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            //clear the Dummy Data
            item.Items.Clear();

            //Get Ful Path
            string fullpath = (string)item.Tag;

            #region Get Folders

            //Create a blank list for folder
            var folders = new List<string>();

            //Try and get the Folder from the directories
            //ignoring any issue doing so
            try
            {
                var dir = Directory.GetDirectories(fullpath);
                if (dir.Length > 0)
                {
                    folders.AddRange(dir);
                }
            }
            catch
            {

            }

            //for each directory..
            folders.ForEach(folderpath =>
            {
                //Create Directory Item
                var subitem = new TreeViewItem()
                {
                    //set header as folderName
                    Header = GetFileFolderName(folderpath),
                    //and Tag as a full path
                    Tag = folderpath
                };

                //Add dummey item to subitem;
                subitem.Items.Add(null);

                //Hendel Expanding
                subitem.Expanded += Folder_Expanded;

                //Add this item to parent
                item.Items.Add(subitem);
            });

            #endregion

            #region GetFiles
            //Create Empty list FOr Files
            var files = new List<string>();

            //try to get al files from the directories
            try
            {
                var fls = Directory.GetFiles(fullpath);
                if (fls.Length > 0)
                {
                    files.AddRange(fls);
                }
            }
            catch { }

            files.ForEach(filepath =>
            {
                var subitem = new TreeViewItem()
                {
                    Header = GetFileFolderName(filepath),
                    Tag = filepath
                };

                //Add this item to the parent
                item.Items.Add(subitem);
            });
            #endregion

        }

        //Find the file or foldername from the full path
        public static string GetFileFolderName(string Path)
        {
            //if we have no path, return empty
            if (string.IsNullOrEmpty(Path))
                return string.Empty;

            //make all slashes backslashs
            var normalizedpath = Path.Replace('/', '\\');

            //find the last index of backslash
            var lastIndex = normalizedpath.LastIndexOf('\\');

            //if we dont find slash return the path itself
            if (lastIndex <= 0)
                return Path;

            //return the name after last back slash
            return Path.Substring(lastIndex + 1);


        }
    }
}
