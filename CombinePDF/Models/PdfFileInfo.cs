namespace CombinePDF.Models
{
  public class PdfFileInfo
  {
    public string FilePath { get; set; }
    public DateTime SelectedTime { get; set; }

    public PdfFileInfo(string filePath)
    {
      FilePath = filePath;
      SelectedTime = DateTime.Now;
    }

    public override string ToString()
    {
      return Path.GetFileName(FilePath);
    }

  }
}
