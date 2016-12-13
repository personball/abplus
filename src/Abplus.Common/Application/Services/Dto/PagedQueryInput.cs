namespace Abp.Application.Services.Dto
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PagedQueryInput : IPagedQueryInput
    {
        const int MaxPageSize = 500;
        const int DefaultPageSize = 10;

        [Range(1, MaxPageSize)]
        public virtual int MaxResultCount { get; set; }

        [Range(0, int.MaxValue)]
        public virtual int SkipCount { get; set; }

        public virtual string Sorting { get; set; }

        public virtual string Filter { get; set; }

        public PagedQueryInput()
        {
            MaxResultCount = DefaultPageSize;
        }

    }
}
