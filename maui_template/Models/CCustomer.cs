using System;
namespace maui_template.Models
{
    public class CCard
    {
        public string CARD_NAME { get; set; }
        public string CARD_NUMBER { get; set; }
        public double CARD_BALANCE { get; set; }
        public string CARD_TYPE { get; set; }
    }

    public class CTransaction
    {
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public double AMT { get; set; }
    }
}

