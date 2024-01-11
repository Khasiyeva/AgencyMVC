using System.Linq;

namespace Agency.MVC.Helpers
{
    public static class FileManager
    {
        public static string Upload(this IFormFile file,string envPath,string folderName)
        {
            string fileName = file.FileName;
            if (fileName.Length>64)
            {
                fileName= fileName.Substring(fileName.Length-64);
            }
            fileName=Guid.NewGuid().ToString()+file.FileName;

            string path = envPath + fileName + folderName;

            using(FileStream stream=new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }

        public static bool CheckLength(this IFormFile file,int length) 
        {
          return  file.Length <= length;
        }

        public static bool CheckContent(this IFormFile file, string content)
        {
            return file.ContentType.Contains(content);
        }

        public static void DeleteFile(string envPath,string folderName, string imgUrl)
        {
            string path = envPath + folderName + imgUrl;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
