using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolSystemDemo;
using System.Runtime.InteropServices;

namespace Consoletestlib
{
    class Program
    {   [DllImport("dlltest1.dll")]
         public extern static void Hi();

        static void Main(string[] args)
        {
            Hi();
            /*
            Exchangers exchangers = new Exchangers();
            exchangers.CoolInTemperature = 30;
            exchangers.HotOutTemperature = 35;
            double ret = exchangers.maxTemperature( exchangers.HotOutTemperature,exchangers.CoolInTemperature);
            System.Console.WriteLine(ret);
            exchangers.coolFlow = 50;
            System.Console.WriteLine(exchangers.coolFlow);
            */

        }
    }
}
