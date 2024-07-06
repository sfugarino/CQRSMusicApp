﻿using Music.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.Repositories
{
    public interface IArtistRepository
    {
        Task<Artist[]> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Artist?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        void Add(Artist artist);
    }
}
