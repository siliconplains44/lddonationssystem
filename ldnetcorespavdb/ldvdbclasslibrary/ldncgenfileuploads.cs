using System;

namespace ldvdbclasslibrary
{
    public class fileuploads
    {
        public long FileUploadID { get; set; } 
        public string Filename { get; set; } 
        public long Size { get; set; } 
        public DateTime Created { get; set; } 
        public byte[] Data { get; set; } 
    }
}
