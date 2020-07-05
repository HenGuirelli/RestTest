using RestTest.Library.Entity;
using RestTest.Library.Entity.Test;
using RestTest.RestRequest;

namespace RestTest.Library.SequenceDependency.ReplaceDependency
{
    public interface IReplaceDependency
    {
        void Replace(RequestConfig requestConfig);
        void Replace(Validation validation);
    }
}
