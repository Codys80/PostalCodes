using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
//using static Google.Android.Material.DatePicker.MaterialDatePicker;
//using static Android.Provider.Telephony.Mms;
//using static Android.Provider.Telephony.Mms;

namespace PostalCodes
{
    public class PCode
    {
        [JsonPropertyName("post code")]
        public string post_code { get; set; }
        public string country { get; set; }
        [JsonPropertyName("country abbreviation")]
        public string country_abbreviation { get; set; }
        public IList<Places> places { get; set; }
    }
    public class Places
    {
        [JsonPropertyName("place name")]
        public string? place_name { get; set; }
        public string? longitude { get; set; }
        public string? state { get; set; }
        [JsonPropertyName("state abbreviation")]
        public string? state_abbreviation { get; set; }
        public string? latitude { get; set; }
    }
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public PCode CreatePostCode(int inputCode)
        {
            string url = $"http://api.zippopotam.us/us/{inputCode}";
            string json;

            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
            }
            PCode p = JsonSerializer.Deserialize<PCode>(json);
            return p;
        }

        private void OnPostClicked(object sender, EventArgs e)
        {
            int postCode = int.Parse(PostEntry.Text);
            string d = "";
            PCode Code = CreatePostCode(postCode);
            d += $"Kod pocztowy: {Code.post_code}\n";
            d += $"Kraj: {Code.country}\n";
            d += $"Kod ISO kraju: {Code.country_abbreviation}\n";
            d += $"Nazwa: {Code.places[0].place_name}\n";
            d += $"Długość geograficzna: {Code.places[0].longitude}\n";
            d += $"Stan: {Code.places[0].state}\n";
            d += $"Skrót stanu: {Code.places[0].state_abbreviation}\n";
            d += $"Szerokość geograficzna: {Code.places[0].latitude}\n";

            PostDisplay.Text = d;
        }

        public PCode CreatePostCodeCity(string state, string city)
        {
            string url = $"http://api.zippopotam.us/us/{state}/{city}";
            string json;

            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
            }
            PCode p = JsonSerializer.Deserialize<PCode>(json);
            return p;
        }

        private void OnPostClickedCity(object sender, EventArgs e)
        {
            string City = PostEntryCity.Text;
            string State = PostEntryCityState.Text;
            string d = "";
            PCode Code = CreatePostCodeCity(State, City);
            d += $"Kod pocztowy: {Code.post_code}\n";
            d += $"Kraj: {Code.country}\n";
            d += $"Kod ISO kraju: {Code.country_abbreviation}\n";
            d += $"Nazwa: {Code.places[0].place_name}\n";
            d += $"Długość geograficzna: {Code.places[0].longitude}\n";
            d += $"Stan: {Code.places[0].state}\n";
            d += $"Skrót stanu: {Code.places[0].state_abbreviation}\n";
            d += $"Szerokość geograficzna: {Code.places[0].latitude}\n";

            PostDisplay.Text = d;
        }
    }
}


