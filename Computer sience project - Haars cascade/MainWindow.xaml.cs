using Haars_casde;
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

namespace Computer_sience_project___Haars_cascade
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Storeage _Storeage = new Storeage();
            image_scaler _Scaler = new image_scaler(_Storeage);
            HaarLikeFeatures _Gradient = new HaarLikeFeatures(_Storeage);
            

            Image image_1 = new Image();
            int FilesLength = 0;

            _Storeage.AddToFileNamesRead();

            foreach (string File in _Storeage.FileNamesReadPos)
            {
                _Scaler.imageScaler(File, 200,"Pos");
            }

            foreach (string File in _Storeage.FileNamesReadNeg)
            {
                _Scaler.imageScaler(File, 200,"Neg");
            }




            _Gradient.CulmativeTheImage("pos");
            _Gradient.CulmativeTheImage("neg");


            image_1 = new Image();           
            Canvas.SetTop(image_1, 0);
            Canvas.SetLeft(image_1, 0);
            can.Children.Add(image_1);
        }
    }
}
