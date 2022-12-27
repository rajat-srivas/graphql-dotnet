using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Data
{
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
