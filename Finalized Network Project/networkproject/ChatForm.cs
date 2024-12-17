using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class ChatForm : Form
    {
        private TcpClient client;
        private TcpListener listener;
        private NetworkStream stream;

        private RSACryptoServiceProvider rsa;
        private string rsaPublicKey;
        private string rsaPrivateKey;
        private string receivedRsaPublicKey;

        private Aes aes;
        private byte[] aesKey;

        private bool CTD;

        public ChatForm()
        {
            InitializeComponent();

            // Generate RSA keys
            rsa = new RSACryptoServiceProvider();
            rsaPublicKey = rsa.ToXmlString(false); // Public key
            rsaPrivateKey = rsa.ToXmlString(true); // Private key
            CTD = false;
            // Prepare AES for encryption
            aes = Aes.Create();

            this.FormClosed += ChatForm_FormClosed;
        }

        private void ChatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private async void btnStartServer_Click_1(object sender, EventArgs e)
        {
            listener = new TcpListener(IPAddress.Any, 9000);
            listener.Start();
            UpdateChat("Server started. Waiting for connection...");
            txtMessage.Enabled = true;
            btnSend.Enabled = true;
            btnConnectClient.Enabled = true;
            btnSend.BackColor = Color.White;
            btnConnectClient.BackColor = Color.White;
            btnStartServer.Enabled = false;
            btnStartServer.BackColor = Color.Gray;
            TcpClient client = await listener.AcceptTcpClientAsync();
            stream = client.GetStream();
            UpdateChat("Client connected.");

            // Send RSA public key to client
            SendMessage(rsaPublicKey);

            // Receive client's RSA public key
            receivedRsaPublicKey = await ReceiveMessageAsync();
            UpdateChat("Received client's RSA public key.");

            // Encrypt and send AES key using client's RSA public key
            RSACryptoServiceProvider clientRsa = new RSACryptoServiceProvider();
            clientRsa.FromXmlString(receivedRsaPublicKey);

            byte[] encryptedAesKey = clientRsa.Encrypt(aes.Key, false);
            stream.Write(encryptedAesKey, 0, encryptedAesKey.Length);
            UpdateChat("Sent encrypted AES key to client.");

            await ReceiveMessagesAsync();           
        }

        private async void btnConnectClient_Click_1(object sender, EventArgs e)
        {
            CTD = true;
            try
            {
                client = new TcpClient(txtServerIP.Text, 9000);
                stream = client.GetStream();
                btnConnectClient.Enabled = false;
                btnConnectClient.BackColor = Color.Gray;
                UpdateChat("Connected to server.");

                // Send RSA public key to server
                SendMessage(rsaPublicKey);

                // Receive server's RSA public key
                receivedRsaPublicKey = await ReceiveMessageAsync();
                UpdateChat("Received server's RSA public key.");

                // Receive encrypted AES key
                int rsaKeySizeInBytes = rsa.KeySize / 8;
                byte[] encryptedAesKey = new byte[rsaKeySizeInBytes];
                int bytesRead = 0;
                while (bytesRead < encryptedAesKey.Length)
                {
                    bytesRead += await stream.ReadAsync(encryptedAesKey, bytesRead, encryptedAesKey.Length - bytesRead);
                }
                aesKey = rsa.Decrypt(encryptedAesKey, false);
                aes.Key = aesKey;
                UpdateChat("Decrypted AES key.");

                await ReceiveMessagesAsync();
            }
            catch (Exception ex)
            {
                UpdateChat($"Error: {ex.Message}");
                CTD = false;
                btnConnectClient.Enabled = true;
                btnConnectClient.BackColor = Color.White;
            }
        }

        private async Task ReceiveMessagesAsync()
        {
            try
            {
                while (true)
                {
                    var (iv, encryptedMessage) = await ReceiveIvAndEncryptedMessageAsync();

                    aes.IV = iv;
                    string decryptedMessage = DecryptMessage(encryptedMessage);

                    if (!string.IsNullOrEmpty(decryptedMessage))
                    {
                        UpdateChat($"[Client]: {decryptedMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateChat($"Error receiving message: {ex.Message}");
            }
        }

        private async Task<(byte[], byte[])> ReceiveIvAndEncryptedMessageAsync()
        {
            try
            {
                // Read the IV (16 bytes for AES)
                byte[] iv = new byte[16];
                int ivBytesRead = 0;
                while (ivBytesRead < iv.Length)
                {
                    ivBytesRead += await stream.ReadAsync(iv, ivBytesRead, iv.Length - ivBytesRead);
                }

                // Read the encrypted message
                List<byte> bufferList = new List<byte>();
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    bufferList.AddRange(buffer.Take(bytesRead));
                    if (bytesRead < buffer.Length)
                    {
                        break;
                    }
                }

                return (iv, bufferList.ToArray());
            }
            catch (Exception ex)
            {
                UpdateChat($"Error receiving IV and encrypted message: {ex.Message}");
                return (null, null);
            }
        }

        private string DecryptMessage(byte[] encryptedMessage)
        {
            try
            {
                ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedMessage, 0, encryptedMessage.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
            catch (CryptographicException ex)
            {
                UpdateChat($"Decryption error: {ex.Message}");
                return null;
            }
        }

        private async void btnSend_Click_1(object sender, EventArgs e)
        {
            if (CTD == true)
            {
                string plainText = txtMessage.Text;

                aes.GenerateIV();
                byte[] iv = aes.IV;

                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                ICryptoTransform encryptor = aes.CreateEncryptor();
                byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                await stream.WriteAsync(iv, 0, iv.Length);
                await stream.WriteAsync(cipherBytes, 0, cipherBytes.Length);

                UpdateChat($"[You]: {plainText}");
                txtMessage.Clear();
            }           
        }
        private async Task<string> ReceiveMessageAsync()
        {
            List<byte> bufferList = new List<byte>();
            byte[] buffer = new byte[1024];
            int bytesRead;
            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                bufferList.AddRange(buffer.Take(bytesRead));
                if (bytesRead < buffer.Length)
                {
                    break;
                }
            }
            return Encoding.UTF8.GetString(bufferList.ToArray());
        }

        private void SendMessage(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            stream.Write(messageBytes, 0, messageBytes.Length);
        }

        private void UpdateChat(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateChat), message);
                return;
            }
            txtChat.Items.Add(message + Environment.NewLine);
            if (txtChat.Items.Count > 24)
            {
                txtChat.Items.RemoveAt(0);
            }
        }
    }
}