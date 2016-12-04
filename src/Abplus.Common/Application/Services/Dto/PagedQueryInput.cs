namespace Abp.Application.Services.Dto
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PagedQueryInput : IPagedQueryInput
    {
        const int MaxPageSize = 100;
        const int DefaultPageSize = 10;

        public string Keyword { get; set; }

        [Range(1, MaxPageSize)]
        public int MaxResultCount { get; set; }

        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }

        public string Sorting { get; set; }

        public string Filter { get; set; }

        public PagedQueryInput()
        {
            MaxResultCount = DefaultPageSize;
        }

    }
}
