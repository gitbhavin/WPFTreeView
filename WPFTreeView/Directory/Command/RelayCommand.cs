using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFTreeView
{
    /// <summary>
    /// Basic command that run an Action
    /// </summary>
    class RelayCommand : ICommand
    {
        #region Private Member
        /// <summary>
        /// Action to run
        /// </summary>
        private Action mAction;
        #endregion

        #region Public Events

        /// <summary>
        /// Event that fire when the <see cref="CanExecute(object)"/> value changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// 
        //Action to run
        public RelayCommand(Action action)
        {
            mAction = action;
        }

        #endregion

        #region Command Methods


        /// <summary>
        /// A Relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Execute the command Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction();
        }
        #endregion
    }
}
