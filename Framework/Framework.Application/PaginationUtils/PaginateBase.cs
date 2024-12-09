using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Application.PaginationUtils;


public class PaginateBase<T>
{
    public int EntityCount { get; private set; }
    public int CurrentPage { get; private set; }
    public int PageCount { get; private set; }
    public int StartPage { get; private set; }
    public int EndPage { get; private set; }
    public int Take { get; private set; }

    public IQueryable<T> GeneratePaging(IQueryable<T> data, int take, int currentPage)
    {
        if (take <= 0) throw new ArgumentException("Take must be greater than zero.", nameof(take));
        if (currentPage <= 0) throw new ArgumentException("Current page must be greater than zero.", nameof(currentPage));

        EntityCount = data.Count();
        Take = take;
        PageCount = (int)Math.Ceiling(EntityCount / (double)take);
        CurrentPage = currentPage > PageCount ? PageCount : currentPage;

        StartPage = Math.Max(1, CurrentPage - 4);
        EndPage = Math.Min(PageCount, CurrentPage + 5);
        return data.Skip((CurrentPage - 1) * Take).Take(Take);
    }

    public object GetPagingSummary()
    {
        return new
        {
            EntityCount,
            CurrentPage,
            PageCount,
            StartPage,
            EndPage,
            Take
        };
    }
}


//var paginator = new PaginateBase<User>();
//var pagedData = paginator.GeneratePaging(users.AsQueryable(), 10, 3); // 10