using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Conv
    {
        public string ToFileVer(UInt32 data)//软件版本
        {
            string str = null;
            byte A = (byte)(data >> 24);
            byte B = (byte)((data >> 16)&0x000000FF);
            byte C = (byte)((data >> 6) & 0x000003FF);
            byte D = (byte)(data & 0x0000003F);

            str = "V"+A.ToString()+"."+B.ToString()+"."+C.ToString()+"."+D.ToString();
            return str;
        }

        public string ToTime(UInt32 data)//软件发布时间
        {
            string str  = null;
            UInt32 Year = (data >> 26) + 2015;
            byte Month  = (byte)((data >> 22) & 0x0000000F);
            byte Day =    (byte)((data >> 17) & 0x0000001F);
            byte Hour   = (byte)((data >> 12) & 0x0000001F);
            byte Minute = (byte)((data >> 6)  & 0x0000003F);
            byte Second = (byte)(data & 0x0000003F);

            str = Year.ToString() + "/" + Month.ToString() + "/" + Day.ToString() + " " + Hour.ToString() + ":" + Minute.ToString() + ":" + Second.ToString();
            return str;
        }
       
        public UInt32 FileVerTo(object obj)//文件版本
        {
            string z = null, a = null, b = null, c = null, d = null;
            string disjunctiveStr = ".V";
            char[] deimiter = disjunctiveStr.ToCharArray();
            string[] starVer = null;
            uint A, B, C, D;
            for (int i = 1; i < 6; i++)
            {
                starVer = Convert.ToString(obj).Split(deimiter, i);
            }
            try
            {
                 z = starVer[0].ToString();
                 a = starVer[1].ToString();
                 b = starVer[2].ToString();
                 c = starVer[3].ToString();
                 d = starVer[4].ToString();
            }
            catch(Exception)
            {
                MessageBox.Show("请输入有效的版本号","警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            try
            {
                A = uint.Parse(a);
                B = uint.Parse(b);
                C = uint.Parse(c);
                D = uint.Parse(d);
            }
            catch (Exception)
            {
                MessageBox.Show("请输入有效的版本号", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
            UInt32 Ver = A << 24;
            Ver += B << 16;
            Ver += C << 6;
            Ver += D;
            return Ver;
        }               
    }
}
