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
using System.Windows.Shapes;

namespace WPFTreeView
{
    /// <summary>
    /// Interaction logic for TreeViewWindowxaml.xaml
    /// </summary>
    public partial class TreeViewWindowxaml : Window
    {
        public TreeViewWindowxaml()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(var directory in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem()
                {
                    Header = directory,
                    Tag = directory
                };

                item.Items.Add(null);

                item.Expanded += Folder_Expanded;


                WPfTreeview.Items.Add(item);

            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            item.Items.Clear();

            string fullpath = (string)item.Tag;

            #region GetFolder
            var directories = new List<string>();

            try
            {
                var drs = Directory.GetDirectories(fullpath);
                if (drs.Length > 0)
                {
                    directories.AddRange(drs);
                }
            }

            catch { 
            }

            directories.ForEach(folderPath =>
            {
                var subitem = new TreeViewItem()
                {
                    Header = GetFileFolderName(folderPath),
                    Tag = folderPath
                };

                subitem.Items.Add(null);
                subitem.Expanded += Folder_Expanded;

                item.Items.Add(subitem);

            });
            #endregion

            #region Get Files

            var files = new List<string>();

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


                item.Items.Add(subitem);
            });

            #endregion

        }

        private object GetFileFolderName(string Path)
        {
            if (string.IsNullOrEmpty(Path))
            {
                return string.Empty;
            }

            var normalizedpath = Path.Replace('/', '\\');

            var lastindex = normalizedpath.LastIndexOf('\\');

            if (lastindex <= 0)
            {
                return Path;
            }

            else
            {
                return Path.Substring(lastindex + 1);
            }
        }
    }
}
