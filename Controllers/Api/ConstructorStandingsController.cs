﻿using F1Project.Data;
using F1Project.Models;

namespace F1Project.Controllers.Api;

public class ConstructorStandingsController : GenericController<ConstructorStanding>
{
    private readonly WatchF1Context _context;
    
    public ConstructorStandingsController(WatchF1Context context) : base(context)
    {
        _context = context;
    }
}