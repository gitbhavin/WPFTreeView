using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WPFTreeView
{
    /// <summary>
    /// Covert a full path to a specific image type of a drive,folder or file
    /// </summary>
    /// 
    //[ValueConversion(typeof(string), typeof(BitmapImage))]
    [ValueConversion(typeof(DirectoryItemType), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ////Get the full Path
            //var path = (string)value;


            ////if the path is null, ignore
            //if (path == null)
            //    return null;

            ////Get the name of the file/folder
            //var name = DirectoryStructure.GetFileFolderName(path);

            //By default,we presume an image
            var image = "Images/file.png";

            ////if name is blank,we presume it's a drive as we cannot have a blank file or folder name
            //if (string.IsNullOrEmpty(name))

            switch ((DirectoryItemType)value)
            {
                case DirectoryItemType.Drive:
                    image = "Images/drive.png";
                    break;
                case DirectoryItemType.Folder:
                    image = "Images/folder-closed.png";
                    break;
            }
            //image = "Images/drive.png";
            //else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
            //    image = "Images/folder-closed.png";

            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
