using System;

namespace ejercicio2
{
    public class CurrencyConversions
    {
        public string currency_base { get; set; }
        public string currency_quote { get; set; }
        public float ratio { get; set; }
        public float rate { get; set; }
        public float inv_rate { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime valid_until { get; set; }
    }

}
