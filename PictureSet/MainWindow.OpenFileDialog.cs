using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PictureSet
{
    public partial class MainWindow
    {
        private Microsoft.Win32.OpenFileDialog openFileDialog;
        private Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog folderBrowserDialog;

        private void Init_OpenFileDialog()
        {
            openFileDialog = new Microsoft.Win32.OpenFileDialog();
            folderBrowserDialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
        }

        private void SetOpenFileDialog_SingleProjectFile(string title, string initialDirectory, bool addExtension, bool checkExists)
        {
            openFileDialog.FileName = "";

            openFileDialog.AddExtension = addExtension;
            openFileDialog.CheckFileExists = checkExists;
            openFileDialog.CheckPathExists = checkExists;
            openFileDialog.DereferenceLinks = true;
            openFileDialog.Filter = "Picture Set Porject Files (*.psp)|*.psp|Json Documents (*.json)|*.json|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            if (initialDirectory != null)
            {
                openFileDialog.InitialDirectory = initialDirectory;
            }
            openFileDialog.Multiselect = false;
            openFileDialog.Title = title;
            openFileDialog.ValidateNames = true;
        }

        private void SetOpenFileDialog_MultiPictures(string title, string initialDirectory)
        {
            openFileDialog.FileName = "";

            openFileDialog.AddExtension = false;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.DereferenceLinks = true;
            // BitmapSource文件类型支持：https://docs.microsoft.com/zh-cn/dotnet/api/system.windows.media.imaging.bitmapsource?view=net-5.0#remarks
            openFileDialog.Filter =
                "All Supportive Image Files (*.bmp;*.dib;*.gif;*.jpg;*.jpeg;*.jpe;*jfif;*.png;*.tif;*.tiff)|*.bmp;*.dib;*.gif;*.jpg;*.jpeg;*.jpe;*jfif;*.png;*.tif;*.tiff|" +
                "Bitmap Files (*.bmp;*.dib)|*.bmp;*.dib|" +
                "Graphics Interchange Format Files (*.gif)|*.gif|" +
                "Joint Photographics Experts Group Files (*.jpg;*.jpeg;*.jpe;*jfif)|*.jpg;*.jpeg;*.jpe;*jfif|" +
                "Portable Network Graphics Files (*.png)|*.png|" +
                "Tagged Image File Format Files (*.tif;*.tiff)|*.tif;*.tiff|" +
                //"?May Supportive Image files (*.ico;*.wdp;*.webp)|*.ico;*.wdp;*.webp|" +
                "All Files|*.*";
            openFileDialog.FilterIndex = 1;
            if (initialDirectory != null)
            {
                openFileDialog.InitialDirectory = initialDirectory;
            }
            openFileDialog.Multiselect = true;
            openFileDialog.Title = title;
            openFileDialog.ValidateNames = true;
        }

        private void SetFolderBrowserDialog_SingleDirectory(string title, string initialDirectory, bool checkExists)
        {
            folderBrowserDialog.ResetUserSelections();

            folderBrowserDialog.DefaultExtension = null;
            folderBrowserDialog.EnsureFileExists = checkExists;
            folderBrowserDialog.EnsurePathExists = checkExists;
            folderBrowserDialog.IsFolderPicker = true;
            if (initialDirectory != null)
            {
                folderBrowserDialog.InitialDirectory = initialDirectory;
            }
            folderBrowserDialog.Multiselect = false;
            folderBrowserDialog.Title = title;
            folderBrowserDialog.EnsureValidNames = true;
        }
    }
}
