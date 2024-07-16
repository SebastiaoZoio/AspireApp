using BlazorBootstrap;

namespace AspireApp.Web.Services.Requests;

public class ListCollaboratorsRequest
{
    public IEnumerable<FilterItem> Filters { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortString { get; set; } = string.Empty;
    public SortDirection SortDirection { get; set; } = SortDirection.None;
}
