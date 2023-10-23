using BT8_LISTBOX.BatLoiControl;
using System.Diagnostics.Eventing.Reader;

namespace BT8_LISTBOX.BT_NANGCAO
{
    public partial class BT16 : Form
    {
        ErrTxt r;
        DanhSachSinhVien arraydata_sv = new DanhSachSinhVien();
        string colWidth = "161";
        public BT16()
        {
            InitializeComponent();
        }

        private void BT16_Load(object sender, EventArgs e)
        {
            r = new ErrTxt(this);
            r.LblHint.Text = "Hello";

            txtColWidth.Text = lvSinhVien.Columns[0].Width.ToString();
            txtHoTen.PlaceholderText = txtHoTen.Text;
            txtPhone.PlaceholderText = txtPhone.Text;
            txtHoTen.Text = string.Empty;
            txtPhone.Text = string.Empty;

            // khoa control
            DisabledControlCRUD();
        }

        private void txtColWidth_TextChanged(object sender, EventArgs e)
        {
            txtColWidth.TextAlign = HorizontalAlignment.Right;
            r.NoSymTextChange(sender);
            for (int i = 0; i < lvSinhVien.Columns.Count; i++)
            {
                lvSinhVien.Columns[i].Width = int.Parse(txtColWidth.Text.ToString());
            }
            trackBar1.Value = int.Parse(txtColWidth.Text.ToString());
        }
        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnThem_Click(object sender, EventArgs e)
        {
            //Thêm thông tin sinh viên + Update list view
            try
            {
                if (txtHoTen.Text.Length <= 0)
                {
                    txtHoTen.Focus();
                    r.LblHint.Text = "Vui lòng nhập họ tên";
                    r.PnError.BackColor = ErrColors.err_txt_required;
                    return;
                }
                if (txtPhone.Text.Length <= 0)
                {
                    txtPhone.Focus();
                    r.LblHint.Text = "Vui lòng nhập Số điện thoại";
                    r.PnError.BackColor = ErrColors.err_txt_required;
                    return;
                }
                // luu vao trong file
                SinhVien sinhvien = new SinhVien();
                sinhvien.Hoten = txtHoTen.Text;
                sinhvien.Phone = txtPhone.Text;
                // kiem tra trung so dien thoai 
                if (arraydata_sv.ContainsPhoneNumber(sinhvien))
                {
                    // đã có số điện thoại này không thêm số đt này đã có
                    btnThem.Enabled = true;// mở nút thêm 
                    r.PnError.BackColor = ErrColors.mes_black;
                    r.LblHint.Text = "Số điện thoại này đã tồn tại";//thông báo tồn tại số điện thoại
                    return;
                }
                // Thêm sinh viên 
                if (!arraydata_sv.Add(sinhvien))
                {
                    // thông báo thêm không thành công
                    r.PnError.BackColor = ErrColors.mes_error;
                    r.LblHint.Text = "Thêm không thành công!";
                    return;
                }
                else
                {
                    // thông báo thêm thành công
                    r.PnError.BackColor = ErrColors.mes_success;
                    r.LblHint.Text = "Thêm thành công!";
                }

                // xử lý tiếp thêm vào list view
                // Thêm sinh viên vào listViewSinhVien
                //Khởi tạo
                ListViewItem item = new ListViewItem();

                //thêm dữ liệu vào listViewItem
                item.Text = sinhvien.Id;// bat buoc de add dung thu tu
                item.SubItems.Add(txtHoTen.Text);
                item.SubItems.Add(txtPhone.Text);

                //thêm dữ liệu vào listView
                lvSinhVien.Items.Add(item);

                //Thay đổi độ rộng các cột
                txtColWidth.Text = colWidth;

                // Cập nhật lại ListView
                lvSinhVien.Refresh();

                // xóa ô nhập
                // reset thông tin
                txtHoTen.Text = string.Empty;
                txtPhone.Text = string.Empty;
            }
            catch (Exception ex)
            {
                // bắt lỗi trùng số phone
                txtPhone.Focus();
                r.LblHint.Text = ex.Message;//Trùng số điện thoại
                r.PnError.BackColor = ErrColors.mes_warning;
            }
            finally
            {
                // dù danh sách có lỗi hay không thì vẫn tiếp tục mở crud để thêm xóa sửa đọc ghi file
                // Mở khóa CRUD
                // sử dụng ở hàm đọc file
                EnabledControlCRUD();
            }
        }
        private void CapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                // TH chưa chọn và nhấn cập nhật
                // kiểm tra user đã chọn danh sách chưa
                if (lvSinhVien.SelectedIndices.Count == 0)
                {
                    r.LblHint.Text = "Chưa chọn dòng để cập nhật";
                    r.PnError.BackColor = ErrColors.mes_black;
                    return;
                }
                //chọn rồi thì tạo mới 1 đối tượng sinh viên
                SinhVien udpateSV = new SinhVien();
                udpateSV.Id = txtIDSinhVien.Text;
                udpateSV.Hoten = txtHoTen.Text;
                udpateSV.Phone = txtPhone.Text;
                // TH Kiểm tra số điện thoại 
                if (arraydata_sv.ContainsPhoneNumber(udpateSV))
                {
                    // đã có số điện thoại này không thêm số đt này đã có
                    btnThem.Enabled = true;// mở nút thêm 
                    r.PnError.BackColor = ErrColors.mes_black;
                    r.LblHint.Text = "Số điện thoại này đã tồn tại";//thông báo tồn tại số điện thoại
                    return;
                }
                //Cập nhật thông tin sinh viên 
                //TH: Thất bại
                if (!arraydata_sv.Update(udpateSV))
                {
                    btnThem.Enabled = true;// mở nút thêm 
                    r.PnError.BackColor = ErrColors.mes_error;
                    r.LblHint.Text = "Cập nhật sinh viên không thành công!";//thông báo cập nhật sinh viên không thành công
                }
                //TH: thành công
                else
                {
                    btnThem.Enabled = true;// mở nút thêm 
                    r.PnError.BackColor = ErrColors.mes_success;
                    r.LblHint.Text = "Cập nhật sinh viên thành công!";//thông báo cập nhật sinh viên thành công
                }

