using System;
using System.Drawing;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Tekla.Structures.Dialog;

namespace Notification
{
    public partial class fNotification : PluginFormBase
    {
        private ClientWebSocket _ws;

        public fNotification()
        {
            InitializeComponent();
            SetOnline();  // trạng thái mặc định

            // chạy WebSocket client nền
            _ = StartWebSocketListener();
        }

        // 🟢 Server có điện
        public void SetOnline()
        {
            this.BackColor = Color.LightGreen;
            lblStatus.Text = "Server đang online";
            lblMinutes.Text = "";
        }

        // 🟡 Server chạy UPS
        public void SetBatteryMode(int minutes)
        {
            this.BackColor = Color.Khaki;
            lblStatus.Text = "⚠ Server đang chạy bằng UPS";
            lblMinutes.Text = $"Thời gian còn lại: {minutes} phút";
        }

        private void fNotification_Load(object sender, EventArgs e)
        {

        }

        // ===========================================================
        // 📌 BUSINESS LOGIC: WebSocket tự động kết nối & cập nhật UI
        // ===========================================================

        private async Task StartWebSocketListener()
        {
            _ws = new ClientWebSocket();

            try
            {
                await _ws.ConnectAsync(new Uri("ws://192.168.50.2:9455/ws"), CancellationToken.None);
            }
            catch (Exception ex)
            {
                this.Invoke(() => {
                    lblStatus.Text = "Không thể kết nối WebSocket";
                    lblMinutes.Text = ex.Message;
                    this.BackColor = Color.LightGray;
                });
                return;
            }

            var buffer = new byte[2048];

            while (_ws.State == WebSocketState.Open)
            {
                var segment = new ArraySegment<byte>(buffer);
                var result = await _ws.ReceiveAsync(segment, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                    break;

                var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

                UpsMessage msg = null;

                try
                {
                    msg = JsonSerializer.Deserialize<UpsMessage>(json);
                }
                catch { }

                if (msg != null)
                {
                    HandleMessage(msg.text);
                }
            }
        }

        // ===========================================================
        // 📌 Xử lý message UPS
        // ===========================================================

        private void HandleMessage(string text)
        {
            // BATTERY-MODE The UPS device connected... Estimated battery time: 23 minutes.
            if (text.StartsWith("BATTERY-MODE"))
            {
                int minutes = ExtractMinutes(text);

                this.Invoke(() => SetBatteryMode(minutes));
            }
            else if (text.StartsWith("AC-MODE"))
            {
                this.Invoke(() => SetOnline());
            }
            else
            {
                this.Invoke(() =>
                {
                    lblStatus.Text = "Message không xác định";
                    lblMinutes.Text = text;
                });
            }
        }

        private int ExtractMinutes(string text)
        {
            var parts = text.Split(' ');
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == "minutes." && i > 0 && int.TryParse(parts[i - 1], out int mins))
                    return mins;
            }
            return -1;
        }

        // JSON object
        private class UpsMessage
        {
            public string text { get; set; } = "";
        }
    }

    // tiện mở rộng
    public static class ControlExtensions
    {
        public static void Invoke(this System.Windows.Forms.Control c, Action a)
        {
            if (c.InvokeRequired)
                c.Invoke(a);
            else
                a();
        }
    }
}
