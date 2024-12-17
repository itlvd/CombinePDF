namespace ConvertImageToPdf
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
      previewBox = new PictureBox();
      imageListBox = new ListBox();
      moveUpButton = new Button();
      moveDownButton = new Button();
      convertButton = new Button();
      progressBar = new ProgressBar();
      pageSizeCombo = new ComboBox();
      qualityTrackBar = new TrackBar();
      qualityLabel = new Label();
      cancelButton = new Button();
      addButton = new Button();
      removeButton = new Button();
      label1 = new Label();
      label2 = new Label();
      label3 = new Label();
      numericMargin = new NumericUpDown();
      label4 = new Label();
      ((System.ComponentModel.ISupportInitialize)previewBox).BeginInit();
      ((System.ComponentModel.ISupportInitialize)qualityTrackBar).BeginInit();
      ((System.ComponentModel.ISupportInitialize)numericMargin).BeginInit();
      SuspendLayout();
      // 
      // previewBox
      // 
      previewBox.BorderStyle = BorderStyle.FixedSingle;
      previewBox.Location = new Point(317, 12);
      previewBox.Name = "previewBox";
      previewBox.Size = new Size(471, 239);
      previewBox.SizeMode = PictureBoxSizeMode.Zoom;
      previewBox.TabIndex = 0;
      previewBox.TabStop = false;
      // 
      // imageListBox
      // 
      imageListBox.FormattingEnabled = true;
      imageListBox.HorizontalScrollbar = true;
      imageListBox.ItemHeight = 15;
      imageListBox.Location = new Point(12, 12);
      imageListBox.Name = "imageListBox";
      imageListBox.ScrollAlwaysVisible = true;
      imageListBox.Size = new Size(202, 244);
      imageListBox.TabIndex = 1;
      // 
      // moveUpButton
      // 
      moveUpButton.Location = new Point(220, 82);
      moveUpButton.Name = "moveUpButton";
      moveUpButton.Size = new Size(75, 23);
      moveUpButton.TabIndex = 2;
      moveUpButton.Text = "Lên";
      moveUpButton.UseVisualStyleBackColor = true;
      // 
      // moveDownButton
      // 
      moveDownButton.Location = new Point(220, 125);
      moveDownButton.Name = "moveDownButton";
      moveDownButton.Size = new Size(75, 23);
      moveDownButton.TabIndex = 3;
      moveDownButton.Text = "Xuống";
      moveDownButton.UseVisualStyleBackColor = true;
      // 
      // convertButton
      // 
      convertButton.Location = new Point(679, 380);
      convertButton.Name = "convertButton";
      convertButton.Size = new Size(109, 49);
      convertButton.TabIndex = 4;
      convertButton.Text = "Convert";
      convertButton.UseVisualStyleBackColor = true;
      // 
      // progressBar
      // 
      progressBar.Location = new Point(12, 397);
      progressBar.Name = "progressBar";
      progressBar.Size = new Size(508, 32);
      progressBar.TabIndex = 6;
      // 
      // pageSizeCombo
      // 
      pageSizeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
      pageSizeCombo.FormattingEnabled = true;
      pageSizeCombo.Items.AddRange(new object[] { "A0", "A1", "A2", "A3", "A4", "A5", "A6", "B5", "Legal", "Letter", "Auto" });
      pageSizeCombo.Location = new Point(317, 345);
      pageSizeCombo.Name = "pageSizeCombo";
      pageSizeCombo.Size = new Size(121, 23);
      pageSizeCombo.TabIndex = 7;
      // 
      // qualityTrackBar
      // 
      qualityTrackBar.LargeChange = 1;
      qualityTrackBar.Location = new Point(452, 275);
      qualityTrackBar.Maximum = 100;
      qualityTrackBar.Name = "qualityTrackBar";
      qualityTrackBar.Size = new Size(221, 45);
      qualityTrackBar.TabIndex = 8;
      qualityTrackBar.Value = 100;
      // 
      // qualityLabel
      // 
      qualityLabel.AutoSize = true;
      qualityLabel.Location = new Point(512, 257);
      qualityLabel.Name = "qualityLabel";
      qualityLabel.Size = new Size(79, 15);
      qualityLabel.TabIndex = 9;
      qualityLabel.Text = "Quality: 100%";
      // 
      // cancelButton
      // 
      cancelButton.Enabled = false;
      cancelButton.Location = new Point(575, 380);
      cancelButton.Name = "cancelButton";
      cancelButton.Size = new Size(98, 49);
      cancelButton.TabIndex = 10;
      cancelButton.Text = "Cancel";
      cancelButton.UseVisualStyleBackColor = true;
      // 
      // addButton
      // 
      addButton.Location = new Point(12, 274);
      addButton.Name = "addButton";
      addButton.Size = new Size(81, 46);
      addButton.TabIndex = 11;
      addButton.Text = "Thêm hình";
      addButton.UseVisualStyleBackColor = true;
      // 
      // removeButton
      // 
      removeButton.Location = new Point(114, 274);
      removeButton.Name = "removeButton";
      removeButton.Size = new Size(79, 46);
      removeButton.TabIndex = 12;
      removeButton.Text = "Xoá hình";
      removeButton.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Location = new Point(384, 275);
      label1.Name = "label1";
      label1.Size = new Size(62, 15);
      label1.TabIndex = 13;
      label1.Text = "Nén nhiều";
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Location = new Point(679, 275);
      label2.Name = "label2";
      label2.Size = new Size(65, 15);
      label2.TabIndex = 14;
      label2.Text = "Không nén";
      // 
      // label3
      // 
      label3.AutoSize = true;
      label3.Location = new Point(220, 345);
      label3.Name = "label3";
      label3.Size = new Size(84, 15);
      label3.TabIndex = 15;
      label3.Text = "Chọn khổ giấy";
      // 
      // numericMargin
      // 
      numericMargin.Location = new Point(526, 343);
      numericMargin.Name = "numericMargin";
      numericMargin.Size = new Size(120, 23);
      numericMargin.TabIndex = 16;
      // 
      // label4
      // 
      label4.AutoSize = true;
      label4.Location = new Point(473, 345);
      label4.Name = "label4";
      label4.Size = new Size(47, 15);
      label4.TabIndex = 17;
      label4.Text = "Canh lề";
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(800, 450);
      Controls.Add(label4);
      Controls.Add(numericMargin);
      Controls.Add(label3);
      Controls.Add(label2);
      Controls.Add(label1);
      Controls.Add(removeButton);
      Controls.Add(addButton);
      Controls.Add(cancelButton);
      Controls.Add(qualityLabel);
      Controls.Add(qualityTrackBar);
      Controls.Add(pageSizeCombo);
      Controls.Add(progressBar);
      Controls.Add(convertButton);
      Controls.Add(moveDownButton);
      Controls.Add(moveUpButton);
      Controls.Add(imageListBox);
      Controls.Add(previewBox);
      Icon = (Icon)resources.GetObject("$this.Icon");
      Name = "Form1";
      Text = "Image to PDF Converter - levandong";
      ((System.ComponentModel.ISupportInitialize)previewBox).EndInit();
      ((System.ComponentModel.ISupportInitialize)qualityTrackBar).EndInit();
      ((System.ComponentModel.ISupportInitialize)numericMargin).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private PictureBox previewBox;
    private ListBox imageListBox;
    private Button moveUpButton;
    private Button moveDownButton;
    private Button convertButton;
    private ProgressBar progressBar;
    private ComboBox pageSizeCombo;
    private TrackBar qualityTrackBar;
    private Label qualityLabel;
    private Button cancelButton;
    private Button addButton;
    private Button removeButton;
    private Label label1;
    private Label label2;
    private Label label3;
    private NumericUpDown numericMargin;
    private Label label4;
  }
}
