using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationCenter.Inputs.Authorization.Services
{
    public class OnlyApiKeyAuthorizationHandler : AuthorizationHandler<OnlyApiKeyAuthorizationRequirement>
    {

        private readonly IHttpContextAccessor contextAccessor;

        public OnlyApiKeyAuthorizationHandler(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OnlyApiKeyAuthorizationRequirement requirement)
        {
            /*if (context.User.IsInRole(Roles.Manager))
            {*/
                context.Succeed(requirement);
            //}

            return Task.CompletedTask;
        }
    }
}
