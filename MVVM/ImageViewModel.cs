using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace MVVM
{
    public class ImageViewModel : INotifyPropertyChanged
    {
        public ImageViewModel()
        {
            Images = new ObservableCollection<MyImage>();
            AddCommand = new RelayCommand(AddImage);
            DeleteCommand = new RelayCommand(DeleteImage);

            string imagesDirectory = @"..\..\Images";
            if (System.IO.Directory.Exists(imagesDirectory))
            {
                foreach (string filename in System.IO.Directory.GetFiles(imagesDirectory, "*.*", System.IO.SearchOption.AllDirectories))
                {
                    string absolutePath = Path.GetFullPath(filename);
                    MyImage image = new MyImage()
                    {
                        
                        Source = new BitmapImage(new Uri(absolutePath)),
                        FilePath = filename,
                        FileName = System.IO.Path.GetFileName(filename),
                        Size = new System.IO.FileInfo(filename).Length / 1024
                    };

                    Images.Add(image);
                }
            }
        }

        private ObservableCollection<MyImage> _images;
        private MyImage _selectedImage;

        public ObservableCollection<MyImage> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                OnPropertyChanged("Images");
            }
        }

        public MyImage SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                _selectedImage = value;
                OnPropertyChanged("SelectedImage");
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private void AddImage(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Images");
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    MyImage image = new MyImage()
                    {
                        Source = new BitmapImage(new Uri(filename)),
                        FilePath = filename,
                        FileName = System.IO.Path.GetFileName(filename),
                        Size = new System.IO.FileInfo(filename).Length / 1024
                    };

                    Images.Add(image);
                }
            }
        }

        private void DeleteImage(object obj)
        {
            if (SelectedImage != null)
            {
                Images.Remove(SelectedImage);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
