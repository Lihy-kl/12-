using Lib_File;
using SmartDyeing.FADM_Control;
using SmartDyeing.FADM_Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SmartDyeing.FADM_Auto.Dye;

namespace SmartDyeing.FADM_Auto
{
    
    internal class AutoBrew
    {
        //自动开料机杯数据
        public struct s_CupRes
        {
            //工位状态 0=未上线（断电），1=待机，2=运行中,3=完成，4=异常
            public int _i_statues;
            //工位动作请求 0=无，1=等待数据、2=放瓶，3=加粉，4=开料完成取瓶，5=取瓶去进行洗瓶，6=保存数据请求
            public int _i_reques;
            //历史状态 0=没有母液瓶，1=先执行一次洗杯
            public int _i_history;
            //母液瓶瓶号
            public int _i_bottleNum;
            //实际浓度
            public double _d_realConcentration;
            //实际用量
            public double _d_currentWeight;
            //当前工序 0=无，1=放瓶，2=加粉，3=加热水，4=加冷水，5=加温水，6=搅拌，7=取瓶，8=校正
            public int _i_index;
            //当前步号
            public int _i_stepNum;
            //当前位置 0=无，1=搅拌位1，2=搅拌位2，3=搅拌位3，4=搅拌位4，5=搅拌位5，6=天平位
            public int _i_position;
            //允许拿瓶 0=无，1=允许取瓶，2=允许放瓶，3=允许加粉
            public int _i_allowGetBottle;

            //已标记的工位动作请求 0=无，1=等待数据、2=放瓶，3=加粉，4=开料完成取瓶，5=取瓶去进行洗瓶，6=保存数据请求
            public int _i_markreques;

        }

        //自动开料机杯数据
        public struct s_WashBottle
        {
            //工位状态 0=未上线（断电），1=待机，2=运行中,3=完成，4=异常
            public int _i_statues;
            //工位动作请求 0=无，1=准备、2=放瓶，3=取瓶
            public int _i_reques;
            //历史状态 0=没有母液瓶，1=先执行一次洗杯
            public int _i_history;
            //母液瓶瓶号
            public int _i_bottleNum;

            //已标记的工位动作请求 0=无，1=准备、2=放瓶，3=取瓶
            public int _i_markreques;
        }

        public static s_CupRes[] _cup_Temps = new s_CupRes[5];

        public static s_WashBottle[] _bottle_Wash = new s_WashBottle[1];

        public static void Brew()
        {
            try
            {
                //开料机通讯
                while (true)
                {
                    //同步数据
                    for (int i = 0; i < 5; i++)
                    {
                        _cup_Temps[i]._i_statues = Communal._ia_d22688[0 + 15 * i];
                        _cup_Temps[i]._i_reques = Communal._ia_d22688[1 + 15 * i];
                        _cup_Temps[i]._i_bottleNum = Communal._ia_d22688[5 + 15 * i];

                        //真实浓度
                        int i_h = 0;
                        int i_l = 0;
                        int i_value;
                        i_h = Communal._ia_d22688[6 + 15 * i];
                        i_l = Communal._ia_d22688[7 + 15 * i];
                        if (i_h < 0)
                        {
                            i_value = (((i_l + 1) * 65536 + i_h));
                        }
                        else
                        {
                            i_value = ((i_l * 65536 + i_h));
                        }
                        int i_realcon = i_value;

                        _cup_Temps[i]._d_realConcentration = i_realcon / 1000000.0;

                        //真实重量
                        i_h = Communal._ia_d22688[8 + 15 * i];
                        i_l = Communal._ia_d22688[9 + 15 * i];
                        if (i_h < 0)
                        {
                            i_value = (((i_l + 1) * 65536 + i_h));
                        }
                        else
                        {
                            i_value = ((i_l * 65536 + i_h));
                        }
                        int i_weight = i_value;
                        _cup_Temps[i]._d_currentWeight = i_weight / 1000.0;

                        _cup_Temps[i]._i_index = Communal._ia_d22688[10 + 15 * i];
                        _cup_Temps[i]._i_stepNum = Communal._ia_d22688[11 + 15 * i];
                        _cup_Temps[i]._i_position = Communal._ia_d22688[12 + 15 * i];
                        _cup_Temps[i]._i_allowGetBottle = Communal._ia_d22688[13 + 15 * i];

                    }

                    //根据信号进行数据处理
                    for (int i = 0; i < 5; i++)
                    {
                        if(_cup_Temps[i]._i_reques == 0)
                        {
                            _cup_Temps[i]._i_markreques = 0;
                        }
                        else if (_cup_Temps[i]._i_reques == 2|| _cup_Temps[i]._i_reques == 3|| _cup_Temps[i]._i_reques == 4|| _cup_Temps[i]._i_reques == 5)
                        {
                            //响应工位放瓶申请
                            FADM_Object.Communal._fadmSqlserver.ReviseData(
                                                        "UPDATE brew_station_details SET Cooperate = "+ _cup_Temps[i]._i_reques+",ReceptionTime='" + DateTime.Now 
                                                        + "' WHERE Station = " + (i+1) + " and Cooperate=0;");

                            //检查是否已把数据库更新
                            DataTable dt_cup = FADM_Object.Communal._fadmSqlserver.GetData("Select * from brew_station_details WHERE Station = " + (i+1)  +  " ;");
                            if (dt_cup.Rows.Count > 0)
                            {
                                if (Convert.ToInt32( dt_cup.Rows[0]["Cooperate"].ToString()) == _cup_Temps[i]._i_reques)
                                    _cup_Temps[i - 1]._i_markreques = _cup_Temps[i]._i_reques;
                            }
                        }

                    }
                    Thread.Sleep(1);

                }
            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    new SmartDyeing.FADM_Object.MyAlarm(ex.Message, "开料机通讯", false, 1);
                else
                    new SmartDyeing.FADM_Object.MyAlarm(ex.Message, "Cutting machine communication", false, 1);
            }
        }

