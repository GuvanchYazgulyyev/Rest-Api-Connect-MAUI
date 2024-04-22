namespace Rest_Api_Connect_MAUI
{
    public partial class MainPage : ContentPage
    {
        private readonly IHttpClientFactory _httpClientBuilder;
        public MainPage(IHttpClientFactory httpClientFactory)
        {
            _httpClientBuilder = httpClientFactory;
            InitializeComponent();
        }



        private async void OnCallApiBtnClicke(object sender, EventArgs e)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            var baseUrl = DeviceInfo.Platform == DevicePlatform.Android
                ? "https://10.0.2.2:7063"
                : "https://localhost:7063";
            var httpClient = new HttpClient(handler);
            var response = await httpClient.GetAsync($"{baseUrl}/WeatherForecast");
            var dataRead = await response.Content.ReadAsStringAsync();
            apiResponseData.Text = dataRead;
        }

        private async void OnGetDataFromApiCliced(object sender, EventArgs e)
        {
            var httpClient = _httpClientBuilder.CreateClient("maui-to-https-localhost");
            var response = await httpClient.GetAsync("/WeatherForecast");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                label.Text = content;
            }
        }
    }

}
