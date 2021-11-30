using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SOCATemplate.Application.Common.Entities;

namespace SOCATemplate.Infrastructure.Persistence.Configurations
{
    public partial class tbl_User_Configuration
    {
        protected override void SeedData(BaseSeeder<tbl_User> builder)
        {
            builder.HasData(new tbl_User
            {
                ID = 1,
                Name = "Test User"
            });
        }
    }
}
