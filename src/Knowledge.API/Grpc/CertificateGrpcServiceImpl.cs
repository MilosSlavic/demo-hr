using Grpc.Core;
using Knowledge.API.Certificate;

namespace Knowledge.API.Grpc
{
    public class CertificateGrpcServiceImpl : CertificateGrpcService.CertificateGrpcServiceBase
    {
        public override Task<GetByEmployeeIdReply> GetByEmployeeId(GetByEmployeeIdMessage request, ServerCallContext context)
        {
            return base.GetByEmployeeId(request, context);
        }

        public override Task<HasCertificateReply> HasCertificate(HasCertificateMessage request, ServerCallContext context)
        {
            return base.HasCertificate(request, context);
        }
    }
}