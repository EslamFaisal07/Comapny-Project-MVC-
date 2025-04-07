using AutoMapper;
using Route.Mvc.BusinessLL.DataTransferObjects.Employee;
using Route.Mvc.DAL.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.BusinessLL.Profiles
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
               CreateMap<Employee , EmployeeDto>().ForMember(d=>d.Gender , options=>options.MapFrom(e=>e.Gender))
                .ForMember( d=>d.EmployeeType , options=>options.MapFrom(e=>e.EmployeeType))
                .ForMember( d=>d.Department , options=>options.MapFrom(e=>e.Department !=null ? e.Department.Name : null));




                CreateMap<Employee, EmployeeDetailsDTO>().ForMember(d => d.Gender, options => options.MapFrom(e => e.Gender))
                .ForMember(d => d.EmployeeType, options => options.MapFrom(e => e.EmployeeType))
                .ForMember(d=>d.HiringDate , options=>options.MapFrom(e=>DateOnly.FromDateTime(e.HiringDate)))
                .ForMember(d => d.Department, options => options.MapFrom(e => e.Department != null ? e.Department.Name : null));






            CreateMap<CreatedEmployeeDto, Employee>()
                 .ForMember(d => d.HiringDate, options => options.MapFrom(e => e.HiringDate.ToDateTime( TimeOnly.MinValue)));
   




            CreateMap<UpdatedEmployeeDto, Employee>().ForMember(d => d.HiringDate, options => options.MapFrom(e => e.HiringDate.ToDateTime(TimeOnly.MinValue)));




        }



    }
}
