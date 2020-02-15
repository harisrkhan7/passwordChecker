using System;
using Microsoft.Extensions.DependencyInjection;
using passwordChecker.Core.Services.Implementations;
using passwordChecker.Core.Services.Interfaces;

namespace passwordChecker.Core
{
    /// <summary>
    /// Manages application dependencies for current dll
    /// </summary>
    public static class CoreDependencyInjection
    {
        /// <summary>
        /// Adds all the dependecies required by this dll to
        /// the IoC container. 
        /// </summary>
        /// <param name="services">Service collection to update</param>
        public static void AddPasswordCheckerCore(this IServiceCollection services)
        {
            services.AddScoped<IPasswordChecker, PasswordChecker>();
        }
    }
}
