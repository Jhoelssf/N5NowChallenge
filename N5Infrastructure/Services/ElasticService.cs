using Application.Common.Services;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using N5Domain.Entities;
using Nest;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class ElasticProducer : IElasticProducer
    {
        private readonly IElasticClient _elasticClient;

        public ElasticProducer(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<string> IndexPermissionDocumentAsync(Permission permission, CancellationToken cancellationToken)
        {
            var elasticResponse = await _elasticClient.IndexAsync(permission, x => x.Index("n5_elastic_index"), cancellationToken);
            return elasticResponse.Id;
        }
    }
}