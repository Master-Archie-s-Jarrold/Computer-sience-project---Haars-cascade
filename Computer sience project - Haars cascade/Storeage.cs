using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;


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

        public void WriteInfo(string[,] Data )
    {
          
            using (StreamWriter fs = new StreamWriter(Data[0,0]))
            {
                for (int i = 0; i < Data.GetLength(0); i++)
                {
                fs.WriteLine(Data[i,0] +" "+ Data[i, 1] + " " + Data[i, 2] + " " + Data[i, 3]);
                }
                
            }
        }
    }
}
