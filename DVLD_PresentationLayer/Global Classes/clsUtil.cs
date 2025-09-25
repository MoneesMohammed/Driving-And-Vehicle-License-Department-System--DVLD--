using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Classes
{
    public class clsUtil
    {

        public static string GenerateGUID()
        {
            Guid guid = Guid.NewGuid();

            return guid.ToString();

        }

        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    // If it doesn't exist, create the folder
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }

            return true;
        }

        public static string ReplaceFileNameWithGUID(string sourceFile)
        {

            string extension = Path.GetExtension(sourceFile);

            return GenerateGUID() + extension;

        }


        public static bool CopyImageToProjectImagesFolder(ref string sourceFile)
        {

            string DestinationFolder = @"C:\DVLD-People-Images\";

            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            string destinationFile = DestinationFolder + ReplaceFileNameWithGUID(sourceFile);

            try
            {
                File.Copy(sourceFile, destinationFile, true);

            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            sourceFile = destinationFile;
            return true;


        }



        /*private string _GetPathFileCopied(string DestinationFolder, string SourcePath)
        {

            if (!Directory.Exists(DestinationFolder))
            {
                Directory.CreateDirectory(DestinationFolder);
            }

            Guid guid = Guid.NewGuid();

            string extension = Path.GetExtension(SourcePath);

            string FileName = guid.ToString() + extension;

            string DestinationPath = Path.Combine(DestinationFolder, FileName);

            try
            {
                File.Copy(SourcePath, DestinationPath, true);
                MessageBox.Show("Image copied successfully ✅");
                return DestinationPath;
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("You do not have permission to access this path. Try running the program as administrator or choose a different folder..");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while copying : " + ex.Message, "error");
            }



            return "";
        }*/



    }
}
