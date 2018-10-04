using AutoMapper.QueryableExtensions;
namespace Abp.Linq.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Threading.Tasks;
    using Abp.Application.Services.Dto;
    using Abp.Linq.Extensions;
    using Abp.Threading;
    using Domain.Entities;
    using System.Linq.Expressions;
    using Abp.Extensions;

    public static class IQueryableExtension
    {
        /// <summary>
        /// 应用requestInput指定的排序参数
        /// </summary>
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, ISortedResultRequest requestInput = null) where T : class
        {
            if (requestInput != null && !requestInput.Sorting.IsNullOrWhiteSpace())  //有排序参数
            {
                return query.OrderBy(requestInput.Sorting);
            }
            return query;
        }

        /// <summary>
        /// 应用requestInput指定的分页参数
        /// </summary>
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IPagedResultRequest requestInput = null) where T : class
        {
            if (requestInput != null)
            {
                return query.PageBy(requestInput);
            }
            return query;
        }

        /// <summary>
        /// 根据requestInput进行排序、分页。
        /// </summary>
        public static IQueryable<T> ApplySortingAndPaging<T>(this IQueryable<T> query, IPagedQueryInput requestInput = null) where T : class
        {
            query = query.ApplySorting(requestInput);
            query = query.ApplyPaging(requestInput);

            return query;
        }

        /// <summary>
        /// 转换为指定Dto类型的IQueryable
        /// </summary>
        public static IQueryable<TDto> ProjectTo<TSource, TDto>(this IQueryable<TSource> query, params Expression<Func<TDto, object>>[] membersToExpand)
        {
            return query.ProjectTo<TDto>(membersToExpand);
        }

        /// <summary>
        /// 只查Dto类型指定字段的列表数据
        /// </summary>
        /// <typeparam name="TSource">实体类型</typeparam>
        /// <typeparam name="TDto">列表Dto类型</typeparam>
        /// <param name="query"></param>
        /// <param name="requestInput">查询参数（指定排序列）</param>
        /// <returns>查询出Dto类型指定字段的列表数据</returns>
        public static async Task<ListResultDto<TDto>> ToListResultAsync<TSource, TDto>(this IQueryable<TSource> query, ISortedResultRequest requestInput = null)
            where TSource : class
            where TDto : class
        {
            query = query.ApplySorting(requestInput);

            var result = new ListResultDto<TDto>()
            {
                Items = await query.ProjectTo<TDto>().ToListAsync()
            };

            return result;
        }

        /// <summary>
        /// 只查Dto类型指定字段的列表数据
        /// </summary>
        /// <typeparam name="TSource">实体类型</typeparam>
        /// <typeparam name="TDto">列表Dto类型</typeparam>
        /// <param name="query"></param>
        /// <param name="requestInput">查询参数（指定排序列）</param>
        /// <returns>查询出Dto类型指定字段的列表数据</returns>
        public static ListResultDto<TDto> ToListResult<TSource, TDto>(this IQueryable<TSource> query, ISortedResultRequest requestInput = null)
            where TSource : class
            where TDto : class
        {
            return AsyncHelper.RunSync(() => query.ToListResultAsync<TSource, TDto>(requestInput));
        }

        /// <summary>
        /// 根据requestInput进行排序、分页后，转换为分页查询结果
        /// </summary>
        /// <typeparam name="TSource">原Entity类型</typeparam>
        /// <typeparam name="TDto">目标Dto类型</typeparam>
        /// <param name="query"></param>
        /// <param name="requestInput">查询参数</param>
        /// <returns></returns>
        public static async Task<PagedResultDto<TDto>> ToPagedResultAsync<TSource, TDto>(this IQueryable<TSource> query, IPagedQueryInput requestInput = null)
            where TSource : class
            where TDto : class
        {
            var pagedQuery = query.ApplySortingAndPaging(requestInput);
            var result = new PagedResultDto<TDto>()
            {
                Items = await pagedQuery.ProjectTo<TDto>().ToListAsync()
            };

            //没有分页参数，或者第1页的结果不足一整页时，不需要统计总记录数
            if (requestInput == null || (requestInput.SkipCount == 0 && result.Items.Count < requestInput.MaxResultCount))
            {
                result.TotalCount = result.Items.Count;
            }
            else
            {
                result.TotalCount = await query.CountAsync();
            }
            return result;
        }

        /// <summary>
        /// 根据requestInput进行排序、分页后，转换为分页查询结果
        /// </summary>
        /// <typeparam name="TSource">原Entity类型</typeparam>
        /// <typeparam name="TDto">目标Dto类型</typeparam>
        /// <param name="query"></param>
        /// <param name="requestInput">查询参数</param>
        /// <returns></returns>
        public static PagedResultDto<TDto> ToPagedResult<TSource, TDto>(this IQueryable<TSource> query, IPagedQueryInput requestInput = null)
            where TSource : class
            where TDto : class
        {
            return AsyncHelper.RunSync(() => query.ToPagedResultAsync<TSource, TDto>(requestInput));
        }

    }
}
