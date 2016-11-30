namespace Abp.Application.Services.Dto
{
    using System;

    public interface IHasDateTimeFilterRequest
    {
        DateTime? StartTime { get; set; }

        DateTime? EndTime { get; set; }
    }
}
