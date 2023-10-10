﻿using F1Project.Data;
using Microsoft.AspNetCore.Mvc;

namespace F1Project.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly WatchF1Context _context;
    
    public UsersController(WatchF1Context context)
    {
        _context = context;
    }
}