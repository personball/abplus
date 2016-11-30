namespace Abp.Application.Services.Dto
{
    using System.ComponentModel.DataAnnotations;

    public class PaginationResultRequestInput : IPaginationResultRequest
    {
        public PaginationResultRequestInput()
        {
            PageIndex = 1;
            PageSize = 10;
        }

        [Range(1, 1000)]
        public int PageIndex { get; set; }

        [Range(1, 1000)]
        public int PageSize { get; set; }

        public string Sorting { get; set; }
    }
}
