using HotChocolate.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GraphQL.Data
{
	[Authorize]
	public class Speaker
	{
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[Required]
		[StringLength(200)]
		public string Name { get; set; }

		[StringLength(4000)]
		public string Bio { get; set; }
		public List<Book> Books { get; set; }

	}
}
