using System.IO;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Services
{
    public abstract class ImportService<TEntity>
        where TEntity : IEntity
    {
        protected abstract bool SkipFirstLine { get; }

        protected abstract char ColumnDelimiter { get; }

        protected virtual Task AfterImportAsync()
        {
            return Task.CompletedTask;
        }

        public virtual async Task ImportAsync(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                await ReadAsync(reader);
            }
        }

        protected virtual async Task ReadAsync(StreamReader reader)
        {
            await AfterImportAsync();
        }
    }
}
