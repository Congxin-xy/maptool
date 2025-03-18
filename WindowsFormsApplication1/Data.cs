using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{     
    class Data
    {
        public string path = null;

        Crc crc = new Crc();
        State state = new State();
        int i, j;
        public void DataMake(Map.MapFile map)//新建文件
        {
            BinaryWriter bw = null;
            if (path == null)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();

                fbd.Description = "请选择文件存放路径";

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    path = fbd.SelectedPath + "\\" + "Emap_pro.bin";
                }
            }
            if (path != null)
            {
                try
                {
                    bw = new BinaryWriter(new FileStream(path, FileMode.Create));
                }
                catch (IOException a)
                {
                    MessageBox.Show(a.Message);
                    return;
                }
                try
                {
                    bw.Write(DataEncode(map));
                }
                catch (IOException a)
                {
                    MessageBox.Show(a.Message);
                    return;
                }
                bw.Close();
            }
        }
        public Map.MapFile DataRead()
        {
            BinaryReader br = null;

            Byte[] read = new Byte[Const.MapFileLen];

            Map.MapFile MapFile = new Map.MapFile();
            int i;
            MapFile = new Map.MapFile();
            MapFile.EmapInfo.EdgeInfo = new Map.EdgeInf[Const.EdgeNumMax];
            for (i = 0; i < Const.EdgeNumMax; i++)
            {
                MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimInfo = new Map.SpeedLimInf[Const.SpeedLimNum];
                MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampInfo = new Map.RampInf[Const.RampNum];
                MapFile.EmapInfo.EdgeInfo[i].EdgeTag.TagInfo = new Map.TagInf[Const.EdgeTagNum];
                MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.PoliceMarkInfo = new Map.PoliceMarkInf[Const.EdgePoliceMarkNumx];
                MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointInfo = new Map.StopPointInf[Const.StopPointNumMax];
            }
            MapFile.Fh.FileLen = 0;

            if (path != null)
            {
                try
                {
                    br = new BinaryReader(new FileStream(path, FileMode.Open));
                }
                catch (IOException a)
                {
                    MessageBox.Show(a.Message);
                    //return;
                }
                if (br.BaseStream.Length <= Const.MapFileLen)
                {
                    try
                    {
                        read = br.ReadBytes(Const.MapFileLen);
                    }

                    catch (IOException a)
                    {
                        MessageBox.Show(a.Message);
                    }
                }
                else
                {
                    MessageBox.Show("文件长度不正确，打开失败");
                }
                br.Close();
                MapFile = DataDecode(read);
            }

            return MapFile;
        }

        private byte[] DataEncode(Map.MapFile cwf)//编码，相当于生成文件
        {
            int Lengths = 0;

            byte[] EncodeData = new byte[Const.MapFileLen - 4];
            byte[] EncodeDataCrc = new byte[Const.MapFileLen];

            /*文件头*/
            Array.Copy(System.BitConverter.GetBytes(cwf.Fh.FileMark), 0, EncodeData, Lengths, 4);
            Array.Reverse(EncodeData, Lengths, 4);//转为大端格式
            Lengths += 4;

            Array.Copy(System.BitConverter.GetBytes(cwf.Fh.FileLen), 0, EncodeData, Lengths, 4);
            Array.Reverse(EncodeData, Lengths, 4);//转为大端格式
            Lengths += 4;

            Array.Copy(System.BitConverter.GetBytes(cwf.Fh.FileVer), 0, EncodeData, Lengths, 4);
            Array.Reverse(EncodeData, Lengths, 4);//转为大端格式
            Lengths += 4;            

            Array.Copy(System.BitConverter.GetBytes(cwf.Fh.FilePutTime), 0, EncodeData, Lengths, 4);
            Array.Reverse(EncodeData, Lengths, 4);//转为大端格式
            Lengths += 4;

            /*电子地图信息*/
            /*edge个数*/
            Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeNum), 0, EncodeData, Lengths, 4);
            Array.Reverse(EncodeData, Lengths, 4);//转为大端格式
            Lengths += 4;

            for (int i = 0; i < cwf.EmapInfo.EdgeNum; i++)
            {
                /*Edge ID*/
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeInfoHead.EdgeId), 0, EncodeData, Lengths, 2);
                Array.Reverse(EncodeData, Lengths, 2);//转为大端格式
                Lengths += 2;

                /*OC   ID*/
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeInfoHead.OcId), 0, EncodeData, Lengths, 2);
                Array.Reverse(EncodeData, Lengths, 2);//转为大端格式
                Lengths += 2;

                /*Edge长度 */
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeInfoHead.EdgeLen), 0, EncodeData, Lengths, 4);
                Array.Reverse(EncodeData, Lengths, 4);//转为大端格式
                Lengths += 4;

                /*Edge连接关系(0方向)*/
                /*连接的道岔ID*/
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].ConReZero.TurId), 0, EncodeData, Lengths, 2);
                Array.Reverse(EncodeData, Lengths, 2);//转为大端格式
                Lengths += 2;

                /*连接的Edge ID（直股） */
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].ConReZero.EdgeIdStr), 0, EncodeData, Lengths, 2);
                Array.Reverse(EncodeData, Lengths, 2);//转为大端格式
                Lengths += 2;

                /*连接的Edge端号（直股）*/
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].ConReZero.EdgeTermNumStr), 0, EncodeData, Lengths, 1);
                Lengths++;

                /*连接的Edge ID（弯股） */
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].ConReZero.EdgeIdCur), 0, EncodeData, Lengths, 2);
                Array.Reverse(EncodeData, Lengths, 2);//转为大端格式
                Lengths += 2;

                /*连接的Edge端号（弯股）*/
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].ConReZero.EdgeTermNumCur), 0, EncodeData, Lengths, 1);
                Lengths++;
                
                /*Edge连接关系(1方向)*/
                /*连接的道岔ID*/
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].ConReOne.TurId), 0, EncodeData, Lengths, 2);
                Array.Reverse(EncodeData, Lengths, 2);//转为大端格式
                Lengths += 2;

                /*连接的Edge ID（直股） */
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].ConReOne.EdgeIdStr), 0, EncodeData, Lengths, 2);
                Array.Reverse(EncodeData, Lengths, 2);//转为大端格式
                Lengths += 2;

                /*连接的Edge端号（直股）*/
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].ConReOne.EdgeTermNumStr), 0, EncodeData, Lengths, 1);
                Lengths++;

                /*连接的Edge ID（弯股） */
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].ConReOne.EdgeIdCur), 0, EncodeData, Lengths, 2);
                Array.Reverse(EncodeData, Lengths, 2);//转为大端格式
                Lengths += 2;

                /*连接的Edge端号（弯股）*/
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].ConReOne.EdgeTermNumCur), 0, EncodeData, Lengths, 1);
                Lengths++;

                /*Edge包含的静态限速信息*/
                /*限速区段个数*/
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum), 0, EncodeData, Lengths, 2);
                Array.Reverse(EncodeData, Lengths, 2);//转为大端格式
                Lengths += 2;

                for (int j = 0; j < cwf.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum; j++)
                {
                    /*限速区段起点偏移*/
                    Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimInfo[j].SpeedLimStart), 0, EncodeData, Lengths, 4);
                    Array.Reverse(EncodeData, Lengths, 4);//转为大端格式
                    Lengths += 4;

                    /*限速区段终点偏移*/
                    Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimInfo[j].SpeedLimEnd), 0, EncodeData, Lengths, 4);
                    Array.Reverse(EncodeData, Lengths, 4);//转为大端格式
                    Lengths += 4;

                    /*限速值*/
                    Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimInfo[j].SpeedLim), 0, EncodeData, Lengths, 4);
                    Array.Reverse(EncodeData, Lengths, 4);//转为大端格式
                    Lengths += 4;
                }

                /*Edge包含的坡道信息*/
                /*坡道区段个数*/
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum), 0, EncodeData, Lengths, 2);
                Array.Reverse(EncodeData, Lengths, 2);//转为大端格式
                Lengths += 2;

                for (int j = 0; j < cwf.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum; j++)
                {
                    /*坡道区段起点偏移*/
                    Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeRamp.RampInfo[j].RampStart), 0, EncodeData, Lengths, 4);
                    Array.Reverse(EncodeData, Lengths, 4);//转为大端格式
                    Lengths += 4;

                    /*坡道区段终点偏移*/
                    Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeRamp.RampInfo[j].RampEnd), 0, EncodeData, Lengths, 4);
                    Array.Reverse(EncodeData, Lengths, 4);//转为大端格式
                    Lengths += 4;

                    /*从起点到终点的坡度*/
                    Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeRamp.RampInfo[j].Slope), 0, EncodeData, Lengths, 4);
                    Array.Reverse(EncodeData, Lengths, 4);//转为大端格式
                    Lengths += 4;
                }

                /*Edge包含的Tag信息*/
                /*Tag 个数*/
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum), 0, EncodeData, Lengths, 2);
                Array.Reverse(EncodeData, Lengths, 2);//转为大端格式
                Lengths += 2;

                for (int j = 0; j < cwf.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum; j++)
                {
                    /*Tag  ID*/
                    Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeTag.TagInfo[j].TagId), 0, EncodeData, Lengths, 4);
                    Array.Reverse(EncodeData, Lengths, 4);//转为大端格式
                    Lengths += 4;

                    /*Tag中心偏移*/
                    Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeTag.TagInfo[j].TagOffset), 0, EncodeData, Lengths, 4);
                    Array.Reverse(EncodeData, Lengths, 4);//转为大端格式
                    Lengths += 4;
                }

                /*Edge包含的警冲标信息*/
                /*警冲标个数*/
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum), 0, EncodeData, Lengths, 2);
                Array.Reverse(EncodeData, Lengths, 2);//转为大端格式
                Lengths += 2;

                for (int j = 0; j < cwf.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum; j++)
                {
                    /*防护方向*/
                    Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgePoliceMark.PoliceMarkInfo[j].ProtDir), 0, EncodeData, Lengths, 1);
                    Lengths++;                    

                    /*警冲标偏移*/
                    Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgePoliceMark.PoliceMarkInfo[j].PoliceMarkOffset), 0, EncodeData, Lengths, 4);
                    Array.Reverse(EncodeData, Lengths, 4);//转为大端格式
                    Lengths += 4;
                }

                /*Edge包含的停车点信息*/
                /*停车点个数*/
                Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum), 0, EncodeData, Lengths, 2);
                Array.Reverse(EncodeData, Lengths, 2);//转为大端格式
                Lengths += 2;

                for (int j = 0; j < cwf.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum; j++)
                {
                    /*停车点ID*/
                    Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointInfo[j].StopPointId), 0, EncodeData, Lengths, 2);
                    Array.Reverse(EncodeData, Lengths, 2);//转为大端格式
                    Lengths += 2;

                    /*停车点偏移*/
                    Array.Copy(System.BitConverter.GetBytes(cwf.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointInfo[j].StopPointOffset), 0, EncodeData, Lengths, 4);
                    Array.Reverse(EncodeData, Lengths, 4);//转为大端格式
                    Lengths += 4;
                }
            }
          
            Array.Copy(EncodeData, 0, EncodeDataCrc, 0, 4);
            Array.Copy(System.BitConverter.GetBytes(crc.CrcValue(EncodeData, Lengths)), 0, EncodeDataCrc, 4, 4);
            Array.Reverse(EncodeDataCrc, 4, 4);//转为大端格式
            Array.Copy(EncodeData, 4, EncodeDataCrc, 8, Const.MapFileLen - 8);

            return EncodeDataCrc;
        }

        private Map.MapFile DataDecode(byte[] read)//解码，相当于打开文件
        {
            int Lengths = 0;
            byte[] readCrc = new byte[Const.MapFileLen - 4];
            byte[] read1 = new byte[Const.MapFileLen];
            Array.Copy(read, 0, read1, 0, Const.MapFileLen);

            Map.MapFile cwf_r = new Map.MapFile();
            /*cwf_r.RteCfg.RouteCfg = new Gw.RouteInfo[Const.RouteNumMax];
            cwf_r.InEqpcfg.MachCfg = new Gw.FirstMachConfig[Const.MachineNumMax];
            for (i = 0; i < Const.MachineNumMax; i++)
            {
                cwf_r.InEqpcfg.MachCfg[i].MachLink.LinkCfg = new Gw.LinkInfo[Const.LinkNumMax];
            }
            cwf_r.OutEqpcfg.AtsCfg = new Gw.AtsInfo[Const.AtsNumMax];
            cwf_r.OutEqpcfg.OutOthCfg.OutOtherCfg = new Gw.OutEquipInfo[Const.OutOthEquNumMax];*/
            /*读文件头*/
            Array.Reverse(read, Lengths, 4);//转为大端格式
            cwf_r.Fh.FileMark = System.BitConverter.ToUInt32(read, Lengths);
            Lengths += 4;

            Array.Reverse(read, Lengths, 4);//转为大端格式
            cwf_r.Fh.FileLen = System.BitConverter.ToUInt32(read, Lengths);
            Lengths += 4;

            Array.Reverse(read, Lengths, 4);//转为大端格式
            cwf_r.Fh.FileVer = System.BitConverter.ToUInt32(read, Lengths);
            Lengths += 4;

            Array.Reverse(read, Lengths, 4);//转为大端格式
            cwf_r.Fh.FilePutTime = System.BitConverter.ToUInt32(read, Lengths);
            Lengths += 4;

            /*edge个数*/
            Array.Reverse(read, Lengths, 4);//转为大端格式
            cwf_r.EmapInfo.EdgeNum = System.BitConverter.ToInt16(read, Lengths);
            Lengths += 4;

            /*edge信息*/
            for (i = 0; i < cwf_r.EmapInfo.EdgeNum; i++)
            {
                /*Edge ID*/ 
                Array.Reverse(read, Lengths, 2);//转为大端格式
                cwf_r.EmapInfo.EdgeInfo[i].EdgeInfoHead.EdgeId = System.BitConverter.ToInt16(read, Lengths);
                Lengths += 2;

                /*OC   ID*/
                Array.Reverse(read, Lengths, 2);//转为大端格式
                cwf_r.EmapInfo.EdgeInfo[i].EdgeInfoHead.OcId = System.BitConverter.ToInt16(read, Lengths);
                Lengths += 2;

                /*Edge长度*/
                Array.Reverse(read, Lengths, 4);//转为大端格式
                cwf_r.EmapInfo.EdgeInfo[i].EdgeInfoHead.EdgeLen = System.BitConverter.ToUInt32(read, Lengths);
                Lengths += 4;

                /*Edge连接关系（0方向）*/
                /*连接的道岔ID*/
                Array.Reverse(read, Lengths, 2);//转为大端格式
                cwf_r.EmapInfo.EdgeInfo[i].ConReZero.TurId = System.BitConverter.ToInt16(read, Lengths);
                Lengths += 2;

                /*连接的Edge ID（直股）*/
                Array.Reverse(read, Lengths, 2);//转为大端格式
                cwf_r.EmapInfo.EdgeInfo[i].ConReZero.EdgeIdStr = System.BitConverter.ToInt16(read, Lengths);
                Lengths += 2;

                /*连接的Edge端号（直股）*/
                cwf_r.EmapInfo.EdgeInfo[i].ConReZero.EdgeTermNumStr = read[Lengths];
                Lengths++;

                /*连接的Edge ID（弯股）*/
                Array.Reverse(read, Lengths, 2);//转为大端格式
                cwf_r.EmapInfo.EdgeInfo[i].ConReZero.EdgeIdCur = System.BitConverter.ToInt16(read, Lengths);
                Lengths += 2;

                /*连接的Edge端号（弯股）*/
                cwf_r.EmapInfo.EdgeInfo[i].ConReZero.EdgeTermNumCur = read[Lengths];
                Lengths++;

                /*Edge连接关系（1方向）*/
                /*连接的道岔ID*/
                Array.Reverse(read, Lengths, 2);//转为大端格式
                cwf_r.EmapInfo.EdgeInfo[i].ConReOne.TurId = System.BitConverter.ToInt16(read, Lengths);
                Lengths += 2;

                /*连接的Edge ID（直股）*/
                Array.Reverse(read, Lengths, 2);//转为大端格式
                cwf_r.EmapInfo.EdgeInfo[i].ConReOne.EdgeIdStr = System.BitConverter.ToInt16(read, Lengths);
                Lengths += 2;

                /*连接的Edge端号（直股）*/
                cwf_r.EmapInfo.EdgeInfo[i].ConReOne.EdgeTermNumStr = read[Lengths];
                Lengths++;

                /*连接的Edge ID（弯股）*/
                Array.Reverse(read, Lengths, 2);//转为大端格式
                cwf_r.EmapInfo.EdgeInfo[i].ConReOne.EdgeIdCur = System.BitConverter.ToInt16(read, Lengths);
                Lengths += 2;

                /*连接的Edge端号（弯股）*/
                cwf_r.EmapInfo.EdgeInfo[i].ConReOne.EdgeTermNumCur = read[Lengths];
                Lengths++;

                /*Edge包含的静态限速信息*/
                /*限速区段个数*/
                Array.Reverse(read, Lengths, 2);//转为大端格式
                cwf_r.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum = System.BitConverter.ToInt16(read, Lengths);
                Lengths += 2;

                for (int j = 0; j < cwf_r.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum; j++)
                {
                    /*限速区段起点偏移*/
                    Array.Reverse(read, Lengths, 4);//转为大端格式
                    cwf_r.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimInfo[j].SpeedLimStart = System.BitConverter.ToInt32(read, Lengths);
                    Lengths += 4;

                    /*限速区段终点偏移*/
                    Array.Reverse(read, Lengths, 4);//转为大端格式
                    cwf_r.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimInfo[j].SpeedLimEnd = System.BitConverter.ToInt32(read, Lengths);
                    Lengths += 4;

                    /*限速值*/
                    Array.Reverse(read, Lengths, 4);//转为大端格式
                    cwf_r.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimInfo[j].SpeedLim = System.BitConverter.ToInt32(read, Lengths);
                    Lengths += 4;
                }

                /*Edge包含的坡道信息*/
                /*坡道区段个数*/
                Array.Reverse(read, Lengths, 2);//转为大端格式
                cwf_r.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum = System.BitConverter.ToInt16(read, Lengths);
                Lengths += 2;

                for (int j = 0; j < cwf_r.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum; j++)
                {
                    /*坡道区段起点偏移*/
                    Array.Reverse(read, Lengths, 4);//转为大端格式
                    cwf_r.EmapInfo.EdgeInfo[i].EdgeRamp.RampInfo[j].RampStart = System.BitConverter.ToInt32(read, Lengths);
                    Lengths += 4;

                    /*坡道区段终点偏移*/
                    Array.Reverse(read, Lengths, 4);//转为大端格式
                    cwf_r.EmapInfo.EdgeInfo[i].EdgeRamp.RampInfo[j].RampEnd = System.BitConverter.ToInt32(read, Lengths);
                    Lengths += 4;

                    /*从起点到终点的坡度*/
                    Array.Reverse(read, Lengths, 4);//转为大端格式
                    cwf_r.EmapInfo.EdgeInfo[i].EdgeRamp.RampInfo[j].Slope = System.BitConverter.ToInt32(read, Lengths);
                    Lengths += 4;
                }

                /*Edge包含的Tag信息*/
                /*Tag 个数*/
                Array.Reverse(read, Lengths, 2);//转为大端格式
                cwf_r.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum = System.BitConverter.ToInt16(read, Lengths);
                Lengths += 2;

                for (int j = 0; j < cwf_r.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum; j++)
                {
                    /*Tag  ID*/
                    Array.Reverse(read, Lengths, 4);//转为大端格式
                    cwf_r.EmapInfo.EdgeInfo[i].EdgeTag.TagInfo[j].TagId = System.BitConverter.ToInt32(read, Lengths);
                    Lengths += 4;

                    /*Tag中心偏移*/
                    Array.Reverse(read, Lengths, 4);//转为大端格式
                    cwf_r.EmapInfo.EdgeInfo[i].EdgeTag.TagInfo[j].TagOffset = System.BitConverter.ToInt32(read, Lengths);
                    Lengths += 4;
                }

                /*Edge包含的警冲标信息*/
                /*警冲标个数*/
                Array.Reverse(read, Lengths, 2);//转为大端格式
                cwf_r.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum = System.BitConverter.ToInt16(read, Lengths);
                Lengths += 2;

                for (int j = 0; j < cwf_r.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum; j++)
                {
                    /*防护方向*/
                    cwf_r.EmapInfo.EdgeInfo[i].EdgePoliceMark.PoliceMarkInfo[j].ProtDir = read[Lengths];
                    Lengths++;

                    /*警冲标偏移*/
                    Array.Reverse(read, Lengths, 4);//转为大端格式
                    cwf_r.EmapInfo.EdgeInfo[i].EdgePoliceMark.PoliceMarkInfo[j].PoliceMarkOffset = System.BitConverter.ToInt32(read, Lengths);
                    Lengths += 4;
                }

                 /*Edge包含的停车点信息*/
                /*停车点个数*/
                Array.Reverse(read, Lengths, 2);//转为大端格式
                cwf_r.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum = System.BitConverter.ToInt16(read, Lengths);
                Lengths += 2;

                for (int j = 0; j < cwf_r.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum; j++)
                {
                    /*停车点ID  */
                    Array.Reverse(read, Lengths, 2);//转为大端格式
                    cwf_r.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointInfo[j].StopPointId = System.BitConverter.ToInt16(read, Lengths);
                    Lengths += 2;

                    /*停车点偏移*/
                    Array.Reverse(read, Lengths, 4);//转为大端格式
                    cwf_r.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointInfo[j].StopPointOffset = System.BitConverter.ToInt32(read, Lengths);
                    Lengths += 4;
                }
            }
           
            //CRC32校验
            Array.Copy(read1, 0, readCrc, 0, 4);
            Array.Copy(read1, 8, readCrc, 4, Const.MapFileLen - 8);
            if (crc.CrcValue(readCrc, Const.MapFileLen - 4) == cwf_r.Fh.Crc)
            {

            }
            else
            {
                MessageBox.Show("CRC校验不一致", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return cwf_r;
        }
    }
}
