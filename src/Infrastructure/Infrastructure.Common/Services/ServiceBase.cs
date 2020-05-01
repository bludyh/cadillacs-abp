using Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services
{
    public abstract class ServiceBase
    {

        protected readonly DbContext _context;

        protected ServiceBase(DbContext context)
        {
            _context = context;
        }

        protected async Task<TEntity> ValidateExistenceAsync<TEntity>(params object[] keyValues) where TEntity : class
        {
            if (!(await _context.FindAsync<TEntity>(keyValues) is TEntity entity))
                throw new HttpResponseException(
                    message: $"{typeof(TEntity).Name} '{JsonConvert.SerializeObject(keyValues)}' does not exist.",
                    status: StatusCodes.Status404NotFound);

            return entity;
        }

        protected async Task ValidateDuplicationAsync<TEntity>(params object[] keyValues) where TEntity : class
        {
            if (await _context.FindAsync<TEntity>(keyValues) is TEntity)
                throw new HttpResponseException(
                    message: $"{typeof(TEntity).Name} '{JsonConvert.SerializeObject(keyValues)}' already exists.",
                    status: StatusCodes.Status409Conflict);
        }

        protected async Task ValidateForeignKeyAsync<TEntity>(params object[] keyValues) where TEntity : class
        {
            if (keyValues.All(kv => kv != null) && !(await _context.FindAsync<TEntity>(keyValues) is TEntity))
                throw new HttpResponseException(
                    message: $"Foreign key constraint is violated: {typeof(TEntity).Name} '{JsonConvert.SerializeObject(keyValues)}'.",
                    status: StatusCodes.Status422UnprocessableEntity);
        }

        protected void Validate(bool condition, string message = null, int status = 500)
        {
            if (condition)
                throw new HttpResponseException(message, status);
        }
    }
}
