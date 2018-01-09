using System;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Text;

namespace Eam.Core.Zip
{
    public class CompressFile
    {
        /// <summary>
        /// 压缩文件参数
        /// </summary>
        public ZipParameter ZipParameter { get; set; }

        /// <summary>
        /// 压缩文件返回压缩后的信息
        /// </summary>
        /// <returns>string 返回压缩后的提示信息</returns>
        public string CompressReturnMsg()
        {
            FileStream Zip_File;
            ZipOutputStream ZipStream;
            ZipEntry ZipEntry;
            string rtnMessage = "";//返回的信息

            try
            {
                //循环文件，如果文件不存在就不添加的压缩里面
                for (int i = 0; i < ZipParameter.ZIPFileList.Count; i++)
                {
                    if (!File.Exists(ZipParameter.ZIPFileList[i]))
                    {
                        ZipParameter.ZIPFileList.RemoveAt(i);
                        i--;
                    }

                }
                //没有有文件下面的压缩不执行
                if (ZipParameter.ZIPFileList.Count == 0)
                {
                    return " file not find";
                }
                //没有目录进行创建
                if (!Directory.Exists(ZipParameter.ZIPDirectoryName))
                {
                    Directory.CreateDirectory(ZipParameter.ZIPDirectoryName);
                }

                // 解决文档名称乱码问题,出现乱码就是因为CodePage不对
                Encoding gbk = Encoding.GetEncoding("gbk");
                ICSharpCode.SharpZipLib.Zip.ZipConstants.DefaultCodePage = gbk.CodePage;

                //文件路径，文档路径与文件名称
                string strPath = ZipParameter.ZIPDirectoryName + ZipParameter.ZIPName;

                Zip_File = File.Create(strPath);
                ZipStream = new ZipOutputStream(Zip_File);
                foreach (string FileToZip in ZipParameter.ZIPFileList)
                {
                    Zip_File = File.OpenRead(FileToZip);
                    byte[] buffer = new byte[Zip_File.Length];
                    Zip_File.Read(buffer, 0, buffer.Length);
                    Zip_File.Close();
                    ZipEntry = new ZipEntry(Path.GetFileName(FileToZip));
                    ZipStream.PutNextEntry(ZipEntry);
                    ZipStream.Write(buffer, 0, buffer.Length);
                }
                ZipStream.Finish();
                ZipStream.Close();
                Zip_File.Close();
                rtnMessage = "success";
            }
            catch (Exception ex)
            {
                rtnMessage = "fail:" + ex.Message;
            }
            finally
            {
                GC.Collect();
                GC.Collect(1);
            }
            return rtnMessage;
        }
    }
}
