﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.BusinessLL.DataTransferObjects.Department
{
    public class DepartmentDto
    {

        public int DeptId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }

        public DateOnly DateOfCreation { get; set; }




    }
}
