using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lib_Card.ADT8940A1.Module
{
    public class GetOrPutCloth
    {
        /// <summary>
        /// 出入布
        /// </summary>
        /// <param name="iCylinderVersion">0：单控上下气缸；1：双控上下气缸</param>
        /// /// <param name="iType">2:出布 1：入布</param>
        /// /// <param name="x_start">开始拿布x坐标</param>
        /// /// <param name="y_start">开始拿布y坐标</param>
        /// /// <param name="x_end">放布x坐标</param>
        /// /// <param name="y_end">放布y坐标</param>
        /// <returns>0：正常；-1：异常；-2：收到退出消息</returns>
        public int GetOrPut(int iCylinderVersion, int iType,int x_start,int y_start,int x_end,int y_end)
        {
            OutPut.Tray.Tray tray = new OutPut.Tray.Tray_Condition();
            if (-1 == tray.Tray_Off())
                return -1;

            //放布
            if (iType == 1)
            {

            }
            int iXRes = -1;
            Thread threadX = new Thread(() =>
            {
                try
                {
                    iXRes = CardObject.OA1Axis.Absolute_X(iCylinderVersion, x_start, 0);
                }
                catch (Exception ex)
                {
                    if ("X轴矢能未接通" == ex.Message)
                        iXRes = -3;
                    if ("X轴伺服器报警" == ex.Message)
                        iXRes = -4;
                    if ("X轴正限位已通" == ex.Message)
                        iXRes = -5;
                    if ("X轴反限位已通" == ex.Message)
                        iXRes = -6;
                }
            });
            threadX.Start();

            int iYRes = -1;
            Thread threadY = new Thread(() =>
            {
                try
                {
                    iYRes = CardObject.OA1Axis.Absolute_Y(iCylinderVersion, y_start, 0);
                }
                catch (Exception ex)
                {
                    if ("Y轴矢能未接通" == ex.Message)
                        iYRes = -3;
                    if ("Y轴伺服器报警" == ex.Message)
                        iYRes = -4;
                    if ("Y轴正限位已通" == ex.Message)
                        iYRes = -5;
                    if ("Y轴反限位已通" == ex.Message)
                        iYRes = -6;
                }
            });
            threadY.Start();

            //Z轴移动
            try
            {
                int iZRes = CardObject.OA1Axis.Absolute_Z(0, iType == 1?Lib_Card.Configure.Parameter.Other_ClothDownPulse: 0, 0);
                if (-1 == iZRes)
                    return -1;
            }
            catch (Exception ex)
            {
                if ("Z轴反限位已通" != ex.Message)
                    throw;
            }

            threadX.Join();
            if (-1 == iXRes)
                return -1;
            else if (-2 == iXRes)
                return -2;
            else if (-3 == iXRes)
                throw new Exception("X轴矢能未接通");
            else if (-4 == iXRes)
                throw new Exception("X轴伺服器报警");
            else if (-5 == iXRes)
                throw new Exception("X轴正限位已通");
            else if (-6 == iXRes)
                throw new Exception("X轴反限位已通");

            threadY.Join();
            if (-1 == iYRes)
                return -1;
            else if (-2 == iYRes)
                return -2;
            else if (-3 == iYRes)
                throw new Exception("Y轴矢能未接通");
            else if (-4 == iYRes)
                throw new Exception("Y轴伺服器报警");
            else if (-5 == iYRes)
                throw new Exception("Y轴正限位已通");
            else if (-6 == iYRes)
                throw new Exception("Y轴反限位已通");

            //阻挡出
            Lib_Card.ADT8940A1.OutPut.Block.Block block = new Lib_Card.ADT8940A1.OutPut.Block.Block_Condition();
            if (-1 == block.Block_Out())
                return -1;

            //气缸慢速下
            CylinderMo cylinderMo = new CylinderMo();
            if (-1 == cylinderMo.CylinderSlow(iCylinderVersion))
                return -1;

            //出布时，在慢速中位置才打开布夹
            if(iType == 2)
            {
                //Z轴移动
                try
                {
                    int iZRes = CardObject.OA1Axis.Absolute_Z(0, Lib_Card.Configure.Parameter.Other_CupDownPulse, 0);
                    if (-1 == iZRes)
                        return -1;
                }
                catch (Exception ex)
                {
                    if ("Z轴反限位已通" != ex.Message)
                        throw;
                }
                //气缸到阻挡位
                if (-1 == cylinderMo.CylinderBlock(iCylinderVersion))
                    return -1;

            }

            //开始夹布
            //Z轴移动
            try
            {
                int iZRes = CardObject.OA1Axis.Absolute_Z(0, Lib_Card.Configure.Parameter.Other_ClosePulse, 0);
                if (-1 == iZRes)
                    return -1;
            }
            catch (Exception ex)
            {
                if ("Z轴反限位已通" != ex.Message)
                    throw;
            }
            //气缸上
            OutPut.Cylinder.Cylinder cylinder;
            if (0 == iCylinderVersion)
                cylinder = new OutPut.Cylinder.SingleControl.Cylinder_Condition();
            else
                cylinder = new OutPut.Cylinder.DualControl.Cylinder_Condition();
            if (-1 == cylinder.CylinderUp(0))
                return -1;

            if (iType == 2)
            {
                //等待5秒
                Thread.Sleep(5000);

                //接液盘伸出
                if (-1 == tray.Tray_On())
                    return -1;
                //废液回抽打开
                Lib_Card.ADT8940A1.OutPut.Waste.Waste waste = new Lib_Card.ADT8940A1.OutPut.Waste.Waste_Basic();
                if (-1 == waste.Waste_On())
                    return -1;
            }

            //定点移动到目标位

            int iXRes1 = -1;
            Thread threadX1 = new Thread(() =>
            {
                try
                {
                    iXRes1 = CardObject.OA1Axis.Absolute_X(iCylinderVersion, x_end, 0);
                }
                catch (Exception ex)
                {
                    if ("X轴矢能未接通" == ex.Message)
                        iXRes1 = -3;
                    if ("X轴伺服器报警" == ex.Message)
                        iXRes1 = -4;
                    if ("X轴正限位已通" == ex.Message)
                        iXRes1 = -5;
                    if ("X轴反限位已通" == ex.Message)
                        iXRes1 = -6;
                }
            });
            threadX1.Start();

            int iYRes1 = -1;
            Thread threadY1 = new Thread(() =>
            {
                try
                {
                    iYRes1 = CardObject.OA1Axis.Absolute_Y(iCylinderVersion, y_end, 0);
                }
                catch (Exception ex)
                {
                    if ("Y轴矢能未接通" == ex.Message)
                        iYRes1 = -3;
                    if ("Y轴伺服器报警" == ex.Message)
                        iYRes1 = -4;
                    if ("Y轴正限位已通" == ex.Message)
                        iYRes1 = -5;
                    if ("Y轴反限位已通" == ex.Message)
                        iYRes1 = -6;
                }
            });
            threadY1.Start();

            threadX1.Join();
            if (-1 == iXRes1)
                return -1;
            else if (-2 == iXRes1)
                return -2;
            else if (-3 == iXRes1)
                throw new Exception("X轴矢能未接通");
            else if (-4 == iXRes1)
                throw new Exception("X轴伺服器报警");
            else if (-5 == iXRes1)
                throw new Exception("X轴正限位已通");
            else if (-6 == iXRes1)
                throw new Exception("X轴反限位已通");

            threadY1.Join();
            if (-1 == iYRes1)
                return -1;
            else if (-2 == iYRes1)
                return -2;
            else if (-3 == iYRes1)
                throw new Exception("Y轴矢能未接通");
            else if (-4 == iYRes1)
                throw new Exception("Y轴伺服器报警");
            else if (-5 == iYRes1)
                throw new Exception("Y轴正限位已通");
            else if (-6 == iYRes1)
                throw new Exception("Y轴反限位已通");

            if (iType == 2)
            {
                //伸出阻挡气缸
                if (-1 == block.Block_Out())
                    return -1;

                //气缸到阻挡位
                if (-1 == cylinderMo.CylinderBlock(iCylinderVersion))
                    return -1;
            }
            else
            {
                //伸出阻挡气缸
                if (-1 == block.Block_Out())
                    return -1;
                //气缸慢速中
                if (-1 == cylinderMo.CylinderSlow(iCylinderVersion))
                    return -1;
            }

            //开始放布
            //Z轴移动
            try
            {
                int iZRes = CardObject.OA1Axis.Absolute_Z(0, Lib_Card.Configure.Parameter.Other_OpenPulse, 0);
                if (-1 == iZRes)
                    return -1;
            }
            catch (Exception ex)
            {
                if ("Z轴反限位已通" != ex.Message)
                    throw;
            }

            //放布延时
            Thread.Sleep(2000);

            if (iType == 2)
            {
                //废液回抽关闭
                Lib_Card.ADT8940A1.OutPut.Waste.Waste waste = new Lib_Card.ADT8940A1.OutPut.Waste.Waste_Basic();
                if (-1 == waste.Waste_Off())
                    return -1;
            }

            return 0;
        }

        /// <summary>
        /// 出入布(转子缸)
        /// </summary>
        /// <param name="iCylinderVersion">0：单控上下气缸；1：双控上下气缸</param>
        /// /// <param name="iType">2:出布 1：入布</param>
        /// /// <param name="x_start">开始拿布x坐标</param>
        /// /// <param name="y_start">开始拿布y坐标</param>
        /// /// <param name="x_end">放布x坐标</param>
        /// /// <param name="y_end">放布y坐标</param>
        /// /// <param name="x_t">挡水板x坐标</param>
        /// /// <param name="y_t">挡水板y坐标</param>
        /// <returns>0：正常；-1：异常；-2：收到退出消息</returns>
        public int GetOrPut_Rotor(int iCylinderVersion, int iType, int x_start, int y_start, int x_end, int y_end, int x_t, int y_t)
        {
            OutPut.Tray.Tray tray = new OutPut.Tray.Tray_Condition();
            if (-1 == tray.Tray_Off())
                return -1;

            
            int iXRes = -1;
            Thread threadX = new Thread(() =>
            {
                try
                {
                    iXRes = CardObject.OA1Axis.Absolute_X(iCylinderVersion, x_start, 0);
                }
                catch (Exception ex)
                {
                    if ("X轴矢能未接通" == ex.Message)
                        iXRes = -3;
                    if ("X轴伺服器报警" == ex.Message)
                        iXRes = -4;
                    if ("X轴正限位已通" == ex.Message)
                        iXRes = -5;
                    if ("X轴反限位已通" == ex.Message)
                        iXRes = -6;
                }
            });
            threadX.Start();

            int iYRes = -1;
            Thread threadY = new Thread(() =>
            {
                try
                {
                    iYRes = CardObject.OA1Axis.Absolute_Y(iCylinderVersion, y_start, 0);
                }
                catch (Exception ex)
                {
                    if ("Y轴矢能未接通" == ex.Message)
                        iYRes = -3;
                    if ("Y轴伺服器报警" == ex.Message)
                        iYRes = -4;
                    if ("Y轴正限位已通" == ex.Message)
                        iYRes = -5;
                    if ("Y轴反限位已通" == ex.Message)
                        iYRes = -6;
                }
            });
            threadY.Start();

            

            threadX.Join();
            if (-1 == iXRes)
                return -1;
            else if (-2 == iXRes)
                return -2;
            else if (-3 == iXRes)
                throw new Exception("X轴矢能未接通");
            else if (-4 == iXRes)
                throw new Exception("X轴伺服器报警");
            else if (-5 == iXRes)
                throw new Exception("X轴正限位已通");
            else if (-6 == iXRes)
                throw new Exception("X轴反限位已通");

            threadY.Join();
            if (-1 == iYRes)
                return -1;
            else if (-2 == iYRes)
                return -2;
            else if (-3 == iYRes)
                throw new Exception("Y轴矢能未接通");
            else if (-4 == iYRes)
                throw new Exception("Y轴伺服器报警");
            else if (-5 == iYRes)
                throw new Exception("Y轴正限位已通");
            else if (-6 == iYRes)
                throw new Exception("Y轴反限位已通");

            

            //阻挡出
            Lib_Card.ADT8940A1.OutPut.Block.Block block = new Lib_Card.ADT8940A1.OutPut.Block.Block_Condition();
            if (-1 == block.Block_Out())
                return -1;
            CylinderMo cylinderMo = new CylinderMo();

            //放布
            if (iType == 1)
            {
                //气缸到阻挡位
                //CylinderMo cylinderMo = new CylinderMo();
                if (-1 == cylinderMo.CylinderSlow(iCylinderVersion))
                    return -1;

                //撑开抓手拿布笼
                try
                {
                    int iZRes = CardObject.OA1Axis.Absolute_Z(0,  Lib_Card.Configure.Parameter.Other_CupDownPulse, 0);
                    if (-1 == iZRes)
                        return -1;
                }
                catch (Exception ex)
                {
                    if ("Z轴反限位已通" != ex.Message)
                        throw;
                }

                //移动到杯位

                //定点移动到目标位

                int iXRes1 = -1;
                Thread threadX1 = new Thread(() =>
                {
                    try
                    {
                        iXRes1 = CardObject.OA1Axis.Absolute_X(iCylinderVersion, x_end, 0);
                    }
                    catch (Exception ex)
                    {
                        if ("X轴矢能未接通" == ex.Message)
                            iXRes1 = -3;
                        if ("X轴伺服器报警" == ex.Message)
                            iXRes1 = -4;
                        if ("X轴正限位已通" == ex.Message)
                            iXRes1 = -5;
                        if ("X轴反限位已通" == ex.Message)
                            iXRes1 = -6;
                    }
                });
                threadX1.Start();

                int iYRes1 = -1;
                Thread threadY1 = new Thread(() =>
                {
                    try
                    {
                        iYRes1 = CardObject.OA1Axis.Absolute_Y(iCylinderVersion, y_end, 0);
                    }
                    catch (Exception ex)
                    {
                        if ("Y轴矢能未接通" == ex.Message)
                            iYRes1 = -3;
                        if ("Y轴伺服器报警" == ex.Message)
                            iYRes1 = -4;
                        if ("Y轴正限位已通" == ex.Message)
                            iYRes1 = -5;
                        if ("Y轴反限位已通" == ex.Message)
                            iYRes1 = -6;
                    }
                });
                threadY1.Start();

                threadX1.Join();
                if (-1 == iXRes1)
                    return -1;
                else if (-2 == iXRes1)
                    return -2;
                else if (-3 == iXRes1)
                    throw new Exception("X轴矢能未接通");
                else if (-4 == iXRes1)
                    throw new Exception("X轴伺服器报警");
                else if (-5 == iXRes1)
                    throw new Exception("X轴正限位已通");
                else if (-6 == iXRes1)
                    throw new Exception("X轴反限位已通");

                threadY1.Join();
                if (-1 == iYRes1)
                    return -1;
                else if (-2 == iYRes1)
                    return -2;
                else if (-3 == iYRes1)
                    throw new Exception("Y轴矢能未接通");
                else if (-4 == iYRes1)
                    throw new Exception("Y轴伺服器报警");
                else if (-5 == iYRes1)
                    throw new Exception("Y轴正限位已通");
                else if (-6 == iYRes1)
                    throw new Exception("Y轴反限位已通");

                //气缸到阻挡位
                if (-1 == cylinderMo.CylinderSlow(iCylinderVersion))
                    return -1;

            }
            else
            {
                //气缸慢速下
                if (-1 == cylinderMo.CylinderSlow(iCylinderVersion))
                    return -1;
                //撑开抓手拿挡水板
                try
                {
                    int iZRes = CardObject.OA1Axis.Absolute_Z(0, Lib_Card.Configure.Parameter.Other_ClothDownPulse, 0);
                    if (-1 == iZRes)
                        return -1;
                }
                catch (Exception ex)
                {
                    if ("Z轴反限位已通" != ex.Message)
                        throw;
                }

                //定点移动到目标位（放挡水板处）

                int iXRes1 = -1;
                Thread threadX1 = new Thread(() =>
                {
                    try
                    {
                        iXRes1 = CardObject.OA1Axis.Absolute_X(iCylinderVersion, x_t, 0);
                    }
                    catch (Exception ex)
                    {
                        if ("X轴矢能未接通" == ex.Message)
                            iXRes1 = -3;
                        if ("X轴伺服器报警" == ex.Message)
                            iXRes1 = -4;
                        if ("X轴正限位已通" == ex.Message)
                            iXRes1 = -5;
                        if ("X轴反限位已通" == ex.Message)
                            iXRes1 = -6;
                    }
                });
                threadX1.Start();

                int iYRes1 = -1;
                Thread threadY1 = new Thread(() =>
                {
                    try
                    {
                        iYRes1 = CardObject.OA1Axis.Absolute_Y(iCylinderVersion, y_t, 0);
                    }
                    catch (Exception ex)
                    {
                        if ("Y轴矢能未接通" == ex.Message)
                            iYRes1 = -3;
                        if ("Y轴伺服器报警" == ex.Message)
                            iYRes1 = -4;
                        if ("Y轴正限位已通" == ex.Message)
                            iYRes1 = -5;
                        if ("Y轴反限位已通" == ex.Message)
                            iYRes1 = -6;
                    }
                });
                threadY1.Start();

                threadX1.Join();
                if (-1 == iXRes1)
                    return -1;
                else if (-2 == iXRes1)
                    return -2;
                else if (-3 == iXRes1)
                    throw new Exception("X轴矢能未接通");
                else if (-4 == iXRes1)
                    throw new Exception("X轴伺服器报警");
                else if (-5 == iXRes1)
                    throw new Exception("X轴正限位已通");
                else if (-6 == iXRes1)
                    throw new Exception("X轴反限位已通");

                threadY1.Join();
                if (-1 == iYRes1)
                    return -1;
                else if (-2 == iYRes1)
                    return -2;
                else if (-3 == iYRes1)
                    throw new Exception("Y轴矢能未接通");
                else if (-4 == iYRes1)
                    throw new Exception("Y轴伺服器报警");
                else if (-5 == iYRes1)
                    throw new Exception("Y轴正限位已通");
                else if (-6 == iYRes1)
                    throw new Exception("Y轴反限位已通");

                

                //气缸到慢速中
                if (-1 == cylinderMo.CylinderSlow(iCylinderVersion))
                    return -1;
            }
            


            //放下布笼或者挡水板
            try
            {
                int iZRes = CardObject.OA1Axis.Absolute_Z(0, 0, 0);
                if (-1 == iZRes)
                    return -1;
            }
            catch (Exception ex)
            {
                if ("Z轴反限位已通" != ex.Message)
                    throw;
            }

            //气缸上
            OutPut.Cylinder.Cylinder cylinder;
            if (0 == iCylinderVersion)
                cylinder = new OutPut.Cylinder.SingleControl.Cylinder_Condition();
            else
                cylinder = new OutPut.Cylinder.DualControl.Cylinder_Condition();
            if (-1 == cylinder.CylinderUp(0))
                return -1;

            //放布
            if (iType == 1)
            {
                

                //移动到杯位

                //定点移动到目标位（挡水板位置）

                int iXRes1 = -1;
                Thread threadX1 = new Thread(() =>
                {
                    try
                    {
                        iXRes1 = CardObject.OA1Axis.Absolute_X(iCylinderVersion, x_t, 0);
                    }
                    catch (Exception ex)
                    {
                        if ("X轴矢能未接通" == ex.Message)
                            iXRes1 = -3;
                        if ("X轴伺服器报警" == ex.Message)
                            iXRes1 = -4;
                        if ("X轴正限位已通" == ex.Message)
                            iXRes1 = -5;
                        if ("X轴反限位已通" == ex.Message)
                            iXRes1 = -6;
                    }
                });
                threadX1.Start();

                int iYRes1 = -1;
                Thread threadY1 = new Thread(() =>
                {
                    try
                    {
                        iYRes1 = CardObject.OA1Axis.Absolute_Y(iCylinderVersion, y_t, 0);
                    }
                    catch (Exception ex)
                    {
                        if ("Y轴矢能未接通" == ex.Message)
                            iYRes1 = -3;
                        if ("Y轴伺服器报警" == ex.Message)
                            iYRes1 = -4;
                        if ("Y轴正限位已通" == ex.Message)
                            iYRes1 = -5;
                        if ("Y轴反限位已通" == ex.Message)
                            iYRes1 = -6;
                    }
                });
                threadY1.Start();

                threadX1.Join();
                if (-1 == iXRes1)
                    return -1;
                else if (-2 == iXRes1)
                    return -2;
                else if (-3 == iXRes1)
                    throw new Exception("X轴矢能未接通");
                else if (-4 == iXRes1)
                    throw new Exception("X轴伺服器报警");
                else if (-5 == iXRes1)
                    throw new Exception("X轴正限位已通");
                else if (-6 == iXRes1)
                    throw new Exception("X轴反限位已通");

                threadY1.Join();
                if (-1 == iYRes1)
                    return -1;
                else if (-2 == iYRes1)
                    return -2;
                else if (-3 == iYRes1)
                    throw new Exception("Y轴矢能未接通");
                else if (-4 == iYRes1)
                    throw new Exception("Y轴伺服器报警");
                else if (-5 == iYRes1)
                    throw new Exception("Y轴正限位已通");
                else if (-6 == iYRes1)
                    throw new Exception("Y轴反限位已通");

                //气缸到阻挡位
                //CylinderMo cylinderMo = new CylinderMo();
                if (-1 == cylinderMo.CylinderSlow(iCylinderVersion))
                    return -1;

                //撑开抓手拿挡水板
                try
                {
                    int iZRes = CardObject.OA1Axis.Absolute_Z(0, Lib_Card.Configure.Parameter.Other_ClothDownPulse, 0);
                    if (-1 == iZRes)
                        return -1;
                }
                catch (Exception ex)
                {
                    if ("Z轴反限位已通" != ex.Message)
                        throw;
                }

            }
            else
            {
                

                //定点移动到目标位（杯位）

                int iXRes1 = -1;
                Thread threadX1 = new Thread(() =>
                {
                    try
                    {
                        iXRes1 = CardObject.OA1Axis.Absolute_X(iCylinderVersion, x_start, 0);
                    }
                    catch (Exception ex)
                    {
                        if ("X轴矢能未接通" == ex.Message)
                            iXRes1 = -3;
                        if ("X轴伺服器报警" == ex.Message)
                            iXRes1 = -4;
                        if ("X轴正限位已通" == ex.Message)
                            iXRes1 = -5;
                        if ("X轴反限位已通" == ex.Message)
                            iXRes1 = -6;
                    }
                });
                threadX1.Start();

                int iYRes1 = -1;
                Thread threadY1 = new Thread(() =>
                {
                    try
                    {
                        iYRes1 = CardObject.OA1Axis.Absolute_Y(iCylinderVersion, y_start, 0);
                    }
                    catch (Exception ex)
                    {
                        if ("Y轴矢能未接通" == ex.Message)
                            iYRes1 = -3;
                        if ("Y轴伺服器报警" == ex.Message)
                            iYRes1 = -4;
                        if ("Y轴正限位已通" == ex.Message)
                            iYRes1 = -5;
                        if ("Y轴反限位已通" == ex.Message)
                            iYRes1 = -6;
                    }
                });
                threadY1.Start();

                threadX1.Join();
                if (-1 == iXRes1)
                    return -1;
                else if (-2 == iXRes1)
                    return -2;
                else if (-3 == iXRes1)
                    throw new Exception("X轴矢能未接通");
                else if (-4 == iXRes1)
                    throw new Exception("X轴伺服器报警");
                else if (-5 == iXRes1)
                    throw new Exception("X轴正限位已通");
                else if (-6 == iXRes1)
                    throw new Exception("X轴反限位已通");

                threadY1.Join();
                if (-1 == iYRes1)
                    return -1;
                else if (-2 == iYRes1)
                    return -2;
                else if (-3 == iYRes1)
                    throw new Exception("Y轴矢能未接通");
                else if (-4 == iYRes1)
                    throw new Exception("Y轴伺服器报警");
                else if (-5 == iYRes1)
                    throw new Exception("Y轴正限位已通");
                else if (-6 == iYRes1)
                    throw new Exception("Y轴反限位已通");

                //气缸到阻挡位
                if (-1 == cylinderMo.CylinderBlock(iCylinderVersion))
                    return -1;
                //撑开抓手拿布笼
                try
                {
                    int iZRes = CardObject.OA1Axis.Absolute_Z(0, Lib_Card.Configure.Parameter.Other_CupDownPulse, 0);
                    if (-1 == iZRes)
                        return -1;
                }
                catch (Exception ex)
                {
                    if ("Z轴反限位已通" != ex.Message)
                        throw;
                }
            }
            //气缸上
            //OutPut.Cylinder.Cylinder cylinder;
            if (0 == iCylinderVersion)
                cylinder = new OutPut.Cylinder.SingleControl.Cylinder_Condition();
            else
                cylinder = new OutPut.Cylinder.DualControl.Cylinder_Condition();
            if (-1 == cylinder.CylinderUp(0))
                return -1;

            if (iType == 2)
            {
                //等待5秒
                Thread.Sleep(5000);

                ////接液盘伸出
                //if (-1 == tray.Tray_On())
                //    return -1;
                //废液回抽打开
                Lib_Card.ADT8940A1.OutPut.Waste.Waste waste = new Lib_Card.ADT8940A1.OutPut.Waste.Waste_Basic();
                if (-1 == waste.Waste_On())
                    return -1;
            }


            //移动到目标位
            int iXRes2 = -1;
            Thread threadX2 = new Thread(() =>
            {
                try
                {
                    iXRes2 = CardObject.OA1Axis.Absolute_X(iCylinderVersion, x_end, 0);
                }
                catch (Exception ex)
                {
                    if ("X轴矢能未接通" == ex.Message)
                        iXRes2 = -3;
                    if ("X轴伺服器报警" == ex.Message)
                        iXRes2 = -4;
                    if ("X轴正限位已通" == ex.Message)
                        iXRes2 = -5;
                    if ("X轴反限位已通" == ex.Message)
                        iXRes2 = -6;
                }
            });
            threadX2.Start();

            int iYRes2 = -1;
            Thread threadY2 = new Thread(() =>
            {
                try
                {
                    iYRes2 = CardObject.OA1Axis.Absolute_Y(iCylinderVersion, y_end, 0);
                }
                catch (Exception ex)
                {
                    if ("Y轴矢能未接通" == ex.Message)
                        iYRes2 = -3;
                    if ("Y轴伺服器报警" == ex.Message)
                        iYRes2 = -4;
                    if ("Y轴正限位已通" == ex.Message)
                        iYRes2 = -5;
                    if ("Y轴反限位已通" == ex.Message)
                        iYRes2 = -6;
                }
            });
            threadY2.Start();

            threadX2.Join();
            if (-1 == iXRes2)
                return -1;
            else if (-2 == iXRes2)
                return -2;
            else if (-3 == iXRes2)
                throw new Exception("X轴矢能未接通");
            else if (-4 == iXRes2)
                throw new Exception("X轴伺服器报警");
            else if (-5 == iXRes2)
                throw new Exception("X轴正限位已通");
            else if (-6 == iXRes2)
                throw new Exception("X轴反限位已通");

            threadY2.Join();
            if (-1 == iYRes2)
                return -1;
            else if (-2 == iYRes2)
                return -2;
            else if (-3 == iYRes2)
                throw new Exception("Y轴矢能未接通");
            else if (-4 == iYRes2)
                throw new Exception("Y轴伺服器报警");
            else if (-5 == iYRes2)
                throw new Exception("Y轴正限位已通");
            else if (-6 == iYRes2)
                throw new Exception("Y轴反限位已通");

            //放布时
            if (iType == 1)
            {
                //移动完成后，气缸慢速中
                if (-1 == cylinderMo.CylinderSlow(iCylinderVersion))
                    return -1;
            }
            else
            {
                //移动完成后，气缸到阻挡位
                if (-1 == cylinderMo.CylinderSlow(iCylinderVersion))
                    return -1;
            }

            //关闭抓手，放下
            try
            {
                int iZRes = CardObject.OA1Axis.Absolute_Z(0, 0, 0);
                if (-1 == iZRes)
                    return -1;
            }
            catch (Exception ex)
            {
                if ("Z轴反限位已通" != ex.Message)
                    throw;
            }

            if (iType == 2)
            {
                //废液回抽关闭
                Lib_Card.ADT8940A1.OutPut.Waste.Waste waste = new Lib_Card.ADT8940A1.OutPut.Waste.Waste_Basic();
                if (-1 == waste.Waste_Off())
                    return -1;
            }

            return 0;
        }


    }
}
