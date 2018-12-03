using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateName.WebApi.Exceptions
{
    static class ExceptionMapperConfig
    {
        public static void ConfigExceptionMapper(IMapperConfigurationExpression exp)
        {
            exp.CreateMap<WebApiException, ExceptionModel>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.ExceptionType, opt => opt.MapFrom(src => src.InnerException.GetType().FullName))
                .ForMember(dest => dest.ExceptionDescription, opt => opt.MapFrom(src => src.InnerException.Message));
        }
    }
}
