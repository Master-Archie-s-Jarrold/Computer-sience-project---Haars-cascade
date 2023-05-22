using Computer_sience_project___Haars_cascade;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Haars_casde
{
    public class image_scaler
    {
        int FileNumber;// the number the defines the files name
        Storeage _Storeage;
        public image_scaler(Storeage _Storeage)
        {
            this.FileNumber = 0;
            this._Storeage = _Storeage;// gets the global storage and sets it to an instancce of this storage
        }

        

        public void imageScaler(string file, int sizeOfImage , string FileType)
        {

            this.FileNumber++;// inceases the FileNumber for writing 

         
            System.Drawing.Image bit = System.Drawing.Image.FromFile(file);

            Bitmap bp = new Bitmap(bit, new System.Drawing.Size(sizeOfImage, sizeOfImage));// sacales the image to the correct size
   

            for (int i = 0; i < sizeOfImage; i++)// x axis of the image
            {
                for (int j = 0; j < sizeOfImage; j++)//y axis of the image
                {
                    System.Drawing.Color c = bp.GetPixel(i, j);//gets the colour of the pixel 
                    int avg = (c.R + c.B + c.G) / 3;//acgs the colous 
                    c = System.Drawing.Color.FromArgb(c.A, avg, avg, avg);
                    bp.SetPixel(i, j, c);//sets the pixel to the new colour 
                }
            }


            
           save(bp , FileType);// Runs the save Subrouten 

        }

        void save(Bitmap bp,string FileType)
        {

            List<string> Files;



            string FileName = "";
            if(FileType == "Pos")//adds the pos to the file directory 
            {

                FileName = this.FileNumber + "-WrittenPos.png";
                FileName = "WritePos\\" + FileName;

            }else if (FileType == "Neg")//adds the neg to the file directory 
            {
                FileName = this.FileNumber + "-WrittenNeg.png";
                FileName = "WriteNeg\\" + FileName;
            }

            byte[] FileBytes;
            
            if (FileType == "Pos")
            {
                this._Storeage.FileNamesWritePos.Add(FileName);// adds the File name to a list for pos 
            }
            else
            {
                this._Storeage.FileNamesWriteNeg.Add(FileName);// adds the File names to the list for Neg
            }

            using (MemoryStream ms = new MemoryStream())//opens a memory stream
            {

                using(FileStream fs = new FileStream(FileName,FileMode.OpenOrCreate,FileAccess.ReadWrite)) // opens a file string read to create or if created then open 
                {
                    
                    bp.Save(ms, ImageFormat.Png);// saves the bitmap in memery stream as a bitmap
                    FileBytes = ms.ToArray();// write the image as a serise of bytes
                    fs.Write(FileBytes, 0, FileBytes.Length);// writes all the bits to a file and then saves as a png
                    fs.Close();
                }
                ms.Close();
                    
                
            }

        
            
        } 
       
    }
}
