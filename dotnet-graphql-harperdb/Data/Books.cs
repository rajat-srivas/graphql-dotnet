
using HotChocolate.AspNetCore.Authorization;

namespace GraphQL.Data
{
	[Authorize]
	public class Book
	{
		public string Isbn { get; set; }
		public string Title { get; set; }
		public int Pages { get; set; }
		public string Description { get; set; }
	}
}
