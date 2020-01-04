using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aurora.Data.Interfaces;
using Aurora.ViewModels;
using Microsoft.AspNetCore.Identity;
using Aurora.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace Aurora.Controllers
{
    public class PersonalController : Controller
    {
       
    
        private UserManager<User> _userManager;
        private readonly IOrderRepository _orderRepository;
        public PersonalController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        //GET : /api/UserProfile
        public async Task<Object> Index() 
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                 user.Email,
                 user.UserName,
                ViewResult = View(_orderRepository.Orders.ToList())
            };
            
        }
        

    }
}