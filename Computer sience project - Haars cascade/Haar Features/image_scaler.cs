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
        int FileNumber;
        Storeage _Storeage;
        public image_scaler(Storeage _Storeage)
        {
            this.FileNumber = 0;
            this._Storeage = _Storeage;
        }

        

        public void imageScaler(string file, int sizeOfImage , string FileType)
        {

            this.FileNumber++;

         
            System.Drawing.Image bit = System.Drawing.Image.FromFile(file);

            Bitmap bp = new Bitmap(bit, new System.Drawing.Size(sizeOfImage, sizeOfImage));
            Bitmap bpGrey;

            for (int i = 0; i < sizeOfImage; i++)
            {
                for (int j = 0; j < sizeOfImage; j++)
                {
                    System.Drawing.Color c = bp.GetPixel(i, j);
                    int avg = (c.R + c.B + c.G) / 3;
                    c = System.Drawing.Color.FromArgb(c.A, avg, avg, avg);
                    bp.SetPixel(i, j, c);
                }
            }


            
           save(bp , FileType);

        }

        void save(Bitmap bp,string FileType)
        {

            List<string> Files;



            string FileName = "";
            if(FileType == "Pos"){

                FileName = this.FileNumber + "-WrittenPos.png";
                FileName = "WritePos\\" + FileName;

            }else if (FileType == "Neg")
            {
                FileName = this.FileNumber + "-WrittenNeg.png";
                FileName = "WriteNeg\\" + FileName;
            }

            byte[] FileBytes;
            
            if (FileType == "Pos")
            {
                this._Storeage.FileNamesWritePos.Add(FileName);
            }
            else
            {
                this._Storeage.FileNamesWriteNeg.Add(FileName);
            }

            using (MemoryStream ms = new MemoryStream())
            {

                using(FileStream fs = new FileStream(FileName,FileMode.OpenOrCreate,FileAccess.ReadWrite))
                {
                    
                    bp.Save(ms, ImageFormat.Png);
                    FileBytes = ms.ToArray();
                    fs.Write(FileBytes, 0, FileBytes.Length);
                    fs.Close();
                }
                    
                
            }

        
            
        } 
       
    }
}
