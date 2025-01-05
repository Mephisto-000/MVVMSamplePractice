using System; // 提供基本的類別和基礎結構
using System.Collections.Generic; // 提供泛型集合的定義
using System.Configuration; // 提供配置系統的功能
using System.Data; // 提供資料存取的功能
using System.Linq; // 提供查詢操作的功能
using System.Threading.Tasks; // 提供多執行緒和非同步程式設計的功能
using System.Windows; // 提供建立 Windows 應用程式的功能
using WpfMVVMSample.ViewModels; // 引用專案中的 ViewModel 命名空間

namespace WpfMVVMSample
{
    /// <summary>
    /// 定義應用程式的主要入口點，繼承自 Application 類別。
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 當應用程式啟動時觸發此方法。
        /// </summary>
        /// <param name="e">包含啟動事件的相關資料。</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // 呼叫基底類別的 OnStartup 方法，確保基礎啟動邏輯被執行
            base.OnStartup(e);

            // 建立主視窗的實例
            MainWindow window = new MainWindow();

            // 建立主視窗的 ViewModel 實例
            var viewModel = new MainWindowViewModel();

            // 設定主視窗的資料上下文為對應的 ViewModel
            window.DataContext = viewModel;

            // 顯示主視窗
            window.Show();
        }
    }
}