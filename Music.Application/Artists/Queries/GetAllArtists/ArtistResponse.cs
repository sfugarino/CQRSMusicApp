﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Artists.Queries.GetAllArtists
{
    public sealed record ArtistResponse(Guid ArtistId, string Name);
}
