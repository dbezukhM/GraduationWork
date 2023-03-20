namespace BlobDataProcessor.Models
{
    public class BlobFilePostRequest
    {
        public string FileName { get; set; }

        public byte[] Contents { get; set; }
    }
}