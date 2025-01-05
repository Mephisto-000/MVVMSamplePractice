using System;
using System.ComponentModel;
using System.Diagnostics;

namespace WpfMVVMSample.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// 取得或設定一個值，指示在屬性名稱無效時是否拋出例外狀況。
        /// </summary>
        /// <value>
        /// 若為 <c>true</c>，則在屬性名稱無效時拋出例外狀況；否則，不拋出例外狀況。
        /// </value>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        /// <summary>
        /// 取得或設定顯示名稱。
        /// </summary>
        /// <value>
        /// 顯示名稱的字串值。
        /// </value>
        public virtual string DisplayName { get; protected set; }

        /// <summary>
        /// 當屬性值變更時發生。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged; // 實作 INotifyPropertyChanged 介面的 PropertyChanged 事件

        /// <summary>
        /// 引發 <see cref="PropertyChanged"/> 事件。
        /// </summary>
        /// <param name="propertyName">變更的屬性名稱。</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);  // 驗證屬性名稱的有效性
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); //觸發 PropertyChanged 事件，通知屬性已變更
        }

        /// <summary>
        /// 驗證指定的屬性名稱是否存在於當前物件中。
        /// 僅在偵錯模式下執行。
        /// </summary>
        /// <param name="propertyName">要驗證的屬性名稱。</param>
        /// <exception cref="Exception"></exception>
        [Conditional("DEBUG")]  // 僅在 DEBUG 模式下編譯此方法
        [DebuggerStepThrough]   // 指示偵錯工具在執行程式時不應逐步執行此方法
        public void VerifyPropertyName(string propertyName)
        {   // 使用 TypeDescriptor 確認屬性名稱是否存在於當前物件中

            string msg = "無效的屬性名稱: " + propertyName;

            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                if (ThrowOnInvalidPropertyName)
                {
                    throw new Exception(msg);
                }
                else
                {
                    Debug.Fail(msg);
                }
            }
        }
    }
}
