using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        string str = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLHN; Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }
        public void loadHoiNghi()
        {
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM HoiNghi";
            adapter.SelectCommand = cmd;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            txtmaHN.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txttenHN.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtsongTG.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            cbxloai.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(str);
            con.Open();
            loadHoiNghi();
            Loadcbx();
        }
        public void Loadcbx()
        {
            string[] Loai = { "PHONG_HN", "PHONG_HOP", };
            cbxloai.Items.AddRange(Loai);
            cbxloai.SelectedIndex = 0;
        }
        private void cbxloai_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loadcbx();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            txtmaHN.Text = null;
            txttenHN.Text = null;
            txtsongTG.Text = null; txtsongTG.Text = null;
        }
        private void Save()
        {
            if (string.IsNullOrEmpty(txttenHN.Text))
            {
                MessageBox.Show("Vui Lòng Nhập Tên Hội Nghị !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (string.IsNullOrEmpty(txtsongTG.Text))
                {
                    MessageBox.Show("Vui Lòng Nhập Số lượng!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    if (int.Parse(txtsongTG.Text) > 50)
                    {
                        DialogResult dlr = MessageBox.Show("\nThêm phòng hội nghị   ", "Xác Nhận ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlr == DialogResult.Yes)
                        {
                            Random rand = new Random();
                            string kytu = "HN";
                            string s = "";
                            int temp = 5;
                            string number;
                            int num = 5;
                            number = num.ToString();
                            List<int> list = new List<int>();
                            for (int i = 0; i <= 5; i++)
                            {
                                list.Add(i);
                                //list.Add(rand.Next(a));
                            }
                            //random
                            for (int i = 0; i <= 4; i++)
                            {
                                temp = rand.Next(list.Count);
                                s += list[temp];
                                list.RemoveAt(temp);

                            }





                            string Ma = kytu + s;
                            //cmd.CommandText = @"insert into HoiNghi values ('" + Ma.Trim() + "',N'" + txtTenHoiNghi.Text.Trim() + "'," + int.Parse(txtSoLuong.Text) + ",N'" + cbxLoaiPhong.Text.Trim() + "')";


                            cmd.CommandText = @"EXEC InsertHoiNghi  @maHoiNghi = '" + Ma.Trim() + "', @tenHoiNghi = N'" + txttenHN.Text.Trim() + "', @SoNguoi = " + int.Parse(txtsongTG.Text) + ",   @tenLoaiPhong = N'" + cbxloai.Text.Trim() + "'; ";

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Thêm Phòng Hội Nghị Thành Công :", "Thông  Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            dataGridView1();

                        }
                        else
                        {
                            MessageBox.Show("Yêu Cầu Đã hủy", "Thông  Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }

                    }

                    else
                    {
                        MessageBox.Show("Số lượng trên 50", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
