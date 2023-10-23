using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT8_LISTBOX.BatLoiControl
{
    internal class BatLoiListBox
    {

        public static void ListBox_ThongBao_ChuaChonItem(string messsage = "Chưa chọn item!")
        {
            MessageBox.Show(messsage, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
