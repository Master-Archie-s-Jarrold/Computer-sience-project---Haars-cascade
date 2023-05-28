using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Computer_sience_project___Haars_cascade.Haar_Features
{
    internal class drawOnImage
    {
        

       public void image()
        {
            Image bt = Image.FromFile("WritePos//1-WrittenPos.png");
            int[,] image = new int[19, 19];
            string Files;
            using (StreamReader sr = new StreamReader("ImageData//File;1;Pos.txt"))
            {
                for (int i = 0; i < 19; i++)
                {
                    Files = sr.ReadLine();
                    string[] strings = Files.Split(',');
                    for (int x = 0; x < 20; x++)
                    {
                        try
                        {
                            image[i, x-1] = Convert.ToInt32(strings[x]);

                        } 
                        catch(FormatException e)
                        {
                            
                        }
                    }
                    
                }
                
            }

            Graphics G = Graphics.FromImage(bt);
            for (int y = 0; y < 190; y+=10)
            {
                for (int x = 0; x < 190; x+=10)
                {
                    if (Math.Abs(image[y/10,x/10]) <400 && Math.Abs(image[y / 10, x / 10]) != 0 )
                    {

                        Point p1 = new Point( x, y+5);
                        Point p2 = new Point( x+10,y+5 );
                        G.DrawLine(Pens.MediumSlateBlue, p1, p2);
                    }
                }
            }
            G.Save();
            bt.Save("image.png");
        }
    }
} 
