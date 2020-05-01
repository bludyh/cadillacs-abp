using Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
                    message: $"{typeof(TEntity).Name} does not exist.",
                    status: StatusCodes.Status404NotFound);

            return entity;
        }

        protected async Task ValidateDuplicationAsync<TEntity>(params object[] keyValues) where TEntity : class
        {
            if (await _context.FindAsync<TEntity>(keyValues) is TEntity)
                throw new HttpResponseException(
                    message: $"{typeof(TEntity).Name} already exists.",
                    status: StatusCodes.Status409Conflict);
        }

        protected async Task<TEntity> ValidateForeignKeyAsync<TEntity>(params object[] keyValues) where TEntity : class
        {
            TEntity entity = null;
            if (keyValues.All(kv => kv != null))
            {
                entity = await _context.FindAsync<TEntity>(keyValues); 

                if (!(entity is TEntity))
                    throw new HttpResponseException(
                        message: $"Foreign key constraint is violated: {typeof(TEntity).Name}.",
                        status: StatusCodes.Status422UnprocessableEntity);
            }

            return entity;
        }

        protected void Validate(bool condition, string message = null, int status = 500)
        {
            if (condition)
                throw new HttpResponseException(message, status);
        }
    }
}
