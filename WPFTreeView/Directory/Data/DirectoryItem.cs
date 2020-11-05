using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPFTreeView
{
    /// <summary>
    /// Information about Directory item such as drive,file or folder
    /// </summary>

    public class DirectoryItem
    {
        /// the directory item type

        public DirectoryItemType Type { get; set; }


        /// <summary>
        ///  The absolute path to this item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of the directory item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(FullPath); } }
    }
}
