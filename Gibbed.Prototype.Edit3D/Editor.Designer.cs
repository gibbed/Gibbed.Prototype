namespace Gibbed.Prototype.Edit3D
{
    partial class Editor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.nodeView = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.previewPicture = new System.Windows.Forms.PictureBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.nodeToolStrip = new System.Windows.Forms.ToolStrip();
            this.exportNodeButton = new System.Windows.Forms.ToolStripButton();
            this.importNodeButton = new System.Windows.Forms.ToolStripButton();
            this.editorToolStrip = new System.Windows.Forms.ToolStrip();
            this.newFileButton = new System.Windows.Forms.ToolStripButton();
            this.openFileButton = new System.Windows.Forms.ToolStripButton();
            this.saveFileButton = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewPicture)).BeginInit();
            this.nodeToolStrip.SuspendLayout();
            this.editorToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.nodeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(784, 539);
            this.splitContainer1.SplitterDistance = 261;
            this.splitContainer1.TabIndex = 0;
            // 
            // nodeView
            // 
            this.nodeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeView.Location = new System.Drawing.Point(0, 0);
            this.nodeView.Name = "nodeView";
            this.nodeView.Size = new System.Drawing.Size(261, 539);
            this.nodeView.TabIndex = 0;
            this.nodeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnSelectNode);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.AutoScroll = true;
            this.splitContainer2.Panel1.Controls.Add(this.previewPicture);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainer2.Panel2.Controls.Add(this.nodeToolStrip);
            this.splitContainer2.Size = new System.Drawing.Size(519, 539);
            this.splitContainer2.SplitterDistance = 165;
            this.splitContainer2.TabIndex = 0;
            // 
            // previewPicture
            // 
            this.previewPicture.BackColor = System.Drawing.Color.Black;
            this.previewPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.previewPicture.Location = new System.Drawing.Point(0, 0);
            this.previewPicture.Name = "previewPicture";
            this.previewPicture.Size = new System.Drawing.Size(519, 165);
            this.previewPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.previewPicture.TabIndex = 0;
            this.previewPicture.TabStop = false;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 25);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(519, 345);
            this.propertyGrid.TabIndex = 0;
            // 
            // nodeToolStrip
            // 
            this.nodeToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportNodeButton,
            this.importNodeButton});
            this.nodeToolStrip.Location = new System.Drawing.Point(0, 0);
            this.nodeToolStrip.Name = "nodeToolStrip";
            this.nodeToolStrip.Size = new System.Drawing.Size(519, 25);
            this.nodeToolStrip.TabIndex = 1;
            this.nodeToolStrip.Text = "toolStrip2";
            // 
            // exportNodeButton
            // 
            this.exportNodeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exportNodeButton.Image = global::Gibbed.Prototype.Edit3D.Properties.Resources.usb_stick_blue;
            this.exportNodeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportNodeButton.Name = "exportNodeButton";
            this.exportNodeButton.Size = new System.Drawing.Size(23, 22);
            this.exportNodeButton.Text = "Export Node Data";
            // 
            // importNodeButton
            // 
            this.importNodeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.importNodeButton.Image = global::Gibbed.Prototype.Edit3D.Properties.Resources.usb_stick_orange;
            this.importNodeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importNodeButton.Name = "importNodeButton";
            this.importNodeButton.Size = new System.Drawing.Size(23, 22);
            this.importNodeButton.Text = "Import Node Data";
            // 
            // editorToolStrip
            // 
            this.editorToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileButton,
            this.openFileButton,
            this.saveFileButton});
            this.editorToolStrip.Location = new System.Drawing.Point(0, 0);
            this.editorToolStrip.Name = "editorToolStrip";
            this.editorToolStrip.Size = new System.Drawing.Size(784, 25);
            this.editorToolStrip.TabIndex = 1;
            this.editorToolStrip.Text = "toolStrip1";
            // 
            // newFileButton
            // 
            this.newFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newFileButton.Enabled = false;
            this.newFileButton.Image = global::Gibbed.Prototype.Edit3D.Properties.Resources.document_new;
            this.newFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newFileButton.Name = "newFileButton";
            this.newFileButton.Size = new System.Drawing.Size(23, 22);
            this.newFileButton.Text = "New Pure3D File";
            // 
            // openFileButton
            // 
            this.openFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openFileButton.Image = global::Gibbed.Prototype.Edit3D.Properties.Resources.folder;
            this.openFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(23, 22);
            this.openFileButton.Text = "Open Pure3D File";
            this.openFileButton.Click += new System.EventHandler(this.OnOpenFile);
            // 
            // saveFileButton
            // 
            this.saveFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveFileButton.Image = global::Gibbed.Prototype.Edit3D.Properties.Resources.floppy;
            this.saveFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveFileButton.Name = "saveFileButton";
            this.saveFileButton.Size = new System.Drawing.Size(23, 22);
            this.saveFileButton.Text = "Save Pure3D File";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "p3d";
            this.openFileDialog.Filter = "Pure3D Files (*.p3d)|*.p3d|All Files (*.*)|*.*";
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 564);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.editorToolStrip);
            this.Name = "Editor";
            this.Text = "Edit3D";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.previewPicture)).EndInit();
            this.nodeToolStrip.ResumeLayout(false);
            this.nodeToolStrip.PerformLayout();
            this.editorToolStrip.ResumeLayout(false);
            this.editorToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView nodeView;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ToolStrip nodeToolStrip;
        private System.Windows.Forms.ToolStrip editorToolStrip;
        private System.Windows.Forms.ToolStripButton newFileButton;
        private System.Windows.Forms.ToolStripButton openFileButton;
        private System.Windows.Forms.ToolStripButton saveFileButton;
        private System.Windows.Forms.ToolStripButton exportNodeButton;
        private System.Windows.Forms.ToolStripButton importNodeButton;
        private System.Windows.Forms.PictureBox previewPicture;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

