using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFTreeView
{
    /// <summary>
    /// A view model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region public Properties
        public DirectoryItemType Type { get; set; }
        /// <summary>
        /// the full path to the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// Name of this directory item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }

        /// <summary>
        /// List of all children contained inside this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        ///Indicate if this item can be expanded
        /// </summary>
        /// 
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        /// <summary>
        /// indicate the current item is expanded or not
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                ///If the Ui tells us to expand..
                if (value == true)
                    ///find all the children
                    Expand();

                // If Ui tell us to close
                else
                    this.ClearChildren();
            }
        }

        #endregion

        #region Public Command

        /// <summary>
        /// Command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Contstructor
        /// </summary>
        /// <param name="fullpath">The full path of this item</param>
        /// <param name="type">type of item</param>
        public DirectoryItemViewModel(string fullpath, DirectoryItemType type)
        {
            //Create Command
            this.ExpandCommand = new RelayCommand(Expand);

            //set the full path
            this.FullPath = fullpath;
            this.Type = type;

            //setup the children as needed
            this.ClearChildren();
        }
        #endregion

        #region Helper Method
        /// <summary>
        /// Remove all the children from the list, adding a dummy item to show the expand icon if required
        /// </summary>
        private void ClearChildren()
        {
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            //show the expand arrow ifwe are not file
            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }

        #endregion

        /// <summary>
        /// Expand the directory and find all the children
        /// </summary>
        private void Expand()
        {
            //we can not expand file
            if (this.Type == DirectoryItemType.File)
                return;

            //Find all Children
            var children = DirectoryStructure.GetDirectoryContents(this.FullPath);
            this.Children = new ObservableCollection<DirectoryItemViewModel>(
                                children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }
    }
}
