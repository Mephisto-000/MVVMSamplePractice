using System; // 提供基本的系統功能
using System.Windows.Input; // 提供 WPF 的命令相關介面和類別
/*
在 MVVM 模式中，Command 的主要作用是將使用者的操作（例如按鈕點擊）與視圖模型（ViewModel）中的邏輯聯繫起來，
從而實現視圖（View）與業務邏輯的解耦。

在軟體開發中，解耦（Decoupling）指的是將系統的不同部分分離，使它們彼此獨立運作，從而提高系統的可維護性和靈活性。
*/

namespace WpfMVVMSample.Command
{
    /// <summary>
    /// 表示可委派的命令，允許將方法作為命令進行傳遞。
    /// </summary>
    public class DelegateCommand : ICommand
    {
        /// <summary>
        /// 要執行的命令邏輯。
        /// </summary>
        public Action<object> ExecuteCommand = null;

        /// <summary>
        /// 確定命令是否可執行的邏輯。
        /// </summary>
        public Func<object, bool> CanExecuteCommand = null;

        /// <summary>
        /// 當命令的可執行狀態發生變化時觸發的事件。
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 確定此命令是否能在其當前狀態下執行。
        /// </summary>
        /// <param name="parameter">命令使用的資料。如果命令不需要傳遞資料，則該物件可以設為 null。</param>
        /// <returns>如果可以執行此命令，則為 true；否則為 false。</returns>
        public bool CanExecute(object parameter)
        {
            if (CanExecuteCommand != null)
            {
                return this.CanExecuteCommand(parameter);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 執行命令的邏輯。
        /// </summary>
        /// <param name="parameter">命令使用的資料。如果命令不需要傳遞資料，則該物件可以設為 null。</param>
        public void Execute(object parameter)
        {
            if (this.ExecuteCommand != null)
            {
                this.ExecuteCommand(parameter);
            }
        }

        /// <summary>
        /// 引發 <see cref="CanExecuteChanged"/> 事件，指示命令的可執行狀態已更改。
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
