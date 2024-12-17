using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Drawing.Printing;

namespace ConvertImageToPdf
{
  public partial class Form1 : Form
  {
    private List<string> imagePaths = new List<string>();
    private CancellationTokenSource cancellationTokenSource;
    public Form1()
    {
      InitializeComponent();
      SetupEventHandlers();
      pageSizeCombo.SelectedIndex = 0;
    }

    private void SetupEventHandlers()
    {
      addButton.Click += AddButton_Click;
      removeButton.Click += RemoveButton_Click;
      moveUpButton.Click += MoveUpButton_Click;
      moveDownButton.Click += MoveDownButton_Click;
      convertButton.Click += ConvertButton_Click;
      imageListBox.SelectedIndexChanged += ImageListBox_SelectedIndexChanged;
      qualityTrackBar.ValueChanged += QualityTrackBar_ValueChanged;
      cancelButton.Click += CancelButton_Click;
    }

    private void QualityTrackBar_ValueChanged(object sender, EventArgs e)
    {
      qualityLabel.Text = $"Quality: {qualityTrackBar.Value}%";
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog ofd = new OpenFileDialog())
      {
        ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
        ofd.Multiselect = true;

        if (ofd.ShowDialog() == DialogResult.OK)
        {
          // Lấy danh sách các file theo đúng thứ tự đã chọn
          string[] selectedFiles = ofd.FileNames;
          for (int i = 0; i < selectedFiles.Length; i++)
          {
            // Thêm đường dẫn vào List
            imagePaths.Add(selectedFiles[i]);
            // Thêm tên file vào ListBox
            imageListBox.Items.Add(System.IO.Path.GetFileName(selectedFiles[i]));
          }
        }
      }
    }

    private void RemoveButton_Click(object sender, EventArgs e)
    {
      if (imageListBox.SelectedIndex != -1)
      {
        imagePaths.RemoveAt(imageListBox.SelectedIndex);
        imageListBox.Items.RemoveAt(imageListBox.SelectedIndex);
      }
    }

    private void MoveUpButton_Click(object sender, EventArgs e)
    {
      MoveItem(-1);
    }

    private void MoveDownButton_Click(object sender, EventArgs e)
    {
      MoveItem(1);
    }


    private void MoveItem(int direction)
    {
      int selectedIndex = imageListBox.SelectedIndex;
      if (selectedIndex < 0) return;

      int newIndex = selectedIndex + direction;
      if (newIndex < 0 || newIndex >= imageListBox.Items.Count) return;

      // Swap items in both lists
      var tempPath = imagePaths[selectedIndex];
      var tempItem = imageListBox.Items[selectedIndex];

      imagePaths[selectedIndex] = imagePaths[newIndex];
      imageListBox.Items[selectedIndex] = imageListBox.Items[newIndex];

      imagePaths[newIndex] = tempPath;
      imageListBox.Items[newIndex] = tempItem;

      imageListBox.SelectedIndex = newIndex;
    }

    private void ImageListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (imageListBox.SelectedIndex != -1)
      {
        try
        {
          using (var image = System.Drawing.Image.FromFile(imagePaths[imageListBox.SelectedIndex]))
          {
            previewBox.Image?.Dispose();
            previewBox.Image = new Bitmap(image);
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Error loading image: {ex.Message}");
        }
      }
    }


    private void CancelButton_Click(object sender, EventArgs e)
    {
      cancellationTokenSource?.Cancel();
    }


