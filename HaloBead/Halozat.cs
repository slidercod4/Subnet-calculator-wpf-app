using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaloBead
{
    public class Halozat
    {
        public string Nev { get; private set; }
        public string Cim { get; private set; }
        public string Maszk { get; private set; }
        public string SzorasCim { get; private set; }
        public int HostDB { get; private set; }
        public int HaloMeret { get; private set; }

        public Halozat(string nev, int hostDb)
        {
            Nev = nev;
            Cim = "";
            SzorasCim = "";
            HostDB = hostDb;
            Maszk = CalcMask();
        }

        public void Calc(string haloIP, bool isFirst)
        {
            haloIP = isFirst ? haloIP : IPAddOne(haloIP);
            Maszk = CalcMask();
            Cim = isFirst ? Converter.BinaryToDec(Converter.CheckIfNetwork(haloIP, Maszk)) : haloIP;
            SzorasCim = IPCalc(Cim.Split('.'));
        }

        private string IPCalc(string[] korrigaltIP)
        {
            string[] wildCard = wildCardCalc().Split('.');
            string broadcast = "";
            for (int i = 0; i < korrigaltIP.Length; i++)
            {
                broadcast += (int.Parse(korrigaltIP[i]) + int.Parse(wildCard[i])) + ".";
            }
            return broadcast.Remove(broadcast.Length - 1);

        }

        private string wildCardCalc()
        {
            string wildCard = "";
            string[] maszkOktettek = Maszk.Split('.');
            for (int i = maszkOktettek.Length - 1; i >= 0; i--)
            {
                wildCard = wildCard.Insert(0, (255 - int.Parse(maszkOktettek[i])).ToString() + ".");
            }
            return wildCard.Remove(wildCard.Length - 1);
        }

        private string IPAddOne(string haloIP)
        {
            string[] IP = haloIP.Split('.');
            string eredmeny = "";
            bool VoltNovel = false;
            for (int i = IP.Length - 1; i >= 0; i--)
            {
                int oktettErtek = int.Parse(IP[i]);
                if (oktettErtek < 255)
                {
                    if (!VoltNovel)
                    {
                        oktettErtek++;
                        VoltNovel = true;
                    }
                }
                else
                {
                    oktettErtek = 0;
                }
                eredmeny = eredmeny.Insert(0, oktettErtek.ToString() + ".");
            }
            return eredmeny.Remove(eredmeny.Length - 1);
        }


        private string CalcMask()
        {
            int halMeret = 0;
            int hatvany = 0;
            for (halMeret = 1; halMeret < HostDB + 2; halMeret *= 2)
            {
                hatvany++;
            }
            HaloMeret = halMeret;
            return Converter.PrefixToMask(32 - hatvany);

        }


    }
}
