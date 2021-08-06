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
        private PictureSetProjectFile pictureSetProjectFile = null;

        private void Init_Project()
        {
            pictureSetProjectFile = new PictureSetProjectFile();
        }

        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            SetOpenFileDialog_SingleProjectFile(
                "Select Project Document",
                pictureSetProjectFile.Path == null ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : pictureSetProjectFile.Directory,
                false,
                true);

            if (openFileDialog.ShowDialog() == true)
            {
                string loadPath = openFileDialog.FileName.Replace('\\', '/');

                MessageBoxResult messageBoxResult = MessageBox.Show("Drop this project?", "Drop?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (messageBoxResult == MessageBoxResult.Cancel || messageBoxResult == MessageBoxResult.None)
                {
                    //Cannel
                    return;
                }
                else if (messageBoxResult == MessageBoxResult.No)
                {
                    if (openFileDialog.ShowDialog() == true)
                    {
                        string savePath = openFileDialog.FileName.Replace('\\', '/');

                        if (!Project_Save(savePath, true))
                        {
                            //Cannel
                            return;
                        }
                    }
                    else
                    {
                        //Cannel
                        return;
                    }
                }

                Projec_Load(loadPath);
            }
        }
        private void Projec_Load(string path)
        {
            //文件是否存在交给系统IO类检测

            pictureSetProjectFile.Load(path);
            PictureItemStructure[] loadData = PictureItemStructuresForLoad();

            this.PictureCanvas.Children.Clear();
            foreach(PictureItemStructure pictureItemStructure in loadData)
            {
                AddItem(pictureItemStructure);
            }
        }
        private PictureItemStructure[] PictureItemStructuresForLoad()
        {
            int i;
            string projectDirectory = pictureSetProjectFile.Directory;
            PictureItemStructure[] loadData = new PictureItemStructure[pictureSetProjectFile.PictureItems.Count];
            for (i = 0; i < pictureSetProjectFile.PictureItems.Count; i++)
            {
                loadData[i] = pictureSetProjectFile.PictureItems[i].ToAbsolutePath(projectDirectory);
            }
            return loadData;
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            SetOpenFileDialog_SingleProjectFile(
                "Select Where to Save Project Document",
                pictureSetProjectFile.Path == null ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : pictureSetProjectFile.Directory,
                true,
                false);

            if (openFileDialog.ShowDialog() == true)
            {
                string savePath = openFileDialog.FileName.Replace('\\', '/');

                Project_Save(savePath, true);
            }
        }
        private bool Project_Save(string path, bool checkFileExists = false)
        {
            if (checkFileExists && System.IO.File.Exists(path))
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("The file has been exist, do you want to overwrite it?", "Overwrite?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.No || messageBoxResult == MessageBoxResult.None)
                {
                    //Cannel
                    return false;
                }
            }

#warning 避免path为null
            if (this.pictureSetProjectFile.Path == null)
            {
                this.pictureSetProjectFile.Path = path;
            }
            pictureSetProjectFile.PictureItems = PictureItemStructuresForSave();
            pictureSetProjectFile.Save(path);
            return true;
        }
        private List<PictureItemStructure> PictureItemStructuresForSave()
        {
            string projectDirectory = pictureSetProjectFile.Directory; //如果c是目录，位置是a/b/c，返回的是a/b
            List<PictureItemStructure> saveData = new List<PictureItemStructure>();
            foreach (PictureItem pictureItem in this.pictureItems)
            {
                PictureItemStructure pictureItemStructure = pictureItem.Data.ToRelativePath(projectDirectory);
                saveData.Add(pictureItemStructure);
            }
            return saveData;
        }
    }
}
