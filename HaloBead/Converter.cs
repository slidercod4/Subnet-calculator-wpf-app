using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaloBead
{
    public static class Converter
    {
        public static string CheckIfNetwork(string IP, string Mask)
        {
            string binaryIP = DecToBinary(IP).Replace(".", String.Empty);
            string binaryMask = DecToBinary(Mask).Replace(".", String.Empty);
            int i = binaryMask.Length - 1;
            while (binaryMask[i] == '0')
            {
                if (binaryIP[i] != '0')
                {
                    binaryIP = binaryIP.Remove(i, 1).Insert(i, "0");
                }
                i--;
            }
            i = 0;
            for (int j = 0; j < binaryIP.Length; j++)
            {
                if (j % 8 == 0 && j != 0)
                {
                    binaryIP = binaryIP.Insert(j + i, ".");
                    i++;
                }
            }
            binaryIP = binaryIP.Remove(binaryIP.Length - 1, 1);
            return binaryIP;
        }

        public static string PrefixToMask(int prefix)
        {
            string binaris = "";
            int db = 0;
            for (int i = 0; i < 32; i++)
            {
                if (i < prefix)
                {
                    binaris += "1";
                }
                else
                {
                    binaris += "0";
                }
                if (i % 8 == 0 && i != 0)
                {
                    binaris = binaris.Insert(i + db, ".");
                    db++;
                }
            }
            return BinaryToDec(binaris);
        }

        public static string DecToBinary(string IP)
        {
            string binaris = "";
            string[] oktetek = IP.Split('.');
            foreach (var oktet in oktetek)
            {
                binaris += Convert.ToString(int.Parse(oktet), 2).PadLeft(8, '0') + ".";

            }
            binaris = binaris.Remove(binaris.Length - 1);
            return binaris;
        }

        public static string BinaryToDec(string binaris)
        {
            string[] darabok = binaris.Split('.');
            string eredmeny = "";
            int db = 0;
            for (int i = 0; i < darabok.Length; i++)
            {
                double tizesSzam = 0;
                db = 0;
                for (int j = darabok[i].Length - 1; j >= 0; j--)
                {
                    if (darabok[i][j] == '1')
                        tizesSzam += Math.Pow(2, db);
                    db++;
                }
                eredmeny += tizesSzam + ".";
            }
            eredmeny = eredmeny.Remove(eredmeny.Length - 1);
            return eredmeny;
        }

        public static bool CheckIfIP(string ip)
        {
            string[] oktettek = ip.Split('.');
            if (oktettek.Length != 4) return false;
            for (int i = 0; i < oktettek.Length; i++)
            {
                if (!int.TryParse(oktettek[i], out int szam) || (szam < 0 || szam > 255))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
