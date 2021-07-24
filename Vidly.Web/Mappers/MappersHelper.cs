using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Web.Models;

namespace Vidly.Web.Mappers
{
    public static class MappersHelper
    {
        public static void MapNewCustomer(Customer oldCustomer, Customer newCustomer)
        {
            oldCustomer.Name = newCustomer.Name;
            oldCustomer.BirthDateTime = newCustomer.BirthDateTime;
            oldCustomer.IsSubscribedToNewsletter = newCustomer.IsSubscribedToNewsletter;
            oldCustomer.MembershipTypeId = newCustomer.MembershipTypeId;

            // return updated customer
            //return oldCustomer;
        }
    }
}