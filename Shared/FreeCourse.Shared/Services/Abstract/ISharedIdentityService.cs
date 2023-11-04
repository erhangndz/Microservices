using System;
using System.Collections.Generic;
using System.Text;

namespace FreeCourse.Shared.Services.Abstract
{
    public interface ISharedIdentityService
    {

        public string GetUserId { get; }
    }
}
