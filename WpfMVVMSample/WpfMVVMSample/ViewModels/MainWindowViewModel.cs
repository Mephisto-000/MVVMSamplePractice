using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using WpfMVVMSample.Command;

namespace WpfMVVMSample.ViewModels
{
    /// <summary>
    /// 表示主視窗的視圖模型，負責管理視圖的顯示和命令的處理。
    /// </summary>
    public class MainWindowViewModel : WorkspaceViewModel
    {
        private const string DisplayViewName = "MainWindowView";

        /// <summary>
        /// 單一視圖的工作區域。
        /// </summary>
        private WorkspaceViewModel _workspaceSingle;

        /// <summary>
        /// 歷史視圖集合，用於存儲已顯示的視圖。
        /// </summary>
        private ObservableCollection<WorkspaceViewModel> _workspaceStory;

        /// <summary>
        /// 多視圖的工作區域。
        /// </summary>
        private ObservableCollection<WorkspaceViewModel> _workspaceMulti;

        /// <summary>
        /// 顯示第一個視圖的命令。
        /// </summary>
        public DelegateCommand ShowFirstViewCmd { get; set; }

        /// <summary>
        /// 顯示第二個視圖的命令。
        /// </summary>
        public DelegateCommand ShowSecondViewCmd { get; set; }

        /// <summary>
        /// 創建第一個視圖的命令。
        /// </summary>
        public DelegateCommand CreateFirstViewCmd { get; set; }

        /// <summary>
        /// 創建第二個視圖的命令。
        /// </summary>
        public DelegateCommand CreateSecondViewCmd { get; set; }

        /// <summary>
        /// 顯示上一個視圖的命令。
        /// </summary>
        public DelegateCommand ShowPreviousViewCmd { get; set; }

        /// <summary>
        /// 顯示下一個視圖的命令。
        /// </summary>
        public DelegateCommand ShowNextViewCmd { get; set; }

        /// <summary>
        /// 初始化 MainWindowViewModel 類別的新實例。
        /// </summary>
        public MainWindowViewModel()
        {
            base.DisplayName = DisplayViewName;

            // 初始化單一視圖命令
            ShowFirstViewCmd = new DelegateCommand
            {
                ExecuteCommand = new Action<object>(ShowFirstView)
            };

            ShowSecondViewCmd = new DelegateCommand
            {
                ExecuteCommand = new Action<object>(ShowSecondView)
            };

            // 初始化多視圖命令
            CreateFirstViewCmd = new DelegateCommand
            {
                ExecuteCommand = new Action<object>(CreateFirstView)
            };

            CreateSecondViewCmd = new DelegateCommand
            {
                ExecuteCommand = new Action<object>(CreateSecondView)
            };

            ShowPreviousViewCmd = new DelegateCommand
            {
                ExecuteCommand = new Action<object>(ShowPreviousView)
            };

            ShowNextViewCmd = new DelegateCommand
            {
                ExecuteCommand = new Action<object>(ShowNextView)
            };
        }

        /// <summary>
        /// 獲取或設置單一視圖的工作區域。
        /// </summary>
        public WorkspaceViewModel WorkspaceSingle
        {
            get
            {
                if (_workspaceSingle == null)
                {
                    _workspaceSingle = new WorkspaceViewModel();
                }

                return _workspaceSingle;
            }
            set
            {
                _workspaceSingle = value;
                OnPropertyChanged("WorkspaceSingle");
            }
        }

        /// <summary>
        /// 獲取多視圖的工作區域集合。
        /// </summary>
        public ObservableCollection<WorkspaceViewModel> WorkspaceMulti
        {
            get
            {
                if (_workspaceMulti == null)
                {
                    _workspaceMulti = new ObservableCollection<WorkspaceViewModel>();
                }
                return _workspaceMulti;
            }
        }

        /// <summary>
        /// 顯示第一個視圖。
        /// </summary>
        /// <param name="obj">命令參數。</param>
        private void ShowFirstView(object obj)
        {
            if (_workspaceStory == null)
            {
                _workspaceStory = new ObservableCollection<WorkspaceViewModel>();
            }

            var model = this._workspaceStory.FirstOrDefault(vm => vm is FirstViewModel) as FirstViewModel;

            if (model == null)
            {
                model = new FirstViewModel();
                _workspaceStory.Add(model);
            }

            WorkspaceSingle = model;
        }

        /// <summary>
        /// 顯示第二個視圖。
        /// </summary>
        /// <param name="obj">命令參數。</param>
        private void ShowSecondView(object obj)
        {
            if (_workspaceStory == null)
            {
                _workspaceStory = new ObservableCollection<WorkspaceViewModel>();
            }

            var model = this._workspaceStory.FirstOrDefault(vm => vm is SecondViewModel) as SecondViewModel;

            if (model == null)
            {
                model = new SecondViewModel();
                _workspaceStory.Add(model);
            }

            WorkspaceSingle = model;
        }

        /// <summary>
        /// 創建並顯示第一個視圖。
        /// </summary>
        /// <param name="obj">命令參數。</param>
        private void CreateFirstView(object obj)
        {
            var model = new FirstViewModel();
            WorkspaceMulti.Add(model);
            ShowCurrentView(model);
        }

        /// <summary>
        /// 創建並顯示第二個視圖。
        /// </summary>
        /// <param name="obj">命令參數。</param>
        private void CreateSecondView(object obj)
        {
            var model = new SecondViewModel();
            WorkspaceMulti.Add(model);
            ShowCurrentView(model);
        }

        /// <summary>
        /// 顯示上一個視圖。
        /// </summary>
        /// <param name="obj">命令參數。</param>
        private void ShowPreviousView(object obj)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.WorkspaceMulti);
            if (collectionView != null)
            {
                collectionView.MoveCurrentToPrevious();
            }
        }

        /// <summary>
        /// 顯示下一個視圖。
        /// </summary>
        /// <param name="obj">命令參數。</param>
        private void ShowNextView(object obj)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.WorkspaceMulti);
            if (collectionView != null)
            {
                collectionView.MoveCurrentToNext();
            }
        }

        /// <summary>
        /// 顯示當前選定的視圖。
        /// </summary>
        /// <param name="workspace">要顯示的工作區視圖模型。</param>
        private void ShowCurrentView(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.WorkspaceMulti.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.WorkspaceMulti);
            if (collectionView != null)
            {
                collectionView.MoveCurrentTo(workspace);
            }
        }
    }
}
