using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SOCATemplate.Application
{
    public static class IQueryableExtensions
    {
        public static EntityEntry<T> Add<T>(this IQueryable<T> set, T newEntry) where T : class => ((DbSet<T>)set).Add(newEntry);

        public static T Find<T>(this IQueryable<T> set, params object[] keyValues) where T : class => ((DbSet<T>)set).Find(keyValues);
    }
}
