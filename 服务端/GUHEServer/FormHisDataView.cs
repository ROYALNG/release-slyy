using GHDatabase.Influxdb;
using GHDatabase.Mysql;
using GHIBMS.Common;
using InfluxData.Net.InfluxDb.Models.Responses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormHisDataView : Form
    {
        public FormHisDataView()
        {
            InitializeComponent();
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btnMysql_Click(object sender, EventArgs e)
        {
            
            MysqlHelper mysqlHelper = new MysqlHelper();
            string ConnectionString = $"server={ServerConfig.DbHost};user={ServerConfig.DbUser};database={ServerConfig.DbName};port={ServerConfig.DBPort};password={ServerConfig.DbPw};";
            try
            {
                this.dataGridView1.Rows.Clear();
                string datatime = dateTimePicker1.Value.ToString("yyyyMMdd");
                //mysqlHelper.Connection.Open();
                string sql = "SELECT * FROM gh_history"+ datatime;
                DataSet ds = mysqlHelper.ExecuteDataSet(ConnectionString, sql,null);
                //遍历一个表多行一列
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Console.WriteLine(row[0].ToString());
                    int id = pubFun.IsInt(row[0].ToString(),0);
                    string serverId = row["server"].ToString();
                    string chId = row["channel"].ToString();
                    string conId = row["controller"].ToString();
                    string varId = row["varKey"].ToString();
                    string value = row["value"].ToString();
                    DateTime time =pubFun.IsDate(row["time"].ToString(),DateTime.Now);
                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells[0].Value = id;
                    this.dataGridView1.Rows[index].Cells[1].Value = time;
                    this.dataGridView1.Rows[index].Cells[2].Value = Rtdb.GetChannelByID(chId).Name;
                    this.dataGridView1.Rows[index].Cells[3].Value = Rtdb.GetControllerByID(chId,chId).Name;
                    this.dataGridView1.Rows[index].Cells[4].Value = Rtdb.GetVariableByID(chId, conId,chId).Name;
                    this.dataGridView1.Rows[index].Cells[5].Value = value;
                }
                
               

            }
            catch(Exception ex) 
            {
                MessageBox.Show("数据库连接失败！");
            
            }

        }

        private  void btnInfluxDB_Click(object sender, EventArgs e)
        {
            try
            {
                InfluxdbHelper influxdb = InfluxdbHelper.GetInstance(ServerConfig.influxIP + ":" + ServerConfig.influxPort, ServerConfig.influxUsername, ServerConfig.influxPassword);
                string startTime = dateTimePicker1.Value.ToString("yyyy-MM-ddT00:00:00Z");
                string endTime = dateTimePicker1.Value.ToString("yyyy-MM-ddT23:59:59Z");
                string sql = $"SELECT * FROM gh_history WHERE  time >= '{startTime}' and time <= '{endTime}'  tz('Asia/Shanghai')";

                Serie serie = influxdb.QuerySync(sql, ServerConfig.influxName);

                int itime = getColumnIndex(serie, "time");
                int iserverId = getColumnIndex(serie,"server");
                int ichId = getColumnIndex(serie, "channel");
                int iconId = getColumnIndex(serie, "controller");
                int ivarId = getColumnIndex(serie, "varKey");
                int ivalue = getColumnIndex(serie, "value");
                
                int i = 0;

                foreach (List<object> list in serie.Values)
                {
                    i++;
                    string serverId =list[iserverId].ToString();
                    string chId = list[ichId].ToString();
                    string conId = list[iconId].ToString();
                    string varId = list[ivarId].ToString();
                    string value = list[ivalue].ToString();
                    DateTime time = pubFun.IsDate(list[itime].ToString(), DateTime.Now);
                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells[0].Value = i.ToString();
                    this.dataGridView1.Rows[index].Cells[1].Value = time;
                    this.dataGridView1.Rows[index].Cells[2].Value = Rtdb.GetChannelByID(chId).Name;
                    this.dataGridView1.Rows[index].Cells[3].Value = Rtdb.GetControllerByID(chId, chId).Name;
                    this.dataGridView1.Rows[index].Cells[4].Value = Rtdb.GetVariableByID(chId, conId, chId).Name;
                    this.dataGridView1.Rows[index].Cells[5].Value = value;

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        int getColumnIndex(Serie serie,string name)
        {
             for (int i=0;i<serie.Columns.Count;i++)
            {
                if(serie.Columns[i]==name)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
