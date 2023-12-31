using System;
namespace maui_template.Models
{
    public interface IResponse
    {
        int int_result { get; set; }
        string str_result { get; set; }
    }

    public class CResponse : IResponse
    {
        public int int_result { get; set; }
        public string str_result { get; set; }
        public string data { get; set; }
    }
}

