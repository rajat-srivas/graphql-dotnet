using HarperNetClient.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Context
{
	public interface IHarperConfiguration
	{
		HarperDbConfiguration GetHarperConfigurations();
	}
}
