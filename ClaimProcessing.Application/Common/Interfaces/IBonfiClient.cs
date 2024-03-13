﻿using ClaimProcessing.Shared.Claims.Commands.UpdateClaimStatus;

namespace ClaimProcessing.Application.Common.Interfaces
{
    public interface IBonfiClient
    {
        Task<UpdateClaimStatusVm> GetClaim(string searchFilter, CancellationToken cancellationToken);
        
    }
}
