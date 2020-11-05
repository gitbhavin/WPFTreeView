using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
namespace WPFTreeView
{
    /// <summary>
    /// 
    /// </summary>
   public class BaseViewModel : INotifyPropertyChanged
    {
        //we have use PropertyChanged.fody packge from nuget which will tack care of propertychanged 
        //no need to sapratly implement method

        /// <summary>
        /// Event that is fire when any child Property Changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #region Testing Commented code
        //private string mtest;
        //public string Test { get; set; } = "Test Property";
        ////{
        ////    get
        ////    {
        ////        return mtest;
        ////    }
        ////    set
        ////    {
        ////        if (mtest == value)
        ////            return ;

        ////        mtest = value;
        ////      //  PropertyChanged(this, new PropertyChangedEventArgs(nameof(Test)));
        ////    }
        ////}

        //public BaseViewModel()
        //{
        //    Task.Run(async () =>
        //    {

        //        int i = 0;
        //        while (true)
        //        {
        //            await Task.Delay(100);
        //            Test = (i++).ToString();
        //           // PropertyChanged(this, new PropertyChangedEventArgs("Test"));
        //        }
        //    });

        //}

        #endregion
    }
}
