﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Backend.Models.UserStats;
using ChatApp.Backend.Stores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStatsController : ControllerBase
    {
        public IActionResult GetStatistics(UserStatsRequestModels statsRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            else
            {
                switch (statsRequest.Type)
                {
                    case StatType.User:
                        return Ok(new UserStatsResponseModels(ChatStore.UsersOnline.Count));
                    case StatType.Group:
                        return Ok(new UserStatsResponseModels(ChatStore.UsersByGroups.Count));
                    case StatType.UserInGroup:
                        var group = ChatStore.UsersByGroups.FirstOrDefault(x => x.GroupName == statsRequest.Group);
                        if (group == null)
                            return NotFound();
                        else
                            return Ok(new UserStatsResponseModels(group.Users.Count));
                    default:
                        return BadRequest();
                }
            }
        }
    }
}