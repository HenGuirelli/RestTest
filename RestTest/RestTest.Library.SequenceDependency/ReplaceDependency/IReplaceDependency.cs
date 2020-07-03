using RestTest.Library.Entity;
using RestTest.RestRequest;

namespace RestTest.Library.SequenceDependency.ReplaceDependency
{
    public interface IReplaceDependency
    {
        void Replace(RequestConfig requestConfig);
        void Replace(Validation validation);
    }
}
