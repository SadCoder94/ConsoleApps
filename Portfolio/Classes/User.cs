using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Classes
{
    public class User
    {
        private List<Fund> portFolio;

        public User()
        {
            portFolio = new List<Fund>();
        }

        public void AddFundsToPortFolio(List<Fund> funds)
        {
            foreach(var fund in funds)
            {
                portFolio.Add(fund);
            }
        }

        public List<Fund> GetPortfolio()
        {
            return portFolio;
        }
    }
}
