using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace WindowsFormsApplication1
{
        
    class State
    {
        TreeList tree = new TreeList();
        Excl excl = new Excl();
        Const cst = new Const();

        public Map.MapFile MapFile;
        //public Map.EdgeInfHead EdgeInfHead;
        //public Map.EmapInf EmapInf;
        //public Map.SpeedLimInf SpeedLimInf;
        //public Map.EdgeInf[] EdgeInf = new Map.EdgeInf[Const.EdgeNumMax];
        //public Map.EdgeSpeedLimStatic[] EdgeSpeedLimStatic = new Map.EdgeSpeedLimStatic[Const.EdgeNumMax];
        //public Map.EdgeRampInf[] EdgeRampInf = new Map.EdgeRampInf[Const.EdgeNumMax];
        //public Map.EdgeTagInf[] EdgeTagInf = new Map.EdgeTagInf[Const.EdgeNumMax];
        //public Map.EdgePolilceMarkInf[] EdgePolilceMarkInf = new Map.EdgePolilceMarkInf[Const.EdgeNumMax];
        //public Map.EdgeStopPointInf[] EdgeStopPointInf = new Map.EdgeStopPointInf[Const.EdgeNumMax];

        public void InitMapFile()
        {
            int i,j;
            MapFile = new Map.MapFile();
            MapFile.EmapInfo.EdgeInfo = new Map.EdgeInf[Const.EdgeNumMax];
            for (i = 0; i < Const.EdgeNumMax; i++)
            {
                MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimInfo= new Map.SpeedLimInf[Const.SpeedLimNum]; 
                MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampInfo = new Map.RampInf[Const.RampNum];
                MapFile.EmapInfo.EdgeInfo[i].EdgeTag.TagInfo = new Map.TagInf[Const.EdgeTagNum];
                MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.PoliceMarkInfo = new Map.PoliceMarkInf[Const.EdgePoliceMarkNumx];
                MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointInfo = new Map.StopPointInf[Const.StopPointNumMax];
                MapFile.EmapInfo.EdgeNum = 0;
                MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum = 0;
                MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum = 0;
                MapFile.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum = 0;
                MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum = 0;
                MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum = 0;
            }

            
        }
        public void InitMapData()
        {
            MapFile = new Map.MapFile();            
            MapFile.Fh.Crc = 0;
            MapFile.Fh.FileLen = 0;
            MapFile.Fh.FileMark = 0;
            MapFile.Fh.FilePutTime = 0;
            MapFile.Fh.FileVer = 0;
        }
        public bool CheakMapFileDataValid(TreeView treeView1)
        {
            int i,j;

             if (MapFile.Fh.FileMark != 0xA55AA55A)
             {
                 MessageBox.Show("文件标识不符");
                 return false;
             }
             if (MapFile.Fh.FileLen >= Const.MapFileLen)
             {
                 MessageBox.Show("文件长度不符");
                 return false;
             }
             for (i = 0; i < MapFile.EmapInfo.EdgeNum;i++)
             {
                 if (MapFile.EmapInfo.EdgeInfo[i].EdgeInfoHead.EdgeLen < 0)
                 {
                     MessageBox.Show("Edge长度不能小于0");
                     return false;
                 }
             }               
             for (i = 0; (MapFile.EmapInfo.EdgeNum > 0) && (MapFile.EmapInfo.EdgeNum <= Const.EdgeNumMax); i++)
             {
                 for (j = 0; j < MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum; j++)
                 {
                     if ((MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimInfo[j].SpeedLimStart > MapFile.EmapInfo.EdgeInfo[i].EdgeInfoHead.EdgeLen)
                         || (MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimInfo[j].SpeedLimEnd > MapFile.EmapInfo.EdgeInfo[i].EdgeInfoHead.EdgeLen))
                     {
                         treeView1.SelectedNode = treeView1.Nodes[0].Nodes[1].Nodes[3];
                         MessageBox.Show("Edge[" + (i + 1).ToString() + "]信息:静态限速[" + (j + 1).ToString() + "]信息", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                         return false;
                     }
                 }
             //}
             //for (i = 0; (MapFile.EmapInfo.EdgeNum > 0) && (MapFile.EmapInfo.EdgeNum <= Const.EdgeNumMax); i++)
             //{
                 for (j = 0; j < MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum; j++)
                 {
                     if ((MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampInfo[j].RampStart > MapFile.EmapInfo.EdgeInfo[i].EdgeInfoHead.EdgeLen)
                         || (MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampInfo[j].RampEnd > MapFile.EmapInfo.EdgeInfo[i].EdgeInfoHead.EdgeLen))
                     {
                         treeView1.SelectedNode = treeView1.Nodes[0].Nodes[1].Nodes[4];
                         MessageBox.Show("Edge[" + (i + 1).ToString() + "]信息:坡道[" + (j + 1).ToString() + "]信息", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                         return false;
                     }
                 }
             //}
             //for (i = 0; (MapFile.EmapInfo.EdgeNum > 0) && (MapFile.EmapInfo.EdgeNum <= Const.EdgeNumMax); i++)
             //{
                 for (j = 0; j < MapFile.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum; j++)
                 {
                     if ((MapFile.EmapInfo.EdgeInfo[i].EdgeTag.TagInfo[j].TagOffset > MapFile.EmapInfo.EdgeInfo[i].EdgeInfoHead.EdgeLen))
                     {
                         treeView1.SelectedNode = treeView1.Nodes[0].Nodes[1].Nodes[5];
                         MessageBox.Show("Edge[" + (i + 1).ToString() + "]信息:Tag[" + (j + 1).ToString() + "]信息", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                         return false;
                     }
                 }
            // }
             //for (i = 0; (MapFile.EmapInfo.EdgeNum > 0) && (MapFile.EmapInfo.EdgeNum <= Const.EdgeNumMax); i++)
             //{
                 for (j = 0; j < MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum; j++)
                 {
                     if ((MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.PoliceMarkInfo[j].PoliceMarkOffset > MapFile.EmapInfo.EdgeInfo[i].EdgeInfoHead.EdgeLen))
                     {
                         treeView1.SelectedNode = treeView1.Nodes[0].Nodes[1].Nodes[6];
                         MessageBox.Show("Edge[" + (i + 1).ToString() + "]信息:警冲标[" + (j + 1).ToString() + "]信息", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                         return false;
                     }
                 }
             //}
             //for (i = 0; (MapFile.EmapInfo.EdgeNum > 0) && (MapFile.EmapInfo.EdgeNum <= Const.EdgeNumMax); i++)
             //{
                 for (j = 0; j < MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum; j++)
                 {
                     if ((MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointInfo[j].StopPointOffset > MapFile.EmapInfo.EdgeInfo[i].EdgeInfoHead.EdgeLen))
                     {
                         treeView1.SelectedNode = treeView1.Nodes[0].Nodes[1].Nodes[7];
                         MessageBox.Show("Edge[" + (i + 1).ToString() + "]信息:停车点[" + (j + 1).ToString() + "]信息", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                         return false;
                     }
                 }
             }

            return true;
        }
        public void RightCommandEnableMange(TreeNode Node, ContextMenuStrip contextMenuStrip1, DataGridView dataGridView1)//右键添加项删除项是否可用
        {
            int i;
            cst.EdgeInfInit();
            for (i = 0; i < Const.EdgeNumMax; i++)
            {
                if (((Convert.ToUInt16(Node.Tag) == Const.EmapInf_Tag) && (MapFile.EmapInfo.EdgeNum > 0) && (MapFile.EmapInfo.EdgeNum < Const.EdgeNumMax))
                ||((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_Tag[i]) && (MapFile.EmapInfo.EdgeNum > 0) && (MapFile.EmapInfo.EdgeNum < Const.EdgeNumMax))
                || ((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgeSpeLimSta_Tag[i]) && ((MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum > 0) && (MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum < Const.SpeedLimNum))
                || ((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgeRamp_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum > 0) && (MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum < Const.RampNum))
                || ((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgeTag_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum > 0) && (MapFile.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum < Const.EdgeTagNum))
                || ((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgePoliceMark_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum > 0) && (MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum < Const.EdgePoliceMarkNumx))
                || ((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgeStopPoint_Tag[i]) && MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum > 0) && (MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum < Const.StopPointNumMax)))
                {
                    contextMenuStrip1.Items[0].Enabled = true;
                    contextMenuStrip1.Items[1].Enabled = true;
                }
                else if (((Convert.ToUInt16(Node.Tag) == Const.EmapInf_Tag) && (MapFile.EmapInfo.EdgeNum == Const.EdgeNumMax))
                ||((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_Tag[i]) && (MapFile.EmapInfo.EdgeNum == Const.EdgeNumMax))
                || ((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgeSpeLimSta_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum == Const.SpeedLimNum))
                || ((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgeRamp_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum == Const.RampNum))
                || ((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgeTag_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum == Const.EdgeTagNum))
                || ((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgePoliceMark_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum == Const.EdgePoliceMarkNumx))
                || ((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgeStopPoint_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum == Const.StopPointNumMax)))
                {
                    contextMenuStrip1.Items[0].Enabled = false;
                    contextMenuStrip1.Items[1].Enabled = true;
                }
                else if (((Convert.ToUInt16(Node.Tag) == Const.EmapInf_Tag) && (MapFile.EmapInfo.EdgeNum >= 0) && (MapFile.EmapInfo.EdgeNum<Const.EdgeNumMax))
                || ((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_Tag[i]) && (MapFile.EmapInfo.EdgeNum >= 0)) && (MapFile.EmapInfo.EdgeNum<Const.EdgeNumMax)
                || ((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgeSpeLimSta_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum == 0))
                || ((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgeRamp_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum == 0))
                || ((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgeTag_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum == 0))
                || ((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgePoliceMark_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum == 0))
                || ((Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgeStopPoint_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum == 0)))
                {
                    contextMenuStrip1.Items[0].Enabled = true;
                    contextMenuStrip1.Items[1].Enabled = false;
                }
                else
                {
                    contextMenuStrip1.Items[0].Enabled = false;
                    contextMenuStrip1.Items[1].Enabled = false;
                }

                if (Convert.ToUInt16(Node.Tag) == Const.EmapInf_Tag)
                {
                    MapFileExclState(dataGridView1);
                    excl.ShowDataToFileExcl(dataGridView1, MapFile.Fh);
                }
                else if (Convert.ToUInt16(Node.Tag) == cst.EdgeInf_Tag[i])
                {
                    EdgeInfExclState(dataGridView1);
                    excl.ShowDataToEdgeInfoExcl(dataGridView1, MapFile.EmapInfo.EdgeInfo[i]);
                }
                else if (Convert.ToUInt16(Node.Tag) == cst.EdgeInf_ConReZero_Tag[i])
                {
                    ConReZeroState(dataGridView1);
                    excl.ShowDataToConReZeroExcl(dataGridView1, MapFile.EmapInfo.EdgeInfo[i]);
                }
                else if (Convert.ToUInt16(Node.Tag) == cst.EdgeInf_ConReOne_Tag[i])
                {
                    ConReOneState(dataGridView1);
                    excl.ShowDataToConReOneExcl(dataGridView1, MapFile.EmapInfo.EdgeInfo[i]);
                }
                else if (Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgeSpeLimSta_Tag[i])
                {
                    SpeLimStaExclState(dataGridView1, MapFile.EmapInfo.EdgeInfo[i]);
                    excl.ShowDataToSpeedLimInfoExcl(dataGridView1, MapFile.EmapInfo.EdgeInfo[i]);
                }
                else if (Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgeRamp_Tag[i])
                {
                    RampExclState(dataGridView1, MapFile.EmapInfo.EdgeInfo[i]);
                    excl.ShowDataToRampInfExcl(dataGridView1, MapFile.EmapInfo.EdgeInfo[i]);
                }
                else if (Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgeTag_Tag[i])
                {
                    TagExclState(dataGridView1, MapFile.EmapInfo.EdgeInfo[i]);
                    excl.ShowDataToTagInfExcl(dataGridView1, MapFile.EmapInfo.EdgeInfo[i]);
                }
                else if (Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgePoliceMark_Tag[i])
                {
                    PoliceMarkExclState(dataGridView1, MapFile.EmapInfo.EdgeInfo[i]);
                    excl.ShowDataToPoliceMarkInfExcl(dataGridView1, MapFile.EmapInfo.EdgeInfo[i]);
                }
                else  if(Convert.ToUInt16(Node.Tag) == cst.EdgeInf_EdgeStopPoint_Tag[i])
                {
                    StopPointExclState(dataGridView1, MapFile.EmapInfo.EdgeInfo[i]);
                    excl.ShowDataToStopPointInfExcl(dataGridView1, MapFile.EmapInfo.EdgeInfo[i]);
                }
                else
                {
                     return;
                }
            }
        }
        public void AddTreeNodeEdgeInfo(TreeView treeView1, DataGridView dataGridView1)
        {
            //添加Edge信息                  
                cst.EdgeInfInit();
                if ((Convert.ToUInt16(treeView1.SelectedNode.Tag) == Const.EmapInf_Tag)&& (MapFile.EmapInfo.EdgeNum < Const.EdgeNumMax))
                {
                    //cst.EdgeInfInit();
                    tree.AddEdgeInfoNode(treeView1.SelectedNode, cst.EdgeInf_Tag[MapFile.EmapInfo.EdgeNum], cst.EdgeInf_ConReZero_Tag[MapFile.EmapInfo.EdgeNum],
                                                                 cst.EdgeInf_ConReOne_Tag[MapFile.EmapInfo.EdgeNum], cst.EdgeInf_EdgeSpeLimSta_Tag[MapFile.EmapInfo.EdgeNum],
                                                                 cst.EdgeInf_EdgeRamp_Tag[MapFile.EmapInfo.EdgeNum], cst.EdgeInf_EdgeTag_Tag[MapFile.EmapInfo.EdgeNum],
                                                                 cst.EdgeInf_EdgePoliceMark_Tag[MapFile.EmapInfo.EdgeNum], cst.EdgeInf_EdgeStopPoint_Tag[MapFile.EmapInfo.EdgeNum],
                                                                 cst.EdgeInf_Text[MapFile.EmapInfo.EdgeNum], cst.EdgeInf_ConReZero_Text[MapFile.EmapInfo.EdgeNum],
                                                                 cst.EdgeInf_ConReOne_Text[MapFile.EmapInfo.EdgeNum], cst.EdgeInf_EdgeSpeLimSta_Text[MapFile.EmapInfo.EdgeNum],
                                                                 cst.EdgeInf_EdgeRamp_Text[MapFile.EmapInfo.EdgeNum], cst.EdgeInf_EdgeTag_Text[MapFile.EmapInfo.EdgeNum],
                                                                 cst.EdgeInf_EdgePoliceMark_Text[MapFile.EmapInfo.EdgeNum], cst.EdgeInf_EdgeStopPoint_Text[MapFile.EmapInfo.EdgeNum]);
                    MapFile.EmapInfo.EdgeNum++;
                }

                for (int i = 0; i < Const.EdgeNumMax; i++)
            {
                if ((Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeSpeLimSta_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum < Const.SpeedLimNum))
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum++;
                }
                else if ((Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeRamp_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum < Const.RampNum))
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum++;
                }
                else if ((Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeTag_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum < Const.EdgeTagNum))
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum++;
                }
                else if ((Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgePoliceMark_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum < Const.EdgePoliceMarkNumx))
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum++;
                }
                else if ((Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeStopPoint_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum < Const.StopPointNumMax))
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum++;
                }
                else if ((Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_Tag[i]) && (MapFile.EmapInfo.EdgeNum < Const.EdgeNumMax))
                {
                    tree.AddEdgeInfoNode(treeView1.Nodes[0], cst.EdgeInf_Tag[MapFile.EmapInfo.EdgeNum], cst.EdgeInf_ConReZero_Tag[MapFile.EmapInfo.EdgeNum],
                                                                 cst.EdgeInf_ConReOne_Tag[MapFile.EmapInfo.EdgeNum], cst.EdgeInf_EdgeSpeLimSta_Tag[MapFile.EmapInfo.EdgeNum],
                                                                 cst.EdgeInf_EdgeRamp_Tag[MapFile.EmapInfo.EdgeNum], cst.EdgeInf_EdgeTag_Tag[MapFile.EmapInfo.EdgeNum],
                                                                 cst.EdgeInf_EdgePoliceMark_Tag[MapFile.EmapInfo.EdgeNum], cst.EdgeInf_EdgeStopPoint_Tag[MapFile.EmapInfo.EdgeNum],
                                                                 cst.EdgeInf_Text[MapFile.EmapInfo.EdgeNum], cst.EdgeInf_ConReZero_Text[MapFile.EmapInfo.EdgeNum],
                                                                 cst.EdgeInf_ConReOne_Text[MapFile.EmapInfo.EdgeNum], cst.EdgeInf_EdgeSpeLimSta_Text[MapFile.EmapInfo.EdgeNum],
                                                                 cst.EdgeInf_EdgeRamp_Text[MapFile.EmapInfo.EdgeNum], cst.EdgeInf_EdgeTag_Text[MapFile.EmapInfo.EdgeNum],
                                                                 cst.EdgeInf_EdgePoliceMark_Text[MapFile.EmapInfo.EdgeNum], cst.EdgeInf_EdgeStopPoint_Text[MapFile.EmapInfo.EdgeNum]);
                    MapFile.EmapInfo.EdgeNum++;
                }
                else
                {
                    return;
                }  
            } 
        }
        public void RemoveTreeNodeEdgeInfo(TreeView treeView1)
        {
            cst.EdgeInfInit();
            for(int i=0;i<Const.EdgeNumMax;i++)
            {
                if ((Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_Tag[i]) && (MapFile.EmapInfo.EdgeNum > 0))
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgeInfoHead.EdgeId = 0;
                    MapFile.EmapInfo.EdgeInfo[i].EdgeInfoHead.OcId = 0;
                    MapFile.EmapInfo.EdgeInfo[i].EdgeInfoHead.EdgeLen = 0;
                    MapFile.EmapInfo.EdgeInfo[i].ConReZero.TurId = 0;
                    MapFile.EmapInfo.EdgeInfo[i].ConReZero.EdgeIdStr = 0;
                    MapFile.EmapInfo.EdgeInfo[i].ConReZero.EdgeTermNumStr = 0;
                    MapFile.EmapInfo.EdgeInfo[i].ConReZero.EdgeIdCur = 0;
                    MapFile.EmapInfo.EdgeInfo[i].ConReZero.EdgeTermNumCur = 0;
                    MapFile.EmapInfo.EdgeInfo[i].ConReOne.TurId = 0;
                    MapFile.EmapInfo.EdgeInfo[i].ConReOne.EdgeIdStr = 0;
                    MapFile.EmapInfo.EdgeInfo[i].ConReOne.EdgeTermNumStr = 0;
                    MapFile.EmapInfo.EdgeInfo[i].ConReOne.EdgeIdCur = 0;
                    MapFile.EmapInfo.EdgeInfo[i].ConReOne.EdgeTermNumCur = 0;
                    for(int j=0;j<Const.SpeedLimNum;j++)
                    {
                        MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimInfo[j].SpeedLimStart = 0;
                        MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimInfo[j].SpeedLimEnd = 0;
                        MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimInfo[j].SpeedLim = 0;
                    }
                    for(int j=0;j<Const.RampNum;j++)
                    {
                        MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampInfo[j].RampStart = 0;
                        MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampInfo[j].RampEnd = 0;
                        MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampInfo[j].Slope = 0;
                    }
                    for(int j=0;j<Const.EdgeTagNum;j++)
                    {
                        MapFile.EmapInfo.EdgeInfo[i].EdgeTag.TagInfo[j].TagId = 0;
                        MapFile.EmapInfo.EdgeInfo[i].EdgeTag.TagInfo[j].TagOffset = 0;
                    }
                    for(int j=0;j<Const.EdgePoliceMarkNumx;j++)
                    {
                        MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.PoliceMarkInfo[j].ProtDir = 0;
                        MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.PoliceMarkInfo[j].PoliceMarkOffset = 0;
                    }
                    for(int j=0;j<Const.StopPointNumMax;j++)
                    {
                        MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointInfo[j].StopPointId = 0;
                        MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointInfo[j].StopPointOffset = 0;
                    }
                    MapFile.EmapInfo.EdgeNum--;
                    treeView1.SelectedNode.Remove();
                }
                else if ((Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeSpeLimSta_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum > 0))
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimInfo[MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum - 1].SpeedLimStart = 0;
                    MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimInfo[MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum - 1].SpeedLimEnd = 0;
                    MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimInfo[MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum - 1].SpeedLim = 0;
                    MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum--;
                }
                else if ((Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeRamp_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum > 0))
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampInfo[MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum - 1].RampStart = 0;
                    MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampInfo[MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum - 1].RampEnd = 0;
                    MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampInfo[MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum - 1].Slope = 0;
                    MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum--;
                }
                else if ((Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeTag_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum > 0))
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgeTag.TagInfo[MapFile.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum - 1].TagId = 0;
                    MapFile.EmapInfo.EdgeInfo[i].EdgeTag.TagInfo[MapFile.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum - 1].TagOffset = 0;
                    MapFile.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum--;
                }
                else if ((Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgePoliceMark_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum > 0))
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.PoliceMarkInfo[MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum - 1].ProtDir = 0;
                    MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.PoliceMarkInfo[MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum - 1].PoliceMarkOffset = 0;
                    MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum --;
                }
                else if ((Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeStopPoint_Tag[i]) && (MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum > 0))
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointInfo[MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum - 1].StopPointId = 0;
                    MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointInfo[MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum - 1].StopPointOffset = 0;
                    MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum --;
                }
                else
                {
                    return;
                }
            }           
        }
        public void SaveFixDataToMapFile()
        {
            MapFile.Fh.FileMark = 0xA55AA55A;
            for (int i = 0; i < MapFile.EmapInfo.EdgeNum;i++)
            {
                MapFile.Fh.FileLen = (UInt32)(2 + MapFile.EmapInfo.EdgeNum * 
                    (42 + 12 * MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta.SpeedLimNum + 2 * MapFile.EmapInfo.EdgeInfo[i].EdgeRamp.RampNum
                    + 8 * MapFile.EmapInfo.EdgeInfo[i].EdgeTag.EdgeTagNum + 4 * MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark.EdgePoliceMarkNum 
                    + 6 * MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint.StopPointNum));
            }                
        }
        public void SaveExcelDataToMapFile(TreeView treeView1, DataGridView dataGridView1)
        {
            for (int i = 0; i < MapFile.EmapInfo.EdgeNum;i++)
            {
                if (Convert.ToUInt16(treeView1.SelectedNode.Tag) == Const.EmapInf_Tag)
                {
                    MapFile.Fh.FileVer = excl.SaveFileDataToMapFile(dataGridView1);
                }
                else if (Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_Tag[i])
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgeInfoHead = excl.SaveEdgeInfDataToMapFile(dataGridView1);
                }
                else if (Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_ConReZero_Tag[i])
                {
                    MapFile.EmapInfo.EdgeInfo[i].ConReZero = excl.SaveConReZeroDataToMapFile(dataGridView1);
                }
                else if (Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_ConReOne_Tag[i])
                {
                    MapFile.EmapInfo.EdgeInfo[i].ConReOne = excl.SaveConReOneDataToMapFile(dataGridView1);
                }
                else if (Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeSpeLimSta_Tag[i])
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgeSpeLimSta = excl.SaveEdgeSpeedLimStaticDataToMapFile(dataGridView1);
                }
                else if (Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeRamp_Tag[i])
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgeRamp = excl.SaveEdgeRampInfDataToMapFile(dataGridView1);
                }
                else if (Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeTag_Tag[i])
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgeTag = excl.SaveEdgeTagInfDataToMapFile(dataGridView1);
                }
                else if (Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgePoliceMark_Tag[i])
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgePoliceMark = excl.SaveEdgePolilceMarkInfDataToMapFile(dataGridView1);
                }
                else if (Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeStopPoint_Tag[i])
                {
                    MapFile.EmapInfo.EdgeInfo[i].EdgeStopPoint = excl.SaveEdgeStopPointInfDataToMapFile(dataGridView1);
                }
                else
                {

                }
            }
        }
        private void MapFileExclState(DataGridView dataGridView1)
        {
            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].HeaderText = "文件标识";
            dataGridView1.Columns[1].HeaderText = "文件长度";
            dataGridView1.Columns[2].HeaderText = "软件版本";
            dataGridView1.Columns[3].HeaderText = "发布时间";
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Show();
        }
        private void EdgeInfExclState(DataGridView dataGridView1)
       {
           dataGridView1.RowCount = 1;
           dataGridView1.ColumnCount = 3;
           dataGridView1.Columns[0].HeaderText = "Edge ID";
           dataGridView1.Columns[1].HeaderText = "OC ID";
           dataGridView1.Columns[2].HeaderText = "Edge长度";
           dataGridView1.Columns[2].ReadOnly = true;
           dataGridView1.Show();
       }
        private void ConReZeroState(DataGridView dataGridView1)
       {          
           dataGridView1.RowCount = 1;
           dataGridView1.ColumnCount = 5;
           dataGridView1.Columns[0].HeaderText = "连接道岔的ID";
           dataGridView1.Columns[1].HeaderText = "连接道岔的Edge ID(直股)";
           dataGridView1.Columns[2].HeaderText = "连接道岔的Edge端号(直股)";
           dataGridView1.Columns[3].HeaderText = "连接道岔的Edge ID(弯股)";
           dataGridView1.Columns[4].HeaderText = "连接道岔的Edge端号(弯股)";
           dataGridView1.Show();
       }
        private void ConReOneState(DataGridView dataGridView1)
        {
            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].HeaderText = "连接道岔的ID";
            dataGridView1.Columns[1].HeaderText = "连接道岔的Edge ID(直股)";
            dataGridView1.Columns[2].HeaderText = "连接道岔的Edge端号(直股)";
            dataGridView1.Columns[3].HeaderText = "连接道岔的Edge ID(弯股)";
            dataGridView1.Columns[4].HeaderText = "连接道岔的Edge端号(弯股)";
            dataGridView1.Show();
        }
        private void SpeLimStaExclState(DataGridView dataGridView1, Map.EdgeInf edgeinf)
       {
           for(int i=0;i<MapFile.EmapInfo.EdgeNum;i++)
           {
               /*if (edgeinf.EdgeSpeLimSta.SpeedLimNum == 0)
               {
                 dataGridView1.RowCount = Const.SpeedLimNum;
                 dataGridView1.ColumnCount = 3;
                 dataGridView1.Hide();
               }
               else
               {*/
                 dataGridView1.RowCount = (int)edgeinf.EdgeSpeLimSta.SpeedLimNum;
                 dataGridView1.ColumnCount = 3;
                 dataGridView1.Columns[0].HeaderText = "限速区段起点偏移";
                 dataGridView1.Columns[1].HeaderText = "限速区段终点偏移";
                 dataGridView1.Columns[2].HeaderText = "限速值";
                 dataGridView1.Show();
               //}
          }          
       }
        private void RampExclState(DataGridView dataGridView1, Map.EdgeInf edgeinf)
       {
            for (int i = 0; i < MapFile.EmapInfo.EdgeNum; i++)
            {
                /*if (EdgeInf[i].EdgeRamp.RampNum == 0)
                {
                    dataGridView1.RowCount = Const.RampNum;
                    dataGridView1.ColumnCount = 3;
                    dataGridView1.Hide();
                }
                else
                {*/
                    dataGridView1.RowCount = (int)edgeinf.EdgeRamp.RampNum;
                    dataGridView1.ColumnCount = 3;
                    dataGridView1.Columns[0].HeaderText = "坡道区段起点偏移";
                    dataGridView1.Columns[1].HeaderText = "坡道区段终点偏移";
                    dataGridView1.Columns[2].HeaderText = "从起点到终点的坡度";
                    dataGridView1.Show();
                //}
            }
        }
        private void TagExclState(DataGridView dataGridView1, Map.EdgeInf edgeinf)
        {
            for (int i = 0; i < MapFile.EmapInfo.EdgeNum; i++)
            {
                /*if (EdgeInf[i].EdgeTag.EdgeTagNum == 0)
                {
                    dataGridView1.RowCount = Const.EdgeTagNum;
                    dataGridView1.ColumnCount = 2;
                    dataGridView1.Hide();
                }
                else
                {*/
                    dataGridView1.RowCount = (int)edgeinf.EdgeTag.EdgeTagNum;
                    dataGridView1.ColumnCount = 2;
                    dataGridView1.Columns[0].HeaderText = "Tag ID";
                    dataGridView1.Columns[1].HeaderText = "Tag中心偏移";
                    dataGridView1.Show();
                //}
            }
        }
        private void PoliceMarkExclState(DataGridView dataGridView1, Map.EdgeInf edgeinf)
        {
            for (int i = 0; i < MapFile.EmapInfo.EdgeNum; i++)
            {
                /*if (EdgeInf[i].EdgePoliceMark.EdgePoliceMarkNum == 0)
                {
                    dataGridView1.RowCount = Const.EdgePoliceMarkNumx;
                    dataGridView1.ColumnCount = 2;
                    dataGridView1.Hide();
                }
                else
                {*/
                    dataGridView1.RowCount = (int)edgeinf.EdgePoliceMark.EdgePoliceMarkNum;
                    dataGridView1.ColumnCount = 2;
                    dataGridView1.Columns[0].HeaderText = "防护方向";
                    dataGridView1.Columns[1].HeaderText = "警冲标偏移";
                    dataGridView1.Show();
                //}
            }
        }
        private void StopPointExclState(DataGridView dataGridView1, Map.EdgeInf edgeinf)
        {
            for (int i = 0; i < MapFile.EmapInfo.EdgeNum; i++)
            {
                /*if (EdgeInf[i].EdgeStopPoint.StopPointNum == 0)
                {
                    dataGridView1.RowCount = Const.StopPointNumMax;
                    dataGridView1.ColumnCount = 2;
                    dataGridView1.Hide();
                }
                else
                {*/
                    dataGridView1.RowCount = (int)edgeinf.EdgeStopPoint.StopPointNum;
                    dataGridView1.ColumnCount = 2;
                    dataGridView1.Columns[0].HeaderText = "停车点ID";
                    dataGridView1.Columns[1].HeaderText = "停车点偏移";
                    dataGridView1.Show();
                //}
            }
        }
       private void DefautStateExclState(DataGridView dataGridView1)
       {
           dataGridView1.Hide();
       }
    }
}
