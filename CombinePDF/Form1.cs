using CombinePDF.Models;
using iText.Forms.Form.Element;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using System.Diagnostics;
using System.Windows.Forms;


namespace CombinePDF
{
  public partial class Form1 : Form
  {
    private int totalFiles;
    private int currentFile;
    private CancellationTokenSource cancellationTokenSource;
    private Stopwatch stopwatch;
    private readonly Queue<TimeSpan> processingTimes = new Queue<TimeSpan>();
    private const int AVERAGE_SAMPLE_SIZE = 3;

    public Form1()
    {
      InitializeComponent();
      InitializeControls();
      listBoxFiles.HorizontalScrollbar = true;
      listBoxFiles.ScrollAlwaysVisible = true;

    }

    private void InitializeControls()
    {
      openFileDialog1.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
      openFileDialog1.Multiselect = true;

      saveFileDialog1.Filter = "PDF files (*.pdf)|*.pdf";
      saveFileDialog1.DefaultExt = "pdf";
      saveFileDialog1.FileName = "merged.pdf";

      // Thêm event handlers
      listBoxFiles.SelectedIndexChanged += (s, e) => UpdateButtonStates();

      progressBar1.Minimum = 0;
      progressBar1.Maximum = 100;
      progressBar1.Value = 0;
      progressBar1.Visible = false;

      btnCancel.Visible = false;
      btnCancel.Click += btnCancel_Click;

      lblCurrentFile.Text = "";
      lblTimeRemaining.Text = "";

      // Khởi tạo trạng thái các nút
      UpdateButtonStates();

    }

    private void btnAddFiles_Click(object sender, EventArgs e)
    {
      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        foreach (string file in openFileDialog1.FileNames)
        {
          // Kiểm tra xem file đã tồn tại trong list chưa
          if (!listBoxFiles.Items.Cast<PdfFileInfo>()
              .Any(x => x.FilePath.Equals(file, StringComparison.OrdinalIgnoreCase)))
          {
            listBoxFiles.Items.Add(new PdfFileInfo(file));
          }
        }

        // Sắp xếp lại items theo thời gian chọn
        SortListBoxItems();
        UpdateButtonStates();
      }
    }

    private void SortListBoxItems()
    {
      var items = listBoxFiles.Items.Cast<PdfFileInfo>()
          .OrderBy(x => x.SelectedTime)
          .ToList();

      listBoxFiles.Items.Clear();
      foreach (var item in items)
      {
        listBoxFiles.Items.Add(item);
      }
    }


    private void MoveSelectedItem(int direction)
    {
      if (listBoxFiles.SelectedItem == null) return;

      int currentIndex = listBoxFiles.SelectedIndex;
      int newIndex = currentIndex + direction;

      if (newIndex >= 0 && newIndex < listBoxFiles.Items.Count)
      {
        var item = listBoxFiles.SelectedItem;
        listBoxFiles.Items.RemoveAt(currentIndex);
        listBoxFiles.Items.Insert(newIndex, item);
        listBoxFiles.SelectedIndex = newIndex;

        // Cập nhật lại thời gian cho tất cả items để duy trì thứ tự
        UpdateItemTimes();
      }

      UpdateButtonStates();
    }

    private void UpdateItemTimes()
    {
      var timeStep = TimeSpan.FromMilliseconds(100);
      var baseTime = DateTime.Now;

      for (int i = 0; i < listBoxFiles.Items.Count; i++)
      {
        var item = (PdfFileInfo)listBoxFiles.Items[i];
        item.SelectedTime = baseTime + (timeStep * i);
      }
    }



    private void btnMoveUp_Click(object sender, EventArgs e)
    {
      MoveSelectedItem(-1);

    }

    private void btnMoveDown_Click(object sender, EventArgs e)
    {
      MoveSelectedItem(1);
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
      if (listBoxFiles.SelectedItem != null)
      {
        int selectedIndex = listBoxFiles.SelectedIndex;
        listBoxFiles.Items.RemoveAt(selectedIndex);

        // Chọn item tiếp theo nếu có
        if (listBoxFiles.Items.Count > 0)
        {
          listBoxFiles.SelectedIndex = Math.Min(selectedIndex, listBoxFiles.Items.Count - 1);
        }

        UpdateButtonStates();
      }
    }

    private void UpdateButtonStates()
    {
      bool hasItems = listBoxFiles.Items.Count > 0;
      bool hasSelection = listBoxFiles.SelectedIndex != -1;
      int selectedIndex = listBoxFiles.SelectedIndex;

      btnMoveUp.Enabled = hasSelection && selectedIndex > 0;
      btnMoveDown.Enabled = hasSelection && selectedIndex < listBoxFiles.Items.Count - 1;
      btnRemove.Enabled = hasSelection;
      btnMerge.Enabled = listBoxFiles.Items.Count >= 2;
    }


