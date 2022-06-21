using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockMarket
{
    public class Investor
    {
        private List<Stock> stocks;

        public Investor(string fullName, string emailAddress, decimal moneyToInvest, string brokerName)
        {
            FullName = fullName;
            EmailAddress = emailAddress;
            MoneyToInvest = moneyToInvest;
            BrokerName = brokerName;

            this.stocks = new List<Stock>();
        }

        public List<Stock> Portfolio { get => this.stocks; set => this.stocks = value; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public decimal MoneyToInvest { get; set; }
        public string BrokerName { get; set; }
        public int Count => this.stocks.Count;

        public void BuyStock(Stock stock)
        {
            if(stock.MarketCapitalization >= 10000 && this.MoneyToInvest >= stock.PricePerShare)
            {
                this.stocks.Add(stock);
                MoneyToInvest -= stock.PricePerShare;
            }
        }

        public string SellStock(string companyName, decimal sellPrice)
        {
            var stockTosell = stocks.FirstOrDefault(s => s.CompanyName == companyName);

            if (stockTosell == null)
                return $"{companyName} does not exist.";

            if (sellPrice < stockTosell.PricePerShare)
                return $"Cannot sell {companyName}.";

            stocks.Remove(stockTosell);
            MoneyToInvest += sellPrice;

            return $"{companyName} was sold.";
        }

        public Stock FindStock(string companyName)
        {
            return this.stocks.FirstOrDefault(s => s.CompanyName == companyName);
        }

        public Stock FindBiggestCompany()
        {
            if(this.stocks.Count == 0) 
                return null;

            return this.stocks.OrderByDescending(s => s.MarketCapitalization).FirstOrDefault();
        }

        public string InvestorInformation()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"The investor {FullName} with a broker {BrokerName} has stocks:");
            sb.Append(string.Join(Environment.NewLine, this.stocks));

            return sb.ToString().TrimEnd();
        }
    }
}
