using System;
using System.IO;
using System.Net;

namespace Examples.System.Net
{
    public class WebRequestGetExample
    {
        public static object Variables { get; private set; }

        public static void Main()
        {
            
            var ftphost = "ftp.servimed.com.br";                                         // Endereço do FTP
            var ftpfilepath = "/retcli/dados2";                                         // Diretório do arquivo dentro do FTP
            var ftpfullpath = "ftp://" + ftphost + ftpfilepath;                          // FTP + Diretório 
            var ftpUser = "496154";                                                        // Usuário do FTP
            var ftpPassword = "496154";                                                    // Senha do FTP

            var FileName = "/servimed_213.zip";                                              // Nome do arquivo que quero baixar
            var PathFullServidor = ftpfullpath + FileName;                                  // FTP + Diretório + nome do arquivo
            //Console.WriteLine("digite o caminho");
            //var caminho = Console.ReadLine();
            var PathDonwload = "C:/DownFTP";                                                     // Diretório no servidor onde será salvo o arquivo
            var outputfilepath = PathDonwload + FileName;                                   // Caminho onde o arquivo será baixado + nome do arquivo
            //var PathViwer = Convert.ToString(Variables["vLinkVisualizacao"].Value);     // URL para baixar o arquivo pelo LATROMI
            //var LinkViwer = PathViwer + "/" + FileName;                                 // Link utilizado para Visualizar o arquivo

            Directory.CreateDirectory(Path.GetDirectoryName(outputfilepath));           // Cria a pasta "FTP_Downloads"

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(PathFullServidor));
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                //request.UseBinary = true;
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    using (Stream rs = response.GetResponseStream())
                    {
                        using (FileStream ws = new FileStream(outputfilepath, FileMode.Create))
                        {
                            byte[] buffer = new byte[2048];
                            int bytesRead = rs.Read(buffer, 0, buffer.Length);
                            while (bytesRead > 0)
                            {
                                ws.Write(buffer, 0, bytesRead);
                                bytesRead = rs.Read(buffer, 0, buffer.Length);
                            }
                        }
                    }
                }
            }
            catch {}
            

            //Variables["outputUrl"].Value = LinkViwer;                                   // Linl completo para visualizar o arquivo
        }
       
    }
}