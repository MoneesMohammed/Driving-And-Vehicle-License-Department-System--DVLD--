using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Classes
{
    internal static class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string Username,string Password)
        {
            try
            {
                using (FileStream fs = File.Create("RememberMe.txt"))
                {
                    string data = Username + "/" + Password;
                    byte[] info = new UTF8Encoding(true).GetBytes(data);
                    fs.Write(info, 0, info.Length);
                }

                return true;
            }
            catch 
            {
              return false;
            }

        }

        public static bool GetStoredCredential(ref string Username,ref string Password)
        {

            try
            {
                using (StreamReader sr = File.OpenText("RememberMe.txt"))
                {
                    string line = sr.ReadLine();

                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] str = line.Split('/');

                        if (str.Length == 2)
                        {
                            Username = str[0].Trim();
                            Password = str[1].Trim();
                        }
                        else
                        {
                            return false;
                        }

                    }
                    else
                    {
                        return false;
                    }


                }

                return true;
            }
            catch
            {
                return false;
            }


        }

    }
}
