﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace web1
{
    public class Startup
    {
        static ReaderWriterLockSlim lock1 = new ReaderWriterLockSlim();

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) { }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async(context) =>
            {
                lock1.EnterWriteLock();

                try
                {
                    await File.AppendAllTextAsync("G:\\a.log", DateTime.Now + "\n");
                }
                finally
                {
                    lock1.ExitWriteLock();
                }

                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}