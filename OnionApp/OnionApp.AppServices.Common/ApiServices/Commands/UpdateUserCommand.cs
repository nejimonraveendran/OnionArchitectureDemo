using System;
using System.Collections.Generic;
using System.Text;

namespace OnionApp.AppServices.Common.ApiServices.Commands
{
    public sealed class UpdateUserCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
