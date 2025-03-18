using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class MainForm : Form
    {
        bool dataNew = false;
        bool treeExit = false;
        ComboBox cmb_Temp = new ComboBox();
        //Map.EmapInf EmapInf = new Map.EmapInf();
        TreeNode theLastNode = null;

        Data data = new Data();
        Excl excl = new Excl();
        TreeList tree = new TreeList();
        State state = new State();
        Const cst = new Const();
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cst.EdgeInfInit();
            state.InitMapFile();                
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;//行高
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//列宽自动填充
            dataGridView1.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//行标题居中
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//列标题居中

            保存SToolStripButton.Enabled = false;

            string[] Path = Environment.CommandLine.Split('\"');
            if (Path.Length>3)
            {
                data.path = Path[3];
                OpenFilePress();
            }
            else
            {
                data.path = null;
            }

        }
        private void 添加项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state.AddTreeNodeEdgeInfo(treeView1, dataGridView1);
            state.RightCommandEnableMange(treeView1.SelectedNode, contextMenuStrip1, dataGridView1);
            dataNew = true;
            保存SToolStripButton.Enabled = true;
            for (int i = 0; i < state.MapFile.EmapInfo.EdgeNum; i++)
            {
                if (((Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeSpeLimSta_Tag[i])
                    || (Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeRamp_Tag[i])
                    || (Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeTag_Tag[i])
                    || (Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgePoliceMark_Tag[i])
                    || (Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeStopPoint_Tag[i]))
                    && (dataGridView1.RowCount > 1))
                {
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;//滚动条最下面
                } 
            }
            //for (int i = 0; i < state.MapFile.EmapInfo.EdgeNum; i++)
            //{
                label1.Text =Convert.ToString(state.MapFile.EmapInfo.EdgeNum) + "   " + Convert.ToString(cst.EdgeInf_Tag) + "   " + cst.EdgeInf_Text + "\n"
                    + Convert.ToString(cst.EdgeInf_ConReZero_Tag) + "   " + cst.EdgeInf_ConReZero_Text + "\n"
                    + Convert.ToString(cst.EdgeInf_ConReOne_Tag) + "   " + cst.EdgeInf_ConReOne_Text + "\n"
                   //+ Convert.ToString(state.MapFile.EmapInfo.EdgeInfo.EdgeSpeLimSta.SpeedLimNum) + "   " + Convert.ToString(cst.EdgeInf_EdgeSpeLimSta_Tag) + "   " + cst.EdgeInf_EdgeSpeLimSta_Text[i] + "\n"
                   //+ Convert.ToString(state.MapFile.EmapInfo.EdgeInfo.EdgeRamp.RampNum) + "   " + Convert.ToString(cst.EdgeInf_EdgeRamp_Tag) + "   " + cst.EdgeInf_EdgeRamp_Text[i] + "\n"
                   //+ Convert.ToString(state.MapFile.EmapInfo.EdgeInfo.EdgeTag.EdgeTagNum) + "   " + Convert.ToString(cst.EdgeInf_EdgeTag_Tag[i]) + "   " + cst.EdgeInf_EdgeTag_Text[i] + "\n"
                    //+ Convert.ToString(state.MapFile.EmapInfo.EdgeInfo.EdgePoliceMark.EdgePoliceMarkNum) + "   " + Convert.ToString(cst.EdgeInf_EdgePoliceMark_Tag[i]) + "   " + cst.EdgeInf_EdgePoliceMark_Text[i] + "\n"
                    //+ Convert.ToString(state.MapFile.EmapInfo.EdgeInfo.EdgeStopPoint.StopPointNum) + "   " + Convert.ToString(cst.EdgeInf_EdgeStopPoint_Tag[i]) + "   " + cst.EdgeInf_EdgeStopPoint_Text[i] + "\n"
                    ;
            //}
        }

        private void 删除项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            state.RemoveTreeNodeEdgeInfo(treeView1);
            state.RightCommandEnableMange(treeView1.SelectedNode, contextMenuStrip1, dataGridView1);
            dataNew = true;
            保存SToolStripButton.Enabled = true;
            for (int i = 0; i < state.MapFile.EmapInfo.EdgeNum; i++)
            {
                if (((Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeSpeLimSta_Tag[i])
                    || (Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeRamp_Tag[i])
                    || (Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeTag_Tag[i])
                    || (Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgePoliceMark_Tag[i])
                    || (Convert.ToUInt16(treeView1.SelectedNode.Tag) == cst.EdgeInf_EdgeStopPoint_Tag[i]))
                    && (dataGridView1.RowCount > 1))
                {
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;//滚动条最下面
                }
            }             
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            state.RightCommandEnableMange(e.Node, contextMenuStrip1, dataGridView1);

            if (this.treeView1.SelectedNode != null)
            {
                theLastNode = treeView1.SelectedNode;
            }    
        }
        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.ForeColor = Color.Blue;
            //e.Node.NodeFont = new Font("宋体", 9, FontStyle.Underline);
            if (theLastNode != null)
            {
                theLastNode.ForeColor = SystemColors.WindowText;
                //theLastNode.NodeFont = new Font("宋体", 9, FontStyle.Regular);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            state.SaveExcelDataToMapFile(treeView1, dataGridView1);
            state.RightCommandEnableMange(treeView1.SelectedNode, contextMenuStrip1, dataGridView1);
            dataNew = true;
            保存SToolStripButton.Enabled = true;
        }

        private void 新建NToolStripButton_Click(object sender, EventArgs e)
        {
            treeExit = tree.treeViewNew(treeView1,0,treeExit);
            state.InitMapFile();
            state.RightCommandEnableMange(treeView1.SelectedNode, contextMenuStrip1, dataGridView1);
            dataNew = true;
            data.path = null;
            保存SToolStripButton.Enabled = true;
        }
        private void 打开OToolStripButton_Click(object sender, EventArgs e)
        {
            string name = null;
            OpenFileDialog fn = new OpenFileDialog();
            fn.InitialDirectory = Application.StartupPath;
            //fn.Filter = "所有文件(*.*)|*.*";
            fn.Filter = "BIN文件(*.bin)|*.bin";
            fn.FilterIndex = 2;
            fn.RestoreDirectory = true;
            fn.CheckFileExists = false;

            if (fn.ShowDialog() == DialogResult.OK)
            {
                data.path = fn.FileName.ToString();
                name = data.path.Substring(data.path.LastIndexOf("\\") + 1);
                OpenFilePress();
            }
            
        }
        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            if (treeExit == true)
            {
                state.SaveFixDataToMapFile();
                if (true == state.CheakMapFileDataValid(treeView1))
                {
                    data.DataMake(state.MapFile);
                }
            }
            dataNew = false;
            if(data.path!=null)
            {
                保存SToolStripButton.Enabled = false;
            }  
            state.RightCommandEnableMange(treeView1.SelectedNode, contextMenuStrip1, dataGridView1);
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(dataNew == true)
            {
                if (treeExit == true)
                {
                    if (MessageBox.Show("是否将更改保存到Map_Inf配置文件中？", "电子地图文件", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        state.SaveFixDataToMapFile();
                        if (true == state.CheakMapFileDataValid(treeView1))
                        {
                            data.DataMake(state.MapFile);
                        }
                    }
                }
            }     
        }
        private void OpenFilePress()
        {
            state.MapFile = data.DataRead();
            if (state.MapFile.Fh.FileLen <= Const.MapFileLen)
            {
                treeExit = tree.treeViewNew(treeView1, state.MapFile.EmapInfo.EdgeNum, treeExit);
            }
            dataNew = false;
            保存SToolStripButton.Enabled = false;
            //state.RightCommandEnableMange(treeView1.SelectedNode, contextMenuStrip1, dataGridView1);//打开地址为空时报错
        }
        /*添加行序号*/
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture),
                this.dataGridView1.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 5);
        }
    }
}
