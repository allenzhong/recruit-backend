using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using CardApplication.Application.Handlers;
using CardApplication.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using MediatR.Registration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace CardApplication.Test.Extensions
{
    [Trait("Category", "Unittest")]
    public class DependencyInjectionConfigTests
    {       
        [Fact]
        public void CanResolveRepositories()
        {
           TryResolveTypes(new List<Type>()
           {
               typeof(ICreditCardRepository)
           }); 
        }

        [Fact]
        public void CanResolveDbConnection()
        {
            TryResolveTypes(new List<Type>()
            {
                typeof(IDbConnection)
            });  
        }
        
        [Fact]
        public void CanAllRequestHandlersBeCreated()
        {
            var requestHandlers =
                Assembly.GetAssembly(typeof(CardRegisterCommandHandler))
                    .GetTypes()
                    .Where(t => IsAssignableToGenericType(t, typeof(IRequestHandler<,>)))
                    .SelectMany(t => t.GetInterfaces())
                    .Where(t => t.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
            
            TryResolveTypes(requestHandlers);
        }

        private void IsAssignableToGenericType(object o, Type type)
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void CanResolveAllFluentValidators()
        {
            var validators = 
                Assembly.GetAssembly(typeof(CardRegisterCommand))
                    .GetTypes()
                    .Where(t => !t.IsAbstract && IsAssignableToGenericType(t, typeof(IValidator<>)))
                    .SelectMany(t => t.GetInterfaces())
                    .Where(t => t.IsGenericType && !typeof(IEnumerable).IsAssignableFrom(t) && !t.IsOpenGeneric())
                    .ToList();

            TryResolveTypes(validators);
        }

        private static void TryResolveTypes(IEnumerable<Type> types)
        {
            var host = Program.CreateHostBuilder(new string[]{})
                .UseEnvironment("Development").Build();
            var services = host.Services.CreateScope().ServiceProvider;
            
            var errorBuilder = new StringBuilder();
            foreach (var serviceType in types)
            {
                try
                {
                    services.GetRequiredService(serviceType);
                }
                catch (Exception ex)
                {
                    errorBuilder.AppendLine(ex.Message);
                }
            }

            Assert.Equal("", errorBuilder.ToString());
        } 
        private static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, genericType);
        }
    }
}