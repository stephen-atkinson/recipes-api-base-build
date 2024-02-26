using Recipes.Core.Domain;

namespace Recipes.Api.Models.Requests;

public class GetGroupsRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public Course? Course { get; set; }
    public Diet? Diet { get; set; }
}