using Grpc.Core;

namespace Employee.API.Grpc
{
    public class EmployeeGrpcServiceImpl : EmployeeGrpcService.EmployeeGrpcServiceBase
    {
        public override Task<CreateReply> Create(CreateMessage request, ServerCallContext context)
        {
            return base.Create(request, context);
        }

        public override Task<GetByIdReply> GetById(GetByIdMessage request, ServerCallContext context)
        {
            return base.GetById(request, context);
        }

        public override Task<UpdateReply> Update(UpdateMessage request, ServerCallContext context)
        {
            return base.Update(request, context);
        }
    }
}