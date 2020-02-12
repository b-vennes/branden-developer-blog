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
        private readonly ILogger<AdminContentService> _logger;
        private readonly IContentService _contentService;

        public AdminContentService(ILogger<AdminContentService> logger,  IContentService contentService)
        {
            _logger = logger;
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

            var success = false;
            var errorMessage = "";

            try
            {
                await _contentService.Publish(publishDto);
                success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while publishing content with id {request.Id}: {ex}");
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

            var success = false;
            var errorMessage = "";

            try
            {
                await _contentService.Update(request.Id, updateDto);
                success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating content with id {request.Id}: {ex}");
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
            var success = false;
            var errorMessage = "";

            try
            {
                await _contentService.Delete(request.Id);
                success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating content with id {request.Id}: {ex}");
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
