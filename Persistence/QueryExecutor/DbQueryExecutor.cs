using Microsoft.Extensions.Logging;
using Nami.DXP.Common;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Nami.DXP.Persistence
{
    public abstract class DbQueryExecutor
    {
        private readonly ILogger _logger;

        protected DbQueryExecutor(ILogger logger)
        {
            _logger = logger;
        }

        protected T ExecuteSafely<T>(Func<T> codeBlock)
        {
            T returnValue;

            try
            {
                returnValue = codeBlock(); 
            }
            catch(TimeoutException timeoutException)
            {
                _logger.LogError(timeoutException, string.Empty);
                throw new WebAppException(ErrorCode.InternalError, "A query executor timeout error occurred.");
            }
            catch(DbException dbException)
            {
                _logger.LogError(dbException, string.Empty);
                throw new WebAppException(ErrorCode.InternalError, "A query executor error occurred.");
            }

            return returnValue;
        }

        protected async Task<T> ExecuteSafelyAsync<T>(Func<Task<T>> codeBlock)
        {
            T returnValue;

            try
            {
                returnValue = await codeBlock();
            }
            catch (TimeoutException timeoutException)
            {
                _logger.LogError(timeoutException, string.Empty);
                throw new WebAppException(ErrorCode.InternalError, "A query executor timeout error occurred.");
            }
            catch (DbException dbException)
            {
                _logger.LogError(dbException, string.Empty);
                throw new WebAppException(ErrorCode.InternalError, "A query executor error occurred.");
            }

            return returnValue;
        }

        protected async Task ExecuteSafelyAsync(Func<Task> codeBlock)
        {
            try
            {
                await codeBlock();
            }
            catch (TimeoutException timeoutException)
            {
                _logger.LogError(timeoutException, string.Empty);
                throw new WebAppException(ErrorCode.InternalError, "A query executor timeout error occurred.");
            }
            catch (DbException dbException)
            {
                _logger.LogError(dbException, string.Empty);
                throw new WebAppException(ErrorCode.InternalError, "A query executor error occurred.");
            }
        }
    }
}
