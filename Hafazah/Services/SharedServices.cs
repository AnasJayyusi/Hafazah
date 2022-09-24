using Hafazah.DAL;
using Hafazah.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hafazah.Services
{
    public class SharedServices
    {
        public HafazahDbContext _db;
        public SharedServices()
        {
            _db = new HafazahDbContext();
        }
        public void AddNewMember(Member member)
        {
            _db.Members.Add(member);
            _db.SaveChanges();
        }
    }
}