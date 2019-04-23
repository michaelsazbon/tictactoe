using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tree
{
    public partial class Form1 : Form
    {

        private int plyid;
        public int Plyid
        {
            get
            {
                return this.plyid++;
            }
            set
            {
                this.plyid = value;
            }
        }

        /*string[,] pluz = new string[3, 3] {
                { "-", "-" , "-" },
                { "-", "-" , "-" },
                { "-", "-" , "-" }
            };*/

        string[,] originply = new string[3, 3] {
                { "-", "-" , "-" },
                { "-", "-" , "-" },
                { "-", "-" , "-" }
            };

        List<string[,]> Plys = new List<string[,]>();
       

        Font tagFont = new Font("Helvetica", 8, FontStyle.Bold);

        public Form1()
        {
            InitializeComponent();

            //InitializeTreeView();

            treeView1.BeginUpdate();
            InitializeTreeViewRec();
            treeView1.EndUpdate();

            //treeView1.BeginUpdate();
            //LoadTree(treeView1, "TicTacTo.tree");
            //treeView1.EndUpdate();
        }

        private string printPluz(string[,] tmppluz)
        {
            string ret = "";
            for (int j = 0; j < 3; j++)
            {
                string line = "";
                for (int i = 0; i < 3; i++)
                {
                    line += " | " + tmppluz[j, i];
                }

                line = line.Substring(3);
                ret += line + "\n";
            }
            return ret;
        }

        public static void SaveTree(TreeView tree, string filename)
        {
            using (Stream file = File.Open(filename, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, tree.Nodes.Cast<TreeNode>().ToList());
            }
        }

        public static void LoadTree(TreeView tree, string filename)
        {
            using (Stream file = File.Open(filename, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                object obj = bf.Deserialize(file);

                TreeNode[] nodeList = (obj as IEnumerable<TreeNode>).ToArray();
                tree.Nodes.AddRange(nodeList);
            }
        }

        void treeView1_NodeMouseClick(object sender,TreeNodeMouseClickEventArgs e)
        {
            string Plyid = e.Node.Name;
            string PlySign = e.Node.Text;
            string[,] Ply = Plys[int.Parse(Plyid)];
            label1.Text = e.Node.ToolTipText;

            bool Plywin = IsPlyWin(Ply, PlySign);
            label2.Text = Plywin ? "Gagant" : "Non gagnant";
        }

        private bool IsPlyFull(string[,] tmppluz)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if(tmppluz[j, i] == "-")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsPlyWin(string[,] tmppluz, string sign)
        {  
            bool win1 = true, win2 = true, win3 = true, win4 = true;
            for (int j = 0; j < 3; j++)
            {
                win1 = true;win2 = true;
                for (int i = 0; i < 3; i++)
                {
                    if (tmppluz[j, i] != sign)
                    {
                        win1 = false;
                    }

                    if (tmppluz[i, j] != sign)
                    {
                        win2 = false;
                    }
                }
                if(win1 || win2)
                {
                    return true;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (tmppluz[i, i] != sign)
                {
                    win3 = false;
                }

                if (tmppluz[i, 2-i] != sign)
                {
                    win4 = false;
                }
            }

            if (win3 || win4)
            {
                return true;
            }

            return false;
        }

        


        private void InitializeTreeViewRec()
        {
            //treeView1.DrawMode = TreeViewDrawMode.OwnerDrawText;
            //treeView1.DrawNode +=
            //   new DrawTreeNodeEventHandler(myTreeView_DrawNode);

            treeView1.NodeMouseClick += treeView1_NodeMouseClick;

            int pid = Plyid;
            TreeNode node0 = new TreeNode("Root");
            node0.ToolTipText = printPluz(originply);
            node0.Name = pid.ToString();
            treeView1.Nodes.Add(node0);
            Plys.Insert(pid, originply);

            string[,] pluz1 = originply.Clone() as string[,];

            BuildTree(9, pluz1, treeView1.Nodes[0], "X");
        }

        private void BuildTree(int level, string[,] ply, TreeNode TreeNode, string sign)
        {
            if (level == 3 /*IsPlyFull(ply)*/)
            {
                return;
            }

            string[,] pluz1 = ply.Clone() as string[,];
            string[,] pluzOrigin1 = ply.Clone() as string[,];
            string[,] tmppluz;
            TreeNode node;


            for (int i = 0; i < level; i++)
            {
                tmppluz = pluzOrigin1.Clone() as string[,];
                node = new TreeNode(sign);
                Add(tmppluz, pluz1, sign);
                node.ToolTipText = printPluz(tmppluz);
                int pid = Plyid;
                node.Name = pid.ToString();
                TreeNode.Nodes.Add(node);
                Plys.Insert(pid, tmppluz);

                BuildTree(level - 1, tmppluz, TreeNode.Nodes[i], (sign == "X") ? "O" : "X");
            }

        }

        private void InitializeTreeView()
        {
            //treeView1.DrawMode = TreeViewDrawMode.OwnerDrawText;
            //treeView1.DrawNode +=
            //   new DrawTreeNodeEventHandler(myTreeView_DrawNode);

            int plyId = 0;

            treeView1.NodeMouseClick += treeView1_NodeMouseClick;

            treeView1.BeginUpdate();
            TreeNode node0 = new TreeNode("Root");
            node0.ToolTipText = printPluz(originply);
            node0.Name = plyId.ToString();
            treeView1.Nodes.Add(node0);

            Plys.Insert(plyId, originply);

            string[,] tmppluz;
            TreeNode node;

            plyId++;
            string[,] pluz1 = originply.Clone() as string[,];
            string[,] pluzOrigin1 = originply.Clone() as string[,];

            for (int i = 0; i < 9; i++)
            {
                
                tmppluz = pluzOrigin1.Clone() as string[,];
                node = new TreeNode("X");
                Add(tmppluz, pluz1, "X");
                node.ToolTipText = printPluz(tmppluz);
                node.Name = plyId.ToString();
                treeView1.Nodes[0].Nodes.Add(node);
                Plys.Insert(plyId, tmppluz);

                plyId++;
                string[,] pluz2 = tmppluz.Clone() as string[,];
                string[,] pluzOrigin2 = tmppluz.Clone() as string[,];

                for (int j = 0; j < 8; j++)
                {
                    tmppluz = pluzOrigin2.Clone() as string[,];
                    node = new TreeNode("O");
                    Add(tmppluz, pluz2, "O");
                    node.ToolTipText = printPluz(tmppluz);
                    node.Name = plyId.ToString();
                    treeView1.Nodes[0].Nodes[i].Nodes.Add(node);
                    Plys.Insert(plyId, tmppluz);

                    plyId++;
                    string[,] pluz3 = tmppluz.Clone() as string[,];
                    string[,] pluzOrigin3 = tmppluz.Clone() as string[,];

                    for (int h = 0; h < 7; h++)
                    {
                        tmppluz = pluzOrigin3.Clone() as string[,];
                        node = new TreeNode("X");
                        Add(tmppluz, pluz3, "X");
                        node.ToolTipText = printPluz(tmppluz);
                        node.Name = plyId.ToString();
                        treeView1.Nodes[0].Nodes[i].Nodes[j].Nodes.Add(node);
                        Plys.Insert(plyId, tmppluz);

                        plyId++;
                        string[,] pluz4 = tmppluz.Clone() as string[,];
                        string[,] pluzOrigin4 = tmppluz.Clone() as string[,];

                        for (int k = 0; k < 6; k++)
                        {
                            tmppluz = pluzOrigin4.Clone() as string[,];
                            node = new TreeNode("O");
                            Add(tmppluz, pluz4, "O");
                            node.ToolTipText = printPluz(tmppluz);
                            node.Name = plyId.ToString();
                            treeView1.Nodes[0].Nodes[i].Nodes[j].Nodes[h].Nodes.Add(node);
                            Plys.Insert(plyId, tmppluz);

                            plyId++;
                            string[,] pluz5 = tmppluz.Clone() as string[,];
                            string[,] pluzOrigin5 = tmppluz.Clone() as string[,];

                            for (int l = 0; l < 5; l++)
                            {
                                tmppluz = pluzOrigin5.Clone() as string[,];
                                node = new TreeNode("X");
                                Add(tmppluz, pluz5, "X");
                                node.ToolTipText = printPluz(tmppluz);
                                node.Name = plyId.ToString();
                                treeView1.Nodes[0].Nodes[i].Nodes[j].Nodes[h].Nodes[k].Nodes.Add(node);
                                Plys.Insert(plyId, tmppluz);

                                plyId++;
                                string[,] pluz6 = tmppluz.Clone() as string[,];
                                string[,] pluzOrigin6 = tmppluz.Clone() as string[,];

                                for (int m = 0; m < 4; m++)
                                {
                                    tmppluz = pluzOrigin6.Clone() as string[,];
                                    node = new TreeNode("O");
                                    Add(tmppluz, pluz6, "O");
                                    node.ToolTipText = printPluz(tmppluz);
                                    node.Name = plyId.ToString();
                                    treeView1.Nodes[0].Nodes[i].Nodes[j].Nodes[h].Nodes[k].Nodes[l].Nodes.Add(node);
                                    Plys.Insert(plyId, tmppluz);

                                    plyId++;
                                    string[,] pluz7 = tmppluz.Clone() as string[,];
                                    string[,] pluzOrigin7 = tmppluz.Clone() as string[,];

                                    for (int n = 0; n < 3; n++)
                                    {
                                        tmppluz = pluzOrigin7.Clone() as string[,];
                                        node = new TreeNode("X");
                                        Add(tmppluz, pluz7, "X");
                                        node.ToolTipText = printPluz(tmppluz);
                                        node.Name = plyId.ToString();
                                        treeView1.Nodes[0].Nodes[i].Nodes[j].Nodes[h].Nodes[k].Nodes[l].Nodes[m].Nodes.Add(node);
                                        Plys.Insert(plyId, tmppluz);

                                        plyId++;
                                        string[,] pluz8 = tmppluz.Clone() as string[,];
                                        string[,] pluzOrigin8 = tmppluz.Clone() as string[,];

                                        for (int o = 0; o < 2; o++)
                                        {
                                            tmppluz = pluzOrigin8.Clone() as string[,];
                                            node = new TreeNode("O");
                                            Add(tmppluz, pluz8, "O");
                                            node.ToolTipText = printPluz(tmppluz);
                                            node.Name = plyId.ToString();
                                            treeView1.Nodes[0].Nodes[i].Nodes[j].Nodes[h].Nodes[k].Nodes[l].Nodes[m].Nodes[n].Nodes.Add(node);
                                            Plys.Insert(plyId, tmppluz);

                                            plyId++;
                                            string[,] pluz9 = tmppluz.Clone() as string[,];
                                            string[,] pluzOrigin9 = tmppluz.Clone() as string[,];

                                            /*for (int p = 0; p < 1; p++)
                                            {
                                                tmppluz = pluzOrigin9.Clone() as string[,];
                                                node = new TreeNode("X");
                                                Add(tmppluz, pluz9, "X");
                                                node.ToolTipText = printPluz(tmppluz);
                                                node.Name = plyId.ToString();
                                                treeView1.Nodes[0].Nodes[i].Nodes[j].Nodes[h].Nodes[k].Nodes[l].Nodes[m].Nodes[n].Nodes[o].Nodes.Add(node);
                                                Plys.Insert(plyId, tmppluz);
                                            }*/
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }

            


            //treeView1.Nodes[0].Nodes.Add("Child 1");
            //treeView1.Nodes[0].Nodes.Add("Child 2");
            //treeView1.Nodes[0].Nodes[1].Nodes.Add("Grandchild");
            //treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add("Great Grandchild");
            treeView1.EndUpdate();

            //SaveTree(treeView1, "TicTacTo.tree");
        }


        private void Add(string[,] tmppluz, string[,] pluzi, string sign)
        {

            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (pluzi[j, i] == "-")
                    {
                        pluzi[j, i] = sign;
                        tmppluz[j, i] = sign;
                        return;
                    }
                }
            }

        }

        private void myTreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            // Draw the background and node text for a selected node.
            //if ((e.State & TreeNodeStates.Selected) != 0)
            //{
                // Draw the background of the selected node. The NodeBounds
                // method makes the highlight rectangle large enough to
                // include the text of a node tag, if one is present.
                //e.Graphics.FillRectangle(Brushes.Green, NodeBounds(e.Node));

                // Retrieve the node font. If the node font has not been set,
                // use the TreeView font.
                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null) nodeFont = ((TreeView)sender).Font;

                // Draw the node text.
                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.Black,
                    Rectangle.Inflate(e.Bounds, 10, 10));
            //}

            // Use the default background and node text.
            /*else
            {
                e.DrawDefault = true;
            }

            // If a node tag is present, draw its string representation 
            // to the right of the label text.
            if (e.Node.Tag != null)
            {
                e.Graphics.DrawString(e.Node.Tag.ToString(), tagFont,
                    Brushes.Yellow, e.Bounds.Right + 2, e.Bounds.Top);
            }

            // If the node has focus, draw the focus rectangle large, making
            // it large enough to include the text of the node tag, if present.
            if ((e.State & TreeNodeStates.Focused) != 0)
            {
                using (Pen focusPen = new Pen(Color.Black))
                {
                    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    Rectangle focusBounds = NodeBounds(e.Node);
                    focusBounds.Size = new Size(focusBounds.Width - 1,
                    focusBounds.Height - 1);
                    e.Graphics.DrawRectangle(focusPen, focusBounds);
                }
            }
            */
        }

        // Returns the bounds of the specified node, including the region 
        // occupied by the node label and any node tag displayed.
        private Rectangle NodeBounds(TreeNode node)
        {
            // Set the return value to the normal node bounds.
            Rectangle bounds = node.Bounds;
            if (node.Tag != null)
            {
                // Retrieve a Graphics object from the TreeView handle
                // and use it to calculate the display width of the tag.
                Graphics g = treeView1.CreateGraphics();
                int tagWidth = (int)g.MeasureString
                    (node.Tag.ToString(), tagFont).Width + 6;

                // Adjust the node bounds using the calculated value.
                bounds.Offset(tagWidth / 2, 0);
                bounds = Rectangle.Inflate(bounds, tagWidth / 2, 0);
                g.Dispose();
            }

            return bounds;

        }
    }
}
