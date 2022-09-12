﻿using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Extensions;
using Riode.WebUI.Models.DAL;

namespace Riode.WebUI.AppCode.Application.ProductColorModule
{
    public class ProductColorEditCommand:ProductColorViewModel, IRequest<int>
    {
        public class ProductColorEditCommandHandler : IRequestHandler<ProductColorEditCommand, int>
        {
            private readonly RiodeDbContext _db;
            private readonly IActionContextAccessor _ctx;
            public ProductColorEditCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                _db = db;
                _ctx = ctx;
            }

            public async Task<int> Handle(ProductColorEditCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id < 0)
                    return 0;
                var entity = await _db.ProductColors.FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedByUserId == null);
                if (entity == null)
                    return 0;
                if (_ctx.IsModelStateValid())
                {
                    entity.HexCode = request.HexCode;
                    entity.Name = request.Name;
                    entity.Description = request.Description;
                    await _db.SaveChangesAsync(cancellationToken);
                    return entity.Id;
                }
                return 0;
            }
        }
    }
}