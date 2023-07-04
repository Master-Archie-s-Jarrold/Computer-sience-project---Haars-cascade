using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Computer_sience_project___Haars_cascade
{
    public class Storeage
    {
        public List<string> FileNamesReadPos= new List<string> { };
        public List<string> FileNamesReadNeg= new List<string> { };
        public List<string> FileNamesWritePos= new List<string> { };
        public List<string> FileNamesWriteNeg= new List<string> { };

        public void AddToFileNamesRead()
        {
            int FcountPos = Directory.GetFiles("PositiveImages", "*", SearchOption.AllDirectories).Length;
            int FcountNeg = Directory.GetFiles("NegativeImages", "*", SearchOption.AllDirectories).Length;

            for (int i = 0; i < FcountPos; i++)
            {
              
                if (i < 10)
                {
                    FileNamesReadPos.Add("PositiveImages\\PositiveImage00" + i + ".png");
                }
                else if (i < 100)
                {
                    FileNamesReadPos.Add("PositiveImages\\PositiveImage0" + i + ".png");
                }
                else
                {
                    FileNamesReadPos.Add("PositiveImages\\PositiveImage" + i + ".png");
                }
            }

            for (int i = 0; i < FcountNeg; i++)
            {
                if (i < 10)
                {
                    FileNamesReadNeg.Add("NegativeImages\\NegativeImage00" + i + ".png");
                }else if (i < 100)
                {
                    FileNamesReadNeg.Add("NegativeImages\\NegativeImage0" + i + ".png");
                }
                else
                {
                    FileNamesReadNeg.Add("NegativeImages\\NegativeImage" + i + ".png");
                }
            }
        }

        public void AddToFileNamesWritePos(String FileName)
        {
            FileNamesWritePos.Add(FileName);
        }
        public void AddToFileNamesWriteNeg(String FileName)
        {
            FileNamesWriteNeg.Add(FileName);
        }

        public void WriteInfo(string[][,][] Data )
        {
            for (int k = 0; k < Data.Length; k++)
            {


                using (StreamWriter fs = new StreamWriter("ImageData//" + Data[k][0, 0][0]))
                {
                    for (int i = 0; i < Data[k].GetLength(0); i++)
                    {
                        for (int j = 0; j < Data[k].GetLength(1); j++)
                        {
                            if (j != 0) {
                                fs.Write("," + Data[k][i, j][0]);
                                fs.Write("," + Data[k][i, j][1]);
                                
                            } else
                            {
                                fs.Write(Data[k][i, j][0]);
                                fs.Write(Data[k][i, j][1]);
                         
                            }

                        }
                        fs.WriteLine();
                    }

                }
            }
        }


        public BitmapImage LoadImage(string FileName)
        {
            System.Drawing.Image img =System.Drawing.Image.FromFile(FileName);
            BitmapImage bit = new BitmapImage();

            using(MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                ms.Seek(0, SeekOrigin.Begin);

                bit.BeginInit();
                bit.CacheOption = BitmapCacheOption.OnLoad;
                bit.StreamSource = ms;
                bit.EndInit();
            }

            return bit;
         }
    }
}
