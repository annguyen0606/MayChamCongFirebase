using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MayChamCongV2
{
    public partial class Form1 : Form
    {
        DataTable dsNhanVien = new DataTable();
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "",
            BasePath = "https://annguyenhoctap.firebaseio.com/"
        };

        IFirebaseClient client;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dsNhanVien.Columns.Add("UID");
            dsNhanVien.Columns.Add("Name");
            dsNhanVien.Columns.Add("Birth");

            client = new FireSharp.FirebaseClient(config);
            if(client != null)
            {
                MessageBox.Show("Kết nối với Server thành công","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kết nối với Server thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnRegistration_Click(object sender, EventArgs e)
        {
            FirebaseResponse rspon = await client.GetTaskAsync("Counter/node");
            Counter_class get = rspon.ResultAs<Counter_class>();

            var data = new Data
            {
                UID = txbUID.Text,
                Name = txtNameCard.Text,
                BirthDay = txbBirthOfDate.Text
            };
            //SetResponse response = await client.SetTaskAsync("DanhSachNhanVien/" + txbUID.Text, data);
            SetResponse response = await client.SetTaskAsync("DanhSachNhanVien/" + Convert.ToInt32(get.cnt) + 1, data);
            Data result = response.ResultAs<Data>();
            if (result.UID.Trim().Equals(txbUID.Text.Trim()))
            {
                MessageBox.Show("Đăng ký thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbUID.Text = "";
                txtNameCard.Text = "";
                txbBirthOfDate.Text = "";
            }

            var obj = new Counter_class
            {
                cnt = (int.Parse(get.cnt) + 1).ToString()
            };

            SetResponse response1 = await client.SetTaskAsync("Counter/node", obj);
            
        }

        private async void BtnGetData_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetTaskAsync("Lop1A/");
            String abc = response.Body;
            JsonReader reader = Json
            JArray ad = JArray.Parse(abc);
            MessageBox.Show(abc);
            //FirebaseResponse response = await client.GetTaskAsync("Counter/node");
            //Counter_class obj = response.ResultAs<Counter_class>();
            //int cnt = int.Parse(obj.cnt.ToString());
            //for(int i = 1; i <= cnt; i++)
            //{
            //    try
            //    {
            //        FirebaseResponse response1;
            //        if (i < 10)
            //        {
            //            response1  = await client.GetTaskAsync("DanhSachNhanVien/0" + i);
            //        }
            //        else
            //        {
            //            response1 = await client.GetTaskAsync("DanhSachNhanVien/" + i);
            //        }
            //        Data data = response1.ResultAs<Data>();

            //        DataRow row = dsNhanVien.NewRow();
            //        row["UID"] = data.UID;
            //        row["Name"] = data.Name;
            //        row["Birth"] = data.BirthDay;
            //        dsNhanVien.Rows.Add(row);

            //    }catch(Exception ex)
            //    {

            //    }
            //}
            //dataGridView1.DataSource = dsNhanVien;
        }
    }
}
