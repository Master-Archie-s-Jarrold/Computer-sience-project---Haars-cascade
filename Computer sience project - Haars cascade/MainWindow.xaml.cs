using Computer_sience_project___Haars_cascade.Haar_Features;
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
using System.Windows.Media.Animation;
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
            Storeage _Storeage = new Storeage();// creates a global instance of storage
            image_scaler _Scaler = new image_scaler(_Storeage);// creates a instance of Image_scaler
            HaarLikeFeatures _HaarLikeFeatures = new HaarLikeFeatures(_Storeage);//creates a instance of HaarLikeFeatures
            

            

            _Storeage.AddToFileNamesRead();// runs the File names in to a list in Storeage

            foreach (string File in _Storeage.FileNamesReadPos)// runs through all the files in the postive image in storage
            {
                _Scaler.imageScaler(File, 200,"Pos");
                
            }
            _Scaler.FileNumber = 0;
            foreach (string File in _Storeage.FileNamesReadNeg)//runs through all the negative files in storeage
            {
                _Scaler.imageScaler(File, 200,"Neg");
            }

            foreach (string File in _Storeage.FileNamesWritePos)
            {
                _HaarLikeFeatures.HaarFeatures(File, "Pos");
            }



            

            drawOnImage draw = new drawOnImage();
            draw.image();

           
        }
    }
}
