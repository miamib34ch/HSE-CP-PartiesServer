namespace PartiesApi.Services.DressCode;

internal interface IDressCodeService
{
    Task<Models.DressCode?> GetDressCodeOrDefaultAsync(Guid dressCodeId);
}