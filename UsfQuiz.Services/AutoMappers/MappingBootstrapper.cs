﻿namespace UsfQuiz.Services.AutoMappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using System.IO;
    using System.Reflection;
    using System.Text;

    public class MappingBootstrapper
    {
        public static MapperConfiguration Configuration { get; private set; }

        public static void Init()
        {
            try
            {
                var types = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(s => s.GetTypes().Where(t => t.GetInterfaces().Any(IsMapperConfigInterface)))
               .ToArray();

                Execute(types);
            }
            catch (ReflectionTypeLoadException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
                //Display or log the error based on your application.
            }
            
        }

        private static bool IsMapperConfigInterface(Type type)
        {
            var isGenericMapping = type.IsGenericType &&
                         (type.GetGenericTypeDefinition() == typeof(IMapFrom<>) || type.GetGenericTypeDefinition() == typeof(IMapTo<>));

            var isCustomMapping = typeof(IHaveCustomMappings).IsAssignableFrom(type);

            return isGenericMapping || isCustomMapping;
        }

        private static void Execute(Type[] types)
        {
            Configuration = new MapperConfiguration(
                cfg =>
                {
                    LoadStandardMappings(types, cfg);
                    LoadReverseMappings(types, cfg);
                    LoadCustomMappings(types, cfg);
                });
        }

        private static void LoadStandardMappings(IEnumerable<Type> types, IMapperConfiguration mapperConfiguration)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                mapperConfiguration.CreateMap(map.Source, map.Destination);
            }
        }

        private static void LoadReverseMappings(IEnumerable<Type> types, IMapperConfiguration mapperConfiguration)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Destination = i.GetGenericArguments()[0],
                            Source = t
                        }).ToArray();

            foreach (var map in maps)
            {
                mapperConfiguration.CreateMap(map.Source, map.Destination);
            }
        }

        private static void LoadCustomMappings(IEnumerable<Type> types, IMapperConfiguration mapperConfiguration)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                              !t.IsAbstract &&
                              !t.IsInterface &&
                              t.GetConstructor(Type.EmptyTypes) != null
                        select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(mapperConfiguration);
            }
        }
    }
}