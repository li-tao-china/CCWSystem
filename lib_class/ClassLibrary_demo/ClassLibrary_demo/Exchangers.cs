using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolSystemDemo
{
    public class Exchangers
    {
        //存放字段
        //热流入口水温
        private double hotInTemperature;
        public double HotInTemperature
        {
            get
            {
                return hotInTemperature;
            }

            set
            {
                hotInTemperature = value;
            }
        }
        //热流出口水温
        private double hotOutTemperature;
        public double HotOutTemperature
        {
            get
            {
                return hotOutTemperature;
            }

            set
            {
                hotOutTemperature = value;
            }
        }
        //冷流入口水温
        private double coolInTemperature;
        public double CoolInTemperature
        {
            get
            {
                return coolInTemperature;
            }

            set
            {
                coolInTemperature = value;
            }
        }
        //冷流出口水温
        private double coolOutTemperature;
        public double CoolOutTemperature
        {
            get
            {
                return coolOutTemperature;
            }

            set
            {
                coolOutTemperature = value;
            }
        }

        public double hydraulicLoss;
        public double hotFlow;
        public double coolFlow;
        public int numOfLiuDao;
        public int numOfLiuCheng;
        public double crossSectionalAreaOfLiuDao;
        public double thicknessOfPlate;
        public double heatTransferAreaOfPlate;
        public double thermalConductivityOfPlate;
        public double aOfNu;
        public double bOfNu;
        public double foulingResistance;

        //方法
        /*实现的功能：
         * 求稳定时冷却水的出口水温
         * 求水力损失
         */
 
        //体积流量-->质量流量
        public double m3_hTokg_h(double Flow)
        {
            return Flow * 1000;
        }

        public double maxTemperature(double t1,double t2)
        {
            if (t1 > t2)
                return t1;
            return t2;
        }
    }
}
