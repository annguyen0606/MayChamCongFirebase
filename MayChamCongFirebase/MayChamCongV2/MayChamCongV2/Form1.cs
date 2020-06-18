using Firebase.Database;
using Firebase.Database.Query;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace MayChamCongV2
{
    public partial class Form1 : Form
    {
        UdpClient udpClient = null;
        IPEndPoint RemoteIP = null;
        String dataUID = "";
        String pathDuLieuDiemDanh = "Conek/DuLieuDiemDanh/";
        String pathDanhSachNhanVien = "Conek/DanhSachNhanVien/";
        String pathServer = "https://annguyenhoctap.firebaseio.com/";
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
            udpClient = new UdpClient(6688);
            try
            {
                udpClient.BeginReceive(new AsyncCallback(rev), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {
                MessageBox.Show("Kết nối với Server thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kết nối với Server thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void rev(IAsyncResult res)
        {
            try
            {   //Receive data
                RemoteIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] received = udpClient.EndReceive(res, ref RemoteIP);
                dataUID = Encoding.UTF8.GetString(received);
                this.Invoke(new EventHandler(GetIDTag));
            }
            catch (ObjectDisposedException)
            {
                return;
            }

            udpClient.BeginReceive(new AsyncCallback(rev), null);
        }
        private void GetIDTag(object sender, EventArgs e)
        {
            txbUID.Text = dataUID;
        }
        private async void BtnRegistration_Click(object sender, EventArgs e)
        {
            DisableButton();
            if (String.IsNullOrEmpty(txbUID.Text))
            {
                MessageBox.Show("Chưa chạm thẻ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EnableButton();
                return;
            }
            FirebaseResponse response1;
            response1 = await client.GetTaskAsync(pathDanhSachNhanVien + txbUID.Text);
            if (response1.Body == "null")
            {
                var data = new Data
                {
                    UID = txbUID.Text,
                    Name = txtNameCard.Text,
                    BirthDay = txbBirthOfDate.Text,
                    SDT = txbSDT.Text,
                    CMND = txbCMND.Text,
                    MST = txbMST.Text,
                    BHXH = txbBHXH.Text,
                    NBDLV = txbNBDLV.Text,
                    statusWork = "ON"
                };
                SetResponse response = await client.SetTaskAsync(pathDanhSachNhanVien + txbUID.Text, data);
                Data result = response.ResultAs<Data>();
                if (result.UID.Trim().Equals(txbUID.Text.Trim()))
                {
                    MessageBox.Show("Đăng ký thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txbUID.Text = "";
                    txtNameCard.Text = "";
                    txbBirthOfDate.Text = "";
                    txbSDT.Text = "";
                    txbCMND.Text = "";
                    txbMST.Text = "";
                    txbBHXH.Text = "";
                    txbNBDLV.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Tài khoản đã tồn tài", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            EnableButton();
        }

        private async void BtnGetData_Click(object sender, EventArgs e)
        {
            DisableButton();
            DataTable dsNhanVien = new DataTable();
            dsNhanVien.Columns.Add("UID");
            dsNhanVien.Columns.Add("Name");
            dsNhanVien.Columns.Add("Birth");
            dsNhanVien.Columns.Add("Ngay");
            dsNhanVien.Columns.Add("ThoiGianVao");
            dsNhanVien.Columns.Add("ThoiGianRa");
            dsNhanVien.Columns.Add("SoPhut");
            dsNhanVien.Columns.Add("GhiChu");

            dataGridView1.DataSource = new DataTable();
            ArrayList danhSachNhanVien = new ArrayList();
            var firebase1 = new FirebaseClient(pathServer);
            var duLieunhanvien = await firebase1.Child(pathDanhSachNhanVien)
                                .OrderByKey()
                                .OnceAsync<KeyIDTagData>();
            foreach (var anc in duLieunhanvien)
            {
                danhSachNhanVien.Add(anc.Key);
            }
            foreach (String idTag in danhSachNhanVien)
            {
                try
                {
                    FirebaseResponse response;
                    response = await client.GetTaskAsync(pathDanhSachNhanVien + idTag.Trim());
                    Data data = response.ResultAs<Data>();
                    if (data.statusWork.Equals("ON"))
                    {
                        DataRow row = dsNhanVien.NewRow();
                        row["UID"] = data.UID;
                        row["Name"] = data.Name;
                        row["Birth"] = data.BirthDay;
                        row["Ngay"] = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                        var firebase = new FirebaseClient(pathServer);
                        var duLieuChamCong = await firebase.Child(pathDuLieuDiemDanh + data.UID + "/" + dateTimePicker1.Value.ToString("yyyy-MM-dd"))
                                            .OrderByKey()
                                            .OnceAsync<String>();
                        switch (duLieuChamCong.Count)
                        {
                            case 0:
                                row["ThoiGianVao"] = " ";
                                row["ThoiGianRa"] = " ";
                                row["SoPhut"] = " ";
                                row["GhiChu"] = "Không làm việc";
                                break;
                            case 1:
                                foreach (var anc in duLieuChamCong)
                                {
                                    string[] ijk = anc.Object.Split(',');
                                    row["ThoiGianVao"] = ijk[0];
                                    row["ThoiGianRa"] = " ";
                                    if (int.Parse(ijk[1].ToString()) > 0)
                                    {
                                        row["SoPhut"] = ijk[1];
                                        row["GhiChu"] = "Không Check Out, Đi Muộn";
                                    }
                                    else
                                    {
                                        row["SoPhut"] = " ";
                                        row["GhiChu"] = "Không Check Out";
                                    }
                                }
                                break;
                            default:
                                int i = 0;
                                foreach (var anc in duLieuChamCong)
                                {
                                    if (i == 0)
                                    {
                                        string[] ijk = anc.Object.Split(',');
                                        row["ThoiGianVao"] = ijk[0];
                                        if (int.Parse(ijk[1].ToString()) > 0)
                                        {
                                            row["SoPhut"] = ijk[1];
                                            row["GhiChu"] = "Đi Muộn";
                                        }
                                        else
                                        {
                                            row["SoPhut"] = " ";
                                            row["GhiChu"] = " ";
                                        }
                                    }
                                    else
                                    {
                                        string[] ijk = anc.Object.Split(',');
                                        row["ThoiGianRa"] = ijk[0];
                                    }
                                    i++;
                                }
                                break;
                        }
                        dsNhanVien.Rows.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dataGridView1.DataSource = dsNhanVien;
            }
            EnableButton();
        }

        class DataDiemDanhNhanVien
        {
            public string UID { get; set; }
            public string NgayChamThe { get; set; }
            public string ThoiGianChamThe { get; set; }
            public string SoPhutMuon { get; set; }
            public string GhiChu { get; set; }
        }

        private async void BtnStatisticsMonth_Click(object sender, EventArgs e)
        {
            DisableButton();
            DataTable dsNhanVienDiemDanh = LayKhoangThoiGianThongKe();
            if(dsNhanVienDiemDanh.Rows.Count <= 0)
            {
                dataGridView1.DataSource = new DataTable();
            }
            else
            {
                DataTable dsNhanVien = new DataTable();
                dsNhanVien.Columns.Add("UID");
                dsNhanVien.Columns.Add("Name");
                dsNhanVien.Columns.Add("Birth");
                dsNhanVien.Columns.Add("Ngay");
                dsNhanVien.Columns.Add("ThoiGianVao");
                dsNhanVien.Columns.Add("ThoiGianRa");
                dsNhanVien.Columns.Add("SoPhut");
                dsNhanVien.Columns.Add("GhiChu");

                dataGridView1.DataSource = new DataTable();
                ArrayList danhSachNhanVien = new ArrayList();
                var firebase1 = new FirebaseClient(pathServer);
                var duLieunhanvien = await firebase1.Child(pathDanhSachNhanVien)
                                    .OrderByKey()
                                    .OnceAsync<KeyIDTagData>();
                foreach (var anc in duLieunhanvien)
                {
                    danhSachNhanVien.Add(anc.Key);
                }
                foreach (String idTag in danhSachNhanVien)
                {
                    try
                    {
                        FirebaseResponse response;
                        response = await client.GetTaskAsync(pathDanhSachNhanVien + idTag.Trim());
                        Data data = response.ResultAs<Data>();
                        if (data.statusWork.Equals("ON"))
                        {
                            foreach (DataRow rows in dsNhanVienDiemDanh.Rows)
                            {
                                DataRow row = dsNhanVien.NewRow();
                                row["UID"] = data.UID;
                                row["Name"] = data.Name;
                                row["Birth"] = data.BirthDay;
                                row["Ngay"] = rows["NgayThang"].ToString();
                                var firebase = new FirebaseClient(pathServer);
                                var duLieuChamCong = await firebase.Child(pathDuLieuDiemDanh + data.UID + "/" + rows["NgayThang"].ToString())
                                                    .OrderByKey()
                                                    .OnceAsync<String>();
                                switch (duLieuChamCong.Count)
                                {
                                    case 0:
                                        row["ThoiGianVao"] = " ";
                                        row["ThoiGianRa"] = " ";
                                        row["SoPhut"] = " ";
                                        row["GhiChu"] = "Không làm việc";
                                        break;
                                    case 1:
                                        foreach (var anc in duLieuChamCong)
                                        {
                                            string[] ijk = anc.Object.Split(',');
                                            row["ThoiGianVao"] = ijk[0];
                                            row["ThoiGianRa"] = " ";
                                            if (int.Parse(ijk[1].ToString()) > 0)
                                            {
                                                row["SoPhut"] = ijk[1];
                                                row["GhiChu"] = "Không Check Out, Đi Muộn";
                                            }
                                            else
                                            {
                                                row["SoPhut"] = " ";
                                                row["GhiChu"] = "Không Check Out";
                                            }
                                        }
                                        break;
                                    default:
                                        int i = 0;
                                        foreach (var anc in duLieuChamCong)
                                        {
                                            if (i == 0)
                                            {
                                                string[] ijk = anc.Object.Split(',');
                                                row["ThoiGianVao"] = ijk[0];
                                                if (int.Parse(ijk[1].ToString()) > 0)
                                                {
                                                    row["SoPhut"] = ijk[1];
                                                    row["GhiChu"] = "Đi Muộn";
                                                }
                                                else
                                                {
                                                    row["SoPhut"] = " ";
                                                    row["GhiChu"] = " ";
                                                }
                                            }
                                            else
                                            {
                                                string[] ijk = anc.Object.Split(',');
                                                row["ThoiGianRa"] = ijk[0];
                                            }
                                            i++;
                                        }
                                        break;
                                }
                                dsNhanVien.Rows.Add(row);
                            }
                        }
                }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    dataGridView1.DataSource = dsNhanVien;
                }
            }
            EnableButton();
        }

        DataTable LayKhoangThoiGianThongKe()
        {
            DataTable dsNgayThang = new DataTable();
            dsNgayThang.Columns.Add("NgayThang");
            int SoNgayThangThuNhat = DateTime.DaysInMonth(int.Parse(dateTimePicker2.Value.ToString("yyyy")), int.Parse(dateTimePicker2.Value.ToString("MM")));
            int SoThangTrongKhoang = int.Parse(dateTimePicker3.Value.ToString("MM")) - int.Parse(dateTimePicker2.Value.ToString("MM"));
            if (int.Parse(dateTimePicker2.Value.ToString("yyyy")) != int.Parse(dateTimePicker3.Value.ToString("yyyy")))
            {
                MessageBox.Show("Chọn sai thời gian thống kế\nChọn lại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return new DataTable();
            }
            if (SoThangTrongKhoang < 0)
            {
                MessageBox.Show("Chọn sai thời gian thống kế\nChọn lại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return new DataTable();
            }
            if (SoThangTrongKhoang == 0)
            {
                int SoNgayCheck = int.Parse(dateTimePicker3.Value.ToString("dd")) - int.Parse(dateTimePicker2.Value.ToString("dd"));
                if (SoNgayCheck < 0)
                {
                    MessageBox.Show("Chọn sai thời gian thống kế\nChọn lại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return new DataTable();
                }
                for (int i = int.Parse(dateTimePicker2.Value.ToString("dd")); i <= int.Parse(dateTimePicker3.Value.ToString("dd")); i++)
                {
                    DataRow row = dsNgayThang.NewRow();
                    if (i < 10)
                    {
                        row["NgayThang"] = dateTimePicker2.Value.ToString("yyyy-MM") + "-0" + i.ToString();
                        dsNgayThang.Rows.Add(row);
                    }
                    else
                    {
                        row["NgayThang"] = dateTimePicker2.Value.ToString("yyyy-MM") + "-" + i.ToString();
                        dsNgayThang.Rows.Add(row);
                    }
                }
            }
            else
            {
                for (int i = int.Parse(dateTimePicker2.Value.ToString("MM")); i <= int.Parse(dateTimePicker3.Value.ToString("MM")); i++)
                {
                    if (i == int.Parse(dateTimePicker2.Value.ToString("MM")))
                    {
                        int soNgayThangHienTai = DateTime.DaysInMonth(int.Parse(dateTimePicker2.Value.ToString("yyyy")), i);
                        for (int j = int.Parse(dateTimePicker2.Value.ToString("dd")); j <= soNgayThangHienTai; j++)
                        {
                            DataRow row = dsNgayThang.NewRow();
                            if (j < 10)
                            {
                                row["NgayThang"] = dateTimePicker2.Value.ToString("yyyy-MM") + "-0" + j.ToString();
                                dsNgayThang.Rows.Add(row);
                            }
                            else
                            {
                                row["NgayThang"] = dateTimePicker2.Value.ToString("yyyy-MM") + "-" +j.ToString();
                                dsNgayThang.Rows.Add(row);
                            }
                        }
                    }
                    else if (i == int.Parse(dateTimePicker3.Value.ToString("MM")))
                    {
                        for (int j = 1; j <= int.Parse(dateTimePicker3.Value.ToString("dd")); j++)
                        {
                            DataRow row = dsNgayThang.NewRow();
                            if (j < 10)
                            {
                                row["NgayThang"] = dateTimePicker3.Value.ToString("yyyy-MM") + "-0" + j.ToString();
                                dsNgayThang.Rows.Add(row);
                            }
                            else
                            {
                                row["NgayThang"] = dateTimePicker3.Value.ToString("yyyy-MM") + "-" +j.ToString();
                                dsNgayThang.Rows.Add(row);
                            }
                        }
                    }
                    else
                    {
                        int soNgayThangHienTai = DateTime.DaysInMonth(int.Parse(dateTimePicker2.Value.ToString("yyyy")), i);
                        for (int j = 1; j <= soNgayThangHienTai; j++)
                        {
                            DataRow row = dsNgayThang.NewRow();
                            if (j < 10)
                            {
                                if (i < 10)
                                {
                                    row["NgayThang"] = dateTimePicker2.Value.ToString("yyyy") + "-0" + i + "-" + "0" + j.ToString();
                                }
                                else
                                {
                                    row["NgayThang"] = dateTimePicker2.Value.ToString("yyyy") + "-" + i + "-" + "0" + j.ToString();
                                }
                                dsNgayThang.Rows.Add(row);
                            }
                            else
                            {
                                if (i < 10)
                                {
                                    row["NgayThang"] = dateTimePicker2.Value.ToString("yyyy") + "-0" + i + "-" + j.ToString();
                                }
                                else
                                {
                                    row["NgayThang"] = dateTimePicker2.Value.ToString("yyyy") + i + "-" + j.ToString();
                                }
                                dsNgayThang.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            return dsNgayThang;
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            DisableButton();

            try
            {
                ExportExcel();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                EnableButton();
            }
        }
        void ExportExcel()
        {
            Microsoft.Office.Interop.Excel.Application cExcel = new Microsoft.Office.Interop.Excel.Application();
            cExcel.Application.Workbooks.Add(Type.Missing);
            int i = 2;
            if (dataGridView1.Columns[3].Name.Equals("SDT"))
            {
                cExcel.Cells[1, 1] = "UID";
                cExcel.Cells[1, 2] = "Họ tên";
                cExcel.Cells[1, 3] = "Ngày sinh";
                cExcel.Cells[1, 4] = "SĐT";
                cExcel.Cells[1, 5] = "CMND";
                cExcel.Cells[1, 6] = "MST";
                cExcel.Cells[1, 7] = "BHXH";
                cExcel.Cells[1, 8] = "Ngày nhận việc";
                for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
                {
                    if (dataGridView1.Rows[j].Cells[8].Value.ToString().Equals("ON"))
                    {
                        cExcel.Cells[i, 1] = "'" + dataGridView1.Rows[j].Cells[0].Value.ToString();
                        cExcel.Cells[i, 2] = dataGridView1.Rows[j].Cells[1].Value.ToString();
                        cExcel.Cells[i, 3] = dataGridView1.Rows[j].Cells[2].Value.ToString();
                        cExcel.Cells[i, 4] = "'" + dataGridView1.Rows[j].Cells[3].Value.ToString();
                        cExcel.Cells[i, 5] = "'" + dataGridView1.Rows[j].Cells[4].Value.ToString();
                        cExcel.Cells[i, 6] = "'" + dataGridView1.Rows[j].Cells[5].Value.ToString();
                        cExcel.Cells[i, 7] = "'" + dataGridView1.Rows[j].Cells[6].Value.ToString();
                        cExcel.Cells[i, 8] = dataGridView1.Rows[j].Cells[7].Value.ToString();
                        i++;
                    }
                }
            }
            else
            {
                cExcel.Cells[1, 1] = "UID";
                cExcel.Cells[1, 2] = "Họ Tên";
                cExcel.Cells[1, 3] = "Ngày";
                cExcel.Cells[1, 4] = "CheckIn";
                cExcel.Cells[1, 5] = "CheckOut";
                cExcel.Cells[1, 6] = "Số phút";
                cExcel.Cells[1, 7] = "Trạng thái";
                for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
                {
                    cExcel.Cells[i, 1] = "'" + dataGridView1.Rows[j].Cells[0].Value.ToString();
                    cExcel.Cells[i, 2] = dataGridView1.Rows[j].Cells[1].Value.ToString();
                    cExcel.Cells[i, 3] = dataGridView1.Rows[j].Cells[3].Value.ToString();
                    cExcel.Cells[i, 4] = dataGridView1.Rows[j].Cells[4].Value.ToString();
                    cExcel.Cells[i, 5] = dataGridView1.Rows[j].Cells[5].Value.ToString();
                    cExcel.Cells[i, 6] = dataGridView1.Rows[j].Cells[6].Value.ToString();
                    cExcel.Cells[i, 7] = dataGridView1.Rows[j].Cells[7].Value.ToString();
                    i++;
                }
            }

            cExcel.Columns.AutoFit();
            cExcel.Visible = true;
        }

        private async void BtnStatisticsAHuman_Click(object sender, EventArgs e)
        {
            DisableButton();
            DataTable dsNhanVienDiemDanh = LayKhoangThoiGianThongKe();
            if (dsNhanVienDiemDanh.Rows.Count <= 0)
            {
                dataGridView1.DataSource = new DataTable();
            }
            else
            {
                DataTable dsNhanVien = new DataTable();
                dsNhanVien.Columns.Add("UID");
                dsNhanVien.Columns.Add("Name");
                dsNhanVien.Columns.Add("Birth");
                dsNhanVien.Columns.Add("Ngay");
                dsNhanVien.Columns.Add("ThoiGianVao");
                dsNhanVien.Columns.Add("ThoiGianRa");
                dsNhanVien.Columns.Add("SoPhut");
                dsNhanVien.Columns.Add("GhiChu");

                dataGridView1.DataSource = new DataTable();
                try
                {
                    FirebaseResponse response;
                    response = await client.GetTaskAsync(pathDanhSachNhanVien + txbUID.Text.Trim());
                    Data data = response.ResultAs<Data>();
                    if (data.statusWork.Equals("ON"))
                    {
                        foreach (DataRow rows in dsNhanVienDiemDanh.Rows)
                        {
                            DataRow row = dsNhanVien.NewRow();
                            row["UID"] = data.UID;
                            row["Name"] = data.Name;
                            row["Birth"] = data.BirthDay;
                            row["Ngay"] = rows["NgayThang"].ToString();
                            var firebase = new FirebaseClient(pathServer);
                            var duLieuChamCong = await firebase.Child(pathDuLieuDiemDanh + data.UID + "/" + rows["NgayThang"].ToString())
                                                .OrderByKey()
                                                .OnceAsync<String>();
                            switch (duLieuChamCong.Count)
                            {
                                case 0:
                                    row["ThoiGianVao"] = " ";
                                    row["ThoiGianRa"] = " ";
                                    row["SoPhut"] = " ";
                                    row["GhiChu"] = "Không làm việc";
                                    break;
                                case 1:
                                    foreach (var anc in duLieuChamCong)
                                    {
                                        string[] ijk = anc.Object.Split(',');
                                        row["ThoiGianVao"] = ijk[0];
                                        row["ThoiGianRa"] = " ";
                                        if (int.Parse(ijk[1].ToString()) > 0)
                                        {
                                            row["SoPhut"] = ijk[1];
                                            row["GhiChu"] = "Không Check Out, Đi Muộn";
                                        }
                                        else
                                        {
                                            row["SoPhut"] = " ";
                                            row["GhiChu"] = "Không Check Out";
                                        }
                                    }
                                    break;
                                default:
                                    int i = 0;
                                    foreach (var anc in duLieuChamCong)
                                    {
                                        if (i == 0)
                                        {
                                            string[] ijk = anc.Object.Split(',');
                                            row["ThoiGianVao"] = ijk[0];
                                            if (int.Parse(ijk[1].ToString()) > 0)
                                            {
                                                row["SoPhut"] = ijk[1];
                                                row["GhiChu"] = "Đi Muộn";
                                            }
                                            else
                                            {
                                                row["SoPhut"] = " ";
                                                row["GhiChu"] = " ";
                                            }
                                        }
                                        else
                                        {
                                            string[] ijk = anc.Object.Split(',');
                                            row["ThoiGianRa"] = ijk[0];
                                        }
                                        i++;
                                    }
                                    break;
                            }
                            dsNhanVien.Rows.Add(row);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nhân viên đã nghỉ việc không thể thống kê", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dataGridView1.DataSource = dsNhanVien;
            }
            EnableButton();
        }
        void EnableButton()
        {
            btnExportExcel.Enabled = true;
            btnGetData.Enabled = true;
            btnRegistration.Enabled = true;
            btnStatisticsAHuman.Enabled = true;
            btnStatisticsMonth.Enabled = true;
            btnUpdateDuLieuNhanVien.Enabled = true;
            btnXoaNhanVien.Enabled = true;
            btnSearchStaff.Enabled = true;
        }
        void DisableButton()
        {
            btnUpdateDuLieuNhanVien.Enabled = false;
            btnXoaNhanVien.Enabled = false;
            btnExportExcel.Enabled = false;
            btnGetData.Enabled = false;
            btnRegistration.Enabled = false;
            btnStatisticsAHuman.Enabled = false;
            btnStatisticsMonth.Enabled = false;
            btnSearchStaff.Enabled = false;
        }

        private async void BtnSearchStaff_Click(object sender, EventArgs e)
        {
            DisableButton();
            if (String.IsNullOrEmpty(txbUID.Text))
            {
                MessageBox.Show("Chưa chạm thẻ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EnableButton();
                return;
            }
            var data = new Data
            {
                UID = txbUID.Text,
                Name = txtNameCard.Text,
                BirthDay = txbBirthOfDate.Text,
                SDT = txbSDT.Text,
                CMND = txbCMND.Text,
                MST = txbMST.Text,
                BHXH = txbBHXH.Text,
                NBDLV = txbNBDLV.Text,
                statusWork = "ON"
            };
            PushResponse response = client.Push(pathDuLieuDiemDanh + txbUID.Text+"/2020-06-18", "jhfjdhj");
            if (String.IsNullOrEmpty(response.Result.Name.ToString()))
            {

            }
            else
            {
                MessageBox.Show("Push success");
            }
            EnableButton();

            //DisableButton();
            //DataTable dsNhanVien = new DataTable();
            //dsNhanVien.Columns.Add("UID");
            //dsNhanVien.Columns.Add("Name");
            //dsNhanVien.Columns.Add("Birth");
            //dsNhanVien.Columns.Add("SDT");
            //dsNhanVien.Columns.Add("CMND");
            //dsNhanVien.Columns.Add("MST");
            //dsNhanVien.Columns.Add("BHXH");
            //dsNhanVien.Columns.Add("NgayBDLV");
            //dsNhanVien.Columns.Add("StatusWork");
            //dataGridView1.DataSource = new DataTable();
            //ArrayList danhSachNhanVien = new ArrayList();
            //var firebase1 = new FirebaseClient(pathServer);
            //var duLieunhanvien = await firebase1.Child(pathDanhSachNhanVien)
            //                    .OrderByKey()
            //                    .OnceAsync<KeyIDTagData>();
            //foreach (var anc in duLieunhanvien)
            //{
            //    danhSachNhanVien.Add(anc.Key);
            //}
            //foreach (String idTag in danhSachNhanVien)
            //{
            //    try
            //    {
            //        FirebaseResponse response;
            //        response = await client.GetTaskAsync(pathDanhSachNhanVien + idTag.Trim());
            //        Data data = response.ResultAs<Data>();
            //        DataRow row = dsNhanVien.NewRow();
            //        row["UID"] = data.UID;
            //        row["Name"] = data.Name;
            //        row["Birth"] = data.BirthDay;
            //        row["SDT"] = data.SDT;
            //        row["CMND"] = data.CMND;
            //        row["MST"] = data.MST;
            //        row["BHXH"] = data.BHXH;
            //        row["NgayBDLV"] = data.NBDLV;
            //        row["StatusWork"] = data.statusWork;
            //        dsNhanVien.Rows.Add(row);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
            //dataGridView1.DataSource = dsNhanVien;
            //lbTotalStaffs.Text = (dataGridView1.Rows.Count - 1).ToString();
            //EnableButton();
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[3].Name.Equals("SDT"))
            {
                if (e.RowIndex == -1)
                {

                }
                else
                {
                    txbUID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtNameCard.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txbBirthOfDate.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txbSDT.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txbCMND.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txbMST.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txbBHXH.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    txbNBDLV.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                }
            }
        }

        private async void BtnXoaNhanVien_Click(object sender, EventArgs e)
        {
            DisableButton();
            if (String.IsNullOrEmpty(txbUID.Text))
            {
                MessageBox.Show("Chưa có ID", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EnableButton();
                return;
            }
            FirebaseResponse response1;
            response1 = await client.GetTaskAsync(pathDanhSachNhanVien + txbUID.Text);
            if (response1.Body == "null")
            {
                MessageBox.Show("Thẻ chưa đăng ký", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var data = new Data
                {
                    UID = txbUID.Text,
                    Name = txtNameCard.Text,
                    BirthDay = txbBirthOfDate.Text,
                    SDT = txbSDT.Text,
                    CMND = txbCMND.Text,
                    MST = txbMST.Text,
                    BHXH = txbBHXH.Text,
                    NBDLV = txbNBDLV.Text,
                    statusWork = "OFF"
                };
                FirebaseResponse response = await client.UpdateTaskAsync(pathDanhSachNhanVien + txbUID.Text, data);
                Data result = response.ResultAs<Data>();
                if (result.UID.Trim().Equals(txbUID.Text.Trim()))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txbUID.Text = "";
                    txtNameCard.Text = "";
                    txbBirthOfDate.Text = "";
                    txbSDT.Text = "";
                    txbCMND.Text = "";
                    txbMST.Text = "";
                    txbBHXH.Text = "";
                    txbNBDLV.Text = "";
                }
            }
            EnableButton();
            //FirebaseResponse response1 = await client.DeleteTaskAsync(pathDanhSachNhanVien + txbUID.Text.Trim());
            //MessageBox.Show("Xóa thành công");
            //txbUID.Text = "";
            //txtNameCard.Text = "";
            //txbBirthOfDate.Text = "";
            //txbSDT.Text = "";
            //txbCMND.Text = "";
            //txbMST.Text = "";
            //txbBHXH.Text = "";
            //txbNBDLV.Text = "";
            //DisableButton();
            //DataTable dsNhanVien = new DataTable();
            //dsNhanVien.Columns.Add("UID");
            //dsNhanVien.Columns.Add("Name");
            //dsNhanVien.Columns.Add("Birth");

            //dataGridView1.DataSource = new DataTable();
            //ArrayList danhSachNhanVien = new ArrayList();
            //var firebase1 = new FirebaseClient(pathServer);
            //var duLieunhanvien = await firebase1.Child(pathDanhSachNhanVien)
            //                    .OrderByKey()
            //                    .OnceAsync<KeyIDTagData>();
            //foreach (var anc in duLieunhanvien)
            //{
            //    danhSachNhanVien.Add(anc.Key);
            //}
            //foreach (String idTag in danhSachNhanVien)
            //{
            //    try
            //    {
            //        FirebaseResponse response;
            //        response = await client.GetTaskAsync(pathDanhSachNhanVien + idTag.Trim());
            //        Data data = response.ResultAs<Data>();
            //        DataRow row = dsNhanVien.NewRow();
            //        row["UID"] = data.UID;
            //        row["Name"] = data.Name;
            //        row["Birth"] = data.BirthDay;
            //        dsNhanVien.Rows.Add(row);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
            //dataGridView1.DataSource = dsNhanVien;
            //EnableButton();
        }

        class KeyIDTagData
        {
            public string keyID { get; set; }
            public string objectID { get; set; }
        }
        private async void BtnUpdateDuLieuNhanVien_Click(object sender, EventArgs e)
        {
            DisableButton();
            if (String.IsNullOrEmpty(txbUID.Text))
            {
                MessageBox.Show("Chưa có ID", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EnableButton();
                return;
            }
            FirebaseResponse response1;
            response1 = await client.GetTaskAsync(pathDanhSachNhanVien + txbUID.Text);
            if (response1.Body == "null")
            {
                MessageBox.Show("Thẻ chưa đăng ký", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var data = new Data
                {
                    UID = txbUID.Text,
                    Name = txtNameCard.Text,
                    BirthDay = txbBirthOfDate.Text,
                    SDT = txbSDT.Text,
                    CMND = txbCMND.Text,
                    MST = txbMST.Text,
                    BHXH = txbBHXH.Text,
                    NBDLV = txbNBDLV.Text,
                    statusWork = "ON"
                };
                FirebaseResponse response = await client.UpdateTaskAsync(pathDanhSachNhanVien + txbUID.Text, data);
                Data result = response.ResultAs<Data>();
                if (result.UID.Trim().Equals(txbUID.Text.Trim()))
                {
                    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txbUID.Text = "";
                    txtNameCard.Text = "";
                    txbBirthOfDate.Text = "";
                    txbSDT.Text = "";
                    txbCMND.Text = "";
                    txbMST.Text = "";
                    txbBHXH.Text = "";
                    txbNBDLV.Text = "";
                }
            }
            EnableButton();
        }
    }
}
