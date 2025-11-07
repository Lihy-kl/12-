//using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace Lib_DataBank
{
    public class AAndB
    {
        public string MB { get; set; }
        public string XS { get; set; }
        public string WC { get; set; }
        public DateTime? SJ { get; set; } // 可空类型匹配数据库NULL
    }

    public class AbsCupDetails
    {
        /// <summary>
        /// 杯号（主键，非空）
        /// </summary>
        public int CupNum { get; set; }

        /// <summary>
        /// 是否启用（tinyint，允许为空）
        /// </summary>
        public byte? Enable { get; set; }

        /// <summary>
        /// 是否正在使用（tinyint，允许为空）
        /// </summary>
        public byte? IsUsing { get; set; }

        /// <summary>
        /// 状态（字符串，最大长度50，允许为空）
        /// </summary>
        public string Statues { get; set; }

        /// <summary>
        /// 瓶号（int，允许为空）
        /// </summary>
        public int? BottleNum { get; set; }

        /// <summary>
        /// 样品剂量（decimal(18,3)，允许为空）
        /// </summary>
        public decimal? SampleDosage { get; set; }

        /// <summary>
        /// 实际样品剂量（decimal(18,3)，允许为空）
        /// </summary>
        public decimal? RealSampleDosage { get; set; }

        /// <summary>
        /// 添加剂数量（int，允许为空）
        /// </summary>
        public int? AdditivesNum { get; set; }

        /// <summary>
        /// 添加剂剂量（decimal(18,3)，允许为空）
        /// </summary>
        public decimal? AdditivesDosage { get; set; }

        /// <summary>
        /// 实际添加剂剂量（decimal(18,3)，允许为空）
        /// </summary>
        public decimal? RealAdditivesDosage { get; set; }

        /// <summary>
        /// 脉冲值（int，允许为空）
        /// </summary>
        public int? Pulse { get; set; }

        /// <summary>
        /// 协作参数（int，允许为空）
        /// </summary>
        public int? Cooperate { get; set; }

        /// <summary>
        /// 类型（int，允许为空）
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// 起始波长（int，允许为空）
        /// </summary>
        public int? StartWave { get; set; }

        /// <summary>
        /// 结束波长（int，允许为空）
        /// </summary>
        public int? EndWave { get; set; }

        /// <summary>
        /// 中间波长（int，允许为空）
        /// </summary>
        public int? IntWave { get; set; }

        /// <summary>
        /// 总重量（decimal(18,3)，允许为空）
        /// </summary>
        public decimal? TotalWeight { get; set; }
    }

    public class AbsDetails
    {
        /// <summary>
        /// 杯号（int，允许为空）
        /// </summary>
        public int? CupNum { get; set; }

        /// <summary>
        /// 完成状态（int，允许为空）
        /// </summary>
        public int? Finish { get; set; }

        /// <summary>
        /// 开始时间（datetime2(7)，允许为空）
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 完成时间（datetime2(7)，允许为空）
        /// </summary>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 协作参数（int，允许为空）
        /// </summary>
        public int? Cooperate { get; set; }

        /// <summary>
        /// 步骤编号（int，允许为空）
        /// </summary>
        public int? StepNum { get; set; }

        /// <summary>
        /// 工艺名称（nvarchar(50)，允许为空）
        /// </summary>
        public string TechnologyName { get; set; }

        /// <summary>
        /// 搅拌速率（int，允许为空）
        /// </summary>
        public int? StirringRate { get; set; }

        /// <summary>
        /// 搅拌时间（int，允许为空）
        /// </summary>
        public int? StirringTime { get; set; }

        /// <summary>
        /// 排水时间（int，允许为空）
        /// </summary>
        public int? DrainTime { get; set; }

        /// <summary>
        /// 平行盘时间（int，允许为空）
        /// </summary>
        public int? ParallelizingDishTime { get; set; }

        /// <summary>
        /// 泵送时间（int，允许为空）
        /// </summary>
        public int? PumpingTime { get; set; }

        /// <summary>
        /// 起始波长（int，允许为空）
        /// </summary>
        public int? StartingWavelength { get; set; }

        /// <summary>
        /// 结束波长（int，允许为空）
        /// </summary>
        public int? EndWavelength { get; set; }

        /// <summary>
        /// 波长间隔（int，允许为空）
        /// </summary>
        public int? WavelengthInterval { get; set; }

        /// <summary>
        /// 剂量（numeric(10,3)，允许为空）
        /// </summary>
        public decimal? Dosage { get; set; }

        /// <summary>
        /// 编码（nvarchar(50)，允许为空）
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 全局唯一标识（nvarchar(50)，允许为空）
        /// </summary>
        public string GUID { get; set; }
    }

    public class AbsDropDetails
    {
        /// <summary>
        /// 批次名称（nvarchar(12)，允许为空）
        /// </summary>
        public string BatchName { get; set; }

        /// <summary>
        /// 杯号（int，允许为空）
        /// </summary>
        public int? CupNum { get; set; }

        /// <summary>
        /// 配方编码（nvarchar(50)，允许为空）
        /// </summary>
        public string FormulaCode { get; set; }

        /// <summary>
        /// 版本号（int，允许为空）
        /// </summary>
        public int? VersionNum { get; set; }

        /// <summary>
        /// 索引编号（int，允许为空）
        /// </summary>
        public int? IndexNum { get; set; }

        /// <summary>
        /// 辅助编码（nvarchar(50)，允许为空）
        /// </summary>
        public string AssistantCode { get; set; }

        /// <summary>
        /// 配方剂量（float，允许为空）
        /// </summary>
        public float? FormulaDosage { get; set; }

        /// <summary>
        /// 计量单位（nvarchar(11)，允许为空）
        /// </summary>
        public string UnitOfAccount { get; set; }

        /// <summary>
        /// 瓶号（int，允许为空）
        /// </summary>
        public int? BottleNum { get; set; }

        /// <summary>
        /// 设置浓度（float，允许为空）
        /// </summary>
        public float? SettingConcentration { get; set; }

        /// <summary>
        /// 实际浓度（float，允许为空）
        /// </summary>
        public float? RealConcentration { get; set; }

        /// <summary>
        /// 辅助名称（nvarchar(50)，允许为空）
        /// </summary>
        public string AssistantName { get; set; }

        /// <summary>
        /// 目标滴加重量（numeric(10,3)，允许为空）
        /// </summary>
        public decimal? ObjectDropWeight { get; set; }

        /// <summary>
        /// 实际滴加重量（numeric(10,3)，允许为空）
        /// </summary>
        public decimal? RealDropWeight { get; set; }

        /// <summary>
        /// 瓶子选择（tinyint，允许为空）
        /// </summary>
        public byte? BottleSelection { get; set; }

        /// <summary>
        /// 最小重量（int，允许为空）
        /// </summary>
        public int? MinWeight { get; set; }

        /// <summary>
        /// 完成状态（tinyint，允许为空）
        /// </summary>
        public byte? Finish { get; set; }

        /// <summary>
        /// 目标粉末重量（nvarchar(12)，允许为空）
        /// </summary>
        public string ObjectPowderWeight { get; set; }

        /// <summary>
        /// 实际粉末重量（nvarchar(12)，允许为空）
        /// </summary>
        public string RealPowderWeight { get; set; }

        /// <summary>
        /// 是否显示（tinyint，允许为空）
        /// </summary>
        public byte? IsShow { get; set; }

        /// <summary>
        /// 是否滴加（tinyint，允许为空）
        /// </summary>
        public byte? IsDrop { get; set; }

        /// <summary>
        /// 冲泡时间（datetime2(7)，允许为空）
        /// </summary>
        public DateTime? BrewingData { get; set; }

        /// <summary>
        /// 需要的脉冲数（int，允许为空）
        /// </summary>
        public int? NeedPulse { get; set; }
    }

    public class AbsDropHead
    {
        /// <summary>
        /// 批次名称（nvarchar(12)，允许为空）
        /// </summary>
        public string BatchName { get; set; }

        /// <summary>
        /// 杯号（int，允许为空）
        /// </summary>
        public int? CupNum { get; set; }

        /// <summary>
        /// 配方编码（nvarchar(50)，允许为空）
        /// </summary>
        public string FormulaCode { get; set; }

        /// <summary>
        /// 版本号（int，允许为空）
        /// </summary>
        public int? VersionNum { get; set; }

        /// <summary>
        /// 状态（nvarchar(50)，允许为空）
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 配方名称（nvarchar(50)，允许为空）
        /// </summary>
        public string FormulaName { get; set; }

        /// <summary>
        /// 布料类型（nvarchar(50)，允许为空）
        /// </summary>
        public string ClothType { get; set; }

        /// <summary>
        /// 客户（nvarchar(50)，允许为空）
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// 加水选择（tinyint，允许为空）
        /// </summary>
        public byte? AddWaterChoose { get; set; }

        /// <summary>
        /// 复合板选择（tinyint，允许为空）
        /// </summary>
        public byte? CompoundBoardChoose { get; set; }

        /// <summary>
        /// 布料重量（float，允许为空）
        /// </summary>
        public float? ClothWeight { get; set; }

        /// <summary>
        /// 浴比（float，允许为空）
        /// </summary>
        public float? BathRatio { get; set; }

        /// <summary>
        /// 总重量（float，允许为空）
        /// </summary>
        public float? TotalWeight { get; set; }

        /// <summary>
        /// 操作员（nvarchar(50)，允许为空）
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 杯编码（nvarchar(50)，允许为空）
        /// </summary>
        public string CupCode { get; set; }

        /// <summary>
        /// 创建时间（datetime2(0)，允许为空）
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 目标加水量（numeric(10,3)，允许为空）
        /// </summary>
        public decimal? ObjectAddWaterWeight { get; set; }

        /// <summary>
        /// 实际加水量（numeric(10,3)，允许为空）
        /// </summary>
        public decimal? RealAddWaterWeight { get; set; }

        /// <summary>
        /// 试管目标加水量（nvarchar(12)，允许为空）
        /// </summary>
        public string TestTubeObjectAddWaterWeight { get; set; }

        /// <summary>
        /// 试管实际加水量（nvarchar(12)，允许为空）
        /// </summary>
        public string TestTubeRealAddWaterWeight { get; set; }

        /// <summary>
        /// 试管完成状态（tinyint，允许为空）
        /// </summary>
        public byte? TestTubeFinish { get; set; }

        /// <summary>
        /// 试管水量不足（tinyint，允许为空）
        /// </summary>
        public byte? TestTubeWaterLower { get; set; }

        /// <summary>
        /// 加水完成状态（tinyint，允许为空）
        /// </summary>
        public byte? AddWaterFinish { get; set; }

        /// <summary>
        /// 杯子完成状态（tinyint，允许为空）
        /// </summary>
        public byte? CupFinish { get; set; }

        /// <summary>
        /// 染色编码（nvarchar(50)，允许为空）
        /// </summary>
        public string DyeingCode { get; set; }

        /// <summary>
        /// 步骤（int，允许为空）
        /// </summary>
        public int? Step { get; set; }

        /// <summary>
        /// 非脱水WR值（numeric(8,2)，允许为空）
        /// </summary>
        public decimal? Non_AnhydrationWR { get; set; }

        /// <summary>
        /// 脱水WR值（numeric(8,2)，允许为空）
        /// </summary>
        public decimal? AnhydrationWR { get; set; }

        /// <summary>
        /// 处理浴比（numeric(8,2)，允许为空）
        /// </summary>
        public decimal? HandleBathRatio { get; set; }

        /// <summary>
        /// 处理参数1（int，允许为空）
        /// </summary>
        public int? Handle_Rev1 { get; set; }

        /// <summary>
        /// 处理参数2（int，允许为空）
        /// </summary>
        public int? Handle_Rev2 { get; set; }

        /// <summary>
        /// 处理参数3（int，允许为空）
        /// </summary>
        public int? Handle_Rev3 { get; set; }

        /// <summary>
        /// 处理参数4（int，允许为空）
        /// </summary>
        public int? Handle_Rev4 { get; set; }

        /// <summary>
        /// 处理参数5（int，允许为空）
        /// </summary>
        public int? Handle_Rev5 { get; set; }

        /// <summary>
        /// 描述字符（nvarchar(100)，允许为空）
        /// </summary>
        public string DescribeChar { get; set; }

        /// <summary>
        /// 完成时间（datetime2(0)，允许为空）
        /// </summary>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 开始时间（datetime2(0)，允许为空）
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 阶段（nvarchar(50)，允许为空）
        /// </summary>
        public string Stage { get; set; }

        /// <summary>
        /// 英文描述字符（nvarchar(200)，允许为空）
        /// </summary>
        public string DescribeChar_EN { get; set; }

        /// <summary>
        /// 处理浴比列表（nvarchar(200)，允许为空）
        /// </summary>
        public string HandleBRList { get; set; }
    }

    public class AbsFormulaDetails
    {
        /// <summary>
        /// 配方代码
        /// </summary>
        public string FormulaCode { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public int? VersionNum { get; set; }

        /// <summary>
        /// 索引号
        /// </summary>
        public int? IndexNum { get; set; }

        /// <summary>
        /// 辅助材料代码
        /// </summary>
        public string AssistantCode { get; set; }

        /// <summary>
        /// 配方用量
        /// </summary>
        public float? FormulaDosage { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitOfAccount { get; set; }

        /// <summary>
        /// 瓶数
        /// </summary>
        public int? BottleNum { get; set; }

        /// <summary>
        /// 设置浓度
        /// </summary>
        public float? SettingConcentration { get; set; }

        /// <summary>
        /// 实际浓度
        /// </summary>
        public float? RealConcentration { get; set; }

        /// <summary>
        /// 辅助材料名称
        /// </summary>
        public string AssistantName { get; set; }

        /// <summary>
        /// 目标滴液重量
        /// </summary>
        public string ObjectDropWeight { get; set; }

        /// <summary>
        /// 实际滴液重量
        /// </summary>
        public string RealDropWeight { get; set; }

        /// <summary>
        /// 瓶子选择
        /// </summary>
        public byte? BottleSelection { get; set; }

        /// <summary>
        /// 目标粉末重量
        /// </summary>
        public string ObjectPowderWeight { get; set; }

        /// <summary>
        /// 实际粉末重量
        /// </summary>
        public string RealPowderWeight { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public byte? IsShow { get; set; }

        /// <summary>
        /// 冲泡数据时间
        /// </summary>
        public DateTime? BrewingData { get; set; }
    }

    /// <summary>
    /// 对应abs_formula_group表的实体类
    /// </summary>
    public class AbsFormulaGroup
    {
        /// <summary>
        /// 自增主键ID（非空）
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 节点编号
        /// </summary>
        public int? Node { get; set; }

        /// <summary>
        /// 辅助材料代码
        /// </summary>
        public string AssistantCode { get; set; }

        /// <summary>
        /// 辅助材料名称
        /// </summary>
        public string AssistantName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitOfAccount { get; set; }
    }

    /// <summary>
    /// 对应abs_formula_head表的实体类（配方表头信息）
    /// </summary>
    public class AbsFormulaHead
    {
        /// <summary>
        /// 配方代码
        /// </summary>
        public string FormulaCode { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public int? VersionNum { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 配方名称
        /// </summary>
        public string FormulaName { get; set; }

        /// <summary>
        /// 布料类型
        /// </summary>
        public string ClothType { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// 加水选择（0/1）
        /// </summary>
        public byte? AddWaterChoose { get; set; }

        /// <summary>
        /// 复合板选择（0/1）
        /// </summary>
        public byte? CompoundBoardChoose { get; set; }

        /// <summary>
        /// 布料重量
        /// </summary>
        public float? ClothWeight { get; set; }

        /// <summary>
        /// 浴比
        /// </summary>
        public float? BathRatio { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public float? TotalWeight { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 杯代码
        /// </summary>
        public string CupCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 目标加水重量
        /// </summary>
        public string ObjectAddWaterWeight { get; set; }

        /// <summary>
        /// 实际加水重量
        /// </summary>
        public string RealAddWaterWeight { get; set; }

        /// <summary>
        /// 试管目标加水重量
        /// </summary>
        public string TestTubeObjectAddWaterWeight { get; set; }

        /// <summary>
        /// 试管实际加水重量
        /// </summary>
        public string TestTubeRealAddWaterWeight { get; set; }

        /// <summary>
        /// 染色代码
        /// </summary>
        public string DyeingCode { get; set; }

        /// <summary>
        /// 杯数量
        /// </summary>
        public int? CupNum { get; set; }

        /// <summary>
        /// 非脱水WR值（数值型，保留2位小数）
        /// </summary>
        public decimal? NonAnhydrationWR { get; set; }

        /// <summary>
        /// 脱水WR值（数值型，保留2位小数）
        /// </summary>
        public decimal? AnhydrationWR { get; set; }

        /// <summary>
        /// 手感浴比（数值型，保留2位小数）
        /// </summary>
        public decimal? HandleBathRatio { get; set; }

        /// <summary>
        /// 手感参数1
        /// </summary>
        public int? HandleRev1 { get; set; }

        /// <summary>
        /// 手感参数2
        /// </summary>
        public int? HandleRev2 { get; set; }

        /// <summary>
        /// 手感参数3
        /// </summary>
        public int? HandleRev3 { get; set; }

        /// <summary>
        /// 手感参数4
        /// </summary>
        public int? HandleRev4 { get; set; }

        /// <summary>
        /// 手感参数5
        /// </summary>
        public int? HandleRev5 { get; set; }

        /// <summary>
        /// 阶段
        /// </summary>
        public string Stage { get; set; }

        /// <summary>
        /// 手感浴比列表
        /// </summary>
        public string HandleBRList { get; set; }
    }

    /// <summary>
    /// 对应 abs_history_details 表的实体类（配方历史明细记录）
    /// </summary>
    public class AbsHistoryDetails
    {
        /// <summary>
        /// 批次名称
        /// </summary>
        public string BatchName { get; set; }
        /// <summary>
        /// 杯数量
        /// </summary>
        public int? CupNum { get; set; }
        /// <summary>
        /// 配方代码
        /// </summary>
        public string FormulaCode { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public int? VersionNum { get; set; }
        /// <summary>
        /// 索引号
        /// </summary>
        public int? IndexNum { get; set; }
        /// <summary>
        /// 辅助材料代码
        /// </summary>
        public string AssistantCode { get; set; }
        /// <summary>
        /// 配方用量
        /// </summary>
        public float? FormulaDosage { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitOfAccount { get; set; }
        /// <summary>
        /// 瓶数
        /// </summary>
        public int? BottleNum { get; set; }
        /// <summary>
        /// 设置浓度
        /// </summary>
        public float? SettingConcentration { get; set; }
        /// <summary>
        /// 实际浓度
        /// </summary>
        public float? RealConcentration { get; set; }
        /// <summary>
        /// 辅助材料名称
        /// </summary>
        public string AssistantName { get; set; }
        /// <summary>
        /// 目标滴液重量
        /// </summary>
        public string ObjectDropWeight { get; set; }
        /// <summary>
        /// 实际滴液重量
        /// </summary>
        public string RealDropWeight { get; set; }
        /// <summary>
        /// 瓶子选择（0/1）
        /// </summary>
        public byte? BottleSelection { get; set; }
        /// <summary>
        /// 目标粉末重量
        /// </summary>
        public string ObjectPowderWeight { get; set; }
        /// <summary>
        /// 实际粉末重量
        /// </summary>
        public string RealPowderWeight { get; set; }
        /// <summary>
        /// 是否滴加（0/1）
        /// </summary>
        public byte? IsDrop { get; set; }
        /// <summary>
        /// 冲泡数据时间
        /// </summary>
        public DateTime? BrewingData { get; set; }
    }

    /// <summary>
    /// 对应abs_history_head表的实体类（配方历史表头记录）
    /// </summary>
    public class AbsHistoryHead
    {
        /// <summary>
        /// 自增主键ID（非空）
        /// </summary>
        public int MyID { get; set; }

        /// <summary>
        /// 批次名称
        /// </summary>
        public string BatchName { get; set; }

        /// <summary>
        /// 杯数量
        /// </summary>
        public int? CupNum { get; set; }

        /// <summary>
        /// 配方代码
        /// </summary>
        public string FormulaCode { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public int? VersionNum { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 配方名称
        /// </summary>
        public string FormulaName { get; set; }

        /// <summary>
        /// 布料类型
        /// </summary>
        public string ClothType { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// 加水选择（0/1）
        /// </summary>
        public byte? AddWaterChoose { get; set; }

        /// <summary>
        /// 复合板选择（0/1）
        /// </summary>
        public byte? CompoundBoardChoose { get; set; }

        /// <summary>
        /// 布料重量
        /// </summary>
        public float? ClothWeight { get; set; }

        /// <summary>
        /// 浴比
        /// </summary>
        public float? BathRatio { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public float? TotalWeight { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 杯代码
        /// </summary>
        public string CupCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 目标加水重量
        /// </summary>
        public string ObjectAddWaterWeight { get; set; }

        /// <summary>
        /// 实际加水重量
        /// </summary>
        public string RealAddWaterWeight { get; set; }

        /// <summary>
        /// 试管目标加水重量
        /// </summary>
        public string TestTubeObjectAddWaterWeight { get; set; }

        /// <summary>
        /// 试管实际加水重量
        /// </summary>
        public string TestTubeRealAddWaterWeight { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 描述字符（中文）
        /// </summary>
        public string DescribeChar { get; set; }

        /// <summary>
        /// 染色代码
        /// </summary>
        public string DyeingCode { get; set; }

        /// <summary>
        /// 步骤
        /// </summary>
        public int? Step { get; set; }

        /// <summary>
        /// 非脱水WR值（保留2位小数）
        /// </summary>
        public decimal? NonAnhydrationWR { get; set; }

        /// <summary>
        /// 脱水WR值（保留2位小数）
        /// </summary>
        public decimal? AnhydrationWR { get; set; }

        /// <summary>
        /// 手感浴比（保留2位小数）
        /// </summary>
        public decimal? HandleBathRatio { get; set; }

        /// <summary>
        /// 手感参数1
        /// </summary>
        public int? HandleRev1 { get; set; }

        /// <summary>
        /// 手感参数2
        /// </summary>
        public int? HandleRev2 { get; set; }

        /// <summary>
        /// 手感参数3
        /// </summary>
        public int? HandleRev3 { get; set; }

        /// <summary>
        /// 手感参数4
        /// </summary>
        public int? HandleRev4 { get; set; }

        /// <summary>
        /// 手感参数5
        /// </summary>
        public int? HandleRev5 { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 过程数据（二进制）
        /// </summary>
        public byte[] ProcessData { get; set; }

        /// <summary>
        /// 标记步骤
        /// </summary>
        public string MarkStep { get; set; }

        /// <summary>
        /// 阶段
        /// </summary>
        public string Stage { get; set; }

        /// <summary>
        /// 光谱数据
        /// </summary>
        public string Spectrum { get; set; }

        /// <summary>
        /// 描述字符（英文）
        /// </summary>
        public string DescribeCharEN { get; set; }

        /// <summary>
        /// 手感浴比列表
        /// </summary>
        public string HandleBRList { get; set; }
    }

    /// <summary>
    /// 对应Abs_process表的实体类（工艺过程参数）
    /// </summary>
    public class AbsProcess
    {
        /// <summary>
        /// 步骤编号
        /// </summary>
        public int? StepNum { get; set; }

        /// <summary>
        /// 工艺名称
        /// </summary>
        public string TechnologyName { get; set; }

        /// <summary>
        /// 搅拌速率
        /// </summary>
        public int? StirringRate { get; set; }

        /// <summary>
        /// 搅拌时间
        /// </summary>
        public int? StirringTime { get; set; }

        /// <summary>
        /// 排水时间
        /// </summary>
        public int? DrainTime { get; set; }

        /// <summary>
        /// 平行盘时间
        /// </summary>
        public int? ParallelizingDishTime { get; set; }

        /// <summary>
        /// 泵送时间
        /// </summary>
        public int? PumpingTime { get; set; }

        /// <summary>
        /// 起始波长
        /// </summary>
        public int? StartingWavelength { get; set; }

        /// <summary>
        /// 结束波长
        /// </summary>
        public int? EndWavelength { get; set; }

        /// <summary>
        /// 波长间隔
        /// </summary>
        public int? WavelengthInterval { get; set; }

        /// <summary>
        /// 用量（保留3位小数）
        /// </summary>
        public decimal? Dosage { get; set; }

        /// <summary>
        /// 工艺代码
        /// </summary>
        public string Code { get; set; }
    }

    public class AbsWaitList
    {
        /// <summary>
        /// 瓶号
        /// </summary>
        public int? BottleNum { get; set; }
        /// <summary>
        /// 插入时间（等待队列的加入时间）
        /// </summary>
        public DateTime? InsertDate { get; set; }
        /// <summary>
        /// 类型（具体含义需结合业务定义，如瓶具类型、等待任务类型等）
        /// </summary>
        public int? Type { get; set; }
    }

    /// <summary>
    /// 对应alarm_table表的实体类（系统告警记录）
    /// </summary>
    public class AlarmTable
    {
        /// <summary>
        /// 自增主键ID（非空）
        /// </summary>
        public int MyID { get; set; }

        /// <summary>
        /// 告警日期
        /// </summary>
        public DateTime? MyDate { get; set; }

        /// <summary>
        /// 告警时间（高精度，保留7位小数）
        /// </summary>
        public TimeSpan? MyTime { get; set; }

        /// <summary>
        /// 告警标题
        /// </summary>
        public string AlarmHead { get; set; }

        /// <summary>
        /// 告警详情
        /// </summary>
        public string AlarmDetails { get; set; }

        /// <summary>
        /// 告警附加信息（具体含义需结合业务定义，如处理状态、类型标识等）
        /// </summary>
        public string TT { get; set; }
    }

    /// <summary>
    /// 对应assistant_details表的实体类（辅助剂详情信息）
    /// </summary>
    public class AssistantDetails
    {
        /// <summary>
        /// 自增主键ID（非空）
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 辅助剂代码
        /// </summary>
        public string AssistantCode { get; set; }

        /// <summary>
        /// 辅助剂条形码
        /// </summary>
        public string AssistantBarCode { get; set; }

        /// <summary>
        /// 辅助剂名称
        /// </summary>
        public string AssistantName { get; set; }

        /// <summary>
        /// 辅助剂类型
        /// </summary>
        public string AssistantType { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitOfAccount { get; set; }

        /// <summary>
        /// 允许的最小染色浓度
        /// </summary>
        public float? AllowMinColoringConcentration { get; set; }

        /// <summary>
        /// 允许的最大染色浓度
        /// </summary>
        public float? AllowMaxColoringConcentration { get; set; }

        /// <summary>
        /// 有效期（单位需结合业务定义，如天）
        /// </summary>
        public int? TermOfValidity { get; set; }

        /// <summary>
        /// 强度
        /// </summary>
        public float? Intensity { get; set; }

        /// <summary>
        /// 成本
        /// </summary>
        public float? Cost { get; set; }

        /// <summary>
        /// 修正值
        /// </summary>
        public float? Correcting { get; set; }

        /// <summary>
        /// 吸光度数据（可能为光谱数据字符串，如波长-吸光度集合）
        /// </summary>
        public string Abs { get; set; }

        /// <summary>
        /// 颜色空间参数L（明度），保留3位小数
        /// </summary>
        public decimal? L { get; set; }

        /// <summary>
        /// 颜色空间参数A（红绿轴），保留3位小数
        /// </summary>
        public decimal? A { get; set; }

        /// <summary>
        /// 颜色空间参数B（黄蓝轴），保留3位小数
        /// </summary>
        public decimal? B { get; set; }

        /// <summary>
        /// 第二组吸光度数据
        /// </summary>
        public string Abs2 { get; set; }

        /// <summary>
        /// 第二组颜色空间参数L
        /// </summary>
        public decimal? L2 { get; set; }

        /// <summary>
        /// 第二组颜色空间参数A
        /// </summary>
        public decimal? A2 { get; set; }

        /// <summary>
        /// 第二组颜色空间参数B
        /// </summary>
        public decimal? B2 { get; set; }

        /// <summary>
        /// 起始波长
        /// </summary>
        public int? StartingWavelength { get; set; }

        /// <summary>
        /// 结束波长
        /// </summary>
        public int? EndWavelength { get; set; }

        /// <summary>
        /// 波长间隔
        /// </summary>
        public int? WavelengthInterval { get; set; }

        /// <summary>
        /// 重新称重标识（可能为0/1或次数，需结合业务定义）
        /// </summary>
        public int? Reweigh { get; set; }
    }

    /// <summary>
    /// 对应bottle_check表的实体类（瓶具检查状态记录）
    /// </summary>
    public class BottleCheck
    {
        /// <summary>
        /// 瓶号（标识具体的瓶具）
        /// </summary>
        public int? BottleNum { get; set; }

        /// <summary>
        /// 检查完成状态（通常0表示未完成，1表示已完成，tinyint类型对应byte）
        /// </summary>
        public byte? Finish { get; set; }

        /// <summary>
        /// 检查成功状态（通常0表示失败，1表示成功，tinyint类型对应byte）
        /// </summary>
        public byte? Successed { get; set; }
    }

    /// <summary>
    /// 对应bottle_details表的实体类（瓶具详细信息，含浓度、重量、调整记录等）
    /// </summary>
    public class BottleDetails
    {
        /// <summary>
        /// 瓶号（标识具体的瓶具）
        /// </summary>
        public int? BottleNum { get; set; }

        /// <summary>
        /// 关联的辅助剂代码
        /// </summary>
        public string AssistantCode { get; set; }

        /// <summary>
        /// 设置浓度
        /// </summary>
        public float? SettingConcentration { get; set; }

        /// <summary>
        /// 实际浓度
        /// </summary>
        public float? RealConcentration { get; set; }

        /// <summary>
        /// 上次调整重量
        /// </summary>
        public float? LastAdjustWeight { get; set; }

        /// <summary>
        /// 当前调整重量
        /// </summary>
        public float? CurrentAdjustWeight { get; set; }

        /// <summary>
        /// 调整值
        /// </summary>
        public float? AdjustValue { get; set; }

        /// <summary>
        /// 酿造/配制代码
        /// </summary>
        public string BrewingCode { get; set; }

        /// <summary>
        /// 允许的最大重量
        /// </summary>
        public float? AllowMaxWeight { get; set; }

        /// <summary>
        /// 当前重量（保留2位小数）
        /// </summary>
        public decimal? CurrentWeight { get; set; }

        /// <summary>
        /// 配制时间（高精度时间戳）
        /// </summary>
        public DateTime? BrewingData { get; set; }

        /// <summary>
        /// 注射器类型
        /// </summary>
        public string SyringeType { get; set; }

        /// <summary>
        /// 最小滴液重量
        /// </summary>
        public float? DropMinWeight { get; set; }

        /// <summary>
        /// 原始瓶号（可能关联母瓶）
        /// </summary>
        public int? OriginalBottleNum { get; set; }

        /// <summary>
        /// 调整成功状态（0=失败，1=成功，tinyint对应byte）
        /// </summary>
        public byte? AdjustSuccess { get; set; }

        /// <summary>
        /// 自检项1结果
        /// </summary>
        public string SelfChecking1 { get; set; }

        /// <summary>
        /// 自检项2结果
        /// </summary>
        public string SelfChecking2 { get; set; }

        /// <summary>
        /// 自检项3结果
        /// </summary>
        public string SelfChecking3 { get; set; }

        /// <summary>
        /// 自检项4结果
        /// </summary>
        public string SelfChecking4 { get; set; }

        /// <summary>
        /// 吸光度数据（可能为波长-吸光度集合字符串）
        /// </summary>
        public string Abs { get; set; }

        /// <summary>
        /// 颜色空间参数L（明度）
        /// </summary>
        public string L { get; set; }

        /// <summary>
        /// 颜色空间参数A（红绿轴）
        /// </summary>
        public string A { get; set; }

        /// <summary>
        /// 颜色空间参数B（黄蓝轴）
        /// </summary>
        public string B { get; set; }

        /// <summary>
        /// 补偿值（保留3位小数）
        /// </summary>
        public decimal? Compensate { get; set; }

        /// <summary>
        /// 吸光度关联代码
        /// </summary>
        public string AbsCode { get; set; }

        /// <summary>
        /// 标准E1数据（可能为光谱或浓度标准数据）
        /// </summary>
        public string StandardE1 { get; set; }

        /// <summary>
        /// 标准E2数据
        /// </summary>
        public string StandardE2 { get; set; }

        /// <summary>
        /// 实际E1数据
        /// </summary>
        public string E1 { get; set; }

        /// <summary>
        /// 实际E2数据
        /// </summary>
        public string E2 { get; set; }

        /// <summary>
        /// 波长列表数据（可能为波长范围或关键波长集合）
        /// </summary>
        public string WL { get; set; }

        /// <summary>
        /// 瓶具状态（具体含义结合业务，如0=空闲，1=使用中，2=报废等）
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 首次滴液保留量
        /// </summary>
        public int? DripReserveFirst { get; set; }

        /// <summary>
        /// 注射器清洗间隔（单位可能为次数或时间）
        /// </summary>
        public int? WashSyringeSpan { get; set; }

        /// <summary>
        /// 上次清洗时间（秒级精度）
        /// </summary>
        public DateTime? LastWashTime { get; set; }
    }

    /// <summary>
    /// 对应brew_run_table表的实体类（酿造/运行过程记录）
    /// </summary>
    public class BrewRunTable
    {
        /// <summary>
        /// 自增主键ID（非空）
        /// </summary>
        public int MyID { get; set; }

        /// <summary>
        /// 运行时间（秒级精度）
        /// </summary>
        public DateTime? MyDateTime { get; set; }

        /// <summary>
        /// 运行状态（如"运行中"、"已完成"、"异常"等，长度限制10字符）
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 运行详情信息（如操作日志、异常描述等，支持长文本）
        /// </summary>
        public string Info { get; set; }
    }

    /// <summary>
    /// 对应brewing_process表的实体类（酿造工艺步骤信息）
    /// </summary>
    public class BrewingProcess
    {
        /// <summary>
        /// 步骤编号（标识工艺中的顺序步骤）
        /// </summary>
        public int? StepNum { get; set; }

        /// <summary>
        /// 工艺名称（如"混合"、"加热"、"静置"等具体工艺环节）
        /// </summary>
        public string TechnologyName { get; set; }

        /// <summary>
        /// 比例或时间参数（根据工艺类型存储比例值或时间值，单位需结合业务定义）
        /// </summary>
        public int? ProportionOrTime { get; set; }

        /// <summary>
        /// 酿造/配制代码（关联具体的酿造批次或配方）
        /// </summary>
        public string BrewingCode { get; set; }

        /// <summary>
        /// 比例值（可能表示原料配比、浓度比例等）
        /// </summary>
        public int? Ratio { get; set; }
    }

    /// <summary>
    /// 对应cup_details表的实体类（杯具详细信息，含状态、工艺参数等）
    /// </summary>
    public class CupDetails
    {
        /// <summary>
        /// 杯号（标识具体的杯具）
        /// </summary>
        public int? CupNum { get; set; }

        /// <summary>
        /// 是否固定（0=不固定，1=固定，tinyint对应byte）
        /// </summary>
        public byte? IsFixed { get; set; }

        /// <summary>
        /// 是否启用（0=禁用，1=启用，tinyint对应byte）
        /// </summary>
        public byte? Enable { get; set; }

        /// <summary>
        /// 是否正在使用（0=未使用，1=使用中，tinyint对应byte）
        /// </summary>
        public byte? IsUsing { get; set; }

        /// <summary>
        /// 杯具状态描述（如"空闲"、"运行中"、"故障"等）
        /// </summary>
        public string Statues { get; set; }

        /// <summary>
        /// 配方代码（关联对应的配方信息）
        /// </summary>
        public string FormulaCode { get; set; }

        /// <summary>
        /// 染色代码（关联染色工艺）
        /// </summary>
        public string DyeingCode { get; set; }

        /// <summary>
        /// 开始时间（秒级精度）
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 设置温度（保留1位小数）
        /// </summary>
        public decimal? SetTemp { get; set; }

        /// <summary>
        /// 实际温度（保留1位小数）
        /// </summary>
        public decimal? RealTemp { get; set; }

        /// <summary>
        /// 总重量（保留2位小数）
        /// </summary>
        public decimal? TotalWeight { get; set; }

        /// <summary>
        /// 当前步骤编号（可能为字符串格式的步骤标识）
        /// </summary>
        public string StepNum { get; set; }

        /// <summary>
        /// 总步骤数（可能为字符串格式的总步骤标识）
        /// </summary>
        public string TotalStep { get; set; }

        /// <summary>
        /// 当前工艺名称（如"加热"、"搅拌"等）
        /// </summary>
        public string TechnologyName { get; set; }

        /// <summary>
        /// 步骤开始时间（秒级精度）
        /// </summary>
        public DateTime? StepStartTime { get; set; }

        /// <summary>
        /// 设置时间（单位可能为分钟或秒）
        /// </summary>
        public int? SetTime { get; set; }

        /// <summary>
        /// 记录索引（用于关联历史数据）
        /// </summary>
        public int? RecordIndex { get; set; }

        /// <summary>
        /// 协作标识（非空，可能表示关联的设备组或协作编号）
        /// </summary>
        public int Cooperate { get; set; }

        /// <summary>
        /// 杯具类型（如1=标准杯，2=专用杯等）
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// 盖子状态（如0=打开，1=关闭，2=异常）
        /// </summary>
        public int? CoverStatus { get; set; }

        /// <summary>
        /// 接收时间（秒级精度，可能表示任务接收时间）
        /// </summary>
        public DateTime? ReceptionTime { get; set; }

        /// <summary>
        /// 染色类型（如1=常规染色，2=快速染色等）
        /// </summary>
        public int? DyeType { get; set; }
    }

    /// <summary>
    /// 对应数据库表[dbo].[drop_details]的实体类
    /// </summary>
    public class DropDetails
    {
        /// <summary>
        /// 批次名称
        /// </summary>
        public string BatchName { get; set; }

        /// <summary>
        /// 杯号
        /// </summary>
        public int? CupNum { get; set; }

        /// <summary>
        /// 配方代码
        /// </summary>
        public string FormulaCode { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public int? VersionNum { get; set; }

        /// <summary>
        /// 索引号
        /// </summary>
        public int? IndexNum { get; set; }

        /// <summary>
        /// 辅助材料代码
        /// </summary>
        public string AssistantCode { get; set; }

        /// <summary>
        /// 配方剂量
        /// </summary>
        public double? FormulaDosage { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitOfAccount { get; set; }

        /// <summary>
        /// 瓶号
        /// </summary>
        public int? BottleNum { get; set; }

        /// <summary>
        /// 设置浓度
        /// </summary>
        public double? SettingConcentration { get; set; }

        /// <summary>
        /// 实际浓度
        /// </summary>
        public double? RealConcentration { get; set; }

        /// <summary>
        /// 辅助材料名称
        /// </summary>
        public string AssistantName { get; set; }

        /// <summary>
        /// 目标滴加重量
        /// </summary>
        public decimal? ObjectDropWeight { get; set; }

        /// <summary>
        /// 实际滴加重量
        /// </summary>
        public decimal? RealDropWeight { get; set; }

        /// <summary>
        /// 瓶子选择
        /// </summary>
        public byte? BottleSelection { get; set; }

        /// <summary>
        /// 最小重量
        /// </summary>
        public int? MinWeight { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public byte? Finish { get; set; }

        /// <summary>
        /// 目标粉末重量
        /// </summary>
        public string ObjectPowderWeight { get; set; }

        /// <summary>
        /// 实际粉末重量
        /// </summary>
        public string RealPowderWeight { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public byte? IsShow { get; set; }

        /// <summary>
        /// 是否滴加
        /// </summary>
        public byte? IsDrop { get; set; }

        /// <summary>
        /// 冲泡数据时间
        /// </summary>
        public DateTime? BrewingData { get; set; }

        /// <summary>
        /// 需要的脉冲数
        /// </summary>
        public int? NeedPulse { get; set; }

        /// <summary>
        /// 标准误差
        /// </summary>
        public string StandError { get; set; }
    }


    /// <summary>
    /// 对应数据库表[dbo].[drop_head]的实体类
    /// </summary>
    public class DropHead
    {
        /// <summary>
        /// 批次名称
        /// </summary>
        public string BatchName { get; set; }

        /// <summary>
        /// 杯号
        /// </summary>
        public int? CupNum { get; set; }

        /// <summary>
        /// 配方代码
        /// </summary>
        public string FormulaCode { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public int? VersionNum { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 配方名称
        /// </summary>
        public string FormulaName { get; set; }

        /// <summary>
        /// 布料类型
        /// </summary>
        public string ClothType { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// 加水选择（0/1）
        /// </summary>
        public byte? AddWaterChoose { get; set; }

        /// <summary>
        /// 复合板选择（0/1）
        /// </summary>
        public byte? CompoundBoardChoose { get; set; }

        /// <summary>
        /// 布料重量
        /// </summary>
        public double? ClothWeight { get; set; }

        /// <summary>
        /// 浴比
        /// </summary>
        public double? BathRatio { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public double? TotalWeight { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 杯代码
        /// </summary>
        public string CupCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 目标加水量
        /// </summary>
        public decimal? ObjectAddWaterWeight { get; set; }

        /// <summary>
        /// 实际加水量
        /// </summary>
        public decimal? RealAddWaterWeight { get; set; }

        /// <summary>
        /// 试管目标加水量
        /// </summary>
        public string TestTubeObjectAddWaterWeight { get; set; }

        /// <summary>
        /// 试管实际加水量
        /// </summary>
        public string TestTubeRealAddWaterWeight { get; set; }

        /// <summary>
        /// 试管完成状态（0/1）
        /// </summary>
        public byte? TestTubeFinish { get; set; }

        /// <summary>
        /// 试管水量不足（0/1）
        /// </summary>
        public byte? TestTubeWaterLower { get; set; }

        /// <summary>
        /// 加水完成状态（0/1）
        /// </summary>
        public byte? AddWaterFinish { get; set; }

        /// <summary>
        /// 杯子完成状态（0/1）
        /// </summary>
        public byte? CupFinish { get; set; }

        /// <summary>
        /// 染色代码
        /// </summary>
        public string DyeingCode { get; set; }

        /// <summary>
        /// 步骤
        /// </summary>
        public int? Step { get; set; }

        /// <summary>
        /// 非脱水WR值
        /// </summary>
        public decimal? Non_AnhydrationWR { get; set; }

        /// <summary>
        /// 脱水WR值
        /// </summary>
        public decimal? AnhydrationWR { get; set; }

        /// <summary>
        /// 处理浴比
        /// </summary>
        public decimal? HandleBathRatio { get; set; }

        /// <summary>
        /// 处理参数1
        /// </summary>
        public int? Handle_Rev1 { get; set; }

        /// <summary>
        /// 处理参数2
        /// </summary>
        public int? Handle_Rev2 { get; set; }

        /// <summary>
        /// 处理参数3
        /// </summary>
        public int? Handle_Rev3 { get; set; }

        /// <summary>
        /// 处理参数4
        /// </summary>
        public int? Handle_Rev4 { get; set; }

        /// <summary>
        /// 处理参数5
        /// </summary>
        public int? Handle_Rev5 { get; set; }

        /// <summary>
        /// 描述字符
        /// </summary>
        public string DescribeChar { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 阶段
        /// </summary>
        public string Stage { get; set; }

        /// <summary>
        /// 英文描述字符
        /// </summary>
        public string DescribeChar_EN { get; set; }

        /// <summary>
        /// 处理浴比列表
        /// </summary>
        public string HandleBRList { get; set; }

        /// <summary>
        /// 布料数量
        /// </summary>
        public byte? ClothNum { get; set; }

        /// <summary>
        /// 水量标准误差
        /// </summary>
        public string WaterStandError { get; set; }

        /// <summary>
        /// 是否自动进入（0/1）
        /// </summary>
        public int? IsAutoIn { get; set; }

        /// <summary>
        /// 重染性
        /// </summary>
        public string Recoloration { get; set; }

        /// <summary>
        ///  vat编号
        /// </summary>
        public string VatNumber { get; set; }

        /// <summary>
        /// 染色代码备注
        /// </summary>
        public string DyeingCodeRemark { get; set; }
    }

    /// <summary>
    /// 对应数据库表 [dbo].[dye_details] 的实体类（染色详情表）
    /// </summary>
    public class DyeDetails
    {
        /// <summary>
        /// 批次名称
        /// </summary>
        public string BatchName { get; set; }
        /// <summary>
        /// 杯号
        /// </summary>
        public int? CupNum { get; set; }
        /// <summary>
        /// 配方代码
        /// </summary>
        public string FormulaCode { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public int? VersionNum { get; set; }
        /// <summary>
        /// 辅助材料代码
        /// </summary>
        public string AssistantCode { get; set; }
        /// <summary>
        /// 配方剂量
        /// </summary>
        public double? FormulaDosage { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitOfAccount { get; set; }
        /// <summary>
        /// 瓶号
        /// </summary>
        public int? BottleNum { get; set; }
        /// <summary>
        /// 设置浓度
        /// </summary>
        public double? SettingConcentration { get; set; }
        /// <summary>
        /// 实际浓度
        /// </summary>
        public double? RealConcentration { get; set; }
        /// <summary>
        /// 辅助材料名称
        /// </summary>
        public string AssistantName { get; set; }
        /// <summary>
        /// 目标滴加重量（精度：3 位小数）
        /// </summary>
        public decimal? ObjectDropWeight { get; set; }
        /// <summary>
        /// 实际滴加重量（精度：3 位小数）
        /// </summary>
        public decimal? RealDropWeight { get; set; }
        /// <summary>
        /// 瓶子选择（0/1，代表是否选中）
        /// </summary>
        public byte? BottleSelection { get; set; }
        /// <summary>
        /// 最小重量（小整数范围：0-255）
        /// </summary>
        public byte? MinWeight { get; set; }
        /// <summary>
        /// 完成状态（0/1，0 = 未完成，1 = 已完成）
        /// </summary>
        public byte? Finish { get; set; }
        /// <summary>
        /// 步骤编号
        /// </summary>
        public int? StepNum { get; set; }
        /// <summary>
        /// 工艺名称（最大长度 12）
        /// </summary>
        public string TechnologyName { get; set; }
        /// <summary>
        /// 温度（精度：2 位小数）
        /// </summary>
        public decimal? Temp { get; set; }
        /// <summary>
        /// 升温速率（精度：2 位小数）
        /// </summary>
        public decimal? TempSpeed { get; set; }
        /// <summary>
        /// 时长（单位：秒 / 分钟，根据业务定义）
        /// </summary>
        public int? Time { get; set; }
        /// <summary>
        /// 目标加水量（精度：3 位小数）
        /// </summary>
        public decimal? ObjectWaterWeight { get; set; }
        /// <summary>
        /// 转子转速（单位：转 / 分钟）
        /// </summary>
        public int? RotorSpeed { get; set; }
        /// <summary>
        /// 超温次数
        /// </summary>
        public int? OvertempNum { get; set; }
        /// <summary>
        /// 超温时长（单位：秒 / 分钟，根据业务定义）
        /// </summary>
        public int? OvertempTime { get; set; }
        /// <summary>
        /// 编码（业务自定义编码）
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 开始时间（无小数秒精度）
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 完成时间（无小数秒精度）
        /// </summary>
        public DateTime? FinishTime { get; set; }
        /// <summary>
        /// 协同参数（业务自定义，如协同设备编号 / 协同步骤）
        /// </summary>
        public int? Cooperate { get; set; }
        /// <summary>
        /// 补偿值（精度：3 位小数，如重量补偿、浓度补偿）
        /// </summary>
        public decimal? Compensation { get; set; }
        /// <summary>
        /// 冲泡数据时间（高精度，含 7 位小数秒）
        /// </summary>
        public DateTime? BrewingData { get; set; }
        /// <summary>
        /// 接收时间（无小数秒精度）
        /// </summary>
        public DateTime? ReceptionTime { get; set; }
        /// <summary>
        /// 染色类型（业务自定义，如 1 = 酸性染色、2 = 碱性染色）
        /// </summary>
        public int? DyeType { get; set; }
        /// <summary>
        /// 需要的脉冲数
        /// </summary>
        public int? NeedPulse { get; set; }
        /// <summary>
        /// 选择状态（业务自定义，如 0 = 未选择、1 = 已选择）
        /// </summary>
        public int? Choose { get; set; }
        /// <summary>
        /// 加水完成状态（业务自定义，如 0 = 未完成、1 = 已完成）
        /// </summary>
        public int? WaterFinish { get; set; }
        /// <summary>
        /// 标准误差
        /// </summary>
        public string StandError { get; set; }
    }

    public class SQLServer
    {
        private string Con { get; set; }

        private SqlConnection m_Connection;

        bool b_isJustShowInfo = false;


        /// <summary>
        /// SQLServer配置参数
        /// </summary>
        public struct SQLServerCon
        {
            /// <summary>
            /// 数据库IP
            /// </summary>
            public string Server { get; set; }

            /// <summary>
            /// 端口号
            /// </summary>
            public string Port { get; set; }

            /// <summary>
            /// 数据库名称
            /// </summary>
            public string Database { get; set; }

            /// <summary>
            /// 用户名
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 密码
            /// </summary>
            public string Password { get; set; }
        }

        

        public void Update(AAndB item)
        {
            string sql = @"UPDATE dbo.AAndB 
                       SET XS = @XS, WC = @WC
                       WHERE SJ = @SJ ";

            using (var connection = new SqlConnection(Con))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@MB", item.MB);
                    command.Parameters.AddWithValue("@XS", item.XS ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@WC", item.WC ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SJ", item.SJ ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }

        public SQLServer(SQLServerCon s_SQLServerCon)
        {
            Con = "Server=" + s_SQLServerCon.Server + "," + s_SQLServerCon.Port +
                ";Database=" + s_SQLServerCon.Database +
                ";uid=" + s_SQLServerCon.UserName +
                ";pwd=" + s_SQLServerCon.Password;
        }

        public SQLServer(String  conPar)
        {
            Con = conPar;
        }

        /// <summary>
        /// 数据库打开
        /// </summary>
        public void Open()
        {
            m_Connection = new SqlConnection(Con);
            m_Connection.Open();
        }

        /// <summary>
        /// 数据库关闭
        /// </summary>
        public void Close()
        {
            if (m_Connection != null)
            {
                if (m_Connection.State == ConnectionState.Open)
                {
                    m_Connection.Close();
                    m_Connection.Dispose();
                }
            }
        }

        /// <summary>
        /// 插入播报内容
        /// </summary>
        /// <param name="sSql">sql语句</param>
        /// <returns></returns>
        public void InsertSpeechInfo(string sInfo)
        {
            sInfo = sInfo.Replace(",", ";");
            sInfo = sInfo.Replace("，", ";");
            DataTable dt = GetData("SELECT * FROM SpeechInfo WHERE Info = '" + sInfo + "'");

            if (dt.Rows.Count == 0)
            {
                string sSql = "Insert Into SpeechInfo(Time, Info,IsFinished) values('" + DateTime.Now.ToLongTimeString() + "','" + sInfo + "',0);";
                ReviseData(sSql);
            }
        }

        /// <summary>
        /// 插入播报内容
        /// </summary>
        /// <param name="sSql">sql语句</param>
        /// <returns></returns>
        public void SetShowInfo(bool b)
        {
            b_isJustShowInfo = b;
        }


        /// <summary>
        /// 删除播报内容
        /// </summary>
        /// <param name="sSql">sql语句</param>
        /// <returns></returns>
        public void DeleteSpeechInfo(string sInfo)
        {
            sInfo = sInfo.Replace(",", ";");
            sInfo = sInfo.Replace("，", ";");
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    DataTable dt = GetData("SELECT * FROM SpeechInfo WHERE Info = '" + sInfo + "'");

                    if (dt.Rows.Count == 0)
                    {
                        break;
                    }
                    if (Convert.ToInt16(dt.Rows[0]["IsFinished"]) == 1)
                    {
                        string sSql = "Delete from SpeechInfo where Info = '" + sInfo + "';";
                        ReviseData(sSql);
                    }
                    Thread.Sleep(1);
                }
            });

            thread.Start();

        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sSql">sql语句</param>
        /// <returns></returns>
        public DataTable GetData(string sSql)
        {
            lock (this)
            {
                DataTable dt = new DataTable();
                try
                {
                    Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sSql, m_Connection);

                    sqlDataAdapter.Fill(dt);
                    Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    Lib_Log.Log.writeLogException(ex + ":" + sSql);
                    return dt;
                }
            }
        }

        private List<string> GetCurrentCallPath()
        {
            var stackTrace = new StackTrace(true); // true表示捕获文件和行号信息
            var callPath = new List<string>();

            // 遍历堆栈帧（跳过当前方法自身）
            foreach (var frame in stackTrace.GetFrames())
            {
                var method = frame.GetMethod();
                if (method == null) continue;

                // 方法全名：命名空间.类名.方法名
                var methodFullName = $"{method.DeclaringType?.Namespace}.{method.DeclaringType?.Name}.{method.Name}";
                callPath.Add(methodFullName);

                // 终止条件：到达程序入口（如Main方法）
                if (method.Name == "Main") break;
            }

            callPath.Reverse(); // 反转后从入口到当前方法
            return callPath;
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="sSql">sql语句</param>
        public void ReviseData(string sSql)
        {
            if (!b_isJustShowInfo)
            {
                //var callPath = GetCurrentCallPath();
                //Console.WriteLine("当前调用路径：");
                //Console.WriteLine(string.Join(" → ", callPath));

                lock (this)
                {
                    try
                    {
                        Open();
                        SqlCommand sqlCommand = new SqlCommand(sSql, m_Connection);
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Dispose();
                        Close();
                    }
                    catch (Exception ex)
                    {
                        Lib_Log.Log.writeLogException(ex + ":" + sSql);

                    }
                }
            }

        }

        /// <summary>
        /// 修改数据,单独输入配方时使用
        /// </summary>
        /// <param name="sSql">sql语句</param>
        public void ReviseData_show(string sSql)
        {
            lock (this)
            {
                try
                {
                    Open();
                    SqlCommand sqlCommand = new SqlCommand(sSql, m_Connection);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Dispose();
                    Close();
                }
                catch (Exception ex)
                {
                    Lib_Log.Log.writeLogException(ex + ":" + sSql);

                }
            }
        }



        /// <summary>
        /// </summary>
        /// <param name="s">过程温度数据</param>
        public void SetImage(string s, int CupNum, string BatchName)
        {
            lock (this)
            {
                try
                {
                    if (s == "")
                        return;
                    Open();
                    byte[] bytStr = System.Text.Encoding.Default.GetBytes(Base64Encrypt(s));
                    string sql = "Update history_head set ProcessData = @Image where BatchName = '" + BatchName + "' and CupNum = " + CupNum.ToString() + ";";

                    SqlCommand sqlCommand = new SqlCommand(sql, m_Connection);
                    sqlCommand.Parameters.Add("@Image", SqlDbType.Image);
                    sqlCommand.Parameters["@Image"].Value = bytStr;
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Dispose();
                    Close();
                }
                catch
                {

                }
            }
        }

        public string GetImage(int CupNum, string BatchName)
        {
            lock (this)
            {
                try
                {
                    Open();
                    string sql = "select * from history_head where BatchName = '" + BatchName + "' and CupNum = " + CupNum.ToString() + ";";
                    SqlCommand sqlCommand = new SqlCommand(sql, m_Connection);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    reader.Read();
                    if (reader["ProcessData"] is DBNull)
                        return "";
                    byte[] bytStr = (byte[])reader["ProcessData"];
                    string s = Base64Decrypt(System.Text.Encoding.Default.GetString(bytStr));
                    Close();
                    return s;
                }
                catch { return ""; }
            }
        }

        #region Base64加密解密
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public static string Base64Encrypt(string input)
        {
            return Base64Encrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encode">字符编码</param>
        /// <returns></returns>
        public static string Base64Encrypt(string input, Encoding encode)
        {
            return Convert.ToBase64String(encode.GetBytes(input));
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <returns></returns>
        public static string Base64Decrypt(string input)
        {
            return Base64Decrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public static string Base64Decrypt(string input, Encoding encode)
        {
            return encode.GetString(Convert.FromBase64String(input));
        }
        #endregion

        /// <summary>
        /// 插入运行表
        /// </summary>
        /// <param name="sName">字段名</param>
        /// <param name="sValues">内容</param>
        public void InsertRun(string sName, string sValues)
        {
            if (!b_isJustShowInfo)
            {
                Thread thread = new Thread(() =>
            {
                lock (this)
                {
                    try
                    {
                        DataTable dataTable = GetData("SELECT * FROM run_table order by MyID DESC;");
                        if (dataTable.Rows.Count > 0)
                        {
                            string sLast = string.Format("{0:d}", dataTable.Rows[0]["MyDate"]);
                            string sNow = string.Format("{0:d}", DateTime.Now);
                            if (sLast != sNow)
                                ReviseData("TRUNCATE TABLE run_table;");
                        }

                        string sSql = "INSERT INTO run_table ( MyDate, MyTime, " + sName + ")" +
                            " VALUES('" + String.Format("{0:d}", DateTime.Now) + "', '" +
                            String.Format("{0:T}", DateTime.Now) + "', '" + sValues + "');";
                        ReviseData(sSql);
                    }
                    catch (Exception ex)
                    {
                        Lib_Log.Log.writeLogException(ex);

                    }


                }
            });
                thread.Start();
            }
        }



    }
}