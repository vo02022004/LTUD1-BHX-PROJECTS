
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ComboBox = System.Windows.Forms.ComboBox;

namespace BT8_LISTBOX.BatLoiControl
{
    /// <summary>
    /// Phải vừa đổi được màu html sang màu hệ thống, 
    /// phải vừa có màu mặc định để gọi cho lẹ. 
    /// phải vừa dùng được cả trong đầu vào các hàm khác nhau
    /// </summary>
    public struct ColorErrors
    {
        //Trong c# cách để thêm 1 màu mới
        //tetradic #E25966 #AAE259 #59E2D5 #9159E2
        public static Color err_textbox_required = ColorTranslator.FromHtml("#E25966");// "light red"        
        public static Color err_textbox_notext = ColorTranslator.FromHtml("#AAE259");// "dark red"
        public static Color err_textbox_nonumber = ColorTranslator.FromHtml("#59E2D5");// "light green" 
        public static Color err_textbox_nospace = ColorTranslator.FromHtml("#9159E2");// "????" 

        //analogous #E29159 #E29159 #E259AA
        public static Color err_textbox_nocontains_exist_item = ColorTranslator.FromHtml("#E29159");// "????" 
        public static Color err_textbox_minmax_length = ColorTranslator.FromHtml("#E259AA");// "????" 
        //triadic #E25966 #66E259   #5966E2
        internal static Color err_textbox_nosymbol = ColorTranslator.FromHtml("#5966E2");// "????"
    }
    public class ErrTxt
    {

        /// <summary>
        /// Chặn nhập số
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Return true if is number</returns>
        public static bool NoNumber_KeyPress(KeyPressEventArgs e)
        {
            Regex regex = new Regex("^[0-9]");
            bool isNumber = regex.IsMatch(e.KeyChar.ToString());
            if (isNumber)
            {
                // Không cho phép gõ tiếp
                return true;
            }
            else
            {
                // Cho phép gõ tiếp
                return false;
            }
        }
        /// <summary>
        /// Không nhập chữ key press
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static bool NoText_KeyPress(KeyPressEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z]");
            bool isText = regex.IsMatch(e.KeyChar.ToString());
            if (isText)
            {
                // Không cho phép gõ tiếp
                return true;
            }
            else
            {
                // Cho phép gõ tiếp
                return false;
            }
        }

        /// <summary>
        /// Cho phép các ký tự số và ký tự điều khiển như backspace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>return true if have symbol</returns>
        internal static bool NoSymbol_KeyPress(KeyPressEventArgs e)
        {
            // Kiểm tra tất cả các ký tự không phải là chữ và số
            Regex regex = new Regex("^[!@#$%^&*()_\\-+=*/]");
            bool isSymbol = regex.IsMatch(e.KeyChar.ToString());
            if (isSymbol)
            {
                // Không cho phép gõ tiếp
                return true;
            }
            else
            {
                // Cho phép gõ tiếp
                return false;
            }
        }
        /// <summary>
        /// Ngừng nhập khoảng trắng textbox
        /// </summary>
        /// <param name="sender">Textbox sender</param>
        /// <param name="e">Nhấn xuống event</param>
        /// <returns>Return true if no have space</returns>
        public static bool NoSpace_KeyPress(KeyPressEventArgs e)
        {
            // Kiểm tra xem phím được nhấn có phải là khoảng trắng hay không
            Regex regex = new Regex("^[ ]");
            bool isSpace = regex.IsMatch(e.KeyChar.ToString());
            if (isSpace)
            {
                // Không cho phép gõ tiếp
                return true;
            }
            else
            {   // Cho phép gõ tiếp
                return false;
            }
        }
        /// <summary>
        /// Không nhập số
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>return true if  have number</returns>
        public static bool NoNumber_TextChange(object sender)
        {
            Control c = (Control)sender;
            Regex regex = new Regex("^[0-9]");
            bool isNumber = regex.IsMatch(c.Text.ToString());
            // check input string
            if (isNumber)
            {
                return true;//phat hien nhap number
            }
            // nhap ok!
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Không nhập text
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static bool NoText_TextChange(object sender)
        {
            Control c = (Control)sender;
            Regex regex = new Regex("^[a-zA-Z]");
            bool isText = regex.IsMatch(c.Text.ToString());
            // check input string
            if (isText)
            {
                return true;//phat hien nhap text
            }
            // nhap ok!
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Không nhập symbol !@#$%^&*()_+/*=-
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>return true if no have symbol</returns>
        public static bool NoSymbol_TextChanged(object sender)
        {
            Control c = (Control)sender;
            Regex regex = new Regex("^[!@#$%^&*()_\\-+=*/]");
            bool isSymbol = regex.IsMatch(c.Text.ToString());
            // check input string
            if (isSymbol)
            {
                return true;
            }
            // nhap ok!
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Không nhập space
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>true if no space</returns>
        public static bool NoSpace_TextChange(object sender)
        {
            TextBox c = (TextBox)sender;
            Regex regex = new Regex("^[ ]");
            bool isSpace = regex.IsMatch(c.Text.ToString());
            if (isSpace)
            {
                return true;
            }
            // nhap ok!
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Không nhập đủ số lượng kí tự
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns>return true if enough length</returns>
        public static bool NoMinMax_TextChanged(object sender, int min = 3, int max = 16)
        {
            Control c = (Control)sender;
            string patern = "a-zA-Z0-9 !@#$%^&*()_+*/-eéèẻẽẹêếềễểệaáàảãạăắằẵẳặâấầẩẫậoóòỏõọơớờỡởợôốồổỗộuúùũủụưứừữửựiíìỉĩịyýỳỷỹỵđEÉÈẺẼẸÊẾỀỄỂỆAÁÀẢÃẠĂẮẰẴẲẶÂẤẦẨẪẬOÓÒỎÕỌƠỚỜỠỞỢÔỐỒỔỖỘUÚÙŨỦỤƯỨỪỮỬỰIÍÌỈĨỊYÝỲỶỸỴĐ";
            Regex regex = new Regex(@"^[" + patern + "]" + "{" + min + "," + max + "}$");
            bool isMinMax = regex.IsMatch(c.Text.ToString());
            if (isMinMax)
            {
                //Phat hiện nhập chưa đủ kí tự
                return false;
            }
            // nhập ok!
            else
            {
                // nhập chính xác
                return true;
            }
        }
    }
}
