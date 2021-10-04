using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.AppServices.Common.ApiServices.Results
{
    public sealed class GetAllUsersResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
