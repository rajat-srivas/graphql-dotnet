using dotnet_graphql_harperdb.GraphQL.Schema.MutationSchema;
using FluentValidation;

namespace dotnet_graphql_harperdb.Helpers.Validators
{
	public class SpeakerTypeValidator : AbstractValidator<SpeakerType>
	{
		public SpeakerTypeValidator()
		{
			RuleFor(x => x.Name).NotEmpty().MinimumLength(10).MaximumLength(100).WithMessage("Name should be between 10 and 100 characters");
		}
	}
}