                // update lại list view hiển thị lên list  view (update listview)
                if (!arraydata_sv.Show(lvSinhVien))
                {
                    r.PnError.BackColor = ErrColors.mes_error;
                    r.LblHint.Text = "Không hiển thị được listview!";
                    return;
                }


                txtColWidth.Text = colWidth;//update độ rộng các cột

                lvSinhVien.Refresh();//Tải lại giao diện của listview

                EnabledControlCRUD();// hiển thị các button  

                // xóa ô nhập
                // reset thông tin
                txtHoTen.Text = string.Empty;
                txtPhone.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvSinhVien.Items.Count > 0 && lvSinhVien.SelectedIndices.Count > 0)
                {
                    DialogResult s = MessageBox.Show("Bạn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (s == DialogResult.Yes)
                    {
                        int row = lvSinhVien.SelectedIndices[0];
                        SinhVien sv = new SinhVien();
                        sv.Id = lvSinhVien.Items[row].SubItems[0].Text;
                        sv.Hoten = lvSinhVien.Items[row].SubItems[1].Text;
                        sv.Phone = lvSinhVien.Items[row].SubItems[2].Text;

                        if (!arraydata_sv.Remove(sv))
                        {
                            r.PnError.BackColor = ErrColors.mes_error;
                            r.LblHint.Text = "Xóa sinh viên không thành công!";//thông báo cập nhật sinh viên không thành công
                            return;
                        }
                        //TH: thành công
                        r.PnError.BackColor = ErrColors.mes_success;
                        r.LblHint.Text = "Xóa sinh viên thành công!";//thông báo cập nhật sinh viên thành công

                        // // TH:xóa list view thành rỗng!
                        if (!arraydata_sv.Show(lvSinhVien))
                        {
                            r.PnError.BackColor = ErrColors.mes_error;
                            r.LblHint.Text = "Danh sách sinh viên rỗng!";//thông báo cập nhật sinh viên không thành công

                            lvSinhVien.Refresh();//Tải lại giao diện của listview

                            //quay lại thao tác thêm
                            DisabledControlCRUD();
                            // xóa ô nhập
                            // reset thông tin
                            txtHoTen.Text = string.Empty;
                            txtPhone.Text = string.Empty;


                            return;//Hủy các thao tác khác
                        }

                        lvSinhVien.Refresh();//Tải lại giao diện của listview

                        //TH: còn thông tin để xóa 
                        //quay lại thao tác thêm
                        EnabledControlCRUD();
                        // xóa ô nhập
                        // reset thông tin
                        txtHoTen.Text = string.Empty;
                        txtPhone.Text = string.Empty;
                    }
                }
                else
                {
                    r.LblHint.Text = "Chưa chọn dòng để xóa";
                    r.PnError.BackColor = ErrColors.err_lv_listview_back_color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaHet_Click(object sender, EventArgs e)
        {
            if (lvSinhVien.Items.Count > 0)
            {
                DialogResult s = MessageBox.Show("Bạn muốn xóa hết không?", "Xác nhận xóa hết", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (s == DialogResult.Yes)
                {
                    lvSinhVien.Items.Clear();
                    arraydata_sv.Clear();
                    r.LblHint.Text = "Danh sách rỗng";
                    r.PnError.BackColor = ErrColors.mes_error;
                    DisabledControlCRUD();
                }
            }
            else
            {
                r.LblHint.Text = "Danh sách rỗng";
                r.PnError.BackColor = ErrColors.err_lv_listview_back_color;
            }
        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {
            int min = 8;
            int max = 35;
            r.NoSymNumMinMaxChange(sender, min, max);

            TextBox t = (TextBox)sender;

            if (t.Text.Length < min)
            {
                r.LblHint.Text = "Chưa nhập đủ";

            }
            else if (t.Text.Length > max)
            {
                r.LblHint.Text = "Nhập dư";
            }
            else
            {
                r.LblHint.Text = "Nhập ok!";
                r.PnError.BackColor = ErrColors.mes_success;
            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            txtPhone.TextAlign = HorizontalAlignment.Right;
            r.NoSymTextChange(sender);
            btnThem.Enabled = true;
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            r.NoSymTextSpacePress(e);
        }

        private void txtHoTen_KeyPress(object sender, KeyPressEventArgs e)
        {
            r.NoSymNumMinMaxPress(sender, e, 35); //test case: Nguyễn Ngọc Diễm Trinh
            r.LblHint.Text = txtHoTen.Text.Length.ToString();
        }

        private void lvDemo_Click(object sender, EventArgs e)
        {
            if (lvSinhVien.SelectedIndices.Count == 0)
            {
                return;
            }
            else
            {
                int dong = lvSinhVien.SelectedIndices[0];
                txtIDSinhVien.Text = lvSinhVien.Items[dong].SubItems[0].Text;
                txtHoTen.Text = lvSinhVien.Items[dong].SubItems[1].Text;
                txtPhone.Text = lvSinhVien.Items[dong].SubItems[2].Text;
            }
        }


        private void txtEmpty_Event(object sender, EventArgs e)
        {
            if (lvSinhVien.Items.Count > 0 && lvSinhVien.SelectedIndices.Count > 0)
            {
                r.PnError.BackColor = ErrColors.mes_info;
                r.LblHint.Text = "Sửa thông tin";
            }
            else
            {
                r.PnError.BackColor = ErrColors.mes_info;
                r.LblHint.Text = "Thêm mới thông tin";
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            txtColWidth.Text = trackBar1.Value.ToString();
        }
        /// <summary>
        /// Tắt các điều khiển dữ liệu
        /// </summary>
        public void DisabledControlCRUD()
        {
            if (lvSinhVien.Items.Count == 0)
            {
                btnGhiFile.Enabled = false;
                btnDocFile.Enabled = true;//open file button
                btnXoa.Enabled = false;
                btnXoaHet.Enabled = false;
                btnCapNhat.Enabled = false;
            }
        }

        /// <summary>
        /// Bật các điều khiển dữ liệu
        /// </summary>
        public void EnabledControlCRUD()
        {
            if (lvSinhVien.Items.Count != 0)
            {
                btnGhiFile.Enabled = true;
                btnDocFile.Enabled = true;
                btnXoa.Enabled = true;
                btnXoaHet.Enabled = true;
                btnCapNhat.Enabled = true;
            }
        }
        private void btnDocFile_Click(object sender, EventArgs e)
        {
            // mở browser lên tìm file để đọc 
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = dlg.FileName;
                // Đọc file 
                if (!arraydata_sv.DocFile(fileName))
                {
                    r.PnError.BackColor = ErrColors.mes_error;
                    r.LblHint.Text = "Không đọc file rỗng!";
                    return;
                }
                // đọc file xong thì hiển thị lên lên view (update listview)
                if (!arraydata_sv.Show(lvSinhVien))
                {
                    r.PnError.BackColor = ErrColors.mes_error;
                    r.LblHint.Text = "Không hiển thị được listview!";
                    return;
                }

                r.PnError.BackColor = ErrColors.mes_success;
                r.LblHint.Text = $"Đọc file thành công! SL: {arraydata_sv.Count()}";

                txtColWidth.Text = colWidth;//update độ rộng các cột

                lvSinhVien.Refresh();//Tải lại giao diện của listview

                EnabledControlCRUD();// hiển thị các button  
            }
        }

        private void btnGhiFile_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                DialogResult result;
                sfd.Title = "sinhvien.txt";
                sfd.Filter = "File text (*.txt)|*.txt";
                result = sfd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // mở browser lên tìm file để lưu
                    string file_name = sfd.FileName;
                    if (arraydata_sv.GhiFile(file_name))
                    {
                        r.LblHint.Text = "Ghi file thành công!";
                        r.PnError.BackColor = ErrColors.mes_success;
                    }
                }
            }
            catch (Exception ex)
            {
                r.PnError.BackColor = ErrColors.mes_error;
                r.LblHint.Text = ex.Message;
            }
        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvSinhVien.View = View.SmallIcon;
            lvSinhVien.Refresh();
        }

        private void detailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvSinhVien.View = View.Details;
            lvSinhVien.Refresh();
        }

        private void BT16_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TH danh sach rong
            if (arraydata_sv.Count()==0)
            {
                ErrFrm.FormClosingEvent(sender, e);
            }

            //TH danh sach khong rong
            else if(arraydata_sv.Count()>0)
            {
                if(!ErrFrm.FormClosingSaveFileEvent(sender, e))
                {
                    return;
                }
                try
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    DialogResult result;
                    sfd.Title = "sinhvien.txt";
                    sfd.Filter = "File text (*.txt)|*.txt";
                    result = sfd.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        // mở browser lên tìm file để lưu
                        string file_name = sfd.FileName;
                        if (arraydata_sv.GhiFile(file_name))
                        {
                            r.LblHint.Text = "Ghi file thành công!";
                            r.PnError.BackColor = ErrColors.mes_success;
                        }
                    }
                }
                catch (Exception ex)
                {
                    r.PnError.BackColor = ErrColors.mes_error;
                    r.LblHint.Text = ex.Message;
                }
            }
        }
    }
}
