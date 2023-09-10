﻿using Microsoft.AspNetCore.Mvc;
using MVCSimpleCRM.Data;
using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context) 
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Users> users = _context.users.ToList();
            return View(users);
        }
    }
}
