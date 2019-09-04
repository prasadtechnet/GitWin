using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.PdfCore.Business.FileModule
{
   public class FileModule
    {
        public void WriteFile(string folderPath,string fileName,string Content)
        {
            try
            {
                if (!Directory.Exists(folderPath))                
                    Directory.CreateDirectory(folderPath);

                File.WriteAllText(folderPath+"\\"+fileName, Content);

            }
            catch (Exception ex)
            {
            }
        }
        public string ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            return File.ReadAllText(filePath);
        }
    }
}
