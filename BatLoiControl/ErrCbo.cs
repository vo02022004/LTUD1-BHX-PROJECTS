using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT8_LISTBOX.BatLoiControl
{
    internal class ErrCbo
    {

        /// <summary>
        /// TC1 Checkcombobox is null;
        /// return true: tất cả combobox đều có dữ liệu
        /// return false: có thể 1 combobox chưa có dữ liệu nào
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static bool Combobox_ArrItem_IsNull(Form form)
        {
            bool kqTrue = false;// gia sử như tất cả combobox đều không có dữ liệu
                                //1.chua chon
            foreach (Control c in form.Controls)
            {
                if (c is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)c;
                    if (comboBox == null)
                    {
                        return false;//combobox dau tien la null
                    }
                }
            }
            return kqTrue;
            /*
             * return true: tất cả combobox đều có dữ liệu
             * return false: có thể 1 combobox chưa có dữ liệu nào
             */
        }

        /// <summary>
        /// TC2 Check combobox is unselected
        /// return true: tất cả combobox đều được chọn
        /// return false: có thể 1 combobox chưa được chọn
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static bool Combobox_ArrSelectedIndex_Required(Form f)
        {
            bool kqTrue = false;// gia sử như tất cả combobox đều chưa được chọn
                                //1.chua chon
            foreach (Control c in f.Controls)
            {
                if (c is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)c;
                    // thực hiện thao tác với combox tại đây
                    int index = comboBox.SelectedIndex;
                    if (index >= 0)
                    {
                        kqTrue = true;// phát hiện combobox được chọn
                    }
                }
            }
            return kqTrue;
            /*
             * return true: tất cả combobox đều được chọn
             * return false: có thể 1 combobox chưa được chọn
             */
        }

        /// <summary>
        /// Kiểm tra sự tồn tại của mục chọn: Sử dụng phương thức Contains() để kiểm tra xem một mục chọn có tồn tại trong danh sách hay không.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static Label Combobox_Add_Item(object sender, KeyEventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            Label lblHint = new Label();
            lblHint.Dock = DockStyle.Bottom;

            if (e.KeyCode == Keys.Enter)/*Keydown enter*/
            {
                lblHint.Text = string.Empty;
                if (c.Items.Contains(c.Text))
                {
                    c.BackColor = ColorErrors.err_textbox_nocontains_exist_item;
                    lblHint.Text += c.Name + ";";
                }
                else
                {
                    c.Items.Add(c.Text);//remove red background error
                    c.SelectedIndex = c.Items.Count - 1;//remove yellow background error
                    lblHint.Text += "";
                    c.BackColor = Color.White;// normal white combobox no error
                }
            }
            return lblHint;
        }

        internal static bool SelectedIndex_Required(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            // thực hiện thao tác với combox tại đây
            int index = comboBox.SelectedIndex;
            if (index >= 0)
            {
                comboBox.BackColor = ColorErrors.err_textbox_required;
                return true;// phát hiện combobox được chọn
            }
            return false;
        }


    }
}
