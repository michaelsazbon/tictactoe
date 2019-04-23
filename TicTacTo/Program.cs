using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TicTacTo
{
    class Program
    {

        private static string[,] originply = new string[3, 3] {
                { "-", "-" , "-" },
                { "-", "-" , "-" },
                { "-", "-" , "-" }
            };

        private static List<string[,]> Plys = new List<string[,]>();

        private static XmlDocument GameTree = new XmlDocument();

        static void Main(string[] args)
        {
            InitializeGameTree();
        }

        private static void InitializeGameTree()
        {
            GameTree.PreserveWhitespace = true;
            XmlNode rootNode = GameTree.CreateElement("Root");
            GameTree.AppendChild(rootNode);

            //XmlNode Node = GameTree.CreateElement("Ply");
            //Node.InnerText = printPluz(originply);
            rootNode.AppendChild(GameTree.CreateCDataSection(printPluz(originply)));

            GameTree.AppendChild(rootNode);
            GameTree.Save("test-doc.xml");
        }

        /*private void InitializeTreeView()
        {
            //treeView1.DrawMode = TreeViewDrawMode.OwnerDrawText;
            //treeView1.DrawNode +=
            //   new DrawTreeNodeEventHandler(myTreeView_DrawNode);

            int plyId = 0;

            GameTree.PreserveWhitespace = true;

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

                                            for (int p = 0; p < 1; p++)
                                            {
                                                tmppluz = pluzOrigin9.Clone() as string[,];
                                                node = new TreeNode("X");
                                                Add(tmppluz, pluz9, "X");
                                                node.ToolTipText = printPluz(tmppluz);
                                                node.Name = plyId.ToString();
                                                treeView1.Nodes[0].Nodes[i].Nodes[j].Nodes[h].Nodes[k].Nodes[l].Nodes[m].Nodes[n].Nodes[o].Nodes.Add(node);
                                                Plys.Insert(plyId, tmppluz);
                                            }
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

            SaveTree(treeView1, "TicTacTo.tree");
        }*/


        private static void Add(string[,] tmppluz, string[,] pluzi, string sign)
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


        private static string printPluz(string[,] tmppluz)
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
                ret += line + Environment.NewLine;
            }
            return ret;
        }

    }
}
