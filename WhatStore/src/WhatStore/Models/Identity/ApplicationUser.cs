﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Models.Identity
{
    public class ApplicationUser : IdentityUser<long>
    {
    }
}
