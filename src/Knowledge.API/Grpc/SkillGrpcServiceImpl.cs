using Grpc.Core;
using Knowledge.API.Skill;

namespace Knowledge.API.Grpc
{
    public class SkillGrpcServiceImpl : SkillGrpcService.SkillGrpcServiceBase
    {
        public override Task<GetByEmployeeIdReply> GetByEmployeeId(GetByEmployeeIdMessage request, ServerCallContext context)
        {
            return base.GetByEmployeeId(request, context);
        }

        public override Task<HasSkillsReply> HasSkills(HasSkillsMessage request, ServerCallContext context)
        {
            return base.HasSkills(request, context);
        }
    }
}
