using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gibbed.Prototype.FileFormats;

namespace Gibbed.Prototype.Edit3D
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();
        }

        ~Editor()
        {
            if (this.ActiveStream != null)
            {
                this.ActiveStream.Close();
            }
        }

        public Stream ActiveStream = null;
        public Pure3DFile ActiveFile = null;

        private void OnOpenFile(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (this.ActiveStream != null)
            {
                this.ActiveStream.Close();
            }

            this.ActiveStream = File.Open(this.openFileDialog.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            this.ActiveFile = new Pure3DFile();
            this.ActiveFile.Deserialize(this.ActiveStream);

            this.UpdateNodeTree();
        }

        private void UpdateNode(Gibbed.Prototype.FileFormats.Pure3D.Node node, TreeNodeCollection parent)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = node.ToString();
            treeNode.Tag = node;

            foreach (Gibbed.Prototype.FileFormats.Pure3D.Node child in node.Children)
            {
                this.UpdateNode(child, treeNode.Nodes);
            }

            parent.Add(treeNode);
        }

        private void UpdateNodeTree()
        {
            this.nodeView.BeginUpdate();
            this.nodeView.Nodes.Clear();

            foreach (Gibbed.Prototype.FileFormats.Pure3D.Node node in this.ActiveFile.Nodes)
            {
                this.UpdateNode(node, this.nodeView.Nodes);
            }

            this.nodeView.EndUpdate();
        }

        private void OnSelectNode(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null)
            {
                return;
            }

            if (!(e.Node.Tag is Gibbed.Prototype.FileFormats.Pure3D.Node))
            {
                return;
            }

            this.propertyGrid.SelectedObject = e.Node.Tag;
            this.previewPicture.Image = ((Gibbed.Prototype.FileFormats.Pure3D.Node)(e.Node.Tag)).Preview();
        }
    }
}
