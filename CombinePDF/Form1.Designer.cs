namespace CombinePDF
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      listBoxFiles = new ListBox();
      btnAddFiles = new Button();
      btnMoveUp = new Button();
      btnMoveDown = new Button();
      btnRemove = new Button();
      btnMerge = new Button();
      openFileDialog1 = new OpenFileDialog();
      saveFileDialog1 = new SaveFileDialog();
      progressBar1 = new ProgressBar();
      btnCancel = new Button();
      lblTimeRemaining = new Label();
      lblProgress = new Label();
      lblCurrentFile = new Label();
      SuspendLayout();
      // 
      // listBoxFiles
      // 
      listBoxFiles.FormattingEnabled = true;
      listBoxFiles.ItemHeight = 15;
      listBoxFiles.Location = new Point(31, 49);
      listBoxFiles.Name = "listBoxFiles";
      listBoxFiles.Size = new Size(615, 139);
      listBoxFiles.TabIndex = 0;
      // 
      // btnAddFiles
      // 
      btnAddFiles.Location = new Point(220, 208);
      btnAddFiles.Name = "btnAddFiles";
      btnAddFiles.Size = new Size(75, 40);
      btnAddFiles.TabIndex = 1;
      btnAddFiles.Text = "Thêm file";
      btnAddFiles.UseVisualStyleBackColor = true;
      btnAddFiles.Click += btnAddFiles_Click;
      // 
      // btnMoveUp
      // 
      btnMoveUp.Location = new Point(652, 49);
      btnMoveUp.Name = "btnMoveUp";
      btnMoveUp.Size = new Size(113, 48);
      btnMoveUp.TabIndex = 2;
      btnMoveUp.Text = "Di chuyển lên";
      btnMoveUp.UseVisualStyleBackColor = true;
      btnMoveUp.Click += btnMoveUp_Click;
      // 
      // btnMoveDown
      // 
      btnMoveDown.Location = new Point(652, 141);
      btnMoveDown.Name = "btnMoveDown";
      btnMoveDown.Size = new Size(113, 47);
      btnMoveDown.TabIndex = 3;
      btnMoveDown.Text = "Di chuyển xuống";
      btnMoveDown.UseVisualStyleBackColor = true;
      btnMoveDown.Click += btnMoveDown_Click;
      // 
      // btnRemove
      // 
      btnRemove.Location = new Point(362, 208);
      btnRemove.Name = "btnRemove";
      btnRemove.Size = new Size(75, 40);
      btnRemove.TabIndex = 4;
      btnRemove.Text = "Xoá file";
      btnRemove.UseVisualStyleBackColor = true;
      btnRemove.Click += btnRemove_Click;
      // 
      // btnMerge
      // 
      btnMerge.Location = new Point(603, 386);
      btnMerge.Name = "btnMerge";
      btnMerge.Size = new Size(160, 41);
      btnMerge.TabIndex = 5;
      btnMerge.Text = "Bắt đầu ghép file";
      btnMerge.UseVisualStyleBackColor = true;
      btnMerge.Click += btnMerge_Click;
      // 
      // openFileDialog1
      // 
      openFileDialog1.FileName = "openFileDialog1";
      // 
      // progressBar1
      // 
      progressBar1.Location = new Point(31, 386);
      progressBar1.Name = "progressBar1";
      progressBar1.Size = new Size(545, 23);
      progressBar1.Style = ProgressBarStyle.Continuous;
      progressBar1.TabIndex = 6;
      progressBar1.Visible = false;
      // 
      // btnCancel
      // 
      btnCancel.Location = new Point(603, 331);
      btnCancel.Name = "btnCancel";
      btnCancel.Size = new Size(160, 42);
      btnCancel.TabIndex = 7;
      btnCancel.Text = "Huỷ ghép file";
      btnCancel.UseVisualStyleBackColor = true;
      btnCancel.Click += btnCancel_Click;
      // 
      // lblTimeRemaining
      // 
      lblTimeRemaining.AutoSize = true;
      lblTimeRemaining.Location = new Point(482, 358);
      lblTimeRemaining.Name = "lblTimeRemaining";
      lblTimeRemaining.Size = new Size(94, 15);
      lblTimeRemaining.TabIndex = 8;
      lblTimeRemaining.Text = "Thời gian còn lại";
      // 
      // lblProgress
      // 
      lblProgress.AutoSize = true;
      lblProgress.Location = new Point(238, 358);
      lblProgress.Name = "lblProgress";
      lblProgress.Size = new Size(0, 15);
      lblProgress.TabIndex = 9;
      // 
      // lblCurrentFile
      // 
      lblCurrentFile.AutoSize = true;
      lblCurrentFile.Location = new Point(31, 358);
      lblCurrentFile.Name = "lblCurrentFile";
      lblCurrentFile.Size = new Size(83, 15);
      lblCurrentFile.TabIndex = 10;
      lblCurrentFile.Text = "File đang xử lý";
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(800, 450);
      Controls.Add(lblCurrentFile);
      Controls.Add(lblProgress);
      Controls.Add(lblTimeRemaining);
      Controls.Add(btnCancel);
      Controls.Add(progressBar1);
      Controls.Add(btnMerge);
      Controls.Add(btnRemove);
      Controls.Add(btnMoveDown);
      Controls.Add(btnMoveUp);
      Controls.Add(btnAddFiles);
      Controls.Add(listBoxFiles);
      Icon = (Icon)resources.GetObject("$this.Icon");
      Name = "Form1";
      Text = "Tool siêu cấp vip pro merge pdf - levandong";
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private ListBox listBoxFiles;
    private Button btnAddFiles;
    private Button btnMoveUp;
    private Button btnMoveDown;
    private Button btnRemove;
    private Button btnMerge;
    private OpenFileDialog openFileDialog1;
    private SaveFileDialog saveFileDialog1;
    private ProgressBar progressBar1;
    private Button btnCancel;
    private Label lblTimeRemaining;
    private Label lblProgress;
    private Label lblCurrentFile;
  }
}
