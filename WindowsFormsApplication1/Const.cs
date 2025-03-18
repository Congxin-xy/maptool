using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Const
    {
        public const int MapFileLen = 3244;

        public const int EdgeNumMax = 100;
        public const int SpeedLimNum = 100;
        public const int RampNum = 100;
        public const int EdgeTagNum = 100;
        public const int EdgePoliceMarkNumx = 100;
        public const int StopPointNumMax = 100;

        public const UInt16 EmapInf_Tag = 0;

        public UInt16[] EdgeInf_Tag = new ushort[100];
        public UInt16[] EdgeInf_ConReZero_Tag = new ushort[100];
        public UInt16[] EdgeInf_ConReOne_Tag = new ushort[100];
        public UInt16[] EdgeInf_EdgeSpeLimSta_Tag = new ushort[100];
        public UInt16[] EdgeInf_EdgeRamp_Tag = new ushort[100];
        public UInt16[] EdgeInf_EdgeTag_Tag = new ushort[100];
        public UInt16[] EdgeInf_EdgePoliceMark_Tag = new ushort[100];
        public UInt16[] EdgeInf_EdgeStopPoint_Tag = new ushort[100];

        public string EmapInf_Text = "电子地图信息";
        public string[] EdgeInf_Text = new string[100];
        public string[] EdgeInf_ConReZero_Text = new string[100];
        public string[] EdgeInf_ConReOne_Text = new string[100];
        public string[] EdgeInf_EdgeSpeLimSta_Text = new string[100];
        public string[] EdgeInf_EdgeRamp_Text = new string[100];
        public string[] EdgeInf_EdgeTag_Text = new string[100];
        public string[] EdgeInf_EdgePoliceMark_Text = new string[100];
        public string[] EdgeInf_EdgeStopPoint_Text = new string[100];

        public void EdgeInfInit()
        {
            int i;

            for (i = 0; i < EdgeNumMax; i++)
            {
                EdgeInf_Tag[i] = (UInt16)(i + 1);
                EdgeInf_ConReZero_Tag[i] = (UInt16)((i + 1) * 10 + 1);
                EdgeInf_ConReOne_Tag[i] = (UInt16)((i + 1) * 10 + 2);
                EdgeInf_EdgeSpeLimSta_Tag[i] = (UInt16)((i + 1) * 10 + 3);
                EdgeInf_EdgeRamp_Tag[i] = (UInt16)((i + 1) * 10 + 4);
                EdgeInf_EdgeTag_Tag[i] = (UInt16)((i + 1) * 10 + 5);
                EdgeInf_EdgePoliceMark_Tag[i] = (UInt16)((i + 1) * 10 + 6);
                EdgeInf_EdgeStopPoint_Tag[i] = (UInt16)((i + 1) * 10 + 7);
                EdgeInf_Text[i] = "Edge" + (i + 1) + "信息";
                EdgeInf_ConReZero_Text[i] = "连接关系（0方向）";
                EdgeInf_ConReOne_Text[i] = "连接关系（1方向）";
                EdgeInf_EdgeSpeLimSta_Text[i] = "静态限速信息+";
                EdgeInf_EdgeRamp_Text[i] = "坡道信息+";
                EdgeInf_EdgeTag_Text[i] = "Tag信息+";
                EdgeInf_EdgePoliceMark_Text[i] = "警冲标信息+";
                EdgeInf_EdgeStopPoint_Text[i] = "停车点信息+";
            }
        }

    }
}
