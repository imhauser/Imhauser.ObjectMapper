using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ObjectMapper.Tests
{
    public class AddressDb
    {
        public int Id{ get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public int Qty { get; set; }
    }
    public class ContactDb
    {
        public string ContactName { get; set; }
        public string Phone { get; set; }
    }



    public class View
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public int? Qty { get; set; }
    }



    public class CopyTests
    {
        [Fact]
        public void ToTest()
        {
            var addressDb = new AddressDb()
            {
                Street = "Ellegårdsvej 74",
                ZipCode = "2820",
                City = "Gentofte",
                Qty=10
                
            };
            var contactDb = new ContactDb()
            {
                ContactName = "Eric Imhauser",
                Phone = "1523456789",
            };

            var view = Imhauser.ObjectMapper.Copy.To<AddressDb, View>(addressDb);
            Assert.Equal(addressDb.Street , view.Street);

            view = Imhauser.ObjectMapper.Copy.To<ContactDb, View>(contactDb, view);
            Assert.Equal(contactDb.ContactName, view.ContactName);


            addressDb = null;
            view = Imhauser.ObjectMapper.Copy.To<AddressDb, View>(addressDb);
            Assert.Null(view);

        }

        [Fact]
        public void ToListTest()
        {
            List<AddressDb> list = new List<AddressDb>()
            {
                new AddressDb()
                {
                    Street = "Ellegårdsvej 74",
                    ZipCode = "2820",
                    City = "Gentofte"
                },
                new AddressDb()
                {
                    Street = "CVE Knuthsvej 2D",
                    ZipCode = "2900",
                    City = "Hellerup"
                }
            };

            var targetList = Imhauser.ObjectMapper.Copy.ToList<AddressDb, View>(list).ToList();
            Assert.Equal(list[0].Street, targetList[0].Street);
        }
    }
}
