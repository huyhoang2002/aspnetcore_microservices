﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Models.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; private set; }
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
