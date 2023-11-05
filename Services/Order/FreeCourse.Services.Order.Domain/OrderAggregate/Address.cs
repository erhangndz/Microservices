using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class Address:ValueObject
    {
        public string Province { get; private set; }
        public string District { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public string AddressLine { get; private set; }

        public Address(string province, string district, string street, string zipCode, string addressLine)
        {
            Province = province;
            District = district;
            Street = street;
            ZipCode = zipCode;
            AddressLine = addressLine;
        }

       

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Province;
            yield return District;
            yield return Street;
            yield return ZipCode;
            yield return AddressLine;
        }
    }
}