        public static void WashBottle()
        {
            try
            {
                //开料机通讯
                while (true)
                {
                    _bottle_Wash[0]._i_statues = Communal._ia_d22938[0];
                    _bottle_Wash[0]._i_reques = Communal._ia_d22938[1];
                    _bottle_Wash[0]._i_history = Communal._ia_d22938[3];
                    _bottle_Wash[0]._i_bottleNum = Communal._ia_d22938[4];

                    if (_bottle_Wash[0]._i_reques == 0)
                    {
                        _bottle_Wash[0]._i_markreques = 0;
                    }
                    else if (_bottle_Wash[0]._i_reques == 2 || _bottle_Wash[0]._i_reques == 3 )
                    {
                        //响应工位放瓶申请
                        FADM_Object.Communal._fadmSqlserver.ReviseData(
                                                    "UPDATE brew_station_details SET Cooperate = " + _bottle_Wash[0]._i_reques + ",ReceptionTime='" + DateTime.Now
                                                    + "' WHERE Station = 6 and Cooperate=0;");

                        //检查是否已把数据库更新
                        DataTable dt_cup = FADM_Object.Communal._fadmSqlserver.GetData("Select * from brew_station_details WHERE Station = 6 ;");
                        if (dt_cup.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dt_cup.Rows[0]["Cooperate"].ToString()) == _bottle_Wash[0]._i_reques)
                                _bottle_Wash[0]._i_markreques = _bottle_Wash[0]._i_reques;
                        }
                    }

                    Thread.Sleep(1);

                }
            }
            catch (Exception ex)
            {
                if (Lib_Card.Configure.Parameter.Other_Language == 0)
                    new SmartDyeing.FADM_Object.MyAlarm(ex.Message, "开料机通讯", false, 1);
                else
                    new SmartDyeing.FADM_Object.MyAlarm(ex.Message, "Cutting machine communication", false, 1);
            }
        }

        /// <summary>
        /// 发送自动开料数据
        /// </summary>
        /// <returns></returns>
        public static void SendBrewData(int i_BottleNum,int i_PositionNum)
        {
            int[] ia_array = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //根据瓶号搜索对应资料
            string s_sql = "SELECT * FROM bottle_details WHERE BottleNum = " + i_BottleNum + ";";
            DataTable dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);

            if (dt_bottle_details.Rows.Count == 0)
            {
                //复位输入完成标记位
            }
            //调液流程代码
            string s_brewingCode = Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["BrewingCode"]]);

            int i_setcon = Convert.ToInt32(Convert.ToDouble(dt_bottle_details.Rows[0][dt_bottle_details.Columns["SettingConcentration"]]) * 1000000.00);

            string s_assistantCode = Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["AssistantCode"]]);

            /*
             * $M100 最大调液量
             */

            //允许最大调液量
            int i_maxweight = Convert.ToInt32(dt_bottle_details.Rows[0][dt_bottle_details.Columns["AllowMaxWeight"]]);

            /*
             * $M101 - $M102 原浓度
             */

            //开稀原瓶号
            int i_oribottle = Convert.ToInt32(dt_bottle_details.Rows[0][dt_bottle_details.Columns["OriginalBottleNum"]]);


            //根据原瓶号找到原浓度
            int i_oricon = 0;
            int i_oriWeight = 0;
            if (i_oribottle != 0)
            {
                s_sql = "SELECT RealConcentration,CurrentWeight FROM bottle_details WHERE BottleNum = " + i_oribottle + ";";
                dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
                if (dt_bottle_details.Rows.Count == 0)
                {
                    //FADM_Form.CustomMessageBox.Show("未找到" + i_oribottle + "号母液瓶资料", "母液泡制", MessageBoxButtons.OK, false);
                    //复位输入完成标记位


                    
                    return;
                }
                i_oricon = Convert.ToInt32(Convert.ToDouble(dt_bottle_details.Rows[0][dt_bottle_details.Columns["RealConcentration"]]) * 1000000.00);
                i_oriWeight = Convert.ToInt32(Convert.ToDouble(dt_bottle_details.Rows[0][dt_bottle_details.Columns["CurrentWeight"]]));

            }

            /*
             * $M103 - $M104 设定浓度
             */

            //设定浓度


            /*
             * $M105 染助剂单位
             */

            //根据染助剂代码找到单位
            s_sql = "SELECT UnitOfAccount, AssistantName FROM" +
                        " assistant_details WHERE AssistantCode = '" + s_assistantCode + "';";
            dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            if (dt_bottle_details.Rows.Count == 0)
            {
                
                return;
            }
            int i_unitOfAccount = 0;
            if (Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["UnitOfAccount"]]) == "%")
            {
                i_unitOfAccount = 0;
            }
            else if (Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["UnitOfAccount"]]) == "g/l")
            {
                i_unitOfAccount = 1;
            }
            else if (Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["UnitOfAccount"]]) == "G/L")
            {
                i_unitOfAccount = 2;
            }
            else
            {
                i_unitOfAccount = 3;
            }

            string s_assistantName = Convert.ToString(dt_bottle_details.Rows[0][dt_bottle_details.Columns["AssistantName"]]);

            /*
             * $M106 总步数
             * $M107 接收完成标志位
             * $M108 步骤1类型
             * $M109 步骤1比例
             * $M110 步骤2类型
             * $M111 步骤2比例
             * $M112 步骤3类型
             * $M113 步骤3比例
             * $M114 步骤4类型
             * $M115 步骤4比例
             * $M116 步骤5类型
             * $M117 步骤5比例
             */


            //根据调液流程代码找到调液流程
            s_sql = "SELECT * FROM brewing_process WHERE BrewingCode = '" + s_brewingCode + "' ORDER BY StepNum;";
            dt_bottle_details = FADM_Object.Communal._fadmSqlserver.GetData(s_sql);
            if (dt_bottle_details.Rows.Count == 0)
            {
                return;
            }

            int[] ia_no_1 = { 0, 0, 0, 0, 0, 0 };
            int[] ia_data_1 = { 0, 0, 0, 0, 0, 0 };
            int[] ia_ratio = { 0, 0, 0, 0, 0, 0 };
            for (int j = 0; j < dt_bottle_details.Rows.Count; j++)
            {
                string s_technologyName = Convert.ToString(dt_bottle_details.Rows[j][dt_bottle_details.Columns["TechnologyName"]]);
                int i_data = Convert.ToInt32(dt_bottle_details.Rows[j][dt_bottle_details.Columns["ProportionOrTime"]]);
                int i_ratio = 0;
                if (s_technologyName == "加温水" || s_technologyName == "Add warm water")
                {
                    if (dt_bottle_details.Rows[j][dt_bottle_details.Columns["Ratio"]] is DBNull)
                    {
                        i_ratio = 50;
                    }
                    else
                    {
                        i_ratio = Convert.ToInt32(dt_bottle_details.Rows[j][dt_bottle_details.Columns["Ratio"]]);
                    }
                }
                switch (s_technologyName)
                {

                    case "加大冷水":
                    case "Add cold water":
                        //1

                        ia_array[j + 8] = 4;
                        ia_array[j + 18] = i_data;
                        ia_array[j + 28] = i_ratio;

                        break;

                    case "加小冷水":
                    case "Add a little cold water":
                        //2
                        ia_array[j + 8] = 4;
                        ia_array[j + 18] = i_data;
                        ia_array[j + 28] = i_ratio;

                        break;

                    case "加热水":
                    case "Add hot water":
                        //3
                        ia_array[j + 8] = 3;
                        ia_array[j + 18] = i_data;
                        ia_array[j + 28] = i_ratio;

                        break;

                    case "手动加染助剂":
                    case "Add dyeing auxiliaries manually":
                        //4
                        ia_array[j + 8] = 2;
                        ia_array[j + 18] = i_data;
                        ia_array[j + 28] = i_ratio;

                        break;
                    case "搅拌":
                    case "Stir":
                        //5
                        ia_array[j + 8] = 6;
                        ia_array[j + 18] = i_data;
                        ia_array[j + 28] = i_ratio;

                        break;
                    //case "加补充剂":
                    //case "Add supplements":
                    //    //6
                    //    ia_no_1[j] = 6;
                    //    ia_data_1[j] = i_data;
                    //    ia_ratio[j] = i_ratio;

                    //    break;
                    case "加温水":
                    case "Add warm water":
                        //7
                        ia_array[j + 8] = 5;
                        ia_array[j + 18] = i_data;
                        ia_array[j + 28] = i_ratio;

                        break;

                    default:

                        break;
                }
            }

            ia_array[0] = 1;
            ia_array[1] = 0;
            ia_array[2] = i_BottleNum;

            int d_1 = 0;
            int d_2 = 0;
            d_2 = i_oricon;
            d_1 = d_2 / 65536;
            d_2 = d_2 % 65536;
            ia_array[3] = d_2;
            ia_array[4] = d_1;

            d_2 = i_setcon;
            d_1 = d_2 / 65536;
            d_2 = d_2 % 65536;
            ia_array[5] = d_2;
            ia_array[6] = d_1;
            ia_array[7] = dt_bottle_details.Rows.Count;

            FADM_Object.Communal._tcpModBus.Write(22288 + 40 * (i_PositionNum-1), ia_array);
        }
    }
}
