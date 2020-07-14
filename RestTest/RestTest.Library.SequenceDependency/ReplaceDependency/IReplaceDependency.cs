using RestTest.Library.Entity;
using RestTest.Library.Entity.Test;
using RestTest.RestRequest;
using System.Threading.Tasks;

namespace RestTest.Library.SequenceDependency.ReplaceDependency
{
    public interface IReplaceDependency
    {
        Task Replace(RequestConfig requestConfig);
        Task Replace(Validation validation);
    }
}
