using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Registration;

namespace Pumpkin.Core
{
    public static class DynamicallyInstaller
    {
        private static IEnumerable<Type> AllTypes
        {
            get { return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()); }
        }

        public static void NeedToInstallConfigExtension(this IServiceCollection services)
        {
            var typesToRegister = AllTypes
                .Where(it => !(it.IsAbstract || it.IsInterface)
                             && typeof(INeedToInstall).IsAssignableFrom(it));

            foreach (var item in typesToRegister)
            {
                var service = (INeedToInstall) Activator.CreateInstance(item);

                service.Install(services);
            }
        }

        public static void NeedToMappingConfigExtension(this ModelBuilder modelBuilder)
        {
            var typesToRegister = AllTypes
                .Where(type =>
                    type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() ==
                    typeof(IEntityTypeConfiguration<>));

            foreach (var item in typesToRegister)
            {
                dynamic service = Activator.CreateInstance(item);

                modelBuilder.ApplyConfiguration(service);
            }
        }
    }
}