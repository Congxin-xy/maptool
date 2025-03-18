using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class Excl
    {
        Conv convert = new Conv();
        int i,j;
        public void ShowDataToFileExcl(DataGridView dataGridView1, Map.FileHead cwf_r)
        {
            dataGridView1.Rows[0].Cells[0].Value = cwf_r.FileMark;                   //文件标识
            dataGridView1.Rows[0].Cells[1].Value = cwf_r.FileLen;                    //文件长度
            dataGridView1.Rows[0].Cells[2].Value = convert.ToFileVer(cwf_r.FileVer); //软件版本
            dataGridView1.Rows[0].Cells[3].Value = convert.ToTime(cwf_r.FilePutTime);//发布时间
            if (dataGridView1.Rows[0].Cells[0].Value.ToString() == "0xA5 0x5A 0xA5 0x5A")
            {
                dataGridView1.Rows[0].Cells[0].Style.ForeColor = Color.Blue;
            }
            else
            {
                dataGridView1.Rows[0].Cells[0].Style.ForeColor = Color.Red;
            }            
            if (dataGridView1.Rows[0].Cells[2].Value.ToString() == "V0.0.0.0")
            {
                dataGridView1.Rows[0].Cells[2].Style.ForeColor = Color.Red;
            }
            else
            {
                dataGridView1.Rows[0].Cells[2].Style.ForeColor = Color.Blue;
            }           
            if (dataGridView1.Rows[0].Cells[3].Value.ToString() == "2015/0/0 0:0:0")
            {
                dataGridView1.Rows[0].Cells[3].Style.ForeColor = Color.Red;
            }
            else
            {
                dataGridView1.Rows[0].Cells[3].Style.ForeColor = Color.Blue;
            }
        }

        public void ShowDataToEdgeInfoExcl(DataGridView dataGridView1, Map.EdgeInf cwf_r)//Edge ID,OC ID,Edge长度
        {
            //for (i = 0; i < dataGridView1.RowCount; i++)
            //{
                dataGridView1.Rows[0].Cells[0].Value = cwf_r.EdgeInfoHead.EdgeId;
                dataGridView1.Rows[0].Cells[1].Value = cwf_r.EdgeInfoHead.OcId;
                dataGridView1.Rows[0].Cells[2].Value = cwf_r.EdgeInfoHead.EdgeLen;
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "0")
                {
                    dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                }
                else
                {
                    dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.Blue;
                }
                if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "0")
                {
                    dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                }
                else
                {
                    dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Blue;
                }
                if (dataGridView1.Rows[i].Cells[2].Value.ToString() == "0")
                {
                    dataGridView1.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                }
                else
                {
                    dataGridView1.Rows[i].Cells[2].Style.ForeColor = Color.Blue;
                }
            //}
        }
        public void ShowDataToConReZeroExcl(DataGridView dataGridView1, Map.EdgeInf cwf_r)//Edge连接关系（0方向）
        {
            dataGridView1.Rows[0].Cells[0].Value = cwf_r.ConReZero.TurId;
            dataGridView1.Rows[0].Cells[1].Value = cwf_r.ConReZero.EdgeIdStr;
            dataGridView1.Rows[0].Cells[2].Value = cwf_r.ConReZero.EdgeTermNumStr;
            dataGridView1.Rows[0].Cells[3].Value = cwf_r.ConReZero.EdgeIdCur;
            dataGridView1.Rows[0].Cells[4].Value = cwf_r.ConReZero.EdgeTermNumCur;
            if (dataGridView1.Rows[0].Cells[0].Value.ToString() == "0")
            {
                dataGridView1.Rows[0].Cells[0].Style.ForeColor = Color.Red;
            }
            else
            {
                dataGridView1.Rows[0].Cells[0].Style.ForeColor = Color.Blue;
            }           
        }

        public void ShowDataToConReOneExcl(DataGridView dataGridView1, Map.EdgeInf cwf_r)//Edge连接关系（1方向）
        {
            dataGridView1.Rows[0].Cells[0].Value = cwf_r.ConReOne.TurId;
            dataGridView1.Rows[0].Cells[1].Value = cwf_r.ConReOne.EdgeIdStr;
            dataGridView1.Rows[0].Cells[2].Value = cwf_r.ConReOne.EdgeTermNumStr;
            dataGridView1.Rows[0].Cells[3].Value = cwf_r.ConReOne.EdgeIdCur;
            dataGridView1.Rows[0].Cells[4].Value = cwf_r.ConReOne.EdgeTermNumCur;
            if (dataGridView1.Rows[0].Cells[0].Value.ToString() == "0")
            {
                dataGridView1.Rows[0].Cells[0].Style.ForeColor = Color.Red;
            }
            else
            {
                dataGridView1.Rows[0].Cells[0].Style.ForeColor = Color.Blue;
            }
        }
        public void ShowDataToSpeedLimInfoExcl(DataGridView dataGridView1, Map.EdgeInf cwf_r)//静态限速区段信息
        {
            //for (int j = 0; j < state.EmapInf.EdgeNum; j++)
            //{
            for (i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = cwf_r.EdgeSpeLimSta.SpeedLimInfo[i].SpeedLimStart;
                    dataGridView1.Rows[i].Cells[1].Value = cwf_r.EdgeSpeLimSta.SpeedLimInfo[i].SpeedLimEnd;
                    dataGridView1.Rows[i].Cells[2].Value = cwf_r.EdgeSpeLimSta.SpeedLimInfo[i].SpeedLim;
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.Blue;
                    }
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Blue;
                    }
                    if (dataGridView1.Rows[i].Cells[2].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[2].Style.ForeColor = Color.Blue;
                    }
                }
           //}
        }

        public void ShowDataToRampInfExcl(DataGridView dataGridView1, Map.EdgeInf cwf_r)//坡道区段信息
        {
            //for (int j = 0; j < cwf_r.EmapInfo.EdgeNum;j++)
            //{
                for (i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = cwf_r.EdgeRamp.RampInfo[i].RampStart;
                    dataGridView1.Rows[i].Cells[1].Value = cwf_r.EdgeRamp.RampInfo[i].RampEnd;
                    dataGridView1.Rows[i].Cells[2].Value = cwf_r.EdgeRamp.RampInfo[i].Slope;
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.Blue;
                    }
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Blue;
                    }
                    if (dataGridView1.Rows[i].Cells[2].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[2].Style.ForeColor = Color.Blue;
                    }
                }
            //}
        }

        public void ShowDataToTagInfExcl(DataGridView dataGridView1, Map.EdgeInf cwf_r)//Tag信息
        {
            //for (int j = 0; j < cwf_r.EmapInfo.EdgeNum; j++)
            //{
                for (i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = cwf_r.EdgeTag.TagInfo[i].TagId;
                    dataGridView1.Rows[i].Cells[1].Value = cwf_r.EdgeTag.TagInfo[i].TagOffset;
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.Blue;
                    }
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Blue;
                    }
                }
            //}

        }

        public void ShowDataToPoliceMarkInfExcl(DataGridView dataGridView1, Map.EdgeInf cwf_r)//警冲标信息
        {
            //for (int j = 0; j < cwf_r.EmapInfo.EdgeNum; j++)
            //{
                for (i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = cwf_r.EdgePoliceMark.PoliceMarkInfo[i].ProtDir;
                    dataGridView1.Rows[i].Cells[1].Value = cwf_r.EdgePoliceMark.PoliceMarkInfo[i].PoliceMarkOffset;
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.Blue;
                    }
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Blue;
                    }
                }
           //}
        }

        public void ShowDataToStopPointInfExcl(DataGridView dataGridView1, Map.EdgeInf cwf_r)//停车点信息
        {
            //for (int j = 0; j < cwf_r.EmapInfo.EdgeNum; j++)
            //{
                for (i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = cwf_r.EdgeStopPoint.StopPointInfo[i].StopPointId;
                    dataGridView1.Rows[i].Cells[1].Value = cwf_r.EdgeStopPoint.StopPointInfo[i].StopPointOffset;
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.Blue;
                    }
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "0")
                    {
                        dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.Blue;
                    }
                }
            //}
        }
        public UInt32 SaveFileDataToMapFile(DataGridView dataGridView1)
        {
            UInt32 Ver;
            Ver = convert.FileVerTo(dataGridView1.Rows[0].Cells[0].Value);
            return Ver;
        }
        public Map.EdgeInfHead SaveEdgeInfDataToMapFile(DataGridView dataGridView1)
        {
            Map.EdgeInfHead EdgeInfHead;
            EdgeInfHead.EdgeId = Convert.ToInt16(dataGridView1.Rows[0].Cells[0].Value);
            EdgeInfHead.OcId = Convert.ToInt16(dataGridView1.Rows[0].Cells[1].Value);
            EdgeInfHead.EdgeLen = Convert.ToUInt32(dataGridView1.Rows[0].Cells[2].Value);
            return EdgeInfHead;
        }

        public Map.ConnectRelation SaveConReZeroDataToMapFile(DataGridView dataGridView1)
        {
            Map.ConnectRelation ConReZero;
            ConReZero.TurId = Convert.ToInt16(dataGridView1.Rows[0].Cells[0].Value);
            ConReZero.EdgeIdStr = Convert.ToInt16(dataGridView1.Rows[0].Cells[1].Value);
            ConReZero.EdgeTermNumStr = Convert.ToByte(dataGridView1.Rows[0].Cells[2].Value);
            ConReZero.EdgeIdCur = Convert.ToInt16(dataGridView1.Rows[0].Cells[1].Value);
            ConReZero.EdgeTermNumCur = Convert.ToByte(dataGridView1.Rows[0].Cells[2].Value);
            return ConReZero;
        }

        public Map.ConnectRelation SaveConReOneDataToMapFile(DataGridView dataGridView1)
        {
            Map.ConnectRelation ConReOne;
            ConReOne.TurId = Convert.ToInt16(dataGridView1.Rows[0].Cells[0].Value);
            ConReOne.EdgeIdStr = Convert.ToInt16(dataGridView1.Rows[0].Cells[1].Value);
            ConReOne.EdgeTermNumStr = Convert.ToByte(dataGridView1.Rows[0].Cells[2].Value);
            ConReOne.EdgeIdCur = Convert.ToInt16(dataGridView1.Rows[0].Cells[1].Value);
            ConReOne.EdgeTermNumCur = Convert.ToByte(dataGridView1.Rows[0].Cells[2].Value);
            return ConReOne;
        }

        public Map.EdgeSpeedLimStatic SaveEdgeSpeedLimStaticDataToMapFile(DataGridView dataGridView1)
        {
            Map.EdgeSpeedLimStatic EdgeSpeedLimStatic;
            EdgeSpeedLimStatic.SpeedLimInfo = new Map.SpeedLimInf[Const.SpeedLimNum];
            EdgeSpeedLimStatic.SpeedLimNum = (Int16)dataGridView1.RowCount;
            for (i = 0; i < EdgeSpeedLimStatic.SpeedLimNum; i++)
            {
                EdgeSpeedLimStatic.SpeedLimInfo[i].SpeedLimStart = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                EdgeSpeedLimStatic.SpeedLimInfo[i].SpeedLimEnd = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                EdgeSpeedLimStatic.SpeedLimInfo[i].SpeedLim = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
            } 
            return EdgeSpeedLimStatic;
        }

        public Map.EdgeRampInf SaveEdgeRampInfDataToMapFile(DataGridView dataGridView1)
        {
            Map.EdgeRampInf EdgeRampInf;
            EdgeRampInf.RampInfo = new Map.RampInf[Const.RampNum];
            EdgeRampInf.RampNum = (Int16)dataGridView1.RowCount;
            for (i = 0; i < EdgeRampInf.RampNum; i++)
            {
                EdgeRampInf.RampInfo[i].RampStart = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                EdgeRampInf.RampInfo[i].RampEnd = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                EdgeRampInf.RampInfo[i].Slope = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
            } 
            return EdgeRampInf;
        }

        public Map.EdgeTagInf SaveEdgeTagInfDataToMapFile(DataGridView dataGridView1)
        {
            Map.EdgeTagInf EdgeTagInf;
            EdgeTagInf.TagInfo = new Map.TagInf[Const.EdgeTagNum];
            EdgeTagInf.EdgeTagNum = (Int16)dataGridView1.RowCount;
            for (i = 0; i < EdgeTagInf.EdgeTagNum; i++)
            {
                EdgeTagInf.TagInfo[i].TagId = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                EdgeTagInf.TagInfo[i].TagOffset = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
            } 
            return EdgeTagInf;
        }

        public Map.EdgePolilceMarkInf SaveEdgePolilceMarkInfDataToMapFile(DataGridView dataGridView1)
        {
            Map.EdgePolilceMarkInf EdgePolilceMarkInf;
            EdgePolilceMarkInf.PoliceMarkInfo = new Map.PoliceMarkInf[Const.EdgePoliceMarkNumx];
            EdgePolilceMarkInf.EdgePoliceMarkNum = (Int16)dataGridView1.RowCount;
            for (i = 0; i < EdgePolilceMarkInf.EdgePoliceMarkNum; i++)
            {
                EdgePolilceMarkInf.PoliceMarkInfo[i].ProtDir = Convert.ToByte(dataGridView1.Rows[i].Cells[0].Value);
                EdgePolilceMarkInf.PoliceMarkInfo[i].PoliceMarkOffset = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
            } 
            return EdgePolilceMarkInf;
        }

        public Map.EdgeStopPointInf SaveEdgeStopPointInfDataToMapFile(DataGridView dataGridView1)
        {
            Map.EdgeStopPointInf EdgeStopPointInf;
            EdgeStopPointInf.StopPointInfo = new Map.StopPointInf[Const.StopPointNumMax];
            EdgeStopPointInf.StopPointNum = (Int16)dataGridView1.RowCount;
            for (i = 0; i < EdgeStopPointInf.StopPointNum; i++)
            {
                EdgeStopPointInf.StopPointInfo[i].StopPointId = Convert.ToInt16(dataGridView1.Rows[i].Cells[0].Value);
                EdgeStopPointInf.StopPointInfo[i].StopPointOffset = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
            } 
            return EdgeStopPointInf;
        }
    }
}  
        
