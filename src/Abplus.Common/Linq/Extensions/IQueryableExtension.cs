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

    public static class IQueryableExtension
    {
        /// <summary>
        /// 根据requestInput进行排序、分页。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestInput">查询参数</param>
        /// <returns></returns>
        public static IQueryable<T> SortAndPageBy<T>(this IQueryable<T> query, IPagedQueryInput requestInput = null) where T : class
        {
            if (requestInput != null)  //有排序、分页参数
            {
                if (!string.IsNullOrWhiteSpace(requestInput.Sorting))
                {
                    query = query.OrderBy(requestInput.Sorting);
                }
                query = query.PageBy(requestInput);
            }

            return query;
        }

        /// <summary>
        /// 转换为指定Dto类型的IQueryable
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="query"></param>
        /// <param name="membersToExpand"></param>
        /// <returns></returns>
        public static IQueryable<TDto> ProjectTo<TSource, TDto>(this IQueryable<TSource> query, params Expression<Func<TDto, object>>[] membersToExpand)
        {
            return query.ProjectTo<TDto>(membersToExpand);
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
            query = query.SortAndPageBy(requestInput);
            var result = new PagedResultDto<TDto>()
            {
                Items = await query.ProjectTo<TDto>().ToListAsync()
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

        ///// <summary>
        ///// 生成一个新的查询表达式，用于支持调用To方法时，只需要指定目标Dto类型，而不需要再指定源实体类型
        ///// 根据requestInput进行排序、分页。
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="requestInput">传递查询参数的对象</param>
        ///// <returns></returns>
        //public static QueryExpression<TSource> Query<TSource>(this IQueryable<TSource> query, IPagedQueryInput requestInput = null)
        //    where TSource : class
        //{
        //    var expression = new QueryExpression<TSource>(query, requestInput);
        //    return expression;
        //}

    }

    //public class QueryExpression<TSource>
    //    where TSource : class
    //{
    //    private IQueryable<TSource> _source;
    //    private IPagedQueryInput _requestInput;

    //    public QueryExpression(IQueryable<TSource> source, IPagedQueryInput requestInput = null)
    //    {
    //        this._source = source;
    //        this._requestInput = requestInput;
    //    }

    //    /// <summary>
    //    /// 返回添加排序和分页后的IQueryable
    //    /// </summary>
    //    /// <typeparam name="TDto"></typeparam>
    //    /// <returns></returns>
    //    public IQueryable<TSource> To()
    //    {
    //        return GetQuery(_source);
    //    }

    //    /// <summary>
    //    /// 转换指定Dto类型的IQueryable
    //    /// </summary>
    //    /// <typeparam name="TDto"></typeparam>
    //    /// <returns></returns>
    //    public IQueryable<TDto> To<TDto>() where TDto : class
    //    {
    //        var query = GetQuery(_source);

    //        return query.ProjectTo<TDto>();
    //    }


    //    /// <summary>
    //    /// 生成指定Dto类型的分页结果
    //    /// </summary>
    //    /// <typeparam name="TDto">输出的Dto类型</typeparam>
    //    /// <returns></returns>
    //    public PagedResultDto<TDto> ToPagedResult<TDto>() where TDto : class
    //    {
    //        return AsyncHelper.RunSync(() => ToPagedResultAsync<TDto>());
    //    }

    //    /// <summary>
    //    /// 生成指定Dto类型的分页结果
    //    /// </summary>
    //    /// <typeparam name="TDto">输出的Dto类型</typeparam>
    //    /// <returns></returns>
    //    public async Task<PagedResultDto<TDto>> ToPagedResultAsync<TDto>() where TDto : class
    //    {
    //        return await _source.ToPagedResultAsync<TSource, TDto>(_requestInput);
    //    }


    //    /// <summary>
    //    /// 生成指定Dto类型的列表
    //    /// </summary>
    //    public List<TDto> ToList<TDto>() where TDto : class
    //    {
    //        return AsyncHelper.RunSync(() => ToListAsync<TDto>());
    //    }

    //    /// <summary>
    //    /// 生成指定Dto类型的列表
    //    /// </summary>
    //    public async Task<List<TDto>> ToListAsync<TDto>() where TDto : class
    //    {
    //        var query = GetQuery(_source);

    //        return await query.ProjectTo<TDto>().ToListAsync();
    //    }

    //    /// <summary>
    //    /// 生成原类型的列表
    //    /// </summary>
    //    public List<TSource> ToList()
    //    {
    //        return AsyncHelper.RunSync(() => ToListAsync());
    //    }

    //    /// <summary>
    //    /// 生成原类型的列表
    //    /// </summary>
    //    public async Task<List<TSource>> ToListAsync()
    //    {
    //        var query = GetQuery(_source);

    //        return await query.ToListAsync();
    //    }

    //    private IQueryable<T> GetQuery<T>(IQueryable<T> query)
    //    {
    //        if (_requestInput != null)  //不排序、分页参数
    //        {
    //            if (!string.IsNullOrWhiteSpace(_requestInput.Sorting))
    //            {
    //                query = query.OrderBy(_requestInput.Sorting);
    //            }
    //            query = query.PageBy(_requestInput);
    //        }
    //        return query;
    //    }
    //}
}
