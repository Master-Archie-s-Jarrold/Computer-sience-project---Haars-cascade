﻿using System;
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

using System.Windows.Documents;
using System.Transactions;

namespace Computer_sience_project___Haars_cascade
{
    public class HaarLikeFeatures
    {
        Storeage _Storeage;
        int[][,][] DataForImages;

        public HaarLikeFeatures(Storeage storeage)
        {
            this._Storeage = storeage;
        }

        public void HaarFeatures(string name, string FileType)
        {


            BitmapImage image = new BitmapImage();
            image= _Storeage.LoadImage(name);   
            

                      




        }

        public int[][] convert(int[] data , BitmapImage image)
        {

            int heigth = image.PixelHeight;
            int width = image.PixelWidth;

            for (int x = 0; x < width; x++)
            {

                for (int y = 0; y < heigth; y++)
                {

                }

            }





            return null;
        }
        
        
    }
}