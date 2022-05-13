using System.Collections.Generic;

namespace Portfolio.Classes
{
    public class Fund
    {
        public string name { get; set; }
        public List<string> stocks { get; set; }
    }
    public class Funds
    {
        public List<Fund> funds { get; set; }
    }
}