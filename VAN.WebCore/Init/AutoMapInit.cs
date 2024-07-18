using AutoMapper;
using VAN.SQLServerCore.SQLServer.Models;
using VAN.WebCore.WebVO;

namespace VAN.WebCore.Init
{
    public class AutoMapInit : Profile
    {
        public AutoMapInit()
        {
            CreateMap<User, UserVO>()
                .ForMember(a => a.UserName, b => b.MapFrom(src => src.Name))
                .ForMember(a => a.Password, b => b.MapFrom(src => src.Password))
                .ReverseMap(); // ReverseMap()方法用于将VO对象映射回实体对象
        }
    }
}
