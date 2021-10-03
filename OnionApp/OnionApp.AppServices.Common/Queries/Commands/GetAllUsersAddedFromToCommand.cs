using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.AppServices.Common.Queries.Commands
{
    public sealed class GetAllUsersAddedFromToCommand
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
