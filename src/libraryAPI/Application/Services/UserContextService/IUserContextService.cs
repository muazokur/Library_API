﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserContextService;
public interface IUserContextService
{
    string GetUserId();
    string GetUserEmail();
}
