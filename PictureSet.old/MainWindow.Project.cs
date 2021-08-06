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

        private void button_Load_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog_SingleJson();

            if (openFileDialog.ShowDialog() == true)
            {
                string loadPath = openFileDialog.FileName.Replace('\\', '/');

                MessageBoxResult messageBoxResult = MessageBox.Show("Drop this project?", "Drop?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (messageBoxResult == MessageBoxResult.Cancel || messageBoxResult == MessageBoxResult.None)
                {
                    //Cannel
                    return;
                }
                else if(messageBoxResult == MessageBoxResult.No)
                {
                    if (openFileDialog.ShowDialog() == true)
                    {
                        string savePath = openFileDialog.FileName.Replace('\\', '/');

                        if(!Project_Save(savePath, true))
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

                //Load
                pictureSetProjectFile.Load(loadPath);

                this.pictureCanvas.Children.Clear();
#warning HERE
                //this.pictureCanvas.Children.Add(pictureSetProjectFile.PictureItems);
            }
        }

        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog_SingleJson();

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

            pictureSetProjectFile.Save(path);
            return true;
        }

        private void openFileDialog_SingleJson()
        {
            openFileDialog.AddExtension = false;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.DereferenceLinks = true;
            openFileDialog.Filter = "Json Document|*.json|All Files|*.*"; //"Json Documents|*.json|All Files|*.*"
            openFileDialog.FilterIndex = 1;
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Select project document";
            openFileDialog.ValidateNames = true;
        }
    }
}