    private async void btnMerge_Click(object sender, EventArgs e)
    {
      if (listBoxFiles.Items.Count < 2)
      {
        MessageBox.Show("Please add at least 2 PDF files to merge.", "Warning",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

      try
      {
        SetControlsEnabled(false);
        progressBar1.Visible = true;
        btnCancel.Visible = true;
        btnCancel.Enabled = true;
        progressBar1.Value = 0;

        totalFiles = listBoxFiles.Items.Count;
        currentFile = 0;
        processingTimes.Clear();
        stopwatch = Stopwatch.StartNew();

        cancellationTokenSource = new CancellationTokenSource();

        await Task.Run(() => MergePDFs(saveFileDialog1.FileName, cancellationTokenSource.Token),
            cancellationTokenSource.Token);

        MessageBox.Show("Merge Thành công!", "Success",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (OperationCanceledException)
      {
        MessageBox.Show("Đã huỷ.", "Cancelled",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      finally
      {
        stopwatch.Stop();
        SetControlsEnabled(true);
        progressBar1.Visible = false;
        btnCancel.Visible = false;
        lblCurrentFile.Text = "";
        lblTimeRemaining.Text = "";
        cancellationTokenSource?.Dispose();
      }
    }

    private void SetControlsEnabled(bool enabled)
    {
      btnAddFiles.Enabled = enabled;
      btnMoveUp.Enabled = enabled;
      btnMoveDown.Enabled = enabled;
      btnRemove.Enabled = enabled;
      btnMerge.Enabled = enabled;
      listBoxFiles.Enabled = enabled;
    }

    private void UpdateProgress(int value, string currentFileName)
    {
      if (InvokeRequired)
      {
        Invoke(new Action(() => UpdateProgressUI(value, currentFileName)));
      }
      else
      {
        UpdateProgressUI(value, currentFileName);
      }
    }

    private void UpdateProgressUI(int value, string currentFileName)
    {
      progressBar1.Value = value;
      lblCurrentFile.Text = $"Đang xử lý: {Path.GetFileName(currentFileName)}";

      if (currentFile > 0)
      {
        var currentTime = stopwatch.Elapsed;
        var timePerFile = currentTime.TotalSeconds / currentFile;
        var remainingFiles = totalFiles - currentFile;
        var estimatedRemainingSeconds = timePerFile * remainingFiles;

        var remainingTime = TimeSpan.FromSeconds(estimatedRemainingSeconds);
        lblTimeRemaining.Text = $"Thời gian còn lại: {FormatTimeSpan(remainingTime)}";
      }
    }

    private void MergePDFs(string outputPath, CancellationToken cancellationToken)
    {
      try
      {
        using var writer = new PdfWriter(outputPath);
        using var mergedDoc = new PdfDocument(writer);
        var merger = new PdfMerger(mergedDoc);

        currentFile = 0;
        foreach (PdfFileInfo fileInfo in listBoxFiles.Items)
        {
          cancellationToken.ThrowIfCancellationRequested();

          var fileStartTime = stopwatch.Elapsed;

          try
          {
            var readerProperties = new ReaderProperties();
            using var reader = new PdfDocument(new PdfReader(fileInfo.FilePath, readerProperties));
            merger.Merge(reader, 1, reader.GetNumberOfPages());

            currentFile++;
            int progressValue = (int)((float)currentFile / totalFiles * 100);

            var fileProcessingTime = stopwatch.Elapsed - fileStartTime;
            processingTimes.Enqueue(fileProcessingTime);
            if (processingTimes.Count > AVERAGE_SAMPLE_SIZE)
              processingTimes.Dequeue();

            UpdateProgress(progressValue, fileInfo.FilePath);
          }
          catch (Exception ex)
          {
            throw new Exception($"Error processing file {fileInfo.FilePath}: {ex.Message}");
          }
        }

        mergedDoc.Close();
      }
      catch (Exception ex)
      {
        throw new Exception($"Error merging PDFs: {ex.Message}");
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      cancellationTokenSource?.Cancel();
      btnCancel.Enabled = false;
    }

    private string FormatTimeSpan(TimeSpan timeSpan)
    {
      if (timeSpan.TotalHours >= 1)
        return $"{(int)timeSpan.TotalHours}h {timeSpan.Minutes}m";
      if (timeSpan.TotalMinutes >= 1)
        return $"{(int)timeSpan.TotalMinutes}m {timeSpan.Seconds}s";
      return $"{(int)timeSpan.TotalSeconds}s";
    }

  }
}
