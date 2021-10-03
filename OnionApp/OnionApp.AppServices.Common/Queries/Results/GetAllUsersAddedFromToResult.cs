using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.AppServices.Common.Queries.Results
{
    public sealed class GetAllUsersAddedFromToResult
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
