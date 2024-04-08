using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuzyceApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace LuzyceApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        
    }
}