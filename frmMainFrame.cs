
using BT8_LISTBOX.BatLoiControl;
using LTUD1_MF_BHX.Helpers;
using LTUD1_MF_BHX.Screen;
using System.Runtime.InteropServices;


namespace LTUD1_MF_BHX
{
    public partial class FrmBHX : Form
    {
        private bool sidebarExpand;
        private int speed = 64 - 16;//chia het tang toc do dong mmở navigation
        private int maxWidthMenu = 320;
        private int minWidthMenu = 80;
        private Button currentButton;
        private Form currentChildForm;
        public FrmBHX()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            // Thiết lập kích thước cho cột đầu tiên  
            tlpBHX.ColumnStyles[0].Width = minWidthMenu;
            panelSidebar.Width = minWidthMenu;
            this.Text = string.Empty;// xóa tiêu đề
            this.ControlBox = false;// tắt nút thoát 
            this.DoubleBuffered = true;// giảm nháy màn hình
        }

        /**
         * Thay đổi panel mỗi khi nhấp vào 1 text (Kích hoạt panel đó)
         */
        public void ActiveButton(object currentButtonSender, Color ActiveColorText, Color ActiveColorBack)
        {
            if (currentButtonSender != null)
            {
                DisableButton();
                currentButton = (Button)currentButtonSender;
                currentButton.ForeColor = ActiveColorText;
                currentButton.BackColor = ActiveColorBack;
                // active current icon title
                currentChangeButton.Image = currentButton.Image;
                currentChangeTitle.Text = currentButton.Text;
            }
        }
        /**
         * Trả về panel khác như cũ sau khi active panel mới
         */
        public void DisableButton()
        {
            // BUTTON
            if (currentButton != null)
            {
                currentButton.BackColor = Color.Transparent;
                currentButton.ForeColor = Color.White;
                currentChangeButton.Image = getIcon(listIcon_navigation_bar, "icon_home", "png");
                currentChangeTitle.Text = "TRANG CHỦ";
            }
        }

        private struct RGB_COLORS
        {
            public static Color PrimaryGreen = Color.FromArgb(0, 31, 72);
            public static Color SecondaryGreen = Color.FromArgb(0, 128, 0);//green
            public static Color DarkGreen = Color.FromArgb(5, 42, 25);
            public static Color PrimaryPink = Color.FromArgb(255, 169, 169);
            public static Color SecondaryPink = Color.FromArgb(240, 140, 140);
            public static Color DarkPink = Color.FromArgb(190, 149, 149);
        }
        public static Image getIcon(ImageList iconList, string nameImage, string type)
        {
            return iconList!.Images[iconList!.Images.Keys.IndexOf(nameImage + "." + type)];
        }

        private struct IMG_LISTS
        {
            public static ImageList ls;
            public static string typeOfFile;
            public static Image nhanvien = getIcon(ls, "icon_nhanvien", typeOfFile);
            public static Image diadiem = getIcon(ls, "icon_diadiem", typeOfFile);
            public static Image sanpham = getIcon(ls, "icon_sanpham", typeOfFile);
            public static Image hoadon = getIcon(ls, "icon_hoadon", typeOfFile);
            public static Image khuyenmai = getIcon(ls, "icon_khuyenmai", typeOfFile);
            public static Image khachhang = getIcon(ls, "icon_khachhang", typeOfFile);
            public static Image nhacungcap = getIcon(ls, "icon_nhacungcap", typeOfFile);
            public static Image home = getIcon(ls, "icon_home", typeOfFile);
        }
        private void NavigationButton_MouseClick(object sender, EventArgs e)
        {
            ActiveButton(sender, RGB_COLORS.PrimaryGreen, RGB_COLORS.PrimaryPink);
        }

        private void NavigationButton_MouseEnter(object sender, EventArgs e)
        {
            Button currentPanel = (Button)sender;
            if (currentPanel != null)
            {
                currentPanel.BackColor = RGB_COLORS.PrimaryPink; // Màu button khi di chuột vào
            }
        }

