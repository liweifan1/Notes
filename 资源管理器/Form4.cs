using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Data.SqlClient;

namespace 资源管理器
{
    public partial class Form4 : Form
    {
        ConstantList constant = new ConstantList();


        public Form4()
        {
            InitializeComponent();
        }
        private void toolStripSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)// 在结点展开后发生 展开子结点
        {

        }
        private void directoryTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)// 在将要展开结点时发生 加载子结点
        {

        }




        private void treeView1_AfterExpand_1(object sender, TreeViewEventArgs e)
        {
            e.Node.Nodes.Clear();
            if (e.Node.Tag.ToString() == "桌面")
            {
                //实例化TreeNode类 TreeNode(string text,int imageIndex,int selectImageIndex)            
                TreeNode rootNode = new TreeNode("我的电脑",
                ConstantList.MyComputer, ConstantList.MyComputer);  //载入显示 选择显示
                rootNode.Tag = "我的电脑";                            //树节点数据
                rootNode.Text = "我的电脑";                           //树节点标签内容
                e.Node.Nodes.Add(rootNode);


                //显示MyDocuments(我的文档)结点
                var myDocuments = Environment.GetFolderPath           //获取计算机我的文档文件夹
                (Environment.SpecialFolder.MyDocuments);
                TreeNode DocNode = new TreeNode(myDocuments);
                DocNode.Tag = "我的文档";                            //设置结点名称
                DocNode.Text = "我的文档";
                DocNode.ImageIndex = ConstantList.MyDocuments;         //设置获取结点显示图片
                DocNode.SelectedImageIndex = ConstantList.MyDocuments; //设置选择显示图片
                e.Node.Nodes.Add(DocNode);                          //rootNode目录下加载
            }
            /*TreeNode reNode = new TreeNode("回收站",
           ConstantList.MyComputer, ConstantList.MyComputer);  //载入显示 选择显示
            reNode.Tag = "回收站";                            //树节点数据
            reNode.Text = "回收站";                           //树节点标签内容
            DocNode.ImageIndex = ConstantList.recycle;         //设置获取结点显示图片
            DocNode.SelectedImageIndex = ConstantList.recycle; //设置选择显示图片
            e.Node.Nodes.Add(reNode);*/

            
            if (e.Node.Tag.ToString() == "桌面")
            {
               constant.path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);  //获取桌面地址 
            }
            else
            {
                constant.path = e.Node.Tag.ToString();
            }
            string[] dics = Directory.GetDirectories(constant.path);
            foreach (string dic in dics)
            {
                TreeNode subNode = new TreeNode(new DirectoryInfo(dic).Name); //实例化
                subNode.Name = new DirectoryInfo(dic).FullName;               //完整目录
                subNode.Tag = subNode.Name;
                //subNode.ImageIndex = ConstantList.ClosedFolder;       //获取节点显示图片
                subNode.SelectedImageIndex = ConstantList.OpenFolder; //选择节点显示图片

                e.Node.Nodes.Add(subNode);
                subNode.Nodes.Add("");   //加载空节点 实现+号

            }
            DirectoryInfo dir = new DirectoryInfo(constant.path);//实例目录与子目录
            FileInfo[] fileInfo = dir.GetFiles();//获取当前目录文件列表
            long length;
            for (int i = 0; i < fileInfo.Length; i++)
            {
                //int itemNumber = this.listView1.Items.Count;
                ListViewItem listItem = new ListViewItem();
                listItem.Text = "[" + (i + 1) + "] " + fileInfo[i].Name;    //显示文件名
                length = fileInfo[i].Length;                                //获取当前文件大小
                listItem.SubItems.Add(Math.Ceiling(decimal.Divide(fileInfo[i].Length, 1024)) + " KB");

                listItem.SubItems.Add(fileInfo[i].Extension + "文件");//获取文件扩展名时可用Substring除去点 否则显示".txt文件"
                listItem.SubItems.Add(fileInfo[i].LastWriteTime.ToString());//获取文件最后访问时间
                this.listView1.Items.Add(listItem); //加载数据至filesList
            }

        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            try
            {
                //定义变量
                long length;                        //文件大小
                string path;                        //文件路径
                listView1.Items.Clear();
                if (e.Node.Tag.ToString() == "桌面")
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);  //获取桌面地址 
                }
                else
                {
                    path = e.Node.Tag.ToString();
                }
                DirectoryInfo dir = new DirectoryInfo(path);//实例目录与子目录
                FileInfo[] fileInfo = dir.GetFiles();//获取当前目录文件列表

                for (int i = 0; i < fileInfo.Length; i++)
                {
                    //int itemNumber = this.listView1.Items.Count;
                    ListViewItem listItem = new ListViewItem();
                    listItem.Text = "[" + (i + 1) + "] " + fileInfo[i].Name;    //显示文件名
                    length = fileInfo[i].Length;                                //获取当前文件大小
                    listItem.SubItems.Add(Math.Ceiling(decimal.Divide(fileInfo[i].Length, 1024)) + " KB");

                    listItem.SubItems.Add(fileInfo[i].Extension + "文件");//获取文件扩展名时可用Substring除去点 否则显示".txt文件"
                    listItem.SubItems.Add(fileInfo[i].LastWriteTime.ToString());//获取文件最后访问时间
                    this.listView1.Items.Add(listItem); //加载数据至filesList
                }

            }
            catch (Exception msg)  //异常处理
            {
                MessageBox.Show(msg.Message);
            }
        }

        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.Nodes[0].Tag = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            

        }

        private void 新建文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path;
             
            path = treeView1.SelectedNode.Tag.ToString();
         
            Directory.CreateDirectory(path + "\\" + "新建文件夹1");
            
           
            treeView1.SelectedNode.Nodes.Add("");   //加载空节点 实现+号
            treeView1.SelectedNode.Collapse();
            treeView1.SelectedNode.Expand();
           
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            

        }

        private void treeView1_NodeMouseClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {

            treeView1.SelectedNode = e.Node;
            e.Node.ContextMenuStrip = contextMenuStrip1;
               
            
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path;

            path = treeView1.SelectedNode.Tag.ToString();
            Directory.Delete(path );

            treeView1.SelectedNode.Remove();   //加载空节点 实现+号//
            //treeView1.SelectedNode.Parent.Collapse();
            
            //Directory.treeView1.SelectedNode.Parent.Expand();
            
        }

        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.LabelEdit = true;
            treeView1.SelectedNode.BeginEdit();
            
           
        }
       
        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string path;
            path = treeView1.SelectedNode.Tag.ToString();
            string rename = path.Substring(0, path.LastIndexOf("\\") + 1);//获取文件路径
            string Rename = rename + e.Label;
            Directory.Move(path,Rename );
            e.Node.EndEdit(true);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            treeView1.Visible = false;
            
            label1.Visible = false;
            button1.Visible = false;
            listView1.Dock = DockStyle.Fill;
        }

        private void 同步ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }


        private void CheckAddress(SqlConnection conn,SqlCommand cmd)
        {
            try
            {
                conn.Open();
                constant.schemaInformation = conn.GetSchema("Tables",constant.restrictionValues);

                foreach (DataRow row in constant.schemaInformation.Rows)
                {
                    constant.tablenames.Add(row.ItemArray[2].ToString());
                }
            }
            finally
            {
                constant.conLocation.Close();
            }
        }

        private void 同步到本地ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region 连接到数据库
            //先打开两个类库文件
           

            // con.ConnectionString = "server=505-03;database=ttt;user=sa;pwd=123";
            constant.conLocation.ConnectionString = "server=.;database=Cloud_Location;uid=sa;pwd=knxy0616";
            constant.conLocation.Open();
            constant.conLocation.GetSchema();
            /*
            SqlDataAdapter 对象。 用于填充DataSet （数据集）。
            SqlDataReader 对象。 从数据库中读取流..
            后面要做增删改查还需要用到 DataSet 对象。
            */
            constant.comLocation.Connection = constant.conLocation;
            constant.comLocation.CommandType = CommandType.Text;
            constant.comLocation.CommandText = "USE Cloud_Location;SELECT * FROM FileInfo";

            constant.dr = constant.comLocation.ExecuteReader();//执行SQL语句
         
            constant.dr.Close();//关闭执行
            constant.conLocation.Close();//关闭数据库

            #endregion



        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";//注意这里写路径时要用c:\\而不是c:\
            openFileDialog1.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                constant.SearhPathFileName = openFileDialog1.FileName;
                constant.SearchFileName = true;
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            constant.ToFilePath = toolStripComboBox1.Text;

        }
    }
}
