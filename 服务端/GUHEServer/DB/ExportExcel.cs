using GHIBMS.Common;
using GHIBMS.Interface;
using System;
using System.Windows.Forms;


namespace GHIBMS.Server
{
    public class ExportExcel
    {
        public static void Export(string tableName, string saveFileName)
        {
            try
            {
                int n = 0;
                foreach (IChannel cha in Rtdb.ChanList)
                    foreach (IController con in cha.ConList)
                        foreach (IVariable var in con.VarList)
                        {
                            n++;
                        }
                //string saveFileName = "";
                //SaveFileDialog saveDialog = new SaveFileDialog();
                //saveDialog.DefaultExt = "xls";
                //saveDialog.Filter = "Excel文件|*.xls";
                //saveDialog.FileName = tableName;
                //saveDialog.ShowDialog();
                //saveFileName = saveDialog.FileName;
                //if (saveFileName.IndexOf(":") < 0)
                //{
                //    //MessageBox.Show("文件路径有误，取消导出操作！", "提示信息：", MessageBoxButtons.OK, MessageBoxIcon.Asterisk,MessageBoxOptions.DefaultDesktopOnly);
                //    MessageBox.Show("文件路径有误，取消导出操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                //    return;
                //}
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                object miss = System.Reflection.Missing.Value;

                if (xlApp == null)
                {
                    //MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel！", "出错了：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    return;
                }


                Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
                Microsoft.Office.Interop.Excel.Range range;


                //总行数和总列数
                long totalRowCount = n;
                long totalColCount = 7;

                worksheet.Cells[1, 1] = "ID";
                worksheet.Cells[1, 2] = "名称";
                worksheet.Cells[1, 3] = "描述";
                worksheet.Cells[1, 4] = "控制器";
                worksheet.Cells[1, 5] = "通道";
                worksheet.Cells[1, 6] = "控制器ID";
                worksheet.Cells[1, 7] = "通道ID";

                for (int i = 0; i < totalColCount; i++)
                {
                    range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                    range.Interior.ColorIndex = 15;
                    range.Font.Bold = true;
                }

                int r = 1;
                foreach (IChannel cha in Rtdb.ChanList)
                    foreach (IController con in cha.ConList)
                        foreach (IVariable var in con.VarList)
                        {
                            worksheet.Cells[r + 1, 1] = var.ID;
                            worksheet.Cells[r + 1, 2] = var.Name;
                            worksheet.Cells[r + 1, 3] = var.Description;
                            worksheet.Cells[r + 1, 4] = var.ControllerObject.Name;
                            worksheet.Cells[r + 1, 5] = var.ControllerObject.ChannelObject.Name;
                            worksheet.Cells[r + 1, 6] = var.ControllerObject.ID;
                            worksheet.Cells[r + 1, 7] = var.ControllerObject.ChannelObject.ID;
                            r++;
                        }
                range = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[n + 1, 7]];

                if (totalRowCount > 0)
                {
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic;
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;

                }

                if (totalColCount > 1)
                {
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic;
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;

                }

                if (range != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
                    range = null;
                }

                if (worksheet != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                    worksheet = null;
                }
                if (saveFileName != "")
                {
                    try
                    {
                        workbook.Saved = true;
                        System.Reflection.Missing missing = System.Reflection.Missing.Value;
                        workbook.SaveAs(saveFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, miss, miss, miss, miss,
                            Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, miss, miss, miss, miss, miss);
                        // MessageBox.Show("导出成功！", "提示信息：", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        MessageBox.Show("导出成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    }
                    catch
                    {
                        //MessageBox.Show("导出文件错误！请重试！", "出错了：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("导出文件错误！请重试！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    }

                }

                if (workbook != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    workbook = null;

                }

                if (workbooks != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbooks);
                    workbooks = null;

                }

                xlApp.Application.Workbooks.Close();

                xlApp.Quit();
                GC.Collect();
            }
            catch (Exception ex)
            {

                MessageBox.Show("导出文件错误！可能您的机子未安装Excel！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            }
        }


    }


}
