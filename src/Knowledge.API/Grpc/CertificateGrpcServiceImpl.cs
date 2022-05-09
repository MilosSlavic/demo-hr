using Grpc.Core;
using Knowledge.API.Certificate;
using Microsoft.EntityFrameworkCore;

namespace Knowledge.API.Grpc
{
    public class CertificateGrpcServiceImpl : CertificateGrpcService.CertificateGrpcServiceBase
    {
        private readonly KnowledgeDbContext _dbContext;

        public CertificateGrpcServiceImpl(KnowledgeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<GetByEmployeeIdReply> GetByEmployeeId(GetByEmployeeIdMessage request, ServerCallContext context)
        {
            var cerificates = await _dbContext.Certificates.Where(x => x.EmployeeId == request.EmployeeId).ToListAsync();
            return new GetByEmployeeIdReply
            {
                Items =
                {
                    cerificates.Select(x => new CertificateModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        EmployeeId = x.EmployeeId,
                        Completness = x.Completness
                    })
                }
            };
        }

        public override async Task<CreateReply> Create(CreateMessage request, ServerCallContext context)
        {
            var certificate = new Entities.Certificate
            {
                Name = request.Name,
                EmployeeId = request.EmployeeId,
                Completness = request.Completness
            };
            _dbContext.Certificates.Add(certificate);
            await _dbContext.SaveChangesAsync();
            return new CreateReply
            {
                Id = certificate.Id
            };
        }

        public override async Task<HasCertificateReply> HasCertificate(HasCertificateMessage request, ServerCallContext context)
        {
            var hasAny = await _dbContext.Certificates.AnyAsync(x => x.EmployeeId == request.EmployeeId);
            return new HasCertificateReply
            {
                Yes = hasAny
            };
        }
    }
}