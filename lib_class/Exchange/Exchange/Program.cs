using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Exchange
{
    class Program
    {
        static void Main(string[] args)
        {
            //Exchangers exchange1 = new Exchangers();
            //exchange1.HotInTemperature = 53;
            //exchange1.CoolFlow = 40;

            //System.Console.WriteLine(exchange1.ExitTempurature());

            
            string connetStr = "server=127.0.0.1;port=3306;user=root;password=1234;database=demo;";
            MySqlConnection conn = new MySqlConnection(connetStr);
            conn.Open();
            System.Console.WriteLine("connected");

            string sql = "select midu from water where wundu=20";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象
            reader.Read();
            Console.WriteLine(reader.GetDouble("midu"));
            

        }
    }
    /*
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
        //水力损失
        private double hydraulicLoss;
        public double HydraulicLoss
        {
            set { hydraulicLoss = value; }
            get { return hydraulicLoss; }
        }
        //热流
        private double hotFlow;
        public double HotFlow
        {
            set { hotFlow = value; }
            get { return hotFlow; }
        }
        //冷流
        private double coolFlow;
        public double CoolFlow
        {
            set { coolFlow = value; }
            get { return coolFlow; }
        }
        //当量直径
        private double de;
        public double De
        {
            set { de = value; }
            get { return de; }
        }
        //流道数
        private int numOfLiuDao;
        public int NumOfLiuDao
        {
            set { numOfLiuDao = value; }
            get { return numOfLiuDao; }
        }
        //流程数
        private int numOfLiuCheng;
        public int NumOfLiuCheng
        {
            set { numOfLiuCheng = value; }
            get { return numOfLiuCheng; }
        }
        //流道截面积
        private double crossSectionalAreaOfLiuDao;
        public double CrossSectionalAreaOfLiuDao
        {
            get { return crossSectionalAreaOfLiuDao; }
            set { crossSectionalAreaOfLiuDao = value; }
        }
        //单板厚度
        private double thicknessOfPlate;
        public double ThicknessOfPlate
        {
            set { thicknessOfPlate = value; }
            get { return thicknessOfPlate; }
        }
        //单板换热面积
        private double heatTransferAreaOfSinglePlate;
        public double HeatTransferAreaOfSinglePlate
        {
            set { heatTransferAreaOfSinglePlate = value; }
            get { return heatTransferAreaOfSinglePlate; }
        }
        //板片导热率
        private double thermalConductivityOfPlate;
        public double ThermalConductivityOfPlate
        {
            set { thermalConductivityOfPlate = value; }
            get { return thermalConductivityOfPlate; }
        }
        //Nu中的参数a
        private double aOfNu;
        public double AOfNu
        {
            set { aOfNu = value; }
            get { return aOfNu; }
        }
        //Nu中的参数b
        private double bOfNu;
        public double BOfNu
        {
            set { bOfNu = value; }
            get { return bOfNu; }
        }
        //污垢热阻
        private double foulingResistance;
        public double FoulingResistace
        {
            set { foulingResistance = value; }
            get { return foulingResistance; }
        }

        //方法
        //实现的功能：
         // 求稳定时冷却水的出口水温
         //求水力损失
         

        //体积流量-->质量流量
        public double m3_hTokg_h(double Flow)
        {
            return Flow * 1000;
        }
        //换热器总的换热面积
        public double A()
        {
            return (2 * numOfLiuCheng * numOfLiuDao - 1) * heatTransferAreaOfSinglePlate;
        }

        public double ExitTempurature()
        {
            double qmOfcool = m3_hTokg_h(CoolFlow);
            double qmOfhot = m3_hTokg_h(HotFlow);
            double areaA = A();
            //冷水的物性参数
            double rhoOfcool;
            double cpOfcool;
            double lamdaOfcool;
            double niOfcool;
            double prOfcool;
            //热流的物性参数
            double rhoOfhot;
            double cpOfhot;
            double lamdaOfhot;
            double niOfhot;
            double prOfhot;

            double temOfcool = CoolInTemperature;
            while (HotOutTemperature - temOfcool >= 0)//double 类型的数值比较不能直接比
            {
                int avetemOfcool = (int)Math.Round((temOfcool + CoolInTemperature) / 2);
                double temOfhot = HotInTemperature - (qmOfcool * cpOfcool * (temOfcool - CoolInTemperature)) / (qmOfhot * 4.178);
                int avetemOfhot = (int)Math.Round((temOfhot + HotInTemperature) / 2);
                //计算流速
                double speedOfhot = (qmOfhot / rhoOfhot) / (3600 * NumOfLiuDao * NumOfLiuCheng);
                double speedOfcool = (qmOfcool / rhoOfcool) / (3600 * NumOfLiuDao * NumOfLiuCheng);
                //计算雷诺数
                double ReOfhot = (speedOfhot * De) / niOfhot;
                double ReOfcool = (speedOfcool * De) / niOfcool;
                //计算努塞尔数
                double NuOfhot = AOfNu * Math.Pow(ReOfhot, BOfNu) * Math.Pow(prOfhot, 0.3);
                double NuOfcool = AOfNu * Math.Pow(ReOfcool, BOfNu) * Math.Pow(prOfcool, 0.4);
                //计算传热系数
                double a1Ofhot = NuOfhot * lamdaOfhot / De;
                double a2Ofcool = NuOfcool * lamdaOfcool / De;
                //计算总传热系数
                double K = 1 / (1 / a1Ofhot + ThicknessOfPlate / ThermalConductivityOfPlate + FoulingResistace + 1 / a2Ofcool);
                //进口处，出口处温差
                double inOftem = HotInTemperature - temOfcool;
                double outOftem = temOfhot - coolOutTemperature;
                //最大，最小温差
                double maxOftem = Math.Max(inOftem, outOftem);
                double minOftem = Math.Min(inOftem, outOftem);
                //平均温差
                double aveOftem = (maxOftem - minOftem) / (Math.Log(maxOftem / minOftem)) * 0.942;
                //冷热流的热负荷
                double qOfcool = (qmOfcool * cpOfcool * Math.Abs(temOfcool - CoolInTemperature) / 3.6);
                double qOfhot = K * areaA * aveOftem;
                //判断是否满足结束循环的条件
                if ((Math.Abs(qOfhot - qOfcool) / qOfhot <= 0.1) && (temOfhot < HotOutTemperature) && (temOfhot > CoolInTemperature)){

                    double EuOfcool = 1030 * Math.Pow(ReOfcool, -0.233);
                    //压降
                    HydraulicLoss = NumOfLiuCheng * EuOfcool * rhoOfcool * Math.Pow(speedOfcool, 2);
                    //水损
                    HydraulicLoss = HydraulicLoss / (rhoOfcool * 9.81);
                    break;
                }
                //冷却水温递增0.2
                temOfcool += 0.2;
            }

            return temOfcool;
        }

    }*/
}
