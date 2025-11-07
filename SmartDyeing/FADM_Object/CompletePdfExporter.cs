using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Font;
using iText.IO.Font;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data;

namespace PdfTableExportTool
{
    /// <summary>
    /// PDF表格导出工具，支持中文显示和自动换行
    /// </summary>
    public class PdfTableExporter
    {
        // 字体对象（支持中文）
        private PdfFont _chineseFont;
        // 字体加载状态
        private bool _isFontLoadedSuccessfully;

        /// <summary>
        /// 初始化导出工具，自动加载中文字体
        /// </summary>
        public PdfTableExporter()
        {
            LoadChineseFont();
        }

        /// <summary>
        /// 加载支持中文的字体
        /// </summary>
        private void LoadChineseFont()
        {
            // 常见中文字体路径列表（Windows和Mac系统）
            var fontPaths = new Dictionary<string, string>
            {
                //{"黑体", "C:/Windows/Fonts/simhei.ttf"},
                {"宋体", "C:/Windows/Fonts/simsun.ttc,0"},
                {"微软雅黑", "C:/Windows/Fonts/msyh.ttf"},
                {"楷体", "C:/Windows/Fonts/simkai.ttf"},
                {"苹方（Mac）", "/Library/Fonts/PingFang.ttc"},
                {"宋体（Mac）", "/System/Library/Fonts/Songti.ttc"}
            };

            try
            {
                // 尝试加载字体
                foreach (KeyValuePair<string, string> font in fontPaths)
                {
                    string fontName = font.Key;
                    string path = font.Value;

                    if (File.Exists(path))
                    {
                        _chineseFont = PdfFontFactory.CreateFont(
                            path,
                            PdfEncodings.IDENTITY_H,
                            PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED
                        );
                        _isFontLoadedSuccessfully = true;
                        Console.WriteLine($"成功加载字体: {fontName} ({path})");
                        return;
                    }
                }

                // 未找到字体时使用默认字体
                _chineseFont = PdfFontFactory.CreateFont();
                Console.WriteLine("警告: 未找到中文字体，使用默认字体（可能无法正常显示中文）");
            }
            catch (Exception ex)
            {
                // 字体加载失败时降级处理
                _chineseFont = PdfFontFactory.CreateFont();
                _isFontLoadedSuccessfully = false;
                Console.WriteLine($"字体加载错误: {ex.Message}，将使用默认字体");
            }
        }

        /// <summary>
        /// 导出表格数据到PDF文件
        /// </summary>
        /// <param name="filePath">PDF保存路径</param>
        /// <param name="tableData">表格数据（二维列表）</param>
        /// <param name="columnWidths">列宽比例（总和建议为100）</param>
        public void Export(string filePath, List<List<string>> tableData, float[] columnWidths)
        {
            // 验证输入参数
            ValidateInput(tableData, columnWidths);

            // 确保输出目录存在
            EnsureDirectoryExists(filePath);

            // 创建PDF文档
            using (var writer = new PdfWriter(filePath))
            using (var pdfDoc = new PdfDocument(writer))
            using (var document = new Document(pdfDoc))
            {
                // 配置文档
                document.SetMargins(40, 40, 40, 40); // 页面边距

                // 创建表格
                var table = CreateTable(columnWidths);

                // 添加表格内容
                AddTableContent(table, tableData);

                // 将表格添加到文档
                document.Add(table);
            }

            // 验证输出结果
            VerifyOutputFile(filePath);
            Console.WriteLine($"PDF已成功导出至: {Path.GetFullPath(filePath)}");
        }

        /// <summary>
        /// 验证输入参数的有效性
        /// </summary>
        private void ValidateInput(List<List<string>> tableData, float[] columnWidths)
        {
            if (tableData == null || tableData.Count == 0)
                throw new ArgumentException("表格数据不能为空", nameof(tableData));

            if (columnWidths == null || columnWidths.Length == 0)
                throw new ArgumentException("列宽设置不能为空", nameof(columnWidths));

            int headerColumnCount = tableData[0].Count;
            if (columnWidths.Length != headerColumnCount)
                throw new ArgumentException($"列宽数组长度({columnWidths.Length})与表头列数({headerColumnCount})不匹配");

            for (int i = 0; i < tableData.Count; i++)
            {
                if (tableData[i].Count != headerColumnCount)
                    throw new ArgumentException($"第{i + 1}行数据列数({tableData[i].Count})与表头列数({headerColumnCount})不匹配");
            }
        }

        /// <summary>
        /// 确保输出目录存在
        /// </summary>
        private void EnsureDirectoryExists(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// 创建表格并设置基本样式
        /// </summary>
        private Table CreateTable(float[] columnWidths)
        {
            return new Table(UnitValue.CreatePercentArray(columnWidths))
                .SetWidth(UnitValue.CreatePercentValue(100)) // 表格宽度100%
                .SetFixedLayout() // 固定布局，确保列宽比例生效
                .SetMarginBottom(20); // 表格底部边距
        }

        /// <summary>
        /// 向表格添加内容（表头和数据行）
        /// </summary>
        private void AddTableContent(Table table, List<List<string>> tableData)
        {
            for (int rowIndex = 0; rowIndex < tableData.Count; rowIndex++)
            {
                var rowData = tableData[rowIndex];
                for (int colIndex = 0; colIndex < rowData.Count; colIndex++)
                {
                    // 创建带自动换行的段落
                    var paragraph = new Paragraph(rowData[colIndex])
                        .SetFont(_chineseFont)
                        .SetFontSize(10)
                        .SetMultipliedLeading(1.2f); // 行间距，增强可读性

                    // 创建单元格
                    var cell = new Cell()
                        .Add(paragraph)
                        .SetPadding(6) // 单元格内边距
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE);

                    //// 设置表头样式
                    //if (rowIndex == 0)
                    //{
                    //    cell.SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
                    //        .SetFontWeight(FontWeight.BOLD);
                    //}
                    //// 隔行变色
                    //else if (rowIndex % 2 == 0)
                    //{
                    //    cell.SetBackgroundColor(iText.Kernel.Colors.ColorConstants.WHITESMOKE);
                    //}

                    table.AddCell(cell);
                }
            }
        }

        /// <summary>
        /// 验证输出文件是否有效
        /// </summary>
        private void VerifyOutputFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("PDF文件生成失败", filePath);

            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Length <= 0)
                throw new Exception("生成的PDF文件为空");
        }

        /// <summary>
        /// 示例用法
        /// </summary>
        public  void Demo(DataTable dt, string outputPath)
        {
            try
            {
                var exporter = new PdfTableExporter();

                // 准备示例数据
                var data = new List<List<string>>
                {
                    // 表头
                    new List<string> { "日期", "配方代码", "布种", "配方名称", "自动", "备注", "操作员" },
                    
                    
                };

                foreach (DataRow row in dt.Rows)
                {
                    List<string> list = new List<string>();
                    DateTime date = DateTime.Parse(row[0].ToString());
                    list.Add(date.ToString("MM/dd"));
                    list.Add(row[1].ToString());
                    list.Add(row[2].ToString());
                    list.Add(row[3].ToString());
                    list.Add(row[4].ToString() == "1"? "√":"");
                    list.Add(row[5].ToString());
                    list.Add(row[6].ToString());
                    data.Add(list);
                }

                // 列宽比例（总和100）
                float[] columnWidths = { 8, 18, 18,18,7, 20, 11 };

                // 导出PDF
                exporter.Export(outputPath, data, columnWidths);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"导出失败: {ex.Message}");
            }
        }
    }

   
}
