namespace IMDBClone.Helpers
{
    public static class ServerFile
    {
        public static string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }

        public static bool Upload(IFormFile file, string serverFullPath)
        {
            if (file != null)
            {
                using (var localFile = File.OpenWrite(serverFullPath))
                using (var uploadedFile = file.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                }
                return true;
            }
            return false;

        }

        public static void Delete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static bool CheckFileExtension(IFormFile file, string[] validTypes)
        {
            string FileExtension = GetExtension(file.FileName);
            if (validTypes.Contains(FileExtension))
            {
                return true;
            }
            return false;
        }
    }
}
