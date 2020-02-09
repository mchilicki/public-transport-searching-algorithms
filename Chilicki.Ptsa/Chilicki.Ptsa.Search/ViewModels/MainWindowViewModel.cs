using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Ptsa.Search.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {

        }


        private DelegateCommand selectFolderCommand;
        public DelegateCommand SelectFolderCommand =>
            selectFolderCommand ?? (selectFolderCommand = new DelegateCommand(ExecuteSelectFolderCommand));

        void ExecuteSelectFolderCommand()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            }
        }



    }
}
