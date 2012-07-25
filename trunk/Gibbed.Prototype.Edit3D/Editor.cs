/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Gibbed.Prototype.FileFormats;

namespace Gibbed.Prototype.Edit3D
{
    public partial class Editor : Form
    {
        public Editor()
        {
            this.InitializeComponent();
        }

        public string LastFileName;
        public Pure3DFile ActiveFile;

        private void OnFileOpen(object sender, EventArgs e)
        {
            if (this.openPure3DFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.LastFileName = this.openPure3DFileDialog.FileName;
            using (var input = this.openPure3DFileDialog.OpenFile())
            {
                var pure3D = new Pure3DFile();
                pure3D.Deserialize(input);
                this.ActiveFile = pure3D;
            }

            this.UpdateNodeTree();
            this.SelectNothing();
        }

        private void OnFileSave(object sender, EventArgs e)
        {
            if (this.ActiveFile == null ||
                this.LastFileName == null)
            {
                return;
            }

            using (var output = File.Create(this.LastFileName))
            {
                this.ActiveFile.Serialize(output);
            }

            /*
            this.UpdateNodeTree();
            this.SelectNothing();
            */
        }

        private void UpdateNode(FileFormats.Pure3D.BaseNode node, TreeNodeCollection parent)
        {
            var treeNode = new TreeNode
            {
                Text = node.ToString(),
                Tag = node,
            };

            foreach (var child in node.Children)
            {
                this.UpdateNode(child, treeNode.Nodes);
            }

            parent.Add(treeNode);
        }

        private void UpdateNodeTree()
        {
            this.nodeView.BeginUpdate();
            this.nodeView.Nodes.Clear();

            var root = new TreeNode
            {
                Text = "Root",
            };

            foreach (var node in this.ActiveFile.Nodes)
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

        private void SelectNode(FileFormats.Pure3D.BaseNode node)
        {
            this.propertyGrid.SelectedObject = node;

            object preview = node.Preview();

            if (preview is Image)
            {
                this.previewPicture.Image = (Image)preview;
            }
            else
            {
                this.previewPicture.Image = null;
            }

            this.importNodeButton.Enabled = node.Importable;
            this.exportNodeButton.Enabled = node.Exportable;
        }

        private void OnSelectNode(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null || !(e.Node.Tag is FileFormats.Pure3D.BaseNode))
            {
                this.SelectNothing();
                return;
            }

            this.SelectNode((FileFormats.Pure3D.BaseNode)e.Node.Tag);
        }

        private void OnNodeExport(object sender, EventArgs e)
        {
            if (this.propertyGrid.SelectedObject == null ||
                !(this.propertyGrid.SelectedObject is FileFormats.Pure3D.BaseNode))
            {
                return;
            }

            var node = (FileFormats.Pure3D.BaseNode)this.propertyGrid.SelectedObject;
            if (node.Exportable == false)
            {
                throw new InvalidOperationException();
            }

            if (this.exportNodeFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (var output = this.exportNodeFileDialog.OpenFile())
            {
                node.Export(output);
            }
        }

        private void OnNodeImport(object sender, EventArgs e)
        {
            if (this.propertyGrid.SelectedObject == null ||
                !(this.propertyGrid.SelectedObject is FileFormats.Pure3D.BaseNode))
            {
                return;
            }

            var node = (FileFormats.Pure3D.BaseNode)this.propertyGrid.SelectedObject;
            if (node.Importable == false)
            {
                throw new InvalidOperationException();
            }

            if (this.importNodeFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (var input = this.importNodeFileDialog.OpenFile())
            {
                node.Import(input);
            }
        }
    }
}
