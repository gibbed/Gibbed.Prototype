using System;
using System.IO;
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

        private void OnFileOpen(object sender, EventArgs e)
        {
            if (this.openPure3DFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (this.ActiveStream != null)
            {
                this.ActiveStream.Close();
            }

            this.ActiveStream = File.Open(this.openPure3DFileDialog.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            this.ActiveFile = new Pure3DFile();
            this.ActiveFile.Deserialize(this.ActiveStream);

            this.UpdateNodeTree();
            this.SelectNothing();
        }

        private void OnFileSave(object sender, EventArgs e)
        {
            if (this.ActiveFile == null || this.ActiveStream == null)
            {
                return;
            }

            this.ActiveStream.SetLength(0);
            this.ActiveFile.Serialize(this.ActiveStream);
            this.ActiveStream.Flush();

            /*
            this.UpdateNodeTree();
            this.SelectNothing();
            */
        }

        private void UpdateNode(Gibbed.Prototype.FileFormats.Pure3D.BaseNode node, TreeNodeCollection parent)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = node.ToString();
            treeNode.Tag = node;

            foreach (Gibbed.Prototype.FileFormats.Pure3D.BaseNode child in node.Children)
            {
                this.UpdateNode(child, treeNode.Nodes);
            }

            parent.Add(treeNode);
        }

        private void UpdateNodeTree()
        {
            this.nodeView.BeginUpdate();
            this.nodeView.Nodes.Clear();

            TreeNode root = new TreeNode();
            root.Text = "Root";

            foreach (Gibbed.Prototype.FileFormats.Pure3D.BaseNode node in this.ActiveFile.Nodes)
            {
                this.UpdateNode(node, root.Nodes);
            }

            this.nodeView.Nodes.Add(root);
            root.Expand();
            this.nodeView.EndUpdate();
        }

        private void SelectNothing()
        {
            this.propertyGrid.SelectedObject = null;
            this.previewPicture.Image = this.previewPicture.InitialImage;
            this.importNodeButton.Enabled = false;
            this.exportNodeButton.Enabled = false;
        }

        private void SelectNode(Gibbed.Prototype.FileFormats.Pure3D.BaseNode node)
        {
            this.propertyGrid.SelectedObject = node;
            this.previewPicture.Image = node.Preview();
            this.importNodeButton.Enabled = node.Importable;
            this.exportNodeButton.Enabled = node.Exportable;
        }

        private void OnSelectNode(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null || !(e.Node.Tag is Gibbed.Prototype.FileFormats.Pure3D.BaseNode))
            {
                this.SelectNothing();
                return;
            }

            this.SelectNode((Gibbed.Prototype.FileFormats.Pure3D.BaseNode)e.Node.Tag);
        }

        private void OnNodeExport(object sender, EventArgs e)
        {
            if (this.propertyGrid.SelectedObject == null ||
                !(this.propertyGrid.SelectedObject is Gibbed.Prototype.FileFormats.Pure3D.BaseNode))
            {
                return;
            }

            Gibbed.Prototype.FileFormats.Pure3D.BaseNode node = (Gibbed.Prototype.FileFormats.Pure3D.BaseNode)this.propertyGrid.SelectedObject;
            if (node.Exportable == false)
            {
                throw new InvalidOperationException();
            }

            if (this.exportNodeFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Stream output = File.Open(this.exportNodeFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.Read);
            node.Export(output);
            output.Close();
        }

        private void OnNodeImport(object sender, EventArgs e)
        {
            if (this.propertyGrid.SelectedObject == null ||
                !(this.propertyGrid.SelectedObject is Gibbed.Prototype.FileFormats.Pure3D.BaseNode))
            {
                return;
            }

            Gibbed.Prototype.FileFormats.Pure3D.BaseNode node = (Gibbed.Prototype.FileFormats.Pure3D.BaseNode)this.propertyGrid.SelectedObject;
            if (node.Importable == false)
            {
                throw new InvalidOperationException();
            }

            if (this.importNodeFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Stream input = File.Open(this.importNodeFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            node.Import(input);
            input.Close();
        }
    }
}
