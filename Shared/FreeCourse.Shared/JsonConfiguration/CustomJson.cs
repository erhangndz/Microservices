using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace FreeCourse.Shared.JsonConfiguration
{
    public static class CustomJson
    {
        public static JsonSerializerOptions Option
        {
            get
            {
                return new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
            }
        }
    }
}
