namespace Abp.Application.Services.Dto
{
    public interface IPaginationResultRequest : ISortedResultRequest
    {
        int PageIndex { get; set; }

        int PageSize { get; set; }
    }
}
