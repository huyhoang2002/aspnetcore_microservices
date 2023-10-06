using Ordering.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Aggregates.Order
{
    public class Order : EntityBase
    {
        public Order(string userName, decimal totalPrice, string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipCode, string cardName, string cardNumber, string expiration, string cVV, int paymentMethod)
        {
            UserName = userName;
            TotalPrice = totalPrice;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            Country = country;
            State = state;
            ZipCode = zipCode;
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cVV;
            PaymentMethod = paymentMethod;
            CreatedBy = userName;
            LastModifiedBy = userName;
        }

        public string UserName { get; private set; }
        public decimal TotalPrice { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public string AddressLine { get; private set; }
        public string Country { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string Expiration { get; private set; }
        public string CVV { get; private set; }
        public int PaymentMethod { get; private set; }
    }
}
