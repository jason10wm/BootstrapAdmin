﻿using Bootstrap.Admin;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class OnlineUsersServicesCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        internal const string IPSvrHttpClientName = "IPSvr";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOnlineUsers(this IServiceCollection services)
        {
            services.TryAddSingleton<IOnlineUsers, DefaultOnlineUsers>();
            services.AddHttpClient(IPSvrHttpClientName, client =>
            {
                client.DefaultRequestHeaders.Connection.Add("keep-alive");
            });
            return services;
        }
    }
}