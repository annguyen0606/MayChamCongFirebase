namespace MayChamCongV2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label3;
            this.btnRegistration = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnXoaNhanVien = new System.Windows.Forms.Button();
            this.btnSearchStaff = new System.Windows.Forms.Button();
            this.btnStatisticsAHuman = new System.Windows.Forms.Button();
            this.btnStatisticsMonth = new System.Windows.Forms.Button();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txbBirthOfDate = new System.Windows.Forms.TextBox();
            this.btnGetData = new System.Windows.Forms.Button();
            this.txtNameCard = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbUID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnUpdateDuLieuNhanVien = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lbTotalStaffs = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txbCMND = new System.Windows.Forms.TextBox();
            this.txbMST = new System.Windows.Forms.TextBox();
            this.txbBHXH = new System.Windows.Forms.TextBox();
            this.txbNBDLV = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txbSDT = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 61);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(50, 13);
            label3.TabIndex = 2;
            label3.Text = "Birth Day";
            // 
            // btnRegistration
            // 
            this.btnRegistration.Location = new System.Drawing.Point(9, 210);
            this.btnRegistration.Name = "btnRegistration";
            this.btnRegistration.Size = new System.Drawing.Size(279, 23);
            this.btnRegistration.TabIndex = 9;
            this.btnRegistration.Text = "Đăng ký";
            this.btnRegistration.UseVisualStyleBackColor = true;
            this.btnRegistration.Click += new System.EventHandler(this.BtnRegistration_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txbSDT);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txbNBDLV);
            this.groupBox1.Controls.Add(this.txbBHXH);
            this.groupBox1.Controls.Add(this.txbMST);
            this.groupBox1.Controls.Add(this.txbCMND);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnUpdateDuLieuNhanVien);
            this.groupBox1.Controls.Add(this.btnXoaNhanVien);
            this.groupBox1.Controls.Add(this.btnSearchStaff);
            this.groupBox1.Controls.Add(this.btnStatisticsAHuman);
            this.groupBox1.Controls.Add(this.btnStatisticsMonth);
            this.groupBox1.Controls.Add(this.dateTimePicker3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.txbBirthOfDate);
            this.groupBox1.Controls.Add(this.btnGetData);
            this.groupBox1.Controls.Add(label3);
            this.groupBox1.Controls.Add(this.txtNameCard);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txbUID);
            this.groupBox1.Controls.Add(this.btnRegistration);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 436);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Đăng ký";
            // 
            // btnXoaNhanVien
            // 
            this.btnXoaNhanVien.Location = new System.Drawing.Point(10, 404);
            this.btnXoaNhanVien.Name = "btnXoaNhanVien";
            this.btnXoaNhanVien.Size = new System.Drawing.Size(129, 23);
            this.btnXoaNhanVien.TabIndex = 17;
            this.btnXoaNhanVien.Text = "Xóa nhân viên";
            this.btnXoaNhanVien.UseVisualStyleBackColor = true;
            this.btnXoaNhanVien.Click += new System.EventHandler(this.BtnXoaNhanVien_Click);
            // 
            // btnSearchStaff
            // 
            this.btnSearchStaff.Location = new System.Drawing.Point(10, 375);
            this.btnSearchStaff.Name = "btnSearchStaff";
            this.btnSearchStaff.Size = new System.Drawing.Size(278, 23);
            this.btnSearchStaff.TabIndex = 16;
            this.btnSearchStaff.Text = "Tìm kiếm thông tin nhân viên";
            this.btnSearchStaff.UseVisualStyleBackColor = true;
            this.btnSearchStaff.Click += new System.EventHandler(this.BtnSearchStaff_Click);
            // 
            // btnStatisticsAHuman
            // 
            this.btnStatisticsAHuman.Location = new System.Drawing.Point(158, 346);
            this.btnStatisticsAHuman.Name = "btnStatisticsAHuman";
            this.btnStatisticsAHuman.Size = new System.Drawing.Size(130, 23);
            this.btnStatisticsAHuman.TabIndex = 15;
            this.btnStatisticsAHuman.Text = "Thống kê một người";
            this.btnStatisticsAHuman.UseVisualStyleBackColor = true;
            this.btnStatisticsAHuman.Click += new System.EventHandler(this.BtnStatisticsAHuman_Click);
            // 
            // btnStatisticsMonth
            // 
            this.btnStatisticsMonth.Location = new System.Drawing.Point(10, 346);
            this.btnStatisticsMonth.Name = "btnStatisticsMonth";
            this.btnStatisticsMonth.Size = new System.Drawing.Size(129, 23);
            this.btnStatisticsMonth.TabIndex = 14;
            this.btnStatisticsMonth.Text = "Thống kê tất cả";
            this.btnStatisticsMonth.UseVisualStyleBackColor = true;
            this.btnStatisticsMonth.Click += new System.EventHandler(this.BtnStatisticsMonth_Click);
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Location = new System.Drawing.Point(61, 320);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(227, 20);
            this.dateTimePicker3.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 323);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Đến ngày";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(61, 294);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(227, 20);
            this.dateTimePicker2.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 297);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Từ ngày";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(9, 239);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(279, 20);
            this.dateTimePicker1.TabIndex = 10;
            // 
            // txbBirthOfDate
            // 
            this.txbBirthOfDate.Location = new System.Drawing.Point(61, 58);
            this.txbBirthOfDate.Name = "txbBirthOfDate";
            this.txbBirthOfDate.Size = new System.Drawing.Size(227, 20);
            this.txbBirthOfDate.TabIndex = 2;
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(9, 265);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(279, 23);
            this.btnGetData.TabIndex = 11;
            this.btnGetData.Text = "Thống kê theo ngày";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.BtnGetData_Click);
            // 
            // txtNameCard
            // 
            this.txtNameCard.Location = new System.Drawing.Point(61, 35);
            this.txtNameCard.Name = "txtNameCard";
            this.txtNameCard.Size = new System.Drawing.Size(227, 20);
            this.txtNameCard.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Họ tên";
            // 
            // txbUID
            // 
            this.txbUID.Location = new System.Drawing.Point(61, 13);
            this.txbUID.Name = "txbUID";
            this.txbUID.Size = new System.Drawing.Size(227, 20);
            this.txbUID.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mã UID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(644, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(281, 31);
            this.label4.TabIndex = 2;
            this.label4.Text = "Danh Sách Nhân Viên";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(324, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1020, 388);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(1224, 18);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(120, 23);
            this.btnExportExcel.TabIndex = 4;
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.BtnExportExcel_Click);
            // 
            // btnUpdateDuLieuNhanVien
            // 
            this.btnUpdateDuLieuNhanVien.Location = new System.Drawing.Point(158, 404);
            this.btnUpdateDuLieuNhanVien.Name = "btnUpdateDuLieuNhanVien";
            this.btnUpdateDuLieuNhanVien.Size = new System.Drawing.Size(130, 23);
            this.btnUpdateDuLieuNhanVien.TabIndex = 18;
            this.btnUpdateDuLieuNhanVien.Text = "Cập nhật nhân viên";
            this.btnUpdateDuLieuNhanVien.UseVisualStyleBackColor = true;
            this.btnUpdateDuLieuNhanVien.Click += new System.EventHandler(this.BtnUpdateDuLieuNhanVien_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(321, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Tổng số nhân viên:";
            // 
            // lbTotalStaffs
            // 
            this.lbTotalStaffs.AutoSize = true;
            this.lbTotalStaffs.Location = new System.Drawing.Point(426, 44);
            this.lbTotalStaffs.Name = "lbTotalStaffs";
            this.lbTotalStaffs.Size = new System.Drawing.Size(0, 13);
            this.lbTotalStaffs.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "CMND";
            // 
            // txbCMND
            // 
            this.txbCMND.Location = new System.Drawing.Point(61, 106);
            this.txbCMND.Name = "txbCMND";
            this.txbCMND.Size = new System.Drawing.Size(227, 20);
            this.txbCMND.TabIndex = 4;
            // 
            // txbMST
            // 
            this.txbMST.Location = new System.Drawing.Point(61, 132);
            this.txbMST.Name = "txbMST";
            this.txbMST.Size = new System.Drawing.Size(227, 20);
            this.txbMST.TabIndex = 5;
            // 
            // txbBHXH
            // 
            this.txbBHXH.Location = new System.Drawing.Point(61, 158);
            this.txbBHXH.Name = "txbBHXH";
            this.txbBHXH.Size = new System.Drawing.Size(227, 20);
            this.txbBHXH.TabIndex = 6;
            // 
            // txbNBDLV
            // 
            this.txbNBDLV.Location = new System.Drawing.Point(61, 184);
            this.txbNBDLV.Name = "txbNBDLV";
            this.txbNBDLV.Size = new System.Drawing.Size(227, 20);
            this.txbNBDLV.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 135);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "MST";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 162);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "BHXH";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 187);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "NBĐLV";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 84);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "SĐT";
            // 
            // txbSDT
            // 
            this.txbSDT.Location = new System.Drawing.Point(61, 80);
            this.txbSDT.Name = "txbSDT";
            this.txbSDT.Size = new System.Drawing.Size(227, 20);
            this.txbSDT.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 455);
            this.Controls.Add(this.lbTotalStaffs);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Máy Chấm Công Conek";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRegistration;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNameCard;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbUID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.TextBox txbBirthOfDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnStatisticsMonth;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnStatisticsAHuman;
        private System.Windows.Forms.Button btnSearchStaff;
        private System.Windows.Forms.Button btnXoaNhanVien;
        private System.Windows.Forms.Button btnUpdateDuLieuNhanVien;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbTotalStaffs;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txbNBDLV;
        private System.Windows.Forms.TextBox txbBHXH;
        private System.Windows.Forms.TextBox txbMST;
        private System.Windows.Forms.TextBox txbCMND;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txbSDT;
        private System.Windows.Forms.Label label12;
    }
}

