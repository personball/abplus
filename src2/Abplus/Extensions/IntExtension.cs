using System;
using Abp.Application.Services.Dto;

namespace Abp.Extensions
{
    public static class IntExtension
    {
        public static int ToSkipCount(this IPaginationResultRequest pagination)
        {
            CheckErrors(pagination);

            return (pagination.PageIndex - 1) * pagination.PageSize;
        }

        public static int ToMaxResultCount(this IPaginationResultRequest pagination)
        {
            CheckErrors(pagination);

            return pagination.PageSize;
        }

        private static void CheckErrors(IPaginationResultRequest pagination)
        {
            if (pagination == null)
            {
                throw new ArgumentNullException("pagination");
            }

            if (pagination.PageSize < 1)
            {
                throw new ArgumentOutOfRangeException("pagesize");
            }

            if (pagination.PageIndex < 1)
            {
                throw new ArgumentOutOfRangeException("pageindex");
            }
        }
    }
}
