using System.Net.Sockets;
using System.Net;
using System.Text;

namespace temp_control_visual
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        string serverIP = "192.168.0.1";
        int serverPort = 12345;
        UdpClient client;
        IPEndPoint serverEP;

        public MainPage()
        {
            InitializeComponent();
            InitializeUDPClient();
            UpdateTemperature();
        }

        private void InitializeUDPClient()
        {
            client = new UdpClient();
            serverEP = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
        }

        private void IncrementButton_Clicked(object sender, EventArgs e)
        {
            count++;
            ControlDegrees.Text = $"{count}°C"; // Display count with °C
            SendUpdatedValue();
        }

        private void DecrementButton_Clicked(object sender, EventArgs e)
        {
            if (count > 0)
            {
                count--;
                ControlDegrees.Text = $"{count}°C"; // Display count with °C
                SendUpdatedValue();
            }
        }

        private void SendUpdatedValue()
        {
            string message = $"SetTemperature:{count}";
            byte[] request = Encoding.ASCII.GetBytes(message);
            client.Send(request, request.Length, serverEP);
        }

        private void UpdateTemperature()
        {
            string request = "GetTemperature";
            byte[] requestData = Encoding.ASCII.GetBytes(request);
            client.Send(requestData, requestData.Length, serverEP);

            byte[] responseData = client.Receive(ref serverEP);
            string responseMessage = Encoding.ASCII.GetString(responseData);
            int temperature;
            if (int.TryParse(responseMessage, out temperature))
            {
                count = temperature;
                ControlDegrees.Text = $"{count}°C"; // Display count with °C
            }
        }
    }

}