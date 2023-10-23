using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTUD1_MF_BHX.Helpers
{
    public class Utilities
    {
        public static ArrayList ConvertListboxToArrInt(ListBox lboNumber)
        {
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < lboNumber.Items.Count; i++)
            {
                int number = int.Parse(lboNumber.Items[i].ToString());

                arrayList.Add(number);
            }
            arrayList.ToArray();
            return arrayList;
        }
        public static MenuStrip CreateMenu(string[] arrMenuCha/*DanhSachMenuCha*/, string[] arrMenuCon /*DanhSachMenuCon*/, int index = -1/*Vị trí cài đặt menu con*/)
        {
            // khai bao bien
            MenuStrip menuStrip = new MenuStrip();
            ToolStripMenuItem[] menuToolStripArr = new ToolStripMenuItem[] { };
            //add menu cha vao menuToolStrip
            for (int i = 0; i < arrMenuCha.Length; i++)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                menuStrip.Items.Add(item);
            }
            // add menuToolStrip cha vao menustrip
            menuStrip.Items.AddRange(menuToolStripArr);

            if (index != -1)
            {
                //update menu con vao menustrip

                // Tạo một ToolStripMenuItem mới
                ToolStripMenuItem parentItem = new ToolStripMenuItem(menuStrip.Items[index].Name);

                // Tạo một số ToolStripMenuItem để thêm vào parentItem

                for (int i = 0; i < arrMenuCon.Length; i++)
                {
                    ToolStripMenuItem childItem1 = new ToolStripMenuItem(arrMenuCon[i]);
                    // Thêm các item con vào parentItem
                    parentItem.DropDownItems.Add(childItem1);
                }
                menuStrip.Items.Add(parentItem);
            }

            return menuStrip;
        }

        internal static Form DockMenuTop(MenuStrip menu, Form form)
        {
            menu.Dock = DockStyle.Top;

            form.Controls.Add(menu);

            return form;
        }
        public static Image GetImage(string imageName, ImageList imglist)
        {
            // Tạo một đối tượng Image từ file
            Image image = imglist.Images[imglist.Images.IndexOfKey(imageName)];
            return image;
        }
    }
}


