﻿using System;
using MvvmCross.ViewModels;
using MvvmCross;
using Loyalty.API.Services;
using Loyalty.API.Services.Mocks;

namespace Loyalty.API
{
    public class App : MvxApplication
    {
        const bool USE_MOCKS = true;
        
        public override void Initialize()
        {
            if (USE_MOCKS)
            {
                Mvx.RegisterType<IAuthService>(() => new MockAuthService());
            }
            else
            {
                
            }
        }
    }
}