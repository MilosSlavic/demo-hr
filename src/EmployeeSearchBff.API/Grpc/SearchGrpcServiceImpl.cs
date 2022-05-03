using Grpc.Core;

namespace EmployeeSearchBff.API.Grpc
{
    public class SearchGrpcServiceImpl : SearchGrpcService.SearchGrpcServiceBase
    {
        public override Task<SearchReply> Search(SearchMessage request, ServerCallContext context)
        {
            return base.Search(request, context);
        }
    }
}