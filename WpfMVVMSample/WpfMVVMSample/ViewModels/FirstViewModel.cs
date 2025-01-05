using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMVVMSample.Models;


namespace WpfMVVMSample.ViewModels
{
    /// <summary>
    /// 表示第一個視圖的視圖模型，繼承自 WorkspaceViewModel。
    /// </summary>
    public class FirstViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// 視圖的顯示名稱。
        /// </summary>
        private const string DisplayViewName = "FirstView";

        /// <summary>
        /// 只讀的 InfoModel 實例，用於存儲視圖的相關資訊。
        /// </summary>
        private readonly InfoModel _info;

        /// <summary>
        /// 初始化 FirstViewModel 類別的新實例。
        /// </summary>
        public FirstViewModel()
        {
            // 設定基類的 DisplayName 屬性為視圖的顯示名稱
            base.DisplayName = DisplayViewName;

            // 如果 _info 尚未初始化，則創建新的 InfoModel 實例
            if (_info == null)
            {
                _info = new InfoModel();
            }
            // 設定 _info 的 Name 屬性為視圖的顯示名稱
            _info.Name = DisplayViewName;
        }

        /// <summary>
        /// 獲取 InfoModel 的 Name 屬性值。
        /// </summary>
        public string Name
        {
            get { return _info.Name; }
        }
    }
}
