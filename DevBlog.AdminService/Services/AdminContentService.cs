using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using DevBlog.AdminService.Protos;
using DevBlog.Domain.Dtos;
using DevBlog.Domain.Services;

namespace DevBlog.AdminService.Services
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
                Summary = request.Summary,
                ImageUrl = request.ImageUrl,
                Format = request.Format,
                Hidden = request.Hidden
            };

            var success = true;
            var errorMessage = "";

            try
            {
                await _contentService.Publish(publishDto);
            }
            catch (Exception ex)
            {
                errorMessage = $"{ex}";
            }

            return new Result
            {
                Success = success,
                Message = errorMessage
            };
        }

        public async override Task<Result> Update(UpdateRequest request, ServerCallContext context)
        {
            var updateDto = new UpdateContentDto() {
                Title = request.Title,
                Url = request.Url,
                Summary = request.Summary,
                ImageUrl = request.ImageUrl,
                Format = request.Format,
                Hidden = request.Hidden
            };

            var success = true;
            var errorMessage = "";

            try
            {
                await _contentService.Update(request.Id, updateDto);
            }
            catch (Exception ex)
            {
                errorMessage = $"{ex}";
            }

            return new Result
            {
                Success = success,
                Message = errorMessage
            };
        }

        public async override Task<Result> Delete(DeleteRequest request, ServerCallContext context)
        {
            var success = true;
            var errorMessage = "";

            try
            {
                await _contentService.Delete(request.Id);
            }
            catch (Exception ex)
            {
                errorMessage = $"{ex}";
            }

            return new Result
            {
                Success = success,
                Message = errorMessage
            };
        }
    }
}
