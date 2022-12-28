using GraphQL.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnet_graphql_harperdb.GraphQL.Schema.MutationSchema
{
	public class SpeakerType
	{
		[Required]
		[StringLength(200)]
		public string Name { get; set; }

		[StringLength(4000)]
		public string Bio { get; set; }
		public List<BookType> Books { get; set; }
	}

}
