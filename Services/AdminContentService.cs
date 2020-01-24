using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using DevBlog.Protos;
using DevBlog.Dtos;

namespace DevBlog.Services
{
    public class AdminContentService : Content.ContentBase
    {
        private readonly IContentService _contentService;
        public AdminContentService(IContentService contentService)
        {
            _contentService = contentService;
        }

        public async override Task<Result> Publish(PublishRequest request, ServerCallContext context)
        {
            var publishDto = new PublishContentDto() {
                Id = request.Id,
                Title = request.Title,
                Url = request.Url,
                Format = request.Format,
                Hidden = request.Hidden
            };

            var success = await _contentService.Publish(publishDto);

            return new Result
            {
                Success = success,
                Message = ""
            };
        }

        public async override Task<Result> Update(UpdateRequest request, ServerCallContext context)
        {
            var updateDto = new UpdateContentDto() {
                Title = request.Title,
                Url = request.Url,
                Format = request.Format,
                Hidden = request.Hidden
            };

            var success = await _contentService.Update(request.Id, updateDto);

            return new Result
            {
                Success = success,
                Message = ""
            };
        }

        public async override Task<Result> Delete(DeleteRequest request, ServerCallContext context)
        {
            var success = await _contentService.Delete(request.Id);

            return new Result
            {
                Success = success,
                Message = ""
            };
        }
    }
}
