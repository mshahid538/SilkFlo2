using System;
using Newtonsoft.Json;
using System.Text.Json;

namespace SilkFlo.Email
{
    [Serializable]
    internal class Verification
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("disposable")]
        public string Disposable { get; set; }

        [JsonProperty("accept_all")]
        public string AcceptAll { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("free")]
        public string Free { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("mx_record")]
        public string MXRecord { get; set; }

        [JsonProperty("mx_domain")]
        public string MSDomain { get; set; }

        [JsonProperty("safe_to_send")]
        public string SafeToSend { get; set; }

        [JsonProperty("did_you_mean")]
        public string DidYouMean { get; set; }

        [JsonProperty("success")]
        public string Success { get; set; }

        [JsonProperty("message")]
        public object Message { get; set; }

        //public string Serializing() => Newtonsoft.Json.JsonSerializer.Serialize<Verification>(this, new JsonSerializerOptions()
        //{
        //    WriteIndented = true
        //});

        public string Serializing() => JsonConvert.SerializeObject(this, new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        });

        public override string ToString() => this.Serializing();
    }
}
