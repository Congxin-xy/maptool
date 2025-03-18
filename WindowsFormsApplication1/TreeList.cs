using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class TreeList
    {
        Const cst = new Const();        
        public bool treeViewNew(TreeView treeVew, Int16 EdgeNum,bool treeExit)
        {
            int i;
            bool treeExit1 = true;
            //Map.EmapInf EmapInfo = new Map.EmapInf();
            TreeNode   EmapInf = new TreeNode();
            TreeNode[] EdgeInf = new TreeNode[Const.EdgeNumMax];
            TreeNode[] EdgeInf_ConReZero = new TreeNode[Const.EdgeNumMax];
            TreeNode[] EdgeInf_ConReOne = new TreeNode[Const.EdgeNumMax];
            TreeNode[] EdgeInf_EdgeSpeLimSta = new TreeNode[Const.EdgeNumMax];
            TreeNode[] EdgeInf_EdgeRamp = new TreeNode[Const.EdgeNumMax];
            TreeNode[] EdgeInf_EdgeTag = new TreeNode[Const.EdgeNumMax];
            TreeNode[] EdgeInf_EdgePoliceMark = new TreeNode[Const.EdgeNumMax];
            TreeNode[] EdgeInf_EdgeStopPoint = new TreeNode[Const.EdgeNumMax];

            for (i = 0; i < Const.EdgeNumMax;i++)
            {
                EdgeInf[i] = new TreeNode();
                EdgeInf_ConReZero[i] = new TreeNode();
                EdgeInf_ConReOne[i] = new TreeNode();
                EdgeInf_EdgeSpeLimSta[i] = new TreeNode();
                EdgeInf_EdgeRamp[i] = new TreeNode();
                EdgeInf_EdgeTag[i] = new TreeNode();
                EdgeInf_EdgePoliceMark[i] = new TreeNode();
                EdgeInf_EdgeStopPoint[i] = new TreeNode();

                cst.EdgeInfInit();
              
                EmapInf.Tag = Const.EmapInf_Tag;
                EdgeInf[i].Tag = cst.EdgeInf_Tag[i];
                EdgeInf_ConReZero[i].Tag = cst.EdgeInf_ConReZero_Tag[i];
                EdgeInf_ConReOne[i].Tag = cst.EdgeInf_ConReOne_Tag[i];
                EdgeInf_EdgeSpeLimSta[i].Tag = cst.EdgeInf_EdgeSpeLimSta_Tag[i];
                EdgeInf_EdgeRamp[i].Tag = cst.EdgeInf_EdgeRamp_Tag[i];
                EdgeInf_EdgeTag[i].Tag = cst.EdgeInf_EdgeTag_Tag[i];
                EdgeInf_EdgePoliceMark[i].Tag = cst.EdgeInf_EdgePoliceMark_Tag[i];
                EdgeInf_EdgeStopPoint[i].Tag = cst.EdgeInf_EdgeStopPoint_Tag[i];

                EmapInf.Text = cst.EmapInf_Text.Trim();
                EdgeInf[i].Text = cst.EdgeInf_Text[i];
                EdgeInf_ConReZero[i].Text = cst.EdgeInf_ConReZero_Text[i].Trim();
                EdgeInf_ConReOne[i].Text = cst.EdgeInf_ConReOne_Text[i].Trim();
                EdgeInf_EdgeSpeLimSta[i].Text = cst.EdgeInf_EdgeSpeLimSta_Text[i].Trim();
                EdgeInf_EdgeRamp[i].Text = cst.EdgeInf_EdgeRamp_Text[i].Trim();
                EdgeInf_EdgeTag[i].Text = cst.EdgeInf_EdgeTag_Text[i].Trim();
                EdgeInf_EdgePoliceMark[i].Text = cst.EdgeInf_EdgePoliceMark_Text[i].Trim();
                EdgeInf_EdgeStopPoint[i].Text = cst.EdgeInf_EdgeStopPoint_Text[i].Trim();
            }
                

            if (treeExit == false)
            {
                treeVew.Nodes.Add(EmapInf);
            }
            else
            {
                treeVew.Nodes[0].Remove();
                treeVew.Nodes.Add(EmapInf);
            }

            for (i = 0; i < EdgeNum; i++)
            {
                //EmapInf.Nodes.Add(EdgeInf[i]);
                AddEdgeInfoNode(EmapInf, cst.EdgeInf_Tag[i], cst.EdgeInf_ConReZero_Tag[i], cst.EdgeInf_ConReOne_Tag[i], cst.EdgeInf_EdgeSpeLimSta_Tag[i],
                    cst.EdgeInf_EdgeRamp_Tag[i], cst.EdgeInf_EdgeTag_Tag[i], cst.EdgeInf_EdgePoliceMark_Tag[i], cst.EdgeInf_EdgeStopPoint_Tag[i],
                     cst.EdgeInf_Text[i],cst.EdgeInf_ConReZero_Text[i], cst.EdgeInf_ConReOne_Text[i], cst.EdgeInf_EdgeSpeLimSta_Text[i],
                    cst.EdgeInf_EdgeRamp_Text[i], cst.EdgeInf_EdgeTag_Text[i], cst.EdgeInf_EdgePoliceMark_Text[i], cst.EdgeInf_EdgeStopPoint_Text[i]);
            }       

            treeVew.SelectedNode = treeVew.Nodes[0];

            treeVew.ExpandAll();

            return treeExit1;
        }

        public void AddEdgeInfoNode(TreeNode ParNode, int tag1, int tag2, int tag3, int tag4, int tag5, int tag6, int tag7, int tag8,
                                  string text1,string text2, string text3, string text4, string text5, string text6, string text7, string text8)
        {
            TreeNode node1 = new TreeNode();
            TreeNode node2 = new TreeNode();
            TreeNode node3 = new TreeNode();
            TreeNode node4 = new TreeNode();
            TreeNode node5 = new TreeNode();
            TreeNode node6 = new TreeNode();
            TreeNode node7 = new TreeNode();
            TreeNode node8 = new TreeNode();

            node1.Tag = tag1;
            node1.Text = text1.Trim();
            node2.Tag = tag2;
            node2.Text = text2.Trim();
            node3.Tag = tag3;
            node3.Text = text3.Trim();
            node4.Tag = tag4;
            node4.Text = text4.Trim();
            node5.Tag = tag5;
            node5.Text = text5.Trim();
            node6.Tag = tag6;
            node6.Text = text6.Trim();
            node7.Tag = tag7;
            node7.Text = text7.Trim();
            node8.Tag = tag8;
            node8.Text = text8.Trim();

            ParNode.Nodes.Add(node1);
            node1.Nodes.Add(node2);
            node1.Nodes.Add(node3);
            node1.Nodes.Add(node4);
            node1.Nodes.Add(node5);
            node1.Nodes.Add(node6);
            node1.Nodes.Add(node7);
            node1.Nodes.Add(node8);

            ParNode.ExpandAll();
        }
    }
}
