using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Map
    {
        public struct FileHead
        {
            public UInt32 FileMark;             /*文件标识*/
            public UInt32 Crc;                  /*CRC校验*/
            public UInt32 FileLen;              /*文件长度*/
            public UInt32 FileVer;              /*文件版本*/
            public UInt32 FilePutTime;          /*文件发布时间*/
        }

        public struct EdgeInfHead
        {
            public Int16 EdgeId;          /*Edge ID */
            public Int16 OcId;            /*OC   ID */
            public UInt32 EdgeLen;        /*Edge长度*/
        }

        public struct ConnectRelation
        {
            public Int16 TurId;           /*连接的道岔ID          */
            public Int16 EdgeIdStr;       /*连接的Edge ID（直股） */
            public Byte EdgeTermNumStr;   /*连接的Edge端号（直股）*/
            public Int16 EdgeIdCur;       /*连接的Edge ID（弯股） */
            public Byte EdgeTermNumCur;   /*连接的Edge端号（弯股）*/
        }

        public struct SpeedLimInf
        {
            public Int32 SpeedLimStart;   /*限速区段起点偏移*/
            public Int32 SpeedLimEnd;     /*限速区段终点偏移*/
            public Int32 SpeedLim;        /*限速值          */
        }

        public struct EdgeSpeedLimStatic
        {
            public Int16 SpeedLimNum;          /*限速区段个数    */
            public SpeedLimInf[] SpeedLimInfo; /*单个限速区段信息*/
        }

        public struct RampInf
        {
            public Int32 RampStart;  /*坡道区段起点偏移  */
            public Int32 RampEnd;    /*坡道区段终点偏移  */
            public Int32 Slope;      /*从起点到终点的坡度*/
        }

        public struct EdgeRampInf
        {
            public Int16 RampNum;      /*坡道区段个数    */
            public RampInf[] RampInfo; /*单个坡道区段信息*/
        }

        public struct TagInf
        {
            public Int32 TagId;      /*Tag  ID    */
            public Int32 TagOffset;  /*Tag中心偏移*/
        }

        public struct EdgeTagInf
        {
            public Int16 EdgeTagNum;    /*Tag 个数   */
            public TagInf[] TagInfo;    /*单个Tag信息*/
        }

        public struct PoliceMarkInf
        {
            public Byte ProtDir;            /*防护方向  */
            public Int32 PoliceMarkOffset;  /*警冲标偏移*/
        }

        public struct EdgePolilceMarkInf
        {
            public Int16 EdgePoliceMarkNum;         /*警冲标个数    */
            public PoliceMarkInf[] PoliceMarkInfo;  /*单个警冲标信息*/
        }

        public struct StopPointInf
        {
            public Int16 StopPointId;      /*停车点ID  */
            public Int32 StopPointOffset;  /*停车点偏移*/
        }

        public struct EdgeStopPointInf
        {
            public Int16 StopPointNum;            /*停车点个数    */
            public StopPointInf[] StopPointInfo;  /*单个停车点信息*/
        }

        public struct EdgeInf
        {
            public EdgeInfHead EdgeInfoHead;            /*EdgeId/OcId/Edge长度  */
            public ConnectRelation ConReZero;           /*Edge连接关系（0方向） */
            public ConnectRelation ConReOne;            /*Edge连接关系（1方向） */
            public EdgeSpeedLimStatic EdgeSpeLimSta;    /*Edge包含的静态限速信息*/
            public EdgeRampInf EdgeRamp;                /*Edge包含的坡道信息    */
            public EdgeTagInf EdgeTag;                  /*Edge包含的Tag信息     */
            public EdgePolilceMarkInf EdgePoliceMark;   /*Edge包含的警冲标信息  */
            public EdgeStopPointInf EdgeStopPoint;      /*Edge包含的停车点信息  */
        }

        public struct EmapInf
        {
            public Int16 EdgeNum;         /*edge个数*/
            public EdgeInf[] EdgeInfo;    /*edge信息*/
        }

        public struct MapFile
        {
            public FileHead Fh;               /*文件头*/
            public EmapInf EmapInfo;          /*电子地图信息*/
        }
    }
}
