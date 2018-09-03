using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolingTower
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }
    class coolingTower
    {   //空气密度
        private double airDensity = 0;
        public double AirDensity { set; get; }
        //空气流量
        private double flowOfAir = 0;
        public double FlowOfAir { set; get; }
        //空气质量流量
        private double massFlowOfAir = 0;
        public double MassFlowOfAir { set { massFlowOfAir = FlowOfAir / 3600 * 0.998; } get { return massFlowOfAir; } }
        //水流量
        private double flowOfWater = 0;
        public double FlowOfWater { set; get; }
        //水的质量流量
        private double massFlowOfWater = 0;
        public double MassFlowOfWater { set { massFlowOfWater = FlowOfWater / 3.6; } get { return massFlowOfWater; } }
        //气水比
        private double qiShuiBi = 0;
        public double QiShuiBi { set { qiShuiBi = FlowOfAir / FlowOfWater; } get { return qiShuiBi; } }
        //入口水温
        private double waterTemperatureOfIn = 0;
        public double WaterTemperatureOfIn { set; get; }
        //出口水温
        private double waterTemperatureOfOut = 0;
        public double WaterTemperatureOfOut { set; get; }
        //出口水温约束
        private double constraintOfOutletTemperature;
        public double ConstraintOfOutletTemperature { set; get; }
        //水的比热
        private double coolWaterBiRe = 0;
        public double CoolWaterBiRe{ set { coolWaterBiRe = value; }get{ return coolWaterBiRe; } }
        //干空气的比热
        private double dryAirBiRe = 0;
        public double DryAirBiRe{ set { dryAirBiRe = value; } get { return dryAirBiRe; } }
        //当地干球温度
        private double localDryAirTemperature = 0;
        public double LocalDryAirTemperature { set; get; }
        //湿空气比热
        private double wetAirBiRe = 0;
        public double WetAirBiRe { set; get; }
        //当地湿球温度
        private double localWetAirTemperature = 0;
        public double LocalWetAirTemperature { set; get; }
        //大气压
        private double atmospherePressure = 0;
        public double AtmospherePressure { set; get; }
        //0摄氏度时汽化热
        private double qiHuaRefor0 = 0;
        public double QiHuaRefor0 { set; get; }
        //蒸汽汽化热
        private double vapourBiRe = 0;
        public double VapourBiRe { set; get; }
        //出口水温的汽化热
        private double qiHuaRefort2 = 0;
        public double QiHuaRefort2 { set{qiHuaRefort2 = -2.3193 * WaterTemperatureOfOut + 2500.8;}get { return qiHuaRefort2; }}

        //冷却塔结构参数
        //填料宽度
        private double width = 0;
        public double Width { set; get; }
        //塔长 在（12-15m）之间
        private double length = 0;
        public double Length { set; get; }
        //填料截面积
        private double area = 0;
        public double Area { set { area = Width * Length; } get { return area; } }
        //填料高度  一般在（1-2m）之间
        private double heightOfFilling = 0;
        public double HeightOfFilling { set; get; }
        //填料体积
        private double volume = 0;
        public double Volume { set { volume = Area * HeightOfFilling; } get { return volume; } }
        //淋水密度
        private double densityOfSprinkling = 0;
        public double DensityOfSprinkling { set { densityOfSprinkling = FlowOfWater / Area; } get { return densityOfSprinkling; } }
        //风机效率
        private double nf = 0;
        public double Nf { set; get; }
        //风机功率
        private double n = 0;
        public double N { set; get; }
        //求特性数
        private double performanceNumber = 0;
        public double PerformanceNumber{set { performanceNumber = 1.2476 * Math.Pow(QiShuiBi, 0.5585); } get { return performanceNumber; } }
        //计算冷却数
        private double coolingNumber = 0;
        public double CoolingNumber { set; get; }
        
        public double eOfP(double T)
        {
            return 0.0141966 - 3142.305 * (1 / T - 1 / 373.16)+ 8.2 * Math.Log10(373.16 / T) - 0.0024804 * (373.16 - T);
        }
        //求解平衡状态
        public bool balanceOfCoolingTower()
        {
            double tg = LocalDryAirTemperature;
            double ts = LocalWetAirTemperature;
            double tgk = 273.16 + tg;
            double tsk = 273.16 + ts;
            //干湿球温度对应饱和蒸汽分压力
            double Pg = 98.0665 * Math.Pow(10, eOfP(tgk));
            double Ps = 98.0665 * Math.Pow(10, eOfP(tsk));
            //进塔空气相对湿度
            double wa = (Ps - 0.000662 * AtmospherePressure * (tg - ts)) / Pg;
            double t1k = WaterTemperatureOfIn;
            //
            bool flag = false;
            //求出口温度
            for(WaterTemperatureOfOut = LocalWetAirTemperature; WaterTemperatureOfOut <= ConstraintOfOutletTemperature; WaterTemperatureOfIn += 0.2)
            {
                double t2k = WaterTemperatureOfOut + 273.16;
                double dt = WaterTemperatureOfOut - WaterTemperatureOfIn;
                double tm = (WaterTemperatureOfOut + WaterTemperatureOfIn) / 2;
                double tmk = tm + 273.16;
                double P1 = 98.0665 * Math.Pow(10, eOfP(t1k));
                double P2 = 98.0665 * Math.Pow(10, eOfP(t2k));
                double Pm = 98.0665 * Math.Pow(10, eOfP(tmk));
                double K = 1 - CoolWaterBiRe * WaterTemperatureOfOut / QiHuaRefort2;
                double i1 = DryAirBiRe * LocalDryAirTemperature + 0.622 * (QiHuaRefor0 + VapourBiRe * LocalDryAirTemperature) * (wa * Pg / (AtmospherePressure - wa * Pg));
                double i2 = i1 + CoolWaterBiRe * dt / (K * QiShuiBi);
                double im = (i1 + i2) / 2;
                double i11 = LocalDryAirTemperature * WaterTemperatureOfIn + 0.622 * (QiHuaRefor0 + VapourBiRe * WaterTemperatureOfIn) * (P1 / (AtmospherePressure - P1));
                double i22 = LocalDryAirTemperature * WaterTemperatureOfOut + 0.622 * (QiHuaRefor0 + VapourBiRe * WaterTemperatureOfOut) * (P2 / (AtmospherePressure - P2));
                double imm = LocalDryAirTemperature * tm+ 0.622 * (QiHuaRefor0 + VapourBiRe * tm) * (Pm / (AtmospherePressure - Pm));
                CoolingNumber = CoolWaterBiRe * dt * (1 / (i22 - i1) + 4 / (imm - im) + 1 / (i11 - i2)) / 6;
                //判断是否满足Merkel等式
                if(Math.Abs(PerformanceNumber - CoolingNumber)/PerformanceNumber < 0.1)
                {
                    flag = true;
                    fan();
                    break;
                }
            }
            return flag;
        }
        //风机运行功率计算
        public double fan()
        {   //风量
            double flowOfair = FlowOfWater * QiShuiBi * 1000 / AirDensity;
            //淋水段风速
            double speedOfAir = flowOfair / (Area * 3600);
            //淋水段阻力
            double dP1 = 9.8 * Math.Pow(speedOfAir, 1.66) * Math.Pow(DensityOfSprinkling, 0.49);
            //其它部分阻力，一般为淋水段阻力的30%-40%
            double dP2 = 0.4 * dP1;
            //风机静压
            double dP = dP1 + dP2;
            //风机全压，风机静压一般为全压的60%-80%
            double dPall = dP / 0.6;
            //风机效率一般为0.8-0.85
            //风机功率
            N = dP * flowOfair * AirDensity / (Nf * 3600 * 1000);
            return N;
        } 
    }
}
