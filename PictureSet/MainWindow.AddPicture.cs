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

using FreeloadMap.Lib.Data;

namespace PictureSet
{
    public partial class MainWindow
    {
#warning 似乎UI不允许拖放文件
        private void PictureCanvas_Drop(object sender, DragEventArgs e)
        {
            int i, _8i;
            Point mousePosition = Mouse.GetPosition(this.PictureCanvas);
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);

            this.BeginInit();

            try
            {
                _8i = -8;
                for (i = 0; i < paths.Length; i++)
                {
                    string path = System.IO.Path.GetFullPath(paths[i]);
                    PictureItemControl pictureItem = new PictureItemControl(this);
                    //pictureItem.MoveFinished += PictureItem_MoveFinished;

                    //_8i = i * 8;
                    _8i += 8;
                    PictureItemStructure pictureItemStructure = PictureItemStructure.Create(mousePosition.X + _8i, mousePosition.Y + _8i, 0, path);
                    AddItem(pictureItemStructure);
                }
            }
            catch (Exception ex)
            {
                this.EndInit();
                throw ex;
            }

            this.EndInit();
        }

        private static string addPicture_LastPath = null;
        private void Button_AddPicture_Click(object sender, RoutedEventArgs e)
        {
            SetOpenFileDialog_MultiPictures(
                "Add Pictures",
                //pictureSetProjectFile.Path == null ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : pictureSetProjectFile.Directory
                addPicture_LastPath
                );

            if (openFileDialog.ShowDialog() == true)
            {
                addPicture_LastPath = System.IO.Path.GetDirectoryName(openFileDialog.FileNames[0]);

                int i, _8i;
                string[] picturePaths = openFileDialog.FileNames;

                _8i = 0;
                for (i = 0; i < picturePaths.Length; i++)
                {
                    string path = System.IO.Path.GetFullPath(picturePaths[i]).Replace('\\', '/');
                    //_8i = i * 8;
                    _8i += 8;
                    PictureItem pictureItem = AddItem(PictureItemStructure.Create(_8i, _8i, 0, path));
                    //pictureItem.MoveFinished+=;
                }
            }
        }
    }
}
