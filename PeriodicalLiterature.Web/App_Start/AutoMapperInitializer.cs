using AutoMapper;
using PeriodicalLiterature.Web.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeriodicalLiterature.Web.App_Start
{
    public class AutoMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MapperProfile>();              
            });
        }
    }
}