﻿using TaskManager.Core.DataAccess;
using TaskManager.Core.Entities.Concrete;

namespace TaskManager.DataAccess.Abstract
{
    public interface IRefreshTokenDal : IEntityRepository<RefreshToken>
    {
    }
}
