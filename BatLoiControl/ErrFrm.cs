using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT8_LISTBOX.BatLoiControl
{
    internal class ErrFrm
    {
        /// <summary>
        /// Required field
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static void Required(object sender, ErrorProvider errorProvider, string requiredMessage = "Please fill the required field")
        {
            Control currentControl = (Control)sender;
            if (currentControl != null && currentControl.Text.Length > 0 && requiredMessage.Length > 0)
            {
                errorProvider.SetError(currentControl, requiredMessage);
            }
            else
            {
                errorProvider.SetError(currentControl, "");
            }
        }
        public static bool FormClosingEvent(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn đóng form hay không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No)
            {
                // hủy đóng form
                e.Cancel = true;
                return false;
            }
            else
            {
                // chạy tiếp this.close
                return true;
            }
        }

    }
}
