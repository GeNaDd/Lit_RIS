using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Lab2Lit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // Найти
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                string str = "Поиск\n" + textBox1.Text;
                byte[] data = Encoding.Unicode.GetBytes(str);
                socket.Send(data);
                data = new byte[4096];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (socket.Available > 0);
                dataGridView1.Rows.Clear();
                string[] massStr = builder.ToString().Split('\n');
                for (int i = 0; i < massStr.Length - 1; i++)
                {
                    dataGridView1.Rows.Add();
                    string[] massStr2 = massStr[i].Split('|');
                    dataGridView1[0, i].Value = massStr2[0];
                    dataGridView1[1, i].Value = massStr2[1];
                    dataGridView1[2, i].Value = massStr2[2];
                }
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception) { }

        }

        private void button2_Click(object sender, EventArgs e) // Сохранить
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                string str = "Сохранение\n";
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    str += dataGridView1[0, i].Value.ToString() + "|" + dataGridView1[1, i].Value.ToString() + "|" + dataGridView1[2, i].Value.ToString() + "\n";
                }
                str += "Конец";
                byte[] data = Encoding.Unicode.GetBytes(str);
                socket.Send(data);
                data = new byte[256];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (socket.Available > 0);
                if (builder.ToString() != "Успешно")
                {
                    MessageBox.Show("Ошибка сохранения", "Ошибка");
                }
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception) { }
        }

        private void button3_Click(object sender, EventArgs e) // Загрузить
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                byte[] data = Encoding.Unicode.GetBytes("Загрузка");
                socket.Send(data);
                data = new byte[4096];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (socket.Available > 0);
                dataGridView1.Rows.Clear();
                string[] massStr = builder.ToString().Split('\n');
                for (int i = 0; i < massStr.Length - 1; i++)
                {
                    dataGridView1.Rows.Add();
                    string[] massStr2 = massStr[i].Split('|');
                    dataGridView1[0, i].Value = massStr2[0];
                    dataGridView1[1, i].Value = massStr2[1];
                    dataGridView1[2, i].Value = massStr2[2];
                }
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception) { }
        }
    }
}
