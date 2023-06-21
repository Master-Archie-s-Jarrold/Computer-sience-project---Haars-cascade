using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows;
using Microsoft.Win32;
using System.Reflection; 
using System.ComponentModel;
using Haars_casde;
using System.Windows.Navigation;
using System.Windows.Media.Media3D;
using System.Drawing.Imaging;
using System.Windows.Documents;

namespace Computer_sience_project___Haars_cascade
{
    public class HaarLikeFeatures
    {
        Storeage _Storeage;
        double[][] ImageMatrix;
        int[][] imageDataMatrix;
        string[][,][] data;
        int FileNumber;
        public HaarLikeFeatures(Storeage _Storeage)
        {
            this._Storeage = _Storeage;
        }
        private BitmapImage LoadImage(string FileName)
        {

            BitmapImage image = new BitmapImage();

            Stream fs = File.OpenRead(FileName);
            try
            {
                image.BeginInit();
                image.StreamSource = fs;
                image.EndInit();
            }catch(Exception e)
            {
               
            }







            return image;

        }
        int[][] convertArrayToMatrix(byte[] array)
        {

            int[][] matrix = new int[200][];
            int item = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new int[200 * 4];
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    matrix[i][j] = array[item];
                    item++;
                }
            }
            return matrix;
        }
        public void CulmativeTheImage(string type)
        {
            List<string> Lists;

            if (type == "pos")
            {
                Lists = _Storeage.FileNamesWritePos;
            }else
            {
                Lists = _Storeage.FileNamesWriteNeg;
            }
            FileNumber = 0;
            data = new string[Lists.Count][,][];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new string[20, 20][];
                
            }
            for (int i = 0; i < data.Length; i++)
            {
                for (int x = 0; x < 20; x++)
                {
                    for (int y = 0; y < 20; y++)
                    {
                        data[i][x, y] = new string[4];
                    }
                }
            }
            
            foreach (string item in Lists)
            {
                BitmapImage currentImage = new BitmapImage();

                currentImage = LoadImage(item);
                int stride = (int)(4 * ((currentImage.PixelWidth * 4 + 3) / 4));
                byte[] ImageData = new byte[stride * currentImage.PixelHeight];
                currentImage.CopyPixels(ImageData, stride, 0);

                imageDataMatrix = convertArrayToMatrix((byte[])ImageData);
                ImageMatrix = new double[(int)currentImage.PixelHeight][];






                data[FileNumber][0, 0][0] = "File;"+FileNumber+";"+type+".txt";
               

                for (int x = 0; x < currentImage.Width; x+=20)

                {
                    for (int y = 0; y < currentImage.Height; y+= 20)
                    {
                        try
                        {

                            data[FileNumber][x / 10, (y / 10) + 1][0] = Convert.ToString( edgeh(20, 10, x, y,FileNumber));
                            data[FileNumber][x / 10, (y / 10) + 1][1] = Convert.ToString( edgev(10, 20, x, y,FileNumber));
                            data[FileNumber][x / 10, (y / 10) + 1][2] = Convert.ToString( edgev(10, 20, x, y,FileNumber));
                            data[FileNumber][x / 10, (y / 10) + 1][3] = Convert.ToString( edgev(10, 20, x, y,FileNumber));
                        }
                        catch (Exception e)
                        {
                            
                        }
                    }
                   
                }
                
             


                FileNumber++;
            }
            _Storeage.WriteInfo(data);

        }

        private int FeatureValue(int rectx, int recty, int recth, int rectw)
        {


            /*for (int x = rectx; x < rectw + rectx; x++)
            {
                ImageMatrix[x] = new double[ImageMatrix.Length];
                for (int y = recty; y < recth + recty; y++)
                {
                    if (y == recty && x == rectx)
                    {
                        ImageMatrix[x][y] = imageDataMatrix[x][y * 4];
                    }
                    else if ((x <= rectx + rectw && y == recty) && (x != rectx ))
                    {
                        ImageMatrix[x][y] = imageDataMatrix[x][y * 4] + ImageMatrix[x - 1][y];
                    }
                    else if ((y <= recty + recth && x == rectx) && ( y != recty))
                    {
                        ImageMatrix[x][y] = imageDataMatrix[x][y * 4] + ImageMatrix[x][y - 1];
                    }
                    else
                    {
                        ImageMatrix[x][y] = imageDataMatrix[x][y * 4] + ImageMatrix[x -1][y - 1] + (- ImageMatrix[x][y - 1] - ImageMatrix[x - 1][y]);
                    }
                }
            }*/
            int featureVal = 0;
            for (int x = rectx; x < rectw + rectx; x++)
            {
                ImageMatrix[x] = new double[ImageMatrix.Length];
                for (int y = recty; y < recth + recty; y++)
                {
                    featureVal += imageDataMatrix[x][y *4];
                }
            }
                    

            

            return featureVal;
        }
           
        int edgeh(int totalWidth, int height, int startingx, int startingy,int FileNumber)
        {
            int rectx = startingx, recty = startingy, recth = height, rectw = totalWidth / 2;
            int rectx2 = totalWidth / 2 + startingx+1, recty2 = startingy, recth2 = height, rectw2 = totalWidth / 2;

            int rect1sum = FeatureValue(rectx, recty, recth, rectw);
            int rect2sum = FeatureValue(rectx2, recty2, recth2, rectw2);

            int feature = rect1sum - rect2sum;
            
            return feature; 
        }
        int edgev(int totalWidth, int height, int startingx, int startingy, int FileNumber)
        {
            int rectx = startingx, recty = startingy, recth = height, rectw = totalWidth / 2;
            int rectx2 = totalWidth / 2 + startingx, recty2 = startingy, recth2 = height, rectw2 = totalWidth / 2;

            int rect1sum = FeatureValue(rectx, recty, recth, rectw);
            int rect2sum = FeatureValue(rectx2, recty2, recth2, rectw2);

            int feature = rect1sum - rect2sum;

            return feature;
        }

        void line(int totalWidth, int height, int startingx, int startingy,int FileNumber)
        {
            int rectx1w = startingx, recty1w = startingy, recth1w = height, rectw1w = totalWidth / 3;
            int rectx2b = startingx + totalWidth / 3, recty2b = startingy, recth2b = height, rectw2b = totalWidth / 3;
            int rectx3w = startingx + 2 * (totalWidth / 3), recty3w = startingy, recth3w = height, rectw3w = totalWidth / 3;

            int rect1sum = FeatureValue(rectx1w, recty1w, recth1w, rectw1w);
            int rect2sum = FeatureValue(rectx2b, recty2b, recth2b, rectw2b);
            int rect3sum = FeatureValue(rectx3w, recty3w, recth3w, rectw3w);

            int feature = rect1sum - rect2sum + rect3sum - rect2sum;
            

        }
        void corrner(int totalWidth, int height, int startingx, int startingy, int FileNumber)
        {
            int rectx1w = startingx, recty1w = startingy, recth1w = height / 2, rectw1w = totalWidth / 2;
            int rectx2b = startingx + totalWidth / 2, recty2b = startingy, recth2b = height / 2, rectw2b = totalWidth / 2;
            int rectx3w = startingx + totalWidth / 2, recty3w = startingy + height / 2, recth3w = height / 2, rectw3w = totalWidth / 2;
            int rectx4b = startingx, recty4b = startingy + height / 2, recth4b = height / 2, rectw4b = totalWidth / 2;


            int rect1sum = FeatureValue(rectx1w, recty1w, recth1w, rectw1w);
            int rect2sum = FeatureValue(rectx2b, recty2b, recth2b, rectw2b);
            int rect3sum = FeatureValue(rectx3w, recty3w, recth3w, rectw3w);
            int rect4sum = FeatureValue(rectx4b, recty4b, recth4b, rectw4b);

            int feature = (rect3sum + rect1sum) - (rect2sum + rect4sum);
           

        }
    }
}