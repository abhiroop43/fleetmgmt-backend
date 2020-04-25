using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using FleetMgmt.Dto;
using FleetMgmt.Repository.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FleetMgmt.Repository.Implementations
{
    public class UserSession : IUserSession
    {
        private readonly HttpContext _context;

        public UserSession(IHttpContextAccessor context)
        {
            _context = context.HttpContext;
        }

        public User GetUser()
        {
            var identity = _context?.User.Identity as ClaimsIdentity;
            User user = null;
            if (identity?.Claims != null && identity.Claims.Any() && !string.IsNullOrEmpty(identity.Name))
            {
                user = new User
                {
                    Name = identity.Name
                };
            }

            return user;
        }
    }
}
