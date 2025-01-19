using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace MyLibraryMVC.Application.Mapping
{
	public class MappingProfile : Profile 
	{
		public MappingProfile() 
		{
			ApplyMappingFromAssembly(Assembly.GetExecutingAssembly());	
		}
		private void ApplyMappingFromAssembly(Assembly assembly)
		{
			var types = assembly.GetTypes()
				.Where(t=>t.GetInterfaces().Any(i=>
				i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
				.ToList();
			foreach (var type in types)
			{
				var instance = Activator.CreateInstance(type);
				var methodInfo = type.GetMethod("Mapping");
				methodInfo?.Invoke(instance, [this] );
			}
		}

	}
}