    private async void PreviewButton_Click(object sender, EventArgs e)
    {
      if (imagePaths.Count == 0)
      {
        MessageBox.Show("Please add some images first!");
        return;
      }

      string tempPdfPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "preview.pdf");
      await CreatePDFAsync(tempPdfPath, true);
    }

    private async void ConvertButton_Click(object sender, EventArgs e)
    {
      if (imagePaths.Count == 0)
      {
        MessageBox.Show("Please add some images first!");
        return;
      }

      using (SaveFileDialog sfd = new SaveFileDialog())
      {
        sfd.Filter = "PDF files (*.pdf)|*.pdf";
        sfd.FilterIndex = 1;

        if (sfd.ShowDialog() == DialogResult.OK)
        {
          await CreatePDFAsync(sfd.FileName, false);
        }
      }
    }

    private PageSize GetSelectedPageSize()
    {
      // Tạo biến để lưu selected size
      string selectedSize = "A4"; // Mặc định A4

      // Sử dụng Invoke để lấy giá trị từ UI thread
      if (this.InvokeRequired)
      {
        this.Invoke((MethodInvoker)delegate
        {
          selectedSize = pageSizeCombo.SelectedItem?.ToString() ?? "A4";
        });
      }
      else
      {
        selectedSize = pageSizeCombo.SelectedItem?.ToString() ?? "A4";
      }

      switch (selectedSize)
      {
        case "A0":
          return PageSize.A0;
        case "A1":
          return PageSize.A1;
        case "A2":
          return PageSize.A2;
        case "A3":
          return PageSize.A3;
        case "A5":
          return PageSize.A5;
        case "A6":
          return PageSize.A6;
        case "Letter":
          return PageSize.LETTER;
        case "Legal":
          return PageSize.LEGAL;
        case "B5":
          return PageSize.B5;
        case "Auto":
          return CalculateOptimalPageSize();
        case "A4":
        default:
          return PageSize.A4;
      }
    }

    private PageSize CalculateOptimalPageSize()
    {
      float maxWidth = 0;
      float maxHeight = 0;
      float margin = 0;

      // Lấy giá trị margin từ UI thread
      this.Invoke((MethodInvoker)delegate
      {
        margin = (float)numericMargin.Value;
      });

      foreach (string imagePath in imagePaths)
      {
        using (var img = System.Drawing.Image.FromFile(imagePath))
        {
          // Chuyển pixel sang point (72 points = 1 inch)
          float widthInPoints = img.Width * 72f / img.HorizontalResolution;
          float heightInPoints = img.Height * 72f / img.VerticalResolution;

          maxWidth = Math.Max(maxWidth, widthInPoints);
          maxHeight = Math.Max(maxHeight, heightInPoints);
        }
      }

      // Thêm margins (36 points mỗi bên)
      maxWidth += (margin * 2);  // 36 * 2
      maxHeight += (margin * 2); // 36 * 2

      return new PageSize(maxWidth, maxHeight);
    }

    private async Task CreatePDFAsync(string outputPath, bool isPreview)
    {
      try
      {
        cancellationTokenSource = new CancellationTokenSource();
        convertButton.Enabled = false;
        cancelButton.Enabled = true;
        progressBar.Value = 0;

        // Lấy giá trị quality trước khi vào Task.Run
        float quality = 0;
        string selectedPageSize = "Auto";
        float margin = 0;

        this.Invoke((MethodInvoker)delegate
        {
          quality = qualityTrackBar.Value / 100f;
          selectedPageSize = pageSizeCombo.SelectedItem?.ToString() ?? "Auto";
          margin = (float)numericMargin.Value;
        });

        await Task.Run(() =>
        {
          // Thiết lập compression level dựa trên quality đã lấy
          var writerProperties = new WriterProperties();
          if (quality < 1)
          {
            int compressionLevel = (int)((1 - quality) * 9);
            writerProperties.SetCompressionLevel(compressionLevel);
          }

          using (var writer = new PdfWriter(outputPath, writerProperties))
          {
            using (var pdf = new PdfDocument(writer))
            {
              var pageSize = GetSelectedPageSize();
              var document = new Document(pdf, pageSize);
              document.SetMargins(margin, margin, margin, margin);

              for (int i = 0; i < imagePaths.Count; i++)
              {
                if (cancellationTokenSource.Token.IsCancellationRequested)
                  break;

                byte[] imageData = File.ReadAllBytes(imagePaths[i]);
                var image = new iText.Layout.Element.Image(ImageDataFactory.Create(imageData));

                float maxWidth = pageSize.GetWidth() - document.GetLeftMargin() - document.GetRightMargin();
                float maxHeight = pageSize.GetHeight() - document.GetTopMargin() - document.GetBottomMargin();

                image.ScaleToFit(maxWidth, maxHeight);
                image.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                document.Add(image);

                if (i < imagePaths.Count - 1)
                {
                  document.Add(new AreaBreak());
                }

                int progress = (int)((i + 1.0) / imagePaths.Count * 100);
                this.Invoke((MethodInvoker)delegate
                {
                  progressBar.Value = progress;
                });
              }
            }
          }
        }, cancellationTokenSource.Token);

        if (!cancellationTokenSource.Token.IsCancellationRequested && !isPreview)
        {
          MessageBox.Show("PDF created successfully!");
        }
      }
      catch (OperationCanceledException)
      {
        MessageBox.Show("Operation cancelled by user.");
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error creating PDF: {ex.Message}");
      }
      finally
      {
        convertButton.Enabled = true;
        cancelButton.Enabled = false;
        progressBar.Value = 0;
      }
    }


  }
}