        private void NavigationButton_MouseLeave(object sender, EventArgs e)
        {
            Button currentPanel = (Button)sender;
            if (currentPanel != null)
            {
                currentPanel.BackColor = Color.Transparent; // Màu button khi di chuột ra
            }
        }

        //drag form
        [DllImport("user32.dll", EntryPoint = "Releasecapture")]
        private static extern void Releasecapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                //open only form
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            labelTitleChildForm.Text = childForm.Text;
        }

        /**
         * Active click 7 button
         */
        //1
        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGB_COLORS.PrimaryGreen, RGB_COLORS.PrimaryPink);
            OpenChildForm(new FormNhanVien());

        }
        //2
        private void btnDiaDiem_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGB_COLORS.PrimaryGreen, RGB_COLORS.PrimaryPink);
            OpenChildForm(new FormDiaDiem());

        }
        //3
        private void btnSanPham_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGB_COLORS.PrimaryGreen, RGB_COLORS.PrimaryPink);
            OpenChildForm(new FormSanPham());
        }
        //4
        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGB_COLORS.PrimaryGreen, RGB_COLORS.PrimaryPink);
            OpenChildForm(new FormHoaDon());
        }
        //5
        private void btnKhuyenMai_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGB_COLORS.PrimaryGreen, RGB_COLORS.PrimaryPink);
            OpenChildForm(new FormKhuyenMai());
        }
        //6
        private void btnNhaCungCap_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGB_COLORS.PrimaryGreen, RGB_COLORS.PrimaryPink);
            OpenChildForm(new FormNhaCungCap());

        }
        //7
        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RGB_COLORS.PrimaryGreen, RGB_COLORS.PrimaryPink);
            OpenChildForm(new FormKhachHang());
        }
        /**
       * reset click 7 button
       */
        private void btnHome_Click(object sender, EventArgs e)
        {
            currentChildForm.Close();
            Reset();
        }
        /**
           * offset navbar
           */
        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            // tự động thu gọn navigation khi mở app
            if (sidebarExpand)
            {
                //panelSidebar.Width -= chiahetcho;
                tlpBHX.ColumnStyles[0].Width += speed;
                if (tlpBHX.ColumnStyles[0].Width == maxWidthMenu)
                //if (panelSidebar.Width == panelSidebar.MinimumSize.Width)
                {
                    // đặt lại chữ
                    this.btnDiaDiem.Text = "địa điểm".ToUpper();
                    this.btnNhanVien.Text = "nhân viên".ToUpper();
                    this.btnNhaCungCap.Text = "nhà cung cấp".ToUpper();
                    this.btnSanPham.Text = "sản phẩm".ToUpper();
                    this.btnHoaDon.Text = "hóa đơn".ToUpper();
                    this.btnKhuyenMai.Text = "khuyến mãi".ToUpper();
                    this.btnKhachHang.Text = "khách hàng".ToUpper();
                    // khong mo rong nua
                    sidebarExpand = false;
                    sidebarTimer.Stop();

                }
            }
            //th nguoc lai sidebar mở ra do nhấn vào nút navigationbar 
            else
            {
                //panelSidebar.Height += chiahetcho;
                tlpBHX.ColumnStyles[0].Width -= speed;
                if (tlpBHX.ColumnStyles[0].Width == minWidthMenu)
                //if (panelSidebar.Width == panelSidebar.MaximumSize.Width)
                {
                    // xóa chữ đi
                    this.btnDiaDiem.Text = "";
                    this.btnNhanVien.Text = "";
                    this.btnNhaCungCap.Text = "";
                    this.btnSanPham.Text = "";
                    this.btnHoaDon.Text = "";
                    this.btnKhuyenMai.Text = "";
                    this.btnKhachHang.Text = "";
                    // khong thu hep nua
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }
        private void Reset()
        {
            DisableButton();
        }
        /**
       * Exit form đóng file hỏi lưu file trước khi đóng
       */
        private void MF_BHX_FormClosing(object sender, FormClosingEventArgs e)
        {
            ErrFrm.FormClosingEvent(sender, e);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void currentChangeTitle_MouseDown(object sender, MouseEventArgs e)
        {
            Releasecapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}